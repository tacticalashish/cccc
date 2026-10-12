using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ChemAnalyst.Models
{
    public class SA_MarketbyPortWiseImport 
    {
        public int ID { get; set; }

        public int? Product { get; set; }

        public string Year { get; set; }


        public string HScode { get; set; }

        public string Grade { get; set; }

        public string ProductDescription { get; set; }

        public string ImporterName { get; set; }

        public string ImportingCountry { get; set; }

        public string PortOfDestination { get; set; }

        public string SupplierName { get; set; }

        public string CountryOfOrigin { get; set; }

        public string PortOfOrigin { get; set; }

        public string QtyInMT { get; set; }

        public string UnitRateInUSDKG { get; set; }

        public string Incoterms { get; set; }

        public string ShipmentMode { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public string FileName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Month { get; set; }

    }
}