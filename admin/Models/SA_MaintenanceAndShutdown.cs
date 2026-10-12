using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ChemAnalyst.Models
{
    public class SA_MaintenanceAndShutdown
    {
        public int ID { get; set; }

        public int? Product { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        public string Region { get; set; }

       

        public string Country { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }
        public string OutageStartDate { get; set; }
        public string OutageEndDate { get; set; }

        public string Duration { get; set; }

        public int? ActiveCapacityTPM { get; set; }

        public int? TotalCapacityTPM { get; set; }
        public string OperatingEfficiency { get; set; }
        public string TypeOfShutdown { get; set; }
       

        public string Insights { get; set; }

        public string ImpactOnPricing { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
        public string CapacityLoss { get; set; }

        public string Unit { get; set; }

        public string FileName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
       
      
       

    }
}