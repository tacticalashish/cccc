using BusinessLayer;
using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ChemAnalyst.Controllers
{

    public class PreSalesNewController : Controller
    {
        private readonly blPreSalesNew _bl = new blPreSalesNew();
        private readonly blPreSalesDashboard _dashboardBl = new blPreSalesDashboard();

        // GET: PreSalesNew — the Leads list page.
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchDomain()
        {
            return View();
        }

        // GET: PreSalesNew/Companies — "Active customers, don't pitch"
        // cross-check list. Real paying customers (active subscription),
        // sourced from SalesPackageSubscriptions / SA_Customer /
        // SA_CompanyMaster — not SA_MaskEmail_PreSales. Company-wide, not
        // scoped to a single presales rep.
        public ActionResult Companies()
        {
            return View();
        }

        public JsonResult GetActiveCustomerList(string search)
        {
            var list = new List<enPreSalesActiveCustomer>();
            try
            {
                list = _bl.GetActiveCustomerList(search);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // True server-side page (10/page) for the Active customers table --
        // this list is company-wide, not scoped to one rep, so it can
        // legitimately get very large in production.
        public JsonResult GetActiveCustomerListPaged(string search, int pageNumber = 1, int pageSize = 10)
        {
            var list = new List<enPreSalesActiveCustomer>();
            int total = 0;
            try
            {
                list = _bl.GetActiveCustomerListPaged(search, pageNumber, pageSize, out total);
            }
            catch (Exception)
            {
                // fall through with empty list / zero count
                total = 0;
            }

            var jsonResult = Json(new { DataList = list, TotalCount = total, PageNumber = pageNumber, PageSize = pageSize }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "Inactive customers — re-engage" tab on the Companies page --
        // opposite of Active customers: companies whose subscription has
        // expired. Company-wide, true server-side page (10/page), no
        // detail popup on this tab.
        public JsonResult GetInactiveCustomerListPaged(string search, int pageNumber = 1, int pageSize = 10)
        {
            var list = new List<enPreSalesInactiveCustomer>();
            int total = 0;
            try
            {
                list = _bl.GetInactiveCustomerListPaged(search, pageNumber, pageSize, out total);
            }
            catch (Exception)
            {
                // fall through with empty list / zero count
                total = 0;
            }

            var jsonResult = Json(new { DataList = list, TotalCount = total, PageNumber = pageNumber, PageSize = pageSize }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "Demoed, not closed — re-target" tab on the Companies page —
        // every SA_MaskEmail_Open row for a company this logged-in
        // presales rep already transferred to sales. Search/country/date
        // filters + paging, same shape as GetLeadList (DataList/TotalCount).
        // DateFrom/DateTo filter on smo.CreatedDate (SA_MaskEmail_Open's own
        // CreatedDate), not the presales lead's CreatedDate.
        public JsonResult GetDemoedNotClosedList(string search, string country, string dateFrom, string dateTo, int pageNumber = 1, int pageSize = 10)
        {
            var list = new List<enPreSalesOpenLeadRow>();
            int total = 0;
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetDemoedNotClosedList(search, country, dateFrom, dateTo, createdBy, pageNumber, pageSize);
                total = _bl.GetDemoedNotClosedListCount(search, country, dateFrom, dateTo, createdBy);
            }
            catch (Exception)
            {
                // fall through with empty list / zero count
            }

            var jsonResult = Json(new { DataList = list, TotalCount = total, PageNumber = pageNumber, PageSize = pageSize }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "All countries" filter dropdown on the "Demoed, not closed" tab.
        public JsonResult GetDemoedNotClosedCountryList()
        {
            var list = new List<enPreSalesFilterOption>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetDemoedNotClosedCountryList(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // GET: PreSalesNew/Incentives — real incentive-earnings page for the
        // logged-in presales user. Backed entirely by the EXISTING
        // usp_GetSalesLeadStatusReporyPreSales proc (never altered here) —
        // Incentive = DemoCount * 500, per the user's explicit rule. The
        // "Demos completed" per-contact table has no backing query, so it
        // stays static, matching the mockup exactly (user's explicit choice).
        public ActionResult Incentives()
        {
            return View();
        }

        public JsonResult GetMonthlyPerformance()
        {
            var list = new List<enPreSalesMonthlyPerformance>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetMonthlyPerformance(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "Demos completed" table on the Incentives page — real leads with
        // a logged demo (SA_MaskEmail_Open_DemoDetails).
        public JsonResult GetDemosCompletedList()
        {
            var list = new List<enPreSalesDemoCompletedRow>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetDemosCompletedList(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // True server-side page (10/page) for the "Demos completed" table --
        // this can grow very large over time, so the browser never has to
        // hold the whole history in memory just to display 10 rows.
        public JsonResult GetDemosCompletedListPaged(string search, int pageNumber = 1, int pageSize = 10)
        {
            var list = new List<enPreSalesDemoCompletedRow>();
            int total = 0;
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetDemosCompletedListPaged(createdBy, search, pageNumber, pageSize);
                total = _bl.GetDemosCompletedListPagedCount(createdBy, search);
            }
            catch (Exception)
            {
                // fall through with empty list / zero count
            }

            var jsonResult = Json(new { DataList = list, TotalCount = total, PageNumber = pageNumber, PageSize = pageSize }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Naturally bounded by the date window (this month / this quarter /
        // last 6 months), so safe to fetch in full for the KPI card's
        // "see the leads" popup, regardless of how big the overall history is.
        public JsonResult GetDemosCompletedListByRange(string rangeType)
        {
            var list = new List<enPreSalesDemoCompletedRow>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetDemosCompletedListByRange(createdBy, rangeType);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // The 3 incentive KPI numbers, computed and counted entirely in SQL
        // instead of fetching every demoed lead to the browser to count
        // date ranges there.
        public JsonResult GetIncentiveKpiCounts()
        {
            enPreSalesIncentiveKpiCounts counts = new enPreSalesIncentiveKpiCounts();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                counts = _bl.GetIncentiveKpiCounts(createdBy);
            }
            catch (Exception)
            {
                // fall through with zeroed counts
            }

            var jsonResult = Json(new { DataItem = counts }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Small, bounded (max 6 rows) month-by-month breakdown for the
        // Dashboard's "Incentive earned" popup -- grouped server-side.
        public JsonResult GetIncentiveLast6MonthsBreakdown()
        {
            var list = new List<enPreSalesIncentiveMonthCount>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetIncentiveLast6MonthsBreakdown(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Monthly "Demos done" counts by actual demo date -- naturally
        // bounded by month count, safe to fetch in full. Merged client-side
        // into the Monthly Performance table so its Demos done / Incentive
        // earned columns match the top KPI cards' demo-date basis instead of
        // the real report proc's RequestedDate basis.
        public JsonResult GetMonthlyDemoCountsByDemoDate()
        {
            var list = new List<enPreSalesMonthDemoCount>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetMonthlyDemoCountsByDemoDate(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Drill-down for the Monthly Performance table's "Demos done" cell --
        // demo-date based (see GetMonthlyDemoCountsByDemoDate), kept separate
        // from GetMonthlyPerformanceDetail which is RequestedDate based.
        public JsonResult GetDemosCompletedListByMonth(int monthNo, int yearNo)
        {
            var list = new List<enPreSalesDemoCompletedRow>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetDemosCompletedListByMonth(createdBy, monthNo, yearNo);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // True server-side page/search for the Monthly Performance table --
        // usp_PreSalesNew_GetMonthlyPerformancePaged captures the real
        // usp_GetSalesLeadStatusReporyPreSales report via INSERT...EXEC
        // (untouched) and merges it with the demo-date based count, so
        // paging/search happens entirely in SQL instead of fetching every
        // month to the browser.
        public JsonResult GetMonthlyPerformancePaged(string search, int pageNumber = 1, int pageSize = 10)
        {
            var list = new List<enPreSalesMonthlyPerformancePagedRow>();
            int total = 0;
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetMonthlyPerformancePaged(createdBy, search, pageNumber, pageSize);
                total = _bl.GetMonthlyPerformancePagedCount(createdBy, search);
            }
            catch (Exception)
            {
                // fall through with empty list / zero count
            }

            var jsonResult = Json(new { DataList = list, TotalCount = total, PageNumber = pageNumber, PageSize = pageSize }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Monthly performance row click-through popup on the Incentives
        // page — every lead this user generated in that month.
        public JsonResult GetMonthlyPerformanceDetail(int monthNo, int yearNo)
        {
            var list = new List<enPreSalesMonthlyLeadDetail>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetMonthlyPerformanceDetail(createdBy, monthNo, yearNo);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // GET: PreSalesNew/Dashboard — KPI/overview page for the logged-in
        // presales user. Sections with real backing data (leads, follow-ups,
        // activity outcomes) are wired to SA_MaskEmail_PreSales /
        // SA_MaskEmail_PreSalesFollowup / SA_MaskEmail_PreSales_LogActivity.
        // Sections with no backing table yet (incentive amounts, recently
        // credited names, demo-done/won funnel stages) are intentionally
        // static in the view — there's no commission/sales-side tracking
        // system to pull that data from.
        public ActionResult Dashboard()
        {
            return View();
        }

        public JsonResult GetDashboardStats()
        {
            enPreSalesDashboardStats stats = new enPreSalesDashboardStats();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                stats = _dashboardBl.GetStats(createdBy);
            }
            catch (Exception)
            {
                // fall through with zeroed stats
            }

            var jsonResult = Json(new { DataItem = stats }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetDashboardLeadsBySource()
        {
            var list = new List<enPreSalesSourceCount>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _dashboardBl.GetLeadsBySource(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetDashboardFollowupsList()
        {
            var list = new List<enPreSalesFollowupRow>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _dashboardBl.GetFollowupsDueList(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Paged/searchable version backing the full "Follow-ups" table on
        // the Dashboard page. GetDashboardFollowupsList above is left as-is
        // for the KPI card's quick popup.
        public JsonResult GetDashboardFollowupsListPaged(string search, int pageNumber = 1, int pageSize = 5)
        {
            var list = new List<enPreSalesFollowupRow>();
            int total = 0;
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _dashboardBl.GetFollowupsDueListPaged(createdBy, search, pageNumber, pageSize);
                total = _dashboardBl.GetFollowupsDueListPagedCount(createdBy, search);
            }
            catch (Exception)
            {
                // fall through with empty list / zero count
            }

            var jsonResult = Json(new { DataList = list, TotalCount = total, PageNumber = pageNumber, PageSize = pageSize }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetDashboardTransferredList()
        {
            var list = new List<enPreSalesTransferredRow>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _dashboardBl.GetTransferredLeads(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // True server-side page (10/page) for the "Transferred — Awaiting
        // sales demo" table -- GetDashboardTransferredList above stays a
        // small quick-glance list for the KPI card's popup.
        public JsonResult GetDashboardTransferredListPaged(string search, int pageNumber = 1, int pageSize = 10)
        {
            var list = new List<enPreSalesTransferredRow>();
            int total = 0;
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _dashboardBl.GetTransferredListPaged(createdBy, search, pageNumber, pageSize);
                total = _dashboardBl.GetTransferredListPagedCount(createdBy, search);
            }
            catch (Exception)
            {
                // fall through with empty list / zero count
            }

            var jsonResult = Json(new { DataList = list, TotalCount = total, PageNumber = pageNumber, PageSize = pageSize }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetDashboardWorkTodayList()
        {
            var list = new List<enPreSalesWorkTodayRow>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _dashboardBl.GetWorkTodayLeads(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetDashboardDemosByMonth()
        {
            var list = new List<enPreSalesMonthDemo>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _dashboardBl.GetDemosByMonth(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetDashboardFunnel()
        {
            enPreSalesFunnel funnel = new enPreSalesFunnel();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                funnel = _dashboardBl.GetFunnel(createdBy);
            }
            catch (Exception)
            {
                // fall through with zeroed funnel
            }

            var jsonResult = Json(new { DataItem = funnel }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Backs the "click a KPI card to see the leads behind it" popups.
        public JsonResult GetDashboardLeadsGeneratedList()
        {
            var list = new List<enPreSalesStatDetailRow>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _dashboardBl.GetLeadsGeneratedList(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetDashboardDemosFixedList()
        {
            var list = new List<enPreSalesStatDetailRow>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _dashboardBl.GetDemosFixedList(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetDashboardPendingTransferList()
        {
            var list = new List<enPreSalesStatDetailRow>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _dashboardBl.GetPendingTransferList(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // GET: PreSalesNew/LeadDetail?id= — single-lead detail page opened
        // by clicking any row on the Leads list. Just renders the shell;
        // the page itself fetches the lead via GetLeadById on load.
        public ActionResult LeadDetail(int id)
        {
            ViewBag.LeadId = id;
            return View();
        }

        // Backs the LeadDetail page's data load.
        public JsonResult GetLeadById(int id)
        {
            enPreSalesNew lead = null;
            try
            {
                lead = _bl.GetLeadById(id);
            }
            catch (Exception)
            {
                // fall through with a null result
            }

            var jsonResult = Json(new { DataItem = lead }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // LeadDetail page — assigned sales person for the lead's country
        // (SA_AssignSalesPersonCountry). Country name comes from the loaded
        // lead, so this is a small second call once the lead is on screen.
        public JsonResult GetAssignPersonByCountry(string country)
        {
            string assignPerson = "";
            try
            {
                assignPerson = _bl.GetAssignPersonByCountry(country);
            }
            catch (Exception)
            {
                // fall through with an empty result
            }

            return Json(new { AssignPerson = assignPerson }, JsonRequestBehavior.AllowGet);
        }

        // "Edit lead" drawer on the LeadDetail page. The proc itself blocks
        // the update once the lead's Status = 'Transferred', reporting back
        // via an output param, so Success=false here means "already
        // transferred" (or the lead ID wasn't found) rather than a hard
        // error -- Message tells the two cases apart.
        [HttpPost]
        public JsonResult UpdateLead(enPreSalesNew lead)
        {
            bool success = false;
            string message = "";
            try
            {
                if (lead == null || lead.ID <= 0)
                {
                    message = "Invalid lead.";
                }
                else
                {
                    success = _bl.UpdateLead(lead);
                    message = success ? "" : "This lead has already been transferred and can no longer be edited.";
                }
            }
            catch (Exception)
            {
                message = "Could not update lead - please try again.";
            }

            return Json(new { Success = success, Message = message });
        }

        // "All sources" filter dropdown.
        public JsonResult GetSourceList()
        {
            var list = new List<enPreSalesFilterOption>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetSourceList(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "All statuses" filter dropdown.
        public JsonResult GetStatusList()
        {
            var list = new List<enPreSalesFilterOption>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetStatusList(createdBy);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // State dropdown on the Create Pre-Sales lead panel — shown only
        // when Country = India. Reuses the existing
        // blLeadManagementSubscription.ReadStateDDL (already backed by the
        // usp_GetStateDDL stored procedure) instead of writing a new one,
        // since that method already exists and works.
        public JsonResult GetStateList(int countryId)
        {
            var list = new List<enPreSalesFilterOption>();
            try
            {
                if (countryId > 0)
                {
                    var states = new blLeadManagementSubscription(new enLeadManagementSubscription())
                        .ReadStateDDL(new enSearchFilter { Country = countryId });
                    list = states
                        .Select(s => new enPreSalesFilterOption { Value = s.StateId.ToString(), Text = s.StateName })
                        .ToList();
                }
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Leads list — scoped to the logged-in presales user, same as
        // GetPreSalesLeadList_CustomSearch's searchFilter.UserName ->
        // @Createdby, plus search + source + status filters, paged.
        public JsonResult GetLeadList(string search, string source, string status, DateTime? fromDate = null, DateTime? toDate = null, int pageNumber = 1, int pageSize = 15)
        {
            var list = new List<enPreSalesNew>();
            int total = 0;
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                list = _bl.GetLeadList(search, source, status, createdBy, fromDate, toDate, pageNumber, pageSize);
                total = _bl.GetLeadListCount(search, source, status, createdBy, fromDate, toDate);
            }
            catch (Exception)
            {
                // fall through with empty list / zero count
            }

            var jsonResult = Json(new { DataList = list, TotalCount = total, PageNumber = pageNumber, PageSize = pageSize }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "Set follow-up" on the LeadDetail page — SA_MaskEmail_PreSalesFollowup.
        [HttpPost]
        public JsonResult SaveFollowup(enPreSalesFollowup followup)
        {
            int newId = 0;
            try
            {
                if (followup != null && followup.LeadID > 0)
                {
                    followup.CreatedBy = Session["User"] != null ? Session["User"].ToString() : "";
                    newId = _bl.InsertFollowup(followup);
                }
            }
            catch (Exception)
            {
                // fall through with newId = 0
            }

            var jsonResult = Json(new { FollowupId = newId });
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Backs the LeadDetail page's Follow-up box.
        public JsonResult GetFollowupByLeadId(int leadId)
        {
            enPreSalesFollowup followup = null;
            try
            {
                followup = _bl.GetFollowupByLeadId(leadId);
            }
            catch (Exception)
            {
                // fall through with a null result
            }

            var jsonResult = Json(new { DataItem = followup }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Full follow-up history for the LeadDetail page's Follow-up box
        // (shows the whole list, scrollable, instead of just the latest one).
        public JsonResult GetFollowupsByLeadId(int leadId)
        {
            var list = new List<enPreSalesFollowup>();
            try
            {
                list = _bl.GetFollowupsByLeadId(leadId);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // "+ Add" activity on the LeadDetail page — SA_MaskEmail_PreSales_LogActivity.
        [HttpPost]
        public JsonResult SaveActivity(enPreSalesActivity activity)
        {
            int newId = 0;
            try
            {
                if (activity != null && activity.LeadID > 0 && !string.IsNullOrWhiteSpace(activity.ActivityNote))
                {
                    activity.CreatedBy = Session["User"] != null ? Session["User"].ToString() : "";
                    newId = _bl.InsertActivity(activity);
                }
            }
            catch (Exception)
            {
                // fall through with newId = 0
            }

            var jsonResult = Json(new { ActivityId = newId });
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Backs the LeadDetail page's Activity timeline.
        public JsonResult GetActivityByLeadId(int leadId)
        {
            var list = new List<enPreSalesActivity>();
            try
            {
                list = _bl.GetActivityByLeadId(leadId);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}
