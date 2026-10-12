using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class CommetaryModel
    {
        public int Product { get; set; }
        public int Country { get; set; }
        public int Region { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }

        public string Producer { get; set; }

        public string QueryType { get; set; }

        public DateTime CreatedTime { get; set;}

        public int PricingId { get; set; }

        public string RangeName { get; set; }

        public string Imgpath { get; set; }

        public int CheckCommentaryId { get; set; }

        public string SourceDesc { get; set; }
        public string Feedback { get; set; }
        public string Source { get; set; }



        // new commentry Parameters
        public string PriceTrend { get; set; }
        public string RawMaterial { get; set; }
        public string ImportingCountries { get; set; }
        public string ExportingCountries { get; set; }
        public string DemandOutlook { get; set; }
        public string SupplyDynamics { get; set; }
        public string CurrencyFluctuation { get; set; }
        public string LogisticsPortscenario { get; set; }
        public string LeadTime { get; set; }
        public string NEWPlantShutdown { get; set; }
        public string MarketSituation { get; set; }


    }
}