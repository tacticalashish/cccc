using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = DataLayer.dlSalesPackageSubscriptions;
using EntityLayer.CustomEntity;

namespace BusinessLayer
{
    public class blSalesPackageSubscriptions
    {
        private enSalesPackageSubscriptions _enSalesPackageSubscriptions = null;
        private DAL _objDAL = null;
        public enSalesPackageSubscriptions GetEntityReference
        {
            get { return this._enSalesPackageSubscriptions; }
        }
        public blSalesPackageSubscriptions(enSalesPackageSubscriptions enSalesPackageSubscriptions_)
        {
            this._enSalesPackageSubscriptions = enSalesPackageSubscriptions_;
        }

        public blSalesPackageSubscriptions()
        {
        }
        public List<enSalesPackageSubscriptions> ReadAllSubscriptions()
        {
            return GetDALReference().ReadAllSubscriptions();
        }
    

        public List<enSalesPackageSubscriptions> GetUserLogDetails(enSalesPackageSubscriptions searchFilter)
        {
            return GetDALReference().GetUserLogDetails(searchFilter);
        }
        public List<enSalesPackageSubscriptions> GetListOfProductOfSubscription(enSalesPackageSubscriptions searchFilter)
        {
            return GetDALReference().GetListOfProductOfSubscription(searchFilter);
        }

        public List<enSalesPackageSubscriptions> GetListOfProductOfSubscription_exim(enSalesPackageSubscriptions searchFilter)
        {
            return GetDALReference().GetListOfProductOfSubscription_exim(searchFilter);
        }
    
        public List<enSalesPackageSubscriptions> CheckCustomerPackage()
        {
            return GetDALReference().CheckCustomerPackage();
        }
        public List<enSalesPackageSubscriptions> ReadAll()
        {
            return GetDALReference().ReadAll();
        }
        public List<enSalesPackageSubscriptions> ReadAllUI()
        {
            return GetDALReference().ReadAllUI();
        }
        

        public List<enSalesPackageSubscriptions> ReadAllSubsCription(enSalesPackageSubscriptions obj)
        {
            return GetDALReference().ReadAllSubsCription(obj);
        }
        public List<enSalesPackageSubscriptions> ReadAllSubsCriptionDetails(enSalesPackageSubscriptions obj)
        {
            return GetDALReference().ReadAllSubsCriptionDetails(obj);
        }
        
        public List<enSalesPackageSubscriptions> LeadMasterReadAll()
        {
            return GetDALReference().LeadMasterReadAll();
        }

        public void UpdateStatus()
        {
            GetDALReference().UpdateStatus();
        }

        public List<enSalesPackageSubscriptions> ReadBasedOnSearchFilter(enSearchFilter searchFilter)
        {
            return GetDALReference().ReadBasedOnSearchFilter(searchFilter);
        }
        public List<enSalesPackageSubscriptions> ReadBasedOnEmail(enSearchFilter searchFilter)
        {
            return GetDALReference().ReadBasedOnEmail(searchFilter);
        }


        public List<enSalesPackageSubscriptions> UpdateBasedOnEmail(enSearchFilter searchFilter)
        {
            return GetDALReference().UpdateBasedOnEmail(searchFilter);
        }

        public List<enSalesPackageSubscriptions> GetMySubscription(enSearchFilter searchFilter)
        {
            return GetDALReference().GetMySubscription(searchFilter);
        }


        public List<enSalesPackageSubscriptions> GetAllCustomerEmail(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return GetDALReference().GetAllCustomerEmail(enSalesPackageSubscriptions);
        }

        public async Task<List<enSalesPackageSubscriptions>> GetUserType(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return await GetDALReference().GetUserType(enSalesPackageSubscriptions);
        }

        public List<enSalesPackageSubscriptions> GetCreditScore(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return GetDALReference().GetCreditScore(enSalesPackageSubscriptions);
        }

        public List<enSalesPackageSubscriptions> Maintainlog_CreditScore(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return GetDALReference().Maintainlog_CreditScore(enSalesPackageSubscriptions);
        }

        public async Task<List<enSalesPackageSubscriptions>> GetCustomerCreditscore(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return await GetDALReference().GetCustomerCreditscore(enSalesPackageSubscriptions);
        }

