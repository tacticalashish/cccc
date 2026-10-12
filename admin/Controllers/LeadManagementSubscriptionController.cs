using BusinessLayer;
using ChemAnalyst.Common;
using ChemAnalyst.DAL;
using ChemAnalyst.Models;
using ChemAnalyst.ViewModel;
using EntityLayer;
using EntityLayer.CustomEntity;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UIHelper;
using Utility;

namespace ChemAnalyst.Controllers
{
    public class LeadManagementSubscriptionController : Controller
    {
        // GET: LeadManagementSubscription
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DashBoard()
        {
            return View();
        }
        // CRM ka "Subscriptions" page — SalesIndex jaisa (same layout, pagination,
        // row-click detail). View + action + data (BL/DAL) sab yahin.
        public ActionResult Subscriptions()
        {
            return View();
        }

        public JsonResult GetSubscriptionRequestsList(string search, int pageNumber = 1)
        {
            const int pageSize = 10;
            int endRowNumber = pageNumber * pageSize;
            int startRowNumber = endRowNumber - pageSize + 1;

            var list = new List<enSalesManagement>();
            try
            {
                int userId = Session["LoginUser"] != null ? Convert.ToInt32(Session["LoginUser"]) : 0;
                var bl = new blLeadManagementSubscription(new enLeadManagementSubscription());
                list = bl.GetSubscriptionRequestsList(search, startRowNumber, endRowNumber, userId);
            }
            catch (Exception)
            {
            }

            int showingFrom = 0, showingTo = 0, recordsCount = 0, numberOfPages = 0;
            if (list != null && list.Count > 0)
            {
                showingFrom = startRowNumber;
                showingTo = list.LastOrDefault().RowNumber;
                recordsCount = list.LastOrDefault().RecordsCount;
                numberOfPages = (int)Math.Ceiling((double)recordsCount / pageSize);
                if (showingFrom >= recordsCount && !string.IsNullOrEmpty(search))
                {
                    showingFrom = 1;
                }
            }

            var returnValue = new
            {
                DataList = list,
                ShowingFrom = showingFrom,
                ShowingTo = showingTo,
                RecordsCount = recordsCount,
                ActivePage = pageNumber,
                PagerSize = pageSize,
                NumberOfPages = numberOfPages,
                PageSize = pageSize
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // CRM version of the subscription-request detail page.
        public ActionResult CRMSubscriptionRequestDetail(string company)
        {
            ViewBag.CompanyName = company;
            return View();
        }

        public JsonResult GetCRMSubscriptionRequestDetail(string company, string search, int pageNumber = 1)
        {
            const int pageSize = 10;
            int endRowNumber = pageNumber * pageSize;
            int startRowNumber = endRowNumber - pageSize + 1;

            var lines = new List<enSubscriptionRequestLine>();
            try
            {
                var bl = new blLeadManagementSubscription(new enLeadManagementSubscription());
                lines = bl.GetSubscriptionRequestLines(company, search, startRowNumber, endRowNumber);
            }
            catch (Exception)
            {
            }

            int showingFrom = 0, showingTo = 0, recordsCount = 0, numberOfPages = 0;
            if (lines != null && lines.Count > 0)
            {
                showingFrom = startRowNumber;
                showingTo = lines.LastOrDefault().RowNumber;
                recordsCount = lines.LastOrDefault().RecordsCount;
                numberOfPages = (int)Math.Ceiling((double)recordsCount / pageSize);
                if (showingFrom >= recordsCount && !string.IsNullOrEmpty(search))
                {
                    showingFrom = 1;
                }
            }

            var jsonResult = Json(new
            {
                DataList = lines,
                ShowingFrom = showingFrom,
                ShowingTo = showingTo,
                RecordsCount = recordsCount,
                ActivePage = pageNumber,
                PagerSize = pageSize,
                NumberOfPages = numberOfPages
            }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult CRMCompanyRelation()
        {
            return View();
        }

        // GET: LeadManagementSubscription/Reports — read-only analytics/export
        // landing page. Only the Churn & Lost Renewals section has a real
        // data source defined so far; the six report cards above it are
        // "coming soon" links until each one is scoped (data source, filters,
        // Excel export contents) individually. No left-hand nav shell here.
        public ActionResult Reports()
        {
            return View();
        }
        public ActionResult LeadsManagement()
        {
            return View();
        }
        public ActionResult CompanyMaster()
        {
            return View();
        }
        
        public ActionResult AddSubscriptionPackage()
        {
            return View();
        }
        public ActionResult CreateLead()
        {
            return View();
        }

        
        public ActionResult UpdateSalesPackageSubscriptions(int SalesPackageID)
        {
            ViewBag.SalesPackageID = SalesPackageID;
            return View();
        }
        public ActionResult ListOfSalesPackageSubscriptions()
        {
            return View();
        }

        public ActionResult OrganizationalChartLayout_PreSales()
        {
            return View();
        }

        public ActionResult PreSalesLeadEmployeeMaster()
        {
            return View();
        }

        public ActionResult SubscriptionPaymentDetails()
        {
            return View();
        }
        
        public ActionResult UpdateLeadByDataAnalyst(int LeadId)
        {
            ViewBag.LeadId = LeadId;
            return View();
        }
        public ActionResult CreateSubscriptionbyParantClint(int LeadId)
        {
            ViewBag.LeadId = LeadId;
            return View();
        }
        public ActionResult EditCustomerLead(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public ActionResult OperationLeadApproval()
        {
            return View();
        }
        public ActionResult AccountsLeadApproval()
        {
            return View();
        }
        public ActionResult LeaddeatilsForProjectManger()
        {
            return View();
        }
        public ActionResult SalesHeadLeadApproval()
        {
            return View();
        }
        public ActionResult CRMDashboard()
        {
            return View();
        }

        public ActionResult GetAllOpenLeadListManager()
        {
            return View();
        }

        public ActionResult OpenLeadEmployeeMaster()
        {
            return View();
        }

        public ActionResult GetOpenLeadTLList()
        {
            return View();
        }

        public ActionResult GetAllChemAnalystProformaInvoiceList()
        {
            return View();
        }

        public ActionResult OrganizationalChartLayout_Sales()
        {
            return View();
        }

        public ActionResult LeadStatusReport()
        {
            return View();
        }
       

        public ActionResult CreatePreSalesLead()
        {
            return View();
        }
        public ActionResult GetPreSalesLeadList()
        {
            return View();
        }

        public ActionResult EditPreSalesLeadList(int ID)
        {
            ViewBag.PreSalesEditID = ID;
            return View();
        }
        public ActionResult ExcelUploadPreSalesLead()
        {
            return View();
        }

        public ActionResult GetPresalesLeadTLList()
        {
            return View();
        }

        public ActionResult GetPresalesLeadManagerList()
        {
            return View();
        }

        public ActionResult LeadStatusReportPreSales()
        {
            return View();
        }

        public ActionResult LeadDetailsById(int LeadId,string StatusType)
        {
            ViewBag.LeadId = LeadId;
            ViewBag.StatusType = StatusType;
            return View();
        }
        public ActionResult DataAnalystCreateSubsCription()
        {
            return View();
        }
        public ActionResult GetTwomonthsbeforeloginDetails()
        {
            return View();
        }

        public JsonResult GetTwomonthsbeforeloginDetails_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * 200;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - 200 + 1;

            List<enAdminChemicalPrice> listOfdata = new List<enAdminChemicalPrice>();
            blAdminChemicalPricing objBL = new blAdminChemicalPricing(new enAdminChemicalPrice());
            List<GetCRMDetails> CheckCrm = new List<GetCRMDetails>();

            try
            {
                searchFilter.UserId = Convert.ToInt32(Session["LoginUser"]);
                listOfdata = objBL.get_Two_months_before_loginDetailscrm(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / 200);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                PagerSize = 200,//(int)ApplicationSettings.PagerSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = 200,//ApplicationSettings.PageSize,
                //SearchFilterHasValue = isSearchFilterHasValue,
                SearchFilterHasValue = string.Empty,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult SavePreSalesLeadDetails(Lead_Master GetDetails)
        {
            enLeadManagementSubscription enObj = new enLeadManagementSubscription();
            string Message = "";
            List<enLeadManagementSubscription> ListofRespone = new List<enLeadManagementSubscription>();
            try
            {
                enObj.Name = GetDetails.Name;
                enObj.CorpEmail = GetDetails.CorpEmail;
                enObj.Mobile = GetDetails.Mobile;
                enObj.CompanyName = GetDetails.Organisation;
                enObj.CountryName = GetDetails.Country;
                enObj.StateName = GetDetails.State;
                enObj.Designation = GetDetails.Designation;
                enObj.DiscriptionMessage = GetDetails.DiscriptionMessage;
                enObj.OutboundLeadSource = GetDetails.OutboundLeadSource;
                enObj.LeadType = GetDetails.LeadType;
                enObj.IPAddress = GetIPAddress();
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                enObj.Phone = GetDetails.Phone;
                enObj.createdby = Session["User"].ToString();
                enObj.RequestedDate = GetDetails.RequestedDate;
                enObj.ContactCountryCode = GetDetails.ContactCountryCode;
                enObj.TelephoneCountryCode = GetDetails.TelephoneCountryCode;


                ListofRespone = new blLeadManagementSubscription(new enLeadManagementSubscription()).InsertPreSalesLeadRecord(enObj);

            }
            catch (Exception ex)
            {

            }

            return Json(ListofRespone, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetPreSalesLeadList_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.EximPageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.EximPageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {

                searchFilter.UserName = Session["User"].ToString(); ;

                listOfdata = objBLs.GetPreSalesLeadList(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.EximPageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                PagerSize = (int)ApplicationSettings.EximPageSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.EximPageSize,
                //SearchFilterHasValue = isSearchFilterHasValue,
                SearchFilterHasValue = string.Empty,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        public ActionResult SelesExcelUpload(FormCollection formcollection)
        {
            ProductDataStore productDataStore = new ProductDataStore();
            DataTable dataTable = new DataTable();
            FreightIndex objFreightIndex = new FreightIndex();
            //bool isReplaceData = Convert.ToBoolean(formcollection["ReplaceData"].Split(',')[0]);

            string filePath = string.Empty;
            string fileName = string.Empty;
            string viewBagStatus = "Success";
            string viewBagMessage = "File uploaded   Successfully.";
            string actionType = EnumHelper.GetEnumDescription(typeof(ActionType), ActionType.CREATE);

            try
            {
                if (Request.Files.Count > 0)
                {
                    //note: pick first file as we have values in first files
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        fileName = Path.GetFileName(file.FileName);
                        if (!fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                            throw new Exception("Only .xlsx files are supported.");

                        var excelPackage = new ExcelPackage(file.InputStream);
                        dataTable = excelPackage.PreSalesDataTable();
                        if (dataTable.Rows.Count == 0)
                            throw new Exception("Uploaded Excel does not contain any data.");

                        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                        string extension = Path.GetExtension(fileName);

                        // Add current date (ddMMyyyy format, you can change as needed)
                        string timeStamp = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                        fileName = $"{fileNameWithoutExt}_{timeStamp}{extension}";


                        //Note: Genrate file path
                        filePath = Path.Combine(Server.MapPath("~/FreightIndexImport/PreSales"), fileName);

                        //Note: to check old file exist or not if exist then delete file
                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);

                        //Note: save file with original data
                        file.SaveAs(filePath);


                        //Note: to check data is valid or not based on columns and range type and handle exception. if return value not success then we throw manuall exception 
                        string dataMessage = ToValidateData(dataTable);
                        if (!dataMessage.Equals("success", StringComparison.CurrentCultureIgnoreCase))
                            throw new Exception(dataMessage);

                        //Note: insert value into database from database
                        InsertPreSalesLeadExcelRecords(dataTable, fileName);
                    }
                }

                CollectionHelper.ExcelLogsCreate(new enExcelLogs
                {
                    FileName = fileName,
                    ActionType = actionType,
                    Status = EnumHelper.GetEnumDescription(typeof(Status), Status.SUCESS),
                    Message = "record has been saved successfully",
                    CreatedBy = Session["User"].ToString(),
                    Type = EnumHelper.GetEnumDescription(typeof(Utility.Type), Utility.Type.CHEMICAL_PRICING),
                    MarketType = null
                });
            }
            catch (Exception ex)
            {
                viewBagStatus = "Failed";
                viewBagMessage = "File could not upload. Please kindly check";
                viewBagMessage += " Error: " + ex.Message;

                CollectionHelper.ExcelLogsCreate(new enExcelLogs
                {
                    FileName = fileName,
                    ActionType = actionType,
                    Status = EnumHelper.GetEnumDescription(typeof(Status), Status.FAIL),
                    Message = ex.Message,
                    CreatedBy = Session["User"].ToString(),
                    Type = EnumHelper.GetEnumDescription(typeof(Utility.Type), Utility.Type.CHEMICAL_PRICING),
                    MarketType = null
                });
            }

            ViewBag.Status = viewBagStatus;
            ViewBag.Messge = viewBagMessage;
            return View("CreatePreSalesLead");
        }




        public string ToValidateData(DataTable dataTable)
        {
            string result = "please check excel, did not found any records";
            int count = dataTable.AsEnumerable().Select(item => item.ItemArray).FirstOrDefault().Count();
            var listOfObject = dataTable.AsEnumerable().SelectMany(x => x.ItemArray).Select((v, i) => new { Value = v, Index = i % count }).ToList();

            bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 15).Select(it => it.Value).ToList());
            if (!isCountNumber)
                throw new Exception("count values are not number");
            result = "success";

            return result;
        }



        private bool IsListTypeOfDecimal(List<object> listOfObject)
        {
            bool result = true;

            try
            {
                List<decimal> castListToDecimal = listOfObject.ConvertAll<decimal>(Convert.ToDecimal).ToList();

                if (listOfObject.Count() != castListToDecimal.Count())
                    throw new Exception("mismatch count list");

            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;

        }

        private bool InsertPreSalesLeadExcelRecords(DataTable excelDataTable, string fileName)
        {

            try
            {
                foreach (DataColumn col in excelDataTable.Columns)
                {
                    col.ColumnName = col.ColumnName.Trim();
                }
                excelDataTable.Columns.Add("IsEmail", typeof(bool));
                excelDataTable.Columns.Add("IPAddress", typeof(string));
                excelDataTable.Columns.Add("LeadType", typeof(string));
                excelDataTable.Columns.Add("Status", typeof(string));
                excelDataTable.Columns.Add("CreatedDate", typeof(DateTime));
                excelDataTable.Columns.Add("CreatedBy", typeof(string));
                excelDataTable.Columns.Add("IsActive", typeof(int));
                excelDataTable.Columns.Add("Source", typeof(string));
                //string[] dateFormats = { "dd-MM-yyyy", "dd/MM/yyyy", "MM-dd-yyyy", "MM/dd/yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "dd-MMM-yyyy", "MMM dd, yyyy", "d-M-yyyy", "d/M/yyyy", "M-d-yyyy", "M/d/yyyy" };
                foreach (DataRow row in excelDataTable.Rows)
                {
                    //var dateStr = row["RequestedDate"]?.ToString().Trim();

                    //if (!string.IsNullOrEmpty(dateStr))
                    //{
                    //    if (DateTime.TryParseExact(dateStr, dateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                    //    {
                    //        row["RequestedDate"] = parsedDate.Date.ToString("dd/MM/yyyy");
                    //    }
                    //    else if (double.TryParse(dateStr, out double oaDate)) // Excel numeric date
                    //    {
                    //        row["RequestedDate"] = DateTime.FromOADate(oaDate).Date.ToString("dd/MM/yyyy");
                    //    }
                    //    else
                    //    {
                    //        throw new Exception("Invalid Date");                           
                    //    }
                    //}



                    if (string.IsNullOrWhiteSpace(row["CountryName"]?.ToString()))
                        throw new Exception("CountryName is mandatory but missing for one or more rows.");

                    if (string.IsNullOrWhiteSpace(row["CountryFID"]?.ToString()) || !int.TryParse(row["CountryFID"].ToString(), out _))
                        throw new Exception("CountryFID is mandatory and must be a valid integer.");

                    if (string.IsNullOrWhiteSpace(row["RequestedDate"]?.ToString()) )
                        throw new Exception("RequestedDate is mandatory and must be a valid date.");

                    if (string.IsNullOrWhiteSpace(row["OutboundLeadSource"]?.ToString()))
                        throw new Exception("OutboundLeadSource is mandatory but missing for one or more rows.");

                    if (string.IsNullOrWhiteSpace(row["Name"]?.ToString()))
                        throw new Exception("Name is mandatory but missing for one or more rows.");

                    if (string.IsNullOrWhiteSpace(row["ContactCountryCode"]?.ToString()))
                        row["ContactCountryCode"] = DBNull.Value;

                    if (string.IsNullOrWhiteSpace(row["ContactNumber"]?.ToString()))
                        row["ContactNumber"] = DBNull.Value;

                    if (string.IsNullOrWhiteSpace(row["StateFID"]?.ToString()))
                        row["StateFID"] = DBNull.Value;

                    if (string.IsNullOrWhiteSpace(row["TelephoneCountryCode"]?.ToString()))
                        row["TelephoneCountryCode"] = DBNull.Value;

                    if (string.IsNullOrWhiteSpace(row["Telephone"]?.ToString()))
                        row["Telephone"] = DBNull.Value;

                }

                for (int i = excelDataTable.Rows.Count - 1; i >= 0; i--)
                {
                    excelDataTable.Rows[i]["IsEmail"] = true;
                    excelDataTable.Rows[i]["IPAddress"] = GetIPAddress();
                    excelDataTable.Rows[i]["LeadType"] = "Outbound";
                    excelDataTable.Rows[i]["Status"] = "New";
                    excelDataTable.Rows[i]["IsActive"] = 1;
                    excelDataTable.Rows[i]["Source"] = "Outbound";
                    excelDataTable.Rows[i]["CreatedDate"] = DateTime.Now;
                    excelDataTable.Rows[i]["CreatedBy"] = Session["User"].ToString();

                }
                excelDataTable.AcceptChanges();

                List<string> existingEmails = new List<string>();
                var connectionString = ConfigurationManager.ConnectionStrings["ChemAnalystContext"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CorpEmail FROM SA_MaskEmail_PreSales", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["CorpEmail"] != DBNull.Value)
                                existingEmails.Add(reader["CorpEmail"].ToString().Trim().ToLower());

                        }
                    }
                    conn.Close();
                }

    //            var alreadyExistEmails = excelDataTable.AsEnumerable()
    //.Where(row => !string.IsNullOrWhiteSpace(row.Field<string>("CorpEmail")))
    //.Select(row => row.Field<string>("CorpEmail").Trim().ToLower())
    //.Where(email => existingEmails.Contains(email))
    //.Distinct()
    //.ToList();

    //            if (alreadyExistEmails.Any())
    //            {
    //                string message = "These  Email values already exist : " + string.Join(", ", alreadyExistEmails);
    //                throw new Exception(message);
    //            }

                for (int i = excelDataTable.Rows.Count - 1; i >= 0; i--)
                {
                    string email = excelDataTable.Rows[i]["CorpEmail"]?.ToString().Trim().ToLower();
                    if (string.IsNullOrWhiteSpace(email) || existingEmails.Contains(email))
                    {
                        excelDataTable.Rows.RemoveAt(i);
                    }


                }

                excelDataTable.AcceptChanges();


                var duplicateEmails = excelDataTable.AsEnumerable()
              .Where(row => !string.IsNullOrWhiteSpace(row.Field<string>("CorpEmail")))
              .GroupBy(row => row.Field<string>("CorpEmail").Trim().ToLower())
              .Where(g => g.Count() > 1)
              .Select(g => g.Key)
              .ToList();

                if (duplicateEmails.Any())
                {
                    string message = "Excel file contains duplicate CorpEmail values: " + string.Join(", ", duplicateEmails);
                    throw new Exception(message);
                }

                if (excelDataTable.Rows.Count == 0)
                    return true;
                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = connectionString;

                SqlBulkCopy objbulk = new SqlBulkCopy(sqlConnection);

                objbulk.DestinationTableName = "SA_MaskEmail_PreSales";

                objbulk.ColumnMappings.Add("Name", "Name");
                objbulk.ColumnMappings.Add("CorpEmail", "CorpEmail");
                objbulk.ColumnMappings.Add("ContactCountryCode", "ContactCountryCode");
                objbulk.ColumnMappings.Add("ContactNumber", "ContactNumber");
                objbulk.ColumnMappings.Add("CompanyName", "CompanyName");
                objbulk.ColumnMappings.Add("Designation", "Designation");
                objbulk.ColumnMappings.Add("CountryName", "CountryName");
                objbulk.ColumnMappings.Add("CountryFID", "CountryFID");
                objbulk.ColumnMappings.Add("StateName", "StateName");
                objbulk.ColumnMappings.Add("StateFID", "StateFID");
                objbulk.ColumnMappings.Add("Message", "Message");
                objbulk.ColumnMappings.Add("RequestedDate", "RequestedDate");
                objbulk.ColumnMappings.Add("OutboundLeadSource", "OutboundLeadSource");
                objbulk.ColumnMappings.Add("TelephoneCountryCode", "TelephoneCountryCode");
                objbulk.ColumnMappings.Add("Telephone", "Telephone");


                objbulk.ColumnMappings.Add("IsEmail", "IsEmail");
                objbulk.ColumnMappings.Add("IPAddress", "IPAddress");
                objbulk.ColumnMappings.Add("CreatedBy", "CreatedBy");
                objbulk.ColumnMappings.Add("CreatedDate", "CreatedDate");
                objbulk.ColumnMappings.Add("Status", "Status");
                objbulk.ColumnMappings.Add("LeadType", "LeadType");
                objbulk.ColumnMappings.Add("IsActive", "IsActive");
                objbulk.ColumnMappings.Add("Source", "Source");

                sqlConnection.Open();
                objbulk.WriteToServer(excelDataTable);
                sqlConnection.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;

        }



        public JsonResult GetPreSalesLisTL_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.UserId = Convert.ToInt32(Session["LoginUser"]);
                listOfdata = objBLs.GetPreSalesListTL(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
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


        public JsonResult GetPreSalesLisManager_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.EximPageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.EximPageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.UserId = Convert.ToInt32(Session["LoginUser"]);
                listOfdata = objBLs.GetPreSalesListManager(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.EximPageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                PagerSize = (int)ApplicationSettings.EximPageSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.EximPageSize,
                //SearchFilterHasValue = isSearchFilterHasValue,
                SearchFilterHasValue = string.Empty,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        public JsonResult GetLeadsForCRM(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enLeadManagementSubscription> listOfCrmLeads = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLLeads = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                searchFilter.UserId = UserId;
                //listOfCrmLeads = objBLLeads.GetListofLeads(searchFilter);
                listOfCrmLeads = objBLLeads.GetListofCRMLeads(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfCrmLeads, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfCrmLeads != null && listOfCrmLeads.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfCrmLeads.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfCrmLeads.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfCrmLeads,
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
        public JsonResult GetLeadsList(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enLeadManagementSubscription> listOfLeads = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLLeads = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                searchFilter.UserId = UserId;
                listOfLeads = objBLLeads.GetListofLeads(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfLeads, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfLeads != null && listOfLeads.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfLeads.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfLeads.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfLeads,
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

        public JsonResult GetAllDropdowndata()
        {
            DataSet ds = new DataSet();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            Dictionary<string, object> Objdic = new Dictionary<string, object>();
            List<enLeadManagementSubscription> Objdic_Region = new List<enLeadManagementSubscription>();
            List<enLeadManagementSubscription> Objdic_Module = new List<enLeadManagementSubscription>();
            List<enLeadManagementSubscription> Objdic_CRM = new List<enLeadManagementSubscription>();
            try {
                List<enLeadManagementSubscription> listOfLeads = new List<enLeadManagementSubscription>();
                blLeadManagementSubscription objBLLeads = new blLeadManagementSubscription(new enLeadManagementSubscription());
                ds = objBLLeads.GetAllDropdownData("[dbo].[Usp_GetAllSubscriptionDropdowndata]", Objdic,50);

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[4].Rows)
                    {
                        Objdic_Region.Add(new enLeadManagementSubscription
                        {
                            RegionId = Convert.ToString(dr["id"]),
                            RegionName = Convert.ToString(dr["RegionName"]),
                        });
                    }
                } 
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[6].Rows)
                    {
                        Objdic_Module.Add(new enLeadManagementSubscription
                        {
                            ModuleId = Convert.ToInt32(dr["id"]),
                            ModuleName = Convert.ToString(dr["ModuleName"]),
                        });
                    }
                }
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[10].Rows)
                    {
                        Objdic_CRM.Add(new enLeadManagementSubscription
                        {
                            CrmId = Convert.ToInt32(dr["id"]),
                            CRM = Convert.ToString(dr["Fname"]),
                        });
                    }
                }
            }
           
            catch (Exception ex)
            {
      
            }
     
            Objdic.Add("Objdic_Region", Objdic_Region);
            Objdic.Add("Objdic_Module", Objdic_Module);
            Objdic.Add("Objdic_CRM", Objdic_CRM);
      

            var returnValue = new
            {
                DataList = Objdic,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult GetChemicalRange(string productlist ,string CountryName)
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);
           
            List<enLeadManagementSubscription> listOfChemicalPriceRange = new List<enLeadManagementSubscription>();
            try
            {
                if (productlist != "")
                {
                    objENSearchFilter.CountryName = CountryName;
                    objENSearchFilter.ProductIdBulk = productlist;
                    listOfChemicalPriceRange =new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadChemicalPriceRange(objENSearchFilter);

                }
                else
                {
                    return Json(Message = "Faild", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }

            //return Json(listOfChemicalPriceRange, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(listOfChemicalPriceRange, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost] 
        public JsonResult GetMarketAnalysisRange(string productlist,string CountryName,string region)
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfMarketAnalysisRange = new List<enLeadManagementSubscription>();
            try
            {
                if (productlist != "")
                {
                    objENSearchFilter.ProductIdBulk = productlist;
                    objENSearchFilter.Regions = region;
                    objENSearchFilter.CountryName = CountryName;
                  

                    listOfMarketAnalysisRange = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadMarketAnalysisRange(objENSearchFilter);
                    
                }
                else
                {
                    return Json(Message = "Faild", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }
            var returnValue = new
            {
                DataList = listOfMarketAnalysisRange,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
            //return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetStateDDL(string CountryID)
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfState = new List<enLeadManagementSubscription>();
            try
            {
                if (CountryID != "")
                {
                    objENSearchFilter.Country = Convert.ToInt32(CountryID);
                 
                    listOfState = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadStateDDL(objENSearchFilter);

                }
                else
                {
                    return Json(Message = "Faild", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }

            return Json(listOfState, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveLeadRecord(List<LeadManagementSubscription> GetLeadDetails)
        {
            enLeadManagementSubscription enObj = new enLeadManagementSubscription();
            string Message = "";

            try
            {
                string strPassword = "Techsci44@Re";
                string strEncrypted = (Helpers.Encrypt(strPassword));
                string strDecrypted = (Helpers.Decrypt(strEncrypted));
              
                foreach (var LeadDetails in GetLeadDetails)
                {
                    enObj.Password = strEncrypted;
                    enObj.Name = LeadDetails.Name;
                    enObj.CorpEmail = LeadDetails.CorpEmail;
                    enObj.Organisation = LeadDetails.Organisation;
                    enObj.CountryID = LeadDetails.CountryID;
                    enObj.StateId = LeadDetails.StateId;
                    enObj.Phone = LeadDetails.Phone;
                    enObj.module = LeadDetails.module;
                    enObj.UserType = LeadDetails.UserType;
                    enObj.Status = LeadDetails.Status;//"New";
                    enObj.IPAddress = "";//LeadDetails.Name;
                    enObj.BillingEmail = LeadDetails.BillingEmail;
                    enObj.SubscriptionType = "2";//LeadDetails.SubscriptionType;
                    enObj.AssignAnalystId = LeadDetails.AssignAnalyst;
                    enObj.SecondaryClintEmail = LeadDetails.SecondaryClintEmail;
                    enObj.SubscriptionDateFrom = LeadDetails.SubscriptionDateFrom;
                    enObj.SubscriptionDateTo = LeadDetails.SubscriptionDateTo;
                    enObj.Subscription_Country = LeadDetails.Subscription_Country;
                    enObj.Subscription_Product = LeadDetails.Subscription_Product;
                    enObj.Chemical_Range = LeadDetails.Chemical_Range;
                    enObj.Market_Analysis_Range = LeadDetails.Market_Analysis_Range;
                    enObj.SubSubscriptionRemark = LeadDetails.SubSubscriptionRemark;
                    enObj.PaymentMode = LeadDetails.PaymentMode;
                    enObj.files = LeadDetails.files;
                    enObj.CRM = LeadDetails.CRM;
                    enObj.Trader = LeadDetails.Trader;
                    enObj.Credit = LeadDetails.Credit;
                    enObj.Renew = LeadDetails.Renew;

                    enObj.createdby = Session["User"].ToString();
                    enObj.RegionId = Convert.ToString(LeadDetails.RegionId);
                    if ((enObj.Name != null) && (enObj.Subscription_Product != null) && (enObj.BillingEmail != null)
                   && (enObj.CorpEmail != null))
                    {
                        List<enLeadManagementSubscription> ListofRespone = new List<enLeadManagementSubscription>();
                        ListofRespone = new blLeadManagementSubscription(new enLeadManagementSubscription()).InsertLeadRecord(enObj);

                        foreach (var ResponseMessage in ListofRespone)
                        {
                            Message = ResponseMessage.ValidationMessage;
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult DeleteLead(int leadId)
        {
            string Message = "";
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> Lead = new List<enLeadManagementSubscription>();
            try
            {
                if (leadId != 0)
                {
                    objENSearchFilter.LeadId = Convert.ToInt32(leadId);

                   // Lead = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadLeadDatainGrid(objENSearchFilter);
                    Lead = new blLeadManagementSubscription(new enLeadManagementSubscription()).DeleteGrid(objENSearchFilter);

                }
                else
                {
                    return Json(Message = "Faild", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }

            return Json(Lead, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetLeadDataForOperations(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enLeadManagementSubscription> listOfLeads = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLLeads = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                searchFilter.UserId = UserId;
              
                listOfLeads = objBLLeads.ReadLeadDatainGridForOperation(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfLeads, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfLeads != null && listOfLeads.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfLeads.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfLeads.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfLeads,
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
        public JsonResult UpdateApproval_ApprovalStatus(int ID,string StatusType)
        {
            string Message = "";
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);
            List<enLeadManagementSubscription> flagStatus = new List<enLeadManagementSubscription>();
            try
            {
                string UserName = Convert.ToString(Session["User"]);
                obj.Id = ID;
                obj.ApprovedBy = UserName;
                obj.Query = StatusType;
                if ((ID != 0) && (StatusType != ""))
                {
                    flagStatus = objB.UpdateLeadApprovalStatus();
                }
                foreach (var status in flagStatus)
                {
                    Message = status.ValidationMessage;
                }
               
            }
            catch (Exception ex)
            {


            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DisApprovalMessage(int LeadID, string DisapprovedMessage, string StatusType)
        {
            string Message = "";
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);
            List<enLeadManagementSubscription> retunstatus = new List<enLeadManagementSubscription>();
            try
            {
                string UserName = Convert.ToString(Session["User"]);
        
                obj.DisapprovedRemark = DisapprovedMessage;
                obj.Id = LeadID;
                obj.createdby = UserName;
                obj.Query = StatusType;

                if ((LeadID != 0) && (StatusType != "") && (DisapprovedMessage!=""))
                {
                    retunstatus = objB.DisApprovalMessage();
                }
                foreach (var status in retunstatus)
                {
                    Message = status.ValidationMessage;
                }
              
            }
            catch (Exception ex)
            {


            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatedisApproval_ApprovalStatus(int ID, string StatusType)
        {
            string Message = "";
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);
            List<enLeadManagementSubscription> flagStatus = new List<enLeadManagementSubscription>();
            try
            {
                string UserName = Convert.ToString(Session["User"]);
             
                obj.Id = ID;
                obj.createdby = UserName;
                obj.Query = StatusType;
                if ((ID != 0) && (StatusType != ""))
                {
                  
                    flagStatus = objB.UpdateLeadDisApprovalStatus();
                }
                foreach (var status in flagStatus)
                {
                    Message = status.ValidationMessage;
                }

            }
            catch (Exception ex)
            {


            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLeadDataForAccounts(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enLeadManagementSubscription> listOfLeads = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLLeads = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                searchFilter.UserId = UserId;
                listOfLeads = objBLLeads.ReadLeadDatainGridForAccounts(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfLeads, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfLeads != null && listOfLeads.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfLeads.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfLeads.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfLeads,
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


        public JsonResult GetLeadDataForProjectManager(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enLeadManagementSubscription> listOfLeads = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLLeads = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                searchFilter.UserId = UserId;
               
                listOfLeads = objBLLeads.ReadLeadDatainGridForProjectManager(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfLeads, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfLeads != null && listOfLeads.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfLeads.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfLeads.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfLeads,
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


        public JsonResult GetLeadDataForSalesHead(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enLeadManagementSubscription> listOfLeads = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLLeads = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                searchFilter.UserId = UserId;
                //listOfLeads = objBLLeads.ReadLeadDatainGridForAccounts(searchFilter);
                listOfLeads = objBLLeads.ReadLeadDatainGridForSalesHead(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfLeads, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfLeads != null && listOfLeads.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfLeads.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfLeads.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfLeads,
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

        public JsonResult GetLeadDataForDataAnalyst(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enLeadManagementSubscription> listOfLeads = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLLeads = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                searchFilter.UserId = UserId;
            
                listOfLeads = objBLLeads.ReadLeadDatainGridForDataAnalyst(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfLeads, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfLeads != null && listOfLeads.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfLeads.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfLeads.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfLeads,
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

        public async Task<JsonResult> CreateSubscriptionByDataAnalyst(int Leadid)
        {
            string Message = "";
            string UserName = Convert.ToString(Session["User"]);
            string UserEmails = "";
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);
            List<enLeadManagementSubscription> flagStatus = new List<enLeadManagementSubscription>();
            List<enLeadManagementSubscription> ListOfCustDetails = new List<enLeadManagementSubscription>();
            try
            {
                List<enLeadManagementSubscription> listOfdat = new List<enLeadManagementSubscription>();
                List<enLeadManagementSubscription> listOfrecord = new List<enLeadManagementSubscription>();
                enSearchFilter objENSearchFilter = new enSearchFilter();



                objENSearchFilter.LeadId = Convert.ToInt32(Leadid);
                //listOfrecord = new blLeadManagementSubscription(new enLeadManagementSubscription()).GetprimaryClintRecord(objENSearchFilter);
                listOfrecord = new blLeadManagementSubscription(new enLeadManagementSubscription()).GetListofLeadDetails(objENSearchFilter);
                // listOfdat = new blLeadManagementSubscription(new enLeadManagementSubscription()).GetLeaddetailsByid(objENSearchFilter);
                string[] emailArray = null;
                foreach (var itemcorpemails in listOfrecord)
                {
                    emailArray = itemcorpemails.CorpEmail.Split(',');

                }

                foreach (var itemcorp in emailArray)
                {
                    obj.Id = Leadid;
                    obj.CorpEmail = itemcorp;
                    obj.createdby = UserName;
                    string strPassword = "Techsci44@Re";
                    string strEncrypted = (Helpers.Encrypt(strPassword));
                    string strDecrypted = (Helpers.Decrypt(strEncrypted));
                    obj.Password = strEncrypted;
           

                    flagStatus = await objB.InsertRecordAsync(obj);

                    //ListOfCustDetails = objB.GetCustSubscriptionDetailsbyId();

                    //foreach (var item in ListOfCustDetails)
                    //{
                    //    string EmailBody = SubscriptionDAL.GetHtml("PackageWelcome.html");
                    //    EmailBody = EmailBody.Replace("@UserName@", item.FName + " " + item.LName);
                    //    EmailBody = EmailBody.Replace("@Pass@", "Techsci44@Re");
                    //    EmailBody = EmailBody.Replace("@SubscriptionType@", item.SubscriptionType);
                    //    EmailBody = EmailBody.Replace("@CountryOrRegionNames@", item.CountryOrRegionNames);
                    //    EmailBody = EmailBody.Replace("@Email@", item.CorpEmail);
                    //    EmailBody = EmailBody.Replace("@StateDate@", item.DateFrom.Value.ToString());
                    //    EmailBody = EmailBody.Replace("@ToDate@", item.DateTo.Value.ToString());
                    //    EmailBody = EmailBody.Replace("@Products@", item.ProductName);
                    //    EmailBody = EmailBody.Replace("@Modules@", item.ModuleName);
                    //    //SubscriptionDAL.SendMail("Chem Analyst", "info@chemanalyst", item.CorpEmail, "Package Activation", EmailBody, "sales@chemanalyst.com");
                    //    SubscriptionDAL.SendMail("Chem Analyst", "info@chemanalyst", "crm@chemanalyst.com", "Package Activation", EmailBody, "crm@chemanalyst.com");
                    //}

                   // //flagStatus = objB.InsertSubscriptionDetailsbyId();
                   // //}
                }
            }
            catch (Exception ex)
            {


            }
            foreach (var status in flagStatus)
            {
                Message = status.ValidationMessage;
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }




        public JsonResult GetRecordByLeadIdForEdit(int Leadid)
        {
          
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);
      
            List<enLeadManagementSubscription> ListOfLead = new List<enLeadManagementSubscription>();
            try
            {
                string UserName = Convert.ToString(Session["User"]);

                obj.Id = Leadid;
               
                if (Leadid != 0)
                {
                    ListOfLead = objB.GetRecordByLeadId();
                   
                }
        
             
            }
            catch (Exception ex)
            {


            }
            return Json(ListOfLead, JsonRequestBehavior.AllowGet);
        }
       public JsonResult UpdateLeadRecord(LeadManagementSubscription LeadDetails)
        {
            enLeadManagementSubscription enObj = new enLeadManagementSubscription();
            string Message = "";

            try
            {
                    enObj.Id = LeadDetails.Id;
                    enObj.Name = LeadDetails.Name;
                    enObj.CorpEmail = LeadDetails.CorpEmail;
                    enObj.Organisation = LeadDetails.Organisation;
                    enObj.CountryID = LeadDetails.CountryID;
                    enObj.StateId = LeadDetails.StateId;
                    enObj.Phone = LeadDetails.Phone;
                    enObj.module = LeadDetails.module;
                    enObj.UserType = LeadDetails.UserType;
                    enObj.Status = "Updated";
                    enObj.IPAddress = "";//LeadDetails.Name;
                    enObj.BillingEmail = LeadDetails.BillingEmail;
                    enObj.SubscriptionType = LeadDetails.SubscriptionType;
                    enObj.AssignAnalystId = LeadDetails.AssignAnalyst;
                    enObj.SecondaryClintEmail = LeadDetails.SecondaryClintEmail;
                    enObj.SubscriptionDateFrom = LeadDetails.SubscriptionDateFrom;
                    enObj.SubscriptionDateTo = LeadDetails.SubscriptionDateTo;
                    enObj.Subscription_Country = LeadDetails.Subscription_Country;
                    enObj.Subscription_Product = LeadDetails.Subscription_Product;
                    enObj.Chemical_Range = LeadDetails.Chemical_Range;
                    enObj.Market_Analysis_Range = LeadDetails.Market_Analysis_Range;
                    enObj.SubSubscriptionRemark = LeadDetails.SubSubscriptionRemark;
                    enObj.createdby = Session["User"].ToString();
                    enObj.RegionId = Convert.ToString(LeadDetails.RegionId);
                    if (enObj.CorpEmail != "")
                    {
                        List<enLeadManagementSubscription> ListofRespone = new List<enLeadManagementSubscription>();
                      
                        ListofRespone = new blLeadManagementSubscription(new enLeadManagementSubscription()).UpdateInsertLeadRecord(enObj);
                        foreach (var ResponseMessage in ListofRespone)
                        {
                            Message = ResponseMessage.ValidationMessage;
                        }
                    }
                    else
                    {

                    } 

                
            }
            catch (Exception ex)
            {

            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }
       public JsonResult InsertrecordForSalesPackageSubscriptions(LeadManagementSubscription LeadDetails)
        {
            
            enLeadManagementSubscription enObj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(enObj);
           
           
            string Message = "";
            List<enLeadManagementSubscription> ListOfCustDetails = new List<enLeadManagementSubscription>();
            try
            {
               
                //enObj.CorpEmail = LeadDetails.CorpEmail;
                var UserId = from val in LeadDetails.CorpEmail.Split(',')
                            select int.Parse(val);

              
                foreach (int User in UserId)
                {
                enObj.Id = LeadDetails.Id;
                enObj.CorpEmail = Convert.ToString(User);
                enObj.Organisation = LeadDetails.Organisation;
                enObj.CountryID = LeadDetails.CountryID;
                enObj.module = LeadDetails.module;
                enObj.SubscriptionType = LeadDetails.SubscriptionType;
                enObj.SubscriptionDateFrom = LeadDetails.SubscriptionDateFrom;
                enObj.SubscriptionDateTo = LeadDetails.SubscriptionDateTo;
                enObj.Subscription_Country = LeadDetails.Subscription_Country;
                enObj.Subscription_Product = LeadDetails.Subscription_Product;
                enObj.Chemical_Range = LeadDetails.Chemical_Range;
                enObj.Market_Analysis_Range = LeadDetails.Market_Analysis_Range;
                enObj.createdby = Session["User"].ToString();
                enObj.RegionId = Convert.ToString(LeadDetails.RegionId);
                  
                 if (enObj.CorpEmail != "")
                {
                    List<enLeadManagementSubscription> ListofRespone = new List<enLeadManagementSubscription>();
                  ListofRespone = new blLeadManagementSubscription(new enLeadManagementSubscription()).InsertrecordForSalesPackageSubscriptions(enObj);
                    foreach (var ResponseMessage in ListofRespone)
                    {
                        Message = ResponseMessage.ValidationMessage;
                    }
                }
                 else
                {

                }
                  
                    ListOfCustDetails = objB.GetCustSubscriptionDetailsbyIdOnupdate();
                    foreach (var item in ListOfCustDetails)
                    {
                        string EmailBody = SubscriptionDAL.GetHtml("PackageWelcome.html");
                        EmailBody = EmailBody.Replace("@UserName@", item.FName + " " + item.LName);
                        EmailBody = EmailBody.Replace("@Pass@", "Techsci44@Re");
                        EmailBody = EmailBody.Replace("@SubscriptionType@", item.SubscriptionType);
                        EmailBody = EmailBody.Replace("@CountryOrRegionNames@", item.CountryOrRegionNames);
                        EmailBody = EmailBody.Replace("@Email@", item.CorpEmail);
                        EmailBody = EmailBody.Replace("@StateDate@", item.DateFrom.Value.ToString());
                        EmailBody = EmailBody.Replace("@ToDate@", item.DateTo.Value.ToString());
                        EmailBody = EmailBody.Replace("@Products@", item.ProductName);
                        EmailBody = EmailBody.Replace("@Modules@", item.ModuleName);
                        //SubscriptionDAL.SendMail("Chem Analyst", "info@chemanalyst", item.CorpEmail, "Package Activation", EmailBody, "sales@chemanalyst.com");
                        SubscriptionDAL.SendMail("Chem Analyst", "info@chemanalyst", "crm@chemanalyst.com", "Package Activation", EmailBody, "crm@chemanalyst.com");
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }
      


        public JsonResult GetSalesPackageSubscriptions(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enLeadManagementSubscription> listOfPackage = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLpackage = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                searchFilter.UserId = UserId;

                listOfPackage = objBLpackage.ReadSalesPackageSubscriptions(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfPackage, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfPackage != null && listOfPackage.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfPackage.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfPackage.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfPackage,
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
       public JsonResult GetRecordBySalesPackageID(int SalesPackageID)
        {

            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> ListOfLead = new List<enLeadManagementSubscription>();
            try
            {
           
                obj.Id = SalesPackageID;

                if (SalesPackageID != 0)
                {
                    ListOfLead = objB.GetRecordBySalesPackageID();
                }


            }
            catch (Exception ex)
            {


            }
            return Json(ListOfLead, JsonRequestBehavior.AllowGet);
        }
       public JsonResult UpdateSalesPackage(LeadManagementSubscription LeadDetails)
        {
            enLeadManagementSubscription enObj = new enLeadManagementSubscription();
            string Message = "";

            try
            {
                enObj.Id = LeadDetails.Id;
                enObj.SubscriptionDateFrom = LeadDetails.SubscriptionDateFrom;
                enObj.SubscriptionDateTo = LeadDetails.SubscriptionDateTo;
                enObj.Subscription_Product = LeadDetails.Subscription_Product;
                enObj.module = LeadDetails.module;
                enObj.Chemical_Range = LeadDetails.Chemical_Range;
                enObj.Market_Analysis_Range = LeadDetails.Market_Analysis_Range;

                if (enObj.Id != 0 && enObj.Subscription_Product!="")
                {
                    List<enLeadManagementSubscription> ListofRespone = new List<enLeadManagementSubscription>();
                    ListofRespone = new blLeadManagementSubscription(new enLeadManagementSubscription()).UpdatePackageRecord(enObj);
                    foreach (var ResponseMessage in ListofRespone)
                    {
                        Message = ResponseMessage.ValidationMessage;
                    }
                }
                else
                {

                }

            }
            catch (Exception ex)
            {

            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        } 
        [HttpPost]
        public JsonResult GetLeadDetails(int LeadId,string StatusType)
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfLeadDetails = new List<enLeadManagementSubscription>();
            try
            {
                if (LeadId != 0)
                {
                    objENSearchFilter.LeadId = LeadId;
                    objENSearchFilter.Status = StatusType;
                    listOfLeadDetails = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadLeadDetails(objENSearchFilter);

                }
                else
                {
                    return Json(Message = "Faild", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }

            return Json(listOfLeadDetails, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            string path = Server.MapPath("~/ChemPricingImportfile/SalesDocument/");
            HttpFileCollectionBase files = Request.Files;
            try
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    file.SaveAs(path + file.FileName);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(files.Count + " Files Uploaded!");
        }
        public FileResult DownloadFile(string filePath, string fileName)
        {
            try {

                string fullPath = Server.MapPath("~") + "/" + filePath;
                return File(fullPath, "application/vnd.ms-excel", fileName);
            }
            catch (FileNotFoundException ex)
            {
                return null;
            }

           
        }
        public JsonResult GetProductName()
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfProduct = new List<enLeadManagementSubscription>();
            try
            {
                objENSearchFilter.ActionType = "Product";
               
                listOfProduct = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadProduct(objENSearchFilter);


            }
            catch (Exception ex)
            {

            }
            var returnValue = new
            {
                DataList = listOfProduct,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
           
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
                objENSearchFilter.ActionType = "ClintEmail";

                listOfReadClintEmail = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadClintEmail(objENSearchFilter);


            }
            catch (Exception ex)
            {

            }

            var returnValue = new
            {
                DataList = listOfReadClintEmail,
            };
            return Json(listOfReadClintEmail, JsonRequestBehavior.AllowGet);
            //var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;

        }

        public JsonResult GetClintEmailByCompanyId(string CompanyId)
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfReadClintEmail = new List<enLeadManagementSubscription>();
            try
            {
                objENSearchFilter.ActionType = "ClintEmail";
                objENSearchFilter.Id = Convert.ToInt32(CompanyId);
                //listOfReadClintEmail = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadClintEmail(objENSearchFilter);
                listOfReadClintEmail = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadClintEmailByCompanyId(objENSearchFilter);


            }
            catch (Exception ex)
            {

            }

            var returnValue = new
            {
                DataList = listOfReadClintEmail,
            };
            return Json(listOfReadClintEmail, JsonRequestBehavior.AllowGet);
            //var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;

        }

        public JsonResult GetClintCompany()
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfrecord= new List<enLeadManagementSubscription>();
            try
            {
                objENSearchFilter.ActionType = "ClintCompany";
                
                listOfrecord = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadClintCompany(objENSearchFilter);

            }
            catch (Exception ex)
            {

            }

            var returnValue = new
            {
                DataList = listOfrecord,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public JsonResult GetClintCountry()
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfrecord = new List<enLeadManagementSubscription>();
            try
            {
                objENSearchFilter.ActionType = "ClintCountry";

                //listOfrecord = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadClintEmail(objENSearchFilter);
                listOfrecord = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadClintCountry(objENSearchFilter);

            }
            catch (Exception ex)
            {

            }

            var returnValue = new
            {
                DataList = listOfrecord,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public JsonResult GetDataAnalyst()
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfrecord = new List<enLeadManagementSubscription>();
            try
            {
                objENSearchFilter.ActionType = "DataAnalyst";

              
                listOfrecord = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadDataAnalyst(objENSearchFilter);

            }
            catch (Exception ex)
            {

            }

            var returnValue = new
            {
                DataList = listOfrecord,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public JsonResult GetModule()
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfrecord = new List<enLeadManagementSubscription>();
            try
            {
                objENSearchFilter.ActionType = "Module";
                listOfrecord = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadModule(objENSearchFilter);
            }
            catch (Exception ex)
            {

            }

            var returnValue = new
            {
                DataList = listOfrecord,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public ActionResult SaveCompanyMaster(enCompanyMaster model)
        {
            string Message = "";
            blCompanyMaster objBLCompanyMaster = new blCompanyMaster(model);
            try
            {
                if (model.ID > 0)
                    objBLCompanyMaster.Update();
                else
                    objBLCompanyMaster.Create();
                Message = "Success";
            }
            catch (Exception ex)
            {

            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }


        public JsonResult InsertrecordprimarytoSecondary(string PrimaryUser, string SecondaryUser, int LeadId)
        {
            enLeadManagementSubscription enObj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(enObj);
            List<enLeadManagementSubscription> ListOfCustDetails = new List<enLeadManagementSubscription>();
            List<enLeadManagementSubscription> ListofRespone = new List<enLeadManagementSubscription>();
            string Message = "";

            try
            {

                //enObj.CorpEmail = LeadDetails.CorpEmail;
                var UserId = from val in SecondaryUser.Split(',')
                             select int.Parse(val);
                List<enLeadManagementSubscription> listOfrecord = new List<enLeadManagementSubscription>();
                enSearchFilter objENSearchFilter = new enSearchFilter();
                objENSearchFilter.UserId = Convert.ToInt32(PrimaryUser);
                //listOfrecord = new blLeadManagementSubscription(new enLeadManagementSubscription()).GetprimaryClintRecord(objENSearchFilter);

                foreach (int User in UserId)
                {
                    //foreach (var items in listOfrecord)
                    //{
                    //    enObj.Subscription_Product = items.Subscription_Product;
                    //    enObj.module = items.module;
                    enObj.Id = LeadId;
                    enObj.CorpEmail = Convert.ToString(User);
                    enObj.UserId = Convert.ToInt32(PrimaryUser);
                    //    enObj.SubscriptionType = items.SubscriptionType;
                    //    enObj.Status = "Active";
                    //    enObj.SubscriptionDateFrom = Convert.ToString(items.DateFrom);
                    //    enObj.SubscriptionDateTo = Convert.ToString(items.DateTo);
                    //    enObj.RegionId = Convert.ToString(items.RegionId);
                    //    enObj.Subscription_Country = items.CountryName;
                    //    enObj.Chemical_Range = items.Chemical_Range;
                    //    enObj.Market_Analysis_Range = items.Market_Analysis_Range;

                    ListofRespone = new blLeadManagementSubscription(new enLeadManagementSubscription()).InsertrecordForSalesPackageSubscriptions(enObj);

                    ListOfCustDetails = objB.GetCustSubscriptionDetailsbyIdOnupdate();
                    foreach (var item in ListOfCustDetails)
                    {
                        string EmailBody = SubscriptionDAL.GetHtml("PackageWelcome.html");
                        EmailBody = EmailBody.Replace("@UserName@", item.FName + " " + item.LName);
                        EmailBody = EmailBody.Replace("@Pass@", "Techsci44@Re");
                        EmailBody = EmailBody.Replace("@SubscriptionType@", item.SubscriptionType);
                        EmailBody = EmailBody.Replace("@CountryOrRegionNames@", item.CountryOrRegionNames);
                        EmailBody = EmailBody.Replace("@Email@", item.CorpEmail);
                        EmailBody = EmailBody.Replace("@StateDate@", item.DateFrom.Value.ToString());
                        EmailBody = EmailBody.Replace("@ToDate@", item.DateTo.Value.ToString());
                        EmailBody = EmailBody.Replace("@Products@", item.ProductName);
                        EmailBody = EmailBody.Replace("@Modules@", item.ModuleName);
                        SubscriptionDAL.SendMail("Chem Analyst", "info@chemanalyst", "crm@chemanalyst.com", "Package Activation", EmailBody, "crm@chemanalyst.com");
                    }
                }


                // }

            }
            catch (Exception ex)
            {

            }
            foreach (var ResponseMessage in ListofRespone)
            {
                Message = ResponseMessage.ValidationMessage;
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SavePaymentDetails(string CompanyName, string Invoice, string Amount, string PaymentMode,
            string PaymentDate, string CorpEmail, string Organisation, string BillingEmail, string LeadId)
        {
            string Message = "";
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);
            List<enLeadManagementSubscription> retunstatus = new List<enLeadManagementSubscription>();
            try
            {
                string UserName = Convert.ToString(Session["User"]);

                obj.CompanyName = CompanyName;
                obj.Invoice = Invoice;
                obj.Amount = Amount;
                obj.PaymentMode = PaymentMode;
                obj.PaymentDate = PaymentDate;
                obj.PaymentDate = PaymentDate;
                obj.CorpEmail = CorpEmail;
                obj.Organisation = Organisation;
                obj.BillingEmail = BillingEmail;
                obj.LeadId = Convert.ToInt32(LeadId);
                obj.createdby = UserName;
          
                if ((CompanyName != "") && (Amount != ""))
                {
                   
                    retunstatus = objB.SavePaymentDetails();
                }
                foreach (var status in retunstatus)
                {
                    Message = status.ValidationMessage;
                }

            }
            catch (Exception ex)
            {


            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckPaymentDetails(string CorpEmail)
        {
            string Message = "";
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);
            List<enLeadManagementSubscription> Status = new List<enLeadManagementSubscription>();
            try
            {
                obj.CorpEmail = CorpEmail;
                if (CorpEmail != "")
                {
                    Status = objB.UpdateLeadApprovalStatus();
                    Status = objB.CheckPaymentDetailsStatus();
                }
                foreach (var status in Status)
                {
                    Message = status.ValidationMessage;
                }

            }
            catch (Exception ex)
            {


            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetClientPrimaryEmailIdRecord(string ClientPrimaryEmailId)
        {
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfrecord = new List<enLeadManagementSubscription>();
            try
            {
                if (ClientPrimaryEmailId != "")
                {
                    objENSearchFilter.UserFID = Convert.ToInt32(ClientPrimaryEmailId);
                    listOfrecord = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadClientPrimaryEmailIdRecord(objENSearchFilter);

                }
                else
                {
                    return Json(listOfrecord, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }
            var returnValue = new
            {
                DataList = listOfrecord,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
            //return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatestatusofLead(int LeadId)
        {
            string Message = "";
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> Lead = new List<enLeadManagementSubscription>();
            try
            {
                if (LeadId != 0)
                {
                    objENSearchFilter.LeadId = Convert.ToInt32(LeadId);
                  
                    Lead = new blLeadManagementSubscription(new enLeadManagementSubscription()).UpdatestatusofLead(objENSearchFilter);

                }
                else
                {
                    return Json(Message = "Faild", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }
            foreach (var ResponseMessage in Lead)
            {
                Message = ResponseMessage.ValidationMessage;
            }
            return Json(Message, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetAllOrganisation()
        {
            List<enLeadManagementSubscription> ListenLeadManagementSubscription = new List<enLeadManagementSubscription>();
            enLeadManagementSubscription enLeadManagementSubscription = new enLeadManagementSubscription();
            blLeadManagementSubscription blLeadManagementSubscription = new blLeadManagementSubscription(new enLeadManagementSubscription());


            ListenLeadManagementSubscription = blLeadManagementSubscription.GetAllOrganisation(enLeadManagementSubscription);


            return Json(ListenLeadManagementSubscription, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllEmailbasedonOrganisation(enLeadManagementSubscription enLeadManagementSubscription)
        {
            List<enLeadManagementSubscription> ListenLeadManagementSubscription = new List<enLeadManagementSubscription>();
            enLeadManagementSubscription objenLeadManagementSubscription = new enLeadManagementSubscription();
            blLeadManagementSubscription blLeadManagementSubscription = new blLeadManagementSubscription(new enLeadManagementSubscription());


            ListenLeadManagementSubscription = blLeadManagementSubscription.GetAllEmailbasedonOrganisation(enLeadManagementSubscription);


            return Json(ListenLeadManagementSubscription, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertCustomerCreditScore(enLeadManagementSubscription enLeadManagementSubscription)
        {
            List<enLeadManagementSubscription> ListenLeadManagementSubscription = new List<enLeadManagementSubscription>();
            enLeadManagementSubscription objenLeadManagementSubscription = new enLeadManagementSubscription();
            blLeadManagementSubscription blLeadManagementSubscription = new blLeadManagementSubscription(new enLeadManagementSubscription());


            ListenLeadManagementSubscription = blLeadManagementSubscription.InsertCustomerCreditScore(enLeadManagementSubscription);


            return Json(ListenLeadManagementSubscription, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCreditBasedonEmail(enLeadManagementSubscription enLeadManagementSubscription)
        {
            List<enLeadManagementSubscription> ListenLeadManagementSubscription = new List<enLeadManagementSubscription>();
            enLeadManagementSubscription objenLeadManagementSubscription = new enLeadManagementSubscription();
            blLeadManagementSubscription blLeadManagementSubscription = new blLeadManagementSubscription(new enLeadManagementSubscription());


            ListenLeadManagementSubscription = blLeadManagementSubscription.GetCreditBasedonEmail(enLeadManagementSubscription);


            return Json(ListenLeadManagementSubscription, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddCustomerCreditPoint()
        {
            return View();
        }
        public JsonResult GetCountryByModuleWise(string Module)
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfrecord = new List<enLeadManagementSubscription>();
            try
            {
                objENSearchFilter.ActionType = Module;

                listOfrecord = new blLeadManagementSubscription(new enLeadManagementSubscription()).GetCountryByModuleWise(objENSearchFilter);
                //listOfrecord = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadClintCountry(objENSearchFilter);

            }
            catch (Exception ex)
            {

            }

            var returnValue = new
            {
                DataList = listOfrecord,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public JsonResult GetProductByCountryName(string SubscriptionRegionCountryName, string Type, string Module)
        {
            string Message = "Success";
            int userId = Convert.ToInt32(Session["LoginUser"]);
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfProduct = new List<enLeadManagementSubscription>();
            try
            {
                objENSearchFilter.ActionType = SubscriptionRegionCountryName;
                objENSearchFilter.Type = Type;//---region wise or country Wise
                objENSearchFilter.ModuleName = Module; //---Module Name 

                //listOfProduct = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadProduct(objENSearchFilter);
                listOfProduct = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadProductByCoutryName(objENSearchFilter);


            }
            catch (Exception ex)
            {

            }
            var returnValue = new
            {
                DataList = listOfProduct,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
         public JsonResult GetCRMCompanyRelation(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enLeadManagementSubscription> listOfCrmLeads = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLLeads = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                searchFilter.UserId = UserId;

                listOfCrmLeads = objBLLeads.GetListofCRMCompanyRalation(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfCrmLeads, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfCrmLeads != null && listOfCrmLeads.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfCrmLeads.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfCrmLeads.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfCrmLeads,
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
         

      

   public ActionResult CreateOutboundLead()
        {
            return View();
        }
        public ActionResult GetYourLeadList()
        {
            return View();
        }
        public ActionResult GetYourOpenLeadList()
        {
            return View();
        }
        public ActionResult GetCRMOpenLeadList()
        {
            return View();
        }
        public ActionResult CRM_OpenLeadList()
        {
            return View();
        }
        //delete this Methods
        public ActionResult CRM_RenewLeadList()
        {
            return View();
        }
        public ActionResult SalesDashBoard()
        {
            return View();
        }
        public ActionResult CRMLeadDashBoard()
        {
            return View();
        }
        public ActionResult SalesPackageExpiry()
        {
            return View();
        }
        public ActionResult CRMRenew_LeadDetails(string Id)
        {
            ViewBag.LeadId = Id;
            return View();
        }
        public ActionResult SalesInbound_OutboundEditLeads(string Id)
        {
            ViewBag.LeadId = Id;

            return View();
        }
        public ActionResult SalesPersonCountryRelation()
        {
            return View();
        }
        public ActionResult SalesPersonCountryRelationList()
        {
            return View();
        }
        public ActionResult SalesInboundOutboundEditLeadsFullDetails(string Id)
        {
            ViewBag.LeadId = Id;
            return View();
        }
        public ActionResult CRMRenewLeadFullDetails(string Id)
        {
            ViewBag.LeadId = Id;
            return View();
        }
        public JsonResult SaveOutboundLeadDetails(Lead_Master GetDetails)
        {
            enLeadManagementSubscription enObj = new enLeadManagementSubscription();
            string Message = "";
            List<enLeadManagementSubscription> ListofRespone = new List<enLeadManagementSubscription>();
            try
            {
                enObj.Name = GetDetails.Name;
                enObj.CorpEmail = GetDetails.CorpEmail;
                enObj.Mobile = GetDetails.Mobile;
                enObj.CompanyName = GetDetails.Organisation;
                enObj.CountryName = GetDetails.Country;
                enObj.StateName = GetDetails.State;
                enObj.Designation = GetDetails.Designation;
                enObj.DiscriptionMessage = GetDetails.DiscriptionMessage;
                enObj.OutboundLeadSource = GetDetails.OutboundLeadSource;
                enObj.LeadType = GetDetails.LeadType;
                enObj.IPAddress = GetIPAddress();
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                enObj.Phone = GetDetails.Phone;
                enObj.createdby = Session["User"].ToString();
                enObj.RequestedDate = GetDetails.RequestedDate;
                enObj.ContactCountryCode = GetDetails.ContactCountryCode;
                enObj.TelephoneCountryCode = GetDetails.TelephoneCountryCode;


                ListofRespone = new blLeadManagementSubscription(new enLeadManagementSubscription()).InsertOutboundLeadRecord(enObj);

            }
            catch (Exception ex)
            {

            }

            return Json(ListofRespone, JsonRequestBehavior.AllowGet);
        }
        private string GetIPAddress()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
            //string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            //Console.WriteLine(hostName);
            //// Get the IP  
            //return Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
        public JsonResult GetYourLeadList_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.EximPageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.EximPageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {

                searchFilter.UserName = Session["User"].ToString(); 

                listOfdata = objBLs.GetYourLeadList(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.EximPageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                PagerSize = (int)ApplicationSettings.EximPageSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.EximPageSize,
                //SearchFilterHasValue = isSearchFilterHasValue,
                SearchFilterHasValue = string.Empty,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult ChangestatusofLead(int LeadId)
        {
            string Message = "";
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> Lead = new List<enLeadManagementSubscription>();
            try
            {
                if (LeadId != 0)
                {
                    objENSearchFilter.LeadId = Convert.ToInt32(LeadId);
                    Lead = new blLeadManagementSubscription(new enLeadManagementSubscription()).ChangestatusofLead(objENSearchFilter);

                }
                else
                {
                    return Json(Message = "Faild", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }
            foreach (var ResponseMessage in Lead)
            {
                Message = ResponseMessage.ValidationMessage;
            }
            return Json(Message, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ChangestatusofLeadCRM(int LeadId)
        {
            string Message = "";
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> Lead = new List<enLeadManagementSubscription>();
            try
            {
                if (LeadId != 0)
                {
                    objENSearchFilter.LeadId = Convert.ToInt32(LeadId);

                    Lead = new blLeadManagementSubscription(new enLeadManagementSubscription()).ChangestatusofLeadCRM(objENSearchFilter);

                }
                else
                {
                    return Json(Message = "Faild", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }
            foreach (var ResponseMessage in Lead)
            {
                Message = ResponseMessage.ValidationMessage;
            }
            return Json(Message, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetOpenMaskEmailList_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.EximPageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.EximPageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.UserName = Session["User"].ToString(); ;
                listOfdata = objBLs.GetYourOpenLeadList(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.EximPageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                PagerSize = (int)ApplicationSettings.EximPageSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.EximPageSize,
                //SearchFilterHasValue = isSearchFilterHasValue,
                SearchFilterHasValue = string.Empty,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult BindOpenLeadDetailsByid(int Id)
        {
            string Message = "";
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> ListofData = new List<enLeadManagementSubscription>();
            try
            {
                if (Id != 0)
                {
                    objENSearchFilter.Id = Convert.ToInt32(Id);

                    ListofData = new blLeadManagementSubscription(new enLeadManagementSubscription()).BindOpenLeadDetailsByid(objENSearchFilter);
                }
                else
                {
                    return Json(ListofData, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }

            return Json(ListofData, JsonRequestBehavior.AllowGet);

        }
        public JsonResult BindCRMOpenLeadDetailsByid(int Id)
        {
            string Message = "";
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> ListofData = new List<enLeadManagementSubscription>();
            try
            {
                if (Id != 0)
                {
                    objENSearchFilter.Id = Convert.ToInt32(Id);
                    ListofData = new blLeadManagementSubscription(new enLeadManagementSubscription()).BindCRMOpenLeadDetailsByid(objENSearchFilter);
                }
                else
                {
                    return Json(ListofData, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }

            return Json(ListofData, JsonRequestBehavior.AllowGet);

        }
        public JsonResult UpdateOpenLeadDetails(Lead_Master GetDetails)
        {
            enLeadManagementSubscription enObj = new enLeadManagementSubscription();
            string Message = "";
            List<enLeadManagementSubscription> ListofRespone = new List<enLeadManagementSubscription>();
            try
            {
                enObj.SetFollowUp = GetDetails.SetFollowUp;
                enObj.Amount = GetDetails.Amount;
                enObj.ExpectedCloseDate = GetDetails.ExpectedCloseDate;
                enObj.Currency = GetDetails.Currency;
                enObj.Id = GetDetails.Id;
                enObj.files = GetDetails.files;
                enObj.createdby = Session["User"].ToString();

                enObj.Status = GetDetails.Status;
                enObj.Invoice = GetDetails.InvoiceNumber;
                enObj.LostReason = GetDetails.LostReason;
                enObj.PaymentTerms = GetDetails.PaymentTerms;
                enObj.PaymentOption = GetDetails.PaymentOption;
                enObj.RemainingPaymentDate = GetDetails.RemainingPaymentDate;
                enObj.DiscriptionMessage = GetDetails.LastFollowupDiscussion;
                enObj.Organisation = GetDetails.Organisation;

                enObj.CountryID = GetDetails.CountryID;
                enObj.StateId = GetDetails.StateId;
                enObj.CountryName = GetDetails.CountryName;
                enObj.StateName = GetDetails.StateName;

                enObj.Phone = GetDetails.Phone;
                enObj.Mobile = GetDetails.Mobile;
                enObj.Name = GetDetails.Name;
                enObj.CorpEmail = GetDetails.CorpEmail;
                enObj.RemainingAmount = GetDetails.RemainingAmount;
                enObj.Designation = GetDetails.Designation;
                enObj.ContactCountryCode = GetDetails.ContactCountryCode;
                enObj.TelephoneCountryCode = GetDetails.TelephoneCountryCode;
                enObj.DemoDiscussion = GetDetails.DemoDiscussion;
                enObj.LastDemoDate = GetDetails.LastDemoDate;


                ListofRespone = new blLeadManagementSubscription(new enLeadManagementSubscription()).UpdateOpenLeadDetails(enObj);

            }
            catch (Exception ex)
            {

            }

            return Json(ListofRespone, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UploadLeadFiles()
        {
            string path = Server.MapPath("~/ChemPricingImportfile/SalesLeadDocuments/");
            HttpFileCollectionBase files = Request.Files;
            int leadId = Convert.ToInt32(Request["LeadId"]);
            try
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    string customName = fileNameWithoutExt + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + leadId + extension;
                    string savePath = Path.Combine(path, customName);
                    file.SaveAs(savePath);
                }
            }

            catch (Exception ex)
            {

            }
            return Json(files.Count + " Files Uploaded!");
        }
        [HttpPost]
        public ActionResult UploadLeadCRMFiles()
        {
            string path = Server.MapPath("~/ChemPricingImportfile/CRMLeadDocuments/");
            HttpFileCollectionBase files = Request.Files;
            try
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    file.SaveAs(path + file.FileName);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(files.Count + " Files Uploaded!");
        }
        public JsonResult GetCRM_OpenMaskEmailList_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.UserName = Session["User"].ToString(); ;

                listOfdata = objBLs.GetYourCRMOpenLeadList(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
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
        public JsonResult GetCRM_RenewMaskEmailList_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.UserName = Session["User"].ToString(); ;


                listOfdata = objBLs.GetYourCRMRenewLeadList(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
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
        public JsonResult UpdateCRMOpenLeadDetails(Lead_Master GetDetails)
        {
            enLeadManagementSubscription enObj = new enLeadManagementSubscription();
            string Message = "";
            List<enLeadManagementSubscription> ListofRespone = new List<enLeadManagementSubscription>();
            try
            {
                enObj.SetFollowUp = GetDetails.SetFollowUp;
                enObj.Amount = GetDetails.Amount;
                enObj.ExpectedCloseDate = GetDetails.ExpectedCloseDate;
                enObj.Currency = GetDetails.Currency;
                enObj.Id = GetDetails.Id;
                enObj.files = GetDetails.files;
                enObj.createdby = Session["User"].ToString();

                enObj.Status = GetDetails.Status;
                enObj.Invoice = GetDetails.InvoiceNumber;
                enObj.LostReason = GetDetails.LostReason;
                enObj.PaymentTerms = GetDetails.PaymentTerms;
                enObj.PaymentOption = GetDetails.PaymentOption;
                enObj.RemainingPaymentDate = GetDetails.RemainingPaymentDate;
                enObj.DiscriptionMessage = GetDetails.LastFollowupDiscussion;
                enObj.Organisation = GetDetails.Organisation;

                enObj.CountryID = GetDetails.CountryID;
                enObj.StateId = GetDetails.StateId;
                enObj.CountryName = GetDetails.CountryName;
                enObj.StateName = GetDetails.StateName;

                enObj.Phone = GetDetails.Phone;
                enObj.Mobile = GetDetails.Mobile;
                enObj.Name = GetDetails.Name;
                enObj.CorpEmail = GetDetails.CorpEmail;
                enObj.RemainingAmount = GetDetails.RemainingAmount;
                enObj.Designation = GetDetails.Designation;
                enObj.TelephoneCountryCode = GetDetails.TelephoneCountryCode;
                enObj.ContactCountryCode = GetDetails.ContactCountryCode;
                enObj.DemoDiscussion = GetDetails.DemoDiscussion;
                enObj.LastDemoDate = GetDetails.LastDemoDate;

                ListofRespone = new blLeadManagementSubscription(new enLeadManagementSubscription()).UpdateCRMOpenLeadDetails(enObj);

            }
            catch (Exception ex)
            {

            }

            return Json(ListofRespone, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSalesTodaysFollowUP_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.UserName = Session["User"].ToString(); ;

                listOfdata = objBLs.GetSalesTodaysFollowUP_CustomSearch(searchFilter);

            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
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
        public JsonResult GetSalesTodaysRemainingDate_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.UserName = Session["User"].ToString(); ;

                listOfdata = objBLs.GetSalesTodaysRemainingDate_CustomSearch(searchFilter);

            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
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
        public JsonResult GetCRMTodaysFollowUP_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.UserId = Convert.ToInt32(Session["LoginUser"]);

                listOfdata = objBLs.GetCRMTodaysFollowUP_CustomSearch(searchFilter);

            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
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
        public JsonResult GetCRMTodaysRemainingDate_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.UserName = Session["User"].ToString(); ;

                listOfdata = objBLs.GetCRMTodaysRemainingDate_CustomSearch(searchFilter);

            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
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
        public JsonResult GetsaleshpackageTodaysExpiry_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.UserId = Convert.ToInt32(Session["LoginUser"]);

                listOfdata = objBLs.GetCRMTodaysExpiry_CustomSearch(searchFilter);

            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
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
        public JsonResult GetSalesPackageExpiry_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {

                searchFilter.UserId = Convert.ToInt32(Session["LoginUser"]);
                listOfdata = objBLs.GetSalesPackageExpiry(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
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
        public JsonResult GetFollowupDicussion(int Id)
        {
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                obj.Id = Id;
                listOfdata = objBLs.GetFollowupDicussion(obj);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            var returnValue = new
            {
                DateList = listOfdata,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult GetDemoDicussion(int Id)
        {
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                obj.Id = Id;
                listOfdata = objBLs.GetDemoDicussion(obj);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            var returnValue = new
            {
                DateList = listOfdata,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        public JsonResult GetDemoDicussioncrm(int Id)
        {
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                obj.Id = Id;
                listOfdata = objBLs.GetDemoDicussioncrm(obj);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            var returnValue = new
            {
                DateList = listOfdata,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult GetFollowupDicussionCRM(int Id)
        {
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                obj.Id = Id;
                listOfdata = objBLs.GetFollowupDicussionCRM(obj);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            var returnValue = new
            {
                DateList = listOfdata,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //------------------------------FOR lINK DYNAMIC ASSIGN COUNTRY TO SALES PERSON------------------------------------------
        public JsonResult GetUser()
        {
            string Message = "Success";
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfrecord = new List<enLeadManagementSubscription>();
            try
            {
                objENSearchFilter.ActionType = "GetUser";

                listOfrecord = new blLeadManagementSubscription(new enLeadManagementSubscription()).ReadUser(objENSearchFilter);
            }
            catch (Exception ex)
            {

            }

            var returnValue = new
            {
                DataList = listOfrecord,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public JsonResult SaveUserRelation(string Country, string UserEmail, string StateName)
        {
            string Message = "Success";
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> listOfrecord = new List<enLeadManagementSubscription>();
            try
            {
                objENSearchFilter.Email = UserEmail;
                objENSearchFilter.CountryName = Country;
                objENSearchFilter.StateName = StateName;

                listOfrecord = new blLeadManagementSubscription(new enLeadManagementSubscription()).SaveUserRelation(objENSearchFilter);
            }
            catch (Exception ex)
            {

            }

            var returnValue = new
            {
                DataList = listOfrecord,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public JsonResult SaveLeadContact(int Id, string Name, string Email, string Phone, string Designation, string EditID)
        {
            string Message = "";
            List<enLeadManagementSubscription> ListofRespone = new List<enLeadManagementSubscription>();
            try
            {
                string CreatedBy = Session["User"].ToString();
                ListofRespone = new blLeadManagementSubscription(new enLeadManagementSubscription()).SaveLeadContact(Id, Name, Email, Phone, Designation, CreatedBy, EditID);
                Message = ListofRespone[0].ValidationMessage;
            }
            catch (Exception ex)
            {
                Message = "error";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }



        public JsonResult getMoreContactDetails(int Id)
        {
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                obj.Id = Id;
                listOfdata = objBLs.getMoreContactDetails(obj);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            var returnValue = new
            {
                DateList = listOfdata,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetOpenAllMaskEmailListManager_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.EximPageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.EximPageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.UserName = Session["User"].ToString(); ;
                listOfdata = objBLs.GetAllOpenLeadListManager(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.EximPageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                PagerSize = (int)ApplicationSettings.EximPageSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.EximPageSize,
                //SearchFilterHasValue = isSearchFilterHasValue,
                SearchFilterHasValue = string.Empty,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult GetEmployeeMasterOpenLeadData()
        {
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);
            List<enLeadManagementSubscription> listOfUser = objB.ReadAllUserOpenLead();
            return Json(listOfUser, JsonRequestBehavior.AllowGet);

        }


        public JsonResult SaveEmployeeOpenLeadData(int ChildId, int ParentId)
        {
            string Message = "";
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            List<enLeadManagementSubscription> listOfUser = new List<enLeadManagementSubscription>();
            try
            {

                blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);
                listOfUser = objB.InsertEmployeeMasterOpenLeadDat(ChildId, ParentId);

            }
            catch (Exception ex)
            {

            }
            return Json(listOfUser, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetOpenAllMaskEmailLisTL_CustomSearch(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;
            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.EximPageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.EximPageSize + 1;


            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.UserId = Convert.ToInt32(Session["LoginUser"]);
                listOfdata = objBLs.GetAllOpenLeadListTL(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.EximPageSize);
                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var returnValue = new
            {
                DateList = listOfdata,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                PagerSize = (int)ApplicationSettings.EximPageSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.EximPageSize,
                //SearchFilterHasValue = isSearchFilterHasValue,
                SearchFilterHasValue = string.Empty,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult OrganizationalChartdata_Sales()
        {

            List<enLeadManagementSubscription> listOfAllEmployeeRecords = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBL = new blLeadManagementSubscription(new enLeadManagementSubscription());
            listOfAllEmployeeRecords = objBL.GetAllEmployeeRecordsChart();
            return Json(listOfAllEmployeeRecords, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindLeadStatusReport(enSearchFilter searchFilter)
        {

            enLeadManagementSubscription objs = new enLeadManagementSubscription();
            List<enLeadManagementSubscription> Listofdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {


                Listofdata = objBLs.GetLeadStatusReport(searchFilter);
            }
            catch (Exception ex)
            {

            }
            return Json(Listofdata, JsonRequestBehavior.AllowGet);
        }




        public JsonResult TransferLeadPreSales(string CorpEmail, string MailChain)
        {
            string Message = "";
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> Lead = new List<enLeadManagementSubscription>();
            try
            {

                Lead = new blLeadManagementSubscription(new enLeadManagementSubscription()).TransferLeadPreSales(CorpEmail, MailChain);

            }
            catch (Exception ex)
            {
                Lead[0].ValidationMessage = ex.Message;
            }
            foreach (var ResponseMessage in Lead)
            {
                Message = ResponseMessage.ValidationMessage;
            }
            return Json(Message, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult BindPreSalesLeadDetailsByid(string LeadId)
        {
            string Message = "";
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> ListofData = new List<enLeadManagementSubscription>();
            try
            {
                objENSearchFilter.Id = Convert.ToInt32(LeadId);

                ListofData = new blLeadManagementSubscription(new enLeadManagementSubscription()).BindPreLeadDetailsByid(objENSearchFilter);


            }
            catch (Exception ex)
            {

            }

            return Json(ListofData, JsonRequestBehavior.AllowGet);

        }




        public JsonResult UpdatePreSalesLeadDetails(Lead_Master GetDetails)
        {
            enLeadManagementSubscription enObj = new enLeadManagementSubscription();
            string Message = "";
            List<enLeadManagementSubscription> ListofRespone = new List<enLeadManagementSubscription>();
            try
            {
                enObj.Id = GetDetails.Id;
                enObj.Name = GetDetails.Name;
                enObj.CorpEmail = GetDetails.CorpEmail;
                enObj.Mobile = GetDetails.Mobile;
                enObj.CompanyName = GetDetails.Organisation;
                enObj.CountryName = GetDetails.Country;
                enObj.StateName = GetDetails.State;
                enObj.Designation = GetDetails.Designation;
                enObj.DiscriptionMessage = GetDetails.DiscriptionMessage;
                enObj.OutboundLeadSource = GetDetails.OutboundLeadSource;
                enObj.LeadType = GetDetails.LeadType;
                enObj.IPAddress = GetIPAddress();
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                enObj.Phone = GetDetails.Phone;
                enObj.createdby = Session["User"].ToString();
                enObj.RequestedDate = GetDetails.RequestedDate;
                enObj.ContactCountryCode = GetDetails.ContactCountryCode;
                enObj.TelephoneCountryCode = GetDetails.TelephoneCountryCode;


                ListofRespone = new blLeadManagementSubscription(new enLeadManagementSubscription()).UpdatePreSalesLeadRecord(enObj);

            }
            catch (Exception ex)
            {

            }

            return Json(ListofRespone, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetPreSalesEmployeeName()
        {
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);
            int UserId = Convert.ToInt32(Session["LoginUser"]);

            List<enLeadManagementSubscription> listOfUser = objB.SalesEmployeeName(UserId);
            return Json(listOfUser, JsonRequestBehavior.AllowGet);

        }


        public JsonResult SaveEmployeePreSalesLeadData(int ChildId, int ParentId)
        {
            string Message = "";
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            List<enLeadManagementSubscription> listOfUser = new List<enLeadManagementSubscription>();
            try
            {

                blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);
                listOfUser = objB.InsertEmployeeMasterPreSales(ChildId, ParentId);

            }
            catch (Exception ex)
            {

            }
            return Json(listOfUser, JsonRequestBehavior.AllowGet);

        }


        public ActionResult OrganizationalChartdata_PreSales()
        {

            List<enLeadManagementSubscription> listOfAllEmployeeRecords = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBL = new blLeadManagementSubscription(new enLeadManagementSubscription());
            listOfAllEmployeeRecords = objBL.GetAllEmployeeRecordsChartPreSales();
            return Json(listOfAllEmployeeRecords, JsonRequestBehavior.AllowGet);
        }



        public JsonResult BindLeadStatusPreSalesReport(enSearchFilter searchFilter)
        {

            enSearchFilter objs = new enSearchFilter();
            List<enLeadManagementSubscription> Listofdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {

                if (!string.IsNullOrEmpty(searchFilter.Name) && searchFilter.Name.StartsWith("["))
                {
                    var names = JsonConvert.DeserializeObject<List<string>>(searchFilter.Name);
                    objs.Name = string.Join(",", names);
                }
                else
                {
                    objs.Name = searchFilter.Name;
                }
                objs.FromDate = searchFilter.FromDate;
                objs.ToDate = searchFilter.ToDate;
                Listofdata = objBLs.GetLeadStatusReportPreSales(objs);
            }
            catch (Exception ex)
            {

            }
            return Json(Listofdata, JsonRequestBehavior.AllowGet);
        }



        public JsonResult PreSalesDemocountDetails(enSearchFilter searchFilter)
        {

            enSearchFilter objs = new enSearchFilter();
            List<enLeadManagementSubscription> Listofdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                objs.Name = searchFilter.Name;
                objs.FromDate = searchFilter.FromDate;
                objs.ToDate = searchFilter.ToDate;
                objs.MonthofMonth = searchFilter.MonthofMonth;
                objs.Year = searchFilter.Year;
                Listofdata = objBLs.PreSalesDemocountDetails(objs);
            }
            catch (Exception ex)
            {

            }
            return Json(Listofdata, JsonRequestBehavior.AllowGet);
        }


        public JsonResult PreSalesDemocountDetailsByCreatedDate(enSearchFilter searchFilter)
        {

            enSearchFilter objs = new enSearchFilter();
            List<enLeadManagementSubscription> Listofdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                objs.Name = searchFilter.Name;
                objs.FromDate = searchFilter.FromDate;
                objs.ToDate = searchFilter.ToDate;
                objs.MonthofMonth = searchFilter.MonthofMonth;
                objs.Year = searchFilter.Year;
                Listofdata = objBLs.PreSalesDemocountDetailsByCreatedDate(objs);
            }
            catch (Exception ex)
            {

            }
            return Json(Listofdata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PreSalesWonCountDetils(enSearchFilter searchFilter)
        {

            enSearchFilter objs = new enSearchFilter();
            List<enLeadManagementSubscription> Listofdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                objs.Name = searchFilter.Name;
                objs.FromDate = searchFilter.FromDate;
                objs.ToDate = searchFilter.ToDate;
                objs.MonthofMonth = searchFilter.MonthofMonth;
                objs.Year = searchFilter.Year;
                Listofdata = objBLs.PreSalesWonCountDetils(objs);
            }
            catch (Exception ex)
            {

            }
            return Json(Listofdata, JsonRequestBehavior.AllowGet);
        }


        public JsonResult PreSalesTransferCountDetils(enSearchFilter searchFilter)
        {

            enSearchFilter objs = new enSearchFilter();
            List<enLeadManagementSubscription> Listofdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                objs.Name = searchFilter.Name;
                objs.FromDate = searchFilter.FromDate;
                objs.ToDate = searchFilter.ToDate;
                objs.MonthofMonth = searchFilter.MonthofMonth;
                objs.Year = searchFilter.Year;
                Listofdata = objBLs.PreSalesTransferCountDetils(objs);
            }
            catch (Exception ex)
            {

            }
            return Json(Listofdata, JsonRequestBehavior.AllowGet);
        }



        public JsonResult SearchDomain(string Search)
        {
            string Message = "";
            enSearchFilter objENSearchFilter = new enSearchFilter();
            enLeadManagementSubscription obj = new enLeadManagementSubscription();
            blLeadManagementSubscription objB = new blLeadManagementSubscription(obj);

            List<enLeadManagementSubscription> Lead = new List<enLeadManagementSubscription>();
            try
            {
                objENSearchFilter.searchKeyword = Search;
                Lead = new blLeadManagementSubscription(new enLeadManagementSubscription()).SearchDomain(objENSearchFilter);
            }
            catch (Exception ex)
            {

            }
            foreach (var ResponseMessage in Lead)
            {
                Message = ResponseMessage.ValidationMessage;
            }
            var jsonResult = Json(Lead, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public JsonResult PreSalesUnusedCountDetils(enSearchFilter searchFilter)
        {

            enSearchFilter objs = new enSearchFilter();
            List<enLeadManagementSubscription> Listofdata = new List<enLeadManagementSubscription>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                objs.Name = searchFilter.Name;
                objs.FromDate = searchFilter.FromDate;
                objs.ToDate = searchFilter.ToDate;
                objs.MonthofMonth = searchFilter.MonthofMonth;
                objs.Year = searchFilter.Year;
                Listofdata = objBLs.PreSalesUnusedCountDetils(objs);
            }
            catch (Exception ex)
            {

            }
            return Json(Listofdata, JsonRequestBehavior.AllowGet);
        }



        //---------------------------------New Crm Module by ashish-----------------------------------



        public ActionResult TicketList()
        {
            return View();
        }
        public ActionResult Customers()
        {
            return View();
        }
        public ActionResult CustomerDetail(int id)
        {
            return View();
        }


        // GetUsersdata / Getcompanywiseleadstatus moved to
        // CrmDashboardWidgetsController (ReadOnly session, see that file for why).

        [HttpPost]
        public JsonResult GetCustomersDetailsByCompany(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword ?? string.Empty;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;

            // 'All' | 'Active' | 'Expired' | 'Inactive' | 'RenewalDue' — sent from the frontend
            // filter tabs; defaults to 'All' if nothing was passed.
            if (string.IsNullOrEmpty(searchFilter.FilterType))
            {
                searchFilter.FilterType = "All";
            }

            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enUsersDetailsbyCompany> listOfdata = new List<enUsersDetailsbyCompany>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.UserId = Convert.ToInt32(Session["LoginUser"]);
                listOfdata = objBLs.GetCustomersDetailsByCompany(searchFilter);
            }
            catch (Exception)
            {
                return Json(listOfdata, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfdata != null && listOfdata.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)RecordsCountValue / (double)ApplicationSettings.PageSize);

                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var shaped = (listOfdata ?? new List<enUsersDetailsbyCompany>()).Select(c => new
            {
                Id = c.Id,
                CompanyName = c.CompanyName,
                FirstContactEmail = c.FirstContactEmail,
                PrimaryContact = c.PrimaryContact,
                Status = c.Status,
                ModuleName = c.ModuleName,
                api = c.api,
                RenewsDate = c.RenewsDate.HasValue ? c.RenewsDate.Value.ToString("yyyy-MM-dd") : null,
                ApiStatus = c.ApiStatus,
                LastLogin = c.LastLogin.HasValue ? c.LastLogin.Value.ToString("dd MMM yyyy, HH:mm") : null,
                Health = c.Health
            }).ToList();

            var returnValue = new
            {
                DateList = shaped,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                PagerSize = (int)ApplicationSettings.PageSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.PageSize,
                SearchFilterHasValue = string.Empty,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        //[HttpPost]
        //public JsonResult GetCustomersDetailsByCompany(enSearchFilter searchFilter)
        //{
        //    string search = searchFilter.searchKeyword ?? string.Empty;
        //    search = search.Replace("\'", "");
        //    searchFilter.searchKeyword = search;

        //    searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
        //    searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;


        //    var listOfdata = new System.Collections.Generic.List<enUsersDetailsbyCompany>();
        //    var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

        //    try
        //    {
        //        searchFilter.UserId = Convert.ToInt32(Session["LoginUser"]);
        //        listOfdata = objBLs.GetCustomersDetailsByCompany(searchFilter);
        //    }
        //    catch (Exception)
        //    {
        //        return Json(listOfdata, JsonRequestBehavior.AllowGet);
        //    }

        //    int ShowingFromValue = 0;
        //    int ShowingToValue = 0;
        //    int RecordsCountValue = 0;
        //    int NumberOfPagesValue = 0;

        //    if (listOfdata != null && listOfdata.Count > 0)
        //    {
        //        ShowingFromValue = searchFilter.StartRowNumber;
        //        ShowingToValue = Convert.ToInt32(listOfdata.LastOrDefault().RowNumber);
        //        RecordsCountValue = Convert.ToInt32(listOfdata.LastOrDefault().RecordsCount);
        //        NumberOfPagesValue = (int)Math.Ceiling((double)RecordsCountValue / (double)ApplicationSettings.EximPageSize);

        //        if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
        //        {
        //            ShowingFromValue = 1;
        //        }
        //    }

        //    var returnValue = new
        //    {
        //        DateList = listOfdata,
        //        ShowingFrom = ShowingFromValue,
        //        ShowingTo = ShowingToValue,
        //        RecordsCount = RecordsCountValue,
        //        ActivePage = searchFilter.PageNumber,
        //        PagerSize = (int)ApplicationSettings.EximPageSize,
        //        NumberOfPages = NumberOfPagesValue,
        //        PageSize = ApplicationSettings.EximPageSize,
        //        SearchFilterHasValue = string.Empty,
        //    };

        //    var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}
        [HttpPost]
        public JsonResult GetCustomerDetailById(int id)
        {
            enSearchFilter enobj = new enSearchFilter();
            enCustomerDetailByCompany data = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                enobj.Id = id;

                List<enCustomerDetailByCompany> listOfData = objBLs.GetCustomerDetailById(enobj);
                data = listOfData != null ? listOfData.FirstOrDefault() : null;
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var jsonResult = Json(data, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult SaveReminder(enReminder reminder)
        {
            enReminder savedReminder = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                reminder.CreatedBy = Convert.ToInt32(Session["LoginUser"]);
                List<enReminder> listOfData = objBLs.InsertReminder(reminder);
                savedReminder = listOfData != null ? listOfData.FirstOrDefault() : null;
            }
            catch (System.Exception)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, reminder = savedReminder }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetRemindersByCompany(int companyId)
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enReminder> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                enobj.Id = companyId;
                listOfData = objBLs.GetRemindersByCompany(enobj);
            }
            catch (System.Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var shaped = (listOfData ?? new List<enReminder>()).Select(r => new
            {
                Id = r.Id,
                CompanyId = r.CompanyId,
                ReminderType = r.ReminderType,
                Note = r.Note,
                ReminderDate = r.ReminderDate.ToString("yyyy-MM-dd"),
                ReminderTime = r.ReminderTime.HasValue ? r.ReminderTime.Value.ToString(@"hh\:mm") : null,
                ShowOnMyDay = r.ShowOnMyDay,
                IsCompleted = r.IsCompleted
            }).ToList();

            var jsonResult = Json(shaped, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult GetContactsByCompany(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword ?? string.Empty;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;

            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enUserContact> listOfData = new List<enUserContact>();
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.CrmId = Convert.ToInt32(Session["LoginUser"]);
                listOfData = objBLs.GetContactsByCompany(searchFilter);
            }
            catch (Exception)
            {
                return Json(new { DateList = listOfData }, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfData != null && listOfData.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfData.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfData.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)RecordsCountValue / (double)ApplicationSettings.PageSize);

                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            // shape dates as clean strings to avoid the /Date(...)/ serialization issue
            var shaped = (listOfData ?? new List<enUserContact>()).Select(c => new
            {
                Id = c.Id,
                Fname = c.Fname,
                Lname = c.Lname,
                Email = c.Email,
                Role = c.Role,
                Status = c.Status,
                LastLogin = c.LastLogin.HasValue ? c.LastLogin.Value.ToString("dd MMM yyyy, HH:mm") : null
            }).ToList();

            var returnValue = new
            {
                DateList = shaped,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.PageSize
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

            [HttpPost]
            public JsonResult GetSubscriptionsByCompany(enSearchFilter searchFilter)
            {
                string search = searchFilter.searchKeyword ?? string.Empty;
                search = search.Replace("\'", "");
                searchFilter.searchKeyword = search;

                searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
                searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

                List<enSubscriptionItem> listOfData = new List<enSubscriptionItem>();
                var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

                try
                {
                    searchFilter.CrmId = Convert.ToInt32(Session["LoginUser"]);
                    listOfData = objBLs.GetSubscriptionsByCompany(searchFilter);
                }
                catch (Exception)
                {
                    return Json(new { DateList = listOfData }, JsonRequestBehavior.AllowGet);
                }

                int ShowingFromValue = 0;
                int ShowingToValue = 0;
                int RecordsCountValue = 0;
                int NumberOfPagesValue = 0;

                if (listOfData != null && listOfData.Count > 0)
                {
                    ShowingFromValue = searchFilter.StartRowNumber;
                    ShowingToValue = Convert.ToInt32(listOfData.LastOrDefault().RowNumber);
                    RecordsCountValue = Convert.ToInt32(listOfData.LastOrDefault().RecordsCount);
                    NumberOfPagesValue = (int)Math.Ceiling((double)RecordsCountValue / (double)ApplicationSettings.PageSize);

                    if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                    {
                        ShowingFromValue = 1;
                    }
                }

                var returnValue = new
                {
                    DateList = listOfData,
                    ShowingFrom = ShowingFromValue,
                    ShowingTo = ShowingToValue,
                    RecordsCount = RecordsCountValue,
                    ActivePage = searchFilter.PageNumber,
                    NumberOfPages = NumberOfPagesValue,
                    PageSize = ApplicationSettings.PageSize
                };

                var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }



        [HttpPost]
        public JsonResult GetRecentLoginsByCompany(int companyId)
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enLoginRecord> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                enobj.Id = companyId;
                listOfData = objBLs.GetRecentLoginsByCompany(enobj);
            }
            catch (Exception)
            {
                return Json(new List<object>(), JsonRequestBehavior.AllowGet);
            }

            var shaped = (listOfData ?? new List<enLoginRecord>()).Select(r => new
            {
                UserName = r.UserName,
                CreatedTime = r.CreatedTime.ToString("dd MMM yyyy, HH:mm"),
                DeviceType = r.DeviceType
            }).ToList();

            return Json(shaped, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLoginUsageSummaryByCompany(int companyId)
        {
            enSearchFilter enobj = new enSearchFilter();
            enUsageSummary data = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                enobj.Id = companyId;
                List<enUsageSummary> listOfData = objBLs.GetLoginUsageSummaryByCompany(enobj);
                data = listOfData != null ? listOfData.FirstOrDefault() : null;
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLoginsByMonthByCompany(int companyId)
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enMonthlyLoginCount> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                enobj.Id = companyId;
                listOfData = objBLs.GetLoginsByMonthByCompany(enobj);
            }
            catch (Exception)
            {
                return Json(new List<enMonthlyLoginCount>(), JsonRequestBehavior.AllowGet);
            }
            return Json(listOfData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTopPricingProductsByCompany(int companyId)
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enProductViewCount> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                enobj.Id = companyId;
                listOfData = objBLs.GetTopPricingProductsByCompany(enobj);
            }
            catch (Exception)
            {
                return Json(new List<enProductViewCount>(), JsonRequestBehavior.AllowGet);
            }
            return Json(listOfData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTopDisruptionProductsByCompany(int companyId)
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enProductViewCount> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                enobj.Id = companyId;
                listOfData = objBLs.GetTopDisruptionProductsByCompany(enobj);
            }
            catch (Exception)
            {
                return Json(new List<enProductViewCount>(), JsonRequestBehavior.AllowGet);
            }
            return Json(listOfData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTopMarketAnalysisProductsByCompany(int companyId)
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enProductViewCount> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                enobj.Id = companyId;
                listOfData = objBLs.GetTopMarketAnalysisProductsByCompany(enobj);
            }
            catch (Exception)
            {
                return Json(new List<enProductViewCount>(), JsonRequestBehavior.AllowGet);
            }
            return Json(listOfData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTopCombinedProductsByCompany(int companyId)
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enProductViewCount> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                enobj.Id = companyId;
                listOfData = objBLs.GetTopCombinedProductsByCompany(enobj);
            }
            catch (Exception)
            {
                return Json(new List<enProductViewCount>(), JsonRequestBehavior.AllowGet);
            }
            return Json(listOfData, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetLoginHistoryByCompany(enSearchFilter searchFilter)
        {
            string loginSearch = searchFilter.searchKeyword ?? string.Empty;
            loginSearch = loginSearch.Replace("\'", "");
            searchFilter.searchKeyword = loginSearch;

            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enLoginRecord> loginData = new List<enLoginRecord>();
            var objBLLogin = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                loginData = objBLLogin.GetLoginHistoryByCompany(searchFilter);
            }
            catch (Exception)
            {
                loginData = new List<enLoginRecord>();
            }

            int loginRecordsCount = 0;
            int loginNumberOfPages = 0;
            if (loginData != null && loginData.Count > 0)
            {
                loginRecordsCount = Convert.ToInt32(loginData.LastOrDefault().RecordsCount);
                loginNumberOfPages = (int)Math.Ceiling((double)loginRecordsCount / (double)ApplicationSettings.PageSize);
            }

            var loginShaped = (loginData ?? new List<enLoginRecord>()).Select(r => new
            {
                UserName = r.UserName,
                Login = r.Login.HasValue ? r.Login.Value.ToString("dd MMM yyyy, HH:mm") : null,
                DeviceType = r.DeviceType
            }).ToList();

            var loginReturn = new
            {
                DateList = loginShaped,
                RecordsCount = loginRecordsCount,
                ActivePage = searchFilter.PageNumber,
                NumberOfPages = loginNumberOfPages,
                PageSize = ApplicationSettings.PageSize
            };
            var loginJson = Json(loginReturn, JsonRequestBehavior.AllowGet);
            loginJson.MaxJsonLength = int.MaxValue;
            return loginJson;
        }

        public JsonResult GetTicketsByCompany(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword ?? string.Empty;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;

            searchFilter.EndRowNumber = searchFilter.PageNumber * ApplicationSettings.PageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enSupportTicket> listOfData = new List<enSupportTicket>();
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.CrmId = Convert.ToInt32(Session["LoginUser"]);
                listOfData = objBLs.GetTicketsByCompany(searchFilter);
            }
            catch (Exception ex)
            {
                // TEMPORARY — remove message exposure once confirmed working
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfData != null && listOfData.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfData.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfData.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)RecordsCountValue / (double)ApplicationSettings.PageSize);

                if (ShowingFromValue >= RecordsCountValue && searchFilter.searchKeyword != "")
                {
                    ShowingFromValue = 1;
                }
            }

            var shaped = (listOfData ?? new List<enSupportTicket>()).Select(t => new
            {
                ID = t.ID,
                Status = t.Status,
                CreatedDate = t.CreatedDate.ToString("dd MMM yyyy"),
                UpdatedDate = t.UpdatedDate.HasValue ? t.UpdatedDate.Value.ToString("dd MMM yyyy") : null,
                Rating = t.Rating,
                RoleName=t.RoleName,
                Subject = string.IsNullOrEmpty(t.Subject) ? "(no message yet)" : t.Subject
            }).ToList();

            var returnValue = new
            {
                DateList = shaped,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.PageSize
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // searchFilter.Id = TicketId
        [HttpPost]
        public JsonResult GetTicketHistoryById(int ticketId)
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enSupportTicketComment> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                enobj.Id = ticketId;
                listOfData = objBLs.GetTicketHistoryById(enobj);
            }
            catch (Exception)
            {
                return Json(new List<object>(), JsonRequestBehavior.AllowGet);
            }

            var shaped = (listOfData ?? new List<enSupportTicketComment>()).Select(c => new
            {
                ID = c.ID,
                UserFID = c.UserFID,
                ManagerFID = c.ManagerFID,
                Comment = c.Comment,
                FileName = c.FileName,
                CreatedDate = c.CreatedDate.ToString("dd MMM yyyy, HH:mm"),
                // heuristic: a non-null/non-zero ManagerFID means a support/manager reply
                IsManagerReply = c.ManagerFID.HasValue && c.ManagerFID.Value > 0
            }).ToList();

            return Json(shaped, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetProformaInvoicesByCompanyName(string companyName)
        {
            List<enProformaInvoiceSummary> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                listOfData = objBLs.GetProformaInvoicesByCompanyName(companyName);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            var shaped = (listOfData ?? new List<enProformaInvoiceSummary>()).Select(i => new
            {
                ID = i.ID,
                InvoiceNo = i.InvoiceNo,
                CreatedDate = i.CreatedDate.ToString("dd MMM yyyy"),
                TotalAmount = i.TotalAmount,
                Currency = i.Currency
            }).ToList();

            return Json(shaped, JsonRequestBehavior.AllowGet);
        }

        // GetTodaysReminders / MarkReminderComplete moved to
        // CrmDashboardWidgetsController (ReadOnly session, see that file for why).

        [HttpPost]
        public JsonResult GetDeliverablesByCompany(enSearchFilter searchFilter)
        {
            const int pageSize = 10;

            searchFilter.EndRowNumber = searchFilter.PageNumber * pageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - pageSize + 1;

            List<enWorkSpaceFile> listOfData = new List<enWorkSpaceFile>();
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.CrmId = Convert.ToInt32(Session["LoginUser"]);
                listOfData = objBLs.GetDeliverablesByCompany(searchFilter);
            }
            catch (Exception ex)
            {
                // TEMPORARY — remove message exposure once confirmed working
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfData != null && listOfData.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfData.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfData.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)RecordsCountValue / (double)pageSize);
            }

            var shaped = (listOfData ?? new List<enWorkSpaceFile>()).Select(f => new
            {
                ID = f.ID,
                Email = f.Email,
                Name = f.Name,
                FileName = f.FileName,
                CreatedDate = f.CreatedDate.ToString("dd MMM yyyy"),
                CreatedBy = f.CreatedBy
            }).ToList();

            var returnValue = new
            {
                DateList = shaped,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                NumberOfPages = NumberOfPagesValue,
                PageSize = pageSize
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult GetRecommendationSignalsByCompany(int companyId)
        {
            enSearchFilter enobj = new enSearchFilter();
            enRecommendationSignals data = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                enobj.Id = companyId;
                List<enRecommendationSignals> listOfData = objBLs.GetRecommendationSignalsByCompany(enobj);
                data = listOfData != null ? listOfData.FirstOrDefault() : null;
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // GetNeedsAttentionToday / GetRenewalsThisQuarter / GetCustomersByCountry /
        // GetCustomersByIndustry / GetCustomersAddedVsLostByMonth /
        // GetUsersAddedVsLostByMonth moved to CrmDashboardWidgetsController
        // (ReadOnly session, see that file for why).
        const int AllTicketsPageSize = 10;

        // searchFilter.FilterType is reused here for the status tab: 'Open' | 'InProgress' | 'Resolved' | 'All'
        [HttpPost]
        public JsonResult GetAllTicketsByCRM(enSearchFilter searchFilter)
        {
            string search = searchFilter.searchKeyword ?? string.Empty;
            search = search.Replace("\'", "");
            searchFilter.searchKeyword = search;

            if (string.IsNullOrEmpty(searchFilter.FilterType))
                searchFilter.FilterType = "All";

            searchFilter.EndRowNumber = searchFilter.PageNumber * AllTicketsPageSize;
            searchFilter.StartRowNumber = searchFilter.EndRowNumber - AllTicketsPageSize + 1;

            List<enAllTicket> listOfData = new List<enAllTicket>();
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                searchFilter.CrmId = Convert.ToInt32(Session["LoginUser"]);
                listOfData = objBLs.GetAllTicketsByCRM(searchFilter);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0, ShowingToValue = 0, RecordsCountValue = 0, NumberOfPagesValue = 0;
            if (listOfData != null && listOfData.Count > 0)
            {
                ShowingFromValue = searchFilter.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfData.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfData.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)RecordsCountValue / (double)AllTicketsPageSize);
            }

            var shaped = (listOfData ?? new List<enAllTicket>()).Select(t => new
            {
                ID = t.ID,
                CompanyName = t.CompanyName,
                ContactName = t.ContactName,
                Email = t.Email,
                Status = t.Status,
                CreatedDate = t.CreatedDate.ToString("yyyy-MM-dd HH:mm"),
                RoleName = t.RoleName,
                UpdatedDate = t.UpdatedDate.HasValue ? t.UpdatedDate.Value.ToString("yyyy-MM-dd HH:mm") : null,
                Subject = string.IsNullOrEmpty(t.Subject) ? "(no message yet)" : t.Subject
            }).ToList();

            var returnValue = new
            {
                DateList = shaped,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = searchFilter.PageNumber,
                NumberOfPages = NumberOfPagesValue,
                PageSize = AllTicketsPageSize
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult InsertrecordinSAcustomer(string Name, string Email, string Phone, string ClientCompanyId, int chkboxPrimary, string Designation)
        {
            enLeadManagementSubscription enObj = new enLeadManagementSubscription();
            string Message = "";

            try
            {
                enObj.Name = Name;
                enObj.CorpEmail = Email;
                enObj.Phone = Phone;
                enObj.CompanyId = Convert.ToInt32(ClientCompanyId);
                enObj.chkboxPrimaryId = chkboxPrimary;
                enObj.Designation = Designation;
                string strPassword = "Techsci44@Re";
                string strEncrypted = (Helpers.Encrypt(strPassword));
                string strDecrypted = (Helpers.Decrypt(strEncrypted));
                enObj.Password = strEncrypted;
                if (enObj.CorpEmail != "")
                {
                    List<enLeadManagementSubscription> ListofRespone = new List<enLeadManagementSubscription>();
                    ListofRespone = new blLeadManagementSubscription(new enLeadManagementSubscription()).InsertrecordinSAcustomer(enObj);
                    foreach (var ResponseMessage in ListofRespone)
                    {
                        Message = ResponseMessage.ValidationMessage;
                    }
                }
                else
                {

                }

            }
            catch (Exception ex)
            {

            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        /* ----------------------------------------------------------------
           Invoicing setup (Billing & invoices tab on CustomerDetail.cshtml)
           Follows this project's en/bl/dal convention (see DAL/LeadDAL.cs):
           entity = EntityLayer.InvoicingSetup, BL = BusinessLayer.InvoicingSetupBL,
           DAL = DataLayer.InvoicingSetupDAL, which calls stored procs
           USP_GetInvoicingSetupByCompany / USP_SaveInvoicingSetup
           (see /database/SA_Invoicingsetup.sql) instead of inline SQL.
           ---------------------------------------------------------------- */
        [HttpPost]
        public JsonResult GetInvoicingSetupByCompany(int companyId)
        {
            try
            {
                var bl = new BusinessLayer.InvoicingSetupBL();
                var model = bl.GetInvoicingSetupByCompany(companyId);

                if (model == null)
                {
                    return Json(new { found = false }, JsonRequestBehavior.AllowGet);
                }

                var data = new
                {
                    found = true,
                    model.Id,
                    model.CompanyId,
                    model.HowToSendInvoice,
                    model.InvoiceEmail,
                    model.VendorRegistrationNo,
                    model.PORequiredBeforeInvoice,
                    model.UploadPortal,
                    model.HardCopyNeeded,
                    model.HardCopyAddress,
                    model.SpecialInstructions,
                    model.CreatedBy,
                    CreatedDate = model.CreatedDate.HasValue ? model.CreatedDate.Value.ToString("dd MMM yyyy") : "",
                    model.UpdatedBy,
                    UpdatedDate = model.UpdatedDate.HasValue ? model.UpdatedDate.Value.ToString("dd MMM yyyy") : ""
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveInvoicingSetup(int CompanyId, string HowToSendInvoice, string InvoiceEmail,
            string VendorRegistrationNo, bool PORequiredBeforeInvoice, string UploadPortal, bool HardCopyNeeded,
            string HardCopyAddress, string SpecialInstructions)
        {
            try
            {
                string userName = Session["User"] != null ? Session["User"].ToString() : "";

                var model = new EntityLayer.InvoicingSetup
                {
                    CompanyId = CompanyId,
                    HowToSendInvoice = HowToSendInvoice,
                    InvoiceEmail = InvoiceEmail,
                    VendorRegistrationNo = VendorRegistrationNo,
                    PORequiredBeforeInvoice = PORequiredBeforeInvoice,
                    UploadPortal = UploadPortal,
                    HardCopyNeeded = HardCopyNeeded,
                    HardCopyAddress = HardCopyAddress,
                    SpecialInstructions = SpecialInstructions
                };

                var bl = new BusinessLayer.InvoicingSetupBL();
                bl.SaveInvoicingSetup(model, userName);

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /* ----------------------------------------------------------------
           Documents tab on CustomerDetail.cshtml.
           Same en/bl/dal + stored-proc convention as invoicing setup above:
           entity = EntityLayer.CustomerDocument, BL = BusinessLayer.CustomerDocumentBL,
           DAL = DataLayer.CustomerDocumentDAL, calling USP_GetCustomerDocumentsByCompany /
           USP_InsertCustomerDocument (see /database/SA_CustomerDocuments.sql).
           Uploaded files themselves are saved on disk under ~/CustomerDocs/{CompanyId}/.
           ---------------------------------------------------------------- */
        [HttpPost]
        public JsonResult GetCustomerDocumentsByCompany(int companyId)
        {
            try
            {
                var bl = new BusinessLayer.CustomerDocumentBL();
                var list = bl.GetCustomerDocumentsByCompany(companyId);

                var shaped = list.Select(d => new
                {
                    d.Id,
                    d.CompanyId,
                    d.DocumentType,
                    d.ReferenceNote,
                    d.FileName,
                    UploadedDate = d.UploadedDate.HasValue ? d.UploadedDate.Value.ToString("dd MMM yyyy") : "",
                    d.UploadedBy
                }).ToList();

                return Json(new { success = true, DataList = shaped }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UploadCustomerDocument(int CompanyId, string DocumentType, string ReferenceNote, HttpPostedFileBase File)
        {
            try
            {
                if (File == null || File.ContentLength == 0)
                {
                    return Json(new { success = false, message = "Please choose a file to upload." });
                }

                string userName = Session["User"] != null ? Session["User"].ToString() : "";

                string originalFileName = Path.GetFileName(File.FileName);

                string folderPath = Server.MapPath("~/CustomerDocs/" + CompanyId + "/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // saved under its own file name — a re-upload of the same name overwrites it
                File.SaveAs(Path.Combine(folderPath, originalFileName));

                var model = new EntityLayer.CustomerDocument
                {
                    CompanyId = CompanyId,
                    DocumentType = DocumentType,
                    ReferenceNote = ReferenceNote,
                    FileName = originalFileName,
                    UploadedBy = userName
                };

                var bl = new BusinessLayer.CustomerDocumentBL();
                bl.InsertCustomerDocument(model);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult DownloadCustomerDocument(int companyId, string fileName)
        {
            try
            {
                string fullPath = Server.MapPath("~/CustomerDocs/" + companyId + "/" + fileName);
                if (!System.IO.File.Exists(fullPath))
                {
                    return HttpNotFound();
                }

                string contentType = MimeMapping.GetMimeMapping(fullPath);
                return File(fullPath, contentType, fileName);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        /* ----------------------------------------------------------------
           History tab summary cards on CustomerDetail.cshtml (Customer since /
           Renewals / Lifetime value). Same en/bl/dal + stored-proc convention:
           entity = EntityLayer.CustomerHistorySummary, BL = BusinessLayer.CustomerHistoryBL,
           DAL = DataLayer.CustomerHistoryDAL, calling USP_GetCustomerHistorySummary
           (see /database/SA_CustomerHistory.sql). LifetimeValue has no source
           yet and always comes back null.
           ---------------------------------------------------------------- */
        [HttpPost]
        public JsonResult GetCustomerHistorySummary(int companyId)
        {
            try
            {
                var bl = new BusinessLayer.CustomerHistoryBL();
                var summary = bl.GetCustomerHistorySummary(companyId);

                var data = new
                {
                    success = true,
                    CustomerSince = summary.CustomerSince.HasValue ? summary.CustomerSince.Value.ToString("yyyy-MM-dd") : "",
                    summary.Renewals,
                    LifetimeValue = summary.LifetimeValue
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /* ----------------------------------------------------------------
           Subscription history table on the History tab (CustomerDetail.cshtml).
           See /database/SA_SubscriptionHistory.sql (USP_GetSubscriptionHistoryByCompany).
           Grouped by ToDate, with Users (distinct LeadId count) and a
           computed Status (ToDate in the future = Active, else Renewed).
           AmountPaid has no source at all — not included; UI always shows "—".
           ---------------------------------------------------------------- */
        [HttpPost]
        public JsonResult GetSubscriptionHistoryByCompany(int companyId)
        {
            try
            {
                var bl = new BusinessLayer.CustomerHistoryBL();
                var list = bl.GetSubscriptionHistoryByCompany(companyId);

                var shaped = list.Select(h => new
                {
                    Term = h.ToDate.HasValue ? h.ToDate.Value.ToString("MMM yyyy") : "?",
                    h.ProductsSubscribed,
                    h.Users,
                    h.Status
                }).ToList();

                return Json(new { success = true, DataList = shaped }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /* ----------------------------------------------------------------
           Quarterly feedback email / Next feedback email / Last feedback
           panel (Overview tab, CustomerDetail.cshtml). Self-contained —
           no dependency on any renewal table/proc.
           Same en/bl/dal + stored-proc convention as elsewhere in this
           controller: entity = EntityLayer.CompanyFeedbackSettings /
           EntityLayer.CompanyFeedbackHistory,
           BL/DAL = BusinessLayer.FeedbackSettingsBL / DataLayer.FeedbackSettingsDAL.
           See /database/SA_CompanyFeedbackSettings.sql.
           ---------------------------------------------------------------- */
        [HttpPost]
        public JsonResult GetCompanyFeedbackSettings(int companyId)
        {
            try
            {
                var bl = new BusinessLayer.FeedbackSettingsBL();
                var settings = bl.GetCompanyFeedbackSettings(companyId);

                var data = new
                {
                    success = true,
                    QuarterlyFeedbackEnabled = settings != null && settings.QuarterlyFeedbackEnabled,
                    NextFeedbackEmail = settings != null && settings.NextFeedbackEmail.HasValue ? settings.NextFeedbackEmail.Value.ToString("dd MMM yyyy") : "",
                    LastFeedbackDate = settings != null && settings.LastFeedbackDate.HasValue ? settings.LastFeedbackDate.Value.ToString("dd MMM yyyy") : "",
                    LastFeedbackRating = settings != null ? settings.LastFeedbackRating : null,
                    Notes = settings != null ? settings.LastFeedbackNotes : ""
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveCompanyFeedbackEntry(int companyId, bool enabled, string nextFeedbackEmail, string lastFeedbackDate, int? lastFeedbackRating, string notes)
        {
            try
            {
                string userName = Session["User"] != null ? Session["User"].ToString() : "";
                DateTime? nextDt = string.IsNullOrWhiteSpace(nextFeedbackEmail) ? (DateTime?)null : DateTime.Parse(nextFeedbackEmail);
                DateTime? lastDt = string.IsNullOrWhiteSpace(lastFeedbackDate) ? (DateTime?)null : DateTime.Parse(lastFeedbackDate);

                var bl = new BusinessLayer.FeedbackSettingsBL();
                bl.SaveCompanyFeedbackEntry(companyId, enabled, nextDt, lastDt, lastFeedbackRating, notes, userName);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ToggleQuarterlyFeedback(int companyId, bool enabled)
        {
            try
            {
                string userName = Session["User"] != null ? Session["User"].ToString() : "";
                var bl = new BusinessLayer.FeedbackSettingsBL();
                bl.ToggleQuarterlyFeedback(companyId, enabled, userName);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetCompanyFeedbackHistory(int companyId)
        {
            try
            {
                var bl = new BusinessLayer.FeedbackSettingsBL();
                var list = bl.GetCompanyFeedbackHistory(companyId);

                var shaped = (list ?? new List<EntityLayer.CompanyFeedbackHistory>()).Select(h => new
                {
                    FeedbackDate = h.FeedbackDate.HasValue ? h.FeedbackDate.Value.ToString("dd MMM yyyy") : "—",
                    h.Rating,
                    h.Notes,
                    h.CreatedBy,
                    CreatedDate = h.CreatedDate.HasValue ? h.CreatedDate.Value.ToString("dd MMM yyyy") : ""
                }).ToList();

                return Json(new { success = true, DataList = shaped }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /* ----------------------------------------------------------------
           Expiry notifications toggle (Overview tab panel, CustomerDetail.cshtml).
           Self-contained — reuses the existing SalesPackageSubscriptions
           .ExpiryNotification column, no new table.
           BL/DAL = BusinessLayer.ExpiryNotificationBL / DataLayer.ExpiryNotificationDAL.
           See /database/SA_ExpiryNotification.sql.
           ---------------------------------------------------------------- */
        [HttpPost]
        public JsonResult GetExpiryNotificationStatus(int companyId)
        {
            try
            {
                var bl = new BusinessLayer.ExpiryNotificationBL();
                bool isEnabled = bl.GetExpiryNotificationStatus(companyId);
                return Json(new { success = true, isEnabled }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateExpiryNotification(int companyId, bool isEnabled)
        {
            try
            {
                var bl = new BusinessLayer.ExpiryNotificationBL();
                bl.UpdateExpiryNotification(companyId, isEnabled);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        // One pipeline (New / Open / In discussion / Won - onboarding) in
        // place of the separate GetCRMOpenLeadList/CRM_OpenLeadList/
        // CRM_RenewLeadList menu items - filterable by owner and search.
        public ActionResult Pipeline()
        {
            return View();
        }

        public JsonResult GetCRMPipelineData(string search)
        {
            List<enLeadManagementSubscription> list = new List<enLeadManagementSubscription>();
            try
            {
                int crmId = Convert.ToInt32(Session["LoginUser"]);
                blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
                list = objBLs.GetCRMPipeline(crmId);
            }
            catch (Exception)
            {
                // fall through with empty list
            }

            return Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
        }

        // "Open" button on CustomerDetail (from Pipeline "Open For Renewal") -
        // inserts the company's renewal into SA_MaskEmailOpen_CRM.
        // In-discussion lead detail page (SA_MaskEmailOpen_CRM row), opened
        // from the Pipeline "In discussion" column.
        public ActionResult CRMLeadDetail(int id)
        {
            ViewBag.LeadId = id;
            return View();
        }

        public JsonResult GetCRMOpenLeadData(int id)
        {
            enLeadManagementSubscription lead = null;
            try
            {
                blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
                lead = objBLs.GetCRMOpenLeadById(id);
            }
            catch (Exception)
            {
                // fall through with null
            }

            return Json(new { DataItem = lead }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCRMFollowUps(int id)
        {
            List<enCRMFollowUp> list = new List<enCRMFollowUp>();
            try
            {
                blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
                list = objBLs.GetCRMFollowUps(id);
            }
            catch (Exception)
            {
                // fall through with empty list
            }

            return Json(new { DataItem = list }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCRMContacts(string companyName)
        {
            List<enCRMContact> list = new List<enCRMContact>();
            try
            {
                blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
                list = objBLs.GetCRMContacts(companyName);
            }
            catch (Exception)
            {
                // fall through with empty list
            }

            return Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCRMContact(int id, string name, string corpEmail, string contactCountryCode, string contactNumber, string designation)
        {
            bool success = false;
            string message = "";
            try
            {
                string createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                // Reuse the same Sales add-contact proc (usp_SalesNew_AddLeadContact).
                BusinessLayer.blSalesNew objSales = new BusinessLayer.blSalesNew();
                success = objSales.AddLeadContact(id, name, corpEmail, contactCountryCode, contactNumber, designation, createdBy, out message);
            }
            catch (Exception)
            {
                message = "Could not add this contact - please try again.";
            }
            return Json(new { Success = success, Message = message });
        }

        [HttpPost]
        public JsonResult MarkCRMWon(int id, decimal? amount, string currency, decimal? amountINR, string paymentOption, string paymentTerms, string invoiceNumber, string OrderFrom)
        {
            bool success = false;
            string message = "";
            try
            {
                blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
                success = objBLs.MarkCRMWon(id, amount, currency, amountINR, paymentOption, paymentTerms, invoiceNumber, OrderFrom);
                if (!success) message = "No lead was updated.";
            }
            catch (Exception)
            {
                message = "Could not mark this lead won - please try again.";
            }
            return Json(new { Success = success, Message = message });
        }

        [HttpPost]
        public JsonResult AddCRMFollowUp(int openId, string discussion, string discussionStatus, string nextFollowUp)
        {
            bool success = false;
            try
            {
                enCRMFollowUp f = new enCRMFollowUp();
                f.OpenId = openId;
                f.Discussion = discussion;
                f.DiscussionStatus = discussionStatus;
                DateTime dt;
                if (!string.IsNullOrEmpty(nextFollowUp) && DateTime.TryParse(nextFollowUp, out dt))
                    f.SetFollowUp = dt;
                f.CreatedBy = Session["User"] != null ? Session["User"].ToString() : null;

                blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
                objBLs.AddCRMFollowUp(f);
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            return Json(new { Success = success });
        }

        [HttpPost]
        public JsonResult UpdateCRMOpenLead(enLeadManagementSubscription lead)
        {
            bool success = false;
            try
            {
                blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
                objBLs.UpdateCRMOpenLead(lead);
                success = true;
            }
            catch (Exception)
            {
                // fall through with success = false
            }

            return Json(new { Success = success });
        }

        [HttpPost]
        public JsonResult OpenRenewalCompany(int companyId)
        {
            bool success = false;
            try
            {
                int crmId = Convert.ToInt32(Session["LoginUser"]);
                string createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
                objBLs.OpenRenewalCompany(companyId, crmId, createdBy);
                success = true;
            }
            catch (Exception)
            {
                // fall through with success = false
            }

            return Json(new { Success = success });
        }
    }
}
