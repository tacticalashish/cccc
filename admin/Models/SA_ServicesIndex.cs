using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_ServicesIndex
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Product { get; set; }
        public string Quarter { get; set; }
        public decimal? Count { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Source { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Filename { get; set; }
    }
}