using BusinessLayer;
using ChemAnalyst.Common;
using ChemAnalyst.DAL;
using ChemAnalyst.Models;
using ChemAnalyst.ViewModel;
using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Utility;

namespace ChemAnalyst.Controllers
{
    public class ApiSubscriptionController : Controller
    {
            // GET: Home
            ProductDataStore ObjProduct = new ProductDataStore();
            public ActionResult Index()
            {
                return View("ChemAnalyst");
            }
        public ActionResult ClintApiSubscription(int id=0)
        {
            ViewBag.Id = id;
            ViewBag.LeadId = id;
            return View();
        }


       
  

        public ActionResult GetAllAPIClint()
        {
            
            return View();
        }
   

       

        public JsonResult GetCountry()
        {
                DALHelper obj = new DALHelper();
                List<SA_Country> returnListOfCountry = new List<SA_Country>();
                var ListOfCountry = obj.IEnumerableCountry(true, null); 
                return Json(ListOfCountry, JsonRequestBehavior.AllowGet);
        }
     }
  }