using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChemAnalyst.Models

{
    public partial class SA_ProcurementGuideViewModel
    {
        [Key]
        public int id { get; set; }

        public string Name { get; set; }

        public string Discription { get; set; }

        public string URL { get; set; }

        public string Author { get; set; }

        public string CountryID { get; set; }

        public int CategoryID { get; set; }

        public string MetaDiscription { get; set; }

        public string Img { get; set; }

        public string Keywords { get; set; }

        public string Videos { get; set; }
        public int status { get; set; }

        public string MetaTitle { get; set; }

        public string Product { get; set; }

        public DateTime? CreatedTime { get; set; }

        public List<SelectListItem> ProductList { get; set; }

        public string Paid { get; set; }
        public string Tags { get; set; }
        public string Summary { get; set; }
        public string Exclusive { get; set; }
        public string Priority { get; set; }
    }
}