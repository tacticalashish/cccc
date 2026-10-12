using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EntityLayer
{
   public class enCustomerDashboardAccess
    {
        public int isFlag { get; set; }
        public int UserId { get; set;}
        public int Flag { get; set; }
        public int ProductId { get; set; }
        public string NewsDiscription { get; set; }
        public string URL { get; set; }
        public string NewsName { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDiscription { get; set; }
        public string Keywords { get; set; }
        public string status { get; set; }

        public DateTime? CreatedTime { get; set; }
        public string NewsImg { get; set; }

        public int Product { get; set; }
        public string ProductName { get; set; }

        public DateTime CreatedBy { get; set; }

        public string Author { get; set; }
        public int CategoryID { get; set; }
        public int CountryID { get; set; }

        public int ID { get; set; }
        public string GenerateItemNameAsParam()
        {
            string phrase = string.Format("{0}-{1}", URL, ID);// Creates in the specific pattern  
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
