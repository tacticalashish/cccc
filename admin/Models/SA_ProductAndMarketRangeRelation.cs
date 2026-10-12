using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_ProductAndMarketRangeRelation
    {
        public int ID { get; set; }

        public int ProductFID { get; set; }

        public int MarketRangeFID { get; set; }

    }
}