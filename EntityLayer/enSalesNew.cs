using System;

namespace EntityLayer
{
    // Entity layer for the new SalesNewController "My Day" dashboard.
    // Every widget on this page is backed by SA_MaskEmail_Open (the Sales
    // side's lead table), scoped to the logged-in sales rep (CreatedBy),
    // same pattern as EntityLayer.enPreSalesNew / dlPreSalesDashboard for
    // the PreSales side. Kept as its own file/class set rather than reused
    // from PreSales, since this is a different table (SA_MaskEmail_Open,
    // not SA_MaskEmail_PreSales) with a different owner (Sales, not
    // PreSales).

    // The 6 top KPI cards, computed and counted entirely in SQL.
    public class enSalesKpiSummary
    {
        public int OpenLeadsCount { get; set; }
        public decimal PipelineValue { get; set; }
        public int ClosingThisMonthCount { get; set; }
        public decimal ClosingThisMonthAmount { get; set; }
        public int OverdueFollowupsCount { get; set; }
        public decimal WinRatePercent { get; set; }
    }

    // "Follow-ups due today" table row.
    public class enSalesFollowupDueRow
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CorpEmail { get; set; }
        public string Status { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
    }

    // "Demos in progress" list row.
    public class enSalesDemoInProgressRow
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CorpEmail { get; set; }
        public string CountryName { get; set; }
        public string Status { get; set; }
        public DateTime? LastDemoDate { get; set; }
    }

    // "Pipeline by stage" bar chart — one row per Status.
    public class enSalesPipelineStageCount
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    // "Pipeline value & forecast" chart — one row per year/month/currency.
    // Bar (Committed) only, no weighted-forecast line -- see delivery notes.
    public class enSalesPipelineForecastRow
    {
        public int YearNo { get; set; }
        public string MonthName { get; set; }
        public string Currency { get; set; }
        public decimal TotalAmount { get; set; }
    }

    // "Won vs lost" chart — one row per status/year/month.
    public class enSalesWonLostRow
    {
        public string Status { get; set; }
        public int YearNo { get; set; }
        public string MonthName { get; set; }
        public int Count { get; set; }
    }

    // "Leads by source" donut — one row per source, with its share of the
    // total already computed server-side.
    public class enSalesLeadSourceRow
    {
        public string Source { get; set; }
        public int SourceCount { get; set; }
        public int TotalCount { get; set; }
        public decimal LeadPercentage { get; set; }
    }

    // Single-lead detail page (SalesNew/LeadDetail).
    public class enSalesLeadDetail
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CorpEmail { get; set; }
        public string ContactCountryCode { get; set; }
        public string ContactNumber { get; set; }
        public string CountryName { get; set; }
        public string Status { get; set; }
        public string LeadStatus { get; set; }
        public string LeadType { get; set; }
        public string Source { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
        public DateTime? ExpectedCloseDate { get; set; }
        public DateTime? SetFollowUp { get; set; }
        public string CreatedBy { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string PaymentOption { get; set; }
        public string PaymentTerms { get; set; }
        public string LostReason { get; set; }
    }

    // "Activity & follow-ups" tab row -- one row per logged follow-up,
    // backed by the real SA_MaskEmailOpen_LastFollowUpDiscussionSales table.
    public class enSalesActivityRow
    {
        public int Id { get; set; }
        public string Discussion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string DiscussionStatus { get; set; }
        public string FollowupType { get; set; }
        public DateTime? SetFollowUp { get; set; }
        public int RowNumber { get; set; }
        public int RecordsCount { get; set; }
    }

    // "Contacts" tab row -- the lead's own primary contact (from
    // SA_MaskEmail_Open) plus any additional contacts (from the new
    // SA_MaskEmail_OpenContact table), unioned and flagged server-side.
    public class enSalesLeadContactRow
    {
        public bool IsPrimary { get; set; }
        public string Name { get; set; }
        public string CorpEmail { get; set; }
        public string ContactCountryCode { get; set; }
        public string ContactNumber { get; set; }
        public string Designation { get; set; }
    }

    // "Pipeline" board card -- one row per lead, used across the New /
    // Contacted / Demo / Won / Lost lists. New comes from SA_MaskEmail
    // (fewer columns available -- Amount/Currency/SetFollowUp will be
    // null there), the rest come from SA_MaskEmail_Open.
    public class enSalesPipelineCardRow
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CorpEmail { get; set; }
        public string CountryName { get; set; }
        public string Status { get; set; }
        public string LeadType { get; set; }
        public string Source { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
        public DateTime? SetFollowUp { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    // "Pipeline" List tab row -- combined New (SA_MaskEmail) + everything
    // else (SA_MaskEmail_Open), backend-driven paging (RowNumber/
    // RecordsCount), same convention as enSalesActivityRow.
    // "Lost-reason analysis" report row -- SA_MaskEmail_Open leads with a
    // LostReason set, backend-driven paging (RowNumber/RecordsCount), same
    // convention as enSalesActivityRow / enSalesPipelineListRow.
    public class enSalesLostReasonRow
    {
        public string Name { get; set; }
        public string CorpEmail { get; set; }
        public string CompanyName { get; set; }
        public string LostReason { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int RowNumber { get; set; }
        public int RecordsCount { get; set; }
    }

    // "Orders & invoices" -> Sales Order Forms table (Invoice/
    // ChemAnalystProformaInvoiceList.cshtml) -- SA_MaskEmail_Open leads that
    // have an uploaded Sales Order Form attachment, scoped to the logged-in
    // sales rep (CreatedBy), backend-driven paging (RowNumber/RecordsCount),
    // same convention as enSalesLostReasonRow.
    public class enSalesOrderFormRow
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string Signatory { get; set; }
        public decimal? Amount { get; set; }
        public string PaymentOption { get; set; }
        public string Attechments { get; set; }
        public int RowNumber { get; set; }
        public int RecordsCount { get; set; }
    }

    // "New subscription" popup (Sales role only - SalesManagement/
    // SalesIndex.cshtml) -- Won leads (SA_MaskEmail_Open) for the logged-in
    // sales rep that don't have a company/subscription yet. Unpaged, same
    // convention as enSalesPipelineCardRow/RunPipelineProc - this list
    // should stay small.
    public class enSalesWonLeadRow
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CorpEmail { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
    }

    public class enSalesPipelineListRow
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CorpEmail { get; set; }
        public string CountryName { get; set; }
        public string Source { get; set; }
        public string LeadType { get; set; }
        public string Status { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
        public DateTime? ExpectedCloseDate { get; set; }
        public DateTime? SetFollowUp { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int RowNumber { get; set; }
        public int RecordsCount { get; set; }
    }
}
