using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enCompanyMaster
    {
        public int ID { get; set; }

        public string CompanyName { get; set; }

        public string FirstContactEmail { get; set; }

        public string FirstContactName { get; set; }

        public string FirstContactNumber { get; set; }

        public string FirstContactDetail { get { return string.Concat(FirstContactName, " / ", FirstContactEmail, " / ", FirstContactNumber); } }

        public string SecondContactEmail { get; set; }

        public string SecondContactName { get; set; }

        public string SecondContactNumber { get; set; }

        public string SecondContactDetail
        {
            get
            {
                string result = string.Empty;
                if (Utility.Helper.ValIfNullOrEmpty(SecondContactName, true, null) != null)
                {
                    result = string.Concat(SecondContactName, " / ", SecondContactEmail, " / ", SecondContactNumber);
                }
                return result;
            }
        }

        public DateTime? CreatedTime { get; set; }

        public string CreatedDateView { get { return CreatedTime.Value.ToString("dd/MMM/yyyy"); } }

        public DateTime? UpdatedTime { get; set; }

        public int RowNumber { get; set; }

        public string Designation { get; set; }

        public string CRM { get; set; }

    }
}
