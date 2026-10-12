using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChemAnalyst.DAL
{

    public class ProductDataStore
    {
        private ChemAnalystContext _context = new ChemAnalystContext();

        public List<SA_Product> GetProductListAdmin()
        {
            return _context.SA_Product.ToList();

        }
        public List<SA_Product> GetProductList()
        {
            return _context.SA_Product.Where(x => x.status == 1).ToList();

        }
        public IQueryable<SA_Product> GetProductListBySearch(string id, string search)
        {
            IQueryable<SA_Product> lst;
            if (id != "" && search != "")
            {
                var ids = Convert.ToInt32(id);
                lst = from r in _context.SA_Product
                      join p in _context.SA_Category on r.Category equals ids
                      where p.status == 1 && r.ProductName.Contains(search) || search == null || search == ""
                      select r;
            }
            else if (id == "" && search != "")
            {

                lst = from r in _context.SA_Product
                      where r.status == 1 && r.ProductName.Contains(search) || search == null || search == ""
                      select r;
            }
            else if (id != "" && search == "")
            {
                var ids = Convert.ToInt32(id);
                lst = from r in _context.SA_Product
                      join p in _context.SA_Category on r.Category equals ids
                      where p.status == 1
                      select r;

            }
            else
            {
                lst = from r in _context.SA_Product
                      where r.status == 1
                      select r;
            }
            return lst.Distinct();

        }

        public bool EditProduct(SA_Product Product)
        {
            //  Product.CreatedDate = DateTime.Now;
            SA_Product EditProduct = _context.SA_Product.Where(Cat => Cat.id == Product.id).FirstOrDefault();
            EditProduct.ProductName = Product.ProductName;
            EditProduct.ProductDiscription = Product.ProductDiscription;
            EditProduct.Meta = Product.Meta;
            EditProduct.MetaDiscription = Product.MetaDiscription;

            EditProduct.RegionFID = Product.RegionFID;
            EditProduct.CountryFID = Product.CountryFID;
            EditProduct.Category = Product.Category;
            EditProduct.status = Product.status;
            EditProduct.MarketAnalysisStatus = Product.MarketAnalysisStatus;



            if (Product.ProductImg != null)
                EditProduct.ProductImg = Product.ProductName;
            _context.Entry(EditProduct).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }

        public bool UpdateStatus(int productId)
        {
            //  Product.CreatedDate = DateTime.Now;
            SA_Product EditProduct = _context.SA_Product.Where(Cat => Cat.id == productId).FirstOrDefault();
            if (EditProduct.status.HasValue)
            {
                if (EditProduct.status.Value == 1)
                {
                    EditProduct.status = 2;
                }
                else
                {
                    EditProduct.status = 1;
                }
            }
            else
            {
                EditProduct.status = 1;
            }
            _context.Entry(EditProduct).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }


        public bool DeleteProduct(int ProductId)
        {
            //  Product.CreatedDate = DateTime.Now;
            SA_Product EditProduct = _context.SA_Product.Where(Product => Product.id == ProductId).FirstOrDefault();
            _context.Entry(EditProduct).State = EntityState.Deleted;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }
        public async Task<bool> AddProduct(SA_Product Product)
        {
            //  Product.CreatedDate = DateTime.Now;
            _context.SA_Product.Add(Product);
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        public async Task<bool> UpdateProduct(SA_Product Product)
        {
            _context.Entry(Product).State = EntityState.Modified;
            //  Product.ModeifiedDate = DateTime.Now;
            int x = await _context.SaveChangesAsync();
            return x == 0 ? false : true;
        }

        //internal SA_Product GetProductByid(int id)
        //{
        //    return _context.SA_Product.Where(x => x.id == id && x.status == 1).SingleOrDefault();
        //}

        internal SA_Product GetProductByid(int id)
        {
            return _context.SA_Product.Where(x => x.id == id ).SingleOrDefault();
        }
        internal bool CheckCategory(int id)
        {
            var data = _context.SA_Product.Where(x => x.Category == id && x.status == 1).SingleOrDefault();
            if (data != null)
                return true;
            else
                return false;
        }

        internal List<SelectListItem> ProductList()
        {
            return (from product in _context.SA_Product
                    where product.status == 1
                    //  select  { Fname = User.Fname+" "+User.Lname , Phone = User.Phone, Role=User.Role,Email=User.Email,UserPassword=User.Password});
                    select new SelectListItem { Text = product.ProductName, Value = product.id.ToString() }).OrderBy(w => w.Text).ToList();
        }

        internal List<SelectListItem> ProductListByCategory(int id)
        {
            return (from product in _context.SA_Product

                    where product.Category == id && product.status == 1
                    orderby product.ProductName  //  select  { Fname = User.Fname+" "+User.Lname , Phone = User.Phone, Role=User.Role,Email=User.Email,UserPassword=User.Password});
                    select new SelectListItem { Text = product.ProductName, Value = product.id.ToString() }).OrderBy(w => w.Text).ToList();
        }
        internal List<SelectListItem> ProductListByCategoryUser(int id, int CustId)
        {
            return (from product in _context.SA_Product
                    join cust in _context.CustProduct on product.id equals cust.ProdId
                    where product.Category == id && cust.CustId == CustId && product.status == 1
                    orderby product.ProductName
                    select new SelectListItem { Text = product.ProductName, Value = product.id.ToString() }).OrderBy(w => w.Text).ToList();
            //  select  { Fname = User.Fname+" "+User.Lname , Phone = User.Phone, Role=User.Role,Email=User.Email,UserPassword=User.Password});
        }

        internal List<SelectListItem> CategoryByUser(int id)
        {
            if (id == 0)
                return (from product in _context.SA_Product
                        join category in _context.SA_Category on product.Category equals category.id
                        where product.status == 1
                        orderby category.CategoryName
                        //  select  { Fname = User.Fname+" "+User.Lname , Phone = User.Phone, Role=User.Role,Email=User.Email,UserPassword=User.Password});
                        select new SelectListItem { Text = category.CategoryName, Value = category.id.ToString() }).Distinct().ToList();
            else
                return (from product in _context.SA_Product
                        join category in _context.SA_Category on product.Category equals category.id
                        join cust in _context.CustProduct on product.id equals cust.ProdId
                        where cust.CustId == id && product.status == 1
                        orderby category.CategoryName
                        //  select  { Fname = User.Fname+" "+User.Lname , Phone = User.Phone, Role=User.Role,Email=User.Email,UserPassword=User.Password});
                        select new SelectListItem { Text = category.CategoryName, Value = category.id.ToString() }).Distinct().ToList();

        }

        public static IEnumerable<SelectListItem> GetProduct()
        {
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var cat = (from coun in _context1.SA_Product where coun.status == 1 select new SelectListItem { Text = coun.ProductName, Value = coun.id.ToString() }).AsEnumerable();
            return cat;
        }
        public static IEnumerable<SelectListItem> GetProductForPriceOverview()
        {
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var cat = (from coun in _context1.SA_Product  select new SelectListItem { Text = coun.ProductName, Value = coun.id.ToString() }).AsEnumerable();
            return cat;
        }
        public static IEnumerable<SelectListItem> GetProductForTrial()
        {
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var cat = (from coun in _context1.SA_Product where coun.status == 1 select new SelectListItem { Text = coun.ProductName, Value = coun.ProductName }).AsEnumerable();
            return cat;
        }

        public static IEnumerable<SelectListItem> GetCountry()
        {
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var cat = (from coun in _context1.SA_Country select new SelectListItem { Text = coun.CountryName, Value = coun.id.ToString() }).AsEnumerable();
            return cat;
        }

        

        public static IEnumerable<SelectListItem> GetState()
        {
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var cat = (from coun in _context1.SA_States select new SelectListItem { Text = coun.State, Value = coun.Id.ToString() }).AsEnumerable();
            return cat;
        }

        public static List<SelectListItem> GetUserType()
        {
            List<SelectListItem> cat = new List<SelectListItem>();
            cat.Add(new SelectListItem { Text = "Manufacturer", Value = "Manufacturer" });
            cat.Add(new SelectListItem { Text = "Trader", Value = "Trader" });
            cat.Add(new SelectListItem { Text = "Executive", Value = "Executive" });
            cat.Add(new SelectListItem { Text = "Industry Expert", Value = "Industry Expert" });
            cat.Add(new SelectListItem { Text = "Customer", Value = "Customer" });
            return cat;
        }
        public static List<SelectListItem> GetCommentaryRange()
        {
            List<SelectListItem> cat = new List<SelectListItem>();
            cat.Add(new SelectListItem { Text = "Capacity By Company", Value = "Company" });
            cat.Add(new SelectListItem { Text = "Capacity By Location", Value = "Location" });
            cat.Add(new SelectListItem { Text = "Capacity By Technology", Value = "Technology" });
            cat.Add(new SelectListItem { Text = "Capacity By Process", Value = "Process" });
            cat.Add(new SelectListItem { Text = "Production By Company", Value = "Production" });
            cat.Add(new SelectListItem { Text = "Operating Efficiency", Value = "Operating Efficiency" });
            cat.Add(new SelectListItem { Text = "Demand By End Use", Value = "Demand By EndUse(T)" });
            cat.Add(new SelectListItem { Text = "Demand By Grade", Value = "Demand By Grade(T)" });
            cat.Add(new SelectListItem { Text = "Demand By Type", Value = "Demand By Type(T)" });
            cat.Add(new SelectListItem { Text = "Demand By Sales Channel", Value = "Demand By SalesChannel(T)" });
            cat.Add(new SelectListItem { Text = "Demand By Region", Value = "Demand By Region(T)" });
            cat.Add(new SelectListItem { Text = "Country-wise Export", Value = "Demand By TradeExport" });
            cat.Add(new SelectListItem { Text = "Country-wise Import", Value = "Demand By TradeImport" });
            cat.Add(new SelectListItem { Text = "Demand-Supply Gap", Value = "Demand Supply Gap" });
            cat.Add(new SelectListItem { Text = "Company Share", Value = "Demand By CompanyShares(T)" });
            cat.Add(new SelectListItem { Text = "Company Share", Value = "Demand By CompanyShares(T)" });
            cat.Add(new SelectListItem { Text = "Customer Analysis", Value = "Customer Analysis" });
            cat.Add(new SelectListItem { Text = "Port Wise Import", Value = "Port Wise Import" });
            cat.Add(new SelectListItem { Text = "Port Wise Export", Value = "Port Wise Export" });
            cat.Add(new SelectListItem { Text = "Supplier Analysis", Value = "Supplier Analysis" });
            return cat;
        }

        public static List<SelectListItem> GetArea()
        {
            List<SelectListItem> cat = new List<SelectListItem>();

            cat.Add(new SelectListItem { Text = "2-Ethyl Hexanol", Value = "2-Ethyl Hexanol" });
            cat.Add(new SelectListItem { Text = "Acetaldehyde", Value = "Acetaldehyde" });
            cat.Add(new SelectListItem { Text = "Acetic Anhydride", Value = "Acetic Anhydride" });
            cat.Add(new SelectListItem { Text = "Acetone", Value = "Acetone" });
            cat.Add(new SelectListItem { Text = "Acetylene", Value = "Acetylene" });
            cat.Add(new SelectListItem { Text = "Acid Chloride", Value = "Acid Chloride" });
            cat.Add(new SelectListItem { Text = "Acrylate", Value = "Acrylate" });
            cat.Add(new SelectListItem { Text = "Acrylate Esters", Value = "Acrylate Esters" });
            cat.Add(new SelectListItem { Text = "Acrylic Acid", Value = "Acrylic Acid" });
            cat.Add(new SelectListItem { Text = "Acrylic Staple Fiber", Value = "Acrylic Staple Fiber" });
            cat.Add(new SelectListItem { Text = "Acrylonitrile", Value = "Acrylonitrile" });
            cat.Add(new SelectListItem { Text = "Acrylonitrile Butadiene Rubber (NBR)", Value = "Acrylonitrile Butadiene Rubber (NBR)" });
            cat.Add(new SelectListItem { Text = "Acrylonitrile Butadiene Styrene (ABS)", Value = "Acrylonitrile Butadiene Styrene (ABS)" });
            cat.Add(new SelectListItem { Text = "Adipic Acid", Value = "Adipic Acid" });
            cat.Add(new SelectListItem { Text = "Alcohol Ethoxylates", Value = "Alcohol Ethoxylates" });
            cat.Add(new SelectListItem { Text = "Aluminum Fluoride", Value = "Aluminum Fluoride" });
            cat.Add(new SelectListItem { Text = "Amino Acid", Value = "Amino Acid" });
            cat.Add(new SelectListItem { Text = "Ammonia", Value = "Ammonia" });
            cat.Add(new SelectListItem { Text = "Ammonium Sulphate", Value = "Ammonium Sulphate" });
            cat.Add(new SelectListItem { Text = "Aniline", Value = "Aniline" });
            cat.Add(new SelectListItem { Text = "Automotive Lubricants", Value = "Automotive Lubricants" });
            cat.Add(new SelectListItem { Text = "Azo Dye", Value = "Azo Dye" });
            cat.Add(new SelectListItem { Text = "Base Oil", Value = "Base Oil" });
            cat.Add(new SelectListItem { Text = "Basic Dye", Value = "Basic Dye" });
            cat.Add(new SelectListItem { Text = "Benzene", Value = "Benzene" });
            cat.Add(new SelectListItem { Text = "Biodiesel", Value = "Biodiesel" });
            cat.Add(new SelectListItem { Text = "Bisphenol A", Value = "Bisphenol A" });
            cat.Add(new SelectListItem { Text = "Bromine", Value = "Bromine" });
            cat.Add(new SelectListItem { Text = "Bulk Nitrogen", Value = "Bulk Nitrogen" });
            cat.Add(new SelectListItem { Text = "Bulk Oxygen", Value = "Bulk Oxygen" });
            cat.Add(new SelectListItem { Text = "Butadiene", Value = "Butadiene" });
            cat.Add(new SelectListItem { Text = "Butanediol", Value = "Butanediol" });
            cat.Add(new SelectListItem { Text = "Butene-1", Value = "Butene-1" });
            cat.Add(new SelectListItem { Text = "Butyl Acetate", Value = "Butyl Acetate" });
            cat.Add(new SelectListItem { Text = "Butyl Acrylate", Value = "Butyl Acrylate" });
            cat.Add(new SelectListItem { Text = "Butyl Rubber", Value = "Butyl Rubber" });
            cat.Add(new SelectListItem { Text = "Calcium Carbide", Value = "Calcium Carbide" });
            cat.Add(new SelectListItem { Text = "Calcium Carbonate", Value = "Calcium Carbonate" });
            cat.Add(new SelectListItem { Text = "Calcium Hydroxide", Value = "Calcium Hydroxide" });
            cat.Add(new SelectListItem { Text = "Calcium Nitrate", Value = "Calcium Nitrate" });
            cat.Add(new SelectListItem { Text = "Caprolactam", Value = "Caprolactam" });
            cat.Add(new SelectListItem { Text = "Carbon Black", Value = "Carbon Black" });
            cat.Add(new SelectListItem { Text = "Caustic Potash", Value = "Caustic Potash" });
            cat.Add(new SelectListItem { Text = "Caustic Soda", Value = "Caustic Soda" });
            cat.Add(new SelectListItem { Text = "Cellulose Ether", Value = "Cellulose Ether" });
            cat.Add(new SelectListItem { Text = "Chloromethane", Value = "Chloromethane" });
            cat.Add(new SelectListItem { Text = "Chlorosulfuric acid", Value = "Chlorosulfuric acid" });
            cat.Add(new SelectListItem { Text = "Copper Sulphate", Value = "Copper Sulphate" });
            cat.Add(new SelectListItem { Text = "Cumene", Value = "Cumene" });
            cat.Add(new SelectListItem { Text = "Cyclohexane", Value = "Cyclohexane" });
            cat.Add(new SelectListItem { Text = "Cyclopentane", Value = "Cyclopentane" });
            cat.Add(new SelectListItem { Text = "Diacetone Alcohol", Value = "Diacetone Alcohol" });
            cat.Add(new SelectListItem { Text = "Diethylene Glycol", Value = "Diethylene Glycol" });
            cat.Add(new SelectListItem { Text = "Dimethyl Terephthalate (DMT)", Value = "Dimethyl Terephthalate (DMT)" });
            cat.Add(new SelectListItem { Text = "Epichlorohydrin", Value = "Epichlorohydrin" });
            cat.Add(new SelectListItem { Text = "Ethanol", Value = "Ethanol" });
            cat.Add(new SelectListItem { Text = "Ethanolamine", Value = "Ethanolamine" });
            cat.Add(new SelectListItem { Text = "Ethoxylate", Value = "Ethoxylate" });
            cat.Add(new SelectListItem { Text = "Ethyl Acetate", Value = "Ethyl Acetate" });
            cat.Add(new SelectListItem { Text = "Ethyl Acrylate", Value = "Ethyl Acrylate" });
            cat.Add(new SelectListItem { Text = "Ethyl Benzene", Value = "Ethyl Benzene" });
            cat.Add(new SelectListItem { Text = "Ethyl Propylene Dimers (EPDM)", Value = "Ethyl Propylene Dimers (EPDM)" });
            cat.Add(new SelectListItem { Text = "Ethyl Tertiary Butyl Ether (ETBE)", Value = "Ethyl Tertiary Butyl Ether (ETBE)" });
            cat.Add(new SelectListItem { Text = "Ethyl Vinyl Alcohol Copolymer", Value = "Ethyl Vinyl Alcohol Copolymer" });
            cat.Add(new SelectListItem { Text = "Ethylene", Value = "Ethylene" });
            cat.Add(new SelectListItem { Text = "Ethylene Acrylic Elastomer", Value = "Ethylene Acrylic Elastomer" });
            cat.Add(new SelectListItem { Text = "Ethylene Dichloride (EDC)", Value = "Ethylene Dichloride (EDC)" });
            cat.Add(new SelectListItem { Text = "Ethylene Oxide", Value = "Ethylene Oxide" });
            cat.Add(new SelectListItem { Text = "Expanded Polystyrene", Value = "Expanded Polystyrene" });
            cat.Add(new SelectListItem { Text = "Fatty Alcohols", Value = "Fatty Alcohols" });
            cat.Add(new SelectListItem { Text = "Gasoline", Value = "Gasoline" });
            cat.Add(new SelectListItem { Text = "Glycol Ether", Value = "Glycol Ether" });
            cat.Add(new SelectListItem { Text = "Halo Butyl Rubber", Value = "Halo Butyl Rubber" });
            cat.Add(new SelectListItem { Text = "Heavy Naphtha", Value = "Heavy Naphtha" });
            cat.Add(new SelectListItem { Text = "Helium gas", Value = "Helium gas" });
            cat.Add(new SelectListItem { Text = "Hydrochloric Acid", Value = "Hydrochloric Acid" });
            cat.Add(new SelectListItem { Text = "Hydrogen", Value = "Hydrogen" });
            cat.Add(new SelectListItem { Text = "Hydrogen Peroxide", Value = "Hydrogen Peroxide" });
            cat.Add(new SelectListItem { Text = "Hydroquinone", Value = "Hydroquinone" });
            cat.Add(new SelectListItem { Text = "Hydroxyapatite", Value = "Hydroxyapatite" });
            cat.Add(new SelectListItem { Text = "Industrial Lubricant", Value = "Industrial Lubricant" });
            cat.Add(new SelectListItem { Text = "Iso-Butanol", Value = "Iso-Butanol" });
            cat.Add(new SelectListItem { Text = "Isobutyl Benzene", Value = "Isobutyl Benzene" });
            cat.Add(new SelectListItem { Text = "Isobutylene", Value = "Isobutylene" });
            cat.Add(new SelectListItem { Text = "Isocyanates", Value = "Isocyanates" });
            cat.Add(new SelectListItem { Text = "Isoprene Rubber", Value = "Isoprene Rubber" });
            cat.Add(new SelectListItem { Text = "Isopropanol", Value = "Isopropanol" });
            cat.Add(new SelectListItem { Text = "Iso-Propranol", Value = "Iso-Propranol" });
            cat.Add(new SelectListItem { Text = "Jet Kerosene", Value = "Jet Kerosene" });
            cat.Add(new SelectListItem { Text = "Leather Chemical", Value = "Leather Chemical" });
            cat.Add(new SelectListItem { Text = "Linear Alkyl Benzene", Value = "Linear Alkyl Benzene" });
            cat.Add(new SelectListItem { Text = "Linear Alpha Olefin", Value = "Linear Alpha Olefin" });
            cat.Add(new SelectListItem { Text = "Liquid Carbon Dioxide", Value = "Liquid Carbon Dioxide" });
            cat.Add(new SelectListItem { Text = "Liquid Chlorine", Value = "Liquid Chlorine" });
            cat.Add(new SelectListItem { Text = "Liquid Sulphuric Dioxide", Value = "Liquid Sulphuric Dioxide" });
            cat.Add(new SelectListItem { Text = "LLDPE", Value = "LLDPE" });
            cat.Add(new SelectListItem { Text = "LPG", Value = "LPG" });
            cat.Add(new SelectListItem { Text = "Magnesium Sulphate", Value = "Magnesium Sulphate" });
            cat.Add(new SelectListItem { Text = "Maleic Anhydride", Value = "Maleic Anhydride" });
            cat.Add(new SelectListItem { Text = "Malic acid", Value = "Malic acid" });
            cat.Add(new SelectListItem { Text = "Masterbatch", Value = "Masterbatch" });
            cat.Add(new SelectListItem { Text = "Melamine", Value = "Melamine" });
            cat.Add(new SelectListItem { Text = "Methacrylate Butadiene Styrene", Value = "Methacrylate Butadiene Styrene" });
            cat.Add(new SelectListItem { Text = "Methyl Ethyl Ketone (MEK)", Value = "Methyl Ethyl Ketone (MEK)" });
            cat.Add(new SelectListItem { Text = "Methyl Isobutyl Ketone (MIBK)", Value = "Methyl Isobutyl Ketone (MIBK)" });
            cat.Add(new SelectListItem { Text = "Methyl Methacrylate", Value = "Methyl Methacrylate" });
            cat.Add(new SelectListItem { Text = "Methylene Dichloride", Value = "Methylene Dichloride" });
            cat.Add(new SelectListItem { Text = "Methylene Diphenyl Diisocyanate", Value = "Methylene Diphenyl Diisocyanate" });
            cat.Add(new SelectListItem { Text = "Mixed Xylene", Value = "Mixed Xylene" });
            cat.Add(new SelectListItem { Text = "m-LLDPE", Value = "m-LLDPE" });
            cat.Add(new SelectListItem { Text = "Mono Propylene Glycol", Value = "Mono Propylene Glycol" });
            cat.Add(new SelectListItem { Text = "Monochloroacetic acid", Value = "Monochloroacetic acid" });
            cat.Add(new SelectListItem { Text = "MTBE", Value = "MTBE" });
            cat.Add(new SelectListItem { Text = "m-Xylene", Value = "m-Xylene" });
            cat.Add(new SelectListItem { Text = "Naphthalene", Value = "Naphthalene" });
            cat.Add(new SelectListItem { Text = "Naphthol", Value = "Naphthol" });
            cat.Add(new SelectListItem { Text = "n-Butanol", Value = "n-Butanol" });
            cat.Add(new SelectListItem { Text = "Nitrile Butadiene Rubber", Value = "Nitrile Butadiene Rubber" });
            cat.Add(new SelectListItem { Text = "Nitro Benzene", Value = "Nitro Benzene" });
            cat.Add(new SelectListItem { Text = "Nitro Toluene", Value = "Nitro Toluene" });
            cat.Add(new SelectListItem { Text = "Non-Phthalate Plasticizer", Value = "Non-Phthalate Plasticizer" });
            cat.Add(new SelectListItem { Text = "Nylon Filament Yarn", Value = "Nylon Filament Yarn" });
            cat.Add(new SelectListItem { Text = "Nylon Tire Yarn", Value = "Nylon Tire Yarn" });
            cat.Add(new SelectListItem { Text = "Oleic Acid", Value = "Oleic Acid" });
            cat.Add(new SelectListItem { Text = "Ortho Nitro Toluene", Value = "Ortho Nitro Toluene" });
            cat.Add(new SelectListItem { Text = "o-Xylene", Value = "o-Xylene" });
            cat.Add(new SelectListItem { Text = "Paraffin Wax", Value = "Paraffin Wax" });
            cat.Add(new SelectListItem { Text = "PBT", Value = "PBT" });
            cat.Add(new SelectListItem { Text = "PET Resin", Value = "PET Resin" });
            cat.Add(new SelectListItem { Text = "Phenol", Value = "Phenol" });
            cat.Add(new SelectListItem { Text = "Phenolic Resin", Value = "Phenolic Resin" });
            cat.Add(new SelectListItem { Text = "Phosphoric Acid", Value = "Phosphoric Acid" });
            cat.Add(new SelectListItem { Text = "Phthalic Anhydride", Value = "Phthalic Anhydride" });
            cat.Add(new SelectListItem { Text = "Poly Butadiene Rubber", Value = "Poly Butadiene Rubber" });
            cat.Add(new SelectListItem { Text = "Poly Methyl Methacrylate", Value = "Poly Methyl Methacrylate" });
            cat.Add(new SelectListItem { Text = "Polyamide", Value = "Polyamide" });
            cat.Add(new SelectListItem { Text = "Polycarbonate", Value = "Polycarbonate" });
            cat.Add(new SelectListItem { Text = "Polyester Filament Yarn", Value = "Polyester Filament Yarn" });
            cat.Add(new SelectListItem { Text = "Polyester Staple Fiber", Value = "Polyester Staple Fiber" });
            cat.Add(new SelectListItem { Text = "Polyether Polyols", Value = "Polyether Polyols" });
            cat.Add(new SelectListItem { Text = "Polyethylene Terephthalate (PET)", Value = "Polyethylene Terephthalate (PET)" });
            cat.Add(new SelectListItem { Text = "Polyisobutylene", Value = "Polyisobutylene" });
            cat.Add(new SelectListItem { Text = "Polyol", Value = "Polyol" });
            cat.Add(new SelectListItem { Text = "Polyolefin Elastomer", Value = "Polyolefin Elastomer" });
            cat.Add(new SelectListItem { Text = "Polyoxymethylene", Value = "Polyoxymethylene" });
            cat.Add(new SelectListItem { Text = "Polypropylene Filament Yarn", Value = "Polypropylene Filament Yarn" });
            cat.Add(new SelectListItem { Text = "Polytetrafluoroethylene", Value = "Polytetrafluoroethylene" });
            cat.Add(new SelectListItem { Text = "Polyurethane Adhesive & Sealant", Value = "Polyurethane Adhesive & Sealant" });
            cat.Add(new SelectListItem { Text = "Polyvinyl Alcohol", Value = "Polyvinyl Alcohol" });
            cat.Add(new SelectListItem { Text = "Potassium Chloride", Value = "Potassium Chloride" });
            cat.Add(new SelectListItem { Text = "Propylene", Value = "Propylene" });
            cat.Add(new SelectListItem { Text = "Propylene Glycol", Value = "Propylene Glycol" });
            cat.Add(new SelectListItem { Text = "Propylene Glycol Ether", Value = "Propylene Glycol Ether" });
            cat.Add(new SelectListItem { Text = "Propylene Oxide", Value = "Propylene Oxide" });
            cat.Add(new SelectListItem { Text = "p-Xylene", Value = "p-Xylene" });
            cat.Add(new SelectListItem { Text = "Raffinate", Value = "Raffinate" });
            cat.Add(new SelectListItem { Text = "Reactive Dyes", Value = "Reactive Dyes" });
            cat.Add(new SelectListItem { Text = "Silicon Additive", Value = "Silicon Additive" });
            cat.Add(new SelectListItem { Text = "Soda Ash", Value = "Soda Ash" });
            cat.Add(new SelectListItem { Text = "Sodium Bicarbonate", Value = "Sodium Bicarbonate" });
            cat.Add(new SelectListItem { Text = "Sodium Chlorate", Value = "Sodium Chlorate" });
            cat.Add(new SelectListItem { Text = "Sodium Hydroxide", Value = "Sodium Hydroxide" });
            cat.Add(new SelectListItem { Text = "Sodium Hypochlorite", Value = "Sodium Hypochlorite" });
            cat.Add(new SelectListItem { Text = "Sodium Silicate", Value = "Sodium Silicate" });
            cat.Add(new SelectListItem { Text = "Sodium Sulphite", Value = "Sodium Sulphite" });
            cat.Add(new SelectListItem { Text = "Sorbic acid", Value = "Sorbic acid" });
            cat.Add(new SelectListItem { Text = "Stearic Acid", Value = "Stearic Acid" });
            cat.Add(new SelectListItem { Text = "Styrene", Value = "Styrene" });
            cat.Add(new SelectListItem { Text = "Styrene Acrylonitrile (SAN)", Value = "Styrene Acrylonitrile (SAN)" });
            cat.Add(new SelectListItem { Text = "Styrene Butadiene Rubber", Value = "Styrene Butadiene Rubber" });
            cat.Add(new SelectListItem { Text = "Succinic Acid", Value = "Succinic Acid" });
            cat.Add(new SelectListItem { Text = "Sulphur", Value = "Sulphur" });
            cat.Add(new SelectListItem { Text = "Sulphuric Acid", Value = "Sulphuric Acid" });
            cat.Add(new SelectListItem { Text = "Textile Chemicals", Value = "Textile Chemicals" });
            cat.Add(new SelectListItem { Text = "Thermoplastic Elastomer", Value = "Thermoplastic Elastomer" });
            cat.Add(new SelectListItem { Text = "Titanium Di-oxide", Value = "Titanium Di-oxide" });
            cat.Add(new SelectListItem { Text = "Toluene", Value = "Toluene" });
            cat.Add(new SelectListItem { Text = "Toluene Di-Isocyanate", Value = "Toluene Di-Isocyanate" });
            cat.Add(new SelectListItem { Text = "Vinyl Acetate Monomer (VAM)", Value = "Vinyl Acetate Monomer (VAM)" });
            cat.Add(new SelectListItem { Text = "Vinyl Chloride Monomer (VCM) ", Value = "Vinyl Chloride Monomer (VCM) " });
            cat.Add(new SelectListItem { Text = "Zinc Sulphate", Value = "Zinc Sulphate" });


            return cat;
        }


        public static IEnumerable<SelectListItem> GetUniqueYears()
        {
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var cat = (from coun in _context1.SA_MarketbyComp select new SelectListItem { Text = coun.year, Value = coun.year.ToString() }).OrderBy(w => w.Value).Distinct().AsEnumerable();
            return cat.ToList().OrderBy(w => w.Value);
        }

        public static IEnumerable<SelectListItem> GetUniqueYearsChemical()
        {
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var cat = (from coun in _context1.SA_ChemPriceYearly select new SelectListItem { Text = coun.year, Value = coun.year.ToString() }).OrderBy(w => w.Value).Distinct().AsEnumerable();
            return cat.ToList().OrderBy(w => w.Value);
        }

        public static bool GetProductNewReletion(int newid, int product)
        {
            ChemAnalystContext _context1 = new ChemAnalystContext();
            return _context1.SA_NewsAndProductRelation.Where(x => x.SA_NewsId == newid && x.SA_ProductId == product).Count() == 0 ? false : true;

        }
        public static bool GetProductCompanyReletion(int companyid, int product)
        {
            ChemAnalystContext _context1 = new ChemAnalystContext();
            return _context1.CompanyAndProductRelations.Where(x => x.SA_CompanyId == companyid && x.SA_ProductId == product).Count() == 0 ? false : true;

        }

        //public static bool GetCompanyProdduct(int companyid)
        //{
        //    ChemAnalystContext _context1 = new ChemAnalystContext();
        //    return _context1.CompanyAndProductRelations.Where(x => x.SA_CompanyId == companyid).FirstOrDefault();

        //}

        public static bool GetProductDealReletion(int newid, int product)
        {
            ChemAnalystContext _context1 = new ChemAnalystContext();
            return _context1.SA_DealsAndProductRelation.Where(x => x.SA_DealID == newid && x.SA_ProductId == product).Count() == 0 ? false : true;

        }

        public static string GetProductName(int ProductId)
        {
            string ProductName = "";
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var cat = _context1.SA_Product.Where(w => w.id == ProductId).FirstOrDefault();

            if (cat != null)
            {
                ProductName = cat.ProductName;
            }
            else
            {
                ProductName = "N/A";
            }
            return ProductName;
        }

        public static string GetCountryName(int CountryFID)
        {
            string result = string.Empty;
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var objCountry = _context1.SA_Country.Where(item => item.id == CountryFID).FirstOrDefault();

            result = objCountry != null ? objCountry.CountryName : string.Empty;
            return result;
        }

        public static string GetRegionName(int RegionFID)
        {
            string result = string.Empty;
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var objRegion = _context1.SA_Region.Where(item => item.id == RegionFID).FirstOrDefault();

            result = objRegion != null ? objRegion.RegionName : string.Empty;
            return result;
        }

        public static string GetModuleName(int PageId)
        {
            string PageName = "";
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var cat = _context1.SA_PageList.Where(w => w.id == PageId).FirstOrDefault();
            if (cat != null)
            {
                PageName = cat.PageDiscription;
            }
            else
            {
                PageName = "N/A";
            }
            return PageName;

        }

        public List<string> GetCustProductList(int custId)
        {
            ChemAnalystContext _context = new ChemAnalystContext();
            List<CustProduct> product = new List<CustProduct>();
            product = _context.CustProduct.Where(x => x.CustId == custId).ToList();
            List<string> lst = product.Select(e => e.ProdId.ToString()).ToList();
            return lst;

        }

        public static string GetCategory(int cat)
        {
            ChemAnalystContext _context = new ChemAnalystContext();
            var category = _context.SA_Category.Where(w => w.id == cat).FirstOrDefault() != null ? _context.SA_Category.Where(w => w.id == cat).FirstOrDefault().CategoryName : "";
            return category;

        }

        public static IEnumerable<SelectListItem> GetCompany()
        {
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var cat = (from coun in _context1.SA_Company select new SelectListItem { Text = coun.Name, Value = coun.id.ToString() }).AsEnumerable();
            return cat;
        }

        public static IEnumerable<SelectListItem> GetAdvisory()
        {
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var cat = (from coun in _context1.SA_Advisory select new SelectListItem { Text = coun.AdvisoryName, Value = coun.id.ToString() }).AsEnumerable();
            return cat;
        }

        public static SA_HomeHeader GetHeaderData()
        {
            ChemAnalystContext _context1 = new ChemAnalystContext();
            var cat = _context1.SA_HomeHeader.OrderByDescending(w => w.Id).FirstOrDefault();
            return cat;
        }


        public List<SA_ProductFeedstock> GetProductFeedstock()
        {
            ChemAnalystContext _context = new ChemAnalystContext();
            List<SA_ProductFeedstock> product = new List<SA_ProductFeedstock>();
            product = _context.SA_ProductFeedstock.ToList();
            return product;

        }
        //internal List<SelectListItem> CategoryByCustomer(int id)
        //{
        //    return (from product in _context.SA_Product
        //            join cust in _context.CustProduct on product.id equals cust.ProdId
        //            where cust.CustId == id
        //            //  select  { Fname = User.Fname+" "+User.Lname , Phone = User.Phone, Role=User.Role,Email=User.Email,UserPassword=User.Password});
        //            select new SelectListItem { Text = product.ProductName, Value = category.id.ToString() }).Distinct().ToList();
        //}
        //public  GetProductByList()
        //{
        //    return (from product in _context.SA_Product
        //            join category in _context.SA_Category on product.Category equals category.id
        //            select new 
        //            {
        //                ProductDiscription = category.CategoryName,
        //                ProductName = product.ProductName,
        //                CreatedTime = product.CreatedTime,
        //                id = product.id

        //            }).ToList();
        //}

        public List<Tuple<int, string>> GetProductByCompany(int companyId)
        {
            var list = (from cpr in _context.CompanyAndProductRelations
                        join p in _context.SA_Product on cpr.SA_ProductId equals p.id
                        where cpr.SA_CompanyId == companyId
                        select p).ToList();
            List<Tuple<int, string>> productList = new List<Tuple<int, string>>();
            foreach (var p in list)
            {
                productList.Add(new Tuple<int, string>(p.id, p.ProductName));
            }
            return productList;
        }

        public List<Tuple<int, string>> GetProductByUser(int userId)
        {
            //var result = (from cp in _context.CustProduct
            //              join p in _context.SA_Product on cp.ProdId equals p.id
            //              join sps in _context.SalesPackageSubscription  on cp.ProdId equals p.id
            //              where cp.CustId == userId && cp.PageId==1
            //              select p).Distinct().ToList();

            List<Tuple<int, string>> prods = new List<Tuple<int, string>>();
            var result = _context.SalesPackageSubscription.Where(w => w.UserId == userId && w.ToDate.Value >= DateTime.Now && w.MenuId.Contains("1")).ToList();


            foreach (var p in result)
            {
                string[] ids = p.ProductId.Split(',').ToArray().Distinct().ToArray();
                for (int i = 0; i < ids.Length; i++)
                {
                    ids[i] = ids[i].Trim();
                }
                foreach (var item in ids)
                {
                    int pid = Convert.ToInt32(item);
                    var product = _context.SA_Product.Where(w => w.id == pid).FirstOrDefault();
                    prods.Add(new Tuple<int, string>(product.id, product.ProductName));
                }
            }
            return prods;
        }

        public static int GetFirstProduct()
        {
            ChemAnalystContext _context = new ChemAnalystContext();
            var product = _context.SA_Product.FirstOrDefault(p => p.status == 1);
            if (product != null)
            {
                return product.id;
            }
            return 0;
        }

        public bool UpdateMarketStatus(int productId)
        {
            //  Product.CreatedDate = DateTime.Now;
            SA_Product EditProduct = _context.SA_Product.Where(Cat => Cat.id == productId).FirstOrDefault();
            if (EditProduct.MarketAnalysisStatus.HasValue)
            {
                if (EditProduct.MarketAnalysisStatus.Value == 1)
                {
                    EditProduct.MarketAnalysisStatus = 2;
                }
                else
                {
                    EditProduct.MarketAnalysisStatus = 1;
                }
            }
            else
            {
                EditProduct.MarketAnalysisStatus = 1;
            }
            _context.Entry(EditProduct).State = EntityState.Modified;
            int x = _context.SaveChanges();
            return x == 0 ? false : true;
        }
    }
}