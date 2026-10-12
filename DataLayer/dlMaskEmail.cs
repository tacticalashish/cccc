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
    public class dlMaskEmail
    {
        private enMaskEmail _enMaskEmail = null;

        public dlMaskEmail(enMaskEmail enMaskEmail_)
        {
            this._enMaskEmail = enMaskEmail_;
        }

        public List<enMaskEmail> ReadBasedOnSearchFilter(enSearchFilter searchFilter)
        {
            List<enMaskEmail> listOfMarketMaskEmail = new List<enMaskEmail>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_MaskEmail_CustomSearch]", new object[] {
                                                            searchFilter.Source,
                                                            searchFilter.IsEmail,
                                                            searchFilter.Name,
                                                            searchFilter.Email,
                                                            searchFilter.ContactNumber,
                                                            searchFilter.CompanyName,
                                                            searchFilter.CountryName,
                                                            searchFilter.StateName,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber
                                                            }))
            {
                while (idr.Read())
                {
                    enMaskEmail objENMaskEmail = new enMaskEmail();
                    ConstructObject(idr, objENMaskEmail);
                    objENMaskEmail.RowNumber = Convert.ToInt32(idr["RowNumber"]);
                    objENMaskEmail.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
                    listOfMarketMaskEmail.Add(objENMaskEmail);
                }
            }

            return listOfMarketMaskEmail;
        }
        public void UnSubscribe()
        {
            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_UnSubscribeClint_UPDATE]", new object[] {
                                                                _enMaskEmail.Email

                                                            });
        }
        public List<enMaskEmail> ReadLeadBasedOnSearchFilter(enSearchFilter searchFilter)
        {
            List<enMaskEmail> listOfMarketMaskEmail1 = new List<enMaskEmail>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_LeadTracker_CustomSearch]", new object[] {
                                                            searchFilter.Source,
                                                            searchFilter.IsEmail,
                                                            searchFilter.Name,
                                                            searchFilter.Email,
                                                            searchFilter.ContactNumber,
                                                            searchFilter.CompanyName,
                                                            searchFilter.CountryName,
                                                            searchFilter.StateName,
                                                            searchFilter.FromDate,
                                                            searchFilter.ToDate,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber
                                                           }))
            {
                while (idr.Read())
                {
                    enMaskEmail objENMaskEmail = new enMaskEmail();
                    ConstructObject(idr, objENMaskEmail);
                    objENMaskEmail.RowNumber = Convert.ToInt32(idr["RowNumber"]);
                    objENMaskEmail.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
                    listOfMarketMaskEmail1.Add(objENMaskEmail);
                }
            }

            return listOfMarketMaskEmail1;
        }
        public void Update()
        {
            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_MaskEmail_UPDATE]", new object[] {
                                                                _enMaskEmail.Email,
                                                                _enMaskEmail.IsChecked
                                                            });
        }


        public void Refresh()
        {
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                                                          "[dbo].[usp_Report_MaskEmailRefresh]", new object[] {}))
            {
                if (idr.Read())
                {
                    _enMaskEmail.ServerResponse = DBNull.Value == idr["ServerResponse"] ? null : Convert.ToString(idr["ServerResponse"]);
                }
            }
        }



        public List<enMaskEmail> getallproduct(string Email)
        {
            List<enMaskEmail> listOfMarketMaskEmail = new List<enMaskEmail>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetallProduct_NewsAndDeals]", new object[] {
                                                          Email
                                                            }))
            {
                while (idr.Read())
                {
                    enMaskEmail objENMaskEmail = new enMaskEmail();
                    ConstructgetproductObject(idr, objENMaskEmail);
                    listOfMarketMaskEmail.Add(objENMaskEmail);
                }
            }

            return listOfMarketMaskEmail;
        }

        public List<enMaskEmail> uploadproductNewsAndDeals(string productlist, string Email)
        {
            List<enMaskEmail> listOfMarketMaskEmail = new List<enMaskEmail>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Insert_Product_NewsAndDeals]", new object[] {
                                                           productlist,
                                                           Email
                                                            }))
            {
                while (idr.Read())
                {
                }
            }

            return listOfMarketMaskEmail;
        }

        private void ConstructgetproductObject(IDataReader idr, enMaskEmail enMaskEmail_)
        {
            enMaskEmail_.ID = Convert.ToInt32(idr["id"]);
            enMaskEmail_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enMaskEmail_.RequestedDate = DBNull.Value == idr["CreatedTime"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedTime"]);
            enMaskEmail_.Flag = DBNull.Value == idr["Flag"] ? (int?)null : Convert.ToInt32(idr["Flag"]);

        }


        private void ConstructObject(IDataReader idr, enMaskEmail enMaskEmail_)
        {
            enMaskEmail_.ID = Convert.ToInt32(idr["ID"]);
            enMaskEmail_.IsEmail = Convert.ToBoolean(idr["IsEmail"]);
            enMaskEmail_.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            enMaskEmail_.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            enMaskEmail_.ContactNumber = DBNull.Value == idr["ContactNumber"] ? null : Convert.ToString(idr["ContactNumber"]);
            enMaskEmail_.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enMaskEmail_.CountryFID = DBNull.Value == idr["CountryFID"] ? (int?)null : Convert.ToInt32(idr["CountryFID"]);
            enMaskEmail_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enMaskEmail_.StateFID = DBNull.Value == idr["StateFID"] ? (int?)null : Convert.ToInt32(idr["StateFID"]);
            enMaskEmail_.StateName = DBNull.Value == idr["StateName"] ? null : Convert.ToString(idr["StateName"]);
            enMaskEmail_.IPAddress = DBNull.Value == idr["IPAddress"] ? null : Convert.ToString(idr["IPAddress"]);
            enMaskEmail_.Message = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
            enMaskEmail_.Source = DBNull.Value == idr["Source"] ? null : Convert.ToString(idr["Source"]);
            enMaskEmail_.RequestedDate = DBNull.Value == idr["RequestedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RequestedDate"]);
            enMaskEmail_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
        }
    }
}
