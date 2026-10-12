using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.ViewModel
{
    public class AdminMarketAnalysis
    {
    }
    public class Capacity_By_Company
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string Company { get; set; }
        public string Year { get; set; }
        public decimal  Count { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string SourceDesc { get; set; }

        public string Source { get; set; }

    }
    public class Capacity_By_Location
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string State { get; set; }
        public string Year { get; set; }
        public decimal Count { get; set; }
        public string SourceDesc { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }

    }
    public class Capacity_By_Technology
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string Company { get; set; }
        public string Technology { get; set; }
        public string Liscensor { get; set; }
        public string Year { get; set; }
        public decimal Count { get; set; }
        public string SourceDesc { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }

    }
    public class Capacity_By_Process
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string Company { get; set; }
        public string Process { get; set; }
        public string Liscensor { get; set; }
        public string Year { get; set; }
        public decimal Count { get; set; }
        public string SourceDesc { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }

    }

    //=---Same Parameter as a Production_By_Company So we use this Class for Operating_Efficiency
    public class Production_By_Company
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string Producer { get; set; }
        public string Year { get; set; }
        public decimal Count { get; set; }
        public string SourceDesc { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }

    }
    public class Demand_By_End_Use
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string EndUse { get; set; }
        public string Year { get; set; }
        public decimal Count { get; set; }
        public string SourceDesc { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }
        public decimal Volume { get; set; }
    }
    public class Demand_By_Grade
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string Grade { get; set; }
        public string Year { get; set; }
        public decimal Count { get; set; }
        public string SourceDesc { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }
        public decimal Volume { get; set; }
    }
    public class Demand_By_Type
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string Type { get; set; }
        public string Year { get; set; }
        public decimal Count { get; set; }
        public string SourceDesc { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }
     
    }
    public class Demand_By_Sales_Channel
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string Channel { get; set; }
        public string Year { get; set; }
        public decimal Count { get; set; }
        public string SourceDesc { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }
        public decimal Volume { get; set; }
    }
    public class Demand_By_Region
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string DemandByRegion { get; set; }
        public string Year { get; set; }
        public decimal Count { get; set; }
        public string SourceDesc { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }
        public decimal Volume { get; set; }
    }
    public class Demand_Supply_Gap
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string DemandSupply { get; set; }
        public string Year { get; set; }
        public decimal Count { get; set; }
        public string SourceDesc { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }
      
    }
    public class Company_Share
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string Company { get; set; }
        public string Year { get; set; }
        public decimal Count { get; set; }
        public string SourceDesc { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }
        public decimal Volume { get; set; }
    }
        public class Country_Wise_Export
        {
            public int Id { get; set; }
            public int Product { get; set; }
            public string ExportCountry { get; set; }
            public string Year { get; set; }
            public decimal Value { get; set; }
            public string SourceDesc { get; set; }
            public string Region { get; set; }
            public string Country { get; set; }
            public string Source { get; set; }
            public decimal Volume { get; set; }
        }
    public class Country_Wise_Import
    {
            public int Id { get; set; }
            public int Product { get; set; }
            public string ImportCountry { get; set; }
            public string Year { get; set; }
            public decimal Value { get; set; }
            public string SourceDesc { get; set; }
            public string Region { get; set; }
            public string Country { get; set; }
            public string Source { get; set; }
            public decimal Volume { get; set; }
        }
        public class Port_Wise_Import
        {
        public int Id { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Year { get; set; }
        public string hsCode { get; set; }
        public string Grade { get; set; }
        public string ImporterName { get; set; }
        public string ImportingCountry { get; set; }
        public string PortOfDestination { get; set; }
        public string SupplierName { get; set; }
        public string CountryOfOrigin { get; set; }
        public string PortOfOrigin { get; set; }
        public string QtyInMT { get; set; }
        public string UnitRateinUSDKg { get; set; }
        public string Incoterms { get; set; }
        public string ShipmentMode { get; set; }
        public int Product { get; set; }
        public string Month { get; set; }
        public string Source { get; set; }
        public string SourceDesc { get; set; }
    }
    public class Port_Wise_Export
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string Year { get; set; }
        public string hsCode { get; set; }
        public string Grade { get; set; }
        public string ExporterName { get; set; }
        public string ExportingCountry { get; set; }
        public string PortOfOrigin { get; set; }
        public string ConsigneeName { get; set; }
        public string CountryofImport { get; set; }
        public string PortOfDestination { get; set; }
        public string QtyInMT { get; set; }
        public string UnitRate { get; set; }
        public string Incoterms { get; set; }
        public string ShipmentMode { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }
        public string SourceDesc { get; set; }

        public DateTime? Date { get; set; }
        public string Weight { get; set; }

    }
    public class Customer_Analysis
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string Year { get; set; }
        public string SourceDesc { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }
        public string CustomerName{ get; set; }
        public string PlantLocation  {get;set;}
        public string MajorGrades{get;set;}
        public string SupplierName{get;set;}
        public string ShipmentOrigin {get;set;}
        public string ShipmentMode{get;set;}
        public string BusinessType  {get;set;}
        public string IncoTerms {get;set;}
        public string DestinationPort { get;set;}
        public decimal PriceRangeKg {get;set;}
        public decimal AnnualOfftakeMT { get;set;}


    }
    public class Technology_Licensor
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public string Year { get; set; }
        public string LicensorName { get; set; }        
        public string LicensorProcess { get; set; }
        public string LicensorType { get; set; }      
        public decimal Count { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string LicensorCompany { get; set; }
        public string LicensorLocation { get; set; }
        public string SourceDesc { get; set; }
        public string Source { get; set; }

    }
}