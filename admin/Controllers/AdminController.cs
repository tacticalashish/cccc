using BusinessLayer;
using ChemAnalyst.DAL;
using ChemAnalyst.Models;
using ChemAnalyst.ViewModel;
using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Utility;

namespace ChemAnalyst.Controllers
{
 
    //[SessionExpireFilter]
    public class AdminController : Controller
    {



        // GET: Admin

        List<IndustryVM> listOfIndustryVM = new List<IndustryVM>();
        private ChemAnalystContext _context = new ChemAnalystContext();

        private const string ImageUrlPath = "http://chemanalyst.com/images/";

        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// get user data
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ActionResult UserLogin(FormCollection collection)
        {

            try
            {
                SA_User login = new Models.SA_User();
                login.Email = Request["Username"];
                LoginDataStore LoginStore = new LoginDataStore();
                login.UserPassword = Request["Password"];
                if ((login.Email == "customer" && login.UserPassword == "Customer"))
                    {
                    string product = null;
                    string ChartType = null;
                    string Range = null;
                    string CompareProject = null;
                    bool Customer = true;
                    return RedirectToAction("ChecmPriceYearlyChart", "ChemicalPricing", new
                    {
                        product,
                        ChartType,
                        Range,
                        CompareProject,
                        Customer
                    });

                    // return this.RedirectToAction("ChecmPriceYearlyChart", "ChemicalPricing");
                }
                if ((LoginStore.CheckUser(login) != null) || (login.Email == "admin" && login.UserPassword == "admin"))
                {
                    // GetCommentaryBasedOnSearchFilter();
                    //Session["var"] = listOfIndustryVM;
                    SA_User objectuser = LoginStore.CheckUser(login);
                    if (objectuser.Status == false)
                    {
                        TempData["ErrorMessage"] = "You don’t have access to the account. Kindly contact Administrator.";
                        return View("Login");
                    }

                    if ((objectuser != null))
                    {
                        Session["LoginUser"] = objectuser.id;
                        Session["User"] = objectuser.Fname + " " + objectuser.Lname;
                        Session["UserImg"] = "images /" + objectuser.ProfileImage; ;
                        Session["UserRole"] = objectuser.Role;
                        List<SA_RoleWiseAccess> Access = LoginStore.Getpage(objectuser.Role);
                        Session["Access"] = Access;
                        if (objectuser.Role == "Sales")
                        {
                            return this.RedirectToAction("SalesDashBoard", "LeadManagementSubscription");
                        }
                        if (objectuser.Role == "CRM")
                        {
                            return this.RedirectToAction("CRMLeadDashBoard", "LeadManagementSubscription");
                        }

                        if (objectuser.Role.ToUpper() != "ADMIN")
                            return this.RedirectToAction("Index", "User");
                    }
                    else
                    {
                        objectuser = new SA_User();
                        Session["LoginUser"] = 100001;
                        objectuser.Role = "Admin";
                        Session["UserRole"] = objectuser.Role;
                        List<SA_RoleWiseAccess> Access = LoginStore.Getpage(objectuser.Role);
                        Session["Access"] = Access;
                        Session["User"] = "Admin";
                        Session["UserImg"] = "images/" + "user.jpg";

                    }
                    return RedirectToAction("ShowUserList");
                }
                else
                {
                    return View("Login");

                }
            }
            catch (NullReferenceException ex)
            {
                return View("Login");

            }
          
        }

        public ActionResult LoadUserData()
        {
            try
            {
                UserDataStore Obj = new UserDataStore();
                List<SA_User> UserList = Obj.GetUserList().Where(w => w.Email != "admin@techsci.com").ToList();
                return Json(new { data = UserList }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult LoadCustomerData()
        {
            try
            {
                var CustomerList = _context.SA_Customers.ToList();
                return Json(new { data = CustomerList }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult LoadStateData()
        {
            try
            {

                var StateList = _context.SA_States.ToList().Select(w => new
                {
                    Country = _context.SA_Country.Where(r => r.id == w.CountryId).FirstOrDefault() != null ? _context.SA_Country.Where(r => r.id == w.CountryId).FirstOrDefault().CountryName : "",
                    Id = w.Id,
                    State = w.State,
                }


                ).ToList();
                return Json(new { data = StateList }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public ActionResult LoadIndustryData()
        {
            try
            {
                ProductDataStore ObjProduct = new ProductDataStore();
                IndustryDataStore Obj = new IndustryDataStore();
                var IndustryList = Obj.GetIndustryList().Select(w => new IndustryVM
                {
                    id = w.id,
                    Title = w.Title,
                    CreateTime = w.CreatedTime != null ? w.CreatedTime.Value.ToString("dd/MM/yyyy") : "",
                    Product = w.Product != null ? ObjProduct.GetProductByid(w.Product).ProductName : "",
                });
                return Json(new { data = IndustryList }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult LoadoverviewData()
        {
            try
            {
                ProductDataStore ObjProduct = new ProductDataStore();
                OverviewDataStore Obj = new OverviewDataStore();
                var overviewList = Obj.GetOverviewList().Select(w => new PriceoverviewVM
                {
                    id = w.id,
                    Title = string.Empty,
                    CreateTime = w.CreatedTime != null ? w.CreatedTime.Value.ToString("dd/MM/yyyy") : "",
                    Product = w.Product != null ? ObjProduct.GetProductByid(w.Product).ProductName : "",
                });
                return Json(new { data = overviewList }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult LoadstrategicData()
        {
            try
            {
                ProductDataStore ObjProduct = new ProductDataStore();
                StrategicDataStore Obj = new StrategicDataStore();
                var strategicList = Obj.GetStrategicList().Select(w => new StrategicVM
                {
                    id = w.id,
                    Title = w.Title,
                    CreateTime = w.CreatedTime != null ? w.CreatedTime.Value.ToString("dd/MM/yyyy") : "",
                    Product = w.Product != null ? ObjProduct.GetProductByid(w.Product).ProductName : "",
                });
                return Json(new { data = strategicList }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult GetUserList()
        {
            return View("grid");
        }

        public ActionResult UserList()
        {
            return View("user-list");
        }

        public ActionResult CustomerList()
        {
            return View("customer-list");
        }
        public ActionResult AddUser()
        {
            ChemAnalystContext _context = new ChemAnalystContext();
            var customerData = (from User in _context.SA_Role
                                    //  select  { Fname = User.Fname+" "+User.Lname , Phone = User.Phone, Role=User.Role,Email=User.Email,UserPassword=User.Password});
                                select new SelectListItem { Text = User.Role, Value = User.Role }).Where(w => w.Value != "Admin").ToList();
            SA_UserViewModel obj = new SA_UserViewModel();
            obj.UserRoleList = customerData;
            return View("add-user", obj);

        }

        public ActionResult AddState()
        {

            return View("add-state");

        }

        public ActionResult SaveUser(SA_User User)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    file.SaveAs(path);
                    User.ProfileImage = fileName;
                }
            }
            UserDataStore Obj = new UserDataStore();
            if (User.id == 0)
            {
                Obj.AddUser(User);
            }
            else
            {
                Obj.UpdateUser(User);
                if ((User.Role).ToUpper() != "ADMIN")
                {
                    return View("UserView");
                }
            }
            return RedirectToAction("ShowUserList");
        }

        #region CRUD operation for Region

        [SessionExpireFilter]
        public ActionResult AddRegion()
        {
            return View(new SA_Region());
        }

        [SessionExpireFilter]
        public ActionResult EditRegion(int id)
        {
            ChemAnalystContext _context = new ChemAnalystContext();

            SA_Region objRegion = _context.SA_Region.Where(w => w.id == id).FirstOrDefault();

            return View("AddRegion", objRegion);
        }


        [SessionExpireFilter]
        public ActionResult SaveRegion(SA_Region region)
        {
            if (region.id == 0)
            {
                var check = _context.SA_Region.Where(item => item.RegionName == region.RegionName).FirstOrDefault();

                if (check != null)
                {
                    TempData["Message"] = "This region is already exist!";
                    return RedirectToAction("AddRegion");
                }
                else
                {
                    SA_Region ObjSARegion = new SA_Region();
                    ObjSARegion.RegionName = region.RegionName;
                    ObjSARegion.Active = true;
                    _context.SA_Region.Add(ObjSARegion);
                    _context.SaveChanges();
                }
            }
            else
            {
                var ObjSARegion = _context.SA_Region.Where(item => item.id == region.id).FirstOrDefault();
                ObjSARegion.RegionName = region.RegionName;
                _context.SaveChanges();
                return RedirectToAction("RegionList");

            }
            return RedirectToAction("RegionList");
        }

        [SessionExpireFilter]
        public ActionResult DeleteRegion(int id)
        {
            try
            {
                SA_Region objRegion = _context.SA_Region.Where(item => item.id == id).FirstOrDefault();
                _context.Entry(objRegion).State = EntityState.Deleted;
                int x = _context.SaveChanges();

                return RedirectToAction("RegionList");
            }
            catch (Exception ex)
            {

                return View("ErrorEventArgs");
            }
        }

        [SessionExpireFilter]
        public ActionResult RegionList()
        {
            UserDataStore Obj = new UserDataStore();
            List<SA_Region> listOfRegion = _context.SA_Region.ToList();
            return View("RegionList", listOfRegion);
        }

        public ActionResult LoadRegionData()
        {
            try
            {

                List<SA_Region> listOfRegion = _context.SA_Region.ToList();
                return Json(new { data = listOfRegion }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        #region region mapping with country

        [SessionExpireFilter]
        public ActionResult ListRegionAndCountryRelation()
        {
            return View(new DALHelper().GetRegionAndCountryRelationViewModel());
        }

        [SessionExpireFilter]
        public ActionResult NewRegionAndCountryRelation()
        {

            var objRegionAndCountryRelationViewModel = new RegionAndCountryRelationViewModel()
            {
                Regions = _context.SA_Region.ToList(),
                ListOfCountry = _context.SA_Country.OrderBy(item => item.CountryName).ToList()
            };

            return View(objRegionAndCountryRelationViewModel);
        }

        [HttpPost]
        public ActionResult SaveRegionAndCountryRelation(RegionAndCountryRelationViewModel model)
        {
            List<SA_RegionAndCountryRelation> listOfSA_RegionAndCountryRelation = new List<SA_RegionAndCountryRelation>();
            listOfSA_RegionAndCountryRelation = _context.SA_RegionAndCountryRelation.ToList().FindAll(item => item.RegionFID == model.RegionId);

            //Note: while create new relation then to check region already exist or not
            if (model.ID == 0 && listOfSA_RegionAndCountryRelation.Count() > 0)
            {
                TempData["Message"] = "Could not save duplicate Region and Country Relationship.";
                return RedirectToAction("ListRegionAndCountryRelation");
            }


            //Note: while for update
            bool result = true;
            if (model.ID > 0)
            {
                result = false;
                result = new DALHelper().DeleteRegionAndCountryRelationBasedOnRegion(model.RegionId);
                TempData["Message"] = "something went wrong, could not save Region and Country Relationship.";
            }

            //Note: save mapping 
            if (result)
            {
                foreach (var item in model.ListOfCountry)
                {
                    if (item.IsSelected)
                    {
                        SA_RegionAndCountryRelation objSARegionAndCountryRelation = new SA_RegionAndCountryRelation();
                        objSARegionAndCountryRelation.RegionFID = model.RegionId;
                        objSARegionAndCountryRelation.CountryFID = item.id;

                        _context.SA_RegionAndCountryRelation.Add(objSARegionAndCountryRelation);
                        TempData["Message"] = "Region and Country Relationship Mapped Successfully.";
                    }
                }
                _context.SaveChanges();
            }

            return RedirectToAction("ListRegionAndCountryRelation");
        }

        public ActionResult DeleteRegionAndCountryRelation(int? id)
        {
            if (id == null && id.Value < 0)
                TempData["Message"] = "could not delete. please try after some time";

            bool result = new DALHelper().DeleteRegionAndCountryRelationBasedOnRegion(id.Value);

            if (result)
                TempData["Message"] = "recod has been deleted successfully.";
            else
                TempData["Message"] = "could not delete. please try after some time";

            return RedirectToAction("ListRegionAndCountryRelation");
        }


        //Note: id parameter id is region id
        [SessionExpireFilter]
        public ActionResult EditRegionAndCountryRelation(int? id)
        {
            List<SA_RegionAndCountryRelation> listOfSA_RegionAndCountryRelation = new List<SA_RegionAndCountryRelation>();
            listOfSA_RegionAndCountryRelation = _context.SA_RegionAndCountryRelation.ToList().FindAll(item => item.RegionFID == id);

            List<SA_Country> listOfSA_Country = _context.SA_Country.OrderBy(item => item.CountryName).ToList();

            foreach (var item in listOfSA_Country)
                if (listOfSA_RegionAndCountryRelation.Exists(it => it.CountryFID == item.id))
                    item.IsSelected = true;


            var objRegionAndCountryRelationViewModel = new RegionAndCountryRelationViewModel()
            {
                ID = id.Value,
                RegionId = id.Value,
                Regions = _context.SA_Region.ToList(),
                ListOfCountry = listOfSA_Country
            };

            return View("NewRegionAndCountryRelation", objRegionAndCountryRelationViewModel);
        }


        #endregion


        #region Product and file type mapping

        //Note: list of relation product and file type
        [SessionExpireFilter]
        public ActionResult ProductAndFileTypeRelation()
        {
            return View(new DALHelper().GetProductAndFileTypeRelationViewModel());
        }

        [SessionExpireFilter]
        public ActionResult NewProductAndFileTypeRelation()
        {
            var objSA_ProductAndFileTypeRelationViewModel = new SA_ProductAndFileTypeRelationViewModel()
            {
                ListOfSA_FileType = _context.SA_FileType.OrderBy(item => item.OrderNumber).ToList()
            };

            return View(objSA_ProductAndFileTypeRelationViewModel);
        }

        [HttpPost]
        public ActionResult SaveProductAndFileTypeRelation(SA_ProductAndFileTypeRelationViewModel model)
        {
            List<SA_ProductAndFileTypeRelation> listOfSA_ProductAndFileTypeRelation = new List<SA_ProductAndFileTypeRelation>();
            listOfSA_ProductAndFileTypeRelation = _context.SA_ProductAndFileTypeRelation.ToList().FindAll(item => item.ProductFID == model.ProductFID);

            //Note: while create new relation then to check region already exist or not
            if (model.ID == 0 && listOfSA_ProductAndFileTypeRelation.Count() > 0)
            {
                TempData["Message"] = "already exists product mapping Range";
                return RedirectToAction("ProductAndFileTypeRelation");
            }


            //Note: while for update
            bool result = true;
            if (model.ID > 0)
            {
                if (!new DALHelper().DeleteSA_ProductAndFileTypeRelation(model.ProductFID))
                {
                    TempData["Message"] = "something went wrong, could not save, please try after sometime.!";
                    return RedirectToAction("ProductAndFileTypeRelation");
                }
            }

            //Note: save mapping 
            if (result)
            {
                foreach (var item in model.ListOfSA_FileType)
                {
                    if (item.IsSelected)
                    {
                        SA_ProductAndFileTypeRelation objSA_ProductAndFileTypeRelation = new SA_ProductAndFileTypeRelation();
                        objSA_ProductAndFileTypeRelation.ProductFID = model.ProductFID;
                        objSA_ProductAndFileTypeRelation.FileTypeFID = item.ID;

                        _context.SA_ProductAndFileTypeRelation.Add(objSA_ProductAndFileTypeRelation);
                        TempData["Message"] = "Region and Country Relationship Mapped Successfully.";
                    }
                }
                _context.SaveChanges();
            }

            return RedirectToAction("ProductAndFileTypeRelation");
        }

        //Note: delete product and file type mapping based on product
        public ActionResult DeleteProductAndFileTypeRelation(int? id)
        {
            if (id == null && id.Value < 0)
                TempData["Message"] = "could not delete. please try after some time";

            bool result = new DALHelper().DeleteSA_ProductAndFileTypeRelation(id.Value);

            if (result)
                TempData["Message"] = "recod has been deleted successfully.";
            else
                TempData["Message"] = "could not delete. please try after some time";

            return RedirectToAction("ProductAndFileTypeRelation");
        }


        //Note: id parameter id is region id
        [SessionExpireFilter]
        public ActionResult EditProductAndFileTypeRelation(int? id)
        {
            List<SA_ProductAndFileTypeRelation> listOfSA_ProductAndFileTypeRelation = new List<SA_ProductAndFileTypeRelation>();
            listOfSA_ProductAndFileTypeRelation = _context.SA_ProductAndFileTypeRelation.ToList().FindAll(item => item.ProductFID == id);

            List<SA_FileType> listOfSA_FileType = _context.SA_FileType.OrderBy(item => item.OrderNumber).ToList();

            foreach (var item in listOfSA_FileType)
                if (listOfSA_ProductAndFileTypeRelation.Exists(it => it.FileTypeFID == item.ID))
                    item.IsSelected = true;

            SA_Product objSA_Product = _context.SA_Product.ToList().Find(item => item.id == id.Value);

            var objSA_ProductAndFileTypeRelationViewModel = new SA_ProductAndFileTypeRelationViewModel()
            {
                ID = id.Value,
                RegionFID = objSA_Product.RegionFID,
                CountryFID = objSA_Product.CountryFID,
                CategoryFID = objSA_Product.Category,
                ProductFID = id.Value,
                ListOfSA_FileType = listOfSA_FileType
            };

            return View("NewProductAndFileTypeRelation", objSA_ProductAndFileTypeRelationViewModel);
        }

        #endregion


        #region Product and market range mapping

        //Note: list of relation product and file type
        [SessionExpireFilter]
        public ActionResult ProductAndMarketRangeRelation()
        {
            return View(new DALHelper().GetSA_ProductAndMarketRangeRelationViewModel());
        }

        [SessionExpireFilter]
        public ActionResult NewProductAndMarketRangeRelation()
        {
            var objSA_ProductAndMarketRangeRelationViewModel = new SA_ProductAndMarketRangeRelationViewModel()
            {
                ListOfSA_MarketRange = _context.SA_MarketRange.OrderBy(item => item.OrderNumber).ToList()
            };

            return View(objSA_ProductAndMarketRangeRelationViewModel);
        }

        [HttpPost]
        public ActionResult SaveProductAndMarketRangeRelation(SA_ProductAndMarketRangeRelationViewModel model)
        {
            List<SA_ProductAndMarketRangeRelation> listOfSA_ProductAndMarketRangeRelation = new List<SA_ProductAndMarketRangeRelation>();
            listOfSA_ProductAndMarketRangeRelation = _context.SA_ProductAndMarketRangeRelation.ToList().FindAll(item => item.ProductFID == model.ProductFID);

            //Note: while create new relation then to check region already exist or not
            if (model.ID == 0 && listOfSA_ProductAndMarketRangeRelation.Count() > 0)
            {
                TempData["Message"] = "already exists mapping with Range";
                return RedirectToAction("ProductAndMarketRangeRelation");
            }

            //Note: while for update
            bool result = true;
            if (model.ID > 0)
            {
                if (!new DALHelper().DeleteSA_ProductAndMarketRangeRelation(model.ProductFID))
                {
                    TempData["Message"] = "something went wrong, could not save, please try after sometime.!";
                    return RedirectToAction("ProductAndMarketRangeRelation");
                }
            }

            //Note: save mapping 
            if (result)
            {
                foreach (var item in model.ListOfSA_MarketRange)
                {
                    if (item.IsSelected)
                    {
                        SA_ProductAndMarketRangeRelation objSA_ProductAndMarketRangeRelation = new SA_ProductAndMarketRangeRelation();
                        objSA_ProductAndMarketRangeRelation.ProductFID = model.ProductFID;
                        objSA_ProductAndMarketRangeRelation.MarketRangeFID = item.ID;

                        _context.SA_ProductAndMarketRangeRelation.Add(objSA_ProductAndMarketRangeRelation);
                        TempData["Message"] = "Region and Country Relationship Mapped Successfully.";
                    }
                }
                _context.SaveChanges();
            }

            return RedirectToAction("ProductAndMarketRangeRelation");
        }

        //Note: delete product and file type mapping based on product
        public ActionResult DeleteProductAndMarketRangeRelation(int? id)
        {
            if (id == null && id.Value < 0)
                TempData["Message"] = "could not delete. please try after some time";

            bool result = new DALHelper().DeleteSA_ProductAndMarketRangeRelation(id.Value);

            if (result)
                TempData["Message"] = "recod has been deleted successfully.";
            else
                TempData["Message"] = "could not delete. please try after some time";

            return RedirectToAction("ProductAndMarketRangeRelation");
        }


        //Note: id parameter id is region id
        [SessionExpireFilter]
        public ActionResult EditProductAndMarketRangeRelation(int? id)
        {
            List<SA_ProductAndMarketRangeRelation> listOfSA_ProductAndMarketRangeRelation = new List<SA_ProductAndMarketRangeRelation>();
            listOfSA_ProductAndMarketRangeRelation = _context.SA_ProductAndMarketRangeRelation.ToList().FindAll(item => item.ProductFID == id);

            List<SA_MarketRange> listOfSA_MarketRange = _context.SA_MarketRange.OrderBy(item => item.OrderNumber).ToList();

            foreach (var item in listOfSA_MarketRange)
                if (listOfSA_ProductAndMarketRangeRelation.Exists(it => it.MarketRangeFID == item.ID))
                    item.IsSelected = true;

            SA_Product objSA_Product = _context.SA_Product.ToList().Find(item => item.id == id.Value);

            var objSA_ProductAndMarketRangeRelationViewModel = new SA_ProductAndMarketRangeRelationViewModel()
            {
                ID = id.Value,
                RegionFID = objSA_Product.RegionFID,
                CountryFID = objSA_Product.CountryFID,
                CategoryFID = objSA_Product.Category,
                ProductFID = id.Value,
                ListOfSA_MarketRange = listOfSA_MarketRange
            };

            return View("NewProductAndMarketRangeRelation", objSA_ProductAndMarketRangeRelationViewModel);
        }

        #endregion



        public ActionResult AddCountry()
        {
            return View("add-country", new SA_Country());
        }

        [SessionExpireFilter]
        public ActionResult EditCountry(int id)
        {
            SA_Country obj = _context.SA_Country.Where(w => w.id == id).FirstOrDefault();

            return View("add-country", obj);
        }

        [SessionExpireFilter]
        public ActionResult Deletecountry(int id)
        {
            try
            {
                SA_Country country = _context.SA_Country.Where(c => c.id == id).FirstOrDefault();
                _context.Entry(country).State = EntityState.Deleted;
                int x = _context.SaveChanges();

                return RedirectToAction("ShowCountryList");
            }
            catch (Exception ex)
            {

                return View("ErrorEventArgs");
            }
        }

        public ActionResult SaveCountry(SA_Country Country)
        {
            if (Country.id == 0)
            {
                var check = _context.SA_Country.Where(w => w.CountryName == Country.CountryName).FirstOrDefault();
                if (check != null)
                {
                    TempData["Message"] = "This country is already exist!";
                    return RedirectToAction("AddCountry");
                }
                else
                {
                    SA_Country Obj = new SA_Country();
                    Obj.CountryName = Country.CountryName;
                    Obj.CountryCode = Country.CountryCode;
                    Obj.Active = true;
                    _context.SA_Country.Add(Obj);
                    _context.SaveChanges();
                }
            }
            else
            {
                var Obj = _context.SA_Country.Where(w => w.id == Country.id).FirstOrDefault();
                Obj.CountryName = Country.CountryName;
                Obj.CountryCode = Country.CountryCode;
                _context.SaveChanges();
                return RedirectToAction("ShowCountryList");

            }
            return RedirectToAction("ShowCountryList");
        }

        [SessionExpireFilter]
        public ActionResult ShowCountryList()
        {
            UserDataStore Obj = new UserDataStore();

            List<SA_Country> CountryList = _context.SA_Country.ToList();

            return View("country-list", CountryList);
        }

        public ActionResult LoadCountryData()
        {
            try
            {

                List<SA_Country> CountryList = _context.SA_Country.ToList();
                return Json(new { data = CountryList }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult SaveState(SA_States model)
        {
            if (model.Id == 0)
            {
                var check = _context.SA_States.Where(w => w.State == model.State).FirstOrDefault();
                if (check != null)
                {
                    TempData["Message"] = "This state is already exist!";
                    return RedirectToAction("AddState");
                }
                else
                {
                    SA_States Obj = new SA_States();
                    Obj.CountryId = model.CountryId;
                    Obj.State = model.State;
                    Obj.CreatedTime = DateTime.Now;
                    _context.SA_States.Add(Obj);
                    _context.SaveChanges();
                }
            }
            else
            {
                var Obj = _context.SA_States.Where(w => w.Id == model.Id).FirstOrDefault();
                Obj.CountryId = model.CountryId;
                Obj.State = model.State;
                _context.SaveChanges();
                return RedirectToAction("ShowStateList");

            }
            return RedirectToAction("ShowStateList");
        }

        [SessionExpireFilter]
        public ActionResult ShowUserList()
        {
            UserDataStore Obj = new UserDataStore();
            List<SA_User> UserList = Obj.GetUserList();
            return View("user-list", UserList);
        }

        [SessionExpireFilter]
        public ActionResult ShowStateList()
        {
            List<SA_States> StateList = _context.SA_States.ToList();
            return View("state-list", StateList);
        }

        [SessionExpireFilter]
        public ActionResult ShowIndustryList()
        {
            //IndustryDataStore Obj = new IndustryDataStore();
            //List<SA_Industry> IndustryList = Obj.GetIndustryList();
            return View("industry-list");
        }

        public ActionResult ShowoverviewList()
        {
            OverviewDataStore Obj = new OverviewDataStore();
            List<SA_priceoverview> overviewList = Obj.GetOverviewList();
            return View("overview-list", overviewList);
        }
        public ActionResult ShowStrategicList()
        {
            StrategicDataStore Obj = new StrategicDataStore();
            List<SA_Mastrategic> strategicList = Obj.GetStrategicList();
            return View("strategic-list", strategicList);
        }
        [SessionExpireFilter]
        [HttpPost]
        public ActionResult UpdateStatus(int userId)
        {
            UserDataStore Obj = new UserDataStore();
            Obj.UpdateUserStatus(userId);
            return RedirectToAction("ShowUserList");
        }

        [SessionExpireFilter]
        [HttpPost]
        public ActionResult UpdateCustomerStatus(int customerId)
        {
            CustomerDataStore Obj = new CustomerDataStore();
            Obj.UpdateCustomerStatus(customerId);
            return RedirectToAction("CustomerList");
        }

        [SessionExpireFilter]
        public ActionResult EditUser(int id)
        {
            ChemAnalystContext _context = new ChemAnalystContext();
            UserDataStore Obj = new UserDataStore();
            SA_User obj = Obj.GetUserByid(id);
            SA_UserViewModel Objuser = new SA_UserViewModel();
            Objuser.id = obj.id;
            Objuser.Fname = obj.Fname;
            Objuser.Lname = obj.Lname;
            Objuser.Phone = obj.Phone;
            Objuser.ProfileImage = obj.ProfileImage;
            Objuser.Role = obj.Role;
            Objuser.Email = obj.Email;
            Objuser.UserPassword = obj.UserPassword;
            Objuser.Gender = obj.Gender;
            var customerData = (from User in _context.SA_Role
                                    //  select  { Fname = User.Fname+" "+User.Lname , Phone = User.Phone, Role=User.Role,Email=User.Email,UserPassword=User.Password});
                                select new SelectListItem { Text = User.Role, Value = User.Role }).ToList();
            Objuser.UserRoleList = customerData;
            return View("add-user", Objuser);
        }

        [SessionExpireFilter]
        public ActionResult EditState(int id)
        {
            ChemAnalystContext _context = new ChemAnalystContext();

            SA_States obj = _context.SA_States.Where(w => w.Id == id).FirstOrDefault();

            return View("add-state", obj);
        }

        [SessionExpireFilter]
        public ActionResult Deleteuser(int id)
        {

            UserDataStore Obj = new UserDataStore();
            if (Obj.DeleteUser(id) == true)
            {
                return RedirectToAction("ShowUserList");
            }
            else
            {
                return View("ErrorEventArgs");
            }

        }

        //[SessionExpireFilter]
        //public ActionResult Deletecountry(int id)
        //{
        //    try
        //    {
        //        SA_Country country = _context.SA_Country.Where(c => c.id == id).FirstOrDefault();
        //        _context.Entry(country).State = EntityState.Deleted;
        //        int x = _context.SaveChanges();

        //        return RedirectToAction("ShowCountryList");
        //    }
        //    catch (Exception ex)
        //    {

        //        return View("ErrorEventArgs");
        //    }
        //}

        [SessionExpireFilter]
        public ActionResult Deletestate(int id)
        {
            try
            {
                SA_States state = _context.SA_States.Where(c => c.Id == id).FirstOrDefault();
                _context.Entry(state).State = EntityState.Deleted;
                int x = _context.SaveChanges();

                return RedirectToAction("ShowStateList");
            }
            catch (Exception ex)
            {

                return View("ErrorEventArgs");
            }
        }

        /// <summary>
        /// Get Role data
        /// </summary>
        /// <returns></returns>
        [SessionExpireFilter]
        public ActionResult GetRoleList()
        {
            return View("role");
        }

        [SessionExpireFilter]
        public JsonResult RoleList()
        {
            RoleDataStore Obj = new RoleDataStore();
            List<SA_RoleViewModel> RoleList = Obj.GetRoleList();
            return Json(new { data = RoleList }, JsonRequestBehavior.AllowGet);

        }

        [SessionExpireFilter]
        public ActionResult AddRole()
        {
            SA_RoleViewModel obj = new SA_RoleViewModel();

            return View("add-role", obj);

        }

        [SessionExpireFilter]
        public ActionResult SaveRole(SA_RoleViewModel UserRole)
        {
            UserRole.CreatedTime = DateTime.Now.ToString();
            RoleDataStore Obj = new RoleDataStore();
            if (UserRole.id == 0)
            {
                Obj.AddRole(UserRole);
            }
            else
            {
                Obj.EditRole(UserRole);
            }
            return RedirectToAction("Role");
        }

        [SessionExpireFilter]
        public ActionResult Role()
        {
            RoleDataStore Obj = new RoleDataStore();
            // List<SA_Role> RoleList = Obj.GetRoleList();
            return View("Role");
        }

        [SessionExpireFilter]
        public ActionResult EditRole(int id)
        {

            RoleDataStore Obj = new RoleDataStore();
            SA_RoleViewModel obj = Obj.GetRoleByid(id);
            return View("add-role", obj);
        }

        [SessionExpireFilter]
        public ActionResult DeleteRole(int id)
        {

            RoleDataStore Obj = new RoleDataStore();
            if (Obj.DeleteRole(id) == true)
            {
                return View("role");
            }
            else
            {
                return View("ErrorEventArgs");
            }
        }

        #region Commentary Management


        //public ActionResult LoadCommentaryData()
        //{
        //    List<IndustryVM> listOfIndustryVM = new List<IndustryVM>();
        //    try
        //    {
        //        List<enCommentary> listOfCommentary = new blCommentary(new enCommentary()).ReadAll().FindAll(item=> item.CreatedTime > DateTime.Now.AddYears(-1));

        //        ProductDataStore ObjProduct = new ProductDataStore();
        //        CommentaryDataStore Obj = new CommentaryDataStore();

        //        foreach (var item in listOfCommentary)
        //        {
        //            IndustryVM objIndustryVM = new IndustryVM();
        //            objIndustryVM.id = item.ID;
        //            objIndustryVM.Title = item.Title;
        //            objIndustryVM.Type = item.Type;
        //            objIndustryVM.CreateTime = item.CreatedTime > DateTime.MinValue ? item.CreatedTime.Value.ToString("dd/MM/yyyy") : string.Empty;
        //            objIndustryVM.Product = item.Product > 0 ? (ObjProduct.GetProductByid(item.Product.Value) != null ? ObjProduct.GetProductByid(item.Product.Value).ProductName : string.Empty) : string.Empty;
        //            listOfIndustryVM.Add(objIndustryVM);
        //        }
        //        return Json(new { data = listOfIndustryVM }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { data = listOfIndustryVM }, JsonRequestBehavior.AllowGet);
        //    }

        //}
    
        public ActionResult LoadSectoralData()
        {
            try
            {
                ProductDataStore ObjProduct = new ProductDataStore();
                SectoralReport Obj = new SectoralReport();

                List<IndustryVM> listOfIndustryVM = new List<IndustryVM>();
                foreach (var item in Obj.GetSectoralList())
                {
                    IndustryVM objIndustryVM = new IndustryVM();
                    objIndustryVM.id = item.id;
                    objIndustryVM.Title = item.Title;
                    objIndustryVM.Type = item.Type;
                    objIndustryVM.CreateTime = item.CreatedTime > DateTime.MinValue ? item.CreatedTime.ToString("dd/MM/yyyy") : string.Empty;
                    objIndustryVM.Product = item.Product > 0 ? (ObjProduct.GetProductByid(item.Product) != null ? ObjProduct.GetProductByid(item.Product).ProductName : string.Empty) : string.Empty;
                    listOfIndustryVM.Add(objIndustryVM);
                }
                return Json(new { data = listOfIndustryVM }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw;
            }

        }
        [SessionExpireFilter]
        public ActionResult ShowCommentaryList()
        {
            CommentaryDataStore Obj = new CommentaryDataStore();
            List<SA_Commentary> IndustryList = Obj.GetCommentaryList();
            return View("commentary-list", IndustryList);
        }
        [SessionExpireFilter]
        public ActionResult ShowSectoralList()
        {
            SectoralReport Obj = new SectoralReport();
            List<SA_Sectoral> IndustryList = Obj.GetSectoralList();
            return View("Sectoral-list", IndustryList);
        }
        [SessionExpireFilter]
        public ActionResult AddCommentary(int id = 0)
        {
            CommentaryDataStore objCommentaryDataStore = new CommentaryDataStore();
            SA_Commentary objSA_Commentary = null;
           
            if (id > 0)
            {
                objSA_Commentary = objCommentaryDataStore.GetCommentaryByid(id);
            }

            if (objSA_Commentary == null)
            {
                objSA_Commentary = new SA_Commentary();
                objSA_Commentary.CreatedTime = DateTime.Now;
            }
            else
            {
                int regionFID = 0;
                if (objSA_Commentary.CountryFID.HasValue && objSA_Commentary.CountryFID.Value > 0)
                {
                    List<SA_RegionAndCountryRelation> listOfSA_RegionAndCountryRelation = _context.SA_RegionAndCountryRelation.ToList().
                        FindAll(item => item.CountryFID == objSA_Commentary.CountryFID.Value);

                    if (listOfSA_RegionAndCountryRelation != null && listOfSA_RegionAndCountryRelation.Count > 0)
                        regionFID = listOfSA_RegionAndCountryRelation.FirstOrDefault().RegionFID;
                }
                TempData["RegionFID"] = regionFID;
            }


            return View(objSA_Commentary);

        }

        [SessionExpireFilter]
        public ActionResult AddSectoral(int id = 0)
        {
            SectoralReport objSectroalDataStore = new SectoralReport();
            SA_Sectoral objSA_Sectroal = null;
            if (id > 0)
            {
                objSA_Sectroal = objSectroalDataStore.GetSectroalByid(id);
            }

            if (objSA_Sectroal == null)
            {
                objSA_Sectroal = new SA_Sectoral();
                objSA_Sectroal.CreatedTime = DateTime.Now;
            }
            else
            {
                int regionFID = 0;
                if (objSA_Sectroal.CountryFID.HasValue && objSA_Sectroal.CountryFID.Value > 0)
                {
                    List<SA_RegionAndCountryRelation> listOfSA_RegionAndCountryRelation = _context.SA_RegionAndCountryRelation.ToList().
                        FindAll(item => item.CountryFID == objSA_Sectroal.CountryFID.Value);

                    if (listOfSA_RegionAndCountryRelation != null && listOfSA_RegionAndCountryRelation.Count > 0)
                        regionFID = listOfSA_RegionAndCountryRelation.FirstOrDefault().RegionFID;
                }
                TempData["RegionFID"] = regionFID;
            }


            return View(objSA_Sectroal);

        }
        [ValidateInput(false)]
        public ActionResult SaveCommentary(SA_Commentary model)
        {
            CommentaryDataStore Obj = new CommentaryDataStore();
            model.FlagId = 1;
            model.CreatedBy = Session["User"].ToString();
            model.CreatedDate = DateTime.Now;
            if (!model.CountryFID.HasValue || model.Product < 1 || string.IsNullOrEmpty(model.Title))
            {
                TempData["ErrorMessage"] = "Invalid values.!";
                return RedirectToAction("AddCommentary");
            }


            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/ProductImages"), fileName);
                    file.SaveAs(path);
                    model.ImgPath = fileName;
                }
            }

            if (model.id == 0)
            {
                model.Type = "Chemical Pricing";
                Obj.AddCommentary(model);
            }
            else
            {
                Obj.EditCommentary(model);
            }

            return RedirectToAction("ShowCommentaryList", "Admin");
        }
        [ValidateInput(false)]
        public ActionResult SaveSectoral(SA_Sectoral model)
        {
            SectoralReport Obj = new SectoralReport();
            model.CreatedBy = Session["User"].ToString();
            if (!model.CountryFID.HasValue || model.Product < 1 || string.IsNullOrEmpty(model.Title))
            {
                TempData["ErrorMessage"] = "Invalid values.!";
                return RedirectToAction("AddSectoral");
            }


            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/ProductImages"), fileName);
                    file.SaveAs(path);
                    model.ImgPath = fileName;
                }
            }

            if (model.id == 0)
            {
                model.Type = "Chemical Pricing";
                Obj.AddSectoral(model);
            }
            else
            {
                Obj.EditSectoral(model);
            }

            return RedirectToAction("ShowSectoralList", "Admin");
        }

        [ValidateInput(false)]
        [SessionExpireFilter]
        public ActionResult AddCommentaryMarket(int id = 0)
        {
            CommentaryDataStore objCommentaryDataStore = new CommentaryDataStore();
            SA_Commentary objSA_Commentary = null;
            if (id > 0)
            {
                objSA_Commentary = objCommentaryDataStore.GetCommentaryByid(id);
            }

            if (objSA_Commentary == null)
            {
                objSA_Commentary = new SA_Commentary();
                objSA_Commentary.CreatedTime = DateTime.Now;
            }
            else
            {
                //int regionFID = 0;
                //if (objSA_Commentary.CountryFID.HasValue && objSA_Commentary.CountryFID.Value > 0)
                //{
                //    List<SA_RegionAndCountryRelation> listOfSA_RegionAndCountryRelation = _context.SA_RegionAndCountryRelation.ToList().
                //        FindAll(item => item.CountryFID == objSA_Commentary.CountryFID.Value);

                //    if (listOfSA_RegionAndCountryRelation != null && listOfSA_RegionAndCountryRelation.Count > 0)
                //        regionFID = listOfSA_RegionAndCountryRelation.FirstOrDefault().RegionFID;
                //}
                //objSA_Commentary.RegionFID = regionFID;
            }


            return View(objSA_Commentary);

        }

        [ValidateInput(false)]
        public ActionResult SaveCommentaryMarket(SA_Commentary model)
        {
            CommentaryDataStore Obj = new CommentaryDataStore();

            try
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("Login", "Admin");
                }
                else {
                    model.CreatedBy = Session["User"].ToString();
                    model.FlagId = 1;
                    if (model.Product < 1 || string.IsNullOrEmpty(model.Title))
                    {
                        TempData["ErrorMessage"] = "Invalid values.!";
                        return RedirectToAction("AddCommentary");
                    }


                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];

                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);

                            var path = Path.Combine(Server.MapPath("~/ProductImages"), fileName);
                            file.SaveAs(path);
                            model.ImgPath = fileName;
                        }
                    }

                    if (model.id == 0)
                    {
                        model.CountryFID = model.CountryFID > 0 ? model.CountryFID : null;
                        model.Type = "Market Analysis";
                        Obj.AddCommentary(model);
                    }
                    else
                    {
                        Obj.EditCommentary(model);
                    }
                    return RedirectToAction("ShowCommentaryList", "Admin");

                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [SessionExpireFilter]
        public ActionResult EditCommentary(int id)
        {
            CommentaryDataStore Obj = new CommentaryDataStore();
            SA_Commentary obj = Obj.GetCommentaryByid(id);
            return View("AddIndustry", obj);
        }

        [SessionExpireFilter]
        public ActionResult DeleteCommentary(int id)
        {
            CommentaryDataStore Obj = new CommentaryDataStore();
            if (Obj.DeleteCommentary(id) == true)
            {
                return RedirectToAction("ShowCommentaryList", "Admin");
            }
            else
            {
                return View("ErrorEventArgs");
            }
        }
        #endregion
        public ActionResult EditSectoral(int id)
        {
            SectoralReport Obj = new SectoralReport();
            SA_Sectoral obj = Obj.GetSectroalByid(id);
            return View("AddSectoral", obj);
        }

        [SessionExpireFilter]
        public ActionResult DeleteSectoral(int id)
        {
            SectoralReport Obj = new SectoralReport();
            if (Obj.DeleteSectoral(id) == true)
            {
                return RedirectToAction("ShowSectoralList", "Admin");
            }
            else
            {
                return View("ErrorEventArgs");
            }
        }

        public ActionResult CreateLead()
        {
            return View();
        }

        public ActionResult NoPageFound()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult CustomerLoginDetail()
        {
            CustomerDataStore obj = new CustomerDataStore();
            var model = obj.GetCustoemrLoginDetails();
            return View(model);
        }

        [SessionExpireFilter]

        [HttpPost]
        public JsonResult ImageUpload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/images"), fileName);
                    file.SaveAs(path);
                    return Json(ImageUrlPath + fileName);
                }
            }
            return Json(string.Empty);
        }


        public FileResult DownloadFile(string filePath, string fileName)
        {
            string fullPath = Server.MapPath("~") + "/" + filePath;
            return File(fullPath, "application/vnd.ms-excel", fileName);
        }


        public ActionResult CompanyReport()
        {
            return View();
        }

       
        public ActionResult DescriptionList()
        {
            return View("DescriptionList");
        }

        [SessionExpireFilter]
        public ActionResult AddDescription(int Id = 0)
        {
            ProductdescriptionData obj = new ProductdescriptionData();
            Productdescription objproduct = null;

            //objproduct = new Productdescription();
            if (Id > 0)
            {
                objproduct = obj.GetDescriptionByid(Id);

            }
            if (objproduct == null)
            {
                objproduct = new Productdescription();
            }

            return View(objproduct);

        }
        [ValidateInput(false)]
        public ActionResult SaveProductDescription(Productdescription model)
        {
            ProductdescriptionData obj = new ProductdescriptionData();
            model.CreatedBy = Session["User"].ToString();

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/ProductImages"), fileName);
                    file.SaveAs(path);
                    model.ProductionProcess = fileName;
                }
            }

            if (model.Id == 0)
            {
                obj.Addproductdescription(model);
            }
            else
            {
                obj.Editproductdescription(model);

            }
            return RedirectToAction("DescriptionList", "Admin");

        }
        [SessionExpireFilter]
        public ActionResult DeleteProductDescription(int id)
        {
            ProductdescriptionData Obj = new ProductdescriptionData();
            if (Obj.DeleteProductdescription(id) == true)
            {
                return RedirectToAction("DescriptionList", "Admin");
            }
            else
            {
                return View("ErrorEventArgs");
            }
        }

        //public ActionResult ProductSearch(string CountryName, int ProductId)
        //{
        //    ViewBag.CountryName = CountryName;
        //    ViewBag.ProductId = ProductId;
        //    return View();
        //}

        public ActionResult ProductSearch(int ProductId)
        {
            ViewBag.ProductId = ProductId;
            return View();
        }
        public ActionResult EditDescription(int ID)
        {
            ViewBag.Id = ID;
            return View();
        }

        public ActionResult AboutProduct()
        {
            return View();
        }
        public ActionResult AddAboutProduct()
        {
            return View();
        }
        public ActionResult EditAboutProduct(int ID)
        {
            ViewBag.Id = ID;
            return View();
        }


        public ActionResult ShowIndustryLandingList()
        {
            OverviewDataStore Obj = new OverviewDataStore();
            List<SA_IndustryLanding> overviewList = Obj.GetIndustrylanding();
            return View("Industry-landing", overviewList);
        }

        public ActionResult StatsView()
        {
            return View();
        }


        public JsonResult GetLoginCustomer(enSalesPackageSubscriptions model)
        {
            List<enSalesPackageSubscriptions> ListofCustomerEmail = new List<enSalesPackageSubscriptions>();
            blSalesPackageSubscriptions objBL = new blSalesPackageSubscriptions(new enSalesPackageSubscriptions());
            enSalesPackageSubscriptions ObjenenSalesPackageSubscriptions = new enSalesPackageSubscriptions();

            try
            {
                ListofCustomerEmail = objBL.GetAllCustomerEmailbyCompany(model);
            }
            catch (Exception ex)
            {

            }

            var jsonResult = Json(ListofCustomerEmail, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult CustomerAllLoginDetail(enSalesPackageSubscriptions model)
        {
            List<enSalesPackageSubscriptions> ListofCustomerEmail = new List<enSalesPackageSubscriptions>();
            blSalesPackageSubscriptions objBL = new blSalesPackageSubscriptions(new enSalesPackageSubscriptions());
            enSalesPackageSubscriptions ObjenenSalesPackageSubscriptions = new enSalesPackageSubscriptions();

            try
            {
                ListofCustomerEmail = objBL.GetAllCustomerLoginDetailsbyCompany(model);
            }
            catch (Exception ex)
            {

            }

            var jsonResult = Json(ListofCustomerEmail, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult GetCompanyMasterData(enSalesPackageSubscriptions model)
        {
            List<enSalesPackageSubscriptions> ListofCustomerEmail = new List<enSalesPackageSubscriptions>();
            blSalesPackageSubscriptions objBL = new blSalesPackageSubscriptions(new enSalesPackageSubscriptions());
            enSalesPackageSubscriptions ObjenenSalesPackageSubscriptions = new enSalesPackageSubscriptions();

            try
            {
                ListofCustomerEmail = objBL.GetCompanyMasterData(model);
            }
            catch (Exception ex)
            {

            }

            var jsonResult = Json(ListofCustomerEmail, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetAllLoginProductDetailsbyID(enSalesPackageSubscriptions model)
        {
            List<enSalesPackageSubscriptions> ListofCustomerEmail = new List<enSalesPackageSubscriptions>();
            blSalesPackageSubscriptions objBL = new blSalesPackageSubscriptions(new enSalesPackageSubscriptions());
            enSalesPackageSubscriptions ObjenenSalesPackageSubscriptions = new enSalesPackageSubscriptions();

            try
            {
                ListofCustomerEmail = objBL.GetAllLoginProductDetailsbyID(model);
            }
            catch (Exception ex)
            {

            }

            var jsonResult = Json(ListofCustomerEmail, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


    }


}