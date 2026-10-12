using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ChemAnalyst.Models
{
    public class SA_ExpectedNewCapacity
    {
        public int ID { get; set; }

        public int? Product { get; set; }

        public string Country { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }

        public string Unit { get; set; }
        public string Capacity { get; set; }

        public string TotalCapacity { get; set; }

        public string TypeOfExpansion { get; set; }

        public string OnstreamDate { get; set; }

        public string FileName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

    }
}