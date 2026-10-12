using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_FreightIndex
    {

       public int id { get; set; }
       public string year { get; set; }
       public string month { get; set; }
       public string countryoforigin { get; set; }
       public string portoforigin { get; set; }
       public string countryofdestination { get; set; }
       public string portofdestination { get; set; }
       public string containertwenty { get; set; }
       public string containerforty { get; set; }
       public string insights { get; set; }
       public string filename { get; set; }
       public string description { get; set; }
       public string createdby { get; set; }
       public DateTime createddate { get; set; }
       
    }
}