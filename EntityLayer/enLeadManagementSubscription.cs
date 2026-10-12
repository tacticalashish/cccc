using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enLeadManagementSubscription
    {
        public int CrmId;

        public int Id { get; set; }
        public string Name { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        public string CountryOrRegionNames { get; set; }
        public string CorpEmail { get; set; }
        public string Organisation { get; set; }
        public string CountryName { get; set; }
        public int CountryID { get; set; }
        public string StateName { get; set; }
        public int StateId { get; set; }
        public string Phone { get; set; }
        public string module { get; set; }

        public string InterestArea { get; set; }
        public string UserType { get; set; }
        public string Subject { get; set; }
        public string Query { get; set; }
        public string Status { get; set; }
        public int Subscribe { get; set; }

        public string AssignTo { get; set; }
        public string Designation { get; set; }
        public DateTime? AssignDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string IPAddress { get; set; }

        public int RowNumber { get; set; }

        public int RecordsCount { get; set; }
        public int ExpiryNotification { get; set; }

        public string BillingEmail { get; set; }
        public string SubscriptionType { get; set; }
        public string AssignAnalyst { get; set; }
        public int AssignAnalystId { get; set; }

        public string SecondaryClintEmail { get; set; }
        public string SubscriptionDateFrom { get; set; }
        public string SubscriptionDateTo { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Subscription_Country { get; set; }
        public string Subscription_Product { get; set; }
        public int Chemical_RangeID { get; set; }
        public string Chemical_Range { get; set; }

        public int UserId { get; set; }

        public string Market_Analysis_Range { get; set; }
        public string SubSubscriptionRemark { get; set; }

        public string ProcedureName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ConnectionTime { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public int StepCounter { get; set; }

        public string RegionId { get; set; }

        public string RegionName { get; set; }

        public int ModuleId { get; set; }
        public string ModuleName { get; set; }

        public int MarketRangeID { get; set; }
        public string MarketRangeName { get; set; }
        public string ValidationMessage { get; set; }

        public string createdby { get; set; }

        public string Password { get; set; }
        public string ApprovedBy { get; set; }
        public string DisapprovedRemark { get; set; }

        public string CompanyName { get; set; }
        public int CompanyId { get; set; }

        public string UpdateProductsName { get; set; }
        public string UpdatedModulesName { get; set; }

        public string files { get; set; }
        public string PaymentMode { get; set; }
        public string CRM { get; set; }
        public int LeadId { get; set; }
        public string ClintCountry { get; set; }

        public string Invoice { get; set; }
        public string Amount { get; set; }
        public string PaymentDate { get; set; }

        public int chkboxPrimaryId { get; set; }

        public string Trader { get; set; }
        public string Credit { get; set; }
        public string Renew { get; set; }

        //--------------------------------------------------
        //----------------For New Module-----------------------
        public string DiscriptionMessage { get; set; }
        public string OutboundLeadSource { get; set; }
        public string LeadType { get; set; }
        public DateTime? RequestedDate { get; set; }

        public DateTime? SetFollowUp { get; set; }
        public DateTime? ExpectedCloseDate { get; set; }
        public string Currency { get; set; }

        public string AssignSalesEmail { get; set; }
        public string LostReason { get; set; }
        public string PaymentTerms { get; set; }
        public string PaymentOption { get; set; }
        public DateTime? RemainingPaymentDate { get; set; }
        public string CRMLeadStatus { get; set; }

        // CRM Pipeline page (LeadManagementSubscription/Pipeline.cshtml) -
        // which of the four columns this lead belongs in: "New", "Open",
        // "In discussion" or "Won - onboarding". Computed in the stored
        // proc, not stored anywhere.
        public string Stage { get; set; }
        public string Last_Followup_Discussion { get; set; }
        public int SAMaskEmail_OpenID { get; set; }

        public string Mobile { get; set; }
        public string DiscussionStatus { get; set; }
        public string RemainingAmount { get; set; }
        public string CCperson { get; set; }


        public int ReportTo { get; set; }
        public string Title { get; set; }
        public int UniqID { get; set; }


        public string ContactCountryCode { get; set; }
        public string TelephoneCountryCode { get; set; }


        public int TotalLead { get; set; }
        public int TotalOpenLead { get; set; }
        public int OpenLead { get; set; }
        public int LostLead { get; set; }
        public int WonLead { get; set; }
        public int FollowUpLead { get; set; }
        public int noFollowUpLead { get; set; }
        public int TotalInvoice { get; set; }
        public string TotalAmount { get; set; }
        public DateTime? LastDemoDate { get; set; }

        public string DemoDiscussion { get; set; }

        public int LeadTransfer { get; set; }
        public int DemoCount { get; set; }
        public int NewDemoCount { get; set; }
        public int UnusedCount { get; set; }
        public string Month { get; set; }

        public string Year { get; set; }

        public string MailChain { get; set; }

        public string CRMEmail { get; set; }


        public int INRInvoice { get; set; }
        public string INRTotalInvoiceAmount { get; set; }
        public int USDInvoice { get; set; }
        public string USDTotalInvoiceAmount { get; set; }
        public int WONInvoice { get; set; }
        public string WONTotalInvoiceAmount { get; set; }
        public int YENInvoice { get; set; }
        public string YENTotalInvoiceAmount { get; set; }
        public int EUROInvoice { get; set; }
        public string EUROTotalInvoiceAmount { get; set; }

        public string ProductType { get; set; }
        public string LeadMessage { get; set; }


    }
    public class enAllclientAllSubs
    {
        public string CorpEmail { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string PrimarySecondary { get; set; }
        public string AccountStatus { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ModuleName { get; set; }
        public string ProductName { get; set; }
        public string CountryRegion { get; set; }
        public string ClintType { get; set; }
        public string ApiAccess { get; set; }
        public int RowNumber { get; set; }
        public string ClientStatus { get; set; }
        public int RecordsCount { get; set; }
        public string SubcriptionEndate { get; set; }




    }
    public class enUsersDetails
    {
        public string ActiveCustomer { get; set; }
        public string Expthirtydays { get; set; }
        public string PaymentsPending { get; set; }
        public string inActivesixtydays { get; set; }
        public string opentickets { get; set; }
        public string ThisMonthAdd { get; set; }
        public string Overdues { get; set; }
        public string Awaitingreply { get; set; }

        public string LeadStatus { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }


        public string CorpEmail { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }

    }
    public class enUsersDetailsbyCompany
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Renewaldate { get; set; }
        public string FirstContactEmail { get; set; }
        public string PrimaryContact { get; set; }
        public string Status { get; set; }
        public string ModuleName { get; set; }
        public string api { get; set; }
        public System.DateTime? RenewsDate { get; set; }
        public string ApiStatus { get; set; }
        public string Health { get; set; }
        public long RowNumber { get; set; }
        public long RecordsCount { get; set; }

        public System.DateTime? LastLogin { get; set; }
    }
    public class enCustomerDetailByCompany
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string FirstContactEmail { get; set; }
        public string PrimaryContact { get; set; }
        public string Status { get; set; }
        public string ModuleName { get; set; }
        public string api { get; set; }
        public string RenewsDate { get; set; }
        public string ApiStatus { get; set; }
        public int? Activeseats { get; set; }
        public string AssignedCSM { get; set; }
        public string Assignedanalyst { get; set; }
        public string LastLogin { get; set; }
        public string Created { get; set; }
        public string Currenttermends { get; set; }
        public string Health { get; set; }
    }
    public class enReminder
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string ReminderType { get; set; }   // "Call" or "Email"
        public string Note { get; set; }
        public System.DateTime ReminderDate { get; set; }
        public System.TimeSpan? ReminderTime { get; set; }
        public bool ShowOnMyDay { get; set; }
        public bool IsCompleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
    public class enUserContact
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }     // "Primary" or "Secondary"
        public string Status { get; set; }   // "Active" or "Inactive"
        public System.DateTime? LastLogin { get; set; }
        public long RowNumber { get; set; }
        public long RecordsCount { get; set; }
    }
    public class enSubscriptionItem
    {
        public string Email { get; set; }
        public string ModuleName { get; set; }
        public string ProductName { get; set; }
        public string Role { get; set; }     // "Primary" or "Secondary"
        public string Status { get; set; }   // "Active" or "inactive"
        public long RowNumber { get; set; }
        public long RecordsCount { get; set; }
    }
    public class enLoginRecord
    {
        public string UserName { get; set; }
        public System.DateTime? Login { get; set; }
        public System.DateTime? Logout { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public string DeviceType { get; set; }
        public long RowNumber { get; set; }
        public long RecordsCount { get; set; }
    }

    public class enUsageSummary
    {
        public int LoginsThisMonth { get; set; }
        public int LoginsThisQuarter { get; set; }
        public int LoginsThisYear { get; set; }
        public int ActiveUserCount { get; set; }
    }

    public class enMonthlyLoginCount
    {
        public string MonthLabel { get; set; }
        public System.DateTime MonthStart { get; set; }
        public int LoginCount { get; set; }
    }

    public class enProductViewCount
    {
        public string ProductName { get; set; }
        public int ViewCount { get; set; }
        // Combined ("Top 10 most-viewed") breakdown per tracker.
        public int PricingViews { get; set; }
        public int DisruptionViews { get; set; }
        public int MarketAnalysisViews { get; set; }
        public int TotalViews { get; set; }
    }

    public class enSupportTicket
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime? UpdatedDate { get; set; }
        public int? Rating { get; set; }
        public string Subject { get; set; }
        public long RowNumber { get; set; }
        public long RecordsCount { get; set; }

        public string RoleName { get; set;}
    }

    public class enSupportTicketComment
    {
        public int ID { get; set; }
        public int? UserFID { get; set; }
        public int? ManagerFID { get; set; }
        public string Comment { get; set; }
        public string FileName { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
    public class enProformaInvoiceSummary
    {
        public int ID { get; set; }
        public string InvoiceNo { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Currency { get; set; }
    }
    public class enTodayReminder
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ReminderType { get; set; }
        public string Note { get; set; }
        public string ReminderTime { get; set; }
    }
    public class enWorkSpaceFile
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public long RowNumber { get; set; }
        public long RecordsCount { get; set; }
    }

    public class enRecommendationSignals
    {
        public string TopDisruptionProduct { get; set; }
        public int? TopDisruptionViews { get; set; }
        public bool AlreadySubscribed { get; set; }

        public int TotalSeats { get; set; }
        public int ActiveSeats { get; set; }
        public string InactiveUserName { get; set; }
        public int? InactiveDays { get; set; }

        public int OpenTicketCount { get; set; }
        public int? OldestOpenTicketDays { get; set; }

        public int? DaysSinceLastDeliverable { get; set; }

        public int? LoginsThisMonth { get; set; }
        public int? LoginsLastMonth { get; set; }

        public int StaleReminderCount { get; set; }
    }

    public class enNeedsAttentionItem
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string PrimaryContact { get; set; }
        public System.DateTime? RenewsDate { get; set; }
        public int? DaysToRenewal { get; set; }
        public System.DateTime? LastLogin { get; set; }
        public int? DaysInactive { get; set; }
        public string Reason { get; set; }   // "expired" | "renewal_due" | "inactive"
    }

    public class enRenewalThisQuarter
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public System.DateTime? RenewsDate { get; set; }
        public string Status { get; set; }   // "overdue" | "due"
    }
    public class enLabelCount
    {
        public string Label { get; set; }
        public int CustomerCount { get; set; }
    }
    public class enMonthlyAddedLost
    {
        public int Yr { get; set; }
        public int Mo { get; set; }
        public int AddedCount { get; set; }
        public int LostCount { get; set; }
    }
    public class enMonthlyRevenue
    {
        public int Yr { get; set; }
        public int Mo { get; set; }
        public decimal RenewalRevenue { get; set; }
        public decimal RevenueLost { get; set; }
    }
    public class enMonthlyAvg
    {
        public int Yr { get; set; }
        public int Mo { get; set; }
        public decimal AvgRevenue { get; set; }
        public decimal AvgUsers { get; set; }
    }
    public class enAllTicket
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime? UpdatedDate { get; set; }
        public string Subject { get; set; }
        public long RowNumber { get; set; }
        public long RecordsCount { get; set; }
        public string RoleName { get; set; }

    }

    // One follow-up / discussion row from SA_MaskEmailOpen_LastFollowUpDiscussionCRM
    public class enCRMFollowUp
    {
        public int Id { get; set; }
        public int OpenId { get; set; }
        public string Discussion { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public string DiscussionStatus { get; set; }
        public System.DateTime? SetFollowUp { get; set; }
    }

    // One contact row for the CRM lead-detail Contacts tab
    // (SA_Customer joined to SA_CompanyMaster by company name).
    public class enCRMContact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Usertype { get; set; }
    }
}