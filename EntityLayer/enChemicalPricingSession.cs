using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.CustomEntity
{
    public class enChemicalPricingSession
    {
        public int RegionFID { get; set; }

        public int CountryFID { get; set; }

        public int ProductFID { get; set; }

        public int RangeFID { get; set; }

        public bool IsSectoral { get; set; }
        public string ProductName { get; set; }

        public int CountryRegionId { get; set; }
        public string RangeName { get; set; }
        public string MarketRangeName { get; set; }
        public string CountryRegion { get; set; }
        public string IsRegion { get; set; }
        public string Year { get; set; }
        public int CategoryId { get; set; }



    }
}
