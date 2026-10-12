using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_ProductAndFileTypeRelation
    {
        public int ID { get; set; }

        public int ProductFID { get; set; }

        public int FileTypeFID { get; set; }

    }
}