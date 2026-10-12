using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ChemAnalyst.Models
{
    public class SA_MarketbyPortWiseExport  
    {
        public int ID { get; set; }

        public int? Product { get; set; }

        public string Year { get; set; }

        public string HSCode { get; set; }

        public string Grade { get; set; }

        public string ProductDescription { get; set; }

        public string ExporterName { get; set; }

        public string ExportingCountry { get; set; }

        public string PortofOrigin { get; set; }

        public string ConsigneeName { get; set; }

        public string CountryofImport { get; set; }

        public string PortofDestination { get; set; }

        public string QtyinMT { get; set; }

        public string UnitRate { get; set; }

        public string Incoterms { get; set; }

        public string ShipmentMode { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public string FileName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? Date { get; set; }

        public string Weight { get; set; }

    }
}