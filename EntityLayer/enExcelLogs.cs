using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enExcelLogs
    {
        public int ID { get; set; }

        public string RegionName { get; set; }

        public string CountryName { get; set; }

        public int ProductFID { get; set; }

        public string ProductName { get; set; }

        public string FileName { get; set; }

        public string ActionType { get; set; }

        public string Status { get; set; }
        public string Message { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string Type { get; set; }
        public string MarketType { get; set; }

        public string CreatedTimeView { get { return CreatedDate.Value.ToString("dd/MMM/yyyy hh:mm:ss"); } }

        public int RowNumber { get; set; }

        public int RecordsCount { get; set; }
    }
}
