using ChemAnalyst.Models;
using ChemAnalyst.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChemAnalyst.DAL
{
    public class DALHelper
    {
        private ChemAnalystContext _context = new ChemAnalystContext();

        //Note: get region and country join with country and region table
        public List<RegionAndCountryRelationViewModel> GetRegionAndCountryRelationViewModel()
        {
            return (from
                          SARCR in _context.SA_RegionAndCountryRelation
                    join SAC in _context.SA_Country on SARCR.CountryFID equals SAC.id
                    join SAR in _context.SA_Region on SARCR.RegionFID equals SAR.id
                    select new RegionAndCountryRelationViewModel()
                    {
                        SA_Region = SAR,
                        SA_Country = SAC,
                        ID = SARCR.Id
                    }).ToList();
        }

        //Note: get list of product and file type mapping
        public List<SA_ProductAndFileTypeRelationViewModel> GetProductAndFileTypeRelationViewModel()
        {
            return (from
                          SARCR in _context.SA_ProductAndFileTypeRelation
                    join SAC in _context.SA_Product on SARCR.ProductFID equals SAC.id
                    join SAR in _context.SA_FileType on SARCR.FileTypeFID equals SAR.ID
                    select new SA_ProductAndFileTypeRelationViewModel()
                    {
                        SA_Product = SAC,
                        SA_FileType = SAR,
                        ID = SARCR.ID
                    }).ToList();
        }

        //Note: get list of product and market range mapping
        public List<SA_ProductAndMarketRangeRelationViewModel> GetSA_ProductAndMarketRangeRelationViewModel()
        {
            return (from
                          SARCR in _context.SA_ProductAndMarketRangeRelation
                    join SAC in _context.SA_Product on SARCR.ProductFID equals SAC.id
                    join SAR in _context.SA_MarketRange on SARCR.MarketRangeFID equals SAR.ID
                    select new SA_ProductAndMarketRangeRelationViewModel()
                    {
                        SA_Product = SAC,
                        SA_MarketRange = SAR,
                        ID = SARCR.ID
                    }).ToList();
        }

        //Note: delete SA_RegionAndCountryRelation records based on region 
        public bool DeleteRegionAndCountryRelationBasedOnRegion(int RegionID)
        {
            try
            {
                _context.Database.ExecuteSqlCommand("delete FROM dbo.SA_RegionAndCountryRelation WHERE regionFID = " + RegionID);

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        //Note: delete SA_ProductAndFileTypeRelation records based on Product
        public bool DeleteSA_ProductAndFileTypeRelation(int productFID)
        {
            try
            {
                _context.Database.ExecuteSqlCommand("delete FROM dbo.SA_ProductAndFileTypeRelation WHERE ProductFID = " + productFID);

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //Note: delete SA_ProductAndMarketRangeRelation records based on Product
        public bool DeleteSA_ProductAndMarketRangeRelation(int productFID)
        {
            try
            {
                _context.Database.ExecuteSqlCommand("delete FROM dbo.SA_ProductAndMarketRangeRelation WHERE ProductFID = " + productFID);

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        public List<SA_Country> GetCountryBasedOnRegion(int regionID)
        {
            List<SA_Country> listOfCountry = new List<SA_Country>();

            List<RegionAndCountryRelationViewModel> list = new List<RegionAndCountryRelationViewModel>();
            list = (from
                      SARCR in _context.SA_RegionAndCountryRelation
                    join SAC in _context.SA_Country on SARCR.CountryFID equals SAC.id
                    where SARCR.RegionFID == regionID
                    select new RegionAndCountryRelationViewModel()
                    {
                        SA_Country = SAC,
                    }).ToList();

            foreach (var item in list)
                listOfCountry.Add(item.SA_Country);

            return listOfCountry;
        }


        public List<SA_Category> GetCategoryBasedOnCountry(int countryID)
        {
            List<SA_Category> returnListOfCategory = new List<SA_Category>();

            //Note: Get country category mapping
            List<SA_CountryAndCategoryRelation> listOfCountryAndCategoryRelation = new List<SA_CountryAndCategoryRelation>();
            listOfCountryAndCategoryRelation = _context.SA_CountryAndCategoryRelation.ToList();

            //Note: filter country and category mapping based on country
            listOfCountryAndCategoryRelation = listOfCountryAndCategoryRelation.FindAll(item => item.CountryFID == countryID);

            foreach (var item in _context.SA_Category.ToList())
                if (listOfCountryAndCategoryRelation.Exists(it => it.CategoryFID == item.id))
                    returnListOfCategory.Add(item);

            return returnListOfCategory;
        }


        public List<SA_Product> GetProductBasedOnCategory(int categoryID)
        {
            return _context.SA_Product.ToList().FindAll(item =>  item.Category == categoryID).OrderBy(it => it.ProductName).ToList();
        }


        public List<SA_Product> GetProductBasedOnCountry(int countryFID)
        {
            List<SA_Product> returnListOfSA_Product = new List<SA_Product>();

            List<SA_Product> listOfSA_Product = (from country_ in _context.SA_Country
                                                 join cacRelation_ in _context.SA_CountryAndCategoryRelation on country_.id equals cacRelation_.CountryFID
                                                 join product_ in _context.SA_Product on cacRelation_.CategoryFID equals product_.Category
                                                 where country_.id == countryFID
                                                 select product_
                    ).ToList();

            List<int> listOfDistinct = listOfSA_Product.Select(item => item.id).Distinct().ToList();


            foreach (var item in listOfDistinct)
                returnListOfSA_Product.Add(listOfSA_Product.Find(itt => itt.id == item));

            return returnListOfSA_Product;

        }

        public List<SA_Product> GetProductBasedOnRegion(int regionFID)
        {
            List<SA_Product> returnListOfSA_Product = new List<SA_Product>();

            List<SA_Product> listOfSA_Product = (from country_ in _context.SA_Country
                                                 join cacRelation_ in _context.SA_CountryAndCategoryRelation on country_.id equals cacRelation_.CountryFID
                                                 join racRelation_ in _context.SA_RegionAndCountryRelation on cacRelation_.CountryFID equals racRelation_.CountryFID
                                                 join product_ in _context.SA_Product on cacRelation_.CategoryFID equals product_.Category
                                                 where racRelation_.RegionFID == regionFID
                                                 select product_
                    ).ToList();

            List<int> listOfDistinct = listOfSA_Product.Select(item => item.id).Distinct().ToList();


            foreach (var item in listOfDistinct)
                returnListOfSA_Product.Add(listOfSA_Product.Find(itt => itt.id == item));

            return returnListOfSA_Product;
        }
        public List<SA_FileType> GetFileTypeBasedOnProduct(int productID)
        {
            //Note: global variable define
            List<SA_FileType> returnListOfSA_FileType = new List<SA_FileType>();

            //Noet: get list file type
            List<SA_FileType> ListOfSA_FileType = _context.SA_FileType.ToList();

            //Note: get product and file type relation based on Product
            List<SA_ProductAndFileTypeRelation> listOfSA_ProductAndFileTypeRelation = new List<SA_ProductAndFileTypeRelation>();
            listOfSA_ProductAndFileTypeRelation = _context.SA_ProductAndFileTypeRelation.ToList().FindAll(item => item.ProductFID == productID);

            //Note: match File type records based on product 
            foreach (var item in ListOfSA_FileType)
                if (listOfSA_ProductAndFileTypeRelation.Exists(it => it.FileTypeFID == item.ID))
                    returnListOfSA_FileType.Add(item);


            return returnListOfSA_FileType.OrderBy(item => item.OrderNumber).ToList();
        }

        public List<SA_MarketRange> GetMarketRangeBasedOnProduct(int productID)
        {
            //Note: global variable define
            List<SA_MarketRange> returnListOfSA_MarketRange = new List<SA_MarketRange>();

            //Noet: get list file type
            List<SA_MarketRange> ListOfSA_MarketRange = _context.SA_MarketRange.ToList();

            //Note: get product and file type relation based on Product
            List<SA_ProductAndMarketRangeRelation> listOfSA_ProductAndMarketRangeRelation = new List<SA_ProductAndMarketRangeRelation>();
            listOfSA_ProductAndMarketRangeRelation = _context.SA_ProductAndMarketRangeRelation.ToList().FindAll(item => item.ProductFID == productID);

            //Note: match File type records based on product 
            foreach (var item in ListOfSA_MarketRange)
                if (listOfSA_ProductAndMarketRangeRelation.Exists(it => it.MarketRangeFID == item.ID))
                    returnListOfSA_MarketRange.Add(item);


            return returnListOfSA_MarketRange.OrderBy(item => item.OrderNumber).ToList();
        }

        public IEnumerable<SelectListItem> IEnumerableCountry(bool isIgnorGlobal, SelectListItem itemToAddAtTop_ = null)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            List<SA_Country> returnListOfCountry = new List<SA_Country>();
            List<SA_Country> listOfCountry = new List<SA_Country>();


            if (itemToAddAtTop_ != null)
                result.Add(itemToAddAtTop_);

            List<SA_Region> listOfRegion = _context.SA_Region.ToList();
            if (isIgnorGlobal)
                listOfRegion.RemoveAll(item => item.RegionName.Equals("global", StringComparison.CurrentCultureIgnoreCase));

            foreach (var item in listOfRegion)
                listOfCountry.AddRange(GetCountryBasedOnRegion(item.id));

            var distinctIDs = listOfCountry.Select(item => item.id).Distinct().ToList();

            foreach (var item in distinctIDs)
                returnListOfCountry.Add(listOfCountry.Find(itt => itt.id == item));


            result.AddRange(from item in returnListOfCountry select new SelectListItem { Text = item.CountryName, Value = item.id.ToString() });

            return result;
        }

        public IEnumerable<SelectListItem> IEnumerableRegion(SelectListItem itemToAddAtTop_ = null)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            if (itemToAddAtTop_ != null)
                result.Add(itemToAddAtTop_);

            List<SA_Region> listOfRegion = _context.SA_Region.ToList();

            result.AddRange(from item in listOfRegion select new SelectListItem { Text = item.RegionName, Value = item.id.ToString() });

            return result;
        }

        public IEnumerable<SelectListItem> IEnumerableRole(SelectListItem itemToAddAtTop_ = null)
        {
            List<SelectListItem> result = new List<SelectListItem>(); 

            if (itemToAddAtTop_ != null)
                result.Add(itemToAddAtTop_);

            List<SA_Role> listOfRole = _context.SA_Role.ToList();

            result.AddRange(from item in listOfRole select new SelectListItem { Text = item.Role, Value = item.id.ToString() });

            return result;
        }

        public IEnumerable<SelectListItem> IEnumerableCurrency(SelectListItem itemToAddAtTop_ = null)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            if (itemToAddAtTop_ != null)
                result.Add(itemToAddAtTop_);

            List<SA_Currency> listOfCurrency = _context.SA_Currency.ToList();

            result.AddRange(from item in listOfCurrency select new SelectListItem { Text = item.Name + " (" + item.Code + ")", Value = item.ID.ToString() });

            return result;

        }

        public IEnumerable<SelectListItem> IEnumerableCustomer(SelectListItem itemToAddAtTop_ = null)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            if (itemToAddAtTop_ != null)
                result.Add(itemToAddAtTop_);

            List<SA_Customer> listOfCustomer = _context.SA_Customers.ToList();

            result.AddRange(from item in listOfCustomer select new SelectListItem { Text = item.Fname + " (" + item.Email + ")", Value = item.id.ToString() });

            return result;

        }

        public IEnumerable<SelectListItem> IEnumerableProduct(SelectListItem itemToAddAtTop_ = null)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            if (itemToAddAtTop_ != null)
                result.Add(itemToAddAtTop_);

            List<SA_Product> listOfProduct = _context.SA_Product.ToList();

            result.AddRange(from item in listOfProduct select new SelectListItem { Text = item.ProductName , Value = item.id.ToString() });

            return result;

        }

        public IEnumerable<SelectListItem> ProductList(SelectListItem itemToAddAtTop_ = null)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            if (itemToAddAtTop_ != null)
                result.Add(itemToAddAtTop_);

            List<SA_Currency> listOfCurrency = _context.SA_Currency.ToList();

            result.AddRange(from product in _context.SA_Product
                            where product.status == 1
                            select new SelectListItem { Text = product.ProductName, Value = product.id.ToString() });

            return result.OrderBy(item => item.Text);

        }


        public bool IsNumberOfColumnsNotValid(int numberOfColumn, string importTypeRangeName)
        {
            bool isNotValid = true;

            //note: for chemical pricing
            if (importTypeRangeName.Equals("Yearly", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 4)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Monthly", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 5)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Weekly", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 7)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Daily Retail", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 37)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Daily Bulk", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 37)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Daily Data Import", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 10)
            {
                isNotValid = false;
            }

            //Note: for market analysis 
            else if (importTypeRangeName.Equals("Company", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 5)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Location", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 6)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Technology", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 6)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Process", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 6)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Production", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 4)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Operating Efficiency", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 4)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Demand By EndUse(T)", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 4)
            {
                isNotValid = false;
            }
            //else if (importTypeRangeName.Equals("Demand By Grade(T)", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 10)
            //{
            //    isNotValid = false;
            //}
            //else if (importTypeRangeName.Equals("Demand By Type(T)", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 10)
            //{
            //    isNotValid = false;
            //}
            else if (importTypeRangeName.Equals("Demand By SalesChannel(T)", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 4)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Demand By Region(T)", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 4)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Demand By TradeExport", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 5)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Demand By TradeImport", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 5)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Demand Supply Gap", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 4)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Demand Supply Gap Graph", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 4)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Demand By CompanyShares(T)", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 4)
            {
                isNotValid = false;
            }
            //else if (importTypeRangeName.Equals("Demand By GradePricing", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 10)
            //{
            //    isNotValid = false;
            //}
            //else if (importTypeRangeName.Equals("Demand By CompanyShares(%)", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 10)
            //{
            //    isNotValid = false;
            //}
            //else if (importTypeRangeName.Equals("Demand By EndUse(%)", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 10)
            //{
            //    isNotValid = false;
            //}
            //else if (importTypeRangeName.Equals("Demand By Grade(%)", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 10)
            //{
            //    isNotValid = false;
            //}
            //else if (importTypeRangeName.Equals("Demand By Region(%)", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 10)
            //{
            //    isNotValid = false;
            //}
            //else if (importTypeRangeName.Equals("Demand By SalesChannel(%)", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 10)
            //{
            //    isNotValid = false;
            //}
            //else if (importTypeRangeName.Equals("Demand By Type(%)", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 10)
            //{
            //    isNotValid = false;
            //}
            else if (importTypeRangeName.Equals("Customer Analysis", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 13)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Port Wise Import", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 15)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Port Wise Export", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 15)
            {
                isNotValid = false;
            }
            else if (importTypeRangeName.Equals("Supplier Analysis", StringComparison.CurrentCultureIgnoreCase) && numberOfColumn == 12)
            {
                isNotValid = false;
            }

            return isNotValid;
        }


        public string ToValidateData(DataTable dataTable, string regionName, string countryName, string rangeName, bool? isCountryWise)
        {
            string result = "please check excel, did not found any records";
            int count = dataTable.AsEnumerable().Select(item => item.ItemArray).FirstOrDefault().Count();
            var listOfObject = dataTable.AsEnumerable().SelectMany(x => x.ItemArray).Select((v, i) => new { Value = v, Index = i % count }).ToList();           

            if (!isCountryWise.HasValue)
            {
                if (rangeName.Equals("Yearly", StringComparison.CurrentCultureIgnoreCase)) 
                {

                    ////Note: index 2 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("year values are not number.");

                    //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("count values are not number");

                    //Note: index 1 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 1).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                    if (!isCountryNameNotExist)
                        throw new Exception("country value is mismatch");

                    result = "success";
                }
                else if (rangeName.Equals("Monthly", StringComparison.CurrentCultureIgnoreCase))
                {

                    //Note: index 1 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 1).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                    if (!isCountryNameNotExist)
                        throw new Exception("country value is mismatch");


                    ////Note: index 2 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");


                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 4).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Weekly", StringComparison.CurrentCultureIgnoreCase))
                {
                    //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                    if (!isCountryNameNotExist)
                        throw new Exception("country value is mismatch");

                    ////Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 6).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Daily Retail", StringComparison.CurrentCultureIgnoreCase))
                {
                    //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 5).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                    if (!isCountryNameNotExist)
                        throw new Exception("country value is mismatch");

                    ////Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 1).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    bool isValueNumber1 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 6).Select(it => it.Value).ToList());
                    if (!isValueNumber1)
                        throw new Exception("Date 1 values are not number");

                    bool isValueNumber2 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 9).Select(it => it.Value).ToList());
                    if (!isValueNumber2)
                        throw new Exception("Date 2 values are not number");

                    bool isValueNumber3 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 12).Select(it => it.Value).ToList());
                    if (!isValueNumber3)
                        throw new Exception("Date 3 values are not number");

                    bool isValueNumber4 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 15).Select(it => it.Value).ToList());
                    if (!isValueNumber4)
                        throw new Exception("Date 4 values are not number");

                    bool isValueNumber5 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 18).Select(it => it.Value).ToList());
                    if (!isValueNumber5)
                        throw new Exception("Date 5 values are not number");

                    bool isValueNumber6 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 21).Select(it => it.Value).ToList());
                    if (!isValueNumber6)
                        throw new Exception("Date 6 values are not number");

                    bool isValueNumber7 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 24).Select(it => it.Value).ToList());
                    if (!isValueNumber7)
                        throw new Exception("Date 7 values are not number");

                    bool isValueNumber8 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 27).Select(it => it.Value).ToList());
                    if (!isValueNumber8)
                        throw new Exception("Date 8 values are not number");

                    bool isValueNumber9 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 30).Select(it => it.Value).ToList());
                    if (!isValueNumber9)
                        throw new Exception("Date 9 values are not number");

                    bool isValueNumber10 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 33).Select(it => it.Value).ToList());
                    if (!isValueNumber10)
                        throw new Exception("Date 10 values are not number");

                    bool isValueNumber11 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 36).Select(it => it.Value).ToList());
                    if (!isValueNumber11)
                        throw new Exception("Date 11 values are not number");

                    bool isValueNumber12 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 39).Select(it => it.Value).ToList());
                    if (!isValueNumber12)
                        throw new Exception("Date 12 values are not number");

                    bool isValueNumber13 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 42).Select(it => it.Value).ToList());
                    if (!isValueNumber13)
                        throw new Exception("Date 13 values are not number");

                    bool isValueNumber14 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 45).Select(it => it.Value).ToList());
                    if (!isValueNumber14)
                        throw new Exception("Date 14 values are not number");

                    bool isValueNumber15 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 48).Select(it => it.Value).ToList());
                    if (!isValueNumber15)
                        throw new Exception("Date 15 values are not number");

                    bool isValueNumber16 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 51).Select(it => it.Value).ToList());
                    if (!isValueNumber16)
                        throw new Exception("Date 16 values are not number");

                    bool isValueNumber17 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 54).Select(it => it.Value).ToList());
                    if (!isValueNumber17)
                        throw new Exception("Date 17 values are not number");

                    bool isValueNumber18 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 57).Select(it => it.Value).ToList());
                    if (!isValueNumber18)
                        throw new Exception("Date 18 values are not number");

                    bool isValueNumber19 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 60).Select(it => it.Value).ToList());
                    if (!isValueNumber19)
                        throw new Exception("Date 19 values are not number");

                    bool isValueNumber20 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 63).Select(it => it.Value).ToList());
                    if (!isValueNumber20)
                        throw new Exception("Date 20 values are not number");

                    bool isValueNumber21 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 66).Select(it => it.Value).ToList());
                    if (!isValueNumber21)
                        throw new Exception("Date 21 values are not number");

                    bool isValueNumber22 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 69).Select(it => it.Value).ToList());
                    if (!isValueNumber22)
                        throw new Exception("Date 22 values are not number");

                    bool isValueNumber23 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 72).Select(it => it.Value).ToList());
                    if (!isValueNumber23)
                        throw new Exception("Date 23 values are not number");

                    bool isValueNumber24 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 75).Select(it => it.Value).ToList());
                    if (!isValueNumber24)
                        throw new Exception("Date 24 values are not number");

                    bool isValueNumber25 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 78).Select(it => it.Value).ToList());
                    if (!isValueNumber25)
                        throw new Exception("Date 25 values are not number");

                    bool isValueNumber26 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 81).Select(it => it.Value).ToList());
                    if (!isValueNumber26)
                        throw new Exception("Date 26 values are not number");

                    bool isValueNumber27 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 84).Select(it => it.Value).ToList());
                    if (!isValueNumber27)
                        throw new Exception("Date 27 values are not number");

                    bool isValueNumber28 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 87).Select(it => it.Value).ToList());
                    if (!isValueNumber28)
                        throw new Exception("Date 28 values are not number");

                    bool isValueNumber29 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 90).Select(it => it.Value).ToList());
                    if (!isValueNumber29)
                        throw new Exception("Date 29 values are not number");

                    bool isValueNumber30 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 93).Select(it => it.Value).ToList());
                    if (!isValueNumber30)
                        throw new Exception("Date 30 values are not number");

                    bool isValueNumber31 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 96).Select(it => it.Value).ToList());
                    if (!isValueNumber31)
                        throw new Exception("Date 31 values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Daily Bulk", StringComparison.CurrentCultureIgnoreCase))
                {
                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 1).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 5).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                    if (!isCountryNameNotExist)
                        throw new Exception("country value is mismatch");


                    bool isValueNumber1 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 6).Select(it => it.Value).ToList());
                    if (!isValueNumber1)
                        throw new Exception("Date 1 values are not number");

                    bool isValueNumber2 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 9).Select(it => it.Value).ToList());
                    if (!isValueNumber2)
                        throw new Exception("Date 2 values are not number");

                    bool isValueNumber3 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 12).Select(it => it.Value).ToList());
                    if (!isValueNumber3)
                        throw new Exception("Date 3 values are not number");

                    bool isValueNumber4 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 15).Select(it => it.Value).ToList());
                    if (!isValueNumber4)
                        throw new Exception("Date 4 values are not number");

                    bool isValueNumber5 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 18).Select(it => it.Value).ToList());
                    if (!isValueNumber5)
                        throw new Exception("Date 5 values are not number");

                    bool isValueNumber6 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 21).Select(it => it.Value).ToList());
                    if (!isValueNumber6)
                        throw new Exception("Date 6 values are not number");

                    bool isValueNumber7 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 24).Select(it => it.Value).ToList());
                    if (!isValueNumber7)
                        throw new Exception("Date 7 values are not number");

                    bool isValueNumber8 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 27).Select(it => it.Value).ToList());
                    if (!isValueNumber8)
                        throw new Exception("Date 8 values are not number");

                    bool isValueNumber9 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 30).Select(it => it.Value).ToList());
                    if (!isValueNumber9)
                        throw new Exception("Date 9 values are not number");

                    bool isValueNumber10 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 33).Select(it => it.Value).ToList());
                    if (!isValueNumber10)
                        throw new Exception("Date 10 values are not number");

                    bool isValueNumber11 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 36).Select(it => it.Value).ToList());
                    if (!isValueNumber11)
                        throw new Exception("Date 11 values are not number");

                    bool isValueNumber12 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 39).Select(it => it.Value).ToList());
                    if (!isValueNumber12)
                        throw new Exception("Date 12 values are not number");

                    bool isValueNumber13 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 42).Select(it => it.Value).ToList());
                    if (!isValueNumber13)
                        throw new Exception("Date 13 values are not number");

                    bool isValueNumber14 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 45).Select(it => it.Value).ToList());
                    if (!isValueNumber14)
                        throw new Exception("Date 14 values are not number");

                    bool isValueNumber15 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 48).Select(it => it.Value).ToList());
                    if (!isValueNumber15)
                        throw new Exception("Date 15 values are not number");

                    bool isValueNumber16 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 51).Select(it => it.Value).ToList());
                    if (!isValueNumber16)
                        throw new Exception("Date 16 values are not number");

                    bool isValueNumber17 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 54).Select(it => it.Value).ToList());
                    if (!isValueNumber17)
                        throw new Exception("Date 17 values are not number");

                    bool isValueNumber18 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 57).Select(it => it.Value).ToList());
                    if (!isValueNumber18)
                        throw new Exception("Date 18 values are not number");

                    bool isValueNumber19 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 60).Select(it => it.Value).ToList());
                    if (!isValueNumber19)
                        throw new Exception("Date 19 values are not number");

                    bool isValueNumber20 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 63).Select(it => it.Value).ToList());
                    if (!isValueNumber20)
                        throw new Exception("Date 20 values are not number");

                    bool isValueNumber21 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 66).Select(it => it.Value).ToList());
                    if (!isValueNumber21)
                        throw new Exception("Date 21 values are not number");

                    bool isValueNumber22 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 69).Select(it => it.Value).ToList());
                    if (!isValueNumber22)
                        throw new Exception("Date 22 values are not number");

                    bool isValueNumber23 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 72).Select(it => it.Value).ToList());
                    if (!isValueNumber23)
                        throw new Exception("Date 23 values are not number");

                    bool isValueNumber24 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 75).Select(it => it.Value).ToList());
                    if (!isValueNumber24)
                        throw new Exception("Date 24 values are not number");

                    bool isValueNumber25 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 78).Select(it => it.Value).ToList());
                    if (!isValueNumber25)
                        throw new Exception("Date 25 values are not number");

                    bool isValueNumber26 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 81).Select(it => it.Value).ToList());
                    if (!isValueNumber26)
                        throw new Exception("Date 26 values are not number");

                    bool isValueNumber27 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 84).Select(it => it.Value).ToList());
                    if (!isValueNumber27)
                        throw new Exception("Date 27 values are not number");

                    bool isValueNumber28 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 87).Select(it => it.Value).ToList());
                    if (!isValueNumber28)
                        throw new Exception("Date 28 values are not number");

                    bool isValueNumber29 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 90).Select(it => it.Value).ToList());
                    if (!isValueNumber29)
                        throw new Exception("Date 29 values are not number");

                    bool isValueNumber30 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 93).Select(it => it.Value).ToList());
                    if (!isValueNumber30)
                        throw new Exception("Date 30 values are not number");

                    bool isValueNumber31 = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 96).Select(it => it.Value).ToList());
                    if (!isValueNumber31)
                        throw new Exception("Date 31 values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Daily Data Import", StringComparison.CurrentCultureIgnoreCase))
                {
                    //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 5).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                    if (!isCountryNameNotExist)
                        throw new Exception("country value is mismatch");

                    bool isMidValueNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 7).Select(it => it.Value).ToList());
                    if (!isMidValueNumber)
                        throw new Exception("Mid values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Chemical 1", StringComparison.CurrentCultureIgnoreCase))
                {
                    result = "success";
                }
                else if (rangeName.Equals("Chemical 2", StringComparison.CurrentCultureIgnoreCase))
                {
                    result = "success";
                }
                else if (rangeName.Equals("Chemical 3", StringComparison.CurrentCultureIgnoreCase))
                {
                    result = "success";
                }
                else if (rangeName.Equals("Chemical 4", StringComparison.CurrentCultureIgnoreCase))
                {
                    result = "success";
                }
                else if (rangeName.Equals("Chemical 5", StringComparison.CurrentCultureIgnoreCase))
                {
                    result = "success";
                }
                else if (rangeName.Equals("Chemical 6", StringComparison.CurrentCultureIgnoreCase))
                {
                    result = "success";
                }
            }
            else
            {
                if (rangeName.Equals("Company", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Location", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 4).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 5).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Technology", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 4).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 5).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Process", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 4).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 5).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Production", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Operating Efficiency", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Demand By EndUse(T)", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Demand By SalesChannel(T)", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Demand By Region(T)", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Demand By Type(T)", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Demand By Grade(T)", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Demand By TradeExport", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    bool isValumeNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 4).Select(it => it.Value).ToList());
                    if (!isValumeNumber)
                        throw new Exception("Volume values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Demand By TradeImport", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    bool isValumeNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 4).Select(it => it.Value).ToList());
                    if (!isValumeNumber)
                        throw new Exception("Volume values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Demand Supply Gap", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Demand Supply Gap Graph", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Demand By CompanyShares(T)", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Customer Analysis", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 1).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 1).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    ////Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 5).Select(it => it.Value).ToList());
                    //if (!isCountNumber)
                    //    throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Port Wise Import", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 1).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    ////Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 5).Select(it => it.Value).ToList());
                    //if (!isCountNumber)
                    //    throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Port Wise Export", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 1).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 5).Select(it => it.Value).ToList());
                    //if (!isCountNumber)
                    //    throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Supplier Analysis", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 1).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 1).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    ////Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 5).Select(it => it.Value).ToList());
                    //if (!isCountNumber)
                    //    throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Industry Market Size", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 2).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 3).Select(it => it.Value).ToList());
                    if (!isCountNumber)
                        throw new Exception("Count values are not number");

                    result = "success";
                }
                else if (rangeName.Equals("Technology Licensor", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isCountryWise.Value)
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isCountryNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(countryName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isCountryNameNotExist)
                            throw new Exception("country value is mismatch");
                    }
                    else
                    {
                        //Note: index 3 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                        bool isRegionNameNotExist = listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList().All(itt => itt.ToString().Equals(regionName, StringComparison.CurrentCultureIgnoreCase));
                        if (!isRegionNameNotExist)
                            throw new Exception("region value is mismatch");
                    }

                    //Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isYearNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 0).Select(it => it.Value).ToList());
                    //if (!isYearNumber)
                    //    throw new Exception("Year values are not number");

                    ////Note: index 4 is year column. to check all values are number or not. if found any one not is number then throw manuall error/exception
                    //bool isCountNumber = IsListTypeOfDecimal(listOfObject.FindAll(item => item.Index == 5).Select(it => it.Value).ToList());
                    //if (!isCountNumber)
                    //    throw new Exception("Count values are not number");

                    result = "success";
                }
            }

            return result;
        }



        private bool IsListTypeOfDecimal(List<object> listOfObject)
        {
            bool result = true;

            try
            {
                List<decimal> castListToDecimal = listOfObject.ConvertAll<decimal>(Convert.ToDecimal).ToList();

                if (listOfObject.Count() != castListToDecimal.Count())
                    throw new Exception("mismatch count list");

            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;

        }

    }
}