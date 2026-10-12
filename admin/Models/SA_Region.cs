using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_Region
    {
        public int id { get; set; }

        [StringLength(200)]
        public string RegionName { get; set; }

        
        public bool Active { get; set; }

        

    }
}