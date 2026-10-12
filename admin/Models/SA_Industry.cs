


namespace ChemAnalyst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Text.RegularExpressions;
    public class SA_Industry
    {
        public int id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Tableoc { get; set; }

        public string Figot { get; set; }

        public string RelatedRep { get; set; }

        public string Methodology { get; set; }

        public string Singleprice { get; set; }

        public string Corpprice { get; set; }

        public string Ques1 { get; set; }

        public string Ans1 { get; set; }

        public string Ques2 { get; set; }

        public string Ans2 { get; set; }

        public string Ques3 { get; set; }

        public string Ans3 { get; set; }

        public string Ques4 { get; set; }

        public string Ans4 { get; set; }

        public string format { get; set; }

        public string Pages { get; set; }
       
        public string Industry { get; set; }

        public string Meta { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public DateTime? CreatedTime { get; set; }

        public string URL { get; set; }

        public int Product { get; set; }

        public int CountryID { get; set; }

        public int CategoryID { get; set; }

        public string modifyBy { get; set; }

        public DateTime? modifydate { get; set; }

        public string IPAddress { get; set; }
        //public List<SelectListItem> ProductList { get; set; }

        public string GenerateItemNameAsParam()
        {
            string phrase = string.Format("{0}-{1}", URL, id);// Creates in the specific pattern  
            string str = GetByteArray(phrase).ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");// Remove invalid characters for param  
            str = Regex.Replace(str, @"\s+", "-").Trim(); // convert multiple spaces into one hyphens   
            str = str.Substring(0, str.Length <= 100 ? str.Length : 100).Trim(); //Trim to max 30 char  
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