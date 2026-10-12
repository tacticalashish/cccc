using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enTicket
    {
        public int ID { get; set; }

        public int UserFID { get; set; }

        public int ManagerFID { get; set; }

        public string RoleName { get; set; } 

        public string Query { get; set; }

        public string FileName { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedDateView { get { return CreatedDate.ToString("dd/MM/yy hh:mm:tt"); } }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedDateView { get { return UpdatedDate.ToString("dd/MM/yy hh:mm:tt"); } }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public int RowNumber { get; set; }

        public int RecordsCount { get; set; }

        public bool IsUserRead { get; set; }

        public bool IsManagerRead { get; set; }

        public int? Rating { get; set; }

        public string RatingView { get { return Rating.HasValue ? Rating.Value.ToString() : string.Empty; } }

        public List<enTicketComment> ListOfTicketComments { get; set; }
    }
}
