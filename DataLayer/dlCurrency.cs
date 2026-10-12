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
    public class dlCurrency
    {
        private enCurrency _enCurrency = null;

        public dlCurrency(enCurrency enCurrency_)
        {
            this._enCurrency = enCurrency_;
        }
        public void Create()
        {
            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_Currency_CREATE]", new object[] {
                                                            _enCurrency.Base,
                                                            _enCurrency.Date,
                                                            _enCurrency.TimeStamp,
                                                            _enCurrency.rates.INR,
                                                            _enCurrency.rates.EUR,
                                                            _enCurrency.rates.KRW,
                                                            _enCurrency.rates.JPY,
                                                            DateTime.Now
                                                            });
        }
        public void CreateINR()
        {
            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_Currency_CREATE_INR]", new object[] {
                                                            _enCurrency.Base,
                                                            _enCurrency.Date,
                                                            _enCurrency.TimeStamp,
                                                            _enCurrency.rates.INR,
                                                            _enCurrency.rates.EUR,
                                                            _enCurrency.rates.KRW,
                                                            _enCurrency.rates.JPY,
                                                            DateTime.Now
                                                            });
        }
        
        public List<enCurrency> ReadBasedOnSearchFilter(enSearchFilter searchFilter)
        {
            List<enCurrency> listOfCurrency = new List<enCurrency>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_SA_Currency_CustomSearch]", new object[] {
                                                            searchFilter.RangeName,
                                                            searchFilter.FromDate,
                                                            searchFilter.ToDate,
                                                            }))
            {
                while (idr.Read())
                {
                    enCurrency objenCurrency = new enCurrency();
                    ConstructObject(idr, objenCurrency);
                    listOfCurrency.Add(objenCurrency);
                }
            }

            return listOfCurrency;
        }
        public List<enCurrency> GetlatestCurrencyrates()
        {
            List<enCurrency> listOfCurrency = new List<enCurrency>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetLatestCurrencyRates]", new object[] {

                                                            }))
            {
                while (idr.Read())
                {
                    enCurrency objenCurrency = new enCurrency();
                    ConstructObject(idr, objenCurrency);
                    listOfCurrency.Add(objenCurrency);
                }
            }
            return listOfCurrency;
        }


        public List<enCurrency> GetCurrencyratesBasedOnfilter(enSearchFilter enSearchFilter)
        {
            List<enCurrency> listOfCurrency = new List<enCurrency>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetCurrencyratesBasedOnfilter]", new object[] {

                                                                             enSearchFilter.StartRowNumber,
                                                                             enSearchFilter.EndRowNumber,
                                                                            enSearchFilter.DateFrom,
                                                                             enSearchFilter.DateTo,

                                                            }))
            {
                while (idr.Read())
                {
                    enCurrency objenCurrency = new enCurrency();
                    ConstructObjectGetCurrencyratesBasedOnfilter(idr, objenCurrency);
                    listOfCurrency.Add(objenCurrency);
                }
            }
            return listOfCurrency;
        }
        public List<enCurrency> getdate()
        {
            List<enCurrency> listOfCurrency = new List<enCurrency>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_Getdate]", new object[] {

                                                            }))
            {
                while (idr.Read())
                {
                    enCurrency objenCurrency = new enCurrency();
                    ConstructObjectdate(idr, objenCurrency);
                    listOfCurrency.Add(objenCurrency);
                }
            }

            return listOfCurrency;
        }

        public List<enCurrency> DownloadCurrencyratesBasedOnfilter(enSearchFilter enSearchFilter)
        {
            List<enCurrency> listOfCurrency = new List<enCurrency>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetDownloadCurrencyratesBasedOnfilter]", new object[] {

                                                                             enSearchFilter.FromDate,
                                                                             enSearchFilter.ToDate

                                                            }))
            {
                while (idr.Read())
                {
                    enCurrency objenCurrency = new enCurrency();
                    ConstructObject(idr, objenCurrency);
                    listOfCurrency.Add(objenCurrency);
                }
            }
            return listOfCurrency;
        }



        private void ConstructObjectdate(IDataReader idr, enCurrency enCurrency_)
        {
            enCurrency_.CreatedDate = Convert.ToDateTime(idr["CreatedDate"]);
        }

        private void ConstructObject(IDataReader idr, enCurrency enCurrency_)
        {
            enCurrency_.ID = Convert.ToInt32(idr["ID"]);
            enCurrency_.Base= DBNull.Value == idr["Base"] ? null : Convert.ToString(idr["Base"]);
            enCurrency_.Date= Convert.ToDateTime(idr["Date"]);
            enCurrency_.TimeStamp= DBNull.Value == idr["TimeStamp"] ? null : Convert.ToString(idr["TimeStamp"]);
            enCurrency_.INR= DBNull.Value == idr["INR"] ? null : Convert.ToString(idr["INR"]);
            enCurrency_.Euro = DBNull.Value == idr["EURO"] ? null : Convert.ToString(idr["EURO"]);
            enCurrency_.KRW = DBNull.Value == idr["Won"] ? null : Convert.ToString(idr["Won"]);
            enCurrency_.YEN = DBNull.Value == idr["Yen"] ? null : Convert.ToString(idr["Yen"]);
            enCurrency_.INREURO = DBNull.Value == idr["INREURO"] ? null : Convert.ToString(idr["INREURO"]);
            enCurrency_.INRWON = DBNull.Value == idr["INRWON"] ? null : Convert.ToString(idr["INRWON"]);
            enCurrency_.INRYEN = DBNull.Value == idr["INRYEN"] ? null : Convert.ToString(idr["INRYEN"]);
            enCurrency_.CreatedDate = Convert.ToDateTime(idr["CreatedDate"]);
        }

        private void ConstructObjectGetCurrencyratesBasedOnfilter(IDataReader idr, enCurrency enCurrency_)
        {
            enCurrency_.ID = Convert.ToInt32(idr["ID"]);
            enCurrency_.Base = DBNull.Value == idr["Base"] ? null : Convert.ToString(idr["Base"]);
            enCurrency_.Date = Convert.ToDateTime(idr["Date"]);
            enCurrency_.TimeStamp = DBNull.Value == idr["TimeStamp"] ? null : Convert.ToString(idr["TimeStamp"]);
            enCurrency_.INR = DBNull.Value == idr["INR"] ? null : Convert.ToString(idr["INR"]);
            enCurrency_.Euro = DBNull.Value == idr["EURO"] ? null : Convert.ToString(idr["EURO"]);
            enCurrency_.KRW = DBNull.Value == idr["Won"] ? null : Convert.ToString(idr["Won"]);
            enCurrency_.YEN = DBNull.Value == idr["Yen"] ? null : Convert.ToString(idr["Yen"]);
            enCurrency_.INREURO = DBNull.Value == idr["INREURO"] ? null : Convert.ToString(idr["INREURO"]);
            enCurrency_.INRWON = DBNull.Value == idr["INRWON"] ? null : Convert.ToString(idr["INRWON"]);
            enCurrency_.INRYEN = DBNull.Value == idr["INRYEN"] ? null : Convert.ToString(idr["INRYEN"]);
            enCurrency_.Createddate = Convert.ToString(idr["CreatedDate"]);
            enCurrency_.RowNumber = Convert.ToInt32(idr["RowNumber"]);
            enCurrency_.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
        }

       
    }
}
