using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enGermanConverter
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string URL { get; set; }

        public string Meta { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string Tableoc { get; set; }

        public string Figot { get; set; }

        public string RelatedRep { get; set; }

        public string Pages { get; set; }


        public string format { get; set; }

        public string Industry { get; set; }

        public DateTime? CreatedTime { get; set; }

        public string CreatedTimeView { get { return CreatedTime == null ? string.Empty : CreatedTime.Value.ToString("dd/MMM/yyyy"); } }

        public int? Product { get; set; }

        public int? CountryID { get; set; }

        public int? CategoryID { get; set; }

        public string ProductName { get; set; }

        public string CountryName { get; set; }
        public string CategoryName { get; set; }
        public string AlternateURL { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public string Q4 { get; set; }
        public string Ans1 { get; set; }
        public string Ans2 { get; set; }
        public string Ans3 { get; set; }
        public string Ans4 { get; set; }
        public string AnalystjapanURL { get; set; }
        public string AnalystkoreaURL { get; set; }
        public int Status { get; set; }

    }
}
