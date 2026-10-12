using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_Sectoral
    {
        
        public int id { get; set; }

        
        public string Title { get; set; }

        public string Type { get; set; }

        
        public string Description { get; set; }

        public string Meta { get; set; }
       
        public string MetaDescription { get; set; }

        public DateTime CreatedTime { get; set; }

        public int Product { get; set; }

        public int? RegionFID { get; set; }

        public int? CountryFID { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public string ImgPath { get; set; }

        public string CreatedBy { get; set; }

        public string Producers { get; set; }

        //public List<SelectListItem> ProductList { get; set; }


    }
}