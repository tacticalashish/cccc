using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Text.RegularExpressions;

namespace ChemAnalyst.Models
{
    public class SA_Mastrategic
    {
        public int id { get; set; }

        [StringLength(10000)]
        public string Title { get; set; }

        [StringLength(500000)]
        public string Description { get; set; }

        [StringLength(500000)]
       
        public string Meta { get; set; }

        [StringLength(1000)]
        public string MetaTitle { get; set; }
        [StringLength(2000)]

        public string MetaDescription { get; set; }

        public DateTime? CreatedTime { get; set; }

      
        public int Product { get; set; }
        public int CountryID { get; set; }
        public int CategoryID { get; set; }
        //public List<SelectListItem> ProductList { get; set; }

       
    }
    
}