using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;

namespace ChemAnalystMaintenance
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
        }



        public class CustomHandleErrorAttribute : HandleErrorAttribute
        {
            public override void OnException(ExceptionContext filterContext)
            {
                DateTime timeStamp = DateTime.Now;
                string e = filterContext.Exception.ToString();
                string action = filterContext.RouteData.Values["action"] != null ? filterContext.RouteData.Values["action"].ToString() : "";
                filterContext.ExceptionHandled = true;

                // TEMP DEBUG: surface the real exception on the Error page
                // itself instead of relying only on the DB log (which isn't
                // being used) -- remove the ViewData lines once the live
                // "LeadDetail" crash is diagnosed and fixed.
                var viewResult = new ViewResult
                {
                    ViewName = "Error"
                };
                viewResult.ViewData = new System.Web.Mvc.ViewDataDictionary();
                viewResult.ViewData["DebugException"] = e;
                filterContext.Result = viewResult;

                string websiteID = "Admin";
                string userAgent = filterContext.HttpContext.Request.Headers["User-Agent"].ToString();
                string fsource = filterContext.Exception.Source;
                string userHost;
                userHost = filterContext.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (userHost == "" || userHost == null)
                    userHost = filterContext.HttpContext.Request.ServerVariables["REMOTE_ADDR"];

                try
                {
                    string str = ConfigurationManager.ConnectionStrings["ChemAnalystContext"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(str))
                    {

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = con;
                            con.Open();
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = "insertLogError";
                            cmd.Parameters.AddWithValue("@Timestamp", timeStamp);
                            cmd.Parameters.AddWithValue("@Company", websiteID);
                            cmd.Parameters.AddWithValue("@UserAgent", userAgent);
                            cmd.Parameters.AddWithValue("@UserIP", userHost);
                            cmd.Parameters.AddWithValue("@UserHost", userHost);
                            cmd.Parameters.AddWithValue("@Error", e);
                            cmd.Parameters.AddWithValue("@ErrorSource", action);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                catch (Exception)
                {
                    // DB logging isn't set up / proc isn't inserting anywhere
                    // right now -- don't let that break the error page itself.
                }
            }
        }

    }
}
