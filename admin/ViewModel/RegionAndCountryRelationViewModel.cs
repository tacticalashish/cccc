using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ChemAnalyst.ViewModel
{
    public class RegionAndCountryRelationViewModel
    {
        public IEnumerable<SA_Region> Regions { get; set; }

        
        public int ID { get; set; }

        public int RegionId { get; set; }

        public List<SA_Country> ListOfCountry { get; set; }


        public SA_Region SA_Region { get; set; }

        public SA_Country SA_Country { get; set; }

    }
}