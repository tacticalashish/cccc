using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
   public class enEmailActivity
    {
        public string Subject { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Body { get; set; }
        public string DateReceived { get; set; }
        public string SentDate { get; set; }
        public string CC_EMAil { get; set; }
        public string BCC_EMail { get; set; }

        public string Email { get; set; }
        public string UserEmail { get; set; }

        public string Password { get; set; }

        public int ID { get; set; }

        public int RowNumber { get; set; }

        public int RecordsCount { get; set; }
        public int StartRowNumber { get; set; }

        public int EndRowNumber { get; set; }

        public string searchKeyword { get; set; }
        public string attachmentFilePaths { get; set; }

        public string CreatedDate { get; set; }
        public string UserType { get; set; }
        

    }
}
