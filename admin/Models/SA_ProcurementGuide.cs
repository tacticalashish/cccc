using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ChemAnalyst.Models
{
    public partial class SA_ProcurementGuide
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string MetaTitle { get; set; }

        public string Discription { get; set; }

        public string URL { get; set; }

        public string Author { get; set; }

        public string MetaDiscription { get; set; }

        public int? status { get; set; }

        public DateTime CreatedTime { get; set; }

        public string Img { get; set; }

        public int? Product { get; set; }

        public string Keywords { get; set; }

        public string CreatedBy { get; set; }

        public int CategoryID { get; set; }

        public int CountryID { get; set; }
        public int AppNotification { get; set; }

        public string Videos { get; set; }
        public string Paid { get; set; }

        public string Tags { get; set; }

        public string Summary { get; set; }
        public string Exclusive { get; set; }
        public string Priority { get; set; }

        public string GenerateItemNameAsParam()
        {
            string phrase = string.Format("{0}-{1}", URL, id);// Creates in the specific pattern  
            string str = GetByteArray(phrase).ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");// Remove invalid characters for param  
            str = Regex.Replace(str, @"\s+", "-").Trim(); // convert multiple spaces into one hyphens   
            //str = str.Substring(0, str.Length <= 30 ? str.Length : 30).Trim(); //Trim to max 30 char  
            str = Regex.Replace(str, @"\s", "-"); // Replaces spaces with hyphens     
            return str;
        }

        private string GetByteArray(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}