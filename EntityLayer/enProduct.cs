using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enProduct
    {
        public int ID { get; set; }

        public int Orderby { get; set; }
        
        public int? RegionFID { get; set; }

        public int? CountryFID { get; set; }

        public string ProductName { get; set; }

        public string ProductDiscription { get; set; }

        public string Meta { get; set; }

        public string MetaDiscription { get; set; }

        public int? status { get; set; }

        public DateTime? CreatedTime { get; set; }

        public string CreatedTimeView { get { return CreatedTime.Value.ToString("dd/MMM/yyyy hh:mm:ss"); } }

        public string ProductImg { get; set; }

        public int? Category { get; set; }

        public bool? IsSelected { get; set; }

        public bool IsAllow { get; set; }

        public int RowNumber { get; set; }

        public int RecordsCount { get; set; }
       
    }

}
