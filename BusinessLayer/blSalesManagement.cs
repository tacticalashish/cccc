using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    // BL for SalesManagementController, sits between the controller and
    // dlSalesManagement. Methods get added here as each Sales Management
    // feature is defined.
    public class blSalesManagement
    {
        private readonly dlSalesManagement _dal = new dlSalesManagement();

        // New Subscription > Primary contact & company step.
        public List<enSelectItem> GetCompanyList()
        {
            return _dal.GetCompanyList();
        }

        public enSalesManagement GetCompanyDetails(int companyId)
        {
            return _dal.GetCompanyDetailsById(companyId);
        }

        // Step 2 (Package) — product checklist for the "À la carte" option.
        public List<enSelectItem> GetProductList()
        {
            return _dal.GetProductList();
        }

        public List<enSelectItem> GetCountryList()
        {
            return _dal.GetCountryList();
        }

        public List<enSelectItem> GetSecondaryClientEmails(int companyId)
        {
            return _dal.GetSecondaryClientEmailsByCompanyId(companyId);
        }

        // Step 3 (Coverage) — countries tracked for a specific product.
        public List<enSelectItem> GetCountriesByProduct(int productId)
        {
            return _dal.GetCountriesByProduct(productId);
        }

        // Package step — Market tab product checklist.
        public List<enSelectItem> GetMarketProductList()
        {
            return _dal.GetMarketProductList();
        }

        // Package step — News tab product checklist.
        public List<enSelectItem> GetNewsProductList()
        {
            return _dal.GetNewsProductList();
        }

        // Package step — Capacity Tracker tab product checklist.
        public List<enSelectItem> GetCapacityTrackerProductList()
        {
            return _dal.GetCapacityTrackerProductList();
        }

        // Step 3 (Coverage) — Market tab, countries + regions for a product.
        // One combined call (one stored proc, two result sets) instead of
        // two separate round trips, since the controller always needs both
        // for the same product at the same time.
        public void GetMarketCoverageByProduct(int productId, out List<enSelectItem> countries, out List<enSelectItem> regions)
        {
            _dal.GetMarketCoverageByProduct(productId, out countries, out regions);
        }

        // Batched Coverage fetch for Full package — see dlSalesManagement for details.
        public void GetBulkCoverageByProducts(string productIdsCsv,
            out Dictionary<string, List<enSelectItem>> chemicalByProduct,
            out Dictionary<string, List<enSelectItem>> marketCountriesByProduct,
            out Dictionary<string, List<enSelectItem>> marketRegionsByProduct)
        {
            _dal.GetBulkCoverageByProducts(productIdsCsv, out chemicalByProduct, out marketCountriesByProduct, out marketRegionsByProduct);
        }

        // Subscriptions list — one row per "Transfer to CSM" submission.
        public int CreateSubscriptionRequest(enSalesManagement req)
        {
            return _dal.CreateSubscriptionRequest(req);
        }

        public List<enSalesManagement> GetSubscriptionRequests(string search, int pageNumber, int pageSize, out int totalCount)
        {
            return _dal.GetSubscriptionRequests(search, pageNumber, pageSize, out totalCount);
        }

        public List<enSubscriptionRequestLine> GetSubscriptionRequestLines(string companyName, string search, int startRowNumber, int endRowNumber)
        {
            return _dal.GetSubscriptionRequestLines(companyName, search, startRowNumber, endRowNumber);
        }

        // Reports — Churn & Lost Renewals.
        public List<enSalesManagement> GetLostRenewals(int top)
        {
            return _dal.GetLostRenewals(top);
        }

        public List<enSelectItem> GetTopChurnReasons()
        {
            return _dal.GetTopChurnReasons();
        }

        // "Subscriptions" list on SalesIndex.cshtml.
        public List<enSalesManagement> GetSalesIndexSubscriptionRequests(string search, int startRowNumber, int endRowNumber, int userId)
        {
            return _dal.GetSalesIndexSubscriptionRequests(search, startRowNumber, endRowNumber, userId);
        }

        public List<enSalesManagement> GetCRMApprovalPipeline(int crmId, int top)
        {
            return _dal.GetCRMApprovalPipeline(crmId, top);
        }

        // Step 4 (Additional users) on CreateSubscription.cshtml, CRM flow only.
        public List<enSecondaryUser> GetExistingSecondaryUsers(int companyId)
        {
            return _dal.GetExistingSecondaryUsers(companyId);
        }

        // Step 2 (Package) on CreateSubscription.cshtml, CRM flow only.
        public List<enSubscribedProduct> GetSubscribedProductsByCompany(int companyId)
        {
            return _dal.GetSubscribedProductsByCompany(companyId);
        }
    }
}
