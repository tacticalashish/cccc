using EntityLayer;
using EntityLayer.CustomEntity;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataLayer
{
    public class dlAdminChemicalPricing
    {
        public string Message = "";
        private enAdminChemicalPrice _enChemicalPricing = null;

        public dlAdminChemicalPricing(enAdminChemicalPrice enChemicalPricing_)
        {
            this._enChemicalPricing = enChemicalPricing_;
        }
        public List<enAdminChemicalPrice> CheckCommentaryAvilability(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> CheckCommentaryAvilabilityDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_CheckAvilabilityofCommentaryAnalysis]", new object[] {
                                             searchFilter.Product,
                                             searchFilter.Country,
                                             searchFilter.Day,
                                             searchFilter.Date,
                                             searchFilter.Month,
                                             searchFilter.Year,
                                             searchFilter.RangeName
                                            
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectForInsertEmployeeMaster(idr, objENChemicalPrice);
                        CheckCommentaryAvilabilityDataList.Add(objENChemicalPrice);
                    }
                }

                return CheckCommentaryAvilabilityDataList;
            }
            catch (Exception ex)
            {

            }
            return CheckCommentaryAvilabilityDataList;
        }
        public List<enAdminChemicalPrice> DeleteAdminChemicalPricingData(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> IdOfPricingData = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetchemicalPricingData_ForDelete]", new object[] {
                                                            searchFilter.ID,
                                                            searchFilter.RegionWise,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        //ConstructObjectListForUpdate(idr, objENChemicalPrice);
                        //IdOfPricingData.Add(objENChemicalPrice);
                    }
                }

                return IdOfPricingData;
            }
            catch (Exception ex)
            {

            }
            return IdOfPricingData;
        }

        public List<enAdminChemicalPrice> DownloadChemicalPricingData(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listOfPricingDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_DownloadchemicalPricingData]", new object[] {
                                             searchFilter.Product,
                                             searchFilter.Country,
                                             searchFilter.RangeName,


                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectForDownload(idr, objENChemicalPrice, searchFilter.RangeName);
                        listOfPricingDataList.Add(objENChemicalPrice);
                    }
                }

                return listOfPricingDataList;
            }
            catch (Exception ex)
            {

            }
            return listOfPricingDataList;
        }
        public List<enAdminChemicalPrice> CheckChemicalPricing(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listOfPricingDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetchemicalPricingData_ForcheckExists]", new object[] {
                                             searchFilter.Product,
                                             searchFilter.ProductVariant,
                                             searchFilter.Year,
                                             searchFilter.Month,
                                             searchFilter.Country,
                                             searchFilter.RegionWise,
                                             searchFilter.Day,
                                             searchFilter.Week


                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObject(idr, objENChemicalPrice);
                        listOfPricingDataList.Add(objENChemicalPrice);
                    }
                }

                return listOfPricingDataList;
            }
            catch (Exception ex)
            {

            }
            return listOfPricingDataList;
        }
        public List<enAdminChemicalPrice> CalculatePercentageChemicalPricingData(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listOfCountPercentage_PricingDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetchemicalPricingData_ForCountPercentage]", new object[] {
                                                             searchFilter.Product,
                                                             searchFilter.ProductVariant,
                                                             searchFilter.Year,
                                                             searchFilter.Country,
                                                             searchFilter.Type,
                                                             searchFilter.Month,
                                                             searchFilter.Day,
                                                             searchFilter.Date,
                                                             searchFilter.Count

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectListForCountPercentage(idr, objENChemicalPrice);
                        listOfCountPercentage_PricingDataList.Add(objENChemicalPrice);
                    }
                }

                return listOfCountPercentage_PricingDataList;
            }
            catch (Exception ex)
            {

            }
            return listOfCountPercentage_PricingDataList;
        }
        public List<enAdminChemicalPrice> InsertCalculatePercentageChemicalPricingData(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listOfCountPercentage_PricingDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_InsertGetchemicalPricingData_ForCountPercentage]", new object[] {
                                                             searchFilter.Product,
                                                             searchFilter.ProductVariant,
                                                             searchFilter.Year,
                                                             searchFilter.Country,
                                                             searchFilter.RangeName,
                                                             searchFilter.Month,
                                                             searchFilter.Day,
                                                             searchFilter.Date,
                                                             searchFilter.Count

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectListForCountPercentage(idr, objENChemicalPrice);
                        listOfCountPercentage_PricingDataList.Add(objENChemicalPrice);
                    }
                }

                return listOfCountPercentage_PricingDataList;
            }
            catch (Exception ex)
            {

            }
            return listOfCountPercentage_PricingDataList;
        }
        public List<enAdminChemicalPrice> ReadAllChemicalPricingDataList_Update(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> listOfPricingDataList_Update = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetchemicalPricingData_ForUpdate]", new object[] {
                                                            searchFilter.Id,
                                                            searchFilter.Regions,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectListForUpdate(idr, objENChemicalPrice);
                        listOfPricingDataList_Update.Add(objENChemicalPrice);
                    }
                }

                return listOfPricingDataList_Update;
            }
            catch (Exception ex)
            {

            }
            return listOfPricingDataList_Update;
        }
        public List<enAdminChemicalPrice> ReadAllChemicalPricingDataList(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> listOfPricingDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetChemicalPricing_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectForList(idr, objENChemicalPrice);
                        listOfPricingDataList.Add(objENChemicalPrice);
                    }
                }

                return listOfPricingDataList;
            }
            catch (Exception ex)
            {

            }
            return listOfPricingDataList;
        }
        public List<enAdminChemicalPrice> ReadAllProjectManagerDataList(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> listOfSimpleViewProduct = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetProjectManager_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectForSimpleViewProduct(idr, objENChemicalPrice);
                        listOfSimpleViewProduct.Add(objENChemicalPrice);
                    }
                }

                return listOfSimpleViewProduct;
            }
            catch (Exception ex)
            {

            }
            return listOfSimpleViewProduct;
        }
        public List<enAdminChemicalPrice> ReadAllSimpleViewProductDataList(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> listOfSimpleViewProduct = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetSimpleViewProduct_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectForSimpleViewProduct(idr, objENChemicalPrice);
                        listOfSimpleViewProduct.Add(objENChemicalPrice);
                    }
                }

                return listOfSimpleViewProduct;
            }
            catch (Exception ex)
            {

            }
            return listOfSimpleViewProduct;
        }
        public List<enAdminChemicalPrice> ReadChemicalPricing(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listOfPricingDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetchemicalPricingData_ForEdit]", new object[] {
                                                            searchFilter.DDlProduct,
                                                            searchFilter.DDlProductVariant,
                                                            searchFilter.DDlCountry,
                                                            searchFilter.DDlRegionWise
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectForList(idr, objENChemicalPrice);
                        listOfPricingDataList.Add(objENChemicalPrice);
                    }
                }

                return listOfPricingDataList;
            }
            catch (Exception ex)
            {

            }
            return listOfPricingDataList;
        }

        public List<enAdminChemicalPrice> GetLatestRecordofProduct(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> ListofLatestRecord = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetLatestRecordofProduct]", new object[] {
                                                            searchFilter.Product,
                                                            searchFilter.ProductVariant,
                                                            searchFilter.Country,
                                                            searchFilter.RangeName,
                                                            searchFilter.Year
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectForLatestRecordofProduct(idr, objENChemicalPrice);
                        ListofLatestRecord.Add(objENChemicalPrice);
                    }
                }

                return ListofLatestRecord;
            }
            catch (Exception ex)
            {

            }
            return ListofLatestRecord;
        }
        public List<enAdminChemicalPrice> GetAssignProduct(enAdminChemicalPrice obj)
        {
            List<enAdminChemicalPrice> listOfGetAssignProduct = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetTotalAssignProduct_Pricing]", new object[] {
                                                             obj.ChildId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectListForAssignProduct(idr, objENChemicalPrice);
                        listOfGetAssignProduct.Add(objENChemicalPrice);
                    }
                }

                return listOfGetAssignProduct;
            }
            catch (Exception ex)
            {

            }
            return listOfGetAssignProduct;
        }

        //-------------------------------------Insert Data---------------------------------------------------------------------------------------
        #region insert
        public List<enAdminChemicalPrice> InsertChemicalPricingDataWeekly(enAdminChemicalPrice objChem_weekly)
        {
            List<enAdminChemicalPrice> listOfInserteddata = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Insert_chemical_PricesWeeklyRecord]", new object[] {
                                             objChem_weekly.Product,
                                             objChem_weekly.ProductVariant,
                                             objChem_weekly.Type,
                                             objChem_weekly.Year,
                                             objChem_weekly.Week,
                                             objChem_weekly.Date,
                                             objChem_weekly.InsertCount,
                                             objChem_weekly.FileName,
                                             objChem_weekly.CreatedBy,
                                             objChem_weekly.Country,
                                             objChem_weekly.Region,
                                             objChem_weekly.InsertUSD,
                                             objChem_weekly.InsertMin,
                                             objChem_weekly.InsertMax,
                                             objChem_weekly.InsertMinUSD,
                                             objChem_weekly.InsertMaxUSD,
                                             objChem_weekly.RangeName,
                                             objChem_weekly.Source,
                                             objChem_weekly.SourceDesc,
                                             objChem_weekly.MarketSituation,
                                             objChem_weekly.Demand,
                                             objChem_weekly.Supply,
                                             objChem_weekly.PlantShutdown,
                                             objChem_weekly.AdditionalRemarks,
                                              objChem_weekly.InsertEURO,
                                              objChem_weekly.InsertYEN,
                                              objChem_weekly.InsertWON,

                                              objChem_weekly.InsertMinEURO,
                                              objChem_weekly.InsertMaxEURO,

                                              objChem_weekly.InsertMinYEN,
                                              objChem_weekly.InsertMaxYEN,

                                              objChem_weekly.InsertMinWON,
                                              objChem_weekly.InsertMaxWON,
                                             }))
                //{
                //    while (idr.Read())
                //    {
                //        enChemicalPricingValidationWebApi objenChemicalPricingWebApi = new enChemicalPricingValidationWebApi();
                //        ConstructObjectValidation(idr, objenChemicalPricingWebApi);
                //        listOfInserteddata.Add(objenChemicalPricingWebApi);
                //    }
                //}

                return listOfInserteddata;
        }
        public List<enAdminChemicalPrice> InsertOnChemicalPricingDataMonthly(enAdminChemicalPrice objChem_Monthly)
        {
            List<enAdminChemicalPrice> listOfInsertedMonthlydata = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Insert_chemical_PricesMonthlyRecord]", new object[] {
                                             objChem_Monthly.Product,
                                             objChem_Monthly.ProductVariant,
                                             objChem_Monthly.Year,
                                             objChem_Monthly.Month,
                                             objChem_Monthly.InsertCount,
                                             objChem_Monthly.FileName,
                                             objChem_Monthly.CreatedBy,
                                             objChem_Monthly.Country,
                                             objChem_Monthly.Region,
                                             objChem_Monthly.InsertUSD,
                                             objChem_Monthly.InsertMin,
                                             objChem_Monthly.InsertMax,
                                             objChem_Monthly.InsertMinUSD,
                                             objChem_Monthly.InsertMaxUSD,
                                             objChem_Monthly.RangeName,
                                             objChem_Monthly.Source,
                                             objChem_Monthly.SourceDesc,
                                             objChem_Monthly.MarketSituation,
                                             objChem_Monthly.Demand,
                                             objChem_Monthly.Supply,
                                             objChem_Monthly.PlantShutdown,
                                             objChem_Monthly.AdditionalRemarks,
                                              objChem_Monthly.Flag,
                                               objChem_Monthly.InsertEURO,
                                              objChem_Monthly.InsertYEN,
                                              objChem_Monthly.InsertWON,

                                              objChem_Monthly.InsertMinEURO,
                                              objChem_Monthly.InsertMaxEURO,

                                              objChem_Monthly.InsertMinYEN,
                                              objChem_Monthly.InsertMaxYEN,

                                              objChem_Monthly.InsertMinWON,
                                              objChem_Monthly.InsertMaxWON

                                             }))
                //{
                //    while (idr.Read())
                //    {
                //        enChemicalPricingValidationWebApi objenChemicalPricingWebApi = new enChemicalPricingValidationWebApi();
                //        ConstructObjectValidation(idr, objenChemicalPricingWebApi);
                //        listOfInserteddata.Add(objenChemicalPricingWebApi);
                //    }
                //}

                return listOfInsertedMonthlydata;
        }
        public List<enAdminChemicalPrice> InsertOnChemicalPricingDataYearly(enAdminChemicalPrice objChem_Yearly)
        {
            List<enAdminChemicalPrice> listOfInsertedYearlydata = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Insert_chemical_PricesYearlyRecord]", new object[] {
                                             objChem_Yearly.Product,
                                             objChem_Yearly.ProductVariant,
                                             objChem_Yearly.Year,
                                             objChem_Yearly.InsertCount,
                                             objChem_Yearly.FileName,
                                             objChem_Yearly.CreatedBy,
                                             objChem_Yearly.Country,
                                             objChem_Yearly.Region,
                                             objChem_Yearly.InsertUSD,
                                             objChem_Yearly.InsertMin,
                                             objChem_Yearly.InsertMax,
                                             objChem_Yearly.InsertMinUSD,
                                             objChem_Yearly.InsertMaxUSD,
                                             objChem_Yearly.RangeName,
                                             objChem_Yearly.Source,
                                             objChem_Yearly.SourceDesc,
                                             objChem_Yearly.MarketSituation,
                                             objChem_Yearly.Demand,
                                             objChem_Yearly.Supply,
                                             objChem_Yearly.PlantShutdown,
                                             objChem_Yearly.AdditionalRemarks,
                                             objChem_Yearly.Flag,
                                              objChem_Yearly.InsertEURO,
                                              objChem_Yearly.InsertYEN,
                                              objChem_Yearly.InsertWON,

                                              objChem_Yearly.InsertMinEURO,
                                              objChem_Yearly.InsertMaxEURO,

                                              objChem_Yearly.InsertMinYEN,
                                              objChem_Yearly.InsertMaxYEN,

                                              objChem_Yearly.InsertMinWON,
                                              objChem_Yearly.InsertMaxWON
                                             }))

                return listOfInsertedYearlydata;
        }
        public List<enAdminChemicalPrice> InsertOnChemicalPricingDataDailyRetail(enAdminChemicalPrice objChem_DailyRetail)
        {
            objChem_DailyRetail.ModeOfDistrubution = "Spot";
            List<enAdminChemicalPrice> listOfInsertedDailyRetail = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Insert_chemical_PricesDailyRetail]", new object[] {
                                             objChem_DailyRetail.Product,
                                             objChem_DailyRetail.ProductVariant,
                                             objChem_DailyRetail.Year,
                                             objChem_DailyRetail.InsertCount,
                                             objChem_DailyRetail.FileName,
                                             objChem_DailyRetail.CreatedBy,
                                             objChem_DailyRetail.Country,
                                             objChem_DailyRetail.Region,
                                             objChem_DailyRetail.InsertUSD,
                                             objChem_DailyRetail.InsertMin,
                                             objChem_DailyRetail.InsertMax,
                                             objChem_DailyRetail.InsertMinUSD,
                                             objChem_DailyRetail.InsertMaxUSD,
                                             objChem_DailyRetail.RangeName,
                                             objChem_DailyRetail.ModeOfDistrubution,
                                             objChem_DailyRetail.Location,
                                             objChem_DailyRetail.Month,
                                             objChem_DailyRetail.Day,
                                             objChem_DailyRetail.Source,
                                             objChem_DailyRetail.SourceDesc,
                                             objChem_DailyRetail.MarketSituation,
                                             objChem_DailyRetail.Demand,
                                             objChem_DailyRetail.Supply,
                                             objChem_DailyRetail.PlantShutdown,
                                             objChem_DailyRetail.AdditionalRemarks,
                                             objChem_DailyRetail.InsertEURO,
                                              objChem_DailyRetail.InsertYEN,
                                              objChem_DailyRetail.InsertWON,

                                              objChem_DailyRetail.InsertMinEURO,
                                              objChem_DailyRetail.InsertMaxEURO,

                                              objChem_DailyRetail.InsertMinYEN,
                                              objChem_DailyRetail.InsertMaxYEN,

                                              objChem_DailyRetail.InsertMinWON,
                                              objChem_DailyRetail.InsertMaxWON
                                             }))

                return listOfInsertedDailyRetail;
        }
        public List<enAdminChemicalPrice> InsertOnChemicalPricingDataDailyBulk(enAdminChemicalPrice objChem_DailyBulk)
        {
            objChem_DailyBulk.ModeOfDistrubution = "Bulk";
            List<enAdminChemicalPrice> listOfInsertedDailyBulk = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Insert_chemical_PricesDailyBulk]", new object[] {
                                             objChem_DailyBulk.Product,
                                             objChem_DailyBulk.ProductVariant,
                                             objChem_DailyBulk.Year,
                                             objChem_DailyBulk.InsertCount,
                                             objChem_DailyBulk.FileName,
                                             objChem_DailyBulk.CreatedBy,
                                             objChem_DailyBulk.Country,
                                             objChem_DailyBulk.Region,
                                             objChem_DailyBulk.InsertUSD,
                                             objChem_DailyBulk.InsertMin,
                                             objChem_DailyBulk.InsertMax,
                                             objChem_DailyBulk.InsertMinUSD,
                                             objChem_DailyBulk.InsertMaxUSD,
                                             objChem_DailyBulk.RangeName,
                                             objChem_DailyBulk.ModeOfDistrubution,
                                             objChem_DailyBulk.Location,
                                             objChem_DailyBulk.Month,
                                             objChem_DailyBulk.Day,
                                             objChem_DailyBulk.Source,
                                             objChem_DailyBulk.SourceDesc,
                                             objChem_DailyBulk.MarketSituation,
                                             objChem_DailyBulk.Demand,
                                             objChem_DailyBulk.Supply,
                                             objChem_DailyBulk.PlantShutdown,
                                             objChem_DailyBulk.AdditionalRemarks,
                                             objChem_DailyBulk.InsertEURO,
                                              objChem_DailyBulk.InsertYEN,
                                              objChem_DailyBulk.InsertWON,

                                              objChem_DailyBulk.InsertMinEURO,
                                              objChem_DailyBulk.InsertMaxEURO,

                                              objChem_DailyBulk.InsertMinYEN,
                                              objChem_DailyBulk.InsertMaxYEN,

                                              objChem_DailyBulk.InsertMinWON,
                                              objChem_DailyBulk.InsertMaxWON

                                             }))

                return listOfInsertedDailyBulk;
        }
        #endregion
        //-----------------------------Update------------------------------------------------------------------------------------------------
        #region update
        public List<enAdminChemicalPrice> UpdateOnChemicalPricingDataWeekly(enAdminChemicalPrice objChem_weekly)
        {
            List<enAdminChemicalPrice> listOfUpdatedata = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Update_chemical_PricesWeeklyRecord]", new object[] {
                                             objChem_weekly.Product,
                                             objChem_weekly.ProductVariant,
                                             objChem_weekly.Type,
                                             objChem_weekly.Year,
                                             objChem_weekly.Week,
                                             objChem_weekly.Date,
                                             objChem_weekly.InsertCount,
                                             objChem_weekly.FileName,
                                             objChem_weekly.CreatedBy,
                                             objChem_weekly.Country,
                                             objChem_weekly.Region,
                                             objChem_weekly.InsertUSD,
                                             objChem_weekly.InsertMin,
                                             objChem_weekly.InsertMax,
                                             objChem_weekly.InsertMinUSD,
                                             objChem_weekly.InsertMaxUSD,
                                             objChem_weekly.RangeName,
                                             objChem_weekly.Source,
                                             objChem_weekly.SourceDesc,
                                              //objChem_weekly.InsertEURO,
                                              //objChem_weekly.InsertYEN,
                                              //objChem_weekly.InsertWON,

                                              //objChem_weekly.InsertMinEURO,
                                              //objChem_weekly.InsertMaxEURO,

                                              //objChem_weekly.InsertMinYEN,
                                              //objChem_weekly.InsertMaxYEN,

                                              //objChem_weekly.InsertMinWON,
                                              //objChem_weekly.InsertMaxWON,
                                             }))
                //{
                //    while (idr.Read())
                //    {
                //        enChemicalPricingValidationWebApi objenChemicalPricingWebApi = new enChemicalPricingValidationWebApi();
                //        ConstructObjectValidation(idr, objenChemicalPricingWebApi);
                //        listOfInserteddata.Add(objenChemicalPricingWebApi);
                //    }
                //}

                return listOfUpdatedata;
        }
        public List<enAdminChemicalPrice> UpdateOnChemicalPricingDataMonthly(enAdminChemicalPrice objChem_Monthly)
        {
            List<enAdminChemicalPrice> listOfUpdatedMonthlydata = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Update_chemical_PricesMonthlyRecord]", new object[] {
                                             objChem_Monthly.Product,
                                             objChem_Monthly.ProductVariant,
                                             objChem_Monthly.Year,
                                             objChem_Monthly.Month,
                                             objChem_Monthly.InsertCount,
                                             objChem_Monthly.FileName,
                                             objChem_Monthly.CreatedBy,
                                             objChem_Monthly.Country,
                                             objChem_Monthly.Region,
                                             objChem_Monthly.InsertUSD,
                                             objChem_Monthly.InsertMin,
                                             objChem_Monthly.InsertMax,
                                             objChem_Monthly.InsertMinUSD,
                                             objChem_Monthly.InsertMaxUSD,
                                             objChem_Monthly.RangeName,
                                              objChem_Monthly.Source,
                                              objChem_Monthly.SourceDesc,
                                               objChem_Monthly.Flag,

                                              //objChem_Monthly.InsertEURO,
                                              //objChem_Monthly.InsertYEN,
                                              //objChem_Monthly.InsertWON,

                                              //objChem_Monthly.InsertMinEURO,
                                              //objChem_Monthly.InsertMaxEURO,

                                              //objChem_Monthly.InsertMinYEN,
                                              //objChem_Monthly.InsertMaxYEN,

                                              //objChem_Monthly.InsertMinWON,
                                              //objChem_Monthly.InsertMaxWON
                                             }))
                //{
                //    while (idr.Read())
                //    {
                //        enChemicalPricingValidationWebApi objenChemicalPricingWebApi = new enChemicalPricingValidationWebApi();
                //        ConstructObjectValidation(idr, objenChemicalPricingWebApi);
                //        listOfInserteddata.Add(objenChemicalPricingWebApi);
                //    }
                //}

                return listOfUpdatedMonthlydata;
        }
        public List<enAdminChemicalPrice> UpdateOnChemicalPricingDataYearly(enAdminChemicalPrice objChem_Yearly)
        {
            List<enAdminChemicalPrice> listOfUpdatedYearlydata = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Update_chemical_PricesYearlyRecord]", new object[] {
                                             objChem_Yearly.Product,
                                             objChem_Yearly.ProductVariant,
                                             objChem_Yearly.Year,
                                             objChem_Yearly.InsertCount,
                                             objChem_Yearly.FileName,
                                             objChem_Yearly.CreatedBy,
                                             objChem_Yearly.Country,
                                             objChem_Yearly.Region,
                                             objChem_Yearly.InsertUSD,
                                             objChem_Yearly.InsertMin,
                                             objChem_Yearly.InsertMax,
                                             objChem_Yearly.InsertMinUSD,
                                             objChem_Yearly.InsertMaxUSD,
                                             objChem_Yearly.RangeName,
                                              objChem_Yearly.Source,
                                             objChem_Yearly.SourceDesc,
                                             objChem_Yearly.Flag,
                                              //objChem_Yearly.InsertEURO,
                                              //objChem_Yearly.InsertYEN,
                                              //objChem_Yearly.InsertWON,

                                              //objChem_Yearly.InsertMinEURO,
                                              //objChem_Yearly.InsertMaxEURO,

                                              //objChem_Yearly.InsertMinYEN,
                                              //objChem_Yearly.InsertMaxYEN,

                                              //objChem_Yearly.InsertMinWON,
                                              //objChem_Yearly.InsertMaxWON
                                             }))

                return listOfUpdatedYearlydata;
        }
        public List<enAdminChemicalPrice> UpdateOnChemicalPricingDataDailyRetail(enAdminChemicalPrice objChem_DailyRetail)
        {
            List<enAdminChemicalPrice> listOfUpdatedDailyRetail = new List<enAdminChemicalPrice>();
            objChem_DailyRetail.ModeOfDistrubution = "Spot";
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Update_chemical_PricesDailyRetail]", new object[] {
                                             objChem_DailyRetail.Product,
                                             objChem_DailyRetail.ProductVariant,
                                             objChem_DailyRetail.Year,
                                             objChem_DailyRetail.InsertCount,
                                             objChem_DailyRetail.FileName,
                                             objChem_DailyRetail.CreatedBy,
                                             objChem_DailyRetail.Country,
                                             objChem_DailyRetail.Region,
                                             objChem_DailyRetail.InsertUSD,
                                             objChem_DailyRetail.InsertMin,
                                             objChem_DailyRetail.InsertMax,
                                             objChem_DailyRetail.InsertMinUSD,
                                             objChem_DailyRetail.InsertMaxUSD,
                                             objChem_DailyRetail.RangeName,
                                             objChem_DailyRetail.Source,
                                             objChem_DailyRetail.ModeOfDistrubution,
                                             objChem_DailyRetail.Location,
                                             objChem_DailyRetail.Month,
                                             objChem_DailyRetail.Day,
                                             objChem_DailyRetail.SourceDesc,
                                              // objChem_DailyRetail.InsertEURO,
                                              //objChem_DailyRetail.InsertYEN,
                                              //objChem_DailyRetail.InsertWON,

                                              //objChem_DailyRetail.InsertMinEURO,
                                              //objChem_DailyRetail.InsertMaxEURO,

                                              //objChem_DailyRetail.InsertMinYEN,
                                              //objChem_DailyRetail.InsertMaxYEN,

                                              //objChem_DailyRetail.InsertMinWON,
                                              //objChem_DailyRetail.InsertMaxWON

                                             }))

                return listOfUpdatedDailyRetail;
        }
        public List<enAdminChemicalPrice> UpdateOnChemicalPricingDataDailyBulk(enAdminChemicalPrice objChem_DailyBulk)
        {
            List<enAdminChemicalPrice> listOfUpdatedDailyBulk = new List<enAdminChemicalPrice>();
            objChem_DailyBulk.ModeOfDistrubution = "Bulk";
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Update_chemical_PricesDailyBulk]", new object[] {
                                             objChem_DailyBulk.Product,
                                             objChem_DailyBulk.ProductVariant,
                                             objChem_DailyBulk.Year,
                                             objChem_DailyBulk.InsertCount,
                                             objChem_DailyBulk.FileName,
                                             objChem_DailyBulk.CreatedBy,
                                             objChem_DailyBulk.Country,
                                             objChem_DailyBulk.Region,
                                             objChem_DailyBulk.InsertUSD,
                                             objChem_DailyBulk.InsertMin,
                                             objChem_DailyBulk.InsertMax,
                                             objChem_DailyBulk.InsertMinUSD,
                                             objChem_DailyBulk.InsertMaxUSD,
                                             objChem_DailyBulk.RangeName,
                                             objChem_DailyBulk.Source,
                                             objChem_DailyBulk.ModeOfDistrubution,
                                             objChem_DailyBulk.Location,
                                             objChem_DailyBulk.Month,
                                             objChem_DailyBulk.Day,
                                              objChem_DailyBulk.SourceDesc,
                                              // objChem_DailyBulk.InsertEURO,
                                              //objChem_DailyBulk.InsertYEN,
                                              //objChem_DailyBulk.InsertWON,

                                              //objChem_DailyBulk.InsertMinEURO,
                                              //objChem_DailyBulk.InsertMaxEURO,

                                              //objChem_DailyBulk.InsertMinYEN,
                                              //objChem_DailyBulk.InsertMaxYEN,

                                              //objChem_DailyBulk.InsertMinWON,
                                              //objChem_DailyBulk.InsertMaxWON
                                             }))

                return listOfUpdatedDailyBulk;
        }
        public List<enAdminChemicalPrice> Update_ReadBasedOnSearchFilterChemicalPrice(enAdminChemicalPrice enAdminChemicalPriceFilter)
        {

            List<enAdminChemicalPrice> listOfAdminChemicalPrice = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_chemical_PricesFindAvg_ForUpdate]", new object[] {
                                                               enAdminChemicalPriceFilter.Product,
                                                               enAdminChemicalPriceFilter.Country,
                                                                enAdminChemicalPriceFilter.ProductVariant,
                                                                enAdminChemicalPriceFilter.Year,
                                                                 enAdminChemicalPriceFilter.Month,
                                                                 enAdminChemicalPriceFilter.RangeName
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenChemicalPricingWebApi = new enAdminChemicalPrice();
                    ConstructObject(idr, objenChemicalPricingWebApi);
                    listOfAdminChemicalPrice.Add(objenChemicalPricingWebApi);
                }
            }

            return listOfAdminChemicalPrice;
        }//--Count Average For weekly to Monthly
        public List<enAdminChemicalPrice> Update_ReadBasedOnSearchFilterChemicalPriceMonthlytoYearly(enAdminChemicalPrice enAdminChemicalPriceFilter)
        {

            List<enAdminChemicalPrice> listOfAdminChemicalPrice = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_chemical_PricesFindAvg_ForUpdate]", new object[] {
                                                               enAdminChemicalPriceFilter.Product,
                                                               enAdminChemicalPriceFilter.Country,
                                                                enAdminChemicalPriceFilter.ProductVariant,
                                                                enAdminChemicalPriceFilter.Year,
                                                                 enAdminChemicalPriceFilter.Month,
                                                                 enAdminChemicalPriceFilter.RangeName
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenChemicalPricingWebApi = new enAdminChemicalPrice();
                    ConstructObject(idr, objenChemicalPricingWebApi);
                    listOfAdminChemicalPrice.Add(objenChemicalPricingWebApi);
                }
            }

            return listOfAdminChemicalPrice;
        }//--Count Average For weekly to Monthly
         //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        #endregion
        public List<enAdminChemicalPrice> ReadBasedOnSearchFilterChemicalPrice(enAdminChemicalPrice enAdminChemicalPriceFilter)
        {

            List<enAdminChemicalPrice> listOfAdminChemicalPrice = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_chemical_PricesFindAvg]", new object[] {
                                                               enAdminChemicalPriceFilter.Product,
                                                               enAdminChemicalPriceFilter.Country,
                                                                enAdminChemicalPriceFilter.ProductVariant,
                                                                enAdminChemicalPriceFilter.Year,
                                                                 enAdminChemicalPriceFilter.Month,
                                                                 enAdminChemicalPriceFilter.RangeName
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenChemicalPricingWebApi = new enAdminChemicalPrice();
                    ConstructObject(idr, objenChemicalPricingWebApi);
                    listOfAdminChemicalPrice.Add(objenChemicalPricingWebApi);
                }
            }

            return listOfAdminChemicalPrice;
        }//--Count Average For weekly to Monthly
        public List<enAdminChemicalPrice> ReadBasedOnSearchFilterChemicalPriceMonthlytoYearly(enAdminChemicalPrice enAdminChemicalPriceFilter)
        {
            enAdminChemicalPriceFilter.RangeName = "Monthly";
            List<enAdminChemicalPrice> listOfAdminChemicalPrice = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_chemical_PricesFindAvg]", new object[] {
                                                               enAdminChemicalPriceFilter.Product,
                                                               enAdminChemicalPriceFilter.Country,
                                                                enAdminChemicalPriceFilter.ProductVariant,
                                                                enAdminChemicalPriceFilter.Year,
                                                                 enAdminChemicalPriceFilter.Month,
                                                                 enAdminChemicalPriceFilter.RangeName
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenChemicalPricingWebApi = new enAdminChemicalPrice();
                    ConstructObject(idr, objenChemicalPricingWebApi);
                    listOfAdminChemicalPrice.Add(objenChemicalPricingWebApi);
                }
            }

            return listOfAdminChemicalPrice;
        }//--Count Average For weekly to Monthly
        public List<enAdminChemicalPrice> ReadAllProductVarient()
        {
            List<enAdminChemicalPrice> listOfeProductVarient = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Product_Variant]", new object[] {
                                                            _enChemicalPricing.Product,
                                                            _enChemicalPricing.Country,
                                                            _enChemicalPricing.RangeName
                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectProductVarient(idr, objenProduct);
                    listOfeProductVarient.Add(objenProduct);
                }
            }

            return listOfeProductVarient;
        }
        public List<enAdminChemicalPrice> ReadAllLocation()
        {
            List<enAdminChemicalPrice> listOfeProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetProductLocation]", new object[] {
                                                            _enChemicalPricing.Product,
                                                            _enChemicalPricing.Country,
                                                           _enChemicalPricing.RangeName,
                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectProductLocation(idr, objenProduct);
                    listOfeProduct.Add(objenProduct);
                }
            }

            return listOfeProduct;
        }
        public List<enAdminChemicalPrice> ReadAllUser()
        {
            List<enAdminChemicalPrice> listOfUser = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_EmployeeMaster_ForDropdown]", new object[] {
                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objen = new enAdminChemicalPrice();
                    ConstructObjectUserDetails(idr, objen);
                    listOfUser.Add(objen);
                }
            }
            return listOfUser;
        }
        public List<enAdminChemicalPrice> GetAllEmployeeRecords()
        {
            List<enAdminChemicalPrice> listOfEmployeeRecords = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetEmployeeHierarchy]", new object[] {

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectListForEmployeeRecords(idr, objENChemicalPrice);
                        listOfEmployeeRecords.Add(objENChemicalPrice);
                    }
                }

                return listOfEmployeeRecords;
            }
            catch (Exception ex)
            {

            }
            return listOfEmployeeRecords;
        }
        public List<enAdminChemicalPrice> GetUserProductOrganizationaldata(enSearchFilter Userdata)
        {
            List<enAdminChemicalPrice> listOfEmployeeRecords = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetUser_Productrecord]", new object[] {
                                                               Userdata.UserId,
                                                                Userdata.searchKeyword,
                                                                Userdata.StartRowNumber,
                                                                Userdata.EndRowNumber,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectListForUserProduct(idr, objENChemicalPrice);
                        listOfEmployeeRecords.Add(objENChemicalPrice);
                    }
                }

                return listOfEmployeeRecords;
            }
            catch (Exception ex)
            {

            }
            return listOfEmployeeRecords;
        }     
        public List<enAdminChemicalPrice> GetAssociatePriceList(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> listOfPricingDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetAssociatePricing_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.UserId,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectListForApproval(idr, objENChemicalPrice);
                        listOfPricingDataList.Add(objENChemicalPrice);
                    }
                }

                return listOfPricingDataList;
            }
            catch (Exception ex)
            {

            }
            return listOfPricingDataList;
        }
        public List<enAdminChemicalPrice> GetConsultantPriceList(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> listOfPricingDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetConsultantPricing_CustomSearch]", new object[] {
                                                             searchFilter.searchKeyword,
                                                            searchFilter.UserId,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                            searchFilter.ProductId,
                                                            searchFilter.RangeType


                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectListForApproval(idr, objENChemicalPrice);
                        listOfPricingDataList.Add(objENChemicalPrice);
                    }
                }

                return listOfPricingDataList;
            }
            catch (Exception ex)
            {

            }
            return listOfPricingDataList;
        }


        public List<enAdminChemicalPrice> OneTimeAllApproved()
        {
            List<enAdminChemicalPrice> ApprovalStatus = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_OneTimeAllApproved_Pricing]", new object[] {
                                                            _enChemicalPricing.ProductId,
                                                            _enChemicalPricing.RangeName,
                                                             _enChemicalPricing.ApprovedBy

                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ApprovalStatus.Add(objenProduct);
                }
            }

            return ApprovalStatus;
        }

        public List<enAdminChemicalPrice> GetPendingProductUsingRange(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> ListofResponse = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetPendingProductUsingRange]", new object[] {
                                                            searchFilter.RangeName,
                                                            searchFilter.UserId

                                                           }))

            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectPendingProductUsingRange(idr, objenProduct);
                    ListofResponse.Add(objenProduct);
                }
            }
            return ListofResponse;
        }





        public List<enAdminChemicalPrice> ReadAllProduct()
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetProduct_ForDropdown]", new object[] {

                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objen = new enAdminChemicalPrice();
                    ConstructObjectProduct(idr, objen);
                    listOfProduct.Add(objen);
                }
            }

            return listOfProduct;
        }

        public List<enAdminChemicalPrice> ReadAllProductCommentaryProductEmpRelation()
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_ProductForCommentaryProductEmpRelation]", new object[] {

                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objen = new enAdminChemicalPrice();
                    ConstructObjectProduct(idr, objen);
                    listOfProduct.Add(objen);
                }
            }

            return listOfProduct;
        }



        public List<enAdminChemicalPrice> ReadAllProductWithStatus()
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetALLProductwithStatus]", new object[] {

                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objen = new enAdminChemicalPrice();
                    ConstructObjectProduct(idr, objen);
                    listOfProduct.Add(objen);
                }
            }

            return listOfProduct;
        }
        public List<enAdminChemicalPrice> ReadAllCountry()
        {
            List<enAdminChemicalPrice> listOfCountry = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetCountry_ForDropdown]", new object[] {

                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objen = new enAdminChemicalPrice();
                    ConstructObjectCountry(idr, objen);
                    listOfCountry.Add(objen);
                }
            }
            return listOfCountry;
        }
        public List<enAdminChemicalPrice> InsertProductEmpRelation()
        {
            List<enAdminChemicalPrice> ObjInsert = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_InsertEmpProductRelation]", new object[] {
                                                            _enChemicalPricing.ID,
                                                            _enChemicalPricing.Product,
                                                            _enChemicalPricing.QueryType
                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }
        public List<enAdminChemicalPrice> InsertEmployeeMaster()
        {
            List<enAdminChemicalPrice> ObjInsert = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_InsertEmployeeMaster]", new object[] {
                                                            _enChemicalPricing.ChildId,
                                                            _enChemicalPricing.ParentId,

                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }
        public List<enAdminChemicalPrice> ReadAllLocationForedit()
        {
            List<enAdminChemicalPrice> listOfeProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetProductLocationForEdit]", new object[] {
                                                            _enChemicalPricing.Product,
                                                            _enChemicalPricing.Country,
                                                           _enChemicalPricing.RangeName,
                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectProductLocation(idr, objenProduct);
                    listOfeProduct.Add(objenProduct);
                }
            }

            return listOfeProduct;
        }
        public List<enAdminChemicalPrice> UpdateApprovalStatus()
        {
            List<enAdminChemicalPrice> ApprovalStatus = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_UpdateConsultantPricing_Status]", new object[] {
                                                            _enChemicalPricing.Flag,
                                                            _enChemicalPricing.ID,
                                                            _enChemicalPricing.RangeName,
                                                             _enChemicalPricing.ApprovedBy
                                                         
                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ApprovalStatus.Add(objenProduct);
                }
            }

            return ApprovalStatus;
        }
        public List<enAdminChemicalPrice> DisApprovalMessage()
        {
            List<enAdminChemicalPrice> ApprovalStatus = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_InsertDisapproved_Message]", new object[] {
                                                            _enChemicalPricing.DisapprovedMessage,
                                                            _enChemicalPricing.ID,
                                                            _enChemicalPricing.RangeName,
                                                            _enChemicalPricing.DisapprovedBy,
                                                             _enChemicalPricing.Product,
                                                              _enChemicalPricing.Country,
                                                               _enChemicalPricing.CreatedBy,
                                                               _enChemicalPricing.ProductVariant
                                                           }))


     
            {
            //    while (idr.Read())
            //    {
            //        enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
            //        ConstructObject_DisApprovalMessage(idr, objenProduct);
            //        ApprovalStatus.Add(objenProduct);
            //    }
            }
            return ApprovalStatus;
        }
        //----------------------------------For News-----------------------------------------------------
        public List<enAdminChemicalPrice> ReadLatestNews(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listOfLatestNews = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetNewsData]", new object[] {
                                                            searchFilter.Product
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectForLatestNews(idr, objENChemicalPrice);
                        listOfLatestNews.Add(objENChemicalPrice);
                    }
                }
                return listOfLatestNews;
            }
            catch (Exception ex)
            {

            }
            return listOfLatestNews;
        }

        //-----------------------------------Add Product Variant----------------------------------
        public List<enAdminChemicalPrice> AddNewProductVariant()
        {
            List<enAdminChemicalPrice> AddNewProductVariant = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_InsertProduct_Variant]", new object[] {
                                                            _enChemicalPricing.ProductVariant,
                                                             _enChemicalPricing.Country,
                                                               _enChemicalPricing.Product
                                                         
                                                           }))



            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    AddNewProductVariant.Add(objenProduct);
                }
            }
            return AddNewProductVariant;
        }
        public List<enAdminChemicalPrice> AddNewProductVariantRealtion()
        {
            List<enAdminChemicalPrice> AddNewProductVariantRealtion = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_InsertProduct_VariantRelation]", new object[] {
                                                            _enChemicalPricing.ProductVariant,
                                                             _enChemicalPricing.Country,
                                                               _enChemicalPricing.Product
                                                              
                                                           }))



            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    AddNewProductVariantRealtion.Add(objenProduct);
                }
            }
            return AddNewProductVariantRealtion;
        }
        public List<enAdminChemicalPrice> AddProductVariantDDL()
        {
            List<enAdminChemicalPrice> ProductVariantList = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetProductVariant]", new object[] {
                                                             _enChemicalPricing.Country,
                                                               _enChemicalPricing.Product
                                                           }))



            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForDDL(idr, objenProduct);
                    ProductVariantList.Add(objenProduct);
                }
            }
            return ProductVariantList;
        }
        //---============================New chenges================================================
        public List<enAdminChemicalPrice> ReadInsertedDataInGrid(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listOfPricingDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetBind_DataGrid]", new object[] {
                                                            searchFilter.Product,
                                                            searchFilter.ProductVariant,
                                                            searchFilter.Country,
                                                            searchFilter.RegionWise,
                                                            searchFilter.Year,
                                                            searchFilter.Month,
                                                            searchFilter.Day,
                                                            searchFilter.Date,
                                                            searchFilter.Week
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectForGrid(idr, objENChemicalPrice, searchFilter.RegionWise);
                        listOfPricingDataList.Add(objENChemicalPrice);
                    }
                }

                return listOfPricingDataList;
            }
            catch (Exception ex)
            {

            }
            return listOfPricingDataList;
        }
        public List<enAdminChemicalPrice> GetlistOfAllDisApprovedRecord(enSearchFilter obj)
        {
            List<enAdminChemicalPrice> listOfDisApprovedRecord = new List<enAdminChemicalPrice>();
            try
            {

                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetAssociateDisApprovedRecord_CustomSearch]", new object[] {
                                                            obj.searchKeyword,
                                                            obj.UserId,
                                                            obj.StartRowNumber,
                                                            obj.EndRowNumber
                                                           }))
                {
                    while (idr.Read())
                    {

                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectListForDisApproveProduct(idr, objENChemicalPrice);
                        listOfDisApprovedRecord.Add(objENChemicalPrice);
                    }
                }

                return listOfDisApprovedRecord;
            }
            catch (Exception ex)
            {

            }
            return listOfDisApprovedRecord;
        }
        public List<enAdminChemicalPrice> GetConsultantPriceListForcasting(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> listOfPricingDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetConsultantPricingForcasting_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.UserId,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectListForApproval(idr, objENChemicalPrice);
                        listOfPricingDataList.Add(objENChemicalPrice);
                    }
                }

                return listOfPricingDataList;
            }
            catch (Exception ex)
            {

            }
            return listOfPricingDataList;
        }

        public List<enAdminChemicalPrice> ReadAllProduct(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetAllSubscribeProductByUser]", new object[] {
                                                             searchFilter.UserId,

                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objen = new enAdminChemicalPrice();
                    ConstructObjectProduct(idr, objen);
                    listOfProduct.Add(objen);
                }
            }
            return listOfProduct;
        }
        public List<enAdminChemicalPrice> ReadCountrybehalf_Of_Product(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_ReadCountrybehalf_Of_Product]", new object[] {
                                                             searchFilter.Product,

                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objen = new enAdminChemicalPrice();
                    ConstructObjectCountry(idr, objen);
                    listOfProduct.Add(objen);
                }
            }
            return listOfProduct;
        }
        public List<enAdminChemicalPrice> ReadChildProductFeedstock(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> listRead = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetChildProductFeedstock]", new object[] {
                                                            searchFilter.UserId,
                                                            searchFilter.ProductId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectChildProductFeedstock(idr, objEN);
                        listRead.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listRead;
        }
        public List<enAdminChemicalPrice> InsertProductFeedstock(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> ListofResponse = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertProductFeedstock]", new object[] {
                                                              enAdminChemicalPrice.ParentProductId,
                                                              enAdminChemicalPrice.ProductChildId,

                                                           }))

            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objenProduct);
                    ListofResponse.Add(objenProduct);
                }
            }
            return ListofResponse;
        }

        public List<enAdminChemicalPrice> ReadPortwiseExport(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPortWiseExportDataForTable]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.Product,
                                                            searchFilter.CountryName,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForPortWiseExport(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }
        private void ConstructObjectChildProductFeedstock(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.Products = DBNull.Value == idr["Products"] ? null : Convert.ToString(idr["Products"]);
        }


        private void ConstructObjectForPortWiseExport(IDataReader idr, enAdminChemicalPrice enobj_)
        {
            enobj_.ID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enobj_.Year = DBNull.Value == idr["year"] ? null : Convert.ToString(idr["year"]);
            enobj_.hsCode = DBNull.Value == idr["HSCode"] ? null : Convert.ToString(idr["HSCode"]);
            //enobj_.Date = DBNull.Value == idr["Date"] ? null : Convert.ToString(idr["Date"]);
            
            object dateValue = DBNull.Value == idr["Date"] ? (DateTime?)null : Convert.ToDateTime(idr["Date"]);
            DateTime? result = (dateValue == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dateValue);
            // string formattedDate = result.HasValue ? result.Value.ToString("MM/dd/yyyy") : null;
            //DateTime? dateOnly = result?.Date;
            enobj_.Date = result.HasValue ? result.Value.ToString("yyyy/MM/dd") : null;

            enobj_.Weight = DBNull.Value == idr["Weight"] ? null : Convert.ToString(idr["Weight"]);
            //enobj_.Region = DBNull.Value == idr["Region"] ? null : Convert.ToString(idr["Region"]);
            enobj_.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            //enobj_.Source = DBNull.Value == idr["Source"] ? null : Convert.ToString(idr["Source"]);
            //enobj_.SourceDesc = DBNull.Value == idr["SourceDesc"] ? null : Convert.ToString(idr["SourceDesc"]);
            enobj_.Grade = DBNull.Value == idr["Grade"] ? null : Convert.ToString(idr["Grade"]);
            enobj_.ExporterName = DBNull.Value == idr["ExporterName"] ? null : Convert.ToString(idr["ExporterName"]);
            enobj_.ExportCountry = DBNull.Value == idr["ExportingCountry"] ? null : Convert.ToString(idr["ExportingCountry"]);
            enobj_.PortOfDestination = DBNull.Value == idr["PortOfDestination"] ? null : Convert.ToString(idr["PortOfDestination"]);
            enobj_.ConsigneeName = DBNull.Value == idr["ConsigneeName"] ? null : Convert.ToString(idr["ConsigneeName"]);
            enobj_.PortOfOrigin = DBNull.Value == idr["PortOfOrigin"] ? null : Convert.ToString(idr["PortOfOrigin"]);
            enobj_.CountryofImport = DBNull.Value == idr["CountryofImport"] ? null : Convert.ToString(idr["CountryofImport"]);
            enobj_.QtyInMT = DBNull.Value == idr["QtyInMT"] ? null : Convert.ToString(idr["QtyInMT"]);
            enobj_.UnitRate = DBNull.Value == idr["UnitRate"] ? null : Convert.ToString(idr["UnitRate"]);
            enobj_.Incoterms = DBNull.Value == idr["Incoterms"] ? null : Convert.ToString(idr["Incoterms"]);
            enobj_.ShipmentMode = DBNull.Value == idr["ShipmentMode"] ? null : Convert.ToString(idr["ShipmentMode"]);
            enobj_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enobj_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            enobj_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enobj_.ProductDiscription = DBNull.Value == idr["ProductDescription"] ? null : Convert.ToString(idr["ProductDescription"]);
            enobj_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            // enobj_.Flag = DBNull.Value == idr["flag"] ? 0 : Convert.ToInt32(idr["flag"]);
        }
        private void ConstructObjectListForDisApproveProduct(IDataReader idr, enAdminChemicalPrice objENChemicalPrice)
        {
            objENChemicalPrice.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENChemicalPrice.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENChemicalPrice.ID = Convert.ToInt32(idr["id"]);
            objENChemicalPrice.Product = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            objENChemicalPrice.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
            objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            objENChemicalPrice.Week = DBNull.Value == idr["Week"] ? null : Convert.ToString(idr["Week"]);
            objENChemicalPrice.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            objENChemicalPrice.Day = DBNull.Value == idr["Day"] ? null : Convert.ToString(idr["Day"]);
            objENChemicalPrice.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
            objENChemicalPrice.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
            objENChemicalPrice.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            objENChemicalPrice.Region = DBNull.Value == idr["Region"] ? null : Convert.ToString(idr["Region"]);
            objENChemicalPrice.RegionWise = DBNull.Value == idr["Regionwise"] ? null : Convert.ToString(idr["Regionwise"]);
            objENChemicalPrice.Remarks = DBNull.Value == idr["Remark"] ? null : Convert.ToString(idr["Remark"]);
            objENChemicalPrice.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            objENChemicalPrice.USD = DBNull.Value == idr["USD"] ? (float?)null : Convert.ToSingle(idr["USD"]);
            objENChemicalPrice.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
            objENChemicalPrice.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
            objENChemicalPrice.MinUSD = DBNull.Value == idr["MinUSD"] ? (decimal?)null : Convert.ToDecimal(idr["MinUSD"]);
            objENChemicalPrice.MaxUSD = DBNull.Value == idr["MaxUSD"] ? (decimal?)null : Convert.ToDecimal(idr["MaxUSD"]);
            objENChemicalPrice.Flag = DBNull.Value == idr["Flag"] ? 0 : Convert.ToInt32(idr["Flag"]);
            objENChemicalPrice.Source = DBNull.Value == idr["source"] ? null : Convert.ToString(idr["source"]);
            objENChemicalPrice.SourceDesc = DBNull.Value == idr["SourceDesc"] ? null : Convert.ToString(idr["SourceDesc"]);
            objENChemicalPrice.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            objENChemicalPrice.ApprovedBy = DBNull.Value == idr["ApprovedBy"] ? null : Convert.ToString(idr["ApprovedBy"]);
            objENChemicalPrice.UpdatedDate = DBNull.Value == idr["UpdatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdatedDate"]);
            objENChemicalPrice.RecordsCount = DBNull.Value == idr["DisApproveProduct"] ? 0 : Convert.ToInt32(idr["DisApproveProduct"]);
        }

        private void ConstructObjectForGrid(IDataReader idr, enAdminChemicalPrice objENChemicalPrice, string Range)
        {
            if (Range == "Yearly")
            {
                objENChemicalPrice.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
                objENChemicalPrice.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
                objENChemicalPrice.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
                objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
                objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
                objENChemicalPrice.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
                objENChemicalPrice.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
                objENChemicalPrice.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
            }
            else if (Range == "Monthly")
            {
                objENChemicalPrice.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
                objENChemicalPrice.ID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
                objENChemicalPrice.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
                objENChemicalPrice.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
                objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
                objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
                objENChemicalPrice.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
                objENChemicalPrice.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
                objENChemicalPrice.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
                objENChemicalPrice.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
            }
            else if (Range == "Weekly")
            {
                objENChemicalPrice.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
                objENChemicalPrice.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
                objENChemicalPrice.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
                objENChemicalPrice.Type = DBNull.Value == idr["Type"] ? null : Convert.ToString(idr["Type"]);
                objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
                objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
                objENChemicalPrice.Week = DBNull.Value == idr["Week"] ? null : Convert.ToString(idr["Week"]);
                objENChemicalPrice.Date = DBNull.Value == idr["Date"] ? null : Convert.ToString(idr["Date"]);
                objENChemicalPrice.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
                objENChemicalPrice.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
                objENChemicalPrice.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
            }
            else
            {
                objENChemicalPrice.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
                objENChemicalPrice.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
                objENChemicalPrice.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
                objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
                objENChemicalPrice.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
                objENChemicalPrice.ModeOfDistrubution = DBNull.Value == idr["ModeOfDistrubution"] ? null : Convert.ToString(idr["ModeOfDistrubution"]);
                objENChemicalPrice.Location = DBNull.Value == idr["Location"] ? null : Convert.ToString(idr["Location"]);
                objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
                objENChemicalPrice.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
                objENChemicalPrice.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
                objENChemicalPrice.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
                objENChemicalPrice.Day = DBNull.Value == idr["Day"] ? null : Convert.ToString(idr["Day"]);
            }
        }
        private void ConstructObjectListForAssignProduct(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.RecordsCount = DBNull.Value == idr["TotalProducts"] ? 0 : Convert.ToInt32(idr["TotalProducts"]);

        }

        private void ConstructObjectForDDL(IDataReader idr, enAdminChemicalPrice enChemicalPricing_)
        {
            enChemicalPricing_.ID = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            enChemicalPricing_.ProductVariant = DBNull.Value == idr["ProductVariantName"] ? null : Convert.ToString(idr["ProductVariantName"]);
        }
        //-----------------------------------------------------------------------------------------
        private void ConstructObject(IDataReader idr, enAdminChemicalPrice enChemicalPricing_)
        {
            enChemicalPricing_.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
            enChemicalPricing_.USD = DBNull.Value == idr["USD"] ? (float?)null : Convert.ToSingle(idr["USD"]);
            enChemicalPricing_.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
            enChemicalPricing_.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
            enChemicalPricing_.MinUSD = DBNull.Value == idr["MinUSD"] ? 0 : Convert.ToUInt64(idr["MinUSD"]);
            enChemicalPricing_.MaxUSD = DBNull.Value == idr["MaxUSD"] ? 0 : Convert.ToUInt64(idr["MaxUSD"]);
            enChemicalPricing_.Date = DBNull.Value == idr["Date"] ? null : Convert.ToString(idr["Date"]);

            enChemicalPricing_.YEN = DBNull.Value == idr["YEN"] ? (float?)null : Convert.ToSingle(idr["YEN"]);
            enChemicalPricing_.WON = DBNull.Value == idr["WON"] ? (float?)null : Convert.ToSingle(idr["WON"]);
            enChemicalPricing_.EURO = DBNull.Value == idr["EURO"] ? (float?)null : Convert.ToSingle(idr["EURO"]);

            enChemicalPricing_.MinYEN = DBNull.Value == idr["MinYEN"] ? (decimal?)null : Convert.ToDecimal(idr["MinYEN"]);
            enChemicalPricing_.MaxYEN = DBNull.Value == idr["MaxYEN"] ? (decimal?)null : Convert.ToDecimal(idr["MaxYEN"]);
           
            enChemicalPricing_.MinWON = DBNull.Value == idr["MinWON"] ? (decimal?)null : Convert.ToDecimal(idr["MinWON"]);
            enChemicalPricing_.MaxWON = DBNull.Value == idr["MaxWON"] ? (decimal?)null : Convert.ToDecimal(idr["MaxWON"]);

            enChemicalPricing_.MinEURO = DBNull.Value == idr["MinEURO"] ? (decimal?)null : Convert.ToDecimal(idr["MinEURO"]);
            enChemicalPricing_.MaxEURO = DBNull.Value == idr["MaxEURO"] ? (decimal?)null : Convert.ToDecimal(idr["MaxEURO"]);
        }
        private void ConstructObject_DisApprovalMessage(IDataReader idr, enAdminChemicalPrice enChemicalPricing_)
        {
            enChemicalPricing_.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
            enChemicalPricing_.USD = DBNull.Value == idr["USD"] ? (float?)null : Convert.ToSingle(idr["USD"]);
            enChemicalPricing_.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
            enChemicalPricing_.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
            enChemicalPricing_.MinUSD = DBNull.Value == idr["MinUSD"] ? 0 : Convert.ToUInt64(idr["MinUSD"]);
            enChemicalPricing_.MaxUSD = DBNull.Value == idr["MaxUSD"] ? 0 : Convert.ToUInt64(idr["MaxUSD"]);
        }
        private void ConstructObjectForList(IDataReader idr, enAdminChemicalPrice objENChemicalPrice)
        {
            objENChemicalPrice.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENChemicalPrice.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENChemicalPrice.ID = Convert.ToInt32(idr["id"]);
            objENChemicalPrice.Product = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            objENChemicalPrice.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
            objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            objENChemicalPrice.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
            objENChemicalPrice.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
            objENChemicalPrice.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            objENChemicalPrice.Region = DBNull.Value == idr["Region"] ? null : Convert.ToString(idr["Region"]);
            objENChemicalPrice.RegionWise = DBNull.Value == idr["Regionwise"] ? null : Convert.ToString(idr["Regionwise"]);
            objENChemicalPrice.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            objENChemicalPrice.USD = DBNull.Value == idr["USD"] ? (float?)null : Convert.ToSingle(idr["USD"]);
            objENChemicalPrice.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
            objENChemicalPrice.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
            objENChemicalPrice.MinUSD = DBNull.Value == idr["MinUSD"] ? (decimal?)null : Convert.ToDecimal(idr["MinUSD"]);
            objENChemicalPrice.MaxUSD = DBNull.Value == idr["MaxUSD"] ? (decimal?)null : Convert.ToDecimal(idr["MaxUSD"]);
            objENChemicalPrice.UpdatedDate = DBNull.Value == idr["UpdatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdatedDate"]);
            objENChemicalPrice.Flag = DBNull.Value == idr["Flag"] ? 0 : Convert.ToInt32(idr["Flag"]);

        }
        private void ConstructObjectForLatestRecordofProduct(IDataReader idr, enAdminChemicalPrice objENChemicalPrice)
        {
            objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            objENChemicalPrice.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            objENChemicalPrice.Week = DBNull.Value == idr["Week"] ? null : Convert.ToString(idr["Week"]);
            objENChemicalPrice.Date = DBNull.Value == idr["Date"] ? null : Convert.ToString(idr["Date"]);
            objENChemicalPrice.Day = DBNull.Value == idr["Day"] ? null : Convert.ToString(idr["Day"]);
        }
        private void ConstructObjectForSimpleViewProduct(IDataReader idr, enAdminChemicalPrice objENChemicalPrice)
        {
            objENChemicalPrice.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENChemicalPrice.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENChemicalPrice.ID = Convert.ToInt32(idr["id"]);
            objENChemicalPrice.Product = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            objENChemicalPrice.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
            objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            objENChemicalPrice.Week = DBNull.Value == idr["Week"] ? null : Convert.ToString(idr["Week"]);
            objENChemicalPrice.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            objENChemicalPrice.Day = DBNull.Value == idr["Day"] ? null : Convert.ToString(idr["Day"]);
            objENChemicalPrice.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
            objENChemicalPrice.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
            objENChemicalPrice.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            objENChemicalPrice.Region = DBNull.Value == idr["Region"] ? null : Convert.ToString(idr["Region"]);
            objENChemicalPrice.RegionWise = DBNull.Value == idr["Regionwise"] ? null : Convert.ToString(idr["Regionwise"]);

            objENChemicalPrice.USD = DBNull.Value == idr["USD"] ? (float?)null : Convert.ToSingle(idr["USD"]);
            objENChemicalPrice.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
            objENChemicalPrice.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
            objENChemicalPrice.MinUSD = DBNull.Value == idr["MinUSD"] ? 0 : Convert.ToUInt64(idr["MinUSD"]);
            objENChemicalPrice.MaxUSD = DBNull.Value == idr["MaxUSD"] ? 0 : Convert.ToUInt64(idr["MaxUSD"]);
            objENChemicalPrice.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            objENChemicalPrice.ApprovedBy = DBNull.Value == idr["ApprovedBy"] ? null : Convert.ToString(idr["ApprovedBy"]);
            objENChemicalPrice.Source = DBNull.Value == idr["Source"] ? null : Convert.ToString(idr["Source"]);
            objENChemicalPrice.Flag = DBNull.Value == idr["Flag"] ? 0 : Convert.ToInt32(idr["Flag"]);
            objENChemicalPrice.Remarks = DBNull.Value == idr["Remark"] ? null : Convert.ToString(idr["Remark"]);
            objENChemicalPrice.UpdatedDate = DBNull.Value == idr["UpdatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdatedDate"]);
            objENChemicalPrice.SourceDesc = DBNull.Value == idr["SourceDesc"] ? null : Convert.ToString(idr["SourceDesc"]);
        }
        private void ConstructObjectListForApproval(IDataReader idr, enAdminChemicalPrice objENChemicalPrice)
        {
            objENChemicalPrice.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENChemicalPrice.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENChemicalPrice.ID = Convert.ToInt32(idr["id"]);
            objENChemicalPrice.Product = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            objENChemicalPrice.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
            objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            objENChemicalPrice.Week = DBNull.Value == idr["Week"] ? null : Convert.ToString(idr["Week"]);
            objENChemicalPrice.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            objENChemicalPrice.Day = DBNull.Value == idr["Day"] ? null : Convert.ToString(idr["Day"]);
            objENChemicalPrice.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
            objENChemicalPrice.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
            objENChemicalPrice.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            objENChemicalPrice.Region = DBNull.Value == idr["Region"] ? null : Convert.ToString(idr["Region"]);
            objENChemicalPrice.RegionWise = DBNull.Value == idr["Regionwise"] ? null : Convert.ToString(idr["Regionwise"]);
            objENChemicalPrice.Remarks = DBNull.Value == idr["Remark"] ? null : Convert.ToString(idr["Remark"]);
            objENChemicalPrice.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            objENChemicalPrice.USD = DBNull.Value == idr["USD"] ? (float?)null : Convert.ToSingle(idr["USD"]);
            objENChemicalPrice.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
            objENChemicalPrice.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
            objENChemicalPrice.MinUSD = DBNull.Value == idr["MinUSD"] ? (decimal?)null : Convert.ToDecimal(idr["MinUSD"]);
            objENChemicalPrice.MaxUSD = DBNull.Value == idr["MaxUSD"] ? (decimal?)null : Convert.ToDecimal(idr["MaxUSD"]);
            objENChemicalPrice.Flag = DBNull.Value == idr["Flag"] ? 0 : Convert.ToInt32(idr["Flag"]);
            objENChemicalPrice.Source = DBNull.Value == idr["source"] ? null : Convert.ToString(idr["source"]);
            objENChemicalPrice.SourceDesc = DBNull.Value == idr["SourceDesc"] ? null : Convert.ToString(idr["SourceDesc"]);
            objENChemicalPrice.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            objENChemicalPrice.ApprovedBy = DBNull.Value == idr["ApprovedBy"] ? null : Convert.ToString(idr["ApprovedBy"]);
            objENChemicalPrice.UpdatedDate = DBNull.Value == idr["UpdatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdatedDate"]);
        }
        private void ConstructObjectListForUpdate(IDataReader idr, enAdminChemicalPrice objENChemicalPrice)
        {

            objENChemicalPrice.ID = Convert.ToInt32(idr["id"]);
            objENChemicalPrice.Product = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            objENChemicalPrice.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
            objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            objENChemicalPrice.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
            objENChemicalPrice.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
            objENChemicalPrice.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            objENChemicalPrice.Region = DBNull.Value == idr["Region"] ? null : Convert.ToString(idr["Region"]);
            objENChemicalPrice.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            objENChemicalPrice.Source = DBNull.Value == idr["source"] ? null : Convert.ToString(idr["source"]);
            objENChemicalPrice.USD = DBNull.Value == idr["USD"] ? (float?)null : Convert.ToSingle(idr["USD"]);
            objENChemicalPrice.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
            objENChemicalPrice.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
            objENChemicalPrice.MinUSD = DBNull.Value == idr["MinUSD"] ? (decimal?)null : Convert.ToDecimal(idr["MinUSD"]);
            objENChemicalPrice.MaxUSD = DBNull.Value == idr["MaxUSD"] ? (decimal?)null : Convert.ToDecimal(idr["MaxUSD"]);
            objENChemicalPrice.Week = DBNull.Value == idr["Week"] ? null : Convert.ToString(idr["Week"]);
            objENChemicalPrice.Date = DBNull.Value == idr["Date"] ? null : Convert.ToString(idr["Date"]);
            objENChemicalPrice.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            objENChemicalPrice.Day = DBNull.Value == idr["Day"] ? null : Convert.ToString(idr["Day"]);
            objENChemicalPrice.Location = DBNull.Value == idr["Location"] ? null : Convert.ToString(idr["Location"]);
            objENChemicalPrice.ModeOfDistrubution = DBNull.Value == idr["ModeOfDistrubution"] ? null : Convert.ToString(idr["ModeOfDistrubution"]);
            objENChemicalPrice.SourceDesc = DBNull.Value == idr["SourceDesc"] ? null : Convert.ToString(idr["SourceDesc"]);

            objENChemicalPrice.AICount = DBNull.Value == idr["AICount"] ? (decimal?)null : Convert.ToDecimal(idr["AICount"]);
            objENChemicalPrice.AIMin = DBNull.Value == idr["AIMin"] ? (decimal?)null : Convert.ToDecimal(idr["AIMin"]);
            objENChemicalPrice.AIMax = DBNull.Value == idr["AIMax"] ? (decimal?)null : Convert.ToDecimal(idr["AIMax"]);
        }
        private void ConstructObjectProductVarient(IDataReader idr, enAdminChemicalPrice enChemicalWeekly_)
        {
            enChemicalWeekly_.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
        }
        private void ConstructObjectProductLocation(IDataReader idr, enAdminChemicalPrice enChemicalWeekly_)
        {
            enChemicalWeekly_.Location = DBNull.Value == idr["Location"] ? null : Convert.ToString(idr["Location"]);
        }
        private void ConstructObjectForDownload(IDataReader idr, enAdminChemicalPrice objENChemicalPrice, string Range)
        {
            if (Range == "Yearly")
            {
                objENChemicalPrice.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
                objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
                objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
                objENChemicalPrice.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
                objENChemicalPrice.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
                objENChemicalPrice.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
            }
            else if (Range == "Monthly")
            {
                objENChemicalPrice.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
                objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
                objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
                objENChemicalPrice.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
                objENChemicalPrice.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
                objENChemicalPrice.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
                objENChemicalPrice.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
            }
            else if (Range == "Weekly")
            {
                objENChemicalPrice.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
                objENChemicalPrice.Type = DBNull.Value == idr["Type"] ? null : Convert.ToString(idr["Type"]);
                objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
                objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
                objENChemicalPrice.Week = DBNull.Value == idr["Week"] ? null : Convert.ToString(idr["Week"]);
                objENChemicalPrice.Date = DBNull.Value == idr["Date"] ? null : Convert.ToString(idr["Date"]);
                objENChemicalPrice.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
                objENChemicalPrice.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
                objENChemicalPrice.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
            }
            else
            {
                objENChemicalPrice.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
                objENChemicalPrice.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
                objENChemicalPrice.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
                objENChemicalPrice.ModeOfDistrubution = DBNull.Value == idr["ModeOfDistrubution"] ? null : Convert.ToString(idr["ModeOfDistrubution"]);
                objENChemicalPrice.Location = DBNull.Value == idr["Location"] ? null : Convert.ToString(idr["Location"]);
                objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
                objENChemicalPrice.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
                objENChemicalPrice.Min = DBNull.Value == idr["Min"] ? (decimal?)null : Convert.ToDecimal(idr["Min"]);
                objENChemicalPrice.Max = DBNull.Value == idr["Max"] ? (decimal?)null : Convert.ToDecimal(idr["Max"]);
                objENChemicalPrice.Day = DBNull.Value == idr["Day"] ? null : Convert.ToString(idr["Day"]);
            }
        }
        private void ConstructObjectListForCountPercentage(IDataReader idr, enAdminChemicalPrice enChemicalWeekly_)
        {
            enChemicalWeekly_.CountPercentage = DBNull.Value == idr["CountPercentage"] ? (float?)null : Convert.ToSingle(idr["CountPercentage"]);
        }
        private void ConstructObjectUserDetails(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enUser_.FirstName = DBNull.Value == idr["Fname"] ? null : Convert.ToString(idr["Fname"]);
            enUser_.LastName = DBNull.Value == idr["Lname"] ? null : Convert.ToString(idr["Lname"]);
            enUser_.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enUser_.Role = DBNull.Value == idr["Role"] ? null : Convert.ToString(idr["Role"]);
        }
        private void ConstructObjectForInsertEmployeeMaster(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
        }
        private void ConstructObjectPendingProductUsingRange(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ProductId = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enUser_.Region = DBNull.Value == idr["Regionwise"] ? null : Convert.ToString(idr["Regionwise"]);
        }

        private void ConstructObjectProduct(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
        }
        private void ConstructObjectCountry(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enUser_.Country = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
        }
        private void ConstructObjectListForEmployeeRecords(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enUser_.FirstName = DBNull.Value == idr["Fname"] ? null : Convert.ToString(idr["Fname"]);
            enUser_.ReportTo = DBNull.Value == idr["ReportTo"] ? 0 : Convert.ToInt32(idr["ReportTo"]);
            enUser_.Title = DBNull.Value == idr["Role"] ? null : Convert.ToString(idr["Role"]);
            enUser_.UniqID = DBNull.Value == idr["UniqID"] ? 0 : Convert.ToInt32(idr["UniqID"]);
        }
        private void ConstructObjectListForUserProduct(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            enUser_.ID = DBNull.Value == idr["UserId"] ? 0 : Convert.ToInt32(idr["UserId"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enUser_.Product = DBNull.Value == idr["ProductId"] ? 0 : Convert.ToInt32(idr["ProductId"]);
            enUser_.Products = DBNull.Value == idr["TotalProduct"] ? null : Convert.ToString(idr["TotalProduct"]);
        }
        private void ConstructObjectForLatestNews(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enUser_.NewsName = DBNull.Value == idr["NewsName"] ? null : Convert.ToString(idr["NewsName"]);
            enUser_.NewsDiscription = DBNull.Value == idr["NewsDiscription"] ? null : Convert.ToString(idr["NewsDiscription"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedTime"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedTime"]);    
        }



        public List<enAdminChemicalPrice> GetCountAdminChemPriceyearly(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> ListofChemicalPricingData = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetCurrentCount_Yearly]", new object[] {
                                                              enAdminChemicalPrice.Product,
                                                              enAdminChemicalPrice.Year,
                                                              enAdminChemicalPrice.Country,
                                                              enAdminChemicalPrice.ProductVariant

                                                           }))

            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectCountChemPriceyearly(idr, objenProduct);
                    ListofChemicalPricingData.Add(objenProduct);
                }
            }
            return ListofChemicalPricingData;
        }


        private void ConstructObjectCountChemPriceyearly(IDataReader idr, enAdminChemicalPrice enChemicalPricing_)
        {
            enChemicalPricing_.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            enChemicalPricing_.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
            enChemicalPricing_.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            enChemicalPricing_.Product = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            enChemicalPricing_.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
            enChemicalPricing_.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            enChemicalPricing_.Change_percentage = DBNull.Value == idr["Change_percentage"] ? (decimal?)null : Convert.ToDecimal(idr["Change_percentage"]);


        }



        public List<enAdminChemicalPrice> GetUpdateCountMonthly(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> ListofChemicalPricingData = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[UpdateCountMonthly]", new object[] {
                                                              enAdminChemicalPrice.Product,
                                                              enAdminChemicalPrice.Country,
                                                              enAdminChemicalPrice.ProductVariant,
                                                              enAdminChemicalPrice.Year,
                                                              enAdminChemicalPrice.Month

                                                           }))

            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateCountMonthly(idr, objenProduct);
                    ListofChemicalPricingData.Add(objenProduct);
                }
            }
            return ListofChemicalPricingData;
        }



        private void ConstructObjectUpdateCountMonthly(IDataReader idr, enAdminChemicalPrice enChemicalPricing_)
        {
            enChemicalPricing_.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            enChemicalPricing_.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
            enChemicalPricing_.Change_percentage = DBNull.Value == idr["Change_percentage"] ? (decimal?)null : Convert.ToDecimal(idr["Change_percentage"]);
            enChemicalPricing_.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enChemicalPricing_.ApprovedBy = DBNull.Value == idr["ApprovedBy"] ? null : Convert.ToString(idr["ApprovedBy"]);
            enChemicalPricing_.OnDate = DBNull.Value == idr["OnDate"] ? (DateTime?)null : Convert.ToDateTime(idr["OnDate"]);

        }

        public List<enAdminChemicalPrice> ShowAllCountMonthly(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> ListofShowAllCountMonthly = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[ShowAllCountMonthly]", new object[] {
                                                              enAdminChemicalPrice.Product,
                                                              enAdminChemicalPrice.Country,
                                                              enAdminChemicalPrice.ProductVariant,
                                                              enAdminChemicalPrice.Year


                                                           }))

            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectListofShowAllCountMonthly(idr, objenProduct);
                    ListofShowAllCountMonthly.Add(objenProduct);
                }
            }
            return ListofShowAllCountMonthly;
        }



        private void ConstructObjectListofShowAllCountMonthly(IDataReader idr, enAdminChemicalPrice enChemicalPricing_)
        {
            enChemicalPricing_.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            enChemicalPricing_.Count = DBNull.Value == idr["Count"] ? (decimal?)null : Convert.ToDecimal(idr["Count"]);
            enChemicalPricing_.Change_percentage = DBNull.Value == idr["Change_percentage"] ? (decimal?)null : Convert.ToDecimal(idr["Change_percentage"]);
            enChemicalPricing_.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enChemicalPricing_.ApprovedBy = DBNull.Value == idr["ApprovedBy"] ? null : Convert.ToString(idr["ApprovedBy"]);
            enChemicalPricing_.OnDate = DBNull.Value == idr["OnDate"] ? (DateTime?)null : Convert.ToDateTime(idr["OnDate"]);


        }
        public List<enAdminChemicalPrice> SearchVariant(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> ProductVariantList = new List<enAdminChemicalPrice>();

            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_SearchProductVariant]", new object[] {
                                                             enAdminChemicalPrice.CountryName,
                                                               enAdminChemicalPrice.ProductId
                                                           }))



                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                        ConstructObjectForSearchProductVariant(idr, objenProduct);
                        ProductVariantList.Add(objenProduct);
                    }
                }

            }

            catch (Exception ex)
            {

            }

            return ProductVariantList;
        }

        public List<enAdminChemicalPrice> SaveProductvariant(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> SaveProductvariant = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_updateProductvariant]", new object[] {
                                                             _enChemicalPricing.ID,
                                                              _enChemicalPricing.ProductId,
                                                              _enChemicalPricing.CountryName,
                                                             _enChemicalPricing.ProductVariant

                                                           }))



            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForSaveProductvariant(idr, objenProduct);
                    SaveProductvariant.Add(objenProduct);
                }
            }
            return SaveProductvariant;
        }


        private void ConstructObjectForSaveProductvariant(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
        }




        private void ConstructObjectForSearchProductVariant(IDataReader idr, enAdminChemicalPrice enChemicalPricing_)
        {
            enChemicalPricing_.ID = Convert.ToInt32(idr["Id"]);
            enChemicalPricing_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enChemicalPricing_.ProductId = DBNull.Value == idr["ProductId"] ? 0 : Convert.ToInt32(idr["ProductId"]);
            enChemicalPricing_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enChemicalPricing_.ProductVariant = DBNull.Value == idr["ProductVariantName"] ? null : Convert.ToString(idr["ProductVariantName"]);
        }


        public List<enAdminChemicalPrice> SearchProductVariantRelation(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> ProductVariantList = new List<enAdminChemicalPrice>();

            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_SearchProductvariantRelation]", new object[] {
                                                             enAdminChemicalPrice.CountryName,
                                                               enAdminChemicalPrice.ProductId
                                                           }))



                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                        ConstructObjectForSearchProductVariantRelation(idr, objenProduct);
                        ProductVariantList.Add(objenProduct);
                    }
                }

            }

            catch (Exception ex)
            {

            }

            return ProductVariantList;
        }

        public List<enAdminChemicalPrice> SaveProductVariantRelation(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> SaveProductVariantRelation = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_updateProductvariantRelation]", new object[] {
                                                             _enChemicalPricing.ID,
                                                              _enChemicalPricing.ProductId,
                                                              _enChemicalPricing.CountryName,
                                                             _enChemicalPricing.ProductVariant

                                                           }))



            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForSaveProductVariantRelation(idr, objenProduct);
                    SaveProductVariantRelation.Add(objenProduct);
                }
            }
            return SaveProductVariantRelation;
        }


        private void ConstructObjectForSaveProductVariantRelation(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
        }




        private void ConstructObjectForSearchProductVariantRelation(IDataReader idr, enAdminChemicalPrice enChemicalPricing_)
        {
            enChemicalPricing_.ID = Convert.ToInt32(idr["Id"]);
            enChemicalPricing_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enChemicalPricing_.ProductId = DBNull.Value == idr["ProductId"] ? 0 : Convert.ToInt32(idr["ProductId"]);
            enChemicalPricing_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enChemicalPricing_.ProductVariant = DBNull.Value == idr["ProductVariantName"] ? null : Convert.ToString(idr["ProductVariantName"]);
        }
        public List<enAdminChemicalPrice> UpdateChemPriceFlag(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> ListofUpdateChemPriceFlag = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateFlagChemPrice]", new object[] {
                                                              enAdminChemicalPrice.Product,
                                                              enAdminChemicalPrice.Country,
                                                              enAdminChemicalPrice.RangeName,
                                                              enAdminChemicalPrice.Flag
                                                           }))

            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objenProduct);
                    ListofUpdateChemPriceFlag.Add(objenProduct);
                }
            }
            return ListofUpdateChemPriceFlag;
        }


        public List<enAdminChemicalPrice> GetCountryBasedOnRegion(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> ListofUpdateChemPriceFlag = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetCountryBasedOnRegion]", new object[] {

                                                              enAdminChemicalPrice.regionID
                                                           }))

            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectGetCountryBasedOnRegion(idr, objenProduct);
                    ListofUpdateChemPriceFlag.Add(objenProduct);
                }
            }
            return ListofUpdateChemPriceFlag;
        }

        public List<enAdminChemicalPrice> GetProductBasedOnCategory(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> ListofUpdateChemPriceFlag = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetProductBasedOnCategoryandCountry]", new object[] {

                                                              enAdminChemicalPrice.categoryID,
                                                              enAdminChemicalPrice.CountryID
                                                           }))

            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectGetProductBasedOnCategory(idr, objenProduct);
                    ListofUpdateChemPriceFlag.Add(objenProduct);
                }
            }
            return ListofUpdateChemPriceFlag;
        }
        public List<enAdminChemicalPrice> ReadPortwiseExportnew(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPortWiseExportDataForTable]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.Product,
                                                            searchFilter.CountryName,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                            searchFilter.Year,
                                                            searchFilter.hsCode,
                                                            searchFilter.Grade,
                                                            searchFilter.ExporterName,
                                                            searchFilter.ExportCountry,
                                                            searchFilter.PortOfOrigin,
                                                            searchFilter.ConsigneeName,
                                                            searchFilter.CountryofImport,
                                                            searchFilter.PortOfDestination,
                                                            searchFilter.Incoterms,
                                                            searchFilter.ShipmentMode,
                                                            searchFilter.fromDateCompares,
                                                            searchFilter.toDateCompares,
                                                            searchFilter.SortColumn,
                                                            searchFilter.SortOrder,
                                                            searchFilter.UserId
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForPortWiseExport(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }

        public List<enAdminChemicalPrice> CheckPortWiseExportAvilability(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_CheckPortWiseExportAvilability]", new object[] {
                                                         
                                                            searchFilter.Product,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectResponse(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }

        public List<enAdminChemicalPrice> BindYear(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPortWiseExportBindYear]", new object[] {
                                                            searchFilter.Product,
                                                            searchFilter.CountryName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForEximYear(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }
        //public List<enAdminChemicalPrice> GetPortwiseexportdata(enAdminChemicalPrice searchFilter)
        //{
        //    List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
        //    try
        //    {
        //        using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetEximdata_forDDL]", new object[] {
        //                                                    searchFilter.Product,
        //                                                    searchFilter.CountryName
        //                                                    }))
        //        {
        //            while (idr.Read())
        //            {
        //                enAdminChemicalPrice objEN = new enAdminChemicalPrice();
        //                ConstructObjectForEximdata(idr, objEN);
        //                listReadReadPortwiseExport.Add(objEN);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return listReadReadPortwiseExport;
        //}
        public List<enAdminChemicalPrice> GetAllFilterValues(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetAllFilterValues]", new object[] {
                                                            searchFilter.Product,
                                                            searchFilter.CountryName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForEximdata(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }
        public List<enAdminChemicalPrice> BindHSCode(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPortWiseExportBindHSCode]", new object[] {
                                                            searchFilter.Product,
                                                            searchFilter.CountryName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForEximBindHSCode(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }

        public List<enAdminChemicalPrice> BindGrade(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPortWiseExportBindGrade]", new object[] {
                                                            searchFilter.Product,
                                                            searchFilter.CountryName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForEximBindGrade(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }

        public List<enAdminChemicalPrice> BindExportername(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPortWiseExportBindExportername]", new object[] {
                                                            searchFilter.Product,
                                                            searchFilter.CountryName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForEximBindExportername(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }

        public List<enAdminChemicalPrice> BindExporterCountry(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPortWiseExportBindExportingCountry]", new object[] {
                                                            searchFilter.Product,
                                                            searchFilter.CountryName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForEximBindExporterCountry(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }

        public List<enAdminChemicalPrice> BindPortofOrgine(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPortWiseExportBindPortOfOrigin]", new object[] {
                                                             searchFilter.Product,
                                                            searchFilter.CountryName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForEximBindPortofOrgine(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }

        public List<enAdminChemicalPrice> BindConsigneeName(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPortWiseExportBindConsigneeName]", new object[] {
                                                             searchFilter.Product,
                                                            searchFilter.CountryName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForEximBindConsigneeName(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }


        public List<enAdminChemicalPrice> BindCountryOfImport(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPortWiseExportCountryofImport]", new object[] {
                                                            searchFilter.Product,
                                                            searchFilter.CountryName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForEximBindCountryOfImport(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }


        public List<enAdminChemicalPrice> BindPortOfDestination(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPortWiseExportPortOfDestination]", new object[] {
                                                             searchFilter.Product,
                                                            searchFilter.CountryName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForEximBindPortOfDestination(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }

        public List<enAdminChemicalPrice> BindInconterms(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPortWiseExportIncoterms]", new object[] {
                                                             searchFilter.Product,
                                                            searchFilter.CountryName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForEximBindInconterms(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }

        public List<enAdminChemicalPrice> BindShipmentMode(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> listReadReadPortwiseExport = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPortWiseShipmentMode]", new object[] {
                                                              searchFilter.Product,
                                                            searchFilter.CountryName
                                                             }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectForEximBindShipmentModes(idr, objEN);
                        listReadReadPortwiseExport.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadReadPortwiseExport;
        }
        public List<enAdminChemicalPrice> GetAllProductFinderDetails(enSearchFilter enSearchFilter)
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetAllProductFinderDetails]", new object[] {

                                                                 enSearchFilter.StartRowNumber,
                                                                  enSearchFilter.EndRowNumber
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    objENProduct.RowNumber = Convert.ToInt32(idr["RowNumber"]);
                    objENProduct.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
                    objENProduct.ProductName = Convert.ToString(idr["ProductName"]);
                    objENProduct.CategoryName = Convert.ToString(idr["CategoryName"]);
                    objENProduct.ProductChildName = Convert.ToString(idr["ChildProductNames"]);

                    // ConstructObjectGetAllProductFinderDetails(idr, objENProduct);
                    listOfProduct.Add(objENProduct);
                }
            }

            return listOfProduct;
        }


        public List<enAdminChemicalPrice> BindProduct()
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetAllProductList]", new object[] {
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindProduct(idr, objENProduct);
                    listOfProduct.Add(objENProduct);
                }
            }

            return listOfProduct;
        }

        public List<enAdminChemicalPrice> BinduserSubscribeProduct(enSearchFilter enSearchFilter)
        {
            List<enAdminChemicalPrice> listOfBinduserSubscribeProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_getAllProductsubscribebyUser]", new object[] {
                                                        enSearchFilter.UserId
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindProduct(idr, objENProduct);
                    listOfBinduserSubscribeProduct.Add(objENProduct);
                }
            }

            return listOfBinduserSubscribeProduct;
        }

        public List<Product> BinduserSubscribeProductnews(enSearchFilter enSearchFilter)
        {
            List<Product> listOfBinduserSubscribeProduct = new List<Product>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_getAllProductsubscribebyUser]", new object[] {
                                                        enSearchFilter.UserId,
                                                         enSearchFilter.PageNo,
                                                          enSearchFilter.PageSize,
                                                           enSearchFilter.SearchTerm
                                                            }))
            {
                while (idr.Read())
                {
                    Product objENProduct = new Product();
                    ConstructObjectCreditDownloadProducts(idr, objENProduct);
                    listOfBinduserSubscribeProduct.Add(objENProduct);
                }
            }

            return listOfBinduserSubscribeProduct;
        }


        public List<enAdminChemicalPrice> ProductSearchforCategory(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfProductSearchforCategory = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_ProductSearchforCategory]", new object[] {
                                                    enAdminChemicalPrice.ProductName
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetAllProductFinderDetails(idr, objENProduct);
                    listOfProductSearchforCategory.Add(objENProduct);
                }
            }

            return listOfProductSearchforCategory;
        }

        public List<enAdminChemicalPrice> GetProductbasedonCategory(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfGetProductbasedonCategory = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetProductbasedCategory]", new object[] {
                                                    enAdminChemicalPrice.CategoryName
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetAllProductFinderDetails(idr, objENProduct);
                    listOfGetProductbasedonCategory.Add(objENProduct);
                }
            }

            return listOfGetProductbasedonCategory;
        }
        public List<enAdminChemicalPrice> get_Two_months_before_loginDetails()
        {
            List<enAdminChemicalPrice> Two_months_before_loginDetails = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Two_months_before_loginDetails]", new object[] {
                                                              
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectusp_Two_months_before_loginDetails(idr, objEN);
                        Two_months_before_loginDetails.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Two_months_before_loginDetails;
        }

        public List<enAdminChemicalPrice> DeleteProductFeedstock(enAdminChemicalPrice searchFilter)
        {
            List<enAdminChemicalPrice> ListofResponse = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_DeleteFeedstockProduct]", new object[] {
                                                            searchFilter.ID,


                                                           }))

            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objenProduct);
                    ListofResponse.Add(objenProduct);
                }
            }
            return ListofResponse;
        }


        public List<enAdminChemicalPrice> ListProductFeedstock(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> ListofResponse = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[sp_GetFeedStockParentChildData]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,

                                                           }))

            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectFeedstock(idr, objenProduct);
                    ListofResponse.Add(objenProduct);
                }
            }
            return ListofResponse;
        }

        public List<enAdminChemicalPrice> getChemicalPricingList(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> getChemicalPricingLists = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetChemicalPricingList_ForFiledownload]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectusp_getChemicalPricingList(idr, objEN);
                        getChemicalPricingLists.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return getChemicalPricingLists;
        }


        public List<enAdminChemicalPrice> getgetoverview_list(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> getgetoverviewLists = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_Getoverview_list]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectUSP_Getoverview_list(idr, objEN);
                        getgetoverviewLists.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return getgetoverviewLists;
        }



        public List<enAdminChemicalPrice> InsertUnit(enAdminChemicalPrice obj)
        {
            List<enAdminChemicalPrice> data = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertPricingUnit]", new object[] {
                                                            obj.ID,
                                                            obj.Product,
                                                            obj.UnitName,
                                                            obj.Country,
                                                            obj.CountryName,
                                                            obj.QueryType,
                                                            obj.Currency
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        ConstructObjectUpdateChemPriceFlag(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public List<enAdminChemicalPrice> GetUnitList(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> RangeList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetPricingUnitDataList]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        ConstructObjectForUnitList(idr, objENLeads);
                        RangeList.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return RangeList;
        }
        public List<enAdminChemicalPrice> GetCurrencyUpdateDetails()
        {
            List<enAdminChemicalPrice> listOfPricingDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetPendingDataForCurrencyConverter]", new object[] {
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectForCurrencyDetails(idr, objENChemicalPrice);
                        listOfPricingDataList.Add(objENChemicalPrice);
                    }
                }

                return listOfPricingDataList;
            }
            catch (Exception ex)
            {

            }
            return listOfPricingDataList;
        }
        public List<enAdminChemicalPrice> ConvertCurrencyData(enAdminChemicalPrice obj)
        {
            List<enAdminChemicalPrice> data = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_ConvertCurrencyData]", new object[] {
                                                            obj.RangeName,
                                                            obj.Type,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        ConstructObjectUpdateChemPriceFlag(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public List<enAdminChemicalPrice> ConvertCurrencyDataYearly(enAdminChemicalPrice obj)
        {
            List<enAdminChemicalPrice> data = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_ConvertMonthlyDataforFinancialyear]", new object[] {
                                                            obj.Country,
                                                            obj.Year,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        ConstructObjectUpdateChemPriceFlag(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public List<enAdminChemicalPrice> ConvertCurrencyDataYEARLYExcel(enAdminChemicalPrice obj)
        {
            List<enAdminChemicalPrice> data = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_ConvertMonthlyDataforFinancialyear_Excel]", new object[] {
                                                            obj.Country,
                                                            obj.Year,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        ConstructObjectUpdateChemPriceFlag(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }


        //------------------------------------------------------------------------------------------
        public List<enAdminChemicalPrice> ConvertCurrencyDataExcel(enAdminChemicalPrice obj)
        {
            string Procedure = "";
         
            if (obj.RangeName == "WeeklyToMonthly")
            {
                Procedure = "[dbo].[Usp_ConvertCurrencyDataWeeklyTomonthly_Excel]";
            }
            else {
                Procedure = "[dbo].[Usp_ConvertCurrencyData_Excel]";
            }

            List<enAdminChemicalPrice> data = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, Procedure, new object[] {
                                                            obj.RangeName,
                                                            obj.Type,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        ConstructObjectUpdateChemPriceFlag(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }


        //----------------------------------------------------------------------------------------

        public List<enAdminChemicalPrice> GetCurrencyUpdateDetailsExcel()
        {
            List<enAdminChemicalPrice> listOfPricingDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetPendingDataForCurrencyConverter_Excel]", new object[] {
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectForCurrencyDetails(idr, objENChemicalPrice);
                        listOfPricingDataList.Add(objENChemicalPrice);
                    }
                }

                return listOfPricingDataList;
            }
            catch (Exception ex)
            {

            }
            return listOfPricingDataList;
        }


        public List<enAdminChemicalPrice> CustomerProductTracking(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfGetProductbasedonCategory = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertCustomerProductTracking]", new object[] {
                                                    enAdminChemicalPrice.ProductId,
                                                    enAdminChemicalPrice.UserId,
                                                     enAdminChemicalPrice.CountryID ,
                                                      enAdminChemicalPrice.rangeFID
                                                            }))


                return listOfGetProductbasedonCategory;
        }

        public List<enAdminChemicalPrice> BindCountryBehalf_Of_Product(enAdminChemicalPrice en)
        {
            List<enAdminChemicalPrice> listOfCountry = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetTriggerSubscribeCountry]", new object[] {
                                                                  //"COUNTRY",
                                                                  en.UserId,
                                                                  en.ProductId,
                                                                   //en.CountryID,
                                                                   // en.RangeName,
                                                                   //  en.ProductVariant,
                                                                   //   en.Min,
                                                                   //   en.Max,
            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindlistOfCountry(idr, objENProduct);
                    listOfCountry.Add(objENProduct);
                }
            }

            return listOfCountry;
        }

        public List<enAdminChemicalPrice> SaveChemicalTrigger(enAdminChemicalPrice en)
        {
            List<enAdminChemicalPrice> listOfCountry = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetdropdownValuesForSetTrigger]", new object[] {
                                                                  "SAVE_TRIGGER_RECORD",
                                                                  en.UserId,
                                                                  en.ProductId,
                                                                   en.CountryID,
                                                                    en.RangeName,
                                                                     en.ProductVariant,
                                                                       en.Min,
                                                                      en.Max,
                                                                      en.Currency,
            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objENProduct);
                    listOfCountry.Add(objENProduct);
                }
            }

            return listOfCountry;
        }
        public List<enAdminChemicalPrice> RemoveChemicalTrigger(enAdminChemicalPrice en)
        {
            List<enAdminChemicalPrice> listOfdata = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetdropdownValuesForSetTrigger]", new object[] {
                                                                  "RemoveChemicalTrigger",
                                                                   en.UserId,
                                                                   en.ID,
                                                                    en.CountryID,
                                                                     en.RangeName,
                                                                      en.ProductVariant,
                                                                        en.Min,
                                                                      en.Max,
                                                                       en.Currency,
            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objENProduct);
                    listOfdata.Add(objENProduct);
                }
            }

            return listOfdata;
        }
        public List<enAdminChemicalPrice> ReadBasedOnSearchFilter(enSearchFilter en)
        {
            List<enAdminChemicalPrice> listOfDATA = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetChemicalTrigger_CustomSearch]", new object[] {
                                                               en.UserId,
                                                                en.searchKeyword,
                                                                en.StartRowNumber,
                                                                en.EndRowNumber
            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectForProductTrigger(idr, objENProduct);
                    listOfDATA.Add(objENProduct);
                }
            }

            return listOfDATA;
        }
        public List<enAdminChemicalPrice> BindProductforTrigger(enAdminChemicalPrice en)
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetdropdownValuesForSetTrigger]", new object[] {
                                                                   "PRODUCT",
                                                                  en.UserId,
                                                                  en.ProductId,
                                                                  en.CountryID,
                                                                   en.RangeName,
                                                                    en.ProductVariant,
                                                                     en.Min,
                                                                      en.Max,
                                                                     en.Currency,
            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindProductTrigger(idr, objENProduct);
                    listOfProduct.Add(objENProduct);
                }
            }

            return listOfProduct;
        }
        public List<enAdminChemicalPrice> BindProductVariant(enAdminChemicalPrice en)
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetdropdownValuesForSetTrigger]", new object[] {
                                                                  "PRODUCTVARIENT",
                                                                   en.UserId,
                                                                   en.ProductId,
                                                                   en.CountryID,
                                                                  // "Monthly",// en.RangeName,
                                                                   en.RangeName,
                                                                   en.ProductVariant,
                                                                   en.Min,
                                                                   en.Max,
                                                                   en.Currency,
            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindProductVariant(idr, objENProduct);
                    listOfProduct.Add(objENProduct);
                }
            }

            return listOfProduct;
        }
        public List<enAdminChemicalPrice> BindMinMax(enAdminChemicalPrice en)
        {
            List<enAdminChemicalPrice> listOfdata = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetdropdownValuesForSetTrigger]", new object[] {
                                                               "BindMinMax",
                                                                  en.UserId,
                                                                  en.ProductId,
                                                                   en.CountryID,
                                                                   en.RangeName,
                                                                     en.ProductVariant,
                                                                      en.Min,
                                                                      en.Max,
                                                                      en.Currency,
            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectMinMax(idr, objENProduct);
                    listOfdata.Add(objENProduct);
                }
            }

            return listOfdata;
        }

        public List<enAdminChemicalPrice> GetUserlist(enAdminChemicalPrice obj)
        {
            List<enAdminChemicalPrice> data = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetUserdetailsForTrigger]", new object[] {
                                                            obj.UserId
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        ConstructObjectForUserTrigger(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public List<enAdminChemicalPrice> listOfUserTriggerDetails()
        {
            List<enAdminChemicalPrice> data = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetUserIdForTrigger]", new object[] {

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        ConstructObjectForUserList(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        
        public List<enAdminChemicalPrice> GetlistTriggers(enAdminChemicalPrice obj)
        {
            List<enAdminChemicalPrice> data = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_SendPriceTrigger]", new object[] {
                                                              obj.UserId,
                                                              obj.ProductName,
                                                              obj.CountryName,
                                                              obj.RangeName,
                                                              obj.ProductVariant,
                                                              obj.Min,
                                                              obj.Max,
                                                              obj.Currency,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        ConstructObjectForTrigger(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public List<enAdminChemicalPrice> UpdatelistTriggers(enAdminChemicalPrice obj)
        {
            List<enAdminChemicalPrice> data = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateSendPriceTriggerSatus]", new object[] {
                                                              obj.ID,
                                                              obj.ProductName,
                                                              obj.CountryName,
                                                              obj.RangeName,
                                                              obj.ProductVariant,
                                                              obj.Min,
                                                              obj.Max,
                                                              obj.Currency,
                                                            }))
                {
                    while (idr.Read())
                    {
                        //enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        //ConstructObjectForTrigger(idr, objENLeads);
                        //data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public List<enAdminChemicalPrice> DeleteCommentaryPricing(string Product, string Country)
        {
            List<enAdminChemicalPrice> ListofResponse = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_DeleteCommentaryPricing]", new object[] {
                                                            Product,
                                                            Country
                                                           }))

            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objenProduct);
                    ListofResponse.Add(objenProduct);
                }
            }
            return ListofResponse;
        }

        public List<enAdminChemicalPrice> CheckProductVarient(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> CheckProductVarient = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_ProductVarient]", new object[] {
                                                     enAdminChemicalPrice.ProductId,
                                                    enAdminChemicalPrice.Country,
                                                     enAdminChemicalPrice.ProductVariant
                }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectResponse(idr, objENChemicalPrice);
                        CheckProductVarient.Add(objENChemicalPrice);
                    }
                }

                return CheckProductVarient;
            }
            catch (Exception ex)
            {

            }
            return CheckProductVarient;
        }


        public List<enAdminChemicalPrice> BindCountry()
        {
            List<enAdminChemicalPrice> listOfCountry = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_Country_READ]", new object[] {
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindCountry(idr, objENProduct);
                    listOfCountry.Add(objENProduct);
                }
            }

            return listOfCountry;
        }



        public List<enAdminChemicalPrice> BindFreelCountry(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfCountry = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_BindFreelCountry]", new object[] {
                                                               enAdminChemicalPrice.Product

                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindFreelCountry(idr, objENProduct);
                    listOfCountry.Add(objENProduct);
                }
            }

            return listOfCountry;
        }


        public List<enAdminChemicalPrice> BindFreelProduct(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_BindFreelProduct]", new object[] {
                                                               
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindProduct(idr, objENProduct);
                    listOfProduct.Add(objENProduct);
                }
            }

            return listOfProduct;
        }



        public List<enAdminChemicalPrice> BindFreelVariant(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_BindFreelProductVariant]", new object[] {
                                                                 enAdminChemicalPrice.Product,
                                                                enAdminChemicalPrice.Country
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindFreelVariant(idr, objENProduct);
                    listOfProduct.Add(objENProduct);
                }
            }

            return listOfProduct;
        }




        public List<enAdminChemicalPrice> InsertNewFreelUser(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> ListInsertNewFreelUser = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[InsertNew_FreelSubscriptions]", new object[] {
                                                                 enAdminChemicalPrice.Name,
                                                                 enAdminChemicalPrice.Email,
                                                                 enAdminChemicalPrice.Phone,
                                                                 enAdminChemicalPrice.CountryID,
                                                                 enAdminChemicalPrice.UserPassword,
                                                                 enAdminChemicalPrice.IsActive,
                                                                 enAdminChemicalPrice.CreatedBy
                }))

                    while (idr.Read())
                    {
                        enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                        ConstructObjectUpdateChemPriceFlag(idr, objenProduct);
                        ListInsertNewFreelUser.Add(objenProduct);
                    }


                return ListInsertNewFreelUser;
            }
            catch (Exception ex)
            {

            }
            return ListInsertNewFreelUser;

        }




        public List<enAdminChemicalPrice> InsertFreelDataAccessRange(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> ListInsertNewFreelUser = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[InsertFreelDataAccessRange]", new object[] {
                                                                 enAdminChemicalPrice.Email,
                                                                 enAdminChemicalPrice.CountryName,
                                                                 enAdminChemicalPrice.Product,
                                                                 enAdminChemicalPrice.ProductVariant,
                                                                 enAdminChemicalPrice.RangeName,
                                                                  enAdminChemicalPrice.Price,
                                                                 enAdminChemicalPrice.CreatedBy
                }))

                    while (idr.Read())
                    {
                        enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                        ConstructObjectUpdateChemPriceFlag(idr, objenProduct);
                        ListInsertNewFreelUser.Add(objenProduct);
                    }


                return ListInsertNewFreelUser;
            }
            catch (Exception ex)
            {

            }
            return ListInsertNewFreelUser;

        }



        public List<enAdminChemicalPrice> BindFreelUser()
        {
            List<enAdminChemicalPrice> listOfCountry = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_BindFreelUser]", new object[] {
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindFreelUser(idr, objENProduct);
                    listOfCountry.Add(objENProduct);
                }
            }

            return listOfCountry;
        }



        public List<enAdminChemicalPrice> GetAllFreelPricing(enSearchFilter enSearchFilter)
        {
            List<enAdminChemicalPrice> listGetAllFreelPricing = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAllFreelPricing]", new object[] {
                                                     enSearchFilter.StartRowNumber,
                                                     enSearchFilter.EndRowNumber
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetAllFreelPricing(idr, objENProduct);
                    listGetAllFreelPricing.Add(objENProduct);
                }
            }

            return listGetAllFreelPricing;
        }



        public List<enAdminChemicalPrice> InsertFreelFeedBack(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> ListInsertFreelFeedBack = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertFreelFeedBack]", new object[] {
                                                                 enAdminChemicalPrice.ID,
                                                                  enAdminChemicalPrice.Remarks,
                                                                   enAdminChemicalPrice.Rating
                }))

                    while (idr.Read())
                    {
                        enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                        ConstructObjectUpdateChemPriceFlag(idr, objenProduct);
                        ListInsertFreelFeedBack.Add(objenProduct);
                    }


                return ListInsertFreelFeedBack;
            }
            catch (Exception ex)
            {

            }
            return ListInsertFreelFeedBack;

        }



        public List<enAdminChemicalPrice> GetFreelPricingDatabasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listGetFreelPricingDatabasedonFilter = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetFreelPricingDatabasedonFilter]", new object[] {
                                                                 enAdminChemicalPrice.Email,
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetAllFreelPricing(idr, objENProduct);
                    listGetFreelPricingDatabasedonFilter.Add(objENProduct);
                }
            }

            return listGetFreelPricingDatabasedonFilter;
        }


        public List<enAdminChemicalPrice> GetFreelPricingBasedonUser(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listGetFreelPricingBasedonUser = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetFreelPricingDatabasedonUser]", new object[] {
                                                                 enAdminChemicalPrice.Email,
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetAllFreelPricing(idr, objENProduct);
                    listGetFreelPricingBasedonUser.Add(objENProduct);
                }
            }

            return listGetFreelPricingBasedonUser;
        }

        public List<enAdminChemicalPrice> GetFreelDataRangebasedonUser(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listGetFreelDataRangebasedonUser = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetFreelDataRangeBasedOnUser]", new object[] {
                                                                 enAdminChemicalPrice.Email
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetFreelDataRangebasedonUser(idr, objENProduct);
                    listGetFreelDataRangebasedonUser.Add(objENProduct);
                }
            }

            return listGetFreelDataRangebasedonUser;
        }


        public List<enAdminChemicalPrice> InsertFreelPricingRecord(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listInsertFreelPricingRecord = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertFreelPricingRecord]", new object[] {
                                                                 enAdminChemicalPrice.CountryName,
                                                                 enAdminChemicalPrice.ProductName,
                                                                 enAdminChemicalPrice.ProductVariant,
                                                                 enAdminChemicalPrice.RangeName,
                                                                 enAdminChemicalPrice.Source,
                                                                 enAdminChemicalPrice.SourceDesc,
                                                                 enAdminChemicalPrice.Count,
                                                                 enAdminChemicalPrice.Email
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objENProduct);
                    listInsertFreelPricingRecord.Add(objENProduct);
                }
            }

            return listInsertFreelPricingRecord;
        }


        public List<enAdminChemicalPrice> NewsDetailsTracking(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfGetProductbasedonCategory = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_InsertNewsDetailsTracking]", new object[] {
                                                    enAdminChemicalPrice.NewsId,
                                                    enAdminChemicalPrice.ReportId,
                                                    enAdminChemicalPrice.Type

                                                            }))


                return listOfGetProductbasedonCategory;
        }

        public List<enAdminChemicalPrice> GetFreelPricingProductEmpRelation(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listGetFreelPricingProductEmpRelation = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetFreelPriceProductEmpRelation]", new object[] {
                                                                 enAdminChemicalPrice.UserId,
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetAllFreelPricing(idr, objENProduct);
                    listGetFreelPricingProductEmpRelation.Add(objENProduct);
                }
            }

            return listGetFreelPricingProductEmpRelation;
        }


        public List<enAdminChemicalPrice> GetFreelAllPricingForUserBilling(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listGetFreelAllPricingForUserBilling = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetFreelALLPricingDataforUserBilling]", new object[] {
                                                                 enAdminChemicalPrice.Email,
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber

                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetFreelAllPricingForUserBilling(idr, objENProduct);
                    listGetFreelAllPricingForUserBilling.Add(objENProduct);
                }
            }

            return listGetFreelAllPricingForUserBilling;
        }

        public List<enAdminChemicalPrice> GetFreelPricingForUserBilling(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listGetFreelPricingForUserBilling = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetFreelPricingDataforUserBilling]", new object[] {
                                                                 enAdminChemicalPrice.Email

                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetFreelPricingForUserBilling(idr, objENProduct);
                    listGetFreelPricingForUserBilling.Add(objENProduct);
                }
            }

            return listGetFreelPricingForUserBilling;
        }

        public List<enAdminChemicalPrice> CheckForRangeUpdateMonthly(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listCheckForRangeUpdateMonthly = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_CheckForRangeUpdateMonthly]", new object[] {
                                                                 enAdminChemicalPrice.Email
                                                            }))


                return listCheckForRangeUpdateMonthly;
        }

        public List<enAdminChemicalPrice> GetCheckUserAccountDetails(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listGetCheckUserAccountDetails = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_CheckFreelUserAccountDetails]", new object[] {
                                                                 enAdminChemicalPrice.Email
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objENProduct);
                    listGetCheckUserAccountDetails.Add(objENProduct);
                }
            }

            return listGetCheckUserAccountDetails;
        }


        public List<enAdminChemicalPrice> GetAllEmpName()
        {
            List<enAdminChemicalPrice> listOfEmployeeRecords = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAllAssociateName]", new object[] {

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectForAllEmployee(idr, objENChemicalPrice);
                        listOfEmployeeRecords.Add(objENChemicalPrice);
                    }
                }

                return listOfEmployeeRecords;
            }
            catch (Exception ex)
            {

            }
            return listOfEmployeeRecords;
        }

        public List<enAdminChemicalPrice> GetAppraisalAmountList(enAdminChemicalPrice _enChemicalPricing)
        {
            List<enAdminChemicalPrice> ObjInsert = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAppraisalAmountList]", new object[] {
                                                               _enChemicalPricing.searchKeyword,
                                                                _enChemicalPricing.fromDateCompares,
                                                                 _enChemicalPricing.toDateCompares,
                                                             _enChemicalPricing.CreatedBy,
                                                           _enChemicalPricing.StartRowNumber,
                                                          _enChemicalPricing.EndRowNumber
                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForAppraisaldata(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enAdminChemicalPrice> insertAppraisalAmount(enAdminChemicalPrice _enChemicalPricing)
        {
            List<enAdminChemicalPrice> ObjInsert = new List<enAdminChemicalPrice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_insertAppraisalAmount]", new object[] {
                                                            _enChemicalPricing.ID,
                                                            _enChemicalPricing.Name,
                                                            _enChemicalPricing.Date,
                                                            _enChemicalPricing.IncentiveAmount,
                                                            _enChemicalPricing.Amount,
                                                            _enChemicalPricing.CreatedBy
                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }

        public List<enAdminChemicalPrice> BindUserbasedonSalesPackageSubscription(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listBindUserbasedonSalesPackageSubscription = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetUserbasedonSalesPackageSubscription]", new object[] {


                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindUserbasedonSalesPackageSubscription(idr, objENProduct);
                    listBindUserbasedonSalesPackageSubscription.Add(objENProduct);
                }
            }

            return listBindUserbasedonSalesPackageSubscription;
        }


        public List<enAdminChemicalPrice> InsertWrokSpacedetails(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listInsertWrokSpacedetails = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertIntoSA_Workpace]", new object[] {
                                                                 enAdminChemicalPrice.ID,
                                                                 enAdminChemicalPrice.FileName,
                                                                 enAdminChemicalPrice.Name,
                                                                 enAdminChemicalPrice.CreatedBy


                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objENProduct);
                    listInsertWrokSpacedetails.Add(objENProduct);
                }
            }

            return listInsertWrokSpacedetails;
        }



        public List<enAdminChemicalPrice> GetListofWorkSpace(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listofWorkSpace = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAdminListOfWorkSpace]", new object[] {
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber,
                                                                 enAdminChemicalPrice.CreatedBy


                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectlistofWorkSpace(idr, objENProduct);
                    listofWorkSpace.Add(objENProduct);
                }
            }

            return listofWorkSpace;
        }


        public List<enAdminChemicalPrice> GetListofWorkSpacebasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listofWorkSpace = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAdminListOfWorkSpaceBasedOnFilter]", new object[] {
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber,
                                                                 enAdminChemicalPrice.UserId,
                                                                  enAdminChemicalPrice.CreatedBy


                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectlistofWorkSpace(idr, objENProduct);
                    listofWorkSpace.Add(objENProduct);
                }
            }

            return listofWorkSpace;
        }

        public List<enAdminChemicalPrice> GetListofWorkSpacebasedonUser(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> GetListofWorkSpacebasedonUser = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetListOfWorkSpaceBasedOnUser]", new object[] {
                                                                  enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber,
                                                                 enAdminChemicalPrice.UserId

                                                            }))

            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectlistofWorkSpaceBasedonUser(idr, objenProduct);
                    GetListofWorkSpacebasedonUser.Add(objenProduct);
                }
            }
            return GetListofWorkSpacebasedonUser;
        }


        public List<enAdminChemicalPrice> UserbasedonCompany(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfUserbasedonCompany = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetUserbasedonOrganisation]", new object[] {

                                                    enAdminChemicalPrice.UserId

                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindUserbasedonSalesPackageSubscription(idr, objENProduct);
                    listOfUserbasedonCompany.Add(objENProduct);
                }
            }


            return listOfUserbasedonCompany;
        }


        public List<enAdminChemicalPrice> InsertWorkSpacebyCustomer(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listInsertWorkSpacebyCustomer = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertintoWorkSpacebyCustomer]", new object[] {
                                                                 enAdminChemicalPrice.ID,
                                                                 enAdminChemicalPrice.CreatedBy,
                                                                 enAdminChemicalPrice.RowId


                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objENProduct);
                    listInsertWorkSpacebyCustomer.Add(objENProduct);
                }
            }

            return listInsertWorkSpacebyCustomer;
        }

        public List<enAdminChemicalPrice> getIndustrylanding_list(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> getgetIndustrylandingLists = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetIndustrylanding_list]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        ConstructObjectgetIndustrylanding_list(idr, objEN);
                        getgetIndustrylandingLists.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return getgetIndustrylandingLists;
        }


        public List<enAdminChemicalPrice> BinduserProduct(enSearchFilter enSearchFilter)
        {
            List<enAdminChemicalPrice> listOfBinduserSubscribeProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_ProductsubscribebyUser]", new object[] {
                                                        enSearchFilter.UserId
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindProduct(idr, objENProduct);
                    listOfBinduserSubscribeProduct.Add(objENProduct);
                }
            }

            return listOfBinduserSubscribeProduct;
        }


        public List<enAdminChemicalPrice> BindPlantBasedonProduct(enSearchFilter enSearchFilter)
        {
            List<enAdminChemicalPrice> listOfBindPlantBasedonProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetPlantBasedonProduct]", new object[] {
                                                        enSearchFilter.ProductId
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectlistBindPlantBasedonProduct(idr, objENProduct);
                    listOfBindPlantBasedonProduct.Add(objENProduct);
                }
            }

            return listOfBindPlantBasedonProduct;
        }


        public List<enAdminChemicalPrice> SaveMaintenanceAndShutdownTrigger(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfCountry = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertMaintenanceAndShutdownTrigger]", new object[] {

                                                                  enAdminChemicalPrice.UserId,
                                                                  enAdminChemicalPrice.ProductId,
                                                                  enAdminChemicalPrice.Plant

            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objENProduct);
                    listOfCountry.Add(objENProduct);
                }
            }

            return listOfCountry;
        }


        public List<enAdminChemicalPrice> ReadMarketAnalysisTriggerBasedOnSearchFilter(enSearchFilter enSearchFilter)
        {
            List<enAdminChemicalPrice> listOfDATA = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetMarketAnalysisTrigger_CustomSearch]", new object[] {
                                                                enSearchFilter.UserId,
                                                                enSearchFilter.searchKeyword,
                                                                enSearchFilter.StartRowNumber,
                                                                enSearchFilter.EndRowNumber
            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectForMarketAnalysisTrigger(idr, objENProduct);
                    listOfDATA.Add(objENProduct);
                }
            }

            return listOfDATA;
        }


        public List<enAdminChemicalPrice> RemoveMarketAnalysisTrigger(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfdata = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_RemoveFromMarketAnalysisTrigger]", new object[] {
                                                                   enAdminChemicalPrice.UserId,
                                                                   enAdminChemicalPrice.ID


            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objENProduct);
                    listOfdata.Add(objENProduct);
                }
            }

            return listOfdata;
        }

        public List<enAdminChemicalPrice> Getavilability_ProductCountryUnit(enAdminChemicalPrice obj)
        {
            List<enAdminChemicalPrice> data = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_Getavilability_ProductCountryUnit]", new object[] {
                                                            obj.CountryName,
                                                            obj.Product,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        ConstructObjectGetavilability_ProductCountryUnit(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public List<enAdminChemicalPrice> Getavilability_ProductCountryVarient(enAdminChemicalPrice obj)
        {
            List<enAdminChemicalPrice> data = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_Getavilability_ProductCountryProductVariant]", new object[] {
                                                            obj.CountryName,
                                                            obj.Product,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        ConstructObjectGetavilability_ProductCountryProductVariant(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public List<enAdminChemicalPrice> Getavilability_ProductCountryVarientRelation(enAdminChemicalPrice obj)
        {
            List<enAdminChemicalPrice> data = new List<enAdminChemicalPrice>();
            try
            {

                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_Getavilability_ProductCountryProductVariantrelation]", new object[] {
                                                            obj.CountryName,
                                                            obj.Product,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        ConstructObjectGetavilability_ProductCountryProductVariant(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }


        public List<enAdminChemicalPrice> BindALLCountry(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfCountry = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_BindALLCountry]", new object[] {


                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindCountry(idr, objENProduct);
                    listOfCountry.Add(objENProduct);
                }
            }

            return listOfCountry;
        }

        public List<enAdminChemicalPrice> SaveCFRPricingData(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listInsertWrokSpacedetails = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertCFRPricing]", new object[] {

                                                                    enAdminChemicalPrice.CreatedBy ,
                                                                    enAdminChemicalPrice.Product,
                                                                    enAdminChemicalPrice.Country ,
                                                                    enAdminChemicalPrice.Year ,
                                                                    enAdminChemicalPrice.Month ,
                                                                    enAdminChemicalPrice.CountryID ,
                                                                    enAdminChemicalPrice.Count,
                                                                    enAdminChemicalPrice.SourceDesc,
                                                                    enAdminChemicalPrice.Source,
                                                                    enAdminChemicalPrice.Currency,
                                                                    enAdminChemicalPrice.Grade

            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objENProduct);
                    listInsertWrokSpacedetails.Add(objENProduct);
                }
            }

            return listInsertWrokSpacedetails;
        }

        public List<enAdminChemicalPrice> GetAssociateCFRPricebasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listGetFreelPricingDatabasedonFilter = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetAssociateCFRPricebasedonFilter]", new object[] {
                                                                 enAdminChemicalPrice.CreatedBy,
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber,
                                                                 enAdminChemicalPrice.searchKeyword
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetAssociateCFRPricebasedonFilter(idr, objENProduct);
                    listGetFreelPricingDatabasedonFilter.Add(objENProduct);
                }
            }

            return listGetFreelPricingDatabasedonFilter;
        }


        public List<enAdminChemicalPrice> GetConsultantCFRPricebasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listGetFreelPricingDatabasedonFilter = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetConsultantCFRPricebasedonFilter]", new object[] {
                                                                 enAdminChemicalPrice.EmpParentId,
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber,
                                                                 enAdminChemicalPrice.searchKeyword
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetAssociateCFRPricebasedonFilter(idr, objENProduct);
                    listGetFreelPricingDatabasedonFilter.Add(objENProduct);
                }
            }

            return listGetFreelPricingDatabasedonFilter;
        }


        public List<enAdminChemicalPrice> UpdateApprovalStatusCFRPrice()
        {
            List<enAdminChemicalPrice> ApprovalStatus = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateConsultantCFRPrice_Status]", new object[] {
                                                            _enChemicalPricing.Flag,
                                                            _enChemicalPricing.ID,
                                                            _enChemicalPricing.ApprovedBy

                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ApprovalStatus.Add(objenProduct);
                }
            }

            return ApprovalStatus;
        }


        public List<enAdminChemicalPrice> DisApprovalMessageCFRPrice()
        {
            List<enAdminChemicalPrice> ApprovalStatus = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_InsertCFRPriceDisapproved_Message]", new object[] {
                                                            _enChemicalPricing.DisapprovedMessage,
                                                            _enChemicalPricing.ID,
                                                            _enChemicalPricing.DisapprovedBy


                                                           }))



            {
                //    while (idr.Read())
                //    {
                //        enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                //        ConstructObject_DisApprovalMessage(idr, objenProduct);
                //        ApprovalStatus.Add(objenProduct);
                //    }
            }
            return ApprovalStatus;
        }


        public List<enAdminChemicalPrice> BindCFRPricingBasedonProductandcountry(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfdata = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetCFRPriceBasedonProductAndCountry]", new object[] {
                                                                   enAdminChemicalPrice.ProductId,
                                                                   enAdminChemicalPrice.CountryID


            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectForCFRPricing(idr, objENProduct);
                    listOfdata.Add(objENProduct);
                }
            }

            return listOfdata;
        }
        

        public List<enAdminChemicalPrice> BindUserAssignProduct(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAllProductbasedonUser]", new object[] {
                                                                    enAdminChemicalPrice.UserId
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindProduct(idr, objENProduct);
                    listOfProduct.Add(objENProduct);
                }
            }

            return listOfProduct;
        }
        public List<enAdminChemicalPrice> UpdateCFRPricingData(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listInsertWrokSpacedetails = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateCFRPricing]", new object[] {

                                                                    enAdminChemicalPrice.CreatedBy ,
                                                                    enAdminChemicalPrice.Count,
                                                                    enAdminChemicalPrice.SourceDesc,
                                                                    enAdminChemicalPrice.Source,
                                                                    enAdminChemicalPrice.Currency,
                                                                    enAdminChemicalPrice.ID,
                                                                    enAdminChemicalPrice.Grade,
                                                                    enAdminChemicalPrice.ProductName,
                                                                    enAdminChemicalPrice.Country,
                                                                    enAdminChemicalPrice.Year,
                                                                    enAdminChemicalPrice.Month,
                                                                    enAdminChemicalPrice.CFRCountry


        }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectUpdateChemPriceFlag(idr, objENProduct);
                    listInsertWrokSpacedetails.Add(objENProduct);
                }
            }

            return listInsertWrokSpacedetails;
        }
        public List<enAdminChemicalPrice> ReadAllProductExim()
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetListOfProductOfExim]", new object[] {

                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objen = new enAdminChemicalPrice();
                    ConstructObjectProduct(idr, objen);
                    listOfProduct.Add(objen);
                }
            }

            return listOfProduct;
        }
        public List<enAdminChemicalPrice> GetCustomerLoginDetailBasedonSearch(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listCustomerLoginDetailBasedonSearch = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetSearchBasedRecordCustomerLoginDetail]", new object[] {
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber,
                                                                 enAdminChemicalPrice.searchKeyword,
                                                                  enAdminChemicalPrice.FromDate,
                                                                     enAdminChemicalPrice.ToDate
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetCustomerLoginDetailBasedonSearch(idr, objENProduct);
                    listCustomerLoginDetailBasedonSearch.Add(objENProduct);
                }
            }

            return listCustomerLoginDetailBasedonSearch;
        }
        public List<enAdminChemicalPrice> GetCustomerLoginDetailDownloadData(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listCustomerLoginDetailBasedonSearch = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetCustomerLoginDetailDownloadData]", new object[] {
                                                                 enAdminChemicalPrice.searchKeyword,
                                                                 enAdminChemicalPrice.FromDate,
                                                                  enAdminChemicalPrice.ToDate

                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetCustomerLoginDetailDownloadData(idr, objENProduct);
                    listCustomerLoginDetailBasedonSearch.Add(objENProduct);
                }
            }

            return listCustomerLoginDetailBasedonSearch;
        }


        public List<enCFRPrice> BindCFRPricingBasedonProductandcountryApi(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enCFRPrice> listOfdata = new List<enCFRPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetCFRPriceBasedonProductAndCountry]", new object[] {
                                                                   enAdminChemicalPrice.ProductId,
                                                                   enAdminChemicalPrice.CountryID


            }))
            {
                while (idr.Read())
                {
                    enCFRPrice objENProduct = new enCFRPrice();
                    ConstructObjectForCFRPricingApi(idr, objENProduct);
                    listOfdata.Add(objENProduct);
                }
            }

            return listOfdata;
        }

        private void ConstructObjectGetavilability_ProductCountryProductVariant(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enUser_.ProductVariant = DBNull.Value == idr["ProductVariantName"] ? null : Convert.ToString(idr["ProductVariantName"]);

        }

        public List<enAdminChemicalPrice> SaveUploadPriceCatalogData(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listUploadPriceCatalogData = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertChemPriceCatalog]", new object[] {
                                                                 enAdminChemicalPrice.Product,
                                                                 enAdminChemicalPrice.CountryID,
                                                                 enAdminChemicalPrice.Year ,
                                                                 enAdminChemicalPrice.Month ,
                                                                 enAdminChemicalPrice.Tittle ,
                                                                 enAdminChemicalPrice.Source,
                                                                 enAdminChemicalPrice.CreatedBy

                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectResponse(idr, objENProduct);
                    listUploadPriceCatalogData.Add(objENProduct);
                }
            }

            return listUploadPriceCatalogData;
        }


        public List<enPriceCatalog> GetPriceCatalogListbasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enPriceCatalog> listGetPriceCatalogListbasedonFilter = new List<enPriceCatalog>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetPriceCatalogListbasedonFilter]", new object[] {
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber,
                                                                 enAdminChemicalPrice.searchKeyword
                                                            }))
            {
                while (idr.Read())
                {
                    enPriceCatalog objENProduct = new enPriceCatalog();
                    ConstructObjectGetPriceCatalogListbasedonFilter(idr, objENProduct);
                    listGetPriceCatalogListbasedonFilter.Add(objENProduct);
                }
            }

            return listGetPriceCatalogListbasedonFilter;
        }

        public List<enPriceCatalog> GetPriceCatalogBasedonProductandCountry(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enPriceCatalog> listGetPriceCatalogListbasedonFilter = new List<enPriceCatalog>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetPriceCatalogbasedonProductandCountry]", new object[] {
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber,
                                                                 enAdminChemicalPrice.ProductId,
                                                                 enAdminChemicalPrice.CountryID
                                                            }))
            {
                while (idr.Read())
                {
                    enPriceCatalog objENProduct = new enPriceCatalog();
                    ConstructObjectGetPriceCatalogListbasedonFilter(idr, objENProduct);
                    listGetPriceCatalogListbasedonFilter.Add(objENProduct);
                }
            }

            return listGetPriceCatalogListbasedonFilter;
        }


        public List<enPriceCatalog> GetPriceCatalogProductbaseonUser(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enPriceCatalog> listOfProduct = new List<enPriceCatalog>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPriceCatalogProductbaseonUser]", new object[] {

                                                                                 enAdminChemicalPrice.UserId

                                                                                   }))
            {
                while (idr.Read())
                {
                    enPriceCatalog objENProduct = new enPriceCatalog();
                    ConstructObjectBindProductPriceCatalog(idr, objENProduct);
                    listOfProduct.Add(objENProduct);
                }
            }

            return listOfProduct;
        }


        public List<enPriceCatalog> GetPriceCatalogCountryBasedonProduct(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enPriceCatalog> listOfProduct = new List<enPriceCatalog>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPriceCatalogCountryBasedonProduct]", new object[] {

                                                                                 enAdminChemicalPrice.UserId,
                                                                                 enAdminChemicalPrice.ProductId
                                                                                   }))
            {
                while (idr.Read())
                {
                    enPriceCatalog objENProduct = new enPriceCatalog();
                    ConstructObjectBindlistOfCountryPriceCatalog(idr, objENProduct);
                    listOfProduct.Add(objENProduct);
                }
            }

            return listOfProduct;
        }

        public List<enAdminChemicalPrice> PriceCatalogDownloadTrack(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfPriceCatalogDownloadTrack = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertintoSA_PriceCatalogDownloadTracking]", new object[] {
                                                    enAdminChemicalPrice.ProductId,
                                                    enAdminChemicalPrice.CountryID ,
                                                    enAdminChemicalPrice.Year ,
                                                    enAdminChemicalPrice.Month ,
                                                    enAdminChemicalPrice.FileName ,
                                                    enAdminChemicalPrice.UserId
            }))


                return listOfPriceCatalogDownloadTrack;
        }


        public List<Product> GetCreditDownloadProducts(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<Product> listofCreditDownloadProducts = new List<Product>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetCreditDownloadProduct]", new object[] {

                                                    enAdminChemicalPrice.UserId
            }))
            {
                while (idr.Read())
                {
                    Product objENProduct = new Product();
                    ConstructObjectCreditDownloadProducts(idr, objENProduct);
                    listofCreditDownloadProducts.Add(objENProduct);
                }
            }


            return listofCreditDownloadProducts;
        }
        public List<GetCRMDetails> GetCRMByUser(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<GetCRMDetails> NameOfCRM = new List<GetCRMDetails>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetCrmDetails]", new object[] {

                                                    enAdminChemicalPrice.Email
            }))
            {
                while (idr.Read())
                {
                    GetCRMDetails objENProduct = new GetCRMDetails();
                    ConstructObjectGetCRMDetails(idr, objENProduct);
                    NameOfCRM.Add(objENProduct);
                }
            }


            return NameOfCRM;
        }




        public List<enAdminChemicalPrice> SaveInternalPriceQuote()
        {
            List<enAdminChemicalPrice> ObjInsert = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_InsertInternalPriceQuote]", new object[] {
                                                            _enChemicalPricing.Product,
                                                            _enChemicalPricing.Country,
                                                            _enChemicalPricing.SupplierName,
                                                            _enChemicalPricing.SourceDesc,
                                                            _enChemicalPricing.FileName,
                                                            _enChemicalPricing.CreatedBy,

                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enAdminChemicalPrice> GetAssociateInternalPriceQuoteList(enSearchFilter searchFilter)
        {
            List<enAdminChemicalPrice> listOfPricingDataList = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetAssociateInternalPriceQuoteList]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.UserName,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objENChemicalPrice = new enAdminChemicalPrice();
                        ConstructObjectListForInternalPriceQuoteList(idr, objENChemicalPrice);
                        listOfPricingDataList.Add(objENChemicalPrice);
                    }
                }

                return listOfPricingDataList;
            }
            catch (Exception ex)
            {

            }
            return listOfPricingDataList;
        }

        public List<enAdminChemicalPrice> SaveUploadOfferPriceData(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listUploadPriceCatalogData = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertOfferPricing]", new object[] {
                                                                 enAdminChemicalPrice.Product,
                                                                 enAdminChemicalPrice.CountryID,
                                                                 enAdminChemicalPrice.Participants,
                                                                 enAdminChemicalPrice.Type,
                                                                 enAdminChemicalPrice.Date ,
                                                                 enAdminChemicalPrice.PriceIncoterm,
                                                                 enAdminChemicalPrice.Incoterm,
                                                                 enAdminChemicalPrice.Grade ,
                                                                 enAdminChemicalPrice.Qty,
                                                                 enAdminChemicalPrice.ValidityDate,
                                                                 enAdminChemicalPrice.SourceDesc,
                                                                 enAdminChemicalPrice.Source,
                                                                 enAdminChemicalPrice.CreatedBy,
                                                                 enAdminChemicalPrice.CFRCountry
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectResponse(idr, objENProduct);
                    listUploadPriceCatalogData.Add(objENProduct);
                }
            }

            return listUploadPriceCatalogData;
        }

        public List<enAdminChemicalPrice> GetAssociateOfferPricebasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listGetFreelPricingDatabasedonFilter = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetAssociateOfferPricebasedonFilter]", new object[] {
                                                                 enAdminChemicalPrice.CreatedBy,
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber,
                                                                 enAdminChemicalPrice.searchKeyword
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetAssociateOfferPricebasedonFilter(idr, objENProduct);
                    listGetFreelPricingDatabasedonFilter.Add(objENProduct);
                }
            }

            return listGetFreelPricingDatabasedonFilter;
        }

        public List<enAdminChemicalPrice> GetConsultantOfferPricebasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listGetFreelPricingDatabasedonFilter = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetConsultantOfferPricebasedonFilter]", new object[] {
                                                                 enAdminChemicalPrice.EmpParentId,
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber,
                                                                 enAdminChemicalPrice.searchKeyword
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetAssociateOfferPricebasedonFilter(idr, objENProduct);
                    listGetFreelPricingDatabasedonFilter.Add(objENProduct);
                }
            }

            return listGetFreelPricingDatabasedonFilter;
        }

        public List<enAdminChemicalPrice> UpdateApprovalStatusOfferPrice()
        {
            List<enAdminChemicalPrice> ApprovalStatus = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateConsultantOfferPrice_Status]", new object[] {
                                                            _enChemicalPricing.Flag,
                                                            _enChemicalPricing.ID,
                                                            _enChemicalPricing.ApprovedBy

                                                           }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objenProduct = new enAdminChemicalPrice();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ApprovalStatus.Add(objenProduct);
                }
            }

            return ApprovalStatus;
        }

        public List<enAdminChemicalPrice> DisApprovalMessageOfferPrice()
        {
            List<enAdminChemicalPrice> ApprovalStatus = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_InsertOfferPriceDisapproved_Message]", new object[] {
                                                            _enChemicalPricing.DisapprovedMessage,
                                                            _enChemicalPricing.ID,
                                                            _enChemicalPricing.DisapprovedBy


                                                           }))



            {

            }
            return ApprovalStatus;
        }

        public List<OfferPrice> BindOfferPricingBasedonProductandcountry(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<OfferPrice> listOfdata = new List<OfferPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetOfferPriceBasedonProductAndCountry]", new object[] {
                                                                   enAdminChemicalPrice.ProductId,
                                                                   enAdminChemicalPrice.CountryID


            }))
            {
                while (idr.Read())
                {
                    OfferPrice objENProduct = new OfferPrice();
                    ConstructObjectForOfferPricing(idr, objENProduct);
                    listOfdata.Add(objENProduct);
                }
            }

            return listOfdata;
        }

        public List<enAdminChemicalPrice> EditUploadOfferPriceData(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listUploadPriceCatalogData = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateOfferPricing]", new object[] {

                                                                 enAdminChemicalPrice.ID,
                                                                 enAdminChemicalPrice.Date ,
                                                                 enAdminChemicalPrice.PriceIncoterm,
                                                                 enAdminChemicalPrice.Grade ,
                                                                 enAdminChemicalPrice.Qty,
                                                                 enAdminChemicalPrice.ValidityDate,
                                                                 enAdminChemicalPrice.SourceDesc,
                                                                 enAdminChemicalPrice.Source,
                                                                 enAdminChemicalPrice.CreatedBy,
                                                                 enAdminChemicalPrice.Participants ,
                                                                 enAdminChemicalPrice.Type ,
                                                                 enAdminChemicalPrice.Incoterm

        }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectResponse(idr, objENProduct);
                    listUploadPriceCatalogData.Add(objENProduct);
                }
            }

            return listUploadPriceCatalogData;
        }
        public List<enAdminChemicalPrice> GetOfferPricingDetailsById(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listUploadPriceCatalogData = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetOfferPricedetailsbyId]", new object[] {

                                                                 enAdminChemicalPrice.ID


                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetOfferPricingDetailsById(idr, objENProduct);
                    listUploadPriceCatalogData.Add(objENProduct);
                }
            }

            return listUploadPriceCatalogData;
        }


        public List<enAdminChemicalPrice> BindIncotermBasedOnProductandCountry(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfCountry = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_BindVariantBasedonProductandCountry]", new object[] {
                                                               enAdminChemicalPrice.Product,
                                                                enAdminChemicalPrice.CountryID

                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindFreelCountry(idr, objENProduct);
                    listOfCountry.Add(objENProduct);
                }
            }

            return listOfCountry;
        }

        public List<enAdminChemicalPrice> GetAssociateLandingPageListbasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listGetAssociateLandingPageListbasedonFilter = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetAssociateLandingPageListbasedonFilter]", new object[] {
                                                                 enAdminChemicalPrice.CreatedBy,
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber,
                                                                 enAdminChemicalPrice.searchKeyword
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetAssociateLandingPageListbasedonFilter(idr, objENProduct);
                    listGetAssociateLandingPageListbasedonFilter.Add(objENProduct);
                }
            }

            return listGetAssociateLandingPageListbasedonFilter;
        }


        public List<enAdminChemicalPrice> GetConsultantLandingPageListbasedonFilter(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listGetConsultantLandingPageListbasedonFilter = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetConsultantLandingPagebasedonFilter]", new object[] {
                                                                 enAdminChemicalPrice.EmpParentId,
                                                                 enAdminChemicalPrice.StartRowNumber,
                                                                 enAdminChemicalPrice.EndRowNumber,
                                                                 enAdminChemicalPrice.searchKeyword
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectGetAssociateLandingPageListbasedonFilter(idr, objENProduct);
                    listGetConsultantLandingPageListbasedonFilter.Add(objENProduct);
                }
            }

            return listGetConsultantLandingPageListbasedonFilter;
        }
        public List<enAdminChemicalPrice> GETAllConsultantProduct_Commentary(enAdminChemicalPrice enAdminChemicalPrice)
        {
            List<enAdminChemicalPrice> listOfProduct = new List<enAdminChemicalPrice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GETAllConsultantProduct_Commentary]", new object[] {
                                                         enAdminChemicalPrice.UserId
                                                            }))
            {
                while (idr.Read())
                {
                    enAdminChemicalPrice objENProduct = new enAdminChemicalPrice();
                    ConstructObjectBindProduct(idr, objENProduct);
                    listOfProduct.Add(objENProduct);
                }
            }

            return listOfProduct;
        }



        public List<enAdminChemicalPrice> get_Two_months_before_loginDetailscrm(enSearchFilter enSearchFilter)
        {
            List<enAdminChemicalPrice> Two_months_before_loginDetails = new List<enAdminChemicalPrice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Two_months_before_loginDetails_CustomSearch]", new object[] {
                                                                    enSearchFilter.searchKeyword,
                                                                   enSearchFilter.StartRowNumber,
                                                                  enSearchFilter.EndRowNumber,
                                                                  enSearchFilter.UserId
                                                            }))
                {
                    while (idr.Read())
                    {
                        enAdminChemicalPrice objEN = new enAdminChemicalPrice();
                        crmConstructObjectusp_Two_months_before_loginDetails(idr, objEN);
                        Two_months_before_loginDetails.Add(objEN);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Two_months_before_loginDetails;
        }


        //-----------------------------------------------------------------------------------------------------------


        private void crmConstructObjectusp_Two_months_before_loginDetails(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.UserId = DBNull.Value == idr["UserId"] ? 0 : Convert.ToInt32(idr["UserId"]);
            enUser_.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enUser_.FirstName = DBNull.Value == idr["Fname"] ? null : Convert.ToString(idr["Fname"]);
            enUser_.Company = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);

        }
        private void ConstructObjectListForInternalPriceQuoteList(IDataReader idr, enAdminChemicalPrice objENChemicalPrice)
        {

            objENChemicalPrice.ID = Convert.ToInt32(idr["id"]);
            objENChemicalPrice.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            objENChemicalPrice.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            objENChemicalPrice.SupplierName = DBNull.Value == idr["Supplier"] ? null : Convert.ToString(idr["Supplier"]);
            objENChemicalPrice.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
            objENChemicalPrice.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENChemicalPrice.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            objENChemicalPrice.SourceDesc = DBNull.Value == idr["Description"] ? null : Convert.ToString(idr["Description"]);
            objENChemicalPrice.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENChemicalPrice.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);



        }
        private void ConstructObjectGetCRMDetails(IDataReader idr, GetCRMDetails enUser_)
        {
            enUser_.CRMName = DBNull.Value == idr["CRMName"] ? null : Convert.ToString(idr["CRMName"]);
            enUser_.CRMEmail = DBNull.Value == idr["CRMEmail"] ? null : Convert.ToString(idr["CRMEmail"]);
            enUser_.PhoneNo= DBNull.Value == idr["Phone"] ? null : Convert.ToString(idr["Phone"]);
        }


        private void ConstructObjectGetavilability_ProductCountryUnit(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enUser_.Currency = DBNull.Value == idr["CURRENCY"] ? null : Convert.ToString(idr["CURRENCY"]);
            enUser_.UnitName = DBNull.Value == idr["Unit"] ? null : Convert.ToString(idr["Unit"]);
        }

        private void ConstructObjectForAllEmployee(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enUser_.Name = DBNull.Value == idr["Fname"] ? null : Convert.ToString(idr["Fname"]);
        }


        private void ConstructObjectForAppraisaldata(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ID = DBNull.Value == idr["AssociateId"] ? 0 : Convert.ToInt32(idr["AssociateId"]);
            enUser_.Name = DBNull.Value == idr["AssociateName"] ? null : Convert.ToString(idr["AssociateName"]);
            enUser_.Date = idr["Date"] == DBNull.Value ? null : Convert.ToDateTime(idr["Date"]).ToString("yyyy-MM-dd");
            enUser_.Amount = DBNull.Value == idr["Amount"] ? null : Convert.ToString(idr["Amount"]);
            enUser_.IncentiveAmount = DBNull.Value == idr["Incentive"] ? null : Convert.ToString(idr["Incentive"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enUser_.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);

        }



        private void ConstructObjectForTrigger(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enUser_.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enUser_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enUser_.ProductVariant = DBNull.Value == idr["ProductVariantName"] ? null : Convert.ToString(idr["ProductVariantName"]);

            enUser_.FirstName = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            enUser_.TriggeredPrice = DBNull.Value == idr["Triggered"] ? null : Convert.ToString(idr["Triggered"]);
            enUser_.SetPrice = DBNull.Value == idr["SetPrice"] ? null : Convert.ToString(idr["SetPrice"]);
            enUser_.RangeName = DBNull.Value == idr["Range"] ? null : Convert.ToString(idr["Range"]);
            enUser_.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
        }
        private void ConstructObjectForUserTrigger(IDataReader idr, enAdminChemicalPrice enUser_)
        {

            enUser_.UserId = DBNull.Value == idr["UserId"] ? 0 : Convert.ToInt32(idr["UserId"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enUser_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enUser_.RangeName = DBNull.Value == idr["RangeName"] ? null : Convert.ToString(idr["RangeName"]);

            enUser_.ProductVariant = DBNull.Value == idr["ProductVariantName"] ? null : Convert.ToString(idr["ProductVariantName"]);
            enUser_.Min = DBNull.Value == idr["MinValues"] ? 0 : Convert.ToDecimal(idr["MinValues"]);
            enUser_.Max = DBNull.Value == idr["MaxValues"] ? 0 : Convert.ToDecimal(idr["MaxValues"]);
            enUser_.Currency = DBNull.Value == idr["CURRENCY"] ? null : Convert.ToString(idr["CURRENCY"]);

        }
        private void ConstructObjectForUserList(IDataReader idr, enAdminChemicalPrice enUser_)
        {

            enUser_.UserId = DBNull.Value == idr["UserId"] ? 0 : Convert.ToInt32(idr["UserId"]);
           
        }
        private void ConstructObjectMinMax(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {

            // enAdminChemicalPrice_.Min = DBNull.Value == idr["MinPrice"] ? 0 : Convert.ToDecimal(idr["MinPrice"]);
            enAdminChemicalPrice_.CurrentPrice = DBNull.Value == idr["CurrentPrice"] ? null : Convert.ToString(idr["CurrentPrice"]);

        }
        private void ConstructObjectBindProductVariant(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {

            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.ProductVariant = Convert.ToString(idr["ProductVariant"]);

        }
        private void ConstructObjectBindProductTrigger(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.ProductName = Convert.ToString(idr["ProductName"]);
        }
        private void ConstructObjectForProductTrigger(IDataReader idr, enAdminChemicalPrice enUser_)
        {

            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            enUser_.ID = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);

            enUser_.Min = DBNull.Value == idr["minvalues"] ? 0 : Convert.ToDecimal(idr["minvalues"]);
            enUser_.Max = DBNull.Value == idr["maxvalues"] ? 0 : Convert.ToDecimal(idr["maxvalues"]);

            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enUser_.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enUser_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enUser_.RangeName = DBNull.Value == idr["RangeId"] ? null : Convert.ToString(idr["RangeId"]);
            enUser_.ProductVariant = DBNull.Value == idr["ProductVariantName"] ? null : Convert.ToString(idr["ProductVariantName"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enUser_.TriggerStatus = DBNull.Value == idr["TriggerStatus"] ? null : Convert.ToString(idr["TriggerStatus"]);
            enUser_.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);


        }
        private void ConstructObjectBindlistOfCountry(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enUser_.CountryName = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
        }
        private void ConstructObjectForUnitList(IDataReader idr, enAdminChemicalPrice objEN)
        {
            objEN.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objEN.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objEN.ID = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objEN.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            objEN.UnitName = DBNull.Value == idr["UnitName"] ? null : Convert.ToString(idr["UnitName"]);
            objEN.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objEN.Country = DBNull.Value == idr["countryId"] ? null : Convert.ToString(idr["countryId"]);
            objEN.Product = DBNull.Value == idr["ProductId"] ? 0 : Convert.ToInt32(idr["ProductId"]);
        }
        private void ConstructObjectForCurrencyDetails(IDataReader idr, enAdminChemicalPrice enUser_)
        {

            enUser_.Weekly = DBNull.Value == idr["Weekly"] ? null : Convert.ToString(idr["Weekly"]);
            enUser_.Monthly = DBNull.Value == idr["Monthly"] ? null : Convert.ToString(idr["Monthly"]);
            enUser_.Yearly = DBNull.Value == idr["Yearly"] ? null : Convert.ToString(idr["Yearly"]);
            enUser_.DailyBulk = DBNull.Value == idr["DailyBulk"] ? null : Convert.ToString(idr["DailyBulk"]);
            enUser_.DailyRetail = DBNull.Value == idr["DailyRetail"] ? null : Convert.ToString(idr["DailyRetail"]);
            enUser_.ForCast = DBNull.Value == idr["ForCast"] ? null : Convert.ToString(idr["ForCast"]);



        }
        ///--------------------------------------------------------------------------------------
        private void ConstructObjectFeedstock(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ID = Convert.ToInt32(idr["Id"]);
            enUser_.ParentProductName = DBNull.Value == idr["ParentProductName"] ? null : Convert.ToString(idr["ParentProductName"]);
            enUser_.ProductChildName = DBNull.Value == idr["ProductChildName"] ? null : Convert.ToString(idr["ProductChildName"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
        }


        //private void ConstructObjectBindProduct(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        //{

        //    enAdminChemicalPrice_.ProductName = Convert.ToString(idr["ProductName"]);

        //}
        private void ConstructObjectResponse(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {

            enAdminChemicalPrice_.ValidationMessage = Convert.ToString(idr["Message"]);

        }

        private void ConstructObjectGetAllProductFinderDetails(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {

            enAdminChemicalPrice_.ProductName = Convert.ToString(idr["ProductName"]);
            enAdminChemicalPrice_.CategoryName = Convert.ToString(idr["CategoryName"]);
            enAdminChemicalPrice_.ProductChildName = Convert.ToString(idr["ChildProductNames"]);
        }


        private void ConstructObjectForEximYear(IDataReader idr, enAdminChemicalPrice enobj_)
        {
            enobj_.Year = DBNull.Value == idr["year"] ? null : Convert.ToString(idr["year"]);
        }

        private void ConstructObjectForEximdata(IDataReader idr, enAdminChemicalPrice enobj_)
        {
            enobj_.Year = DBNull.Value == idr["year"] ? null : Convert.ToString(idr["year"]);
            enobj_.hsCode = DBNull.Value == idr["HSCode"] ? null : Convert.ToString(idr["HSCode"]);
            enobj_.Grade = DBNull.Value == idr["Grade"] ? null : Convert.ToString(idr["Grade"]);
            enobj_.ExporterName = DBNull.Value == idr["ExporterName"] ? null : Convert.ToString(idr["ExporterName"]);
            enobj_.ExportCountry = DBNull.Value == idr["ExportingCountry"] ? null : Convert.ToString(idr["ExportingCountry"]);
            enobj_.PortOfOrigin = DBNull.Value == idr["PortOfOrigin"] ? null : Convert.ToString(idr["PortOfOrigin"]);
            enobj_.ConsigneeName = DBNull.Value == idr["ConsigneeName"] ? null : Convert.ToString(idr["ConsigneeName"]);
            enobj_.PortOfDestination = DBNull.Value == idr["PortOfDestination"] ? null : Convert.ToString(idr["PortOfDestination"]);
            enobj_.CountryofImport = DBNull.Value == idr["CountryofImport"] ? null : Convert.ToString(idr["CountryofImport"]);
            enobj_.Incoterms = DBNull.Value == idr["Incoterms"] ? null : Convert.ToString(idr["Incoterms"]);
            enobj_.ShipmentMode = DBNull.Value == idr["ShipmentMode"] ? null : Convert.ToString(idr["ShipmentMode"]);
        }
        private void ConstructObjectForEximBindHSCode(IDataReader idr, enAdminChemicalPrice enobj_)
        {
            enobj_.hsCode = DBNull.Value == idr["HSCode"] ? null : Convert.ToString(idr["HSCode"]);
        }
        private void ConstructObjectForEximBindGrade(IDataReader idr, enAdminChemicalPrice enobj_)
        {
            enobj_.Grade = DBNull.Value == idr["Grade"] ? null : Convert.ToString(idr["Grade"]);
        }

        private void ConstructObjectForEximBindExportername(IDataReader idr, enAdminChemicalPrice enobj_)
        {
            enobj_.ExporterName = DBNull.Value == idr["ExporterName"] ? null : Convert.ToString(idr["ExporterName"]);
        }

        private void ConstructObjectForEximBindExporterCountry(IDataReader idr, enAdminChemicalPrice enobj_)
        {
            enobj_.ExportCountry = DBNull.Value == idr["ExportingCountry"] ? null : Convert.ToString(idr["ExportingCountry"]);
        }

        private void ConstructObjectForEximBindPortofOrgine(IDataReader idr, enAdminChemicalPrice enobj_)
        {
            enobj_.PortOfOrigin = DBNull.Value == idr["PortOfOrigin"] ? null : Convert.ToString(idr["PortOfOrigin"]);
        }

        private void ConstructObjectForEximBindConsigneeName(IDataReader idr, enAdminChemicalPrice enobj_)
        {
            enobj_.ConsigneeName = DBNull.Value == idr["ConsigneeName"] ? null : Convert.ToString(idr["ConsigneeName"]);
        }

        private void ConstructObjectForEximBindCountryOfImport(IDataReader idr, enAdminChemicalPrice enobj_)
        {
            enobj_.CountryofImport = DBNull.Value == idr["CountryofImport"] ? null : Convert.ToString(idr["CountryofImport"]);
        }

        private void ConstructObjectForEximBindPortOfDestination(IDataReader idr, enAdminChemicalPrice enobj_)
        {
            enobj_.PortOfDestination = DBNull.Value == idr["PortOfDestination"] ? null : Convert.ToString(idr["PortOfDestination"]);
        }

        private void ConstructObjectForEximBindInconterms(IDataReader idr, enAdminChemicalPrice enobj_)
        {
            enobj_.Incoterms = DBNull.Value == idr["Incoterms"] ? null : Convert.ToString(idr["Incoterms"]);
        }

        private void ConstructObjectForEximBindShipmentModes(IDataReader idr, enAdminChemicalPrice enobj_)
        {
            enobj_.ShipmentMode = DBNull.Value == idr["ShipmentMode"] ? null : Convert.ToString(idr["ShipmentMode"]);
        }



        private void ConstructObjectUpdateChemPriceFlag(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
        }

        private void ConstructObjectGetCountryBasedOnRegion(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enUser_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
        }

        private void ConstructObjectGetProductBasedOnCategory(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
        }
        private void ConstructObjectusp_Two_months_before_loginDetails(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.UserId = DBNull.Value == idr["UserId"] ? 0 : Convert.ToInt32(idr["UserId"]);
            enUser_.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enUser_.FirstName = DBNull.Value == idr["Fname"] ? null : Convert.ToString(idr["Fname"]);
        }

        private void ConstructObjectusp_getChemicalPricingList(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.Product = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enUser_.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
            enUser_.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enUser_.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            enUser_.Region = DBNull.Value == idr["Region"] ? null : Convert.ToString(idr["Region"]);
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);

        }

        private void ConstructObjectUSP_Getoverview_list(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            enUser_.Product = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enUser_.Language = DBNull.Value == idr["Language"] ? null : Convert.ToString(idr["Language"]);
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);

        }

        private void ConstructObjectBindCountry(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = Convert.ToInt32(idr["Id"]);
            enAdminChemicalPrice_.CountryName = Convert.ToString(idr["CountryName"]);

        }

        private void ConstructObjectBindFreelCountry(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = Convert.ToInt32(idr["Id"]);
            enAdminChemicalPrice_.CountryName = Convert.ToString(idr["CountryName"]);

        }

        private void ConstructObjectBindProduct(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = Convert.ToInt32(idr["Id"]);
            enAdminChemicalPrice_.ProductName = Convert.ToString(idr["ProductName"]);

        }

        private void ConstructObjectBindFreelVariant(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {

            enAdminChemicalPrice_.ProductVariant = Convert.ToString(idr["ProductVariant"]);

        }


        private void ConstructObjectBindFreelUser(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {

            enAdminChemicalPrice_.Email = Convert.ToString(idr["Email"]);

        }


        private void ConstructObjectGetAllFreelPricing(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.UserId = DBNull.Value == idr["UserId"] ? 0 : Convert.ToInt32(idr["UserId"]);
            enAdminChemicalPrice_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enAdminChemicalPrice_.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
            enAdminChemicalPrice_.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            enAdminChemicalPrice_.RangeName = DBNull.Value == idr["Range"] ? null : Convert.ToString(idr["Range"]);
            enAdminChemicalPrice_.Counts =  DBNull.Value == idr["Price"] ? null : Convert.ToString(idr["Price"]);
            enAdminChemicalPrice_.Source = DBNull.Value == idr["Source"] ? null : Convert.ToString(idr["Source"]);
            enAdminChemicalPrice_.SourceDesc = DBNull.Value == idr["sourceDestination"] ? null : Convert.ToString(idr["sourceDestination"]);
            enAdminChemicalPrice_.Feedback = DBNull.Value == idr["Feedback"] ? null : Convert.ToString(idr["Feedback"]);
            enAdminChemicalPrice_.Rating = DBNull.Value == idr["Rating"] ? 0 : Convert.ToInt32(idr["Rating"]);
            enAdminChemicalPrice_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enAdminChemicalPrice_.RowNumber = Convert.ToInt32(idr["RowNumber"]);
            enAdminChemicalPrice_.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
        }

        private void ConstructObjectGetFreelDataRangebasedonUser(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.UserId = DBNull.Value == idr["UserId"] ? 0 : Convert.ToInt32(idr["UserId"]);
            enAdminChemicalPrice_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enAdminChemicalPrice_.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
            enAdminChemicalPrice_.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            enAdminChemicalPrice_.RangeName = DBNull.Value == idr["Range"] ? null : Convert.ToString(idr["Range"]);
            //enAdminChemicalPrice_.Count = DBNull.Value == idr["Price"] ? 0 : Convert.ToInt32(idr["Price"]);
            //enAdminChemicalPrice_.Source = DBNull.Value == idr["Source"] ? null : Convert.ToString(idr["Source"]);
            //enAdminChemicalPrice_.SourceDesc = DBNull.Value == idr["sourceDestination"] ? null : Convert.ToString(idr["sourceDestination"]);
            //enAdminChemicalPrice_.Feedback = DBNull.Value == idr["Feedback"] ? null : Convert.ToString(idr["Feedback"]);
            //enAdminChemicalPrice_.Rating = DBNull.Value == idr["Rating"] ? 0 : Convert.ToInt32(idr["Rating"]);
            //enAdminChemicalPrice_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
        }
        private void ConstructObjectGetFreelAllPricingForUserBilling(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.UserId = DBNull.Value == idr["UserId"] ? 0 : Convert.ToInt32(idr["UserId"]);
            enAdminChemicalPrice_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enAdminChemicalPrice_.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
            enAdminChemicalPrice_.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            enAdminChemicalPrice_.RangeName = DBNull.Value == idr["Range"] ? null : Convert.ToString(idr["Range"]);
            //enAdminChemicalPrice_.Count = DBNull.Value == idr["Price"] ? 0 : Convert.ToInt32(idr["Price"]);
            enAdminChemicalPrice_.Counts = DBNull.Value == idr["Price"] ? null : Convert.ToString(idr["Price"]);
            enAdminChemicalPrice_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enAdminChemicalPrice_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            enAdminChemicalPrice_.RowNumber = Convert.ToInt32(idr["RowNumber"]);
        }

        private void ConstructObjectGetFreelPricingForUserBilling(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.UserId = DBNull.Value == idr["UserId"] ? 0 : Convert.ToInt32(idr["UserId"]);
            enAdminChemicalPrice_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enAdminChemicalPrice_.ProductVariant = DBNull.Value == idr["ProductVariant"] ? null : Convert.ToString(idr["ProductVariant"]);
            enAdminChemicalPrice_.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            enAdminChemicalPrice_.RangeName = DBNull.Value == idr["Range"] ? null : Convert.ToString(idr["Range"]);
            //enAdminChemicalPrice_.Count = DBNull.Value == idr["Price"] ? 0 : Convert.ToInt32(idr["Price"]);
            enAdminChemicalPrice_.Counts = DBNull.Value == idr["Price"] ? null : Convert.ToString(idr["Price"]);
            enAdminChemicalPrice_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enAdminChemicalPrice_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);

        }

        private void ConstructObjectBindUserbasedonSalesPackageSubscription(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.Email = Convert.ToString(idr["Email"]);

        }

        private void ConstructObjectlistofWorkSpace(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.Email = Convert.ToString(idr["Email"]);
            enAdminChemicalPrice_.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
            enAdminChemicalPrice_.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            enAdminChemicalPrice_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enAdminChemicalPrice_.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enAdminChemicalPrice_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enAdminChemicalPrice_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
        }


        private void ConstructObjectlistofWorkSpaceBasedonUser(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.Email = Convert.ToString(idr["Email"]);
            enAdminChemicalPrice_.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
            enAdminChemicalPrice_.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            enAdminChemicalPrice_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enAdminChemicalPrice_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enAdminChemicalPrice_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
        }

        private void ConstructObjectgetIndustrylanding_list(IDataReader idr, enAdminChemicalPrice enUser_)
        {
            //enUser_.Product = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            enUser_.ID = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            //enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enUser_.Tittle = DBNull.Value == idr["RelatedRep"] ? null : Convert.ToString(idr["RelatedRep"]);
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);

        }



        private void ConstructObjectlistBindPlantBasedonProduct(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {

            enAdminChemicalPrice_.Company = Convert.ToString(idr["Company"]);
        }

        private void ConstructObjectForMarketAnalysisTrigger(IDataReader idr, enAdminChemicalPrice enUser_)
        {

            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            enUser_.ID = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enUser_.Plant = DBNull.Value == idr["PlantShutdown"] ? null : Convert.ToString(idr["PlantShutdown"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);

        }
        private void ConstructObjectGetAssociateCFRPricebasedonFilter(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enAdminChemicalPrice_.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            enAdminChemicalPrice_.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            enAdminChemicalPrice_.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            enAdminChemicalPrice_.CFRCountry = DBNull.Value == idr["CFRCountry"] ? null : Convert.ToString(idr["CFRCountry"]);
            enAdminChemicalPrice_.Count = DBNull.Value == idr["Count"] ? 0 : Convert.ToDecimal(idr["Count"]);
            enAdminChemicalPrice_.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
            enAdminChemicalPrice_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enAdminChemicalPrice_.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enAdminChemicalPrice_.Remarks = DBNull.Value == idr["Remarks"] ? null : Convert.ToString(idr["Remarks"]);
            enAdminChemicalPrice_.Source = DBNull.Value == idr["Source"] ? null : Convert.ToString(idr["Source"]);
            enAdminChemicalPrice_.SourceDesc = DBNull.Value == idr["sourceDesc"] ? null : Convert.ToString(idr["sourceDesc"]);
            enAdminChemicalPrice_.ApprovedBy = DBNull.Value == idr["ApprovedBy"] ? null : Convert.ToString(idr["ApprovedBy"]);
            enAdminChemicalPrice_.ApprovalDate = DBNull.Value == idr["ApprovalDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ApprovalDate"]);
            enAdminChemicalPrice_.Flag = DBNull.Value == idr["Flag"] ? 0 : Convert.ToInt32(idr["Flag"]);
            enAdminChemicalPrice_.Grade = DBNull.Value == idr["Grade"] ? null : Convert.ToString(idr["Grade"]);
            enAdminChemicalPrice_.RowNumber = Convert.ToInt32(idr["RowNumber"]);
            enAdminChemicalPrice_.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
        }
        private void ConstructObjectForCFRPricing(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            enAdminChemicalPrice_.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            enAdminChemicalPrice_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enAdminChemicalPrice_.Counts = DBNull.Value == idr["Price"] ? null : Convert.ToString(idr["Price"]);
            enAdminChemicalPrice_.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
            enAdminChemicalPrice_.Grade = DBNull.Value == idr["Grade"] ? null : Convert.ToString(idr["Grade"]);
        }
        private void ConstructObjectForCFRPricingApi(IDataReader idr, enCFRPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            enAdminChemicalPrice_.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            enAdminChemicalPrice_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enAdminChemicalPrice_.Counts = DBNull.Value == idr["Price"] ? null : Convert.ToString(idr["Price"]);
            enAdminChemicalPrice_.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
            enAdminChemicalPrice_.Grade = DBNull.Value == idr["Grade"] ? null : Convert.ToString(idr["Grade"]);
        }
        

        private void ConstructObjectGetCustomerLoginDetailBasedonSearch(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            enAdminChemicalPrice_.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enAdminChemicalPrice_.Company = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enAdminChemicalPrice_.Devicetype = DBNull.Value == idr["Devicetype"] ? null : Convert.ToString(idr["Devicetype"]);
            enAdminChemicalPrice_.LoginTime = DBNull.Value == idr["Login"] ? (DateTime?)null : Convert.ToDateTime(idr["Login"]);
            enAdminChemicalPrice_.LogoutTime = DBNull.Value == idr["Logout"] ? (DateTime?)null : Convert.ToDateTime(idr["Logout"]);
            enAdminChemicalPrice_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enAdminChemicalPrice_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
        }


        private void ConstructObjectGetCustomerLoginDetailDownloadData(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            enAdminChemicalPrice_.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enAdminChemicalPrice_.Company = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enAdminChemicalPrice_.Devicetype = DBNull.Value == idr["Devicetype"] ? null : Convert.ToString(idr["Devicetype"]);
            enAdminChemicalPrice_.LoginTime = DBNull.Value == idr["Login"] ? (DateTime?)null : Convert.ToDateTime(idr["Login"]);
            enAdminChemicalPrice_.LogoutTime = DBNull.Value == idr["Logout"] ? (DateTime?)null : Convert.ToDateTime(idr["Logout"]);

        }

        private void ConstructObjectGetPriceCatalogListbasedonFilter(IDataReader idr, enPriceCatalog enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enAdminChemicalPrice_.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            enAdminChemicalPrice_.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            enAdminChemicalPrice_.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            enAdminChemicalPrice_.Tittle = DBNull.Value == idr["Tittle"] ? null : Convert.ToString(idr["Tittle"]);
            enAdminChemicalPrice_.FileName = DBNull.Value == idr["Filename"] ? null : Convert.ToString(idr["Filename"]);
            enAdminChemicalPrice_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enAdminChemicalPrice_.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enAdminChemicalPrice_.ProductId = DBNull.Value == idr["ProductId"] ? 0 : Convert.ToInt32(idr["ProductId"]);
            enAdminChemicalPrice_.CountryID = DBNull.Value == idr["CountryID"] ? 0 : Convert.ToInt32(idr["CountryID"]);
            enAdminChemicalPrice_.RowNumber = Convert.ToInt32(idr["RowNumber"]);
            enAdminChemicalPrice_.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
        }

        private void ConstructObjectBindProductPriceCatalog(IDataReader idr, enPriceCatalog enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = Convert.ToInt32(idr["Id"]);
            enAdminChemicalPrice_.ProductName = Convert.ToString(idr["ProductName"]);

        }

        private void ConstructObjectBindlistOfCountryPriceCatalog(IDataReader idr, enPriceCatalog enUser_)
        {
            enUser_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enUser_.CountryName = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
        }


        private void ConstructObjectCreditDownloadProducts(IDataReader idr, Product enUser_)
        {
            enUser_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
        }

        private void ConstructObjectGetAssociateOfferPricebasedonFilter(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enAdminChemicalPrice_.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            enAdminChemicalPrice_.Date = DBNull.Value == idr["Date"] ? null : Convert.ToString(idr["Date"]);
            enAdminChemicalPrice_.PriceIncoterm = DBNull.Value == idr["PriceIncoterm"] ? null : Convert.ToString(idr["PriceIncoterm"]);
            enAdminChemicalPrice_.Grade = DBNull.Value == idr["Grade"] ? null : Convert.ToString(idr["Grade"]);
            enAdminChemicalPrice_.Qty = DBNull.Value == idr["Qty"] ? null : Convert.ToString(idr["Qty"]);
            enAdminChemicalPrice_.ValidityDate = DBNull.Value == idr["ValidityDate"] ? null : Convert.ToString(idr["ValidityDate"]);
            enAdminChemicalPrice_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enAdminChemicalPrice_.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enAdminChemicalPrice_.Remarks = DBNull.Value == idr["Remarks"] ? null : Convert.ToString(idr["Remarks"]);
            enAdminChemicalPrice_.Source = DBNull.Value == idr["Source"] ? null : Convert.ToString(idr["Source"]);
            enAdminChemicalPrice_.SourceDesc = DBNull.Value == idr["sourceDesc"] ? null : Convert.ToString(idr["sourceDesc"]);
            enAdminChemicalPrice_.ApprovalDate = DBNull.Value == idr["ApprovalDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ApprovalDate"]);
            enAdminChemicalPrice_.ApprovedBy = DBNull.Value == idr["ApprovedBy"] ? null : Convert.ToString(idr["ApprovedBy"]);
            enAdminChemicalPrice_.Flag = DBNull.Value == idr["Flag"] ? 0 : Convert.ToInt32(idr["Flag"]);
            enAdminChemicalPrice_.CFRCountry = DBNull.Value == idr["CFRCountry"] ? null : Convert.ToString(idr["CFRCountry"]);
            enAdminChemicalPrice_.Participants = DBNull.Value == idr["Participants"] ? null : Convert.ToString(idr["Participants"]);
            enAdminChemicalPrice_.Type = DBNull.Value == idr["Type"] ? null : Convert.ToString(idr["Type"]);
            enAdminChemicalPrice_.Incoterm = DBNull.Value == idr["Incoterm"] ? null : Convert.ToString(idr["Incoterm"]);
            enAdminChemicalPrice_.RowNumber = Convert.ToInt32(idr["RowNumber"]);
            enAdminChemicalPrice_.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
        }


        private void ConstructObjectForOfferPricing(IDataReader idr, OfferPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enAdminChemicalPrice_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enAdminChemicalPrice_.Date = DBNull.Value == idr["Date"] ? null : Convert.ToString(idr["Date"]);
            enAdminChemicalPrice_.PriceIncoterm = DBNull.Value == idr["PriceIncoterm"] ? null : Convert.ToString(idr["PriceIncoterm"]);
            enAdminChemicalPrice_.Grade = DBNull.Value == idr["Grade"] ? null : Convert.ToString(idr["Grade"]);
            enAdminChemicalPrice_.Qty = DBNull.Value == idr["Qty"] ? null : Convert.ToString(idr["Qty"]);
            enAdminChemicalPrice_.ValidityDate = DBNull.Value == idr["ValidityDate"] ? null : Convert.ToString(idr["ValidityDate"]);
            enAdminChemicalPrice_.CFRCountry = DBNull.Value == idr["CFRCountry"] ? null : Convert.ToString(idr["CFRCountry"]);
            enAdminChemicalPrice_.Participants = DBNull.Value == idr["Participants"] ? null : Convert.ToString(idr["Participants"]);
            enAdminChemicalPrice_.Type = DBNull.Value == idr["Type"] ? null : Convert.ToString(idr["Type"]);
            enAdminChemicalPrice_.Incoterm = DBNull.Value == idr["Incoterm"] ? null : Convert.ToString(idr["Incoterm"]);
        }

        private void ConstructObjectGetOfferPricingDetailsById(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enAdminChemicalPrice_.Country = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enAdminChemicalPrice_.ProductId = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            enAdminChemicalPrice_.CountryID = DBNull.Value == idr["Country"] ? 0 : Convert.ToInt32(idr["Country"]);
            enAdminChemicalPrice_.CollectedDate = DBNull.Value == idr["Date"] ? null : Convert.ToString(idr["Date"]);
            enAdminChemicalPrice_.PriceIncoterm = DBNull.Value == idr["PriceIncoterm"] ? null : Convert.ToString(idr["PriceIncoterm"]);
            enAdminChemicalPrice_.Grade = DBNull.Value == idr["Grade"] ? null : Convert.ToString(idr["Grade"]);
            enAdminChemicalPrice_.Qty = DBNull.Value == idr["Qty"] ? null : Convert.ToString(idr["Qty"]);
            enAdminChemicalPrice_.ValidityDate = DBNull.Value == idr["ValidityDate"] ? null : Convert.ToString(idr["ValidityDate"]);
            enAdminChemicalPrice_.Source = DBNull.Value == idr["Source"] ? null : Convert.ToString(idr["Source"]);
            enAdminChemicalPrice_.SourceDesc = DBNull.Value == idr["sourceDesc"] ? null : Convert.ToString(idr["sourceDesc"]);
            enAdminChemicalPrice_.CFRCountry = DBNull.Value == idr["CFRCountry"] ? null : Convert.ToString(idr["CFRCountry"]);
            enAdminChemicalPrice_.Participants = DBNull.Value == idr["Participants"] ? null : Convert.ToString(idr["Participants"]);
            enAdminChemicalPrice_.Type = DBNull.Value == idr["Type"] ? null : Convert.ToString(idr["Type"]);
            enAdminChemicalPrice_.Incoterm = DBNull.Value == idr["Incoterm"] ? null : Convert.ToString(idr["Incoterm"]);

        }

        private void ConstructObjectGetAssociateLandingPageListbasedonFilter(IDataReader idr, enAdminChemicalPrice enAdminChemicalPrice_)
        {
            enAdminChemicalPrice_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAdminChemicalPrice_.Product = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            enAdminChemicalPrice_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enAdminChemicalPrice_.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            enAdminChemicalPrice_.Quaterly = DBNull.Value == idr["Quaterly"] ? null : Convert.ToString(idr["Quaterly"]);
            enAdminChemicalPrice_.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            enAdminChemicalPrice_.Description = DBNull.Value == idr["Description"] ? null : Convert.ToString(idr["Description"]);
            enAdminChemicalPrice_.Ques1 = DBNull.Value == idr["Ques1"] ? null : Convert.ToString(idr["Ques1"]);
            enAdminChemicalPrice_.Ans1 = DBNull.Value == idr["Ans1"] ? null : Convert.ToString(idr["Ans1"]);
            enAdminChemicalPrice_.Ques2 = DBNull.Value == idr["Ques2"] ? null : Convert.ToString(idr["Ques2"]);
            enAdminChemicalPrice_.Ans2 = DBNull.Value == idr["Ans2"] ? null : Convert.ToString(idr["Ans2"]);
            enAdminChemicalPrice_.Ques3 = DBNull.Value == idr["Ques3"] ? null : Convert.ToString(idr["Ques3"]);
            enAdminChemicalPrice_.Ans3 = DBNull.Value == idr["Ans3"] ? null : Convert.ToString(idr["Ans3"]);
            enAdminChemicalPrice_.Ques4 = DBNull.Value == idr["Ques4"] ? null : Convert.ToString(idr["Ques4"]);
            enAdminChemicalPrice_.Ans4 = DBNull.Value == idr["Ans4"] ? null : Convert.ToString(idr["Ans4"]);
            enAdminChemicalPrice_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enAdminChemicalPrice_.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enAdminChemicalPrice_.ApprovalDate = DBNull.Value == idr["ApprovalDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ApprovalDate"]);
            enAdminChemicalPrice_.ApprovedBy = DBNull.Value == idr["ApprovedBy"] ? null : Convert.ToString(idr["ApprovedBy"]);
            enAdminChemicalPrice_.Flag = DBNull.Value == idr["Flag"] ? 0 : Convert.ToInt32(idr["Flag"]);
            enAdminChemicalPrice_.RowNumber = Convert.ToInt32(idr["RowNumber"]);
            enAdminChemicalPrice_.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
        }


    }
}
