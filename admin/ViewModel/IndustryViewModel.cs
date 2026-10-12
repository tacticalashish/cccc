using ChemAnalyst.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChemAnalyst.ViewModel
{
    public class IndustryViewModel
    {
        public IEnumerable<SA_Industry> Industry { get; set; }
        public IEnumerable<SA_Deals> DealsList { get; set; }

        public IEnumerable<SelectListItem> lstCategory { get; set; }

        public string Title { get; set; }
        public string Meta { get; set; }

       
        public string URL { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public IEnumerable<SA_News> NewsList { get; set; }

        public List<SelectListItem> lstCountry { get; set; }

    }

    public class IndustryViewModel1
    {
        public IPagedList<SA_Industry> Industry { get; set; }
        public IEnumerable<SA_Deals> DealsList { get; set; }

        public IEnumerable<SelectListItem> lstCategory { get; set; }

        public string Title { get; set; }
        public string Meta { get; set; }


        public string URL { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public IEnumerable<SA_News> NewsList { get; set; }

        public List<SelectListItem> lstCountry { get; set; }

    }

    public class IndustryVM
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Product { get; set; }
        public string Meta { get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }

        public string URL { get; set; }
        public string Type { get; set; }
        public string CreateTime { get; set; }
    }

    public class ComentryVM
    {

        public IPagedList<SA_Commentary> Commentary { get; set; }
        public int id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public string Meta { get; set; }
        public string MetaDescription { get; set; }

        public DateTime CreatedTime { get; set; }

        public int Product { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public string ImgPath { get; set; }
    }

    


}
