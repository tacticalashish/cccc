using BusinessLayer;
using DataLayer;
using ChemAnalyst.Models;
using ClosedXML.Excel;
using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using ChemAnalyst.DAL;

namespace ChemAnalyst.Controllers
{
    public class TicketController : Controller
    {
        public ActionResult List()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetTicketBasedOnSearchFilter(enSearchFilter searchFilter)
        {
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enTicket> listOfTicket = new List<enTicket>();
            blTicket objBLTicket = new blTicket(new enTicket());

            try
            {
                //To get all  values
                listOfTicket = objBLTicket.ReadBasedOnSearchFilter(searchFilter);
            }
            catch (Exception ex)
            {
                return Json("f", JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfTicket != null && listOfTicket.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfTicket.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfTicket.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfTicket,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                PagerSize = (int)ApplicationSettings.PagerSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.PageSize,
                //SearchFilterHasValue = isSearchFilterHasValue,
                SearchFilterHasValue = string.Empty
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public FileResult GetTicketExcelExportBasedOnSearchFilter(enSearchFilter searchFilter)
        {

            searchFilter.StartRowNumber = 1;
            searchFilter.EndRowNumber = 999999;

            List<enTicket> listOfTicket = new List<enTicket>();

            try
            {
                //To get all  values
                listOfTicket = new blTicket(new enTicket()).ReadBasedOnSearchFilter(searchFilter);
            }
            catch (Exception ex)
            {
                //To capture the Exception
                //Log.Error(Helper.CRUDErrorMessage(CRUDMode.READ, null, ". Stack:\r\n" + ex.ToString()), false);
                listOfTicket = new List<enTicket>();
            }
            //Note: start to prepare csv
            DataTable dateTable = new DataTable("Report"); //excel sheet name

            dateTable.Columns.Add("S.No");
            dateTable.Columns.Add("Status");
            dateTable.Columns.Add("Client Name");
            dateTable.Columns.Add("Client Email");
            dateTable.Columns.Add("Last Updated On");
            dateTable.Columns.Add("Created On");


            //note: bind value into the column
            foreach (var item in listOfTicket)
            dateTable.Rows.Add(
                                    item.RowNumber,
                                    item.Status,
                                    item.CustomerName,
                                    item.CustomerEmail,
                                    item.UpdatedDateView,
                                    item.CreatedDateView
                                  );
            using (XLWorkbook xlWorkbook = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                xlWorkbook.Worksheets.Add(dateTable);
                using (MemoryStream memoryStream = new MemoryStream()) //using System.IO;  
                {
                    xlWorkbook.SaveAs(memoryStream);
                    return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelReportFile.xlsx");
                }
            }
        }

        public ActionResult TicketDetail(int id)
        {
            enTicket objENTicket = new enTicket { ID = id };
            try
            {
                new blTicket(objENTicket).Read();
            }
            catch (Exception ex)
            {
                return RedirectToAction("list");
            }

            try
            {
                objENTicket.ListOfTicketComments = new blTicketComment(new enTicketComment { TicketFID = id }).ReadAll();
            }
            catch (Exception ex)
            {
                return RedirectToAction("list");
            }
            foreach (var item in objENTicket.ListOfTicketComments)
                if (System.IO.File.Exists(Path.Combine(ApplicationSettings.TicketRootPath + item.FileName)))
                    item.IsFileNameExist = true;

            return View(objENTicket);
        }

        [HttpPost]
        public ActionResult TicketCommentCreate(enTicketComment modal)
        {
            try
            {
                //Note: check file exist or not and save file on related location 
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength != 0)
                    {
                        if (file != null)
                        {
                            modal.FileName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));
                            string filePath = string.Concat(ApplicationSettings.TicketRootPath);

                            if (!Directory.Exists(filePath))
                                Directory.CreateDirectory(filePath);

                            var path = string.Concat(filePath, "\\", modal.FileName);
                            file.SaveAs(path);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("list");
            }
            try
            {
                modal.Status = Utility.Helper.GetEnumDescription(typeof(Utility.TicketStatus), Convert.ToByte(Utility.TicketStatus.PENDING_CUSTOMER_RESPONSE));
                new blTicketComment(modal).Create();
            }
            catch (Exception ex)
            {
                return RedirectToAction("List");
            }


            //Note: Email Trigger to customer with updated comment and status
            EmailTriggerToCustomer(modal, "Updated ChemAnalyst Ticket");
            return RedirectToAction("TicketDetail", new { id = modal.TicketFID });
        }

        private void EmailTriggerToCustomer(enTicketComment modal, string title)
        {
            try
            {
                enTicket objENTicket = new enTicket { ID = modal.TicketFID };
                blTicket objBLTicket = new blTicket(objENTicket);
                objBLTicket.Read();

                string EmailBody = SubscriptionDAL.GetHtml("TicketComment.html");
                var customerDetails = new ChemAnalystContext().SA_Customers.ToList().Find(item => item.id == modal.UserFID);

                EmailBody = EmailBody.Replace("@TicketNumber@", string.Concat("CHEM00", objENTicket.ID));
                EmailBody = EmailBody.Replace("@CreatedOn@", objENTicket.CreatedDateView);
                EmailBody = EmailBody.Replace("@LastUpdatedOn@", objENTicket.UpdatedDateView);
                EmailBody = EmailBody.Replace("@Status@", objENTicket.Status);
                EmailBody = EmailBody.Replace("@Comment@", modal.Comment);
                EmailBody = EmailBody.Replace("@CustomerName@", objENTicket.CustomerName);
                EmailBody = EmailBody.Replace("@CustomerEmail@", objENTicket.CustomerEmail);

                string Toemail = objENTicket.CustomerEmail;
                string titleTicket = title + " :- CHEM00" + objENTicket.ID;

                //for testing
                //Toemail = "mahesh.kumar@techsciresearch.com";
                SubscriptionDAL.SendMail("Chem Analyst", "info@chemanalyst", Toemail, titleTicket, EmailBody, string.Empty);

            }
            catch (Exception ex)
            {

            }
        }

        public JsonResult ChangeTicketType(enTicket modal)
        {
            try
            {
                modal.ManagerFID = Convert.ToInt32(Session["LoginUser"]);
                modal.Status = "Pending Admin Response";
                new blTicket(modal).UpdateStatus();
            }
            catch (Exception ex)
            {
                return Json('f', JsonRequestBehavior.AllowGet);
            }

            //Note: Trigger email to related manager, content will be status has been changed 
            TriggerEmailToCustomerAndManager(modal);

            return Json('s', JsonRequestBehavior.AllowGet);
        }
        public JsonResult CloseTicket(enTicket modal)
        {
            try
            {
                modal.ManagerFID = Convert.ToInt32(Session["LoginUser"]);
                modal.Status = "Closed";
                new blTicket(modal).CloseTicket();
            }
            catch (Exception ex)
            {
                return Json('f', JsonRequestBehavior.AllowGet);
            }

            //Note: Trigger email to related manager, content will be status has been changed 
            TriggerEmailToCustomerForTicketClosed(modal);


            return Json('s', JsonRequestBehavior.AllowGet);
        }
        private void TriggerEmailToCustomerForTicketClosed(enTicket modal)
        {
            enTicket objENTicket = new enTicket { ID = modal.ID };
            blTicket objBLTicket = new blTicket(objENTicket);

            try
            {
                objBLTicket.Read();
            }
            catch (Exception)
            {

                throw;
            }

            //Note: tigger email
            try
            {
                enTicketComment objENenTicketComment = new enTicketComment();
                objENenTicketComment.TicketFID = modal.ID;
                objENenTicketComment.Comment = modal.Query;

                EmailTriggerToCustomer(objENenTicketComment, "Closed ChemAnalyst Ticket");
            }
            catch (Exception ex)
            {


            }

        }
        private void TriggerEmailToCustomerAndManager(enTicket modal)
        {
            enTicket objENTicket = new enTicket { ID = modal.ID };
            blTicket objBLTicket = new blTicket(objENTicket);

            try
            {
                objBLTicket.Read();
            }
            catch (Exception)
            {

                throw;
            }

            //Note: tigger email
            try
            {
                EmailTriggerToCustomer(new enTicketComment { TicketFID = modal.ID, Comment = modal.Query }, "Updated ChemAnalyst Ticket");


                //note: trigger to manager
                var role = new ChemAnalystContext().SA_Role.ToList().Find(item => item.Role.Equals(objENTicket.RoleName, StringComparison.CurrentCultureIgnoreCase));
                string EmailBody = SubscriptionDAL.GetHtml("TicketComment.html");

                EmailBody = EmailBody.Replace("@TicketNumber@", string.Concat("CHEM00", objENTicket.ID));
                EmailBody = EmailBody.Replace("@CreatedOn@", objENTicket.CreatedDateView);
                EmailBody = EmailBody.Replace("@LastUpdatedOn@", objENTicket.UpdatedDateView);
                EmailBody = EmailBody.Replace("@Status@", objENTicket.Status);
                EmailBody = EmailBody.Replace("@Comment@", modal.Query);
                EmailBody = EmailBody.Replace("@CustomerEmail@", modal.CustomerEmail);
                EmailBody = EmailBody.Replace("@CustomerName@", "Reporting Manager");

                string Toemail = role.ForwardingEmail;
                string titleTicket = "Transfer Ticket :- CHEM00" + objENTicket.ID;
                //for testing
                //Toemail = "mahesh.kumar@techsciresearch.com";
                SubscriptionDAL.SendMail("Chem Analyst", "info@chemanalyst", Toemail, titleTicket, EmailBody, string.Empty);
            }
            catch (Exception ex)
            {

            }

        }
        public JsonResult TicketNotificationBell()
        {

            List<enTicket> listOfTicket = new List<enTicket>();
            enSearchFilter searchFilter = new enSearchFilter();
            searchFilter.StartRowNumber = 1;
            searchFilter.EndRowNumber = 9999;

            //Note:for testing purpose comment below code
            string userRole = Convert.ToString(Session["UserRole"]);
            

            blTicket objBLTicket = new blTicket(new enTicket());

            try
            {
                //To get all  values
                listOfTicket = objBLTicket.ReadBasedOnSearchFilter(searchFilter);
            }
            catch (Exception ex)
            {
                listOfTicket = new List<enTicket>();
            }

            //Note: remove rest of tickets which is not created by current user
            listOfTicket.RemoveAll(item => !item.RoleName.Equals(userRole,StringComparison.CurrentCultureIgnoreCase));

            //Note: remove those tickets which manager sides
            listOfTicket.RemoveAll(item => item.IsManagerRead);


            return Json(listOfTicket, JsonRequestBehavior.AllowGet);
        }

        //UpdateTicketNotificationBell
        public JsonResult UpdateTicketNotificationBell()
        {
            //Note:for testing purpose comment below code
            string userRole = Convert.ToString(Session["UserRole"]);
            userRole = "Manager";

            enTicket objENTicket = new enTicket();
            objENTicket.RoleName = userRole;

            blTicket objBLTicket = new blTicket(objENTicket);
            try
            {
                objBLTicket.UpdateNotification();
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return Json("s", JsonRequestBehavior.AllowGet);
        }
    }
}