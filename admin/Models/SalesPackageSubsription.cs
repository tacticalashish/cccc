using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Models
{
    public class SalesPackageSubscription 
    {
        public int Id { get; set; }
        public int CustNotificationFlag { get; set; }
        public string ProductId { get; set; }

        public string RegionsID { get; set; }

        public string CountriesID { get; set; }

        public string ChemicalPricingRange { get; set; }

        public string MarketAnalysisRange { get; set; }

        public string MenuId { get; set; } 

        public int LeadId { get; set; }

        public int UserId { get; set; }

        public int SubscriptionType { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
        public int ExpiryNotification { get; set; }
        
    }
}
