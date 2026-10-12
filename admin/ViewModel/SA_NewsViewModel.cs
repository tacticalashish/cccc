namespace ChemAnalyst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class SA_NewsViewModel
    {
        [Key]
        public int id { get; set; }

        public string NewsName { get; set; }

        public string NewsDiscription { get; set; }

        public string URL { get; set; }

        public string Author { get; set; }

        public string CountryID { get; set; }

        public int CategoryID { get; set; }

        public string MetaDiscription { get; set; }

        public string NewsImg { get; set; }

        public string Keywords { get; set; }

        public string NewsVideos { get; set; }
        public int status { get; set; }

        public string MetaTitle { get; set; }

        public string Product { get; set; }

        public DateTime? CreatedTime { get; set; }

        public List<SelectListItem> ProductList { get; set; }

        public List<SelectListItem> CategoryList { get; set; }

        public string Paid { get; set; }
        public string Tags { get; set; }
        public string NewsSummary { get; set; }
        public string ExclusiveNews { get; set; }
        public string NewsPriority { get; set; }

    }


}
