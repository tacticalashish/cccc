using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ChemAnalyst.Models
{
    public class SA_Logistic
    {
        public int ID { get; set; }

        public int? Product { get; set; }

        public string FromCountry { get; set; }

        public string FromPort { get; set; }

        public string FromLatitude { get; set; }

        public string FromLongitude { get; set; }

        public string ToCountry { get; set; }

        public string ToPort { get; set; }

        public string ToLatitude { get; set; }

        public string ToLongitude { get; set; }

        public string Unit { get; set; }

        public string PackagingType { get; set; }

        public string MinPrice { get; set; }

        public string MaxPrice { get; set; }

        public string FileName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

    }
}