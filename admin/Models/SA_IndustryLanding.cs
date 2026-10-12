using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_IndustryLanding
    {
        public int id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Tableoc { get; set; }

        public string Figot { get; set; }

        public string RelatedRep { get; set; }

        public string format { get; set; }

        public string Pages { get; set; }

        public string Industry { get; set; }

        public string Meta { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public DateTime? CreatedTime { get; set; }

        public string URL { get; set; }

        public int CountryID { get; set; }


        public DateTime? CreatedDateFirst { get; set; }

        public string GenerateItemNameAsParam()
        {
            string phrase = string.Format("{0}-{1}", id, URL);// Creates in the specific pattern  
            string str = GetByteArray(phrase).ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");// Remove invalid characters for param  
            str = Regex.Replace(str, @"\s+", "-").Trim(); // convert multiple spaces into one hyphens   
            str = str.Substring(0, str.Length <= 30 ? str.Length : 30).Trim(); //Trim to max 30 char  
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