
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace ChemAnalyst.Models
{
    public partial class SA_Product_IndiaMapData
    {
        [Key]
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int ProductId { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(500)]
        public string Location { get; set; }

        [StringLength(500)]
        public string City { get; set; }

        [StringLength(50)]
        public string Capacity { get; set; }

        public int? status { get; set; }

        public DateTime? CreatedTime { get; set; }

        public string FileName { get; set; }
    }
}