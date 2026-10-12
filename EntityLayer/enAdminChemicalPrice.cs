using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
   public  class enAdminChemicalPrice
    {
        
        public int ID { get; set; }

        public int ParentProductId { get; set; }
        public string Devicetype { get; set; }
        public int ProductChildId { get; set; }
        public int? Product { get; set; }

        public string ProductVariant { get; set; }
        public string ProductName { get; set; }

        public string ProductVariantView { get { return ProductVariant + " (" + Country + ")"; } }

        public string Type { get; set; }
        public DateTime? FromDate { get; set; }      
        public DateTime? ToDate { get; set; }

        public string Year { get; set; }

        public string Week { get; set; }

        public string Date { get; set; }

        public decimal? Count { get; set; }
  
        public decimal InsertCount { get; set; }

        public decimal InsertMin { get; set; }
        public decimal InsertMax { get; set; }
        public decimal InsertMinUSD { get; set; }
        public decimal InsertMaxUSD { get; set; }

        public decimal InsertMinEURO { get; set; }
        public decimal InsertMaxEURO { get; set; }
        public decimal InsertMinYEN { get; set; }
        public decimal InsertMaxYEN { get; set; }
        public decimal InsertMinWON { get; set; }
        public decimal InsertMaxWON { get; set; }

        public string Discription { get; set; }

        public string FileName { get; set; }

        public DateTime? CreatedDate { get; set; }
         public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }

        public string Country { get; set; }


        public string Region { get; set; }
        public string RegionWise { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public decimal? MinUSD { get; set; }

        public float? USD { get; set; }

        public decimal InsertUSD { get; set; }


        public decimal InsertEURO { get; set; }
        public decimal InsertYEN { get; set; }
        public decimal InsertWON { get; set; }

        public float? EURO { get; set; }
        public float? YEN { get; set; }
        public float? WON { get; set; }


        public decimal? MinYEN { get; set; }

        public decimal? MinWON { get; set; }

        public decimal? MinEURO { get; set; }

        public decimal? MaxYEN { get; set; }

        public decimal? MaxWON { get; set; }

        public decimal? MaxEURO { get; set; }

        public decimal? MaxUSD { get; set; }

        public string RangeName { get; set; }

        public string ValidationMessage { get; set; }

        public string Month { get; set; }

        public string Location { get; set; }
        public string Day { get; set; }

        public string ModeOfDistrubution { get; set; }
        public int RowNumber { get; set; }

        public int RecordsCount { get; set; }

        public float? CountPercentage { get; set; }

        public float? SavePercentage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int ChildId { get; set; }
        public int ParentId { get; set; }
        public string Source { get; set;}
        public int Flag { get; set;}
        public string ApprovedBy { get; set; }
        public string DisapprovedBy { get; set; }
        
        public string Title { get; set; }
        public int ReportTo { get; set; }
        public int UniqID { get; set; }
        public string DisapprovedMessage { get; set; }

        public string Remarks { get; set; }

        public int? FromYearCompare { get; set; }

        public int? ToYearCompare { get; set; }

        public DateTime? fromDateCompare { get; set; }
        public string fromDateCompares { get; set; }
        public DateTime? toDateCompare { get; set; }
        public string toDateCompares { get; set; }

        public string Countries { get; set; }

        public string Products { get; set; }

        public string NewsName { get; set; }

        public string NewsDiscription { get; set; }

        public string SourceDesc { get; set; }

        public string QueryType { get; set; }

        public string ProductReview { get; set; }

        public string MarketSituation { get; set; }
        public string Demand { get; set; }
        public string Supply { get; set; }
        public string PlantShutdown { get; set; }
        public string AdditionalRemarks { get; set; }
        public string MetaDescription { get; set; }
        public string Meta { get; set; }
        public string Producers { get; set; }
        public string Language { get; set; }

        public string CountryName { get; set; }
        public string spnUnitType { get; set; }

        public decimal? Change_percentage { get; set; }
        public DateTime? OnDate { get; set; }

        public string DDlCountry { get; set; }
        public string DDlProductVariant { get; set; }
        public int? DDlProduct { get; set; }

        public string DDlRegionWise { get; set; }

        public string DDlType { get; set; }
        public int? ProductId { get; set; }
        public int regionID { get; set; }
        public int categoryID { get; set; }
        public int CountryID { get; set; }

        //-------------MES----------------------
        public string hsCode { get; set; }
        public string ImportingCountry { get; set; }
        public string PortOfDestination { get; set; }
        public string SupplierName { get; set; }
        public string CountryOfOrigin { get; set; }
        public string PortOfOrigin { get; set; }
        public string QtyInMT { get; set; }
        public string UnitRateinUSDKg { get; set; }
        public string Incoterms { get; set; }
        public string ShipmentMode { get; set; }
        public string ConsigneeName { get; set; }
        public string CountryofImport { get; set; }
        public string UnitRate { get; set; }
        public string CustomerName { get; set; }
        public string Grade { get; set; }
        public string Weight { get; set; }
        public string ExporterName { get; set; }
        public string ExportCountry { get; set; }
        public string ProductDiscription { get; set; }
        public string searchKeyword { get; set; }
        public int StartRowNumber { get; set; }
        public int EndRowNumber { get; set; }
        public int PageNumber { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }

        public string ParentProductName { get; set; }
        public string ProductChildName { get; set; }


        //-------------------------------
        public string UnitName { get; set; }
        public string Weekly { get; set; }
        public string Monthly { get; set; }
        public string Yearly { get; set; }
        public string DailyBulk { get; set; }
        public string DailyRetail { get; set; }
        public string ForCast { get; set; }

        public int rangeFID { get; set; }
        public string TriggerStatus { get; set; }
        public string Currency { get; set; }
        public string CurrentPrice { get; set; }

        public string SetPrice { get; set; }
        public string TriggeredPrice { get; set; }

        public string Name { get; set; }
        public string UserPassword { get; set; }
        public string Phone { get; set; }

        public int IsActive { get; set; }

        public string Feedback { get; set; }

        public int Rating { get; set; }

        public int NewsId { get; set; }
        public int ReportId { get; set; }
        public string Counts { get; set; }
        public int Price { get; set; }
        public string Amount { get; set; }
        public string IncentiveAmount { get; set; }

        public int RowId { get; set; }
        public string Tittle { get; set; }


        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string Company { get; set; }
        public string Plant { get; set; }

        public int EmpParentId { get; set; }
        public string CFRCountry { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }

        public int Status { get; set; }
        public string Qty { get; set; }
        public string ValidityDate { get; set; }
        public string PriceIncoterm { get; set; }
        public string Incoterm { get; set; }
        public string Participants { get; set; }
        public string CollectedDate { get; set; }
        public string Quaterly { get; set; }
        public string Description { get; set; }
        public string Ques1 { get; set; }
        public string Ques2 { get; set; }
        public string Ques3 { get; set; }
        public string Ques4 { get; set; }
        public string Ans1 { get; set; }
        public string Ans2 { get; set; }
        public string Ans3 { get; set; }
        public string Ans4 { get; set; }


        public decimal? AICount { get; set; }
        public decimal? AIMin { get; set; }
        public decimal? AIMax { get; set; }
    }

    public class enCFRPrice
    {
        public string Grade { get; set; }
        public string Currency { get; set; }
        public string Counts { get; set; }
        public string CountryName { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }

    public class enPriceCatalog
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string Country { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Tittle { get; set; }
        public string FileName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int? ProductId { get; set; }
        public int CountryID { get; set; }
        public int RowNumber { get; set; }
        public int RecordsCount { get; set; }
        public string CountryName { get; set; }

    }


    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
    }

    public class GetCRMDetails
    {
        public string CRMName { get; set; }
        public string CRMEmail { get; set; }

        public string PhoneNo { get; set; }
    }


    public class OfferPrice
    {

        public string ProductName { get; set; }
        public string CountryName { get; set; }
        public string Date { get; set; }
        public string PriceIncoterm { get; set; }
        public string Grade { get; set; }
        public string Qty { get; set; }
        public string ValidityDate { get; set; }
        public string CFRCountry { get; set; }
        public string Participants { get; set; }
        public string Type { get; set; }
        public string Incoterm { get; set; }

    }


}

