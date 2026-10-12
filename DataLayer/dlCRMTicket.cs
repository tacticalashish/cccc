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
  public  class dlCRMTicket
  {

        public string Message = "";
        private enCRMTicket _enCRMTicket = null;

        public dlCRMTicket(enCRMTicket enCRMTicket_)
        {
            this._enCRMTicket = enCRMTicket_;
        }
        public List<enCRMTicket> GetProductList()
        {
            List<enCRMTicket> ObjInsert = new List<enCRMTicket>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetAllProductList_CRMTicket]", new object[] {

                                                           }))
            {
                while (idr.Read())
                {
                    enCRMTicket objenProduct = new enCRMTicket();
                    ConstructObjectForProductList(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }



        public List<enCRMTicket> GetCRMDetails(int id)
        {
            List<enCRMTicket> ObjInsert = new List<enCRMTicket>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetCRMQuearyDetails]", new object[] {
                                                               id
                                                           }))
            {
                while (idr.Read())
                {
                    enCRMTicket objenProduct = new enCRMTicket();
                    ConstructObjectForGetCRMDetails(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }
        public List<enCRMTicket> CRMTicketViewRatingList(enCRMTicket enCRMTicket)
        {
            List<enCRMTicket> ObjInsert = new List<enCRMTicket>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetCRMTicketList]", new object[] {
                                                            enCRMTicket.searchKeyword,
                                                            enCRMTicket.CreatedBy,
                                                            enCRMTicket.StartRowNumber,
                                                            enCRMTicket.EndRowNumber,
                                                            enCRMTicket.FromDate,
                                                            enCRMTicket.ToDate

                                                           }))
            {
                while (idr.Read())
                {
                    enCRMTicket objenProduct = new enCRMTicket();
                    ConstructObjectForGetCRMTicket(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enCRMTicket> CRMTicketViewRatingListFormanager(enCRMTicket enCRMTicket)
        {
            List<enCRMTicket> ObjInsert = new List<enCRMTicket>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetCRMTicketListForManager]", new object[] {
                                                            enCRMTicket.searchKeyword,
                                                            enCRMTicket.UserId,
                                                            enCRMTicket.StartRowNumber,
                                                            enCRMTicket.EndRowNumber,
                                                            enCRMTicket.FromDate,
                                                            enCRMTicket.ToDate,
                                                            enCRMTicket.QuearyType

                                                           }))
            {
                while (idr.Read())
                {
                    enCRMTicket objenProduct = new enCRMTicket();
                    ConstructObjectForGetCRMTicketForManager(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }

        public List<enCRMTicket> insertCRMTicket(enCRMTicket enCRMTicket)
        {
            List<enCRMTicket> ObjInsert = new List<enCRMTicket>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_insertCRMTicket]", new object[] {
                                                          enCRMTicket.ID,
                                                          enCRMTicket.ProductName,
                                                          enCRMTicket.QuearyType,
                                                          enCRMTicket.Queary,
                                                          enCRMTicket.CreatedBy,
                                                          enCRMTicket.CompanyName
                                                           }))
            {
                while (idr.Read())
                {
                    enCRMTicket objenProduct = new enCRMTicket();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }

        private void ConstructObjectForProductList(IDataReader idr, enCRMTicket enUser_)
        {
            enUser_.ID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enUser_.ProductName = DBNull.Value == idr["productname"] ? null : Convert.ToString(idr["productname"]);           

        }

        private void ConstructObjectForInsertEmployeeMaster(IDataReader idr, enCRMTicket enUser_)
        {
            enUser_.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
        }
        private void ConstructObjectForGetCRMDetails(IDataReader idr, enCRMTicket enUser_)
        {
            enUser_.Queary = DBNull.Value == idr["Queary"] ? null : Convert.ToString(idr["Queary"]);
            enUser_.QuearyType = DBNull.Value == idr["QuearyType"] ? null : Convert.ToString(idr["QuearyType"]);
            enUser_.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);


        }

        private void ConstructObjectForGetCRMTicket(IDataReader idr, enCRMTicket enUser_)
        {
            enUser_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enUser_.QuearyType = DBNull.Value == idr["QuearyType"] ? null : Convert.ToString(idr["QuearyType"]);
            enUser_.Queary = DBNull.Value == idr["Queary"] ? null : Convert.ToString(idr["Queary"]);
            enUser_.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enUser_.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);          
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
        }

        private void ConstructObjectForGetCRMTicketForManager(IDataReader idr, enCRMTicket enUser_)
        {
            enUser_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enUser_.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enUser_.QuearyType = DBNull.Value == idr["QuearyType"] ? null : Convert.ToString(idr["QuearyType"]);
            enUser_.Queary = DBNull.Value == idr["Queary"] ? null : Convert.ToString(idr["Queary"]);
            enUser_.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enUser_.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.UserName = DBNull.Value == idr["EmpName"] ? null : Convert.ToString(idr["EmpName"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
        }


    }
}
