using BusinessLayer;
using DataLayer;
using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EntityLayer;

namespace ChemAnalyst.Controllers
{
    // New dedicated controller for Sales Management features, following this
    // project's en/bl/dal convention (see enSalesManagement, blSalesManagement,
    // dlSalesManagement) — actions get added here as each feature is defined.
    public class SalesManagementController : Controller
    {
        private readonly blSalesManagement _bl = new blSalesManagement();

        // GET: SalesManagement
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SalesIndex()
        {
            return View();
        }
        
        // GET: SalesManagement/CreateSubscription — "New Subscription" form,
        // Primary contact & company step. Optional companyId comes from the
        // "Renew / extend" button on LeadManagementSubscription/CustomerDetail
        // — when present, the Contact step pre-selects that company and
        // autofills its contact details the same way picking it manually would.
        // Optional leadId comes from CRMRenew_LeadDetails.cshtml's Update
        // button when a lead is marked "Renew Won" — the Contact step then
        // pulls that lead's own contact details (name/designation/email/
        // phone/country) via the same BindCRMOpenLeadDetailsByid endpoint
        // that page already uses to bind its own fields.
        // Optional salesFlow=1 comes from the "New subscription" popup on
        // SalesManagement/SalesIndex.cshtml (Sales role only) - both its
        // "Convert" and "Start a blank subscription" buttons set it, so the
        // Contact step's Company/Secondary-emails fields start in free-text
        // mode even for a blank subscription with no leadId to fall back to.
        public ActionResult CreateSubscription(int? companyId, int? leadId, bool salesFlow = false)
        {
            ViewBag.PrefillCompanyId = companyId ?? 0;
            ViewBag.PrefillLeadId = leadId ?? 0;
            ViewBag.IsSalesFlow = salesFlow;
            return View();
        }