        public List<enSalesPackageSubscriptions> MarketAnalysisDownloadPdfExcel_log(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return GetDALReference().MarketAnalysisDownloadPdfExcel_log(enSalesPackageSubscriptions);
        }
        public List<enChemicalPricingSession> ReadAllChemicalPricing()
        {
            return GetDALReference().ReadAllChemicalPricing();
        }
        public List<enChemicalPricingSession> ReadAllMarketAnalysis()
        {
            return GetDALReference().ReadAllMarketAnalysis();
        }


        public List<enSalesPackageSubscriptions> GetCustomerPrimaryEmail()
        {
            return GetDALReference().GetCustomerPrimaryEmail();
        }

        public List<enSalesPackageSubscriptions> GetCompanySecondaryEmail(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return GetDALReference().GetCompanySecondaryEmail(enSalesPackageSubscriptions);
        }
        public List<enSalesPackageSubscriptions> DeleteCompanySecondaryEmail(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return GetDALReference().DeleteCompanySecondaryEmail(enSalesPackageSubscriptions);
        }


        public List<enSalesPackageSubscriptions> DisruptionProductTracking(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return GetDALReference().DisruptionProductTracking(enSalesPackageSubscriptions);
        }


        public List<enSalesPackageSubscriptions> MarketProductTracking(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return GetDALReference().MarketProductTracking(enSalesPackageSubscriptions);
        }

        public List<enSalesPackageSubscriptions> Maintainlog_CreditScoreonZero(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return GetDALReference().Maintainlog_CreditScoreonZero(enSalesPackageSubscriptions);
        }

        //--------------------
        public List<enChemicalPricingSession> ReadAllMarketAnalysisProductForApi()
        {
            return GetDALReference().ReadAllMarketAnalysisProductForApi();
        }
        public List<enChemicalPricingSession> ReadAllMarketAnalysisCountryForApi()
        {
            return GetDALReference().ReadAllMarketAnalysisCountryForApi();
        }
        public List<enChemicalPricingSession> ReadAllMarketAnalysisRangeForApi()
        {
            return GetDALReference().ReadAllMarketAnalysisRangeForApi();
        }

        public List<enChemicalPricingSession> GetMarketAnalysisYear()
        {
            return GetDALReference().GetMarketAnalysisYear();
        }
        public List<enChemicalPricingSession> ReadAllMarketAnalysisCountryCompareForApi()
        {
            return GetDALReference().ReadAllMarketAnalysisCountryCompareForApi();
        }
        public List<enAllclientAllSubs> GellAllClintAllSubsciption(enSearchFilter obj)
        {
            return GetDALReference().GellAllClintAllSubsciption(obj);
        }
     
        public List<enAllclientAllSubs> GetCustomerByProduct(enSearchFilter obj)
        {
            return GetDALReference().GetCustomerByProduct(obj);
        }
        public List<enAllclientAllSubs> GetCustomerISApiAccess(enSearchFilter en)
        {
            return GetDALReference().GetCustomerISApiAccess(en);
        }
        public List<enAllclientAllSubs> GetCustomerModule(enSearchFilter obj)
        {
            return GetDALReference().GetCustomerModule(obj);
        }
        public List<enAllclientAllSubs> GetCustomerPrimary(enSearchFilter en)
        {
            return GetDALReference().GetCustomerPrimary(en);
        }


        public List<enSalesPackageSubscriptions> GetAllCustomerEmailbyCompany(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return GetDALReference().GetAllCustomerEmailbyCompany(enSalesPackageSubscriptions);
        }

        public List<enSalesPackageSubscriptions> GetAllCustomerLoginDetailsbyCompany(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return GetDALReference().GetAllCustomerLoginDetailsbyCompany(enSalesPackageSubscriptions);
        }

        public List<enSalesPackageSubscriptions> GetAllLoginProductDetailsbyID(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return GetDALReference().GetAllLoginProductDetailsbyID(enSalesPackageSubscriptions);
        }

        public List<enSalesPackageSubscriptions> GetCompanyMasterData(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            return GetDALReference().GetCompanyMasterData(enSalesPackageSubscriptions);
        }

        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enSalesPackageSubscriptions);
            }
            return _objDAL;
        }
      


    }
}
