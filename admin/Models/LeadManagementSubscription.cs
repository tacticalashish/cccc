using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class LeadManagementSubscription
    {
        public int Id { get; set; }
        public string Name { get; set; }
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

        public string AssignTo { get; set; }
        public string Designation { get; set; }
        public DateTime? AssignDate { get; set; }
        public string IPAddress { get; set; }


        public string BillingEmail { get; set; }
        public string SubscriptionType { get; set; }
        public int AssignAnalyst { get; set; }
        public string SecondaryClintEmail { get; set; }
        public string SubscriptionDateFrom { get; set; }
        public string SubscriptionDateTo { get; set; }
        public string Subscription_Country { get; set; }
        public string Subscription_Product { get; set; }
        public string Chemical_Range { get; set; }
        public string Market_Analysis_Range { get; set; }
        public string SubSubscriptionRemark { get; set; }
        public string ProcedureName { get; set; }
        public int ConnectionTime { get; set; }
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int MarketRangeID { get; set; }
        public string MarketRangeName { get; set; }
        public string files { get; set; }
        public string PaymentMode { get; set; }

        public string CRM { get; set; }

        public string Trader { get; set; }
        public string Credit { get; set; }
        public string Renew { get; set; }
    }
}