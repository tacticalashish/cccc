using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class Productdescription
    {
        public int Id { get; set; }
        public int Region { get; set; }
        public int Country { get; set; }

        public int Product { get; set; }
        public string Iupac { get; set; }
        public string Cas { get; set; }
        public string Feedstock { get; set; }
        public string Grades { get; set; }
        public string Enduse { get; set; }
        public string Referallinks { get; set; }
        public string ProductionProcess { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }




    }
}