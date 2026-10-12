using BusinessLayer;
using ChemAnalyst.Common;
using ChemAnalyst.DAL;
using ChemAnalyst.Models;
using ChemAnalyst.ViewModel;
using EntityLayer;
using EntityLayer.CustomEntity;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ChemAnalyst.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Productlist()
        {
            ProductDataStore Obj = new ProductDataStore();
            //List<SA_Product> ProductList = Obj.GetProductList();
            return View("Productlist");
        }

        public ActionResult GetProductList()
        {
            return View("Productlist");
        }

        public JsonResult ProductList1()
        {
            ProductDataStore Obj = new ProductDataStore();
            ChemAnalystContext _context = new ChemAnalystContext();
            var list = (from product in _context.SA_Product
                        join category in _context.SA_Category on product.Category equals category.id
                        select new
                        {
                            ProductDiscription = category.CategoryName,
                            ProductName = product.ProductName,
                            CreatedTime = product.CreatedTime,
                            id = product.id,
                            status = product.status,

                        }).ToList();

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Custlogin(FormCollection collection)
        {
            CustWiseAccessDataStore Obj = new CustWiseAccessDataStore();
            SA_Customer login = new SA_Customer();
            ChemAnalystContext _context = new ChemAnalystContext();
            login.Email = Request["Username"];

            login.UserPassword = Request["Password"];

            string language = Request["ddllang"];

            string strEncrypted = (Helpers.Encrypt(login.UserPassword));
            string strDecrypted = (Helpers.Decrypt(strEncrypted));

            login.UserPassword = strEncrypted;

            CustomerDataStore LoginStore = new CustomerDataStore();
            SA_Customer objectuser = LoginStore.CheckCustomer(login);
            if (objectuser != null)
            {
                if (!objectuser.Status)
                {
                    TempData["ErrorMessage"] = "Your account has been suspended.";
                    return RedirectToAction("Index");
                }
                //...........comment by anuj rana ip restrict..............

                //var currentIP = GetIPAddress();
                //var custIP = "121221";
                //var checkcustIP = _context.Lead_Master.Where(w => w.CorpEmail == objectuser.Email).FirstOrDefault();
                //if (checkcustIP != null)
                //    custIP = checkcustIP.IPAddress;

                //if (custIP != currentIP)
                //{
                //    TempData["ErrorMessage"] = "You are  not authorized to login from this IP Address.";
                //    return RedirectToAction("Index");
                //}
                else if (LoginStore.CheckCustomerPackage(login))
                {
                    TempData["ErrorMessage"] = "Your subscription expired.";
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["LoginUser"] = objectuser.id;
                    Session["User"] = objectuser.Fname + " " + objectuser.Lname;

                    string userImage = "/images/" + objectuser.ProfileImage;
                    if (string.IsNullOrEmpty(objectuser.ProfileImage) || string.IsNullOrWhiteSpace(objectuser.ProfileImage))
                        userImage = "/images/no-img.jpg";

                    Session["UserImg"] = userImage;
                    Session["UserRole"] = objectuser.Role;
                    Session["UserEmail"] = objectuser.Email;
                    Session["Language"] = language;
                    List<CustWiseAccess> Access = Obj.GetCustpage(objectuser.id);
                    Session["Access"] = Access;


                    Session["IsInitialPasswordChanged"] = objectuser.IsInitialPasswordChanged;
                    //redirect user to change password if logins for firstime
                    if (!objectuser.IsInitialPasswordChanged)
                    {
                        return RedirectToAction("ChangePassword");
                    }

                    int loginId;
                    LoginStore.AddLoginDetail(objectuser.id, out loginId);
                    Session["LoginId"] = loginId;

                    return RedirectToAction("Dashboard");

                }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid credentials.";
                return RedirectToAction("Index");
            }
            // return View("~/Views/Login/Dashboard.cshtml");
        }

 
        public ActionResult ChemicalPricing()
        {
            return View("chemicalpricingUser");
        }

   

        public ActionResult CustIndustryList(int? page)
        {
            int pageSize = 10;
            IndustryDataStore Obj = new IndustryDataStore();
            string cate = Request.Form["categoryname"] != null ? Request.Form["categoryname"].ToString() : "";
            string country = Request.Form["countryname"] != null ? Request.Form["countryname"].ToString() : "";
            ViewBag.Cat = cate;
            ViewBag.Cou = country;
            CategoryDataStore d = new CategoryDataStore();
            IndustryViewModel1 model = new IndustryViewModel1();
            IPagedList<SA_Industry> IndustryList = Obj.GetIndustryList().Where(x => (cate == "" || cate.Contains(x.CategoryID.ToString() + ",")) && (country == "" || country.Contains(x.CountryID.ToString() + ","))).OrderBy(w => w.id).OrderByDescending(w => w.id).ToList().ToPagedList(page ?? 1, pageSize);
            model.Industry = IndustryList;
            List<SelectListItem> lstCategory = d.CategoryList();
            model.lstCategory = lstCategory;

            List<SelectListItem> lstCountry = d.CountryList();
            model.lstCountry = lstCountry;

            return View("CustIndustry", model);
            //return View("CustIndustry");
        }

        public ActionResult Reportsection(int? page)
        {
            int pageSize = 10;
            IndustryDataStore Obj = new IndustryDataStore();
            string cate = Request.Form["categoryname"] != null ? Request.Form["categoryname"].ToString() : "";
            string country = Request.Form["countryname"] != null ? Request.Form["countryname"].ToString() : "";
            ViewBag.Cat = cate;
            ViewBag.Cou = country;
            CategoryDataStore d = new CategoryDataStore();
            IndustryViewModel1 model = new IndustryViewModel1();
            IPagedList<SA_Industry> IndustryList = Obj.GetIndustryList().Where(x => (cate == "" || cate.Contains(x.CategoryID.ToString() + ",")) && (country == "" || country.Contains(x.CountryID.ToString() + ","))).OrderBy(w => w.id).OrderByDescending(w => w.id).ToList().ToPagedList(page ?? 1, pageSize);
            model.Industry = IndustryList;
            List<SelectListItem> lstCategory = d.CategoryList();
            model.lstCategory = lstCategory;

            List<SelectListItem> lstCountry = d.CountryList();
            model.lstCountry = lstCountry;

            return View("CustIndustry", model);

        }

        public JsonResult GetCustIndustryList()
        {

            var LoginUser = Convert.ToInt32(Session["LoginUser"]);
            ProductDataStore ObjProduct = new ProductDataStore();

            //var custProduct = ObjProduct.GetCustProductList(LoginUser);
            List<string> custProduct = ObjProduct.GetCustProductList(LoginUser);

            IndustryDataStore Obj = new IndustryDataStore();
            var IndustryList = Obj.GetCustIndustryList(custProduct).Select(w => new IndustryVM
            {
                id = w.id,
                Title = w.Title,
                CreateTime = w.CreatedTime != null ? w.CreatedTime.Value.ToString("dd/MM/yyyy") : "",
                Product = w.Product != null ? ObjProduct.GetProductByid(w.Product).ProductName : "",
            }); ;
            return Json(new { data = IndustryList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustCompaniesList()
        {
            CompanyDataStore Obj = new CompanyDataStore();

            ViewBag.Product = Obj.GetCompanyProducts();
            ViewBag.Category = Obj.GetUniqueCategory();
            ViewBag.Fyear = Obj.GetUniqueFyear();
            ViewBag.Cat = "";
            ViewBag.Prod = "";
            ViewBag.RevS = "";
            ViewBag.EmpSiz = "";
            ViewBag.CName = "";
            ViewBag.Fy = DateTime.Now.Year;
            NewsDataStore n = new NewsDataStore();
            ViewBag.f = n.GetFirstProduct();
            return View("CustCompanies", Obj.GetCompanyList().OrderBy(x => x.CreatedTime));
            // return View("CustCompanies");
        }

        [HttpPost]
        public ActionResult CompanyProfile(string category, string products, string revsize, string empsize, string fyear, string companyname)
        {
            CompanyDataStore Obj = new CompanyDataStore();
            NewsDataStore n = new NewsDataStore();
            ViewBag.f = n.GetFirstProduct();
            ViewBag.Cat = category;
            ViewBag.Prod = products;
            ViewBag.RevS = revsize;
            ViewBag.EmpSiz = empsize;
            ViewBag.Fy = "";
            ViewBag.CName = companyname;
            ViewBag.Fyear = Obj.GetUniqueFyear();
            ViewBag.Product = Obj.GetCompanyProducts();
            ViewBag.Category = Obj.GetUniqueCategory();
            return View("CustCompanies", Obj.GetCompaniesList(category, products, revsize, empsize, "", companyname).OrderBy(x => x.CreatedTime));
            //NewsDataStore n = new NewsDataStore();
            //ViewBag.f = n.GetFirstProduct();
            //ViewBag.Cat = category;
            //ViewBag.Prod = products;
            //ViewBag.RevS = revsize;
            //ViewBag.EmpSiz = empsize;
            //ViewBag.Product = Obj.GetCompanyProducts();
            //ViewBag.Category = Obj.GetUniqueCategory();
            //return View("CustCompanies", Obj.GetCompanyList(category, products, revsize, empsize, DateTime.Now.Year.ToString()).OrderBy(x => x.CreatedTime));

        }

        public JsonResult GetCustcompaniesList()
        {
            var LoginUser = Convert.ToInt32(Session["LoginUser"]);

            LeadDAL leadObj = new LeadDAL();
            List<CustCompanyVM> lstCompanies = new List<CustCompanyVM>();
            lstCompanies = leadObj.GetCompanies(LoginUser);

            return Json(new { data = lstCompanies }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CompanyDetail(int id)
        {
            ChemAnalystContext db = new ChemAnalystContext();

            CompanyDataStore obj = new CompanyDataStore();
            CustCompanyVM model = new CustCompanyVM();
            var data = obj.GetCompanyByid(id);
            model.Name = data.Name;
            model.Description = data.Description;
            model.Logo = data.Logo;

            model.EmailId = data.EmailId;
            model.Address = data.Address;
            model.phoneNo = data.phoneNo;
            model.fax = data.fax;
            model.website = data.website;
            model.RegDate = data.RegDate.Date.ToString("dd/MM/yyyy");
            model.CreatedTime = data.RegDate.Date.Year.ToString();
            model.NOE = data.NOE;
            model.CEO = data.CEO;
            model.CIN = data.CIN;
            model.Category = data.Category;
            model.id = data.id;
            model.Meta = data.Meta;
            model.MetaDescription = data.MetaDescription;
            model.lstFinacialData = db.CompanyProfRecordNew.Where(w => w.SA_CompanyId == data.id).Select(x => new CompanyFinacialData
            {
                //FinacialYear = db.FinancialYears.Where(f => f.Id == x.FinancialYearId).FirstOrDefault().FinYear,
                FinacialYear = x.year,
                Growth = x.Growth,
                Revenue = x.Revenue,
                PBT = x.PBT,
                Liablities = x.Liablities,
                Margin = x.Margin,
                Margin1 = x.Margin1,
                Pat = x.Pat


            }).ToList();

            ViewBag.S = obj.GetSWOTByCompany(id);

            int CustId = Convert.ToInt32(Session["LoginUser"]);
            ViewBag.P = obj.GetCompanyProductByCustomer(id, CustId).Select(p => new SelectListItem()
            {
                Text = p.Item2,
                Value = p.Item1.ToString()
            }).ToList();

            var checkFirstCategory = db.SA_Category.OrderBy(w => w.id).FirstOrDefault();
            var checkFirstProductFirstCategory = db.SA_Product.Where(w => w.Category == checkFirstCategory.id).OrderBy(w => w.id).FirstOrDefault();

            var compData = db.CompanyAndProductRelations.Where(w => w.SA_CompanyId == id && w.SA_ProductId == checkFirstProductFirstCategory.id).FirstOrDefault();
            if (compData == null)
            {
                ViewBag.FP = (ViewBag.P as ICollection<SelectListItem>).First().Value;
            }
            else
            {
                ViewBag.FP = checkFirstProductFirstCategory.id;
            }

            return View(model);

        }

        [HttpPost]
        public JsonResult GetMapData(int companyId, int productId)
        {
            CompanyDataStore obj = new CompanyDataStore();
            var mapData = obj.GetMapData(companyId, productId);
            var swotData = obj.GetSWOTByCompanyAndProduct(companyId, productId);
            return Json(new { mapData = mapData, swotData = swotData });
        }

        public ActionResult ChangePassword()
        {
            return View("custChangePassword");
        }

        public ActionResult UpdatePassword(FormCollection ChangePassword)
        {

            int LoginUser = int.Parse(ChangePassword["LoginUser"]);
            string CurPassword = ChangePassword["CurPassword"];
            string newPassword = ChangePassword["newPassword"];
            string confirmpasswd = ChangePassword["confirmpasswd"];
            if (newPassword != confirmpasswd)
            {
                ViewBag.Message = "Your new password and confirm password do not match.";
                return View("cust-update-profile");
            }
            //string strPassword = "1234";

            string strEncryptedCurr = (Helpers.Encrypt(CurPassword));
            string strDecryptedCurr = (Helpers.Decrypt(strEncryptedCurr));

            string strEncrypted = (Helpers.Encrypt(newPassword));
            string strDecrypted = (Helpers.Decrypt(strEncrypted));

            CustomerDataStore ObjUser = new CustomerDataStore();
            SA_Customer loginUser = ObjUser.GetCustomerByid(LoginUser);
            if ((loginUser != null))
            {
                if (loginUser.UserPassword == strEncryptedCurr)
                {
                    loginUser.id = LoginUser;
                    loginUser.UserPassword = strEncrypted;
                    int valid = ObjUser.UpdatePassword(loginUser);
                    if (valid > 0)
                    {
                        Session["IsInitialPasswordChanged"] = true;
                        ViewBag.Message = "Password Updated Successfuly.";
                    }
                    else
                    {
                        ViewBag.Message = "Password not Updated Successfuly.";
                    }
                }
                else
                    ViewBag.Message = "Your current Password is not matched.";
            }
            return View("cust-update-profile");
        }

        public ActionResult ChangeLanguage(FormCollection changeLanguage)
        {
            string language = Request["ddllang"];
            Session["Language"] = language;
            return View("cust-update-profile");
        }

        public ActionResult CustUpdateProfile()
        {
            int id = int.Parse(Session["LoginUser"].ToString());
            ChemAnalystContext _context = new ChemAnalystContext();
            CustomerDataStore Obj = new CustomerDataStore();
            SA_Customer obj = Obj.GetCustomerByid(id);
            SA_CustomerViewModel Objuser = new SA_CustomerViewModel();
            Objuser.id = obj.id;
            Objuser.Fname = obj.Fname;
            Objuser.Lname = obj.Lname;
            Objuser.Phone = obj.Phone;
            Objuser.Role = obj.Role;
            Objuser.Email = obj.Email;
            Objuser.Gender = obj.Gender;
            Objuser.UserPassword = obj.UserPassword;
            Objuser.ProfileImage = obj.ProfileImage;

            var customerData = (from User in _context.SA_Role
                                    //  select  { Fname = User.Fname+" "+User.Lname , Phone = User.Phone, Role=User.Role,Email=User.Email,UserPassword=User.Password});
                                select new SelectListItem { Text = User.Role, Value = User.Role }).ToList();
            Objuser.UserRoleList = customerData;
            return View("cust-update-profile", Objuser);
        }

        public ActionResult CustSaveProfile(SA_Customer User)
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
            CustomerDataStore Obj = new CustomerDataStore();

            Obj.UpdateCustomer(User);

            return RedirectToAction("CustUpdateProfile", "Login");

        }

        public ActionResult LogOut()
        {
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
            Session.Clear();
            return RedirectToAction("Index", "ChemAnalyst");
        }

        private string GetIPAddress()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
            //string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            //Console.WriteLine(hostName);
            //// Get the IP  
            //return Dns.GetHostByName(hostName).AddressList[0].ToString();
        }

        [HttpPost]
        public ActionResult SearchIndustry(string keyword, int? page)
        {
            int pageSize = 10;
            IndustryDataStore Obj = new IndustryDataStore();
            string cate = Request.Form["categoryname"] != null ? Request.Form["categoryname"].ToString() : "";
            string country = Request.Form["countryname"] != null ? Request.Form["countryname"].ToString() : "";
            ViewBag.Cat = cate;
            ViewBag.Cou = country;
            CategoryDataStore d = new CategoryDataStore();
            IndustryViewModel1 model = new IndustryViewModel1();
            IPagedList<SA_Industry> IndustryList;
            if (!string.IsNullOrEmpty(keyword))
            {
                IndustryList = Obj.GetIndustryList().Where(x => (cate == "" || cate.Contains(x.CategoryID.ToString() + ",")) && (country == "" || country.Contains(x.CountryID.ToString() + ","))).OrderBy(w => w.id).OrderByDescending(w => w.id).ToList().ToPagedList(page ?? 1, pageSize);
                IndustryList = IndustryList.Where(w => w.Title.ToLower().Contains(keyword.ToLower())).ToList().ToPagedList(page ?? 1, pageSize);
            }
            else
            {
                IndustryList = Obj.GetIndustryList().Where(x => (cate == "" || cate.Contains(x.CategoryID.ToString() + ",")) && (country == "" || country.Contains(x.CountryID.ToString() + ","))).OrderBy(w => w.id).OrderByDescending(w => w.id).ToList().ToPagedList(page ?? 1, pageSize);
            }

            model.Industry = IndustryList;
            List<SelectListItem> lstCategory = d.CategoryList();
            model.lstCategory = lstCategory;

            List<SelectListItem> lstCountry = d.CountryList();
            model.lstCountry = lstCountry;

            return View("CustIndustry", model);
        }

        public string anuj(string anuj)
        {
            string password = string.Empty;
            try
            {
                //string strEncrypted = (Helpers.Encrypt(login.UserPassword));
                password = (Helpers.Decrypt(anuj));

            }
            catch (Exception ex)
            {
                password = ex.Message.ToString();
            }

            return password;
        }

    }

    public class enLoginDashboard
    {
        public SA_News News { get; set; }

        public List<SA_News> ListOfNews { get; set; }

        public enChemPriceDailyAverage ChemPriceDailyAverage { get; set; }

        public List<enChemPriceDailyAverage> ListOfChePriceDailyAverage { get; set; }
    }

    public class enChemPriceDailyAverage
    {
        public int ID { get; set; }

        public int Product { get; set; }

        public string ProductName { get; set; }

        public string Year { get; set; }

        public string Month { get; set; }

        public string Day { get; set; }

        public DateTime Date { get { return Convert.ToDateTime(Year + "-" + Month + "-" + Day); } }

        public string DateView { get { return Date.ToString("dd-MMM-yy"); } }

        public decimal Count { get; set; }

        public string Country { get; set; }

        public float ChangesPercent { get; set; }

        public string RangeName { get; set; }
    }

}
