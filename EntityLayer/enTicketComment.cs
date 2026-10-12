using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enTicketComment
    {
        public int ID { get; set; }

        public int TicketFID { get; set; }

        public int? UserFID { get; set; }

        public string CustomerName { get; set; }

        public int? ManagerFID { get; set; }

        public string ManagerName { get; set; }

        public string Comment { get; set; }

        public string FileName { get; set; }

        public bool IsFileNameExist { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedDateView { get { return CreatedDate.ToString("dd/MM/yy hh:mm:tt"); } }

        public string Status { get; set; }

    }
}
