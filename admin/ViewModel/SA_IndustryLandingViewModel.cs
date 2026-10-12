using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChemAnalyst.ViewModel
{
    public partial class SA_IndustryLandingViewModel
    {
        public int id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Tableoc { get; set; }

        public string Figot { get; set; }

        public string RelatedRep { get; set; }

        public string format { get; set; }

        public string Pages { get; set; }

        public string Industry { get; set; }

        public string Meta { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public DateTime? CreatedTime { get; set; }

        public string URL { get; set; }

        public int CountryID { get; set; }

        public string Product { get; set; }


        public DateTime? CreatedDateFirst { get; set; }

        public List<SelectListItem> ProductList { get; set; }

    }
}