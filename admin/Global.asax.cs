using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using DataLayer;
using ChemAnalystMaintenance;
using ChemAnalyst.Controllers;
using ChemAnalyst.DAL;

namespace ChemAnalyst
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 60;
            // Response.Redirect("Index");
        }
        void Session_End(object sender, EventArgs e)
        {
            try
            {
                //To auto set logout time if user closes the browser without logout
                if (Session.Keys.Count > 0)
                {
                 if (Session["LoginId"] != null)
                    {
                        int loginId = Convert.ToInt32(Session["LoginId"]);
                        CustomerDataStore obj = new CustomerDataStore();
                        obj.AddLogoutDetail(loginId);
                    }
                }
                FormsAuthentication.SignOut();
                // Response.Redirect("user/LogOut");
                Session.Clear();
            }
            catch
            {

            }
        }

        //void Application_Error(object sender, EventArgs e)
        //{
        //    Exception ex = Server.GetLastError();

        //    Context.Response.Clear();
        //    Context.ClearError();
        //    var httpException = ex as HttpException;

        //    RequestContext requestContext = new RequestContext();
        //    if (Context.CurrentHandler != null)
        //        requestContext = ((MvcHandler)Context.CurrentHandler).RequestContext;

        //    if (requestContext.HttpContext != null && requestContext.HttpContext.Request.IsAjaxRequest())
        //    {
        //        Context.Response.Write("Ajax request failed due to server error!");
        //        Context.Response.End();
        //    }
        //    else
        //    {
        //        var routeData = new RouteData();
        //        routeData.Values["controller"] = "error";
        //        routeData.Values["action"] = "generalerror";
        //        routeData.Values.Add("url", Context.Request.Url.OriginalString);
        //        if (httpException != null)
        //        {
        //            switch (httpException.GetHttpCode())
        //            {
        //                case 404:
        //                    routeData.Values["action"] = "pagenotfound";
        //                    break;
        //                case 403:
        //                    routeData.Values["action"] = "generalerror";
        //                    break;
        //                case 500:
        //                    routeData.Values["action"] = "generalerror";
        //                    break;
        //            }
        //        }
        //        IController controller = new ErrorController();
        //        controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        //    }

        //}

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception ex = Server.GetLastError();

                // Clear response and error
                Context.Response.Clear();
                Context.ClearError();

                // Check for HttpException
                var httpException = ex as HttpException;

                // Get the current request context
                RequestContext requestContext = new RequestContext();
                if (Context.CurrentHandler != null)
                {
                    requestContext = ((MvcHandler)Context.CurrentHandler).RequestContext;
                }

                // Check if the request is an AJAX request
                if (requestContext.HttpContext != null && requestContext.HttpContext.Request.IsAjaxRequest())
                {
                    try
                    {
                        // Handle AJAX request failure
                        Context.Response.ContentType = "application/json";
                        Context.Response.Write("Ajax request failed due to server error!");
                        Context.Response.End();
                    }
                    catch (Exception ajaxEx)
                    {
                        // Log or handle the AJAX exception
                        // For example: LogException(ajaxEx);
                        throw; // Optionally rethrow the exception
                    }
                }
                else
                {
                    try
                    {
                        // Handle non-AJAX request by routing to an error controller
                        var routeData = new RouteData();
                        routeData.Values["controller"] = "error";
                        routeData.Values["action"] = "generalerror";
                        routeData.Values.Add("url", Context.Request.Url.OriginalString);

                        // Set specific action based on HttpException code
                        if (httpException != null)
                        {
                            switch (httpException.GetHttpCode())
                            {
                                case 404:
                                    routeData.Values["action"] = "pagenotfound";
                                    break;
                                case 403:
                                case 500:
                                default:
                                    routeData.Values["action"] = "generalerror";
                                    break;
                            }
                        }

                        // Execute the error controller action
                        IController controller = new ErrorController();
                        controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                    }
                    catch (Exception routeEx)
                    {
                        // Log or handle the route exception
                        // For example: LogException(routeEx);
                        throw; // Optionally rethrow the exception
                    }
                }
            }
            catch (Exception outerEx)
            {
                // Log or handle the outer exception
                // For example: LogException(outerEx);
                // You may want to redirect to a fallback error page or handle it differently
                Response.Redirect("~/Admin/Login");
            }
        }

    }
}