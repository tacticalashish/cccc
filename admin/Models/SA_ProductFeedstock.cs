using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_ProductFeedstock
    {
        public int ID { get; set; }
        public int ParentProductID { get; set; }
        public int ChildProductId { get; set; }
        public string ChildProductName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int IsActive { get; set; }
    }
}