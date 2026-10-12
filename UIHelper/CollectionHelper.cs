using BusinessLayer;
using DataLayer;
using EntityLayer;
using EntityLayer.CustomEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace UIHelper
{
    public class CollectionHelper
    {





        public static void ExcelLogsCreate(enExcelLogs modal)
        {
            blExcelLogs objBLExcelLogs = new blExcelLogs(modal);

            try
            {
                objBLExcelLogs.Create();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string GenerateItemNameAsParam(string URL, int id)
        {

            string phrase = string.Format("{0}-{1}", URL, id);// Creates in the specific pattern  
            string str = GetByteArray(phrase).ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");// Remove invalid characters for param  
            str = Regex.Replace(str, @"\s+", "-").Trim(); // convert multiple spaces into one hyphens   
            str = str.Substring(0, str.Length <= 100 ? str.Length : 100).Trim(); //Trim to max 30 char  
            str = Regex.Replace(str, @"\s", "-"); // Replaces spaces with hyphens     
            return str;
        }

        private static string GetByteArray(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public static int GetMinValue(decimal minValue)
        {
            string result = "0";

            if (minValue > 1)
            {
                // string minValueIntoString = Convert.ToInt32(minValue).ToString();
                string minValueIntoString = ((int)minValue).ToString();
                int minValueIntoInt = Convert.ToInt32(minValueIntoString.Substring(0, 1));

                if (minValueIntoInt > 1)
                    result = Convert.ToString(minValueIntoInt - 1);
                else
                    result = Convert.ToString(minValueIntoInt);

                for (int i = 1; i < minValueIntoString.Length; i++)
                {
                    result = result + "0";
                };
            }

           else if (minValue < 0)
            {
                int intValue = Convert.ToInt32(minValue);

                // Handle values like -0.50
                if (intValue == 0)
                {
                    result = "0";
                }
                else
                {
                    string minValueIntoString = intValue.ToString();

                    int minValueIntoInt = Convert.ToInt32(minValueIntoString.Substring(1, 1));

                    result = Convert.ToString(minValueIntoInt + 1);

                    for (int i = 1; i < minValueIntoString.Length - 1; i++)
                    {
                        result = result + "0";
                    }

                    result = minValueIntoString.Substring(0, 1) + result;
                }
            }

            return Convert.ToInt32(result);
        }

     
     

      
    }
}


class ChemicalMonthly
{
    public string ProductVariantView { get; set; }
    public string DateView { get; set; }
    // Other properties
}