using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = DataLayer.dlAdminChemicalPricing;

namespace BusinessLayer
{
   public class blAdminChemicalPricing
    {
        private enAdminChemicalPrice _enChemicalPricing = null;
        private Product _enChemicalPricingproduct = null;
        private DAL _objDAL = null;

        public enAdminChemicalPrice GetEntityReference
        {
            get { return this._enChemicalPricing; }
        }
        public blAdminChemicalPricing(enAdminChemicalPrice enChemicalPricing_)
        {
            this._enChemicalPricing = enChemicalPricing_;
        }
        public List<enAdminChemicalPrice> ListProductFeedstock(enSearchFilter enSearchFilter)
        {
            return GetDALReference().ListProductFeedstock(enSearchFilter);
        }

        public List<enAdminChemicalPrice> DeleteProductFeedstock(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().DeleteProductFeedstock(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> DeleteCommentaryPricing(string Product, string County)
        {
            return GetDALReference().DeleteCommentaryPricing(Product, County);
        }

        //----------------------------------------------------Insert data--------------------------------------------------------------
        public List<enAdminChemicalPrice> InsertProductFeedstock(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().InsertProductFeedstock(enAdminChemicalPrice);
        }
        public List<enAdminChemicalPrice> ReadChildProductFeedstock(enSearchFilter objChemical)
        {
            return GetDALReference().ReadChildProductFeedstock(objChemical);
        }
        public List<enAdminChemicalPrice> ReadCountrybehalf_Of_Product(enSearchFilter searchFilter)
        {
            return GetDALReference().ReadCountrybehalf_Of_Product(searchFilter);
        }

        public List<enAdminChemicalPrice> ReadAllProduct(enSearchFilter searchFilter)
        {
            return GetDALReference().ReadAllProduct(searchFilter);
        }
        public List<enAdminChemicalPrice> ReadPortwiseExport(enSearchFilter objChemical)
        {
            return GetDALReference().ReadPortwiseExport(objChemical);
        }

        public List<enAdminChemicalPrice> InsertOnChemicalPricingDataWeekly(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().InsertChemicalPricingDataWeekly(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> InsertOnChemicalPricingDataMonthly(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().InsertOnChemicalPricingDataMonthly(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> InsertOnChemicalPricingDataYearly(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().InsertOnChemicalPricingDataYearly(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> GetAssignProduct(enAdminChemicalPrice obj)
        {
            return GetDALReference().GetAssignProduct(obj);
        }
        public List<enAdminChemicalPrice> CheckCommentaryAvilability(enAdminChemicalPrice objCom)
        {
            return GetDALReference().CheckCommentaryAvilability(objCom);
        }
        public List<enAdminChemicalPrice> InsertOnChemicalPricingDataDailyRetail(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().InsertOnChemicalPricingDataDailyRetail(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> InsertOnChemicalPricingDataDailyBulk(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().InsertOnChemicalPricingDataDailyBulk(objChemicalWeekly);
        }
        //-----------------------------------------------------------------------------------********************************************************

        //**********************************************Update************************************************************************
        public List<enAdminChemicalPrice> UpdateOnChemicalPricingDataWeekly(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().UpdateOnChemicalPricingDataWeekly(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> UpdateOnChemicalPricingDataMonthly(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().UpdateOnChemicalPricingDataMonthly(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> UpdateOnChemicalPricingDataYearly(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().UpdateOnChemicalPricingDataYearly(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> UpdateOnChemicalPricingDataDailyRetail(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().UpdateOnChemicalPricingDataDailyRetail(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> UpdateOnChemicalPricingDataDailyBulk(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().UpdateOnChemicalPricingDataDailyBulk(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> Update_ReadBasedOnSearchFilterChemicalPrice(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().Update_ReadBasedOnSearchFilterChemicalPrice(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> Update_ReadBasedOnSearchFilterChemicalPriceMonthlytoYearly(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().Update_ReadBasedOnSearchFilterChemicalPriceMonthlytoYearly(objChemicalWeekly);
        }
        //*****************************************************************************************************************************

        public List<enAdminChemicalPrice> ReadBasedOnSearchFilterChemicalPrice(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().ReadBasedOnSearchFilterChemicalPrice(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> ReadBasedOnSearchFilterChemicalPriceMonthlytoYearly(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().ReadBasedOnSearchFilterChemicalPriceMonthlytoYearly(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> ReadAllChemicalPricingDataList(enSearchFilter objChemicalWeekly)
        {
            return GetDALReference().ReadAllChemicalPricingDataList(objChemicalWeekly);
        }
        
        public List<enAdminChemicalPrice> ReadAllSimpleViewProductDataList(enSearchFilter objChemicalWeekly)
        {
            return GetDALReference().ReadAllSimpleViewProductDataList(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> ReadAllProjectManagerDataList(enSearchFilter objChemicalWeekly)
        {
            return GetDALReference().ReadAllProjectManagerDataList(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> ReadChemicalPricing(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().ReadChemicalPricing(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> GetLatestRecordofProduct(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().GetLatestRecordofProduct(objChemicalWeekly);
        }
        
        public List<enAdminChemicalPrice> ReadAllChemicalPricingDataList_Update(enSearchFilter objChemicalWeekly)
        {
            return GetDALReference().ReadAllChemicalPricingDataList_Update(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> CalculatePercentageChemicalPricingData(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().CalculatePercentageChemicalPricingData(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> InsertCalculatePercentageChemicalPricingData(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().InsertCalculatePercentageChemicalPricingData(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> CheckChemicalPricing(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().CheckChemicalPricing(objChemicalWeekly);
        }

        public List<enAdminChemicalPrice> DownloadChemicalPricingData(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().DownloadChemicalPricingData(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> DeleteAdminChemicalPricingData(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().DeleteAdminChemicalPricingData(objChemicalWeekly);
        }
        public List<enAdminChemicalPrice> ReadAllProductVarient()
        {
            return GetDALReference().ReadAllProductVarient();
        }
        
        public List<enAdminChemicalPrice> ReadAllLocation()
        {
            return GetDALReference().ReadAllLocation();
        }
        
        public List<enAdminChemicalPrice> ReadAllUser()
        {
            return GetDALReference().ReadAllUser();
        }
        public List<enAdminChemicalPrice> ReadAllProduct()
        {
            return GetDALReference().ReadAllProduct();
        }
        public List<enAdminChemicalPrice> ReadAllProductCommentaryProductEmpRelation()
        {
            return GetDALReference().ReadAllProductCommentaryProductEmpRelation();
        }
        public List<enAdminChemicalPrice> ReadAllProductWithStatus()
        {
            return GetDALReference().ReadAllProductWithStatus();
        }
        public List<enAdminChemicalPrice> ReadAllCountry()
        {
            return GetDALReference().ReadAllCountry();
        }
        public List<enAdminChemicalPrice> UpdateApprovalStatus()
        {
            return GetDALReference().UpdateApprovalStatus();
        }
        public List<enAdminChemicalPrice> DisApprovalMessage()
        {
            return GetDALReference().DisApprovalMessage();
        }
        public List<enAdminChemicalPrice> AddNewProductVariant()
        {
            return GetDALReference().AddNewProductVariant();
        }
        public List<enAdminChemicalPrice> AddNewProductVariantRealtion()
        {
            return GetDALReference().AddNewProductVariantRealtion();
        }
        public List<enAdminChemicalPrice> AddProductVariantDDL()
        {
            return GetDALReference().AddProductVariantDDL();
        }
        
        public List<enAdminChemicalPrice> GetAssociatePriceList(enSearchFilter objChemical)
        {
            return GetDALReference().GetAssociatePriceList(objChemical);
        }
        public List<enAdminChemicalPrice> GetAllEmployeeRecords()
        {
            return GetDALReference().GetAllEmployeeRecords();
        }

        public List<enAdminChemicalPrice> GetAllEmpName()
        {
            return GetDALReference().GetAllEmpName();
        }
        public List<enAdminChemicalPrice> GetAppraisalAmountList(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetAppraisalAmountList(enAdminChemicalPrice);
        }
        public List<enAdminChemicalPrice> insertAppraisalAmount(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().insertAppraisalAmount(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetUserProductOrganizationaldata(enSearchFilter Userdata)
        {
            return GetDALReference().GetUserProductOrganizationaldata(Userdata);
        }
        public List<enAdminChemicalPrice> GetConsultantPriceList(enSearchFilter objChemical)
        {
            return GetDALReference().GetConsultantPriceList(objChemical);
        }

        public List<enAdminChemicalPrice> OneTimeAllApproved()
        {
            return GetDALReference().OneTimeAllApproved();
        }

        public List<enAdminChemicalPrice> GetPendingProductUsingRange(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetPendingProductUsingRange(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> InsertEmployeeMaster()
        {
            return GetDALReference().InsertEmployeeMaster();
        }
        public List<enAdminChemicalPrice> InsertProductEmpRelation()
        {
            return GetDALReference().InsertProductEmpRelation();
        }
        public List<enAdminChemicalPrice> ReadAllLocationForedit()
        {
            return GetDALReference().ReadAllLocationForedit();
        }
        //------------------------------------News ------------------------------------------------------
        public List<enAdminChemicalPrice> ReadLatestNews(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().ReadLatestNews(objChemicalWeekly);
        }
        //------------------------------------------------------------------------------------------------
        //-------------------New chenges------------------------------
        public List<enAdminChemicalPrice> ReadInsertedDataInGrid(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().ReadInsertedDataInGrid(objChemicalWeekly);
        }
        //----------------------------------------------------------
        public List<enAdminChemicalPrice> GetlistOfAllDisApprovedRecord(enSearchFilter searchFilter)
        {
            return GetDALReference().GetlistOfAllDisApprovedRecord(searchFilter);
        }
        public List<enAdminChemicalPrice> GetConsultantPriceListForcasting(enSearchFilter objChemical)
        {
            return GetDALReference().GetConsultantPriceListForcasting(objChemical);
        }
        public List<enAdminChemicalPrice> GetCountAdminChemPriceyearly(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetCountAdminChemPriceyearly(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetUpdateCountMonthly(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetUpdateCountMonthly(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> ShowAllCountMonthly(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().ShowAllCountMonthly(enAdminChemicalPrice);
        }
        public List<enAdminChemicalPrice> SearchVariant(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().SearchVariant(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> SaveProductvariant(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().SaveProductvariant(enAdminChemicalPrice);
        }


        public List<enAdminChemicalPrice> SearchProductVariantRelation(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().SearchProductVariantRelation(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> SaveProductVariantRelation(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().SaveProductVariantRelation(enAdminChemicalPrice);
        }
        public List<enAdminChemicalPrice> UpdateChemPriceFlag(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().UpdateChemPriceFlag(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetCountryBasedOnRegion(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetCountryBasedOnRegion(enAdminChemicalPrice);
        }
        public List<enAdminChemicalPrice> GetProductBasedOnCategory(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetProductBasedOnCategory(enAdminChemicalPrice);
        }
        public List<enAdminChemicalPrice> ReadPortwiseExportnew(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().ReadPortwiseExportnew(objChemical);
        }
        public List<enAdminChemicalPrice> CheckPortWiseExportAvilability(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().CheckPortWiseExportAvilability(objChemical);
        }
        
        public List<enAdminChemicalPrice> BindYear(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().BindYear(objChemical);
        }
        public List<enAdminChemicalPrice> GetAllFilterValues(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().GetAllFilterValues(objChemical);
        }
        
        public List<enAdminChemicalPrice> BindHSCode(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().BindHSCode(objChemical);
        }

        public List<enAdminChemicalPrice> BindGrade(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().BindGrade(objChemical);
        }

        public List<enAdminChemicalPrice> BindExportername(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().BindExportername(objChemical);
        }
        public List<enAdminChemicalPrice> BindExporterCountry(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().BindExporterCountry(objChemical);
        }

        public List<enAdminChemicalPrice> BindPortofOrgine(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().BindPortofOrgine(objChemical);
        }

        public List<enAdminChemicalPrice> BindConsigneeName(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().BindConsigneeName(objChemical);
        }
        public List<enAdminChemicalPrice> BindCountryOfImport(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().BindCountryOfImport(objChemical);
        }

        public List<enAdminChemicalPrice> BindPortOfDestination(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().BindPortOfDestination(objChemical);
        }

        public List<enAdminChemicalPrice> BindInconterms(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().BindInconterms(objChemical);
        }

        public List<enAdminChemicalPrice> BindShipmentMode(enAdminChemicalPrice objChemical)
        {
            return GetDALReference().BindShipmentMode(objChemical);
        }

        public List<enAdminChemicalPrice> GetAllProductFinderDetails(enSearchFilter enSearchFilter)
        {
            return GetDALReference().GetAllProductFinderDetails(enSearchFilter);
        }


        public List<enAdminChemicalPrice> BindProduct()
        {
            return GetDALReference().BindProduct();
        }

        public List<enAdminChemicalPrice> BinduserSubscribeProduct(enSearchFilter enSearchFilter)
        {
            return GetDALReference().BinduserSubscribeProduct(enSearchFilter);
        }
        public List<Product>BinduserSubscribeProductnews(enSearchFilter enSearchFilter)
        {
            return GetDALReference().BinduserSubscribeProductnews(enSearchFilter);
        }
        public List<enAdminChemicalPrice> ProductSearchforCategory(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().ProductSearchforCategory(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetProductbasedonCategory(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetProductbasedonCategory(enAdminChemicalPrice);
        }
        public List<enAdminChemicalPrice> get_Two_months_before_loginDetails()
        {
            return GetDALReference().get_Two_months_before_loginDetails();
        }
        public List<enAdminChemicalPrice> getChemicalPricingList(enSearchFilter searchFilter)
        {
            return GetDALReference().getChemicalPricingList(searchFilter);
        }
        public List<enAdminChemicalPrice> getgetoverview_list(enSearchFilter searchFilter)
        {
            return GetDALReference().getgetoverview_list(searchFilter);
        }

        //------------Unit Rate-------------------
        public List<enAdminChemicalPrice> InsertUnit(enAdminChemicalPrice obj)
        {
            return GetDALReference().InsertUnit(obj);
        }
        public List<enAdminChemicalPrice> GetUnitList(enSearchFilter obj)
        {
            return GetDALReference().GetUnitList(obj);
        }
        public List<enAdminChemicalPrice> GetCurrencyUpdateDetails()
        {
            return GetDALReference().GetCurrencyUpdateDetails();
        }
        public List<enAdminChemicalPrice> ConvertCurrencyData(enAdminChemicalPrice obj)
        {
            return GetDALReference().ConvertCurrencyData(obj);
        }
        public List<enAdminChemicalPrice> ConvertCurrencyDataYearly(enAdminChemicalPrice obj)
        {
            return GetDALReference().ConvertCurrencyDataYearly(obj);
        }
        public List<enAdminChemicalPrice> ConvertCurrencyDataExcel(enAdminChemicalPrice obj)
        {
            return GetDALReference().ConvertCurrencyDataExcel(obj);
        }
        public List<enAdminChemicalPrice> GetCurrencyUpdateDetailsExcel()
        {
            return GetDALReference().GetCurrencyUpdateDetailsExcel();
        }
        public List<enAdminChemicalPrice> ConvertCurrencyDataYEARLYExcel(enAdminChemicalPrice obj)
        {
            return GetDALReference().ConvertCurrencyDataYEARLYExcel(obj);
        }

        public List<enAdminChemicalPrice> CustomerProductTracking(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().CustomerProductTracking(enAdminChemicalPrice);
        }

        //--------------------unit end---------------------------
        public List<enAdminChemicalPrice> BindCountryBehalf_Of_Product(enAdminChemicalPrice en)
        {
            return GetDALReference().BindCountryBehalf_Of_Product(en);
        }
        public List<enAdminChemicalPrice> BindProductforTrigger(enAdminChemicalPrice en)
        {
            return GetDALReference().BindProductforTrigger(en);
        }
        public List<enAdminChemicalPrice> BindProductVariant(enAdminChemicalPrice en)
        {
            return GetDALReference().BindProductVariant(en);
        }
        public List<enAdminChemicalPrice> SaveChemicalTrigger(enAdminChemicalPrice en)
        {
            return GetDALReference().SaveChemicalTrigger(en);
        }
        public List<enAdminChemicalPrice> ReadBasedOnSearchFilter(enSearchFilter en)
        {
            return GetDALReference().ReadBasedOnSearchFilter(en);
        }
        public List<enAdminChemicalPrice> RemoveChemicalTrigger(enAdminChemicalPrice en)
        {
            return GetDALReference().RemoveChemicalTrigger(en);
        }
        public List<enAdminChemicalPrice> BindMinMax(enAdminChemicalPrice en)
        {
            return GetDALReference().BindMinMax(en);
        }
        public List<enAdminChemicalPrice> GetUserlist(enAdminChemicalPrice obj)
        {
            return GetDALReference().GetUserlist(obj);
        }
        public List<enAdminChemicalPrice> listOfUserTriggerDetails()
        {
            return GetDALReference().listOfUserTriggerDetails();
        }
        
        public List<enAdminChemicalPrice> GetlistTriggers(enAdminChemicalPrice obj)
        {
            return GetDALReference().GetlistTriggers(obj);
        }
        public List<enAdminChemicalPrice> UpdatelistTriggers(enAdminChemicalPrice obj)
        {
            return GetDALReference().UpdatelistTriggers(obj);
        }
        //---------Excel Upload------------------
        public List<enAdminChemicalPrice> CheckProductVarient(enAdminChemicalPrice objChemicalWeekly)
        {
            return GetDALReference().CheckProductVarient(objChemicalWeekly);
        }

        public List<enAdminChemicalPrice> BindFreelProduct(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().BindFreelProduct(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> BindFreelCountry(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().BindFreelCountry(enAdminChemicalPrice);
        }
        public List<enAdminChemicalPrice> BindCountry()
        {
            return GetDALReference().BindCountry();
        }

        public List<enAdminChemicalPrice> BindFreelVariant(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().BindFreelVariant(enAdminChemicalPrice);
        }


        public List<enAdminChemicalPrice> InsertNewFreelUser(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().InsertNewFreelUser(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> InsertFreelDataAccessRange(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().InsertFreelDataAccessRange(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> InsertFreelFeedBack(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().InsertFreelFeedBack(enAdminChemicalPrice);
        }

        
        public List<enAdminChemicalPrice> BindFreelUser()
        {
            return GetDALReference().BindFreelUser();
        }

        public List<enAdminChemicalPrice> GetAllFreelPricing(enSearchFilter enSearchFilter)
        {
            return GetDALReference().GetAllFreelPricing(enSearchFilter);
        }

        public List<enAdminChemicalPrice> GetFreelPricingDatabasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetFreelPricingDatabasedonFilter(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetFreelPricingBasedonUser(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetFreelPricingBasedonUser(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetFreelDataRangebasedonUser(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetFreelDataRangebasedonUser(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> InsertFreelPricingRecord(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().InsertFreelPricingRecord(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> NewsDetailsTracking(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().NewsDetailsTracking(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetFreelPricingProductEmpRelation(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetFreelPricingProductEmpRelation(enAdminChemicalPrice);
        }


        //public List<enAdminChemicalPrice> InsertFreelRating(enAdminChemicalPrice enAdminChemicalPrice)
        //{
        //    return GetDALReference().InsertFreelRating(enAdminChemicalPrice);
        //}

        public List<enAdminChemicalPrice> GetCheckUserAccountDetails(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetCheckUserAccountDetails(enAdminChemicalPrice);
        }
        public List<enAdminChemicalPrice> CheckForRangeUpdateMonthly(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().CheckForRangeUpdateMonthly(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetFreelPricingForUserBilling(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetFreelPricingForUserBilling(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetFreelAllPricingForUserBilling(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetFreelAllPricingForUserBilling(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> BindUserbasedonSalesPackageSubscription(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().BindUserbasedonSalesPackageSubscription(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> InsertWrokSpacedetails(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().InsertWrokSpacedetails(enAdminChemicalPrice);
        }
        public List<enAdminChemicalPrice> GetListofWorkSpace(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetListofWorkSpace(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetListofWorkSpacebasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetListofWorkSpacebasedonFilter(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetListofWorkSpacebasedonUser(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetListofWorkSpacebasedonUser(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> UserbasedonCompany(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().UserbasedonCompany(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> InsertWorkSpacebyCustomer(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().InsertWorkSpacebyCustomer(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> getIndustrylanding_list(enSearchFilter searchFilter)
        {
            return GetDALReference().getIndustrylanding_list(searchFilter);
        }


        public List<enAdminChemicalPrice> BinduserProduct(enSearchFilter enSearchFilter)
        {
            return GetDALReference().BinduserProduct(enSearchFilter);
        }
        public List<enAdminChemicalPrice> BindPlantBasedonProduct(enSearchFilter enSearchFilter)
        {
            return GetDALReference().BindPlantBasedonProduct(enSearchFilter);
        }

        public List<enAdminChemicalPrice> SaveMaintenanceAndShutdownTrigger(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().SaveMaintenanceAndShutdownTrigger(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> ReadMarketAnalysisTriggerBasedOnSearchFilter(enSearchFilter en)
        {
            return GetDALReference().ReadMarketAnalysisTriggerBasedOnSearchFilter(en);
        }

        public List<enAdminChemicalPrice> RemoveMarketAnalysisTrigger(enAdminChemicalPrice en)
        {
            return GetDALReference().RemoveMarketAnalysisTrigger(en);
        }
        public List<enAdminChemicalPrice> Getavilability_ProductCountryVarient(enAdminChemicalPrice obj)
        {
            return GetDALReference().Getavilability_ProductCountryVarient(obj);
        }
        public List<enAdminChemicalPrice> Getavilability_ProductCountryVarientRelation(enAdminChemicalPrice obj)
        {
            return GetDALReference().Getavilability_ProductCountryVarientRelation(obj);
        }

        public List<enAdminChemicalPrice> Getavilability_ProductCountryUnit(enAdminChemicalPrice obj)
        {
            return GetDALReference().Getavilability_ProductCountryUnit(obj);
        }

        public List<enAdminChemicalPrice> BindALLCountry(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().BindALLCountry(enAdminChemicalPrice);
        }


        public List<enAdminChemicalPrice> SaveCFRPricingData(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().SaveCFRPricingData(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetAssociateCFRPricebasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetAssociateCFRPricebasedonFilter(enAdminChemicalPrice);
        }
        public List<enAdminChemicalPrice> GetConsultantCFRPricebasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetConsultantCFRPricebasedonFilter(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> UpdateApprovalStatusCFRPrice()
        {
            return GetDALReference().UpdateApprovalStatusCFRPrice();
        }
        public List<enAdminChemicalPrice> DisApprovalMessageCFRPrice()
        {
            return GetDALReference().DisApprovalMessageCFRPrice();
        }

        public List<enAdminChemicalPrice> BindCFRPricingBasedonProductandcountry(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().BindCFRPricingBasedonProductandcountry(enAdminChemicalPrice);
        }
        
        public List<enAdminChemicalPrice> BindUserAssignProduct(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().BindUserAssignProduct(enAdminChemicalPrice);
        }
       
        public List<enAdminChemicalPrice> UpdateCFRPricingData(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().UpdateCFRPricingData(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> ReadAllProductExim()
        {
            return GetDALReference().ReadAllProductExim();
        }

        public List<enAdminChemicalPrice> GetCustomerLoginDetailBasedonSearch(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetCustomerLoginDetailBasedonSearch(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetCustomerLoginDetailDownloadData(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetCustomerLoginDetailDownloadData(enAdminChemicalPrice);
        }
        public List<enCFRPrice> BindCFRPricingBasedonProductandcountryApi(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().BindCFRPricingBasedonProductandcountryApi(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> SaveUploadPriceCatalogData(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().SaveUploadPriceCatalogData(enAdminChemicalPrice);
        }

        public List<enPriceCatalog> GetPriceCatalogListbasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetPriceCatalogListbasedonFilter(enAdminChemicalPrice);
        }


        public List<enPriceCatalog> GetPriceCatalogBasedonProductandCountry(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetPriceCatalogBasedonProductandCountry(enAdminChemicalPrice);
        }

        public List<enPriceCatalog> GetPriceCatalogProductbaseonUser(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetPriceCatalogProductbaseonUser(enAdminChemicalPrice);
        }
        public List<enPriceCatalog> GetPriceCatalogCountryBasedonProduct(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetPriceCatalogCountryBasedonProduct(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> PriceCatalogDownloadTrack(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().PriceCatalogDownloadTrack(enAdminChemicalPrice);
        }

        public List<Product> GetCreditDownloadProducts(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetCreditDownloadProducts(enAdminChemicalPrice);
        }
        public List<GetCRMDetails> GetCRMByUser(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetCRMByUser(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> SaveInternalPriceQuote()
        {
            return GetDALReference().SaveInternalPriceQuote();
        }


        public List<enAdminChemicalPrice> GetAssociateInternalPriceQuoteList(enSearchFilter objChemical)
        {
            return GetDALReference().GetAssociateInternalPriceQuoteList(objChemical);
        }


        public List<enAdminChemicalPrice> SaveUploadOfferPriceData(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().SaveUploadOfferPriceData(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetAssociateOfferPricebasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetAssociateOfferPricebasedonFilter(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetConsultantOfferPricebasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetConsultantOfferPricebasedonFilter(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> UpdateApprovalStatusOfferPrice()
        {
            return GetDALReference().UpdateApprovalStatusOfferPrice();
        }
        public List<enAdminChemicalPrice> DisApprovalMessageOfferPrice()
        {
            return GetDALReference().DisApprovalMessageOfferPrice();
        }

        public List<OfferPrice> BindOfferPricingBasedonProductandcountry(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().BindOfferPricingBasedonProductandcountry(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> EditUploadOfferPriceData(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().EditUploadOfferPriceData(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetOfferPricingDetailsById(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetOfferPricingDetailsById(enAdminChemicalPrice);
        }
        public List<enAdminChemicalPrice> BindIncotermBasedOnProductandCountry(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().BindIncotermBasedOnProductandCountry(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetAssociateLandingPageListbasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetAssociateLandingPageListbasedonFilter(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GetConsultantLandingPageListbasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GetConsultantLandingPageListbasedonFilter(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> GETAllConsultantProduct_Commentary(enAdminChemicalPrice enAdminChemicalPrice)
        {
            return GetDALReference().GETAllConsultantProduct_Commentary(enAdminChemicalPrice);
        }

        public List<enAdminChemicalPrice> get_Two_months_before_loginDetailscrm(enSearchFilter searchFilter)
        {
            return GetDALReference().get_Two_months_before_loginDetailscrm(searchFilter);
        }


        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enChemicalPricing);
            }
            return _objDAL;
        }

    }
}
