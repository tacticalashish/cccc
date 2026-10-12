using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_RegionAndCountryRelation
    {
        public int Id { get; set; }

        public int RegionFID { get; set; }

        public int CountryFID { get; set; }

    }
}