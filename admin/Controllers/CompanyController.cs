using ChemAnalyst.DAL;
using ChemAnalyst.Models;
using ChemAnalyst.ViewModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChemAnalyst.Controllers
{
    public class CompanyController : Controller
    {
        private ChemAnalystContext _context = new ChemAnalystContext();
        CompanyDataStore Obj = new CompanyDataStore();
       
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }


        // GET: AdvisorySolutions
        public ActionResult Company()
        {
            JobDataStore Obj = new JobDataStore();
            return View("Company");
        }

        public ActionResult CompanySWOT()
        {
            JobDataStore Obj = new JobDataStore();
            return View("CompanySWOT");
        }
        // GET: NewsAndDeals
        public ActionResult GetCompanyList()
        {
            return View("Company");
        }

        public JsonResult CompanyList()
        {

            List<SA_Company> NewsList = Obj.GetCompanyList();

            return Json(new { data = NewsList }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CompanyListSWOT()
        {

            List<CompanySWOT> NewsList = _context.SA_Company_SWOT.ToList().Select(w=>new CompanySWOT {
                Id=w.Id,
                CompanyId=w.CompanyId,
                Company=_context.SA_Company.Where(x=>x.id == w.CompanyId).FirstOrDefault().Name
            }).ToList();

            return Json(new { data = NewsList }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddCompany()
        {
            var Model = new SA_Company();
            return View(Model);

        }

        public ActionResult SaveCompany(SA_Company UserNews)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/ProductImages"), fileName);
                    file.SaveAs(path);
                    UserNews.Logo = fileName;



                }
            }
            UserNews.CreatedTime = DateTime.Now;

            if (UserNews.id == 0)
            {
                Obj.AddCompany(UserNews);
            }
            else
            {
                Obj.EditCompany(UserNews);
            }
            return RedirectToAction("Company");
        }


        public ActionResult AddCompanySWOT(int id = 0)
        {
            //NewsDataStore ObjDal = new NewsDataStore();
            //SA_NewsViewModel objCatViewModel = new SA_NewsViewModel();
            //objCatViewModel.ProductList = ObjDal.GetProductList();
            //return View("AddIndustry", objCatViewModel);

            SA_Company_SWOT obj = new SA_Company_SWOT();
            if (id > 0)
            {
                obj = _context.SA_Company_SWOT.Where(w => w.Id == id).FirstOrDefault();
                ViewBag.PId = _context.SA_Product.Where(w => w.id == obj.ProductId).FirstOrDefault().id;
                ViewBag.PName = _context.SA_Product.Where(w=>w.id== obj.ProductId).FirstOrDefault().ProductName;
                //ViewBag.P = GetProductByCompany(id, obj.ProductId.HasValue ? obj.ProductId.Value : 0);
            }

            if (obj == null)
            {
                obj = new SA_Company_SWOT();
            }
            //obj.ProductList = ObjDal.GetProductList();
            //SA_Industry obj = new SA_Industry();
            return View(obj);

        }

        [HttpPost]
        public JsonResult GetCompanyProducts(int id)
        {
            var productList = GetProductByCompany(id);
            return Json(productList);
        }

        private List<SelectListItem> GetProductByCompany(int id, int productId = 0)
        {
            var list = new ProductDataStore().GetProductByCompany(id);
            return list.Select(p => new SelectListItem() { Text = p.Item2, Value = p.Item1.ToString(), Selected = productId == p.Item1 }).ToList();
        }

        [ValidateInput(false)]
        public ActionResult SaveCompanySWOT(SA_Company_SWOT UserNews)
        {
            string fileName = string.Empty;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file != null && file.ContentLength > 0)
                {

                     fileName = Path.GetFileName(file.FileName);
                    int data = Obj.CheckExistingdata(fileName);
                    if (UserNews.Id == 0)
                    {
                        var existing = Obj.GetSWOTByCompanyAndProduct(UserNews.CompanyId, UserNews.ProductId.Value);
                        if (existing == null)
                        {
                            Obj.AddCompanySWOT(UserNews);
                            
                           // int data = CompanyDataStore.CheckExistingdata(fileName);
                        }
                        else
                        {
                            Obj.EditSameCompanySWOT(UserNews);
                           
                        }
                    }
                    else
                    {
                        Obj.EditCompanySWOT(UserNews);
                    }
                    
                    //int data = CompanyDataStore.CheckExistingdata(fileName);
                    string path = Path.Combine(Server.MapPath("~/ProductIndiaMapImportfile"), fileName);
                    file.SaveAs(path);

                    var excel = new ExcelPackage(file.InputStream);
                    DataTable dt = excel.ToDataTable(5);
                    InsertIndiaMapExcelRecords(UserNews.CompanyId,UserNews.ProductId.Value, path, dt, fileName);
                    ViewBag.Status = "Success";
                        //ViewBag.SuccesMessge = "File uploaded under " + productName + " Successfully.";
                   
                }
            }
            return RedirectToAction("CompanySWOT");
        }

        private void InsertIndiaMapExcelRecords(int companyId, int productId, string path, DataTable Exceldt, string fileName)
        {
            try
            {
                Exceldt.Columns.Add("CompanyId", typeof(int));
                Exceldt.Columns.Add("ProductId", typeof(int));
                Exceldt.Columns.Add("CreatedTime", typeof(DateTime));
                Exceldt.Columns.Add("FileName", typeof(string));
                for (int i = Exceldt.Rows.Count - 1; i >= 0; i--)
                {
                    if (Exceldt.Rows[i]["Type"] == DBNull.Value || Exceldt.Rows[i]["Location"] == DBNull.Value || Exceldt.Rows[i]["City"] == DBNull.Value || Exceldt.Rows[i]["Capacity"] == DBNull.Value)
                    {
                        Exceldt.Rows[i].Delete();
                    }
                    else
                    {
                        Exceldt.Rows[i]["CompanyId"] = companyId;
                        Exceldt.Rows[i]["ProductId"] = productId;
                        Exceldt.Rows[i]["CreatedTime"] = DateTime.Now;
                        Exceldt.Rows[i]["FileName"] = fileName;
                    }
                }
                Exceldt.AcceptChanges();

                //inserting Datatable Records to DataBase   
                var connectionString = ConfigurationManager.ConnectionStrings["ChemAnalystContext"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = connectionString;//Connection Details  
                                                                  //creating object of SqlBulkCopy      
                SqlBulkCopy objbulk = new SqlBulkCopy(sqlConnection);
                //assigning Destination table name      
                objbulk.DestinationTableName = "SA_Product_IndiaMapData";
                //Mapping Table column    
                objbulk.ColumnMappings.Add("[CompanyId]", "CompanyId");
                objbulk.ColumnMappings.Add("[ProductId]", "ProductId");
                objbulk.ColumnMappings.Add("[Type]", "Type");
                objbulk.ColumnMappings.Add("[Location]", "Location");
                objbulk.ColumnMappings.Add("[City]", "City");
                objbulk.ColumnMappings.Add("[Capacity]", "Capacity");
                objbulk.ColumnMappings.Add("[CreatedTime]", "CreatedTime");
                objbulk.ColumnMappings.Add("[FileName]", "FileName");

                sqlConnection.Open();
                objbulk.WriteToServer(Exceldt);
                sqlConnection.Close();
            }
            catch
            { }
        }

        public ActionResult EditCompany(int id)
        {
            SA_Company obj = Obj.GetCompanyByid(id);
            return View("AddCompany", obj);
        }

        public ActionResult DeleteCompany(int id)
        {
            if (Obj.DeleteCompany(id) == true)
            {
                return View("Company");
            }
            else
            {
                return View("ErrorEventArgs");
            }
        }

        public ActionResult DeleteCompanySWOT(int id)
        {
            if (Obj.DeleteCompanySWOT(id) == true)
            {
                return RedirectToAction("CompanySWOT");
            }
            else
            {
                return View("ErrorEventArgs");
            }
        }

        
        public ActionResult CompanyProfile()
        {
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
            return View(Obj.GetCompanyList().OrderBy(x => x.Name));
            

        }

        public ActionResult CompanyProfileDetails(int id)
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

            return View(model);

        }

        [HttpPost]
        public ActionResult CompanyProfile(string category, string products, string revsize, string empsize, string companyname)
        {
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
            return View(Obj.GetCompaniesList(category, products, revsize, empsize, "", companyname).OrderBy(x => x.CreatedTime));

        }

        [HttpPost]
        public JsonResult CheckAccess(string ProductId)
        {
            int custid = int.Parse(Session["LoginUser"].ToString());
            if (!string.IsNullOrEmpty(ProductId))
            {
                int PId = int.Parse(ProductId);

                try
                {

                    var IsAccess = _context.CustProduct.Where(w => w.CustId == custid && w.ProdId == PId && w.PageId == 3).OrderByDescending(w => w.id).FirstOrDefault();

                    if (IsAccess == null)
                    {
                        return Json("NoAcesss");
                    }
                    else
                    {
                        return Json("Access");
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            else
            {
                //var Commentaries = ObjCommentary.GetCommentaryList().Select(w => new SelectListItem { Text = w.Title, Value = w.Description }).ToList();
                return Json("ProductNotFound");
            }
        }

        public JsonResult GetVisitorMapData(int companyId)
        {
            int firstProduct = ProductDataStore.GetFirstProduct();
            if(firstProduct != 0)
            {
                CompanyDataStore obj = new CompanyDataStore();
                var mapData = obj.GetMapData(companyId, firstProduct);
                return Json(mapData);
            }
            return Json(new string[0]);
        }

    }
}
