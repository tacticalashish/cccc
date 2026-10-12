using System;

namespace EntityLayer
{
    // Entity for SalesManagementController. Fields get added here as each
    // Sales Management feature is defined — kept minimal until then.
    public class enSalesManagement
    {
        public int Id { get; set; }
        public string ActionType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // New Subscription — "Primary contact & company" step.
        // CompanyId/CompanyName identify the SA_Companymaster row; the rest
        // are either typed by the user or auto-filled from that company's
        // record once CompanyId is selected (see dlSalesManagement.GetCompanyDetailsById).
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ClientType { get; set; }
        public string PrimaryContactName { get; set; }
        public string Designation { get; set; }
        public string PrimaryEmail { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string BillingEmail { get; set; }
        public string SecondaryClientEmails { get; set; }

        // New Subscription — "Package" step (Full package vs. À la carte).
        public string PackageType { get; set; }
        public string SelectedProductIds { get; set; }

        // Subscriptions list (SA_SubscriptionRequest) — one row per
        // "Transfer to CSM" submission, independent of how many Lead_Master
        // rows that submission fanned out into (one per country/region).
        public int RequestId { get; set; }
        public string PackageSummary { get; set; }
        public string CoverageSummary { get; set; }
        public string CSM { get; set; }
        public int StepCounter { get; set; }
        public string StatusRemark { get; set; }
        public DateTime? RequestCreatedDate { get; set; }
        public int TotalRequests { get; set; }

        // Reports — Churn & Lost Renewals (SA_MaskEmailOpen_CRM where
        // Lead_status = 'Lost'). Reuses CompanyName above for the company.
        public string LostReason { get; set; }
        public decimal? LostArr { get; set; }
        public string LostCurrency { get; set; }
        public DateTime? LostDate { get; set; }

        // SalesIndex "Subscriptions" list (usp_SalesIndex_GetSubscriptionRequests,
        // Lead_Master-based approvals workflow) -- distinct from the
        // SA_SubscriptionRequest-based list above. Reuses CompanyName/
        // PrimaryContactName/PackageSummary/CoverageSummary/CSM/
        // RequestCreatedDate for the matching concepts.
        public string RequestNumber { get; set; }
        public string SalesIndexStatus { get; set; }
        // 'Renewal' when the company is already an existing customer
        // (SalesPackageSubscriptions), else '' -- used to colour the CRM
        // dashboard approval pipeline (Renewal vs New).
        public string Renew { get; set; }
        public int RowNumber { get; set; }
        public int RecordsCount { get; set; }
    }

    // One row per Lead_Master record for this company (one per covered
    // country/region) -- keyed straight off CompanyName (Organisation),
    // no SA_SubscriptionRequest table involved.
    public class enSubscriptionRequestLine
    {
        public int LeadMasterId { get; set; }
        public string Company { get; set; }
        public string PrimaryContact { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string CRM { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string SecondaryClintEmail { get; set; }
        public string Package { get; set; }
        public string Products { get; set; }
        public DateTime? SubscriptionDateFrom { get; set; }
        public DateTime? SubscriptionDateTo { get; set; }
        public string Trader { get; set; }
        public string Renew { get; set; }
        public string Credit { get; set; }
        public string BillingEmail { get; set; }
        public string UserType { get; set; }
        public string Phone { get; set; }
        public int RowNumber { get; set; }
        public int RecordsCount { get; set; }
    }
}
