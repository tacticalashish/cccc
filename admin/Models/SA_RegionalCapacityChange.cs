using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ChemAnalyst.Models
{
    public class SA_RegionalCapacityChange
    {
        public int ID { get; set; }

        public int? Product { get; set; }

        public string Date { get; set; }

        public string Region { get; set; }

        public string TotalActiveCapacity { get; set; }

        public string MonthlyChange { get; set; }

        public string FileName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

    }
}