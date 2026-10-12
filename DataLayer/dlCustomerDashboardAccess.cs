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
 public  class dlCustomerDashboardAccess
    {
        private enCustomerDashboardAccess _CustomerDashboardAccess = null;
        public dlCustomerDashboardAccess(enCustomerDashboardAccess enCustomerDashboardAccess_)
        {
            this._CustomerDashboardAccess = enCustomerDashboardAccess_;
        }
        public void ReadBasedOnCustomerDashboardAccess()
        {

            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_CustomerDashboardAccess]", new object[] {
                                                            _CustomerDashboardAccess.UserId,
                                                            _CustomerDashboardAccess.Flag,
                                                            _CustomerDashboardAccess.ProductId,
                                                             DateTime.Now
                                                            });
        }
        
        public List<enCustomerDashboardAccess> ReadBasedOnCustomerDashboardAccessUpdate(enSearchFilter searchFilter)
        {
            List<enCustomerDashboardAccess> listOfExpectedNewCapacity = new List<enCustomerDashboardAccess>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_CustomerDashboardAccess_UPDATE]", new object[] {
                                                            searchFilter.UserId,
                                                            searchFilter.ProductIdBulk
                                                              }))
            {
                while (idr.Read())
                {
                    enCustomerDashboardAccess objenExpectedNewCapacity = new enCustomerDashboardAccess();
                    ConstructObject(idr, objenExpectedNewCapacity);
                    listOfExpectedNewCapacity.Add(objenExpectedNewCapacity);
                }
            }

            return listOfExpectedNewCapacity;
        }

        public List<enCustomerDashboardAccess> ReadAll()
        {
            List<enCustomerDashboardAccess> listOfeCustomerDashboardAccess = new List<enCustomerDashboardAccess>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_CustomerDashboardAccess_READ]", new object[] {
                                                            _CustomerDashboardAccess.UserId
                                                           }))
            {
                while (idr.Read())
                {
                    enCustomerDashboardAccess objenCustomerDashboardAccess = new enCustomerDashboardAccess();
                    ConstructObject(idr, objenCustomerDashboardAccess);
                    listOfeCustomerDashboardAccess.Add(objenCustomerDashboardAccess);
                }
            }

            return listOfeCustomerDashboardAccess;
        }
        public List<enCustomerDashboardAccess> ReadAllProduct()
        {
            List<enCustomerDashboardAccess> listOfeProductDashboardAccess = new List<enCustomerDashboardAccess>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_CustomerProduct_Access]", new object[] {
                                                            _CustomerDashboardAccess.UserId
                                                           }))
            {
                while (idr.Read())
                {
                    enCustomerDashboardAccess objenProductDashboardAccess = new enCustomerDashboardAccess();
                    ConstructObjectForDashBoardProduct(idr, objenProductDashboardAccess);
                    listOfeProductDashboardAccess.Add(objenProductDashboardAccess);
                }
            }

            return listOfeProductDashboardAccess;
        }

        public List<enCustomerDashboardAccess> InsertLogindetails(enSearchFilter obj)
        {
            List<enCustomerDashboardAccess> listOfdata = new List<enCustomerDashboardAccess>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_LoginDetails]", new object[] {
                                                            obj.UserIPAddress,
                                                            obj.Email
                                                           }))
            {
                while (idr.Read())
                {
                    enCustomerDashboardAccess objenProductDashboardAccess = new enCustomerDashboardAccess();
                    ConstructObject(idr, objenProductDashboardAccess);
                    listOfdata.Add(objenProductDashboardAccess);
                }
            }

            return listOfdata;
        }
        public List<enCustomerDashboardAccess> NewsReadAll()
        {
            List<enCustomerDashboardAccess> listOfeCustomerDashboardAccessNews = new List<enCustomerDashboardAccess>();

            using (IDataReader idrNews = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_CustomerDashboardNews_Access]", new object[] {
                                                            _CustomerDashboardAccess.UserId
                                                           }))
            {
                while (idrNews.Read())
                {
                    enCustomerDashboardAccess objenCustomerDashboardAccess = new enCustomerDashboardAccess();
                    ConstructObjectNews(idrNews, objenCustomerDashboardAccess);
                    listOfeCustomerDashboardAccessNews.Add(objenCustomerDashboardAccess);
                }
            }

            return listOfeCustomerDashboardAccessNews;
        }

        private void ConstructObject(IDataReader idr, enCustomerDashboardAccess enCustomerDashboardAccess_)
        {
            enCustomerDashboardAccess_.UserId = Convert.ToInt32(idr["UserId"]);
            enCustomerDashboardAccess_.Flag = Convert.ToInt32(idr["FlagId"]); 
            enCustomerDashboardAccess_.ProductId = Convert.ToInt32(idr["ProductId"]);
            enCustomerDashboardAccess_.Product = Convert.ToInt32(idr["ProductId"]);
            enCustomerDashboardAccess_.ID = Convert.ToInt32(idr["ProductId"]);
            enCustomerDashboardAccess_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
        }
        private void ConstructObjectForDashBoardProduct(IDataReader idr, enCustomerDashboardAccess enCustomerDashboardAccess_)
        {
            enCustomerDashboardAccess_.ID = Convert.ToInt32(idr["ProductId"]);
            enCustomerDashboardAccess_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enCustomerDashboardAccess_.Flag = Convert.ToInt32(idr["FlagId"]);
        }
        
        private void ConstructObjectNews(IDataReader idrNews, enCustomerDashboardAccess enCustomerDashboardAccess_)
        {
            enCustomerDashboardAccess_.ID = Convert.ToInt32(idrNews["SA_NewsId"]);
            enCustomerDashboardAccess_.NewsName = DBNull.Value == idrNews["NewsName"] ? null : Convert.ToString(idrNews["NewsName"]);
            enCustomerDashboardAccess_.NewsDiscription = DBNull.Value == idrNews["NewsDiscription"] ? null : Convert.ToString(idrNews["NewsDiscription"]);
            enCustomerDashboardAccess_.URL = DBNull.Value == idrNews["URL"] ? null : Convert.ToString(idrNews["URL"]);
            enCustomerDashboardAccess_.MetaTitle = DBNull.Value == idrNews["MetaTitle"] ? null : Convert.ToString(idrNews["MetaTitle"]);
            enCustomerDashboardAccess_.MetaDiscription = DBNull.Value == idrNews["MetaDiscription"] ? null : Convert.ToString(idrNews["MetaDiscription"]);
            enCustomerDashboardAccess_.Keywords = DBNull.Value == idrNews["Keywords"] ? null : Convert.ToString(idrNews["Keywords"]);
            enCustomerDashboardAccess_.status = DBNull.Value == idrNews["status"] ? null : Convert.ToString(idrNews["status"]);
            enCustomerDashboardAccess_.CreatedTime = DBNull.Value == idrNews["CreatedTime"] ? (DateTime?)null : Convert.ToDateTime(idrNews["CreatedTime"]);
            enCustomerDashboardAccess_.NewsImg = DBNull.Value == idrNews["NewsImg"] ? null : Convert.ToString(idrNews["NewsImg"]);
        }
        }
}
