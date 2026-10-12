using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ChemAnalyst.ViewModel
{
    public class SA_ProductAndFileTypeRelationViewModel
    {
        public int ID { get; set; }

        public int RegionFID { get; set; }

        public int CountryFID { get; set; }

        public int CategoryFID { get; set; }

        public int ProductFID { get; set; }

        public int FileTypeFID { get; set; }

        //public int SA_ProductID { get; set; }

        public SA_Product SA_Product { get; set; }

        public List<SA_Product> listOfSA_Product { get; set; }

        public SA_FileType SA_FileType { get; set; }

        public List<SA_FileType> ListOfSA_FileType { get; set; }



        //public List<SelectListItem> IEnumerableSA_Product { get; set; }
        //public IEnumerable<SA_Region> Regions { get; set; }

        //public int RegionId { get; set; }

        //public List<SA_Country> ListOfCountry { get; set; }


        //public SA_Region SA_Region { get; set; }

        //public SA_Country SA_Country { get; set; }

    }
}