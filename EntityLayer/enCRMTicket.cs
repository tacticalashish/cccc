using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enCRMTicket
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ValidationMessage { get; set; }
        public string QuearyType { get; set; }
        public string Queary { get; set; }
        public string CreatedBy { get; set; }
        public int RowNumber { get; set; }
        public int RecordsCount { get; set; }
        public int UserId { get; set; }
        public string UserName{ get; set; }
        public string CompanyName { get; set; }
        public string searchKeyword { get; set; }
        public int StartRowNumber { get; set; }
        public int EndRowNumber { get; set; }
        public int PageNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
