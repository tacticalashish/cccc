
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ChemAnalyst.Controllers
{
    public class ErrorController : Controller
    {  
        public ActionResult pagenotfound()
        {
            Response.StatusCode = (int) HttpStatusCode.NotFound;
            ViewBag.URL = RouteData.Values["url"].ToString();
            return View();
        }
        public ActionResult generalerror()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ViewBag.Message = "Error occured!";
            return View();
        }
    }
}