using BusinessLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace ChemAnalyst.Controllers
{
    public class CRMTicketController : Controller
    {
        // GET: CRMTicket
        public ActionResult CRMTicket()
        {
            return View();
        }

        public ActionResult CRMTicketList()
        {
            return View();
        }

        public ActionResult CRMTicketListForManager()
        {
            return View();
        }
        public JsonResult GetProductList()
        {
            enCRMTicket enCRMTicket = new enCRMTicket();           
            List<enCRMTicket> listCRMTicket = new List<enCRMTicket>();
            try
            {

                blCRMTicket objB = new blCRMTicket(enCRMTicket);
                listCRMTicket = objB.GetProductList();
            }
            catch (Exception ex)
            {
                listCRMTicket[0].ValidationMessage = ex.Message;
            }
            return Json(listCRMTicket, JsonRequestBehavior.AllowGet);
           
        }


        public JsonResult insertCRMTicket(enCRMTicket Desc)
        {
            string Message = "";
            enCRMTicket objCRMTicke = new enCRMTicket();
            List<enCRMTicket> listCRMTicke = new List<enCRMTicket>();
            try
            {
                objCRMTicke.CreatedBy = Session["User"].ToString();
                objCRMTicke.ProductName = Desc.ProductName;
                objCRMTicke.ID = Desc.ID;
                objCRMTicke.QuearyType = Desc.QuearyType;
                objCRMTicke.Queary = Desc.Queary;
                objCRMTicke.CompanyName = Desc.CompanyName;
                blCRMTicket objB = new blCRMTicket(new enCRMTicket());
                listCRMTicke  = objB.insertCRMTicket(objCRMTicke);
                foreach (var ResponseMessage in listCRMTicke)
                {
                    Message = ResponseMessage.ValidationMessage;
                }
            }
            catch (Exception ex)
            {
                listCRMTicke[0].ValidationMessage = ex.Message;
            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CRMTicketViewRatingList(enCRMTicket enCRMTicket)
        {
            string search = enCRMTicket.searchKeyword;
            search = search.Replace("\'", "");
            enCRMTicket.searchKeyword = search;
            enCRMTicket.EndRowNumber = enCRMTicket.PageNumber * ApplicationSettings.PageSize;
            enCRMTicket.StartRowNumber = enCRMTicket.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enCRMTicket> listOfPricingRating = new List<enCRMTicket>();
            blCRMTicket objBLPricingRating = new blCRMTicket(new enCRMTicket());

            try
            {
                //int UserId = Convert.ToInt32(Session["LoginUser"]);
                enCRMTicket.CreatedBy= Session["User"].ToString();                
                enCRMTicket.FromDate = enCRMTicket.FromDate;
                enCRMTicket.ToDate= enCRMTicket.ToDate;
                listOfPricingRating = objBLPricingRating.CRMTicketViewRatingList(enCRMTicket);
            }
            catch (Exception ex)
            {
                return Json(listOfPricingRating, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfPricingRating != null && listOfPricingRating.Count > 0)
            {
                ShowingFromValue = enCRMTicket.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfPricingRating.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfPricingRating.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfPricingRating,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = enCRMTicket.PageNumber,
                PagerSize = (int)ApplicationSettings.PagerSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.PageSize,
                SearchFilterHasValue = string.Empty,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }




        public JsonResult CRMTicketViewRatingListFormanager(enCRMTicket enCRMTicket)
        {
            string search = enCRMTicket.searchKeyword;
            search = search.Replace("\'", "");
            enCRMTicket.searchKeyword = search;
            enCRMTicket.EndRowNumber = enCRMTicket.PageNumber * ApplicationSettings.PageSize;
            enCRMTicket.StartRowNumber = enCRMTicket.EndRowNumber - ApplicationSettings.PageSize + 1;

            List<enCRMTicket> listOfPricingRating = new List<enCRMTicket>();
            blCRMTicket objBLPricingRating = new blCRMTicket(new enCRMTicket());

            try
            {
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                enCRMTicket.UserId = UserId;
                enCRMTicket.FromDate = enCRMTicket.FromDate;
                enCRMTicket.ToDate = enCRMTicket.ToDate;
                listOfPricingRating = objBLPricingRating.CRMTicketViewRatingListFormanager(enCRMTicket);
            }
            catch (Exception ex)
            {
                return Json(listOfPricingRating, JsonRequestBehavior.AllowGet);
            }

            int ShowingFromValue = 0;
            int ShowingToValue = 0;
            int RecordsCountValue = 0;
            int NumberOfPagesValue = 0;

            if (listOfPricingRating != null && listOfPricingRating.Count > 0)
            {
                ShowingFromValue = enCRMTicket.StartRowNumber;
                ShowingToValue = Convert.ToInt32(listOfPricingRating.LastOrDefault().RowNumber);
                RecordsCountValue = Convert.ToInt32(listOfPricingRating.LastOrDefault().RecordsCount);
                NumberOfPagesValue = (int)Math.Ceiling((double)Convert.ToInt32(RecordsCountValue) / (double)ApplicationSettings.PageSize);
            }

            var returnValue = new
            {
                DateList = listOfPricingRating,
                ShowingFrom = ShowingFromValue,
                ShowingTo = ShowingToValue,
                RecordsCount = RecordsCountValue,
                ActivePage = enCRMTicket.PageNumber,
                PagerSize = (int)ApplicationSettings.PagerSize,
                NumberOfPages = NumberOfPagesValue,
                PageSize = ApplicationSettings.PageSize,
                SearchFilterHasValue = string.Empty,
            };

            var jsonResult = Json(returnValue, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult GetCRMDetails(int id)
        {
            enCRMTicket enCRMTicket = new enCRMTicket();
            List<enCRMTicket> listCRMTicket = new List<enCRMTicket>();
            try
            {

                blCRMTicket objB = new blCRMTicket(enCRMTicket);
                listCRMTicket = objB.GetCRMDetails(id);
            }
            catch (Exception ex)
            {
                listCRMTicket[0].ValidationMessage = ex.Message;
            }

            return Json(listCRMTicket, JsonRequestBehavior.AllowGet);
        }
    }
}