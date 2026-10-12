using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChemAnalyst.ViewModel
{
    public class NewsDetailsViewModel
    {
        public IEnumerable<SelectListItem> LstCategory { get; set; }
        public IEnumerable<SelectListItem> LstCountry { get; set; }
        public SA_News News { get; set; }

        public IList<SA_News> NewsList { get; set; }

        public IList<SA_News> RelatedNewsList { get; set; }

        public IList<SA_Deals> DealList { get; set; }
    }
}