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
    public class dlProduct
    {
        private enProduct _enProduct = null;

        public dlProduct(enProduct enProduct_)
        {
            this._enProduct = enProduct_;
        }
        

        public List<enProduct> ReportBasedOnSearchFilter(enSearchFilter searchFilter)
        {
            List<enProduct> listOfProduct = new List<enProduct>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Report_Product_CustomSearch]", new object[] {
                                                            
                                                            searchFilter.Regions,
                                                            searchFilter.Countries,
                                                            searchFilter.RangeName,
                                                            searchFilter.FromDate,
                                                            searchFilter.ToDate,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                            }))
            {
                while (idr.Read())
                {
                    enProduct objENProduct = new enProduct();
                    ConstructObject(idr, objENProduct);
                    objENProduct.ProductName= Convert.ToString(idr["ProductName"]);
                    objENProduct.RowNumber = Convert.ToInt32(idr["RowNumber"]);
                    objENProduct.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);

                    listOfProduct.Add(objENProduct);
                }
            }

            return listOfProduct;
        }


        public List<enProduct> SchedulerNewsProduct()
        {
            List<enProduct> listOfProduct = new List<enProduct>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetNewsPendingProduct]", new object[] {


                                                            }))
            {
                while (idr.Read())
                {
                    enProduct objENProduct = new enProduct();
                    ConstructObjectSchedulerNewsProduct(idr, objENProduct);
                    listOfProduct.Add(objENProduct);
                }
            }

            return listOfProduct;
        }




        private void ConstructObject(IDataReader idr, enProduct enProduct_)
        {
            enProduct_.ID = Convert.ToInt32(idr["ID"]);
            enProduct_.RegionFID = DBNull.Value == idr["RegionFID"] ? (int?)null : Convert.ToInt32(idr["RegionFID"]);
            enProduct_.CountryFID = DBNull.Value == idr["CountryFID"] ? (int?)null : Convert.ToInt32(idr["CountryFID"]);
            enProduct_.ProductName = Convert.ToString(idr["ProductName"]);
            enProduct_.ProductDiscription = Convert.ToString(idr["ProductDiscription"]);
            enProduct_.Meta = Convert.ToString(idr["Meta"]);
            enProduct_.MetaDiscription = Convert.ToString(idr["MetaDiscription"]);
            enProduct_.status = DBNull.Value == idr["status"] ? (int?)null : Convert.ToInt32(idr["status"]);
            enProduct_.CreatedTime = DBNull.Value == idr["CreatedTime"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedTime"]);
            enProduct_.ProductImg = Convert.ToString(idr["ProductImg"]);
            enProduct_.Category = DBNull.Value == idr["Category"] ? (int?)null : Convert.ToInt32(idr["Category"]);
            enProduct_.IsSelected = DBNull.Value == idr["IsSelected"] ? (bool?)null : Convert.ToBoolean(idr["IsSelected"]);
        }

        private void ConstructObjectSchedulerNewsProduct(IDataReader idr, enProduct enProduct_)
        {

            enProduct_.ProductName = Convert.ToString(idr["ProductName"]);

        }


    }
}
