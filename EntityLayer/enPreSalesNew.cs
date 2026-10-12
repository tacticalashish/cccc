using System;

namespace EntityLayer
{
    // Entity for the new PreSalesNew Leads list feature (PreSalesNewController
    // in the admin project). Mirrors the columns of SA_MaskEmail_PreSales.
    // Kept separate from enLeadManagementSubscription since this is a fresh
    // build for a new page, not a reuse of the existing PreSales code path.
    public class enPreSalesNew
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CorpEmail { get; set; }
        public string ContactCountryCode { get; set; }
        public string ContactNumber { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public int? CountryFID { get; set; }
        public string CountryName { get; set; }
        public int? StateFID { get; set; }
        public string StateName { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string OutboundLeadSource { get; set; }
        public string Status { get; set; }
        public string LeadType { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string TelephoneCountryCode { get; set; }
        public string Telephone { get; set; }
        public string MailChain { get; set; }

        // Sector / Size — the Leads list mockup has these columns, but
        // SA_MaskEmail_PreSales has no backing data for either, so these
        // stay null and the UI renders them blank rather than guessing.
        public string Sector { get; set; }
        public string Size { get; set; }

        // Paging / total count, only populated on the list endpoint.
        public int RecordsCount { get; set; }
    }

    // Lightweight Value/Text pair for the source/status filter dropdowns.
    // Deliberately not System.Web.Mvc.SelectListItem — EntityLayer/DataLayer
    // don't reference System.Web.Mvc, and this avoids adding that dependency
    // just for two dropdowns.
    public class enPreSalesFilterOption
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

    // The LeadDetail page's Follow-up box — backed by the new
    // SA_MaskEmail_PreSalesFollowup table (one lead can have several
    // follow-up entries over time; the page shows the most recent
    // not-yet-completed one).
    public class enPreSalesFollowup
    {
        public int ID { get; set; }
        public int LeadID { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string Note { get; set; }
        public bool IsCompleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    // The LeadDetail page's Activity timeline — backed by the new
    // SA_MaskEmail_PreSales_LogActivity table. Same shape/pattern as
    // enPreSalesFollowup: one lead can have many entries, newest first.
    public class enPreSalesActivity
    {
        public int ID { get; set; }
        public int LeadID { get; set; }
        public string ActivityType { get; set; }
        public string ActivityNote { get; set; }
        public string Outcome { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    // The "Companies" page's "Active customers — don't pitch" tab —
    // real paying customers, cross-checked against SalesPackageSubscriptions
    // (active subscription) / SA_Customer / SA_CompanyMaster, NOT
    // SA_MaskEmail_PreSales. This list is company-wide (not scoped to a
    // single presales rep), matching the real query used elsewhere for
    // this same check. Industry / Country from the original mockup have no
    // backing column here, so they were dropped rather than faked — Plan
    // is real though, backed by ModuleNames (SA_Module, via the active
    // subscription's MenuId).
    public class enPreSalesActiveCustomer
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string FirstContactName { get; set; }
        public string FirstContactEmail { get; set; }
        public string FirstContactNumber { get; set; }
        public string SecondContactName { get; set; }
        public string SecondContactEmail { get; set; }
        public string SecondContactNumber { get; set; }
        public DateTime? CustomerSince { get; set; }
        public string ModuleNames { get; set; }
        public string CountryName { get; set; }
    }

    // The "Companies" page's "Inactive customers — re-engage" tab -- the
    // opposite of Active customers: companies whose subscription HAS
    // expired (no module with ToDate >= today), i.e. former customers with
    // no currently active coverage. Company-wide, not scoped to one rep.
    public class enPreSalesInactiveCustomer
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string FirstContactName { get; set; }
        public string FirstContactEmail { get; set; }
        public string FirstContactNumber { get; set; }
        public string SecondContactName { get; set; }
        public string SecondContactEmail { get; set; }
        public string SecondContactNumber { get; set; }
        public DateTime? CustomerSince { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public string ModuleNames { get; set; }
    }

    // The "Companies" page's "Demoed, not closed — re-target" tab —
    // every SA_MaskEmail_Open row for a company this presales rep already
    // transferred to sales (joined on CorpEmail via SA_MaskEmail_PreSales,
    // scoped to Status='Transferred'). Mirrors SA_MaskEmail_Open's full
    // column set (~40 columns) so the row-click popup can show everything
    // exactly as it exists in that table, not a trimmed-down view. Most
    // columns are kept as plain strings rather than typed/parsed, since
    // this table isn't otherwise used elsewhere in PreSalesNew and several
    // columns (Amount, Expected_Close_Date, RemainingAmount, etc.) have
    // uncertain/mixed real-world formatting — only the three date columns
    // already proven reliable elsewhere in this codebase (RequestedDate,
    // CreatedDate, UpdateDate) are typed as DateTime?.
    public class enPreSalesOpenLeadRow
    {
        public int ID { get; set; }
        public string IsEmail { get; set; }
        public string Name { get; set; }
        public string CorpEmail { get; set; }
        public string TelephoneCountryCode { get; set; }
        public string Telephone { get; set; }
        public string ContactCountryCode { get; set; }
        public string ContactNumber { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string CountryFID { get; set; }
        public string CountryName { get; set; }
        public string StateFID { get; set; }
        public string StateName { get; set; }
        public string IPAddress { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string OutboundLeadSource { get; set; }
        public string CRM_LeadStatus { get; set; }
        public string Status { get; set; }
        public string LeadType { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string IsActive { get; set; }
        public string MaskMail_Id { get; set; }
        public string Lead_status { get; set; }
        public string Set_Follow_up { get; set; }
        public string Amount { get; set; }
        public string Expected_Close_Date { get; set; }
        public string Currency { get; set; }
        public string OpenCreatedDate { get; set; }
        public string LostReason { get; set; }
        public string InvoiceNumber { get; set; }
        public string OpenUpdateddate { get; set; }
        public string Attechments { get; set; }
        public string PaymentTerms { get; set; }
        public string PaymentOption { get; set; }
        public string RemainingPaymentDate { get; set; }
        public string RemainingAmount { get; set; }
    }

    // The "Incentives" page's "Monthly performance" table (and the KPI
    // cards above it, which are just this same list summed client-side by
    // month/quarter) — one row per month, backed by the EXISTING
    // usp_GetSalesLeadStatusReporyPreSales proc (untouched, not authored
    // by this feature). Incentive earned isn't a column on that proc — per
    // the user's explicit rule, it's DemoCount * 500, computed in the view.
    public class enPreSalesMonthlyPerformance
    {
        public string MonthName { get; set; }
        public int YearNo { get; set; }
        public string CreatedBy { get; set; }
        public int TotalLead { get; set; }
        public int TotalOpenLead { get; set; }
        public int LeadTransfer { get; set; }
        public int WonLead { get; set; }
        public int DemoCount { get; set; }
        public int UnusedLead { get; set; }
    }

    // The "Incentives" page's "Demos completed" table — one row per lead
    // that actually has a demo logged (SA_MaskEmail_Open_DemoDetails,
    // deduped to that lead's latest demo, same maskemailopenid grouping
    // the given usp_GetSalesLeadStatusReporyPreSales proc's DemoCount uses)
    // via the new usp_PreSalesNew_GetDemosCompletedList.
    public class enPreSalesDemoCompletedRow
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string CorpEmail { get; set; }
        public string Source { get; set; }
        public DateTime? DemoDate { get; set; }
        public string DemoBySales { get; set; }
        public string Status { get; set; }
    }

    // Incentive KPI card totals (This month / This quarter / Last 6 months)
    // computed entirely server-side by usp_PreSalesNew_GetIncentiveKpiCounts
    // -- the Incentives page and Dashboard used to fetch the FULL demos-
    // completed list to the browser just to count date ranges client-side,
    // which doesn't scale once that list gets into the thousands.
    public class enPreSalesIncentiveKpiCounts
    {
        public int ThisMonth { get; set; }
        public int ThisQuarter { get; set; }
        public int SixMonths { get; set; }
    }

    // One row per month for the "last 6 months" incentive breakdown popup --
    // small and bounded (at most 6 rows) since it's grouped server-side.
    public class enPreSalesIncentiveMonthCount
    {
        public string MonthLabel { get; set; }
        public int Count { get; set; }
    }

    // Monthly "Demos done" count keyed off the demo's ACTUAL date
    // (lastdemodate) rather than the lead's RequestedDate -- used to keep
    // the Monthly Performance table's Demos done / Incentive earned columns
    // consistent with the top Incentive KPI cards, which are also demo-date
    // based. Naturally bounded by number of distinct months.
    public class enPreSalesMonthDemoCount
    {
        public int MonthNo { get; set; }
        public int YearNo { get; set; }
        public int DemoCount { get; set; }
    }

    // True server-side paged/searched row for the "Incentives" page's
    // Monthly Performance table. Wraps the real usp_GetSalesLeadStatusReporyPreSales
    // report (Leads generated/Transferred/Won -- untouched) merged in SQL
    // with the demo-date based Demos done count, so paging/search happens
    // entirely server-side instead of fetching every month to the browser.
    public class enPreSalesMonthlyPerformancePagedRow
    {
        public string MonthName { get; set; }
        public int YearNo { get; set; }
        public int MonthNo { get; set; }
        public int TotalLead { get; set; }
        public int LeadTransfer { get; set; }
        public int WonLead { get; set; }
        public int DemoDateBasedCount { get; set; }
    }

    // The "Incentives" page's Monthly performance row click-through popup
    // — every SA_MaskEmail_PreSales lead this user generated in that
    // month, with its presales status, its linked SA_MaskEmail_Open
    // status (Won/Unused/etc, if transferred), and whether it has a
    // logged demo — so the popup can label each lead Won / Transferred /
    // Demo done / New, via the new usp_PreSalesNew_GetMonthlyPerformanceDetail.
    public class enPreSalesMonthlyLeadDetail
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CorpEmail { get; set; }
        public string CompanyName { get; set; }
        public string OutboundLeadSource { get; set; }
        public string PreSalesStatus { get; set; }
        public DateTime? RequestedDate { get; set; }
        public string OpenStatus { get; set; }
        public bool HasDemo { get; set; }
    }
}
