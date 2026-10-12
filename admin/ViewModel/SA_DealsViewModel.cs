namespace ChemAnalyst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class SA_DealsViewModel
    {
        [Key]
        public int id { get; set; }

        
        public string DealsName { get; set; }

        
        public string DealsDiscription { get; set; }
      
        public string URL { get; set; }

       
        public string MetaDiscription { get; set; }

        
        public string DealsImg { get; set; }

       
        public string Keywords { get; set; }
        public int status { get; set; }

       
        public string MetaTitle { get; set; }
        public string Product { get; set; }
        public DateTime? CreatedTime { get; set; }
        public List<SelectListItem> ProductList { get; set; }

    }
}
