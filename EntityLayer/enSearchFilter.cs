using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace EntityLayer.CustomEntity
{
    public class enSearchFilter  
    {
        public bool isRegionWise;

        public int? Region { get; set; } 

        public int? Country { get; set; } 

        //Comma seprated values 
        public string Regions { get; set; }
        public string ProcedureName { get; set; }
        
        public int? Menu { get; set; }

        //Comma seprated values 
        public string Countries { get; set; }

        public int? Category { get; set; }

        public int? Product { get; set; }

        public int? ChemicalRangeType { get; set; }

        public int? MarketAnalysisType { get; set; }

        //Comma seprated values
        public string Products { get; set; } 

        public int? Range { get; set; }

        public int? ChartType { get; set; }

        public int? FromYear { get; set; }

        public int? ToYear { get; set; }

        //Comma seprated values 
        public string Quarterly { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
        public DateTime? FromDateTable { get; set; }

        public DateTime? ToDateTable { get; set; }

        public string Type { get; set; }

        public bool IsRegion { get; set; }

        public int PageNumber { get; set; }

        public int StartRowNumber { get; set; }

        public int EndRowNumber { get; set; }

        public bool IsRegionWise { get; set; }

        public string MetaDescription { get; set; }

        public string MarketAnalysisRange { get; set; }

        public string RangeName { get; set; }

        public string ActionType { get; set; }

        public string ModuleName { get; set; }
        
        public string Status { get; set; }

        public string MarketType { get; set; }

        public string Base { get; set; }

        public string TimeStamp { get; set; }

        public string INR { get; set; }

        public string CreatedDate { get; set; }

        public string spnUnitType { get; set; }

        public string Source { get; set; }

        public bool? IsEmail { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public string CompanyName { get; set; }

        public string CountryName { get; set; }

        public string StateName { get; set; }

        public string News { get; set; }

        public string Deal { get; set; }

        public string RangeType { get; set; }

        public string Pids { get; set; }

        public bool IsCountryWise { get; set; }

        public int? Customer { get; set; }

        public string SubscriptionType { get; set; }

        public int UserFID { get; set; }

        public string Causes { get; set; }


        public int? RoleFID { get; set; }

        public string RoleName { get; set; }
        public string Language { get; set; }
        public string searchKeyword { get; set; }
        public string NewsName { get; set; }

        public string MetaTitle { get; set; }
        public string NewsDiscription { get; set; }
        public string MetaDiscription { get; set; }
        public string Keywords { get; set; }

        public string NewsImg { get; set; }
        public int Id { get; set; }

        public int ForecastFlag { get; set; }
        public int? productFID { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductIdBulk { get; set; }
        public int Flag { get; set; }

        //for Api 
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchTerm { get; set; } = "";
        public string UserName { get; set; }
        //public string UserIpAddress { get; set; }
        public string AuthCode { get; set; }
        public string ProductName { get; set; }
        public string ValidationMessage { get; set; }

        public string UserIPAddress { get; set; }
        public string Local_IP_Address { get; set; }
        public string LatLong { get; set; }
        public string CustomerCountry { get; set; }
        public string City { get; set; }
        public int Yearly { get; set; }
        public int Monthly { get; set; }
        public int Weekly { get; set; }
        public int Daily { get; set; }
        public bool IsActive { get; set; }
        public int ValidYearFrom { get; set; }
        public int ValidYearTo { get; set; }
        public string ProductsId { get; set; }
        public int LeadId { get; set; }
        public int RangeID { get; set; }

        public string UserType { get; set; }
        public string Credit { get; set; }
        public string Year { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public string INREuro { get; set; }
        public string INRKRW { get; set; }
        public string INRYEN { get; set; }

        public string Mobile { get; set; }

        public string MonthofMonth { get; set; }
        public string CountryRegionWise { get; set; }
        public string CategoryName { get; set; }
        public string Currency { get; set; }

         public int CrmId { get; set; }

        public string UserStatus { get; set; }
        public string FilterType { get; set; }
    }
}
