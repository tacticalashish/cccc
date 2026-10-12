using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_CountryAndCategoryRelation
    {
        public int ID { get; set; }

        public int CountryFID { get; set; }

        public int CategoryFID { get; set; }

    }
}