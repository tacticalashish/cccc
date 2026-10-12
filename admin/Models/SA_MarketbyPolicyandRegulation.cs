using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_MarketbyPolicyandRegulation
    {

        public int ID { get; set; }

        public int? Product { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public string PolicyandRegulation { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

       
    }
}