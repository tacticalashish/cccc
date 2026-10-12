using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class CFRPricing
    {

        public int CountryId { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }

}
