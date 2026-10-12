using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enAccountDetails
    {
        public int Id { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string AccountHolderAddress { get; set; }
        public string IBAN { get; set; }
        public string SWIFTcode { get; set; }
        public string BICcode { get; set; }
        public string BSBcode { get; set; }
        public string Transitcode { get; set; }
        public string Routingnumber { get; set; }
        public string Identification { get; set; }
        public string Email { get; set; }
        public string ValidationMessage { get; set; }

    }
}
