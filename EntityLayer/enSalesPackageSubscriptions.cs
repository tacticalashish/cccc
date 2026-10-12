using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.CustomEntity;
using Utility;
using Newtonsoft.Json;

namespace EntityLayer
{
    public class enSalesPackageSubscriptions
    {
        public int ID { get; set; }
        public bool IsSelected { get; set; }
        public string ProductID { get; set; }

        public string MenuID { get; set; }

        public string MenuView { get; set; }

        public int? LeadID { get; set; }

        public int? UserID { get; set; }

        public int? SubscriptionType { get; set; }

        public string SubscriptionTypeView { get
            {
                string result = string.Empty;
                if (SubscriptionType.HasValue && SubscriptionType.Value == 1)
                {
                    result = "Free trail";
                }
                else if (SubscriptionType.HasValue && SubscriptionType.Value == 2)
                {
                    result = "Package";
                }

                return result;
            } }

        public DateTime? CreatedDate { get; set; }

        public string CreatedDateView { get { return CreatedDate.HasValue ? CreatedDate.Value.ToString("dd/MMM/yy") : string.Empty; } }

        public string Status { get; set; }

        public string StatusView
        {
            get
            {
                string result = Status;
                if (ToDate.HasValue && ToDate.Value.Date < DateTime.Now.Date)
                    result = result + " (Expired)";
                return result;
            }
        }

        public string Description { get; set; }

        public DateTime? FromDate { get; set; }
        //public string FromDateView { get { return FromDate.Value.ToString("dd/MMM/yyyy"); } }
        public DateTime? ToDate { get; set; }
        public string ToExpire { get; set; }
        //public string ToDateView { get { return ToDate.Value.ToString("dd/MMM/yyyy"); } }

        //public string ExpireDateRangeView { get { return string.Concat("From ", FromDate.Value.ToString("dd/MMM/yyyy"), " To ", ToDate.Value.ToString("dd/MMM/yyyy")); } }

        public string RegionsID { get; set; }

        public string CountriesID { get; set; }

        public string ChemicalPricingRange { get; set; }

        public string CompanyName { get; set; }

        public bool IsSectoral { get; set; }

     

    

        public string MarketAnalysisRange { get; set; }

        public string Email { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

   

    

        public int? CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string RegionName { get; set; }

        public string RegionNameView { get { return Helper.ValIfNullOrEmpty(RegionName, true, string.Empty); } }

        public string CountryName { get; set; }

        public string CountryNameView { get { return Helper.ValIfNullOrEmpty(CountryName, true, string.Empty); } }

        public int RowNumber { get; set; }
        public int CommodityCount { get; set; }
        public int DisruptionCount { get; set; }
        public int EximCount { get; set; }
        public int FreightIndexCount { get; set; }
        public int MarketAnalysisCount { get; set; }
        public int ApiCount { get; set; }


        public int RecordsCount { get; set; }

        public string IDs { get; set; }

        public string Name { get; set; }

        public string CorpEmail { get; set; }

        public string Organisation { get; set; }

        public string InterestArea { get; set; }

        public string UserType { get; set; }
        public string Language { get; set; }
        public int CustNotificationFlag { get; set; }

        public string ProductName { get; set; }
        public string FromDateView { get { return FromDate.HasValue ? FromDate.Value.ToString("dd/MMM/yyyy") : string.Empty; } }

        public string ToDateView { get { return ToDate.HasValue ? ToDate.Value.ToString("dd/MMM/yyyy") : string.Empty; } }

        public DateTime? ExpireDateRange { get; set; }

        public string ExpireDateRangeView { get { return ExpireDateRange.HasValue ? string.Concat("From ", FromDate.Value.ToString("dd/MMM/yyyy"), " To ", ToDate.Value.ToString("dd/MMM/yyyy")) : string.Empty; } }

        public string Chemical_Pricing_Countries { get; set; }
        public string MarketAnalysis_RegionName { get; set; }
        public string MarketAnalysis_Countries { get; set; }
        public string Capacity_Tracker { get; set; }
        public string Deals_Countries { get; set; }
        public string News_Countries { get; set; }
        public string Phone { get; set; }
        public int? NumberOfSubscription { get; set; }

        public string FromDate_Date { get; set; }

        public string ToDate_Date { get; set; }

        public string Credit { get; set; }
        public string DownloadType { get; set; }
        public string Subscribe { get; set; }
        public string Package { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Range { get; set; }
        public int? CompanyPrimaryId { get; set; }
        public string ValidationMessage { get; set; }
        public string Productcompare { get; set; }
        public string CountryRegion { get; set; }
        public string isRegionCountry { get; set; }
        public string QuarterlyCompare { get; set; }
        public string CategoryId { get; set; }

        public string UserStatus { get; set; }
    }
}
