using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_MarketbyTechnologyLicensor
    {

        public int ID { get; set; }

        public int? Product { get; set; }

        public string Year { get; set; }

        public string TechnologyLicensor { get; set; }

        public string Process { get; set; }

        public string Type { get; set; }

        public decimal count { get; set; }

         public string Region { get; set; }

        public string Country { get; set; }

        public string FileName { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }

        public DateTime? CreatedDate { get; set; }
    
    }
}