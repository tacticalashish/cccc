using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enMaskEmail
    {
        public int ID { get; set; }

        public bool IsEmail { get; set; }

        public string Name { get; set; }

        public string NameView { get { return Utility.Helper.ValIfNullOrEmpty(Name, true, string.Empty); } }

        public string CorpEmail { get; set; }

        public string CorpEmailView { get { return Utility.Helper.ValIfNullOrEmpty(CorpEmail, true, string.Empty); } }

        public string Email { get; set; }

        public bool IsChecked{ get; set; }

        public string EmailView { get { return Utility.Helper.ValIfNullOrEmpty(Email, true, string.Empty); } }

        public string ContactNumber { get; set; }

        public string ContactNumberView { get { return Utility.Helper.ValIfNullOrEmpty(ContactNumber, true, string.Empty); } }

        public string CompanyName { get; set; }

        public string CompanyNameView { get { return Utility.Helper.ValIfNullOrEmpty(CompanyName, true, string.Empty); } }

        public int? CountryFID { get; set; }

        public string CountryName { get; set; }

        public string CountryNameView { get { return Utility.Helper.ValIfNullOrEmpty(CountryName, true, string.Empty); } }

        public int? StateFID { get; set; }

        public string StateName { get; set; }

        public string StateNameView { get { return Utility.Helper.ValIfNullOrEmpty(StateName, true, string.Empty); } }

        public string IPAddress { get; set; }

        public string Message { get; set; }

        public string Source { get; set; }

        public DateTime? RequestedDate { get; set; }

        public string RequestedDateView { get { return RequestedDate.Value.ToString("dd/MMM/yy"); } }

        public DateTime? CreatedDate { get; set; }

        public int RowNumber { get; set; }

        public int RecordsCount { get; set; }

        public string ServerResponse { get; set; }

        public string ProductName { get; set; }
        public int ? Flag { get; set; }
    }


}
