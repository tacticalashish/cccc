using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChemAnalyst.ViewModel
{
    public class BlogsDetailsViewModel
    {
        
        public IEnumerable<SelectListItem> LstCountry { get; set; }
        public SA_Blogs Blogs { get; set; }

        public IList<SA_Blogs> BlogsList { get; set; }

        public IList<SA_Blogs> RelatedNewsList { get; set; }

        
    }
}