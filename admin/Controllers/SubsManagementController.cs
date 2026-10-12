using ChemAnalyst.DAL;
using ChemAnalyst.Models;
using ChemAnalyst.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedUpCoreAPIExample.Models;
using EntityLayer;
using BusinessLayer;
using System.Data;
using OfficeOpenXml;
using System.IO;
using Utility;
using EntityLayer.CustomEntity;

namespace ChemAnalyst.Controllers
{
    public class SubsManagementController : Controller
    {
        // GET: SubsManagement

        private ChemAnalystContext _contextDb = new ChemAnalystContext();

        private LeadDAL ObjLead;
        UserDataStore ObjUser = new UserDataStore();
        public SubsManagementController()
        {

            ObjLead = new LeadDAL();
        }

        public ActionResult LeadManagement()
        {
            return View("LeadManagementList");
        }

        public JsonResult GetLeadManagementList()
        {
            List<Lead_Master> leadList;
            string loggeduser = Session["User"].ToString().Trim();
            string items = Session["LoginUser"].ToString();
            List<SubscriptionViewModel> SubscriptionList;
            if (Session["UserRole"].ToString() == "Admin")
            {
                leadList = ObjLead.GetLeadList();
            }
            else
            {
                        
                leadList = ObjLead.GetLeadListforothers(items);
            }
            
            foreach (var item in leadList)
            {
                if (item.AssignTo == "NA" || item.AssignTo == null)
                {
                    item.AssignTo = "---";
                }
                else
                {
                    item.AssignTo = ObjUser.GetUserByid(Convert.ToInt32(item.AssignTo)) != null ? ObjUser.GetUserByid(Convert.ToInt32(item.AssignTo)).Fname : "NA";
                }
            }
            return Json(new { data = leadList }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ViewDetails(int Id)
        {


            LeadViewModel objLeadModel = new LeadViewModel();
            objLeadModel.StatusList = new List<SelectListItem> {
        new SelectListItem { Value = "Pending", Text = "Pending" },
        new SelectListItem { Value = "Assign", Text = "Assign" },
        new SelectListItem { Value = "Lost", Text = "Lost" } };

            objLeadModel.UserList = ObjLead.GetUserList();

            objLeadModel.hdId = Convert.ToString(Id);

            objLeadModel.LeadMaster = new Lead_Master();
            objLeadModel.LeadMaster = ObjLead.GetLeadList().Where(x => x.Id == Id).FirstOrDefault();
            if (objLeadModel.LeadMaster.AssignTo == "NA" || objLeadModel.LeadMaster.AssignTo == null)
            {
                objLeadModel.LeadMaster.AssignTo = "---";
            }
            else
            {
                int AssinToId = Convert.ToInt32(objLeadModel.LeadMaster.AssignTo);
                objLeadModel.LeadMaster.AssignTo = _contextDb.SA_User.Where(w => w.id == AssinToId).FirstOrDefault() != null ? _contextDb.SA_User.Where(w => w.id == AssinToId).FirstOrDefault().Fname + " " + _contextDb.SA_User.Where(w => w.id == AssinToId).FirstOrDefault().Lname : "N/A";
            }
            if (!string.IsNullOrEmpty(objLeadModel.LeadMaster.Country))
            {
                ViewBag.Country = LeadDAL.GetCountryName(Convert.ToInt32(objLeadModel.LeadMaster.Country));
            }

            if (!string.IsNullOrEmpty(objLeadModel.LeadMaster.State))
            {
                ViewBag.State = LeadDAL.GetStateName(Convert.ToInt32(objLeadModel.LeadMaster.State));
            }
            return View("ViewLeadDetails", objLeadModel);
        }

        public ActionResult ViewAllSubscriptions(int Id)
        {

            LeadViewModel objLeadModel = new LeadViewModel();
            objLeadModel.StatusList = new List<SelectListItem>();
            objLeadModel.StatusList = new List<SelectListItem> {
            new SelectListItem { Value = "Pending", Text = "Pending" },
            new SelectListItem { Value = "Assign", Text = "Assign" },
            new SelectListItem { Value = "Lost", Text = "Lost" } };

            objLeadModel.UserList = ObjLead.GetUserList();

            objLeadModel.hdId = Convert.ToString(Id);
            objLeadModel.LeadMaster = new Lead_Master();

            int leadId = _contextDb.SalesPackageSubscription.Where(w => w.UserId == Id).FirstOrDefault().LeadId;
            objLeadModel.LeadMaster = ObjLead.GetLeadList().Where(x => x.Id == leadId).FirstOrDefault();
            if (objLeadModel.LeadMaster.AssignTo == "NA" || objLeadModel.LeadMaster.AssignTo == null)
                objLeadModel.LeadMaster.AssignTo = "---";

            var lstSubscription = _contextDb.SalesPackageSubscription.Where(w => w.UserId == Id).ToList().OrderByDescending(w => w.CreatedDate);

            //foreach (var item in lstSubscription)
            //{
            //    string[] pages = item.ProductId.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //}



            objLeadModel.SubscriptionList = lstSubscription.ToList();


            return View("ViewAllSubscriptions", objLeadModel);
        }

        public ActionResult ViewAllCustSubscriptions(int Id)
        {
            LeadViewModel objLeadModel = new LeadViewModel();
            objLeadModel.StatusList = new List<SelectListItem>();
            objLeadModel.StatusList = new List<SelectListItem> {
        new SelectListItem { Value = "Pending", Text = "Pending" },
        new SelectListItem { Value = "Assign", Text = "Assign" },
        new SelectListItem { Value = "Lost", Text = "Lost" } };

            objLeadModel.UserList = ObjLead.GetUserList();

            objLeadModel.hdId = Convert.ToString(Id);
            objLeadModel.LeadMaster = new Lead_Master();

            int leadId = _contextDb.SalesPackageSubscription.Where(w => w.UserId == Id).FirstOrDefault().LeadId;
            objLeadModel.LeadMaster = ObjLead.GetLeadList().Where(x => x.Id == leadId).FirstOrDefault();
            if (objLeadModel.LeadMaster.AssignTo == "NA" || objLeadModel.LeadMaster.AssignTo == null)
                objLeadModel.LeadMaster.AssignTo = "---";

            var lstSubscription = _contextDb.SalesPackageSubscription.Where(w => w.UserId == Id).ToList().OrderByDescending(w => w.CreatedDate);

            //foreach (var item in lstSubscription)
            //{
            //    string[] pages = item.ProductId.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //}



            objLeadModel.SubscriptionList = lstSubscription.ToList();



            return View("ViewAllCustSubscriptions", objLeadModel);
        }

        public ActionResult AssignLeadDetails(LeadViewModel model)
        {
            int id = Convert.ToInt32(model.hdId);
            Lead_Master lm = new Lead_Master();
            lm = ObjLead.GetLeadList().Where(x => x.Id == id).FirstOrDefault();
            lm.Status = model.LeadMaster.Status;
            lm.AssignTo = model.LeadMaster.AssignTo;
            lm.AssignDate = DateTime.Now.Date;
            ObjLead.AddLeadDetails(lm);
            //ObjLead.AddSubscrption(lm);
            return RedirectToAction("LeadManagement");
        }


        public ActionResult ShowSubscriptionListForSales()
        {
            return View("ShowSubscriptionListForSales");
        }

        public ActionResult ShowSubscriptionListForSalesNew()
        {
            return View("ShowSubscriptionListForSalesNew");
        }

        public ActionResult ShowCustSubscriptionListForSales()
        {
            return View("ShowCustSubscriptionListForSales");
        }

        public JsonResult ShowSubscriptionList()
        {
            string loggeduser = Session["User"].ToString().Trim();
            string item = Session["LoginUser"].ToString();
            List<SubscriptionViewModel> SubscriptionList;

            if (Session["UserRole"].ToString() == "Admin")
            {
                //SubscriptionList = ObjLead.SubscriptionListforadmin(loggeduser);
                SubscriptionList = ObjLead.SubscriptionListforAdminNew(loggeduser);
            }
            else
            {
                SubscriptionList = ObjLead.SubscriptionList(item);
            }
            // return View("Subscription-list", SubscriptionListList);
            return Json(new { data = SubscriptionList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowCustSubscriptionList()
        {
            string loggeduser = Session["User"].ToString().Trim();
            string item = Session["LoginUser"].ToString();
            List<SubscriptionViewModel> SubscriptionList;

            SubscriptionList = ObjLead.SubCustscriptionList(Convert.ToInt32(item));

            return Json(new { data = SubscriptionList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActiveDeactiveSubscription(int subscriptionFID, string status)
        {
            enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
            objENSalesPackageSubscriptions.ID = subscriptionFID;

            objENSalesPackageSubscriptions.Status = "Active";
            if (status.Trim().Equals("active", StringComparison.CurrentCultureIgnoreCase))
            {
                objENSalesPackageSubscriptions.Status = "Deactive";
            }
            
            
            blSalesPackageSubscriptions objBLSalesPackageSubscriptions = new blSalesPackageSubscriptions(objENSalesPackageSubscriptions);

            try
            {
                objBLSalesPackageSubscriptions.UpdateStatus();
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }

            return Json("s", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult SendNotification(int LeadId, int subID)
        {

            ChemAnalystContext _context = new ChemAnalystContext();

            var email = _context.Lead_Master.Where(w => w.Id == LeadId).FirstOrDefault().CorpEmail;



            var Customer = _contextDb.SA_Customers.Where(w => w.Email == email).FirstOrDefault();
            var MenuId = _contextDb.SalesPackageSubscription.Where(w => w.UserId == Customer.id).OrderBy(w => w.Id).FirstOrDefault().MenuId;
            var ProductId = _contextDb.SalesPackageSubscription.Where(w => w.UserId == Customer.id).OrderBy(w => w.Id).FirstOrDefault().ProductId;

            var subs = _contextDb.SalesPackageSubscription.Where(w => w.Id == subID).FirstOrDefault();

            var products = "";
            var moduiles = "";
            string[] moduilesID = MenuId.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string[] productsID = ProductId.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in productsID)
            {
                int ID = Convert.ToInt32(item);
                products += _contextDb.SA_Product.Where(w => w.id == ID).FirstOrDefault().ProductName;
                products += " , ";
            }

            foreach (var item in moduilesID)
            {
                int ID = Convert.ToInt32(item);
                moduiles += _contextDb.SA_PageList.Where(w => w.id == ID).FirstOrDefault().PageDiscription;
                moduiles += " , ";
            }

            string subscriptionType = string.Empty;
            if (!string.IsNullOrEmpty(subs.RegionsID))
                subscriptionType = "Region wise";
            else if (!string.IsNullOrEmpty(subs.CountriesID))
                subscriptionType = "Country wise";

            string countryOrRegionNames = GetCountryOrRegionNames(subs);

            string EmailBody = SubscriptionDAL.GetHtml("PackageExpiry.html");
            EmailBody = EmailBody.Replace("@UserName@", Customer.Fname);

            EmailBody = EmailBody.Replace("@SubscriptionType@", subscriptionType);
            EmailBody = EmailBody.Replace("@CountryOrRegionNames@", countryOrRegionNames);

            EmailBody = EmailBody.Replace("@StateDate@", subs.FromDate.Value.ToString());
            EmailBody = EmailBody.Replace("@ToDate@", subs.ToDate.Value.ToString());
            EmailBody = EmailBody.Replace("@Products@", products);
            EmailBody = EmailBody.Replace("@Modules@", moduiles);


            SubscriptionDAL.SendMail("Chem Analyst", "info@chemanalyst", email, "Package Expiry Notification", EmailBody, "sales@chemanalyst.com");

            

            return Json(new { data = "Success" }, JsonRequestBehavior.AllowGet);
        }

        private string GetCountryOrRegionNames(SalesPackageSubscription subs)
        {
            string result = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(subs.RegionsID))
                {
                    var regionIDs = subs.RegionsID.Split(',');
                    var listOfRegion = _contextDb.SA_Region.ToList();
                    foreach (var item in regionIDs)
                    {
                        var region = listOfRegion.Find(itt => itt.id == Convert.ToInt32(item));
                        result += " " + region.RegionName + ",";
                    }
                    result = result.TrimEnd(',');

                }
                else if (!string.IsNullOrEmpty(subs.CountriesID))
                {
                    var countryIDs = subs.CountriesID.Split(',');
                    var listOfCountry = _contextDb.SA_Country.ToList();
                    foreach (var item in countryIDs)
                    {
                        var country = listOfCountry.Find(itt => itt.id == Convert.ToInt32(item));
                        result += " " + country.CountryName + ",";
                    }
                    result = result.TrimEnd(',');    
                }

            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            return result;
        }

        [HttpPost]
        public JsonResult RenewSubscription(int subId, DateTime fromDate, DateTime toDate)
        {

            ChemAnalystContext _context = new ChemAnalystContext();

            var subsData = _context.SalesPackageSubscription.Where(w => w.Id == subId).FirstOrDefault();
            subsData.FromDate = fromDate;
            subsData.ToDate = toDate;
            _context.SaveChanges();
            //string EmailBody = SubscriptionDAL.GetHtml("PackageExpiry.html");
            // SubscriptionDAL.SendMail("Chem Analyst", "info@chemanalyst", email, "Package Expiry Notification", EmailBody, "mrnickolasjames@gmail.com");

            return Json(new { data = "Success" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult DeleteLead(int leadId)
        {
            bool result = false;
            try
            {
                Lead_Master lead = _contextDb.Lead_Master.Where(News => News.Id == leadId).FirstOrDefault();
                _contextDb.Entry(lead).State = EntityState.Deleted;
                _contextDb.SaveChanges();

                return Json(new { data = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "fail" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = result, JsonRequestBehavior.AllowGet });
        }


        public ActionResult DownloadCustomerSubscription()
        {
            return View();

        }


        public ActionResult DownloadExcel(int ID, string Email)
        {
            //int userID = Convert.ToInt32(Session["LoginUser"]);
            var email = Email;
            List<enSalesPackageSubscriptions> listOfSubscriptions = new List<enSalesPackageSubscriptions>();

            blSalesPackageSubscriptions objBLlistOfSubscriptions = new blSalesPackageSubscriptions(
                                                    new enSalesPackageSubscriptions
                                                    {
                                                        UserID = ID,

                                                    });
            try
            {
                //listOfSubscriptions = objBLlistOfSubscriptions.ReadAll();
                listOfSubscriptions = objBLlistOfSubscriptions.ReadAllSubscriptions();
            }
            catch (Exception ex)
            {
                listOfSubscriptions = null;
            }
            // Create a sample DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
            new DataColumn("ProductName", typeof(string)),
            new DataColumn("Chemical Pricing Countries", typeof(string)),
            new DataColumn("MarketAnalysis RegionName", typeof(string)),
            new DataColumn("MarketAnalysis Countries", typeof(string)),
            new DataColumn("Capacity Tracker", typeof(string)),
            new DataColumn("Deals Countries", typeof(string)),
            new DataColumn("News Countries", typeof(string)),
            new DataColumn("FromDate_Date", typeof(string)),
            new DataColumn("ToDate_Date", typeof(string)),
            });

            foreach (var customer in listOfSubscriptions)
            {
                dt.Rows.Add(customer.ProductName, customer.Chemical_Pricing_Countries, customer.MarketAnalysis_RegionName, customer.MarketAnalysis_Countries, customer.Capacity_Tracker, customer.Deals_Countries, customer.News_Countries, customer.FromDate_Date, customer.ToDate_Date);
            }
            // Create a new Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Write data from DataTable to Excel worksheet
                worksheet.Cells.LoadFromDataTable(dt, true);

                // Generate a MemoryStream from the Excel package
                MemoryStream stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                // Return the Excel file as a FileResult
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", email + "Subscribed_Product.xlsx");
            }
        }

        public ActionResult GetAllCustomerEmail(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofCustomerEmail = new List<enSalesPackageSubscriptions>();
            blSalesPackageSubscriptions objBL = new blSalesPackageSubscriptions(new enSalesPackageSubscriptions());
            // enSalesPackageSubscriptions ObjenenSalesPackageSubscriptions = new enSalesPackageSubscriptions();

            try
            {
                ListofCustomerEmail = objBL.GetAllCustomerEmail(enSalesPackageSubscriptions);
            }
            catch (Exception ex)
            {

            }

            var jsonResult = Json(ListofCustomerEmail, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            //return Json(ListofCustomerEmail, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewSubscriptionList()
        {
            return View();
        }

        public JsonResult GetSalesPackageSubscriptionsList(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enLeadManagementSubscription> listOfsusbs = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLdata = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                searchFilter.UserId = UserId;
                listOfsusbs = objBLdata.GetListofLeads(searchFilter);

                listOfsusbs = objBLdata.GetSalesPackageSubscriptionsList(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfsusbs, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfsusbs != null && listOfsusbs.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfsusbs.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfsusbs.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfsusbs,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                PagerSize = (int)ApplicationSettings.PagerSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.PageSize,
                //SearchFilterHasValue = isSearchFilterHasValue,
                SearchFilterHasValue = string.Empty,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ManageExpireSubscriptionNotification()
        {
            return View();
        }
        public JsonResult GetListForExpireSubscriptionNotification(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enLeadManagementSubscription> listOfsusbs = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLdata = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                // int UserId = Convert.ToInt32(Session["LoginUser"]);
                // searchFilter.UserId = UserId;

                listOfsusbs = objBLdata.GetListForExpireSubscriptionNotification(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfsusbs, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfsusbs != null && listOfsusbs.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfsusbs.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfsusbs.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfsusbs,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                PagerSize = (int)ApplicationSettings.PagerSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.PageSize,
                //SearchFilterHasValue = isSearchFilterHasValue,
                SearchFilterHasValue = string.Empty,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult UpdateNotifystatus(int SubsId, int Status)
        {
            bool result = false;

            try
            {
                (from p in _contextDb.SalesPackageSubscription
                 where p.Id == SubsId select p).ToList().ForEach(x => x.ExpiryNotification = Status);
                _contextDb.SaveChanges();
                
            }
            catch (Exception ex)
            {
                
            }

            return View("ManageExpireSubscriptionNotification");
        }
        public JsonResult GetAllClintEmail()
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            List<enLeadManagementSubscription> listOfsusbs = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLdata = new blLeadManagementSubscription(new enLeadManagementSubscription());


            List<enLeadManagementSubscription> listOfReadClintEmail = new List<enLeadManagementSubscription>();
            try
            {

                listOfReadClintEmail = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadAllClintEmail(objENSearchFilter);

            }
            catch (Exception ex)
            {

            }

            var returnValue = new
            {
                DataList = listOfReadClintEmail,
            };
            // return Json(listOfReadClintEmail, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult DeleteSecondarySubscription()
        {
            return View();
        }

        public ActionResult GetCustomerPrimaryEmail()
        {
            List<enSalesPackageSubscriptions> ListofCustomerEmail = new List<enSalesPackageSubscriptions>();
            blSalesPackageSubscriptions objBL = new blSalesPackageSubscriptions(new enSalesPackageSubscriptions());
            try
            {
                ListofCustomerEmail = objBL.GetCustomerPrimaryEmail();
            }
            catch (Exception ex)
            {
            }
            return Json(ListofCustomerEmail, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCompanySecondaryEmail(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofCustomerEmail = new List<enSalesPackageSubscriptions>();
            blSalesPackageSubscriptions objBL = new blSalesPackageSubscriptions(new enSalesPackageSubscriptions());

            try
            {
                ListofCustomerEmail = objBL.GetCompanySecondaryEmail(enSalesPackageSubscriptions);
            }
            catch (Exception ex)
            {

            }
            return Json(ListofCustomerEmail, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCompanySecondaryEmail(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofCustomerEmail = new List<enSalesPackageSubscriptions>();
            blSalesPackageSubscriptions objBL = new blSalesPackageSubscriptions(new enSalesPackageSubscriptions());

            try
            {
                ListofCustomerEmail = objBL.DeleteCompanySecondaryEmail(enSalesPackageSubscriptions);
            }
            catch (Exception ex)
            {

            }
            return Json(ListofCustomerEmail, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeSelectAllCheck(enSearchFilter enSearchFilter)
        {
            List<enLeadManagementSubscription> ListenLeadManagementSubscription = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBL = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                ListenLeadManagementSubscription = objBL.DeSelectAllCheck(enSearchFilter);
            }
            catch (Exception ex)
            {

            }
            return Json(ListenLeadManagementSubscription, JsonRequestBehavior.AllowGet);

        }


        //---------------------------------New development by ashish by all client all subcription----------------------------


        public ActionResult GetAllSubs()
        {
            return View();
        }
        public ActionResult SubsDetailsByUser()
        {
            return View();
        }
        public JsonResult GetClintEmail()
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfReadClintEmail = new List<enLeadManagementSubscription>();
            try
            {
                objENSearchFilter.ActionType = "AllClintEmail";

                listOfReadClintEmail = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadClintEmail(objENSearchFilter);


            }
            catch (Exception ex)
            {

            }

            var returnValue = new
            {
                DataList = listOfReadClintEmail,
            };
            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public JsonResult GellAllClintAllSubsciption(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enAllclientAllSubs> ListofData = new List<enAllclientAllSubs>();
            blSalesPackageSubscriptions objBLdata = new blSalesPackageSubscriptions(new enSalesPackageSubscriptions());

            try
            {
                searchFilter.CrmId = Convert.ToInt32(Session["LoginUser"]);
                ListofData = objBLdata.GellAllClintAllSubsciption(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(ListofData, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (ListofData != null && ListofData.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(ListofData.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(ListofData.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = ListofData,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                PagerSize = (int)ApplicationSettings.PagerSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.PageSize,
                SearchFilterHasValue = string.Empty,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
     
        public ActionResult GetCustomerByProduct(string Productid)
        {
            enSearchFilter obj = new enSearchFilter();
            List<enAllclientAllSubs> Listofdata = new List<enAllclientAllSubs>();
            blSalesPackageSubscriptions objBL = new blSalesPackageSubscriptions(new enSalesPackageSubscriptions());
            try
            {
                obj.UserId = Convert.ToInt32(Session["LoginUser"]);
                obj.ProductId = Convert.ToInt32(Productid);
                Listofdata = objBL.GetCustomerByProduct(obj);
            }
            catch (Exception ex)
            {
            }
            var returnValue = new
            {
                DataList = Listofdata,
            };
            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult GetCustomerISApiAccess()
        {
            enSearchFilter obj = new enSearchFilter();
            List<enAllclientAllSubs> Listofdata = new List<enAllclientAllSubs>();
            blSalesPackageSubscriptions objBL = new blSalesPackageSubscriptions(new enSalesPackageSubscriptions());
            try
            {
                obj.UserId = Convert.ToInt32(Session["LoginUser"]);
                Listofdata = objBL.GetCustomerISApiAccess(obj);
            }
            catch (Exception ex)
            {
            }
            var returnValue = new
            {
                DataList = Listofdata,
            };
            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult GetCustomerbyModule(string ModuleId)
        {
            enSearchFilter obj = new enSearchFilter();
            List<enAllclientAllSubs> Listofdata = new List<enAllclientAllSubs>();
            blSalesPackageSubscriptions objBL = new blSalesPackageSubscriptions(new enSalesPackageSubscriptions());
            try
            {
                obj.UserId = Convert.ToInt32(Session["LoginUser"]);
                obj.Menu = Convert.ToInt32(ModuleId);
                Listofdata = objBL.GetCustomerModule(obj);
            }
            catch (Exception ex)
            {
            }
            var returnValue = new
            {
                DataList = Listofdata,
            };
            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult GetCustomerPrimary()
        {
            enSearchFilter obj = new enSearchFilter();
            List<enAllclientAllSubs> Listofdata = new List<enAllclientAllSubs>();
            blSalesPackageSubscriptions objBL = new blSalesPackageSubscriptions(new enSalesPackageSubscriptions());
            try
            {
                obj.UserId = Convert.ToInt32(Session["LoginUser"]);
                Listofdata = objBL.GetCustomerPrimary(obj);
            }
            catch (Exception ex)
            {
            }
            var returnValue = new
            {
                DataList = Listofdata,
            };
            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


    }
}