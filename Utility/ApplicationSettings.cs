using System;
using System.Configuration;
using System.Web;


namespace Utility
{
    public static class ApplicationSettings
    {
        public static string DefaultConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ChemAnalystContext"].ConnectionString; }
        }
        public static string JapanConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["JapanContext"].ConnectionString; }
        }
        public static string KoreaConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["KoreaContext"].ConnectionString; }
        }
        public static string GermanConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["GermanContext"].ConnectionString; }
        }

        public static string ProcurementConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["Procurement"].ConnectionString; }
        }

        public static byte PageSize
        {
            get { return Convert.ToByte(ConfigurationManager.AppSettings["PageSize"]); }
        }
        public static byte PageSizeForProductfinder
        {
            get { return Convert.ToByte(ConfigurationManager.AppSettings["PageSizeForProductfinder"]); }
        }

        public static byte EximPageSize
        {
            get { return Convert.ToByte(ConfigurationManager.AppSettings["eximPagerSize"]); }
        }

        public static byte PagerSize
        {
            get { return Convert.ToByte(ConfigurationManager.AppSettings["PagerSize"]); }
        }

        public static string TicketVirtualPath
        {
            get { return ConfigurationManager.AppSettings["TicketVirtualPath"]; }
        }

        public static string TicketRootPath
        {
            get { return  HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["TicketRootPath"]); }
        }


    }
}
