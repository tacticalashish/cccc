using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
     public class enProductionCost
    {
        
        public string Product { get; set; }
        public string ProductName { get; set; }

        public string ServicesType { get; set; }
        public string Services { get; set; }
        
        public string Region { get; set; }

        public string Year { get; set; }

        public string Quarter { get; set; }


        public decimal? GlobalPercent { get; set; }
        
       public decimal PercentageValue { get; set; }

        public decimal NorthAmerica { get; set; }

        public decimal Europe { get; set; }

        public decimal Asia { get; set; }

        public decimal LatinAmerica { get; set; }

        public decimal MiddleEastAfrica { get; set; }

        public decimal ANZ { get; set; }
        public string Summary { get; set; }
        public string ServiceStatus { get; set; }
        
    }
}
