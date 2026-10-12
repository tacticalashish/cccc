using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_InterviewVideo
    {

        public int id { get; set; }

        public string Name { get; set; }
        public string Videos { get; set; }

        public int? status { get; set; }

        public DateTime CreatedTime { get; set; }

        public string CreatedBy { get; set; }

        


    }
}
    