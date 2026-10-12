using BusinessLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChemAnalyst.Models;
using System.IO;

namespace ChemAnalyst.Controllers
{
    public class SalesNewController : Controller
    {
        private readonly blSalesNew _bl = new blSalesNew();

        // GET: SalesNew
        public ActionResult Index()
        {
            return View();
        }

        // GET: SalesNew/Dashboard — the "My Day" page. Every widget here is
        // scoped to the logged-in sales rep (SA_MaskEmail_Open.CreatedBy),
        // same pattern as PreSalesNewController's Dashboard action.
        public ActionResult Dashboard()
        {
            return View();
        }

        // The 6 top KPI cards (Open leads / Pipeline value / Closing this
        // month / Overdue follow-ups / Win rate), computed entirely in SQL.
        public JsonResult GetKpiSummary()
        {
            var summary = new enSalesKpiSummary();
            try
            {
                var createdBy = Session["LoginUser"] != null ? Session["LoginUser"].ToString() : "";
                summary = _bl.GetKpiSummary(createdBy);
            }
            catch (Exception)
            {
                // fall through with a zeroed summary
            }

            var jsonResult = Json(new { DataItem = summary }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "Follow-ups due today" table — server-side search + paging (5/page).
        public JsonResult GetFollowupsDueToday(string search, int pageNumber = 1, int pageSize = 5)
        {
            var list = new List<enSalesFollowupDueRow>();
            int totalCount = 0;
            try
            {
                var createdBy = Session["LoginUser"] != null ? Session["LoginUser"].ToString() : "";
                list = _bl.GetFollowupsDueToday(createdBy, search, pageNumber, pageSize, out totalCount);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "Demos in progress" list — server-side search + paging (5/page).
        public JsonResult GetDemosInProgress(string search, int pageNumber = 1, int pageSize = 5)
        {
            var list = new List<enSalesDemoInProgressRow>();
            int totalCount = 0;
            try
            {
                var createdBy = Session["LoginUser"] != null ? Session["LoginUser"].ToString() : "";
                list = _bl.GetDemosInProgress(createdBy, search, pageNumber, pageSize, out totalCount);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "Pipeline by stage" bar chart.
        public JsonResult GetPipelineByStage()
        {
            var list = new List<enSalesPipelineStageCount>();
            try
            {
                var createdBy = Session["LoginUser"] != null ? Session["LoginUser"].ToString() : "";
                list = _bl.GetPipelineByStage(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "Pipeline value & forecast" chart -- Committed ($K) bars only,
        // current + previous calendar year.
        public JsonResult GetPipelineValueForecast()
        {
            var list = new List<enSalesPipelineForecastRow>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetPipelineValueForecast(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "Won vs lost" chart.
        public JsonResult GetWonVsLost(DateTime? fromDate = null, DateTime? toDate = null)
        {
            var list = new List<enSalesWonLostRow>();
            try
            {
                var createdBy = Session["LoginUser"] != null ? Session["LoginUser"].ToString() : "";
                list = _bl.GetWonVsLost(createdBy, fromDate, toDate);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "Leads by source" donut.
        public JsonResult GetLeadsBySource()
        {
            var list = new List<enSalesLeadSourceRow>();
            try
            {
                var createdBy = Session["LoginUser"] != null ? Session["LoginUser"].ToString() : "";
                list = _bl.GetLeadsBySource(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // GET: SalesNew/Pipeline -- the kanban board page.
        public ActionResult Pipeline()
        {
            return View();
        }

        // Reports landing page -- read-only analytics menu, out of the daily
        // workflow. Just the shell/menu for now; individual report cards
        // aren't wired to data yet.
        public ActionResult Reports()
        {
            return View();
        }

        // "Lost-reason analysis" report -- same StartRowNumber/EndRowNumber +
        // RowNumber/RecordsCount backend-driven paging contract as
        // GetPipelineList, same page size.
        public JsonResult GetLostReasonReport(string search, int pageNumber = 1)
        {
            const int pageSize = 10;
            int endRowNumber = pageNumber * pageSize;
            int startRowNumber = endRowNumber - pageSize + 1;

            var list = new List<enSalesLostReasonRow>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetLostReasonReport(createdBy, search, startRowNumber, endRowNumber);
            }
            catch (Exception)
            {
            }

            int showingFrom = 0;
            int showingTo = 0;
            int recordsCount = 0;
            int numberOfPages = 0;

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
                PagerSize = 10,           // max 10 page-number buttons, then next window
                NumberOfPages = numberOfPages,
                PageSize = pageSize
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Combined board data -- New/Contacted/Demo columns + Won/Lost
        // counts+totals, all top-30-per-list per the given queries. One
        // call instead of 5 round trips since the board renders all of
        // them together on load.
        public JsonResult GetPipelineBoard()
        {
            var newList = new List<enSalesPipelineCardRow>();
            var contactedList = new List<enSalesPipelineCardRow>();
            var demoList = new List<enSalesPipelineCardRow>();
            var wonList = new List<enSalesPipelineCardRow>();
            var lostList = new List<enSalesPipelineCardRow>();
            try
            {
                var createdBy = Session["LoginUser"] != null ? Session["LoginUser"].ToString() : "";
                newList = _bl.GetPipelineNew(createdBy);
                contactedList = _bl.GetPipelineContacted(createdBy);
                demoList = _bl.GetPipelineDemo(createdBy);
                wonList = _bl.GetPipelineWon(createdBy);
                lostList = _bl.GetPipelineLost(createdBy);
            }
            catch (Exception)
            {
                // fall through with whatever lists loaded before the failure
            }

            decimal wonTotal = 0;
            foreach (var row in wonList) wonTotal += row.Amount ?? 0;
            decimal lostTotal = 0;
            foreach (var row in lostList) lostTotal += row.Amount ?? 0;

            var returnValue = new
            {
                NewList = newList,
                ContactedList = contactedList,
                DemoList = demoList,
                WonCount = wonList.Count,
                WonTotal = wonTotal,
                LostCount = lostList.Count,
                LostTotal = lostTotal
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Pipeline "List" tab -- backend-driven paging, same
        // StartRowNumber/EndRowNumber + RowNumber/RecordsCount contract as
        // GetLeadActivity. No client-side page-count math anywhere.
        public JsonResult GetPipelineList(string stage, string leadType, string source, string search, DateTime? fromDate = null, DateTime? toDate = null, int pageNumber = 1)
        {
            const int pageSize = 50;
            int endRowNumber = pageNumber * pageSize;
            int startRowNumber = endRowNumber - pageSize + 1;

            var list = new List<enSalesPipelineListRow>();
            try
            {
                var createdBy = Session["LoginUser"] != null ? Session["LoginUser"].ToString() : "";
                list = _bl.GetPipelineList(createdBy, stage, leadType, source, search, fromDate, toDate, startRowNumber, endRowNumber);
            }
            catch (Exception)
            {
            }

            int showingFrom = 0;
            int showingTo = 0;
            int recordsCount = 0;
            int numberOfPages = 0;

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
                PagerSize = 10,           // max 10 page-number buttons, then next window
                NumberOfPages = numberOfPages,
                PageSize = pageSize
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // GET: SalesNew/LeadDetail?id=&source= -- single-lead detail page.
        // "source" distinguishes which table the id belongs to: New-stage
        // Pipeline cards come from SA_MaskEmail (source=New), everything
        // else (Contacted/Demo/Won/Lost) comes from SA_MaskEmail_Open. The
        // two tables have independent identity columns, so the id alone
        // isn't enough to know where to look it up.
        public ActionResult LeadDetail(int id, string source)
        {
            ViewBag.LeadID = id;
            ViewBag.Source = string.IsNullOrEmpty(source) ? "Open" : source;
            return View();
        }

        public JsonResult GetLeadDetail(int id, string source)
        {
            enSalesLeadDetail detail = null;
            try
            {
                detail = string.Equals(source, "New", StringComparison.OrdinalIgnoreCase)
                    ? _bl.GetLeadDetailNew(id)
                    : _bl.GetLeadDetail(id);
            }
            catch (Exception)
            {
            }

            var jsonResult = Json(new { DataItem = detail }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // New-stage "Lost"/"Junk" buttons - single status update on
        // SA_MaskEmail. "Open" (the third New-stage action) isn't wired up
        // yet - it needs to INSERT the lead's data into SA_MaskEmail_Open,
        // and that table's full column list needs confirming first so the
        // insert doesn't guess at columns that don't exist.
        [HttpPost]
        public JsonResult MarkLeadNewStatus(int id, string status)
        {
            bool success = false;
            string message = "";
            try
            {
                success = _bl.MarkLeadNewStatus(id, status);
                message = success ? "" : "Could not update this lead's status.";
            }
            catch (Exception)
            {
                message = "Could not update this lead's status - please try again.";
            }
            return Json(new { Success = success, Message = message });
        }

        // "Mark lost" action button (Open/Contacted-stage lead). Reason
        // isn't persisted yet - see dlSalesNew.MarkLeadLost.
        [HttpPost]
        public JsonResult MarkLeadLost(int id, string reason)
        {
            bool success = false;
            string message = "";
            try
            {
                success = _bl.MarkLeadLost(id, reason);
                message = success ? "" : "Could not mark this lead as lost.";
            }
            catch (Exception)
            {
                message = "Could not mark this lead as lost - please try again.";
            }
            return Json(new { Success = success, Message = message });
        }

        // "Mark won - start closing" modal. invoiceNumber is accepted for
        // forward compatibility but not persisted yet - see
        // dlSalesNew.MarkLeadWon. amountINR is optional - only meaningful
        // when currency is INR, saved into SA_MaskEmail_Open.AmountINR
        // alongside the (always mandatory) USD amount.
        [HttpPost]
        public JsonResult MarkLeadWon(int id, decimal? amount, string currency, decimal? amountINR, string paymentOption, string paymentTerms, string invoiceNumber, string OrderFrom)
        {
            bool success = false;
            string message = "";
            try
            {
                success = _bl.MarkLeadWon(id, amount, currency, amountINR, paymentOption, paymentTerms, invoiceNumber, OrderFrom);
                message = success ? "" : "Could not mark this lead as won.";
            }
            catch (Exception)
            {
                message = "Could not mark this lead as won - please try again.";
            }
            return Json(new { Success = success, Message = message });
        }
        // "Update" on the Lead summary card's Value row - Open/Contacted
        // stage leads only (SldSource != "New"), and only while the lead
        // isn't Won yet; the view enforces that visibility, this action just
        // persists it.
        [HttpPost]
        public JsonResult UpdateLeadValue(int id, decimal? amount, string currency, decimal? amountINR)
        {
            bool success = false;
            string message = "";
            try
            {
                success = _bl.UpdateLeadValue(id, amount, currency, amountINR);
                message = success ? "" : "Could not update the value.";
            }
            catch (Exception)
            {
                message = "Could not update the value - please try again.";
            }
            return Json(new { Success = success, Message = message });
        }

        // Inline edit on the Overview tab (Lead summary + Company & contact
        // cards). Updates the open lead's editable fields; the proc itself
        // blocks the update once Status = 'Won'.
        [HttpPost]
        public JsonResult UpdateLeadDetails(int id, string leadType, string source, string message, decimal? amount,
            DateTime? expectedCloseDate, DateTime? setFollowUp, string companyName, string name,
            string corpEmail, string contactNumber, string countryName)
        {
            bool success = false;
            string msg = "";
            try
            {
                success = _bl.UpdateLeadDetails(id, leadType, source, message, amount, expectedCloseDate, setFollowUp,
                    companyName, name, corpEmail, contactNumber, countryName);
                msg = success ? "" : "This lead is already won and can no longer be edited.";
            }
            catch (Exception)
            {
                msg = "Could not update the lead - please try again.";
            }
            return Json(new { Success = success, Message = msg });
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
        public JsonResult LogFollowup(int id, DateTime followUpDate)
        {
            bool success = false;
            string message = "";
            try
            {
                success = _bl.LogFollowup(id, followUpDate);
                message = success ? "" : "Could not update the follow-up date.";
            }
            catch (Exception)
            {
                message = "Could not update the follow-up date - please try again.";
            }
            return Json(new { Success = success, Message = message });
        }

        // "Activity & follow-ups" tab history -- server-side search + paging (5/page).
        // Same StartRowNumber/EndRowNumber + RowNumber/RecordsCount contract
        // as GetOpenAllMaskEmailListManager_CustomSearch -- the proc alone
        // works out RecordsCount/paging, this action just relays it
        // (ShowingFrom/ShowingTo/NumberOfPages) instead of recomputing
        // anything itself.
        public JsonResult GetLeadActivity(int id, string search, int pageNumber = 1)
        {
            const int pageSize = 5;
            int endRowNumber = pageNumber * pageSize;
            int startRowNumber = endRowNumber - pageSize + 1;

            var list = new List<enSalesActivityRow>();
            try
            {
                list = _bl.GetLeadActivity(id, search, startRowNumber, endRowNumber);
            }
            catch (Exception)
            {
            }

            int showingFrom = 0;
            int showingTo = 0;
            int recordsCount = 0;
            int numberOfPages = 0;

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
                PagerSize = 10,           // max 10 page-number buttons, then next window
                NumberOfPages = numberOfPages,
                PageSize = pageSize
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "Log follow-up" modal Save.
        [HttpPost]
        public JsonResult LogFollowupActivity(int id, string followupType, string discussion, string stage, DateTime? nextFollowUpDate, bool addToMyDay)
        {
            bool success = false;
            string message = "";
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                success = _bl.LogFollowupActivity(id, followupType, discussion, stage, nextFollowUpDate, addToMyDay, createdBy, out message);
            }
            catch (Exception)
            {
                message = "Could not save this follow-up - please try again.";
            }
            return Json(new { Success = success, Message = message });
        }

        // "Schedule demo" modal - writes to the real, already-existing
        // SA_MaskEmail_Open_DemoDetails table. demoTime/sendCalendarInvite
        // are still accepted from the form but unused - no column for
        // either on that table.
        [HttpPost]
        public JsonResult ScheduleDemo(int id, DateTime? demoDate, string demoTime, string productsToDemo, bool sendCalendarInvite = false)
        {
            bool success = false;
            string message = "";
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                success = _bl.ScheduleDemo(id, demoDate, productsToDemo, createdBy, out message);
            }
            catch (Exception)
            {
                message = "Could not schedule this demo - please try again.";
            }
            return Json(new { Success = success, Message = message });
        }

        // "Mark won" modal - saves the contract sign start/end dates into
        // SA_ContractSign (id = lead id, kept for reference; createdBy from session).
        [HttpPost]
        public JsonResult SaveContractSign(int id, DateTime? startDate, DateTime? endDate)
        {
            bool success = false;
            string message = "";
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                success = _bl.SaveContractSign(id, startDate, endDate, createdBy, out message);
            }
            catch (Exception)
            {
                message = "Could not save the contract sign dates.";
            }
            return Json(new { Success = success, Message = message });
        }

        public JsonResult GetContractSignByLead(int id)
        {
            var list = new List<enContractSign>();
            try
            {
                list = _bl.GetContractSignByLead(id);
            }
            catch (Exception)
            {
                list = new List<enContractSign>();
            }

            var shaped = (list ?? new List<enContractSign>()).Select(c => new
            {
                StartDate = c.StartDate.HasValue ? c.StartDate.Value.ToString("dd MMM yyyy") : null,
                EndDate = c.EndDate.HasValue ? c.EndDate.Value.ToString("dd MMM yyyy") : null,
                CreatedBy = c.CreatedBy,
                CreatedDate = c.CreatedDate.HasValue ? c.CreatedDate.Value.ToString("dd MMM yyyy, HH:mm") : null
            }).ToList();

            return Json(new { DataList = shaped }, JsonRequestBehavior.AllowGet);
        }

        // "Move to stage" dropdown options.
        public JsonResult GetStageOptions()
        {
            var list = new List<string>();
            try
            {
                list = _bl.GetStageOptions();
            }
            catch (Exception)
            {
            }

            return Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
        }

        // "Contacts" tab.
        public JsonResult GetLeadContacts(int id)
        {
            var list = new List<enSalesLeadContactRow>();
            try
            {
                list = _bl.GetLeadContacts(id);
            }
            catch (Exception)
            {
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "Add contact" drawer Save.
        [HttpPost]
        public JsonResult AddLeadContact(int id, string name, string corpEmail, string contactCountryCode, string contactNumber, string designation)
        {
            bool success = false;
            string message = "";
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                success = _bl.AddLeadContact(id, name, corpEmail, contactCountryCode, contactNumber, designation, createdBy, out message);
            }
            catch (Exception)
            {
                message = "Could not add this contact - please try again.";
            }
            return Json(new { Success = success, Message = message });
        }

        // "Create lead" modal on the Pipeline board -- same save logic and same
        // proc as LeadManagementSubscriptionController.SaveOutboundLeadDetails /
        // CreateOutboundLead.cshtml, just called from the new SalesNew Pipeline
        // UI instead of the old standalone page. Do not change
        // blLeadManagementSubscription.InsertOutboundLeadRecord or its proc.
        [HttpPost]
        public JsonResult SaveOutboundLeadDetails(Lead_Master GetDetails)
        {
            enLeadManagementSubscription enObj = new enLeadManagementSubscription();
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
                enObj.Phone = GetDetails.Phone;
                enObj.createdby = Session["User"] != null ? Session["User"].ToString() : "";
                enObj.RequestedDate = GetDetails.RequestedDate;
                enObj.ContactCountryCode = GetDetails.ContactCountryCode;
                enObj.TelephoneCountryCode = GetDetails.TelephoneCountryCode;

                ListofRespone = new blLeadManagementSubscription(new enLeadManagementSubscription()).InsertOutboundLeadRecord(enObj);
            }
            catch (Exception)
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
        }
    }
}
