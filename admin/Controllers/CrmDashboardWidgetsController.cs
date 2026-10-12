using BusinessLayer;
using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

namespace ChemAnalyst.Controllers
{
    // Read-only data widgets for LeadManagementSubscription/DashBoard (the CRM
    // "Control Panel" landing page). Split out of LeadManagementSubscriptionController
    // into their own [SessionState(ReadOnly)] controller because that page fires
    // ~9 AJAX calls in parallel on load, and classic ASP.NET Session is exclusive-
    // locked per session by default -- every request that touches Session[] on the
    // same controller gets serialized by IIS/ASP.NET even though the browser sent
    // them all at once, turning "9 parallel requests" into "9 requests, one after
    // another". None of these actions write to Session (only read Session["LoginUser"]
    // to scope the query to the logged-in CRM user), so ReadOnly session behavior is
    // safe here and lets all 9 run concurrently -- this is the fix for the DashBoard
    // page feeling slow to load. Business logic/BL/DAL/procs are untouched, only
    // moved verbatim from LeadManagementSubscriptionController.
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class CrmDashboardWidgetsController : Controller
    {
        public JsonResult GetUsersdata(enSearchFilter searchFilter)
        {
            enSearchFilter objs = new enSearchFilter();
            int UserId = Convert.ToInt32(Session["LoginUser"]);
            List<enUsersDetails> Listofdata = new List<enUsersDetails>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                objs.CrmId = UserId;
                Listofdata = objBLs.GetUsersdata(objs);
            }
            catch (Exception)
            {
            }
            return Json(Listofdata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Getcompanywiseleadstatus(enSearchFilter searchFilter)
        {
            enSearchFilter objs = new enSearchFilter();
            int UserId = Convert.ToInt32(Session["LoginUser"]);
            List<enUsersDetails> Listofdata = new List<enUsersDetails>();
            blLeadManagementSubscription objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());

            try
            {
                objs.CrmId = UserId;
                Listofdata = objBLs.Getcompanywiseleadstatus(objs);
            }
            catch (Exception)
            {
            }
            return Json(Listofdata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTodaysReminders()
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enTodayReminder> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                listOfData = objBLs.GetTodaysRemindersForUser(enobj);
            }
            catch (Exception)
            {
                return Json(new List<enTodayReminder>(), JsonRequestBehavior.AllowGet);
            }
            return Json(listOfData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetNeedsAttentionToday()
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enNeedsAttentionItem> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                listOfData = objBLs.GetNeedsAttentionToday(enobj);
            }
            catch (Exception)
            {
                return Json(new List<object>(), JsonRequestBehavior.AllowGet);
            }

            var shaped = (listOfData ?? new List<enNeedsAttentionItem>()).Select(i => new
            {
                ID = i.ID,
                CompanyName = i.CompanyName,
                PrimaryContact = i.PrimaryContact,
                RenewsDate = i.RenewsDate.HasValue ? i.RenewsDate.Value.ToString("yyyy-MM-dd") : null,
                DaysToRenewal = i.DaysToRenewal,
                DaysInactive = i.DaysInactive,
                Reason = i.Reason
            }).ToList();

            return Json(shaped, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRenewalsThisQuarter()
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enRenewalThisQuarter> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                listOfData = objBLs.GetRenewalsThisQuarter(enobj);
            }
            catch (Exception)
            {
                return Json(new List<object>(), JsonRequestBehavior.AllowGet);
            }

            var shaped = (listOfData ?? new List<enRenewalThisQuarter>()).Select(i => new
            {
                ID = i.ID,
                CompanyName = i.CompanyName,
                RenewsDate = i.RenewsDate.HasValue ? i.RenewsDate.Value.ToString("yyyy-MM-dd") : null,
                Status = i.Status
            }).ToList();

            return Json(shaped, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCustomersByCountry()
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enLabelCount> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                listOfData = objBLs.GetCustomersByCountry(enobj);
            }
            catch (Exception)
            {
                return Json(new List<enLabelCount>(), JsonRequestBehavior.AllowGet);
            }
            return Json(listOfData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCustomersByIndustry()
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enLabelCount> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                listOfData = objBLs.GetCustomersByIndustry(enobj);
            }
            catch (Exception)
            {
                return Json(new List<enLabelCount>(), JsonRequestBehavior.AllowGet);
            }
            return Json(listOfData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCustomersAddedVsLostByMonth()
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enMonthlyAddedLost> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                listOfData = objBLs.GetCustomersAddedVsLostByMonth(enobj);
            }
            catch (Exception)
            {
                return Json(new List<enMonthlyAddedLost>(), JsonRequestBehavior.AllowGet);
            }
            return Json(listOfData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUsersAddedVsLostByMonth()
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enMonthlyAddedLost> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                listOfData = objBLs.GetUsersAddedVsLostByMonth(enobj);
            }
            catch (Exception)
            {
                return Json(new List<enMonthlyAddedLost>(), JsonRequestBehavior.AllowGet);
            }
            return Json(listOfData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRenewalRevenueVsLostByMonth()
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enMonthlyRevenue> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                listOfData = objBLs.GetRenewalRevenueVsLostByMonth(enobj);
            }
            catch (Exception)
            {
                return Json(new List<enMonthlyRevenue>(), JsonRequestBehavior.AllowGet);
            }
            return Json(listOfData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAvgRevenueUsersPerCustomerByMonth()
        {
            enSearchFilter enobj = new enSearchFilter();
            List<enMonthlyAvg> listOfData = null;
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                enobj.CrmId = Convert.ToInt32(Session["LoginUser"]);
                listOfData = objBLs.GetAvgRevenueUsersPerCustomerByMonth(enobj);
            }
            catch (Exception)
            {
                return Json(new List<enMonthlyAvg>(), JsonRequestBehavior.AllowGet);
            }
            return Json(listOfData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MarkReminderComplete(int reminderId)
        {
            var objBLs = new blLeadManagementSubscription(new enLeadManagementSubscription());
            try
            {
                objBLs.MarkReminderComplete(reminderId);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