        // "New subscription" popup on SalesIndex.cshtml (Sales role only -
        // CRM's Index.cshtml keeps going straight to CreateSubscription,
        // unchanged). Lists Won leads for the logged-in sales rep that don't
        // have a company/subscription yet, so they can be converted instead
        // of picking an existing Company that doesn't exist yet. Data comes
        // from SA_MaskEmail_Open via blSalesNew, not this controller's usual
        // blSalesManagement.
        public JsonResult GetWonLeadsWithoutSubscription()
        {
            var list = new List<enSalesWonLeadRow>();
            try
            {
                var createdBy = Session["User"] != null ? Session["User"].ToString() : "";
                var blSales = new blSalesNew();
                list = blSales.GetWonLeadsWithoutSubscription(createdBy);
            }
            catch (Exception)
            {
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // Reports moved to LeadManagementSubscription/Reports — this is a
        // Lead Management page, not Sales Management.

        // Populates the Company dropdown on the CreateSubscription form.
        public JsonResult GetCompanyList()
        {
            var list = new List<enSelectItem>();
            try
            {
                list = _bl.GetCompanyList();
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Called when a company is selected on CreateSubscription — returns
        // the primary contact name/email/phone/designation to auto-fill.
        public JsonResult GetCompanyDetails(int companyId)
        {
            enSalesManagement details = null;
            try
            {
                if (companyId > 0)
                {
                    details = _bl.GetCompanyDetails(companyId);
                }
            }
            catch (Exception)
            {
                // fall through with a null result
            }

            var jsonResult = Json(new { DataList = details }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Populates the "SELECT PRODUCTS" checklist on Step 2 (Package).
        public JsonResult GetProductList()
        {
            var list = new List<enSelectItem>();
            try
            {
                list = _bl.GetProductList();
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Populates the Country dropdown on Step 1.
        public JsonResult GetCountryList()
        {
            var list = new List<enSelectItem>();
            try
            {
                list = _bl.GetCountryList();
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Populates the Secondary client emails dropdown once a company is
        // selected on Step 1.
        public JsonResult GetSecondaryClientEmails(int companyId)
        {
            var list = new List<enSelectItem>();
            try
            {
                if (companyId > 0)
                {
                    list = _bl.GetSecondaryClientEmails(companyId);
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

        // Populates the à la carte country grid on Step 3 (Coverage) for one product.
        public JsonResult GetCountriesByProduct(int productId)
        {
            var list = new List<enSelectItem>();
            try
            {
                if (productId > 0)
                {
                    list = _bl.GetCountriesByProduct(productId);
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

        // Populates the Market tab's product checklist on Step 2 (Package).
        public JsonResult GetMarketProductList()
        {
            var list = new List<enSelectItem>();
            try
            {
                list = _bl.GetMarketProductList();
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Populates the Package step's News tab product checklist.
        public JsonResult GetNewsProductList()
        {
            var list = new List<enSelectItem>();
            try
            {
                list = _bl.GetNewsProductList();
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Populates the Package step's Capacity Tracker tab product checklist.
        public JsonResult GetCapacityTrackerProductList()
        {
            var list = new List<enSelectItem>();
            try
            {
                list = _bl.GetCapacityTrackerProductList();
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Populates the Market sub-tab (countries + regions) inside a
        // product's coverage row on Step 3 (Coverage).
        public JsonResult GetMarketCoverageByProduct(int productId)
        {
            var countries = new List<enSelectItem>();
            var regions = new List<enSelectItem>();
            try
            {
                if (productId > 0)
                {
                    // One combined call (one stored proc, two result sets)
                    // instead of two separate round trips for the same product.
                    _bl.GetMarketCoverageByProduct(productId, out countries, out regions);
                }
            }
            catch (Exception)
            {
                // fall through with empty lists
            }

            var jsonResult = Json(new { Countries = countries, Regions = regions }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Batched Coverage fetch for the Full package Coverage step — one
        // call for every selected product instead of GetCountriesByProduct +
        // GetMarketCoverageByProduct per product (which explodes into 1000s
        // of requests at full-catalog scale). POST because the comma-joined
        // product id list can be long enough to blow a GET URL's length limit.
        // A la carte (small product counts) keeps using the original
        // per-product endpoints above — this is Full-package only.
        [HttpPost]
        public JsonResult GetBulkCoverageByProducts(string productIds)
        {
            var chemical = new Dictionary<string, List<enSelectItem>>();
            var marketCountries = new Dictionary<string, List<enSelectItem>>();
            var marketRegions = new Dictionary<string, List<enSelectItem>>();
            try
            {
                if (!string.IsNullOrEmpty(productIds))
                {
                    _bl.GetBulkCoverageByProducts(productIds, out chemical, out marketCountries, out marketRegions);
                }
            }
            catch (Exception)
            {
                // fall through with empty dictionaries
            }

            var jsonResult = Json(new { Chemical = chemical, MarketCountries = marketCountries, MarketRegions = marketRegions });
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Called right before SaveLeadRecord when Transfer to CSM is clicked —
        // creates the one SA_SubscriptionRequest row for this submission (the
        // Subscriptions list reads from this table, not from Lead_Master
        // directly, since one submission can fan out into many Lead_Master
        // rows). Returns the new RequestId, which the page then stamps into
        // every Lead_Master row it saves (via InterestArea) so coverage can
        // still be traced back if ever needed.
        [HttpPost]
        public JsonResult CreateSubscriptionRequest(enSalesManagement request)
        {
            int newId = 0;
            try
            {
                if (request != null)
                {
                    request.CreatedBy = Session["User"] != null ? Session["User"].ToString() : "";
                    newId = _bl.CreateSubscriptionRequest(request);
                }
            }
            catch (Exception)
            {
                // fall through with newId = 0
            }

            var jsonResult = Json(new { RequestId = newId });
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Populates the Subscriptions list (Index.cshtml) — search + paging.
        public JsonResult GetSubscriptionRequests(string search, int pageNumber = 1, int pageSize = 20)
        {
            var list = new List<enSalesManagement>();
            int total = 0;
            try
            {
                // One proc call now returns both the page of rows and the
                // total count (RecordsCount window column) instead of two
                // separate queries.
                list = _bl.GetSubscriptionRequests(search, pageNumber, pageSize, out total);
            }
            catch (Exception)
            {
                // fall through with empty list / zero count
            }

            var jsonResult = Json(new { DataList = list, TotalCount = total, PageNumber = pageNumber, PageSize = pageSize }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Subscription request detail page -- clicking any row on the
        // Subscriptions list (Index.cshtml) lands here.
        public ActionResult SubscriptionRequestDetail(string company)
        {
            ViewBag.CompanyName = company;
            return View();
        }

        public JsonResult GetSubscriptionRequestDetail(string company, string search, int pageNumber = 1)
        {
            const int pageSize = 10;
            int endRowNumber = pageNumber * pageSize;
            int startRowNumber = endRowNumber - pageSize + 1;

            var lines = new List<enSubscriptionRequestLine>();
            try
            {
                lines = _bl.GetSubscriptionRequestLines(company, search, startRowNumber, endRowNumber);
            }
            catch (Exception)
            {
                // fall through with empty lines
            }

            int showingFrom = 0;
            int showingTo = 0;
            int recordsCount = 0;
            int numberOfPages = 0;

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

        // Reports (LeadManagementSubscription/Reports) — Churn & Lost
        // Renewals section. SA_MaskEmailOpen_CRM where Lead_status = 'Lost'.
        public JsonResult GetLostRenewals()
        {
            var list = new List<enSalesManagement>();
            try
            {
                list = _bl.GetLostRenewals(50);
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetTopChurnReasons()
        {
            var list = new List<enSelectItem>();
            try
            {
                list = _bl.GetTopChurnReasons();
            }
            catch (Exception)
            {
                // fall through with an empty list
            }

            var jsonResult = Json(new { DataList = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // Step 4 (Additional users) on CreateSubscription.cshtml — CRM flow
        // only, called from the Company dropdown's change handler.
        public JsonResult GetExistingSecondaryUsers(int companyId)
        {
            var list = new List<enSecondaryUser>();
            try
            {
                if (companyId > 0)
                {
                    list = _bl.GetExistingSecondaryUsers(companyId);
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

        // Step 2 (Package) on CreateSubscription.cshtml — CRM flow only,
        // called from the Company dropdown's change handler.
        public JsonResult GetSubscribedProductsByCompany(int companyId)
        {
            var list = new List<enSubscribedProduct>();
            try
            {
                if (companyId > 0)
                {
                    list = _bl.GetSubscribedProductsByCompany(companyId);
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

        // "Subscriptions" list on SalesIndex.cshtml -- backend-driven paging,
        // same StartRowNumber/EndRowNumber + RowNumber/RecordsCount contract
        // used across the SalesNew module (proc alone decides RecordsCount,
        // this action only relays ShowingFrom/ShowingTo/NumberOfPages).
        public JsonResult GetSalesIndexList(string search, int pageNumber = 1)
        {
            const int pageSize = 10;
            int endRowNumber = pageNumber * pageSize;
            int startRowNumber = endRowNumber - pageSize + 1;

            var list = new List<enSalesManagement>();
            try
            {
                int userId = Session["LoginUser"] != null ? Convert.ToInt32(Session["LoginUser"]) : 0;
                list = _bl.GetSalesIndexSubscriptionRequests(search, startRowNumber, endRowNumber, userId);
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
                PagerSize = pageSize,
                NumberOfPages = numberOfPages,
                PageSize = pageSize
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // CRM dashboard "New accounts - approval pipeline" - accounts assigned
        // to the logged-in CRM (SA_CRMCustomerRelation), top 10, with New/Renew.
        public JsonResult GetCRMApprovalPipeline()
        {
            var list = new List<enSalesManagement>();
            try
            {
                int crmId = Session["LoginUser"] != null ? Convert.ToInt32(Session["LoginUser"]) : 0;
                list = _bl.GetCRMApprovalPipeline(crmId, 10);
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
