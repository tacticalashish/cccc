using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ChemAnalyst.Models
{
    public class SA_MarketbyCustomer
    {
        public int ID { get; set; }

        public int? Product { get; set; }

        public string Year { get; set; }

        public string CustomerName { get; set; }

        public string PlantLocation { get; set; }

        public string MajorGrades { get; set; }

        public string SupplierName { get; set; }

        public string ShipmentOrigin { get; set; }

        public string ShipmentMode { get; set; }

        public string AnnualOfftakeMT { get; set; }

        public string PriceRangeKg { get; set; }

        public string DestinationPort { get; set; }

        public string BusinessType { get; set; }

        public string IncoTerms { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public string FileName { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}