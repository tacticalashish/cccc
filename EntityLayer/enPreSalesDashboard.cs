using System;

namespace EntityLayer
{
    // Entities backing the new PreSalesDashboard page (PreSalesDashboardController
    // in the admin project). Every field here is computed from real data in
    // SA_MaskEmail_PreSales / SA_MaskEmail_PreSalesFollowup /
    // SA_MaskEmail_PreSales_LogActivity, scoped to the logged-in presales user
    // (CreatedBy). Anything the dashboard mockup shows that has no backing
    // table (incentive amounts, recently-credited names, demo-done/won stages)
    // is intentionally NOT modeled here — those stay static in the view.
    public class enPreSalesDashboardStats
    {
        public int LeadsGenerated { get; set; }
        public int DemosFixed { get; set; }
        public int FollowUpsDue { get; set; }
        public int PendingTransfer { get; set; }
        public int AwaitingDemo { get; set; }
    }

    public class enPreSalesSourceCount
    {
        public string Source { get; set; }
        public int Count { get; set; }
    }

    public class enPreSalesFollowupRow
    {
        public int ID { get; set; }
        public int LeadID { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string Note { get; set; }
        public string LeadName { get; set; }
        public string CompanyName { get; set; }
    }

    public class enPreSalesTransferredRow
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string OutboundLeadSource { get; set; }
        public string MailChain { get; set; }
        public string Status { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class enPreSalesWorkTodayRow
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CorpEmail { get; set; }
        public string OutboundLeadSource { get; set; }
        public string Status { get; set; }
        public string LastOutcome { get; set; }
    }

    // Backed by the real demo/won tracking tables (SA_MaskEmail_Open +
    // SA_MaskEmail_Open_DemoDetails, joined via CorpEmail) instead of the
    // presales-side Activity log, per the actual usp used elsewhere in the
    // system for this same report.
    public class enPreSalesMonthDemo
    {
        public string MonthName { get; set; }
        public int YearNo { get; set; }
        public int DemoCount { get; set; }
        public int WonLead { get; set; }
    }

    public class enPreSalesFunnel
    {
        public int Generated { get; set; }
        public int Contacted { get; set; }
        public int DemoAgreed { get; set; }
        public int Transferred { get; set; }
    }

    // Generic row for the "click a KPI card to see the leads behind it"
    // popups (Leads generated / Demos fixed / Pending transfer). Follow-ups
    // due and Awaiting demo reuse enPreSalesFollowupRow / enPreSalesTransferredRow
    // instead, since those lists already existed.
    public class enPreSalesStatDetailRow
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public DateTime? DetailDate { get; set; }
        public string DetailNote { get; set; }
    }
}
