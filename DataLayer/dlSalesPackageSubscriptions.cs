using EntityLayer;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using EntityLayer.CustomEntity;
using System.Data.SqlClient;

namespace DataLayer
{
    public class dlSalesPackageSubscriptions
    {
        private enSalesPackageSubscriptions _enSalesPackageSubscriptions = null;
        public dlSalesPackageSubscriptions(enSalesPackageSubscriptions enSalesPackageSubscriptions_)
        {
            this._enSalesPackageSubscriptions = enSalesPackageSubscriptions_;
        }        
        public List<enSalesPackageSubscriptions> LeadMasterReadAll()
        {
            List<enSalesPackageSubscriptions> listOfSalesPackageSubscriptions = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_LeadMaster_READ]", new object[] {
                                                            _enSalesPackageSubscriptions.IDs }))
            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    objENSalesPackageSubscriptions.ID = Convert.ToInt32(idr["ID"]);
                    objENSalesPackageSubscriptions.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
                    objENSalesPackageSubscriptions.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
                    objENSalesPackageSubscriptions.Organisation = DBNull.Value == idr["Organisation"] ? null : Convert.ToString(idr["Organisation"]);
                    objENSalesPackageSubscriptions.InterestArea = DBNull.Value == idr["InterestArea"] ? null : Convert.ToString(idr["InterestArea"]);
                    objENSalesPackageSubscriptions.UserType = DBNull.Value == idr["UserType"] ? null : Convert.ToString(idr["UserType"]);
                    objENSalesPackageSubscriptions.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }
          return listOfSalesPackageSubscriptions;
        }     
       public List<enSalesPackageSubscriptions> ReadAllSubsCription(enSalesPackageSubscriptions obj)
        {
            List<enSalesPackageSubscriptions> listOfSalesPackageSubscriptions = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_SalesPackageSubscriptions_READ_New]", new object[] {
                                                            obj.UserID,
                                                            obj.ToExpire }))
            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructObjectAllSubsCription(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }
        public List<enSalesPackageSubscriptions> GetListOfProductOfSubscription(enSalesPackageSubscriptions obj)
        {
            List<enSalesPackageSubscriptions> listOfSalesPackageSubscriptions = new List<enSalesPackageSubscriptions>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetListOfProductOfSubscription]", new object[] {
                                                            obj.UserID,
            }))
            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructObjectAllSubsCriptionProduct(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }
            return listOfSalesPackageSubscriptions;
        }

        public List<enSalesPackageSubscriptions> GetListOfProductOfSubscription_exim(enSalesPackageSubscriptions obj)
        {
            List<enSalesPackageSubscriptions> listOfSalesPackageSubscriptions = new List<enSalesPackageSubscriptions>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetListOfProductOfSubscription_Exim]", new object[] {
                                                            obj.UserID,
            }))
            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructObjectAllSubsCriptionProduct(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }
            return listOfSalesPackageSubscriptions;
        }

      

        public List<enSalesPackageSubscriptions> ReadAllSubsCriptionDetails(enSalesPackageSubscriptions obj)
        {
            List<enSalesPackageSubscriptions> listOfSalesPackageSubscriptions = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_SalesPackageSubscriptions_DistinctProduct]", new object[] {
                                                            obj.UserID,
                                                            obj.ToExpire }))
            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructObjectAllSubsCriptionDetails(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }
        public List<enSalesPackageSubscriptions> ReadAllSubscriptions()
        {
            List<enSalesPackageSubscriptions> listOfSubscriptions = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetSubscription_Details]", new object[] {
                                                            _enSalesPackageSubscriptions.UserID,
                                                           }))
            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructObjectForSubscriptions(idr, objENSalesPackageSubscriptions);
                    listOfSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSubscriptions;
        }
        public List<enSalesPackageSubscriptions> ReadAllUI()
        {
            List<enSalesPackageSubscriptions> listOfSalesPackageSubscriptions = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_SalesPackageSubscriptions_READ_UI]", new object[] {
                                                            _enSalesPackageSubscriptions.UserID,
                                                            _enSalesPackageSubscriptions.ToDate,
                                                            _enSalesPackageSubscriptions.Email,
                                                              _enSalesPackageSubscriptions.Language

            }))
            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructObject(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }

        public List<enSalesPackageSubscriptions> ReadAll()
        {
            List<enSalesPackageSubscriptions> listOfSalesPackageSubscriptions = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_SalesPackageSubscriptions_READ]", new object[] {
                                                            _enSalesPackageSubscriptions.UserID,
                                                            _enSalesPackageSubscriptions.ToDate,
                                                        

            }))
            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructObject(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }


        public List<enSalesPackageSubscriptions> CheckCustomerPackage()
        {
            List<enSalesPackageSubscriptions> listOfSalesPackageSubscriptions = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_CheckCustomerPackage]", new object[] {
                                                            _enSalesPackageSubscriptions.UserID
                                                            }))
            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructObjectCheckCustomerPackage(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }
        public List<enSalesPackageSubscriptions> GetUserLogDetails(enSalesPackageSubscriptions searchFilter)
        {
            List<enSalesPackageSubscriptions> UserDetails = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetUserDetails]", new object[] {
                                                            searchFilter.Language,
                                                            searchFilter.UserType,
                                                             searchFilter.UserID
                                                  }))
            {
                while (idr.Read())
                {
                    //enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    //ConstructObject(idr, objENSalesPackageSubscriptions);
                    //UserDetails.Add(objENSalesPackageSubscriptions);
                }
            }

            return UserDetails;
        }
        public List<enSalesPackageSubscriptions> ReadBasedOnSearchFilter(enSearchFilter searchFilter)
        {
            List<enSalesPackageSubscriptions> listOfSalesPackageSubscriptions = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Report_Subscription_CustomSearch]", new object[] {
                                                            searchFilter.Region,
                                                            searchFilter.Country,
                                                            searchFilter.Product,
                                                            searchFilter.Menu,
                                                            searchFilter.Status,
                                                            searchFilter.Customer,
                                                            searchFilter.SubscriptionType,
                                                            searchFilter.FromDate,
                                                            searchFilter.ToDate,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber
                                                             }))
            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructObject(idr, objENSalesPackageSubscriptions);

                    objENSalesPackageSubscriptions.CustomerID = DBNull.Value == idr["CustomerID"] ? (int?)null : Convert.ToInt32(idr["CustomerID"]);
                    objENSalesPackageSubscriptions.CustomerName = DBNull.Value == idr["CustomerName"] ? null : Convert.ToString(idr["CustomerName"]);
                    objENSalesPackageSubscriptions.CustomerEmail = DBNull.Value == idr["CustomerEmail"] ? null : Convert.ToString(idr["CustomerEmail"]);
                    objENSalesPackageSubscriptions.RegionName = DBNull.Value == idr["RegionName"] ? null : Convert.ToString(idr["RegionName"]);
                    objENSalesPackageSubscriptions.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
                    objENSalesPackageSubscriptions.RowNumber = Convert.ToInt32(idr["RowNumber"]);
                    objENSalesPackageSubscriptions.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);

                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }


        public void UpdateStatus()
        {

            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_SalesPackageSubscriptions_UpdateStatus]", new object[] {
                                                                    _enSalesPackageSubscriptions.ID,
                                                                    _enSalesPackageSubscriptions.Status
                                                                    });


        }
        public List<enSalesPackageSubscriptions> ReadBasedOnEmail(enSearchFilter enSearchFilter)
        {
            List<enSalesPackageSubscriptions> listOfSalesPackageSubscriptions = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[SP_GetCustFlag]", new object[] {
                                                            enSearchFilter.Email
                                                                }))

            {

                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructSalesPackageObject(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);

                }
            }

            return listOfSalesPackageSubscriptions;
        }



        private void ConstructSalesPackageObject(IDataReader idr, enSalesPackageSubscriptions objENSalesPackageSubscriptions)
        {
            objENSalesPackageSubscriptions.CustNotificationFlag = Convert.ToInt32(idr["CustNotificationFlag"]);
            objENSalesPackageSubscriptions.ProductName = Convert.ToString(idr["ProductName"]);
            objENSalesPackageSubscriptions.CountryName = Convert.ToString(idr["CountryName"]);

        }


        public List<enSalesPackageSubscriptions> UpdateBasedOnEmail(enSearchFilter enSearchFilter)
        {
            List<enSalesPackageSubscriptions> listOfSalesPackageSubscriptions = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Sp_UpdateCustFlag]", new object[] {
                                                            enSearchFilter.Email
                                                                }))

            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructUpdateObject(idr, objENSalesPackageSubscriptions);

                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);

                }
            }

            return listOfSalesPackageSubscriptions;
        }



        private void ConstructUpdateObject(IDataReader idr, enSalesPackageSubscriptions objENSalesPackageSubscriptions)
        {
        }


        public List<enSalesPackageSubscriptions> GetMySubscription(enSearchFilter enSearchFilter)
        {
            List<enSalesPackageSubscriptions> ListofMySubscription = new List<enSalesPackageSubscriptions>();
          
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Mysubscription]", new object[] {
                                                            enSearchFilter.Email
                                                                }))

            {

                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructMySubscription(idr, objENSalesPackageSubscriptions);
                    ListofMySubscription.Add(objENSalesPackageSubscriptions);
                }
            }
          
            return ListofMySubscription;
        }


        public List<enSalesPackageSubscriptions> GetAllCustomerEmail(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetAllCustomerEmail = new List<enSalesPackageSubscriptions>();
          
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAllCustomerEmail]", new object[] {
                                                            //enSalesPackageSubscriptions.Email
                                                                }))

                {

                    while (idr.Read())
                    {
                        enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                        ConstructGetAllCustomerEmail(idr, objENSalesPackageSubscriptions);
                        ListofGetAllCustomerEmail.Add(objENSalesPackageSubscriptions);
                    }
                }
          
            return ListofGetAllCustomerEmail;
        }

        public async Task<List<enSalesPackageSubscriptions>> GetUserType(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetUserType = new List<enSalesPackageSubscriptions>();
            await Task.Run(() =>
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetUsertypeforTrader]", new object[] {
                                                            enSalesPackageSubscriptions.Email
                                                                }))

            {

                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructGetUserType(idr, objENSalesPackageSubscriptions);
                    ListofGetUserType.Add(objENSalesPackageSubscriptions);
                }
            }
         });
            return ListofGetUserType;
        }


        public List<enSalesPackageSubscriptions> GetCreditScore(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetCreditScore = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetUserCreditScore]", new object[] {
                                                            enSalesPackageSubscriptions.Email
                                                                }))

            {

                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructGetCreditScore(idr, objENSalesPackageSubscriptions);
                    ListofGetCreditScore.Add(objENSalesPackageSubscriptions);
                }
            }
            return ListofGetCreditScore;
        }


        public List<enSalesPackageSubscriptions> Maintainlog_CreditScore(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetCreditScore = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_CheckTraderProductForDownload]", new object[] {
                                                            enSalesPackageSubscriptions.Email,
                                                            enSalesPackageSubscriptions.ProductID,
                                                            enSalesPackageSubscriptions.Credit,
                                                            enSalesPackageSubscriptions.DownloadType,
                                                            enSalesPackageSubscriptions.Productcompare
                                                                }))

            {

                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructDeleteCompanySecondaryEmail(idr, objENSalesPackageSubscriptions);
                    ListofGetCreditScore.Add(objENSalesPackageSubscriptions);
                }
            }
            return ListofGetCreditScore;
        }

        public async Task<List<enSalesPackageSubscriptions>> GetCustomerCreditscore(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetCustomerCreditscore = new List<enSalesPackageSubscriptions>();
            await Task.Run(() =>
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetCustomerCreditscore]", new object[] {
                                                            enSalesPackageSubscriptions.Email
                                                                }))

                {
                    while (idr.Read())
                    {
                        enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                        ConstructGetCustomerCreditscore(idr, objENSalesPackageSubscriptions);
                        ListofGetCustomerCreditscore.Add(objENSalesPackageSubscriptions);
                    }
                }
            });
            return ListofGetCustomerCreditscore;
        }


        public List<enSalesPackageSubscriptions> MarketAnalysisDownloadPdfExcel_log(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetCreditScore = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Insert_SA_MarketAnalysisDownloadPdfExcel_log]", new object[] {
                                                            enSalesPackageSubscriptions.Email,
                                                            enSalesPackageSubscriptions.UserID,
                                                            enSalesPackageSubscriptions.ProductID,
                                                            enSalesPackageSubscriptions.Range,
                                                            enSalesPackageSubscriptions.DownloadType
                                                                }))


                return ListofGetCreditScore;
        }

        public List<enChemicalPricingSession> ReadAllChemicalPricing()
        {
            List<enChemicalPricingSession> listOfSalesPackageSubscriptions = new List<enChemicalPricingSession>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_ReadAllChemicalPricingSubscription]", new object[] {
                                                            _enSalesPackageSubscriptions.UserID,
                /*_enSalesPackageSubscriptions.ToDate*/
            }))
            {
                while (idr.Read())
                {
                    enChemicalPricingSession objENSalesPackageSubscriptions = new enChemicalPricingSession();
                    ConstructObjectSalesPackageSubscriptionPricing(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }
        public List<enChemicalPricingSession> ReadAllMarketAnalysis()
        {
            List<enChemicalPricingSession> listOfSalesPackageSubscriptions = new List<enChemicalPricingSession>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_ReadAllMarketAnalysisPricingSubscription]", new object[] {
                                                            _enSalesPackageSubscriptions.UserID,
                                                            _enSalesPackageSubscriptions.Language,
                                                            _enSalesPackageSubscriptions.Email,
                /*_enSalesPackageSubscriptions.ToDate*/
            }))
            {
                while (idr.Read())
                {
                    enChemicalPricingSession objENSalesPackageSubscriptions = new enChemicalPricingSession();
                    ConstructObjectSalesPackageSubscriptionMarket(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }


        public List<enSalesPackageSubscriptions> GetCustomerPrimaryEmail()
        {
            List<enSalesPackageSubscriptions> ListofGetCustomerPrimaryEmail = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetPrimaryCustomers]", new object[] {

            }))
            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructGetCustomerPrimaryEmail(idr, objENSalesPackageSubscriptions);
                    ListofGetCustomerPrimaryEmail.Add(objENSalesPackageSubscriptions);
                }
            }
            return ListofGetCustomerPrimaryEmail;
        }

        public List<enSalesPackageSubscriptions> GetCompanySecondaryEmail(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetCompanySecondaryEmail = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetCompanySecondaryCustomer]", new object[] {
                                                             enSalesPackageSubscriptions.CompanyPrimaryId
                                                                }))
            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructGetCustomerPrimaryEmail(idr, objENSalesPackageSubscriptions);
                    ListofGetCompanySecondaryEmail.Add(objENSalesPackageSubscriptions);
                }
            }
            return ListofGetCompanySecondaryEmail;
        }

        public List<enSalesPackageSubscriptions> DeleteCompanySecondaryEmail(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofDeleteCompanySecondaryEmail = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_DeleteCompanySecondayUser]", new object[] {
                                                             enSalesPackageSubscriptions.CompanyPrimaryId
                                                                }))
            {
                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructDeleteCompanySecondaryEmail(idr, objENSalesPackageSubscriptions);
                    ListofDeleteCompanySecondaryEmail.Add(objENSalesPackageSubscriptions);
                }
            }
            return ListofDeleteCompanySecondaryEmail;
        }



        public List<enSalesPackageSubscriptions> DisruptionProductTracking(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetCreditScore = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Insert_SA_DisruptionProductTracking]", new object[] {
                                                            enSalesPackageSubscriptions.Email,
                                                            enSalesPackageSubscriptions.UserID,
                                                            enSalesPackageSubscriptions.ProductID


                                                                }))

                return ListofGetCreditScore;
        }

        public List<enSalesPackageSubscriptions> MarketProductTracking(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetCreditScore = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Insert_SA_MarketAnalysisProductTracking]", new object[] {
                                                            enSalesPackageSubscriptions.Email,
                                                            enSalesPackageSubscriptions.UserID,
                                                            enSalesPackageSubscriptions.ProductID,
                                                            enSalesPackageSubscriptions.Range,
                                                            enSalesPackageSubscriptions.CountryName,
                                                            enSalesPackageSubscriptions.QuarterlyCompare

                                                                }))

                return ListofGetCreditScore;
        }

        public List<enSalesPackageSubscriptions> Maintainlog_CreditScoreonZero(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetCreditScore = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_CheckTraderProductForDownloadForZero]", new object[] {
                                                            enSalesPackageSubscriptions.Email,
                                                            enSalesPackageSubscriptions.ProductID,
                                                            enSalesPackageSubscriptions.Credit,
                                                            enSalesPackageSubscriptions.DownloadType
                                                                }))

            {

                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructDeleteCompanySecondaryEmail(idr, objENSalesPackageSubscriptions);
                    ListofGetCreditScore.Add(objENSalesPackageSubscriptions);
                }
            }
            return ListofGetCreditScore;
        }



        //-------------------------------------
        public List<enChemicalPricingSession> ReadAllMarketAnalysisProductForApi()
        {
            List<enChemicalPricingSession> listOfSalesPackageSubscriptions = new List<enChemicalPricingSession>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_ReadAllMarketAnalysisProductSubscription_Api]", new object[] {
                                                            _enSalesPackageSubscriptions.UserID,


            }))
            {
                while (idr.Read())
                {
                    enChemicalPricingSession objENSalesPackageSubscriptions = new enChemicalPricingSession();
                    ConstructObjectAllSubsCriptionProductsubs(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }
        public List<enChemicalPricingSession> ReadAllMarketAnalysisCountryForApi()
        {
            List<enChemicalPricingSession> listOfSalesPackageSubscriptions = new List<enChemicalPricingSession>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_ReadAllMarketAnalysisCountrySubscription_Api]", new object[] {
                                            _enSalesPackageSubscriptions.ProductID,
                                            _enSalesPackageSubscriptions.UserID



            }))
            {
                while (idr.Read())
                {
                    enChemicalPricingSession objENSalesPackageSubscriptions = new enChemicalPricingSession();
                    ConstructObjectAllSubsCriptionCountrysubs(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }
        public List<enChemicalPricingSession> ReadAllMarketAnalysisRangeForApi()
        {
            List<enChemicalPricingSession> listOfSalesPackageSubscriptions = new List<enChemicalPricingSession>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_ReadAllMarketAnalysisRangeSubscription_Api]", new object[] {
                                                        _enSalesPackageSubscriptions.ProductID,
                                                        _enSalesPackageSubscriptions.UserID,
                                                        _enSalesPackageSubscriptions.CountryName


            }))
            {
                while (idr.Read())
                {
                    enChemicalPricingSession objENSalesPackageSubscriptions = new enChemicalPricingSession();
                    ConstructObjectAllSubsCriptionRange(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }
        public List<enChemicalPricingSession> GetMarketAnalysisYear()
        {
            List<enChemicalPricingSession> listOfSalesPackageSubscriptions = new List<enChemicalPricingSession>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_SA_MarketAnalysisYearly_READ]", new object[] {
                                                        _enSalesPackageSubscriptions.MarketAnalysisRange,
                                                        _enSalesPackageSubscriptions.ProductID,
                                                        _enSalesPackageSubscriptions.CountryName,
                                                        _enSalesPackageSubscriptions.RegionName


            }))
            {
                while (idr.Read())
                {
                    enChemicalPricingSession objENSalesPackageSubscriptions = new enChemicalPricingSession();
                    ConstructObjectAllSubsCriptionYearly(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }

        public List<enChemicalPricingSession> ReadAllMarketAnalysisCountryCompareForApi()
        {
            List<enChemicalPricingSession> listOfSalesPackageSubscriptions = new List<enChemicalPricingSession>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_ReadAllMarketAnalysisCountryCompare_Api]", new object[] {
                                            _enSalesPackageSubscriptions.ProductID,
                                            _enSalesPackageSubscriptions.UserID,
                                            _enSalesPackageSubscriptions.Range,
                                            _enSalesPackageSubscriptions.CountryRegion,
                                            _enSalesPackageSubscriptions.isRegionCountry


            }))
            {
                while (idr.Read())
                {
                    enChemicalPricingSession objENSalesPackageSubscriptions = new enChemicalPricingSession();
                    ConstructObjectAllSubsCriptionCountrycomparesubs(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }


        public List<enAllclientAllSubs> GellAllClintAllSubsciption(enSearchFilter searchFilter)
        {
            List<enAllclientAllSubs> listOfSalesPackageSubscriptions = new List<enAllclientAllSubs>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetAllSubscription]", new object[] {
                                                           searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                            searchFilter.UserId,
                                                            searchFilter.Type,
                                                             searchFilter.CrmId,
            }))
            {
                while (idr.Read())
                {
                    enAllclientAllSubs objENSalesPackageSubscriptions = new enAllclientAllSubs();
                    ConstructObjectGellAllClintAllSubsciption(idr, objENSalesPackageSubscriptions);
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                }
            }

            return listOfSalesPackageSubscriptions;
        }
  
        public List<enAllclientAllSubs> GetCustomerByProduct(enSearchFilter obj)
        {
            List<enAllclientAllSubs> listofdata = new List<enAllclientAllSubs>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetSuscriptionDetailsFilter]", new object[] {
                                                           "Product",
                                                           obj.ProductId,
                                                           obj.UserId
            }))
            {
                while (idr.Read())
                {
                    enAllclientAllSubs objENSalesPackageSubscriptions = new enAllclientAllSubs();
                    ConstructObjectCustomer(idr, objENSalesPackageSubscriptions);
                    listofdata.Add(objENSalesPackageSubscriptions);
                }
            }

            return listofdata;
        }
        public List<enAllclientAllSubs> GetCustomerISApiAccess(enSearchFilter obj)
        {
            List<enAllclientAllSubs> listofdata = new List<enAllclientAllSubs>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetSuscriptionDetailsFilter]", new object[] {
                                                           "API",
                                                            "0",
                                                            obj.UserId//--Null
            }))
            {
                while (idr.Read())
                {
                    enAllclientAllSubs objENSalesPackageSubscriptions = new enAllclientAllSubs();
                    ConstructObjectApi(idr, objENSalesPackageSubscriptions);
                    listofdata.Add(objENSalesPackageSubscriptions);
                }
            }

            return listofdata;
        }
        public List<enAllclientAllSubs> GetCustomerModule(enSearchFilter OBJ)
        {
            List<enAllclientAllSubs> listofdata = new List<enAllclientAllSubs>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetSuscriptionDetailsFilter]", new object[] {
                                                           "MODULE",
                                                            OBJ.Menu,
                                                            OBJ.UserId
            }))
            {
                while (idr.Read())
                {
                    enAllclientAllSubs objENSalesPackageSubscriptions = new enAllclientAllSubs();
                    ConstructObjectModuleName(idr, objENSalesPackageSubscriptions);
                    listofdata.Add(objENSalesPackageSubscriptions);
                }
            }

            return listofdata;
        }
        private void ConstructObjectCustomer(IDataReader idr, enAllclientAllSubs enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enSalesPackageSubscriptions_.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enSalesPackageSubscriptions_.ModuleName = DBNull.Value == idr["ModuleName"] ? null : Convert.ToString(idr["ModuleName"]);

        }

      

        private void ConstructObjectApi(IDataReader idr, enAllclientAllSubs enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enSalesPackageSubscriptions_.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enSalesPackageSubscriptions_.ApiAccess = DBNull.Value == idr["ApiAccess"] ? null : Convert.ToString(idr["ApiAccess"]);
            enSalesPackageSubscriptions_.SubcriptionEndate = DBNull.Value == idr["SubcriptionEndate"] ? null : Convert.ToString(idr["SubcriptionEndate"]);
        }

        private void ConstructObjectModuleName(IDataReader idr, enAllclientAllSubs enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enSalesPackageSubscriptions_.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enSalesPackageSubscriptions_.ModuleName = DBNull.Value == idr["ModuleName"] ? null : Convert.ToString(idr["ModuleName"]);
        }

        public List<enAllclientAllSubs> GetCustomerPrimary(enSearchFilter obj)
        {
            List<enAllclientAllSubs> listofdata = new List<enAllclientAllSubs>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetSuscriptionDetailsFilter]", new object[] {
                                                           "PrimarySecondary",
                                                            "0",//NULL
                                                             obj.UserId
            }))
            {
                while (idr.Read())
                {
                    enAllclientAllSubs objENSalesPackageSubscriptions = new enAllclientAllSubs();
                    ConstructObjectPrimarySecondary(idr, objENSalesPackageSubscriptions);
                    listofdata.Add(objENSalesPackageSubscriptions);
                }
            }

            return listofdata;
        }



       


        public List<enSalesPackageSubscriptions> GetAllCustomerEmailbyCompany(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetAllCustomerEmail = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAllCustomerEmailbyCompany]", new object[] {
                                                            enSalesPackageSubscriptions.CompanyName,
                                                             enSalesPackageSubscriptions.FromDate,
                                                              enSalesPackageSubscriptions.ToDate
                                                                }))

            {

                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructGetAllCustomerEmailBYCompany(idr, objENSalesPackageSubscriptions);
                    ListofGetAllCustomerEmail.Add(objENSalesPackageSubscriptions);
                }
            }

            return ListofGetAllCustomerEmail;
        }

        public List<enSalesPackageSubscriptions> GetAllCustomerLoginDetailsbyCompany(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetAllCustomerEmail = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAllLoginCountbyID]", new object[] {
                                                            enSalesPackageSubscriptions.ID,
                                                             enSalesPackageSubscriptions.FromDate,
                                                              enSalesPackageSubscriptions.ToDate
                                                                }))

            {

                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructGetAllLoginDetailsBYCompany(idr, objENSalesPackageSubscriptions);
                    ListofGetAllCustomerEmail.Add(objENSalesPackageSubscriptions);
                }
            }

            return ListofGetAllCustomerEmail;
        }


        public List<enSalesPackageSubscriptions> GetAllLoginProductDetailsbyID(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetAllCustomerEmail = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAllLoginProductDetailsbyID]", new object[] {
                                                            enSalesPackageSubscriptions.ID,
                                                             enSalesPackageSubscriptions.UserType,
                                                             enSalesPackageSubscriptions.FromDate,
                                                              enSalesPackageSubscriptions.ToDate
                                                                }))

            {

                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructGetAllProductDetailsbyID(idr, objENSalesPackageSubscriptions);
                    ListofGetAllCustomerEmail.Add(objENSalesPackageSubscriptions);
                }
            }

            return ListofGetAllCustomerEmail;
        }


        public List<enSalesPackageSubscriptions> GetCompanyMasterData(enSalesPackageSubscriptions enSalesPackageSubscriptions)
        {
            List<enSalesPackageSubscriptions> ListofGetAllCustomerEmail = new List<enSalesPackageSubscriptions>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetCompanyMasterData]", new object[] {
                                                                }))

            {

                while (idr.Read())
                {
                    enSalesPackageSubscriptions objENSalesPackageSubscriptions = new enSalesPackageSubscriptions();
                    ConstructGetAllCompanyName(idr, objENSalesPackageSubscriptions);
                    ListofGetAllCustomerEmail.Add(objENSalesPackageSubscriptions);
                }
            }

            return ListofGetAllCustomerEmail;
        }


        private void ConstructGetAllCustomerEmailBYCompany(IDataReader idr, enSalesPackageSubscriptions objENSalesPackageSubscriptions)
        {
            objENSalesPackageSubscriptions.ID = Convert.ToInt32(idr["ID"]);
            objENSalesPackageSubscriptions.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            objENSalesPackageSubscriptions.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENSalesPackageSubscriptions.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objENSalesPackageSubscriptions.CompanyPrimaryId = DBNull.Value == idr["ParentPrimaryId"] ? 0 : Convert.ToInt32(idr["ParentPrimaryId"]);
        }

        private void ConstructGetAllLoginDetailsBYCompany(IDataReader idr, enSalesPackageSubscriptions objENSalesPackageSubscriptions)
        {
            objENSalesPackageSubscriptions.CommodityCount = DBNull.Value == idr["CommodityCount"] ? 0 : Convert.ToInt32(idr["CommodityCount"]);
            objENSalesPackageSubscriptions.DisruptionCount = DBNull.Value == idr["DisruptionCount"] ? 0 : Convert.ToInt32(idr["DisruptionCount"]);
            objENSalesPackageSubscriptions.EximCount = DBNull.Value == idr["EximCount"] ? 0 : Convert.ToInt32(idr["EximCount"]);
            objENSalesPackageSubscriptions.FreightIndexCount = DBNull.Value == idr["FreightIndexCount"] ? 0 : Convert.ToInt32(idr["FreightIndexCount"]);
            objENSalesPackageSubscriptions.MarketAnalysisCount = DBNull.Value == idr["MarketAnalysisCount"] ? 0 : Convert.ToInt32(idr["MarketAnalysisCount"]);
            objENSalesPackageSubscriptions.ApiCount = DBNull.Value == idr["APICount"] ? 0 : Convert.ToInt32(idr["APICount"]);
        }

        private void ConstructGetAllProductDetailsbyID(IDataReader idr, enSalesPackageSubscriptions objENSalesPackageSubscriptions)
        {
            objENSalesPackageSubscriptions.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            objENSalesPackageSubscriptions.RecordsCount = DBNull.Value == idr["ProductCount"] ? 0 : Convert.ToInt32(idr["ProductCount"]);
            objENSalesPackageSubscriptions.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENSalesPackageSubscriptions.Range = DBNull.Value == idr["Range"] ? null : Convert.ToString(idr["Range"]);

        }

        private void ConstructGetAllCompanyName(IDataReader idr, enSalesPackageSubscriptions objENSalesPackageSubscriptions)
        {
            objENSalesPackageSubscriptions.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);

        }
        private void ConstructObjectPrimarySecondary(IDataReader idr, enAllclientAllSubs enSalesPackageSubscriptions_)
        {

            enSalesPackageSubscriptions_.ToDate = DBNull.Value == idr["ToDate"] ? null : Convert.ToString(idr["ToDate"]);
            enSalesPackageSubscriptions_.ClientStatus = DBNull.Value == idr["ClientStatus"] ? null : Convert.ToString(idr["ClientStatus"]);
            enSalesPackageSubscriptions_.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enSalesPackageSubscriptions_.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enSalesPackageSubscriptions_.UserName = DBNull.Value == idr["UserName"] ? null : Convert.ToString(idr["UserName"]);
            enSalesPackageSubscriptions_.PrimarySecondary = DBNull.Value == idr["PrimarySecondary"] ? null : Convert.ToString(idr["PrimarySecondary"]);
        }

        private void ConstructObjectGellAllClintAllSubsciption(IDataReader idr, enAllclientAllSubs enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            enSalesPackageSubscriptions_.UserName = DBNull.Value == idr["UserName"] ? null : Convert.ToString(idr["UserName"]);
            enSalesPackageSubscriptions_.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enSalesPackageSubscriptions_.PrimarySecondary = DBNull.Value == idr["PrimarySecondary"] ? null : Convert.ToString(idr["PrimarySecondary"]);
            enSalesPackageSubscriptions_.AccountStatus = DBNull.Value == idr["AccountStatus"] ? null : Convert.ToString(idr["AccountStatus"]);
            enSalesPackageSubscriptions_.FromDate = DBNull.Value == idr["FromDate"] ? null : Convert.ToString(idr["FromDate"]);
            enSalesPackageSubscriptions_.ToDate = DBNull.Value == idr["ToDate"] ? null : Convert.ToString(idr["ToDate"]);
            enSalesPackageSubscriptions_.ModuleName = DBNull.Value == idr["ModuleName"] ? null : Convert.ToString(idr["ModuleName"]);
            enSalesPackageSubscriptions_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enSalesPackageSubscriptions_.CountryRegion = DBNull.Value == idr["CountryRegion"] ? null : Convert.ToString(idr["CountryRegion"]);
            enSalesPackageSubscriptions_.ClintType = DBNull.Value == idr["ClintType"] ? null : Convert.ToString(idr["ClintType"]);
            enSalesPackageSubscriptions_.ApiAccess = DBNull.Value == idr["ApiAccess"] ? null : Convert.ToString(idr["ApiAccess"]);
            enSalesPackageSubscriptions_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enSalesPackageSubscriptions_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
        }

        private void ConstructObjectAllSubsCriptionCountrycomparesubs(IDataReader idr, enChemicalPricingSession enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.CountryRegionId = DBNull.Value == idr["CountryRegionID"] ? 0 : Convert.ToInt32(idr["CountryRegionID"]);
            enSalesPackageSubscriptions_.IsRegion = DBNull.Value == idr["IsRegion"] ? null : Convert.ToString(idr["IsRegion"]);
            enSalesPackageSubscriptions_.CountryRegion = DBNull.Value == idr["CountryRegion"] ? null : Convert.ToString(idr["CountryRegion"]);
            enSalesPackageSubscriptions_.RangeFID = DBNull.Value == idr["MarketAnalysisRange"] ? 0 : Convert.ToInt32(idr["MarketAnalysisRange"]);
        }

        private void ConstructObjectAllSubsCriptionYearly(IDataReader idr, enChemicalPricingSession enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.Year = DBNull.Value == idr["YEAR"] ? null : Convert.ToString(idr["YEAR"]);
        }
        private void ConstructObjectAllSubsCriptionRange(IDataReader idr, enChemicalPricingSession enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.RangeFID = DBNull.Value == idr["MarketAnalysisRange"] ? 0 : Convert.ToInt32(idr["MarketAnalysisRange"]);
            enSalesPackageSubscriptions_.RangeName = DBNull.Value == idr["RangeName"] ? null : Convert.ToString(idr["RangeName"]);
            enSalesPackageSubscriptions_.MarketRangeName = DBNull.Value == idr["MarketRangeName"] ? null : Convert.ToString(idr["MarketRangeName"]);

        }


        private void ConstructObjectAllSubsCriptionProductsubs(IDataReader idr, enChemicalPricingSession enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.ProductFID = DBNull.Value == idr["ProductID"] ? 0 : Convert.ToInt32(idr["ProductID"]);
            enSalesPackageSubscriptions_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);

        }

        private void ConstructObjectAllSubsCriptionCountrysubs(IDataReader idr, enChemicalPricingSession enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.CountryRegionId = DBNull.Value == idr["CountryRegionID"] ? 0 : Convert.ToInt32(idr["CountryRegionID"]);
            enSalesPackageSubscriptions_.IsRegion = DBNull.Value == idr["IsRegion"] ? null : Convert.ToString(idr["IsRegion"]);
            enSalesPackageSubscriptions_.CountryRegion = DBNull.Value == idr["CountryRegion"] ? null : Convert.ToString(idr["CountryRegion"]);

        }

        private void ConstructObjectSalesPackageSubscriptionPricing(IDataReader idr, enChemicalPricingSession enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enSalesPackageSubscriptions_.ProductFID = DBNull.Value == idr["ProductID"] ? 0 : Convert.ToInt32(idr["ProductID"]);
            enSalesPackageSubscriptions_.RangeFID = DBNull.Value == idr["ChemicalPricingRange"] ? 0 : Convert.ToInt32(idr["ChemicalPricingRange"]);
            enSalesPackageSubscriptions_.CountryFID = DBNull.Value == idr["CountriesID"] ? 0 : Convert.ToInt32(idr["CountriesID"]);
           
        }
        private void ConstructObjectSalesPackageSubscriptionMarket(IDataReader idr, enChemicalPricingSession enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.IsRegion = DBNull.Value == idr["IsRegion"] ? null : Convert.ToString(idr["IsRegion"]);
            enSalesPackageSubscriptions_.CountryRegion = DBNull.Value == idr["CountryRegion"] ? null : Convert.ToString(idr["CountryRegion"]);
            enSalesPackageSubscriptions_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enSalesPackageSubscriptions_.MarketRangeName = DBNull.Value == idr["MarketRangeName"] ? null : Convert.ToString(idr["MarketRangeName"]);
            enSalesPackageSubscriptions_.RangeName = DBNull.Value == idr["RangeName"] ? null : Convert.ToString(idr["RangeName"]);
            enSalesPackageSubscriptions_.CountryRegionId = DBNull.Value == idr["CountryRegionId"] ? 0 : Convert.ToInt32(idr["CountryRegionId"]);
            enSalesPackageSubscriptions_.ProductFID = DBNull.Value == idr["ProductID"] ? 0 : Convert.ToInt32(idr["ProductID"]);
            enSalesPackageSubscriptions_.RangeFID = DBNull.Value == idr["MarketAnalysisRange"] ? 0 : Convert.ToInt32(idr["MarketAnalysisRange"]);
            //enSalesPackageSubscriptions_.CountryFID = DBNull.Value == idr["CountriesID"] ? 0 : Convert.ToInt32(idr["CountriesID"]);
            //enSalesPackageSubscriptions_.RegionFID = DBNull.Value == idr["RegionsID"] ? 0 : Convert.ToInt32(idr["RegionsID"]);
        }




        private void ConstructGetCreditScore(IDataReader idr, enSalesPackageSubscriptions objENSalesPackageSubscriptions)
        {

            objENSalesPackageSubscriptions.Credit = DBNull.Value == idr["Credit"] ? null : Convert.ToString(idr["Credit"]);
        }


        private void ConstructGetUserType(IDataReader idr, enSalesPackageSubscriptions objENSalesPackageSubscriptions)
        {

            objENSalesPackageSubscriptions.UserType = DBNull.Value == idr["message"] ? null : Convert.ToString(idr["message"]);
        }


        private void ConstructGetAllCustomerEmail(IDataReader idr, enSalesPackageSubscriptions objENSalesPackageSubscriptions)
        {
            objENSalesPackageSubscriptions.ID = Convert.ToInt32(idr["ID"]);
            objENSalesPackageSubscriptions.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
        }



        private void ConstructMySubscription(IDataReader idr, enSalesPackageSubscriptions objENSalesPackageSubscriptions)
        {
            objENSalesPackageSubscriptions.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENSalesPackageSubscriptions.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENSalesPackageSubscriptions.Phone = DBNull.Value == idr["Phone"] ? null : Convert.ToString(idr["Phone"]);
            objENSalesPackageSubscriptions.NumberOfSubscription = DBNull.Value == idr["NumberOfSubscription"] ? (int?)null : Convert.ToInt32(idr["NumberOfSubscription"]);
            objENSalesPackageSubscriptions.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objENSalesPackageSubscriptions.UserID = DBNull.Value == idr["UserID"] ? (int?)null : Convert.ToInt32(idr["UserID"]);
        }
        private void ConstructObject(IDataReader idr, enSalesPackageSubscriptions enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.ID = Convert.ToInt32(idr["ID"]);   
            enSalesPackageSubscriptions_.ProductID = DBNull.Value == idr["ProductID"] ? null : Convert.ToString(idr["ProductID"]);
            enSalesPackageSubscriptions_.MenuID = DBNull.Value == idr["MenuId"] ? null : Convert.ToString(idr["MenuId"]);
            enSalesPackageSubscriptions_.LeadID = DBNull.Value == idr["LeadId"] ? (int?)null : Convert.ToInt32(idr["LeadId"]);
            enSalesPackageSubscriptions_.UserID = DBNull.Value == idr["UserId"] ? (int?)null : Convert.ToInt32(idr["UserId"]);
            enSalesPackageSubscriptions_.SubscriptionType = DBNull.Value == idr["SubscriptionType"] ? (int?)null : Convert.ToInt32(idr["SubscriptionType"]);
            enSalesPackageSubscriptions_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enSalesPackageSubscriptions_.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            enSalesPackageSubscriptions_.Description = DBNull.Value == idr["Description"] ? null : Convert.ToString(idr["Description"]);
            enSalesPackageSubscriptions_.FromDate = DBNull.Value == idr["FromDate"] ? (DateTime?)null : Convert.ToDateTime(idr["FromDate"]);
            enSalesPackageSubscriptions_.ToDate = DBNull.Value == idr["ToDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ToDate"]);
            enSalesPackageSubscriptions_.RegionsID = DBNull.Value == idr["RegionsID"] ? null : Convert.ToString(idr["RegionsID"]);
            enSalesPackageSubscriptions_.CountriesID = DBNull.Value == idr["CountriesID"] ? null : Convert.ToString(idr["CountriesID"]);
            enSalesPackageSubscriptions_.ChemicalPricingRange = DBNull.Value == idr["ChemicalPricingRange"] ? null : Convert.ToString(idr["ChemicalPricingRange"]);
            enSalesPackageSubscriptions_.MarketAnalysisRange = DBNull.Value == idr["MarketAnalysisRange"] ? null : Convert.ToString(idr["MarketAnalysisRange"]);
            enSalesPackageSubscriptions_.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enSalesPackageSubscriptions_.FName = DBNull.Value == idr["FName"] ? null : Convert.ToString(idr["FName"]);
            enSalesPackageSubscriptions_.LName = DBNull.Value == idr["LName"] ? null : Convert.ToString(idr["LName"]);
        }
        private void ConstructObjectAllSubsCription(IDataReader idr, enSalesPackageSubscriptions enSalesPackageSubscriptions_)
        {
       
            enSalesPackageSubscriptions_.UserID = DBNull.Value == idr["UserId"] ? (int?)null : Convert.ToInt32(idr["UserId"]);
          
        }
        private void ConstructObjectAllSubsCriptionDetails(IDataReader idr, enSalesPackageSubscriptions enSalesPackageSubscriptions_)
        {

            enSalesPackageSubscriptions_.UserID = DBNull.Value == idr["UserId"] ? (int?)null : Convert.ToInt32(idr["UserId"]);
            enSalesPackageSubscriptions_.ProductID = DBNull.Value == idr["ProductID"] ? null : Convert.ToString(idr["ProductID"]);
            enSalesPackageSubscriptions_.ToDate = DBNull.Value == idr["ToDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ToDate"]);
        }
        private void ConstructObjectForSubscriptions(IDataReader idr, enSalesPackageSubscriptions enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enSalesPackageSubscriptions_.Chemical_Pricing_Countries = DBNull.Value == idr["Chemical_Pricing_Countries"] ? null : Convert.ToString(idr["Chemical_Pricing_Countries"]);
            enSalesPackageSubscriptions_.MarketAnalysis_RegionName = DBNull.Value == idr["MarketAnalysis_RegionName"] ? null : Convert.ToString(idr["MarketAnalysis_RegionName"]);
            enSalesPackageSubscriptions_.MarketAnalysis_Countries = DBNull.Value == idr["MarketAnalysis_Countries"] ? null : Convert.ToString(idr["MarketAnalysis_Countries"]);
            enSalesPackageSubscriptions_.Capacity_Tracker = DBNull.Value == idr["Capacity_Tracker"] ? null : Convert.ToString(idr["Capacity_Tracker"]);
            enSalesPackageSubscriptions_.Deals_Countries = DBNull.Value == idr["Deals_Countries"] ? null : Convert.ToString(idr["Deals_Countries"]);
            enSalesPackageSubscriptions_.News_Countries = DBNull.Value == idr["News_Countries"] ? null : Convert.ToString(idr["News_Countries"]);
            enSalesPackageSubscriptions_.FromDate_Date = DBNull.Value == idr["FromDate_Date"] ? null : Convert.ToString(idr["FromDate_Date"]);
            enSalesPackageSubscriptions_.ToDate_Date = DBNull.Value == idr["ToDate_Date"] ? null : Convert.ToString(idr["ToDate_Date"]);

        }
        private void ConstructObjectAllSubsCriptionProduct(IDataReader idr, enSalesPackageSubscriptions enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.ID = Convert.ToInt32(idr["ID"]);
            enSalesPackageSubscriptions_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enSalesPackageSubscriptions_.IsSelected= DBNull.Value == idr["IsSelected"] ? true : Convert.ToBoolean(idr["IsSelected"]);
        }
       
        
        private void ConstructGetCustomerCreditscore(IDataReader idr, enSalesPackageSubscriptions objENSalesPackageSubscriptions)
        {

            objENSalesPackageSubscriptions.Credit = DBNull.Value == idr["Credit"] ? null : Convert.ToString(idr["Credit"]);
            objENSalesPackageSubscriptions.UserType = DBNull.Value == idr["UserType"] ? null : Convert.ToString(idr["UserType"]);
            objENSalesPackageSubscriptions.UserStatus = DBNull.Value == idr["UserStatus"] ? null : Convert.ToString(idr["UserStatus"]);
        }
        private void ConstructObjectCheckCustomerPackage(IDataReader idr, enSalesPackageSubscriptions enSalesPackageSubscriptions_)
        {
            enSalesPackageSubscriptions_.ID = Convert.ToInt32(idr["id"]);
            enSalesPackageSubscriptions_.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            enSalesPackageSubscriptions_.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            enSalesPackageSubscriptions_.Organisation = DBNull.Value == idr["Organisation"] ? null : Convert.ToString(idr["Organisation"]);
            enSalesPackageSubscriptions_.Phone = DBNull.Value == idr["Phone"] ? null : Convert.ToString(idr["Phone"]);
            enSalesPackageSubscriptions_.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            enSalesPackageSubscriptions_.Subscribe = DBNull.Value == idr["Subscribe"] ? null : Convert.ToString(idr["Subscribe"]);
            enSalesPackageSubscriptions_.ExpiryDate = DBNull.Value == idr["ExpiryDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ExpiryDate"]);
            enSalesPackageSubscriptions_.Package = DBNull.Value == idr["Package"] ? null : Convert.ToString(idr["Package"]);
            enSalesPackageSubscriptions_.LeadID = DBNull.Value == idr["LeadId"] ? (int?)null : Convert.ToInt32(idr["LeadId"]);

        }

        private void ConstructGetCustomerPrimaryEmail(IDataReader idr, enSalesPackageSubscriptions objENSalesPackageSubscriptions)
        {
            objENSalesPackageSubscriptions.ID = Convert.ToInt32(idr["ID"]);
            objENSalesPackageSubscriptions.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            objENSalesPackageSubscriptions.CompanyPrimaryId = DBNull.Value == idr["CompanyPrimaryId"] ? 0 : Convert.ToInt32(idr["CompanyPrimaryId"]);
        }

        private void ConstructDeleteCompanySecondaryEmail(IDataReader idr, enSalesPackageSubscriptions objENSalesPackageSubscriptions)
        {

            objENSalesPackageSubscriptions.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
        }


    }
}
