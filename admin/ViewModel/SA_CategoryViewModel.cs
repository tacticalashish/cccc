using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChemAnalyst.ViewModel
{
    public class SA_CategoryViewModel
    {
        public int id { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string CategoryDiscription { get; set; }

        [StringLength(50)]
        public string Meta { get; set; }

        [StringLength(500)]
        public string MetaDiscription { get; set; }

        public int? status { get; set; }

        public DateTime? CreatedTime { get; set; }

        [StringLength(50)]
        public string CategoryImg { get; set; }

        public int RegionFID { get; set; }

        public string Countries { get; set; }

        public List<int> ListOfCountry { get { return Countries.Split(',').Select(int.Parse).ToList(); } }

        public List<SA_CountryAndCategoryRelation> listOfSACountryAndCategoryRelation { get; set; }

        public string CountryWithCommaValue { get; set; }

    }
}