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
    public class dlTicket
    {
        private enTicket _enTicket = null;

        public dlTicket(enTicket enTicket_)
        {
            this._enTicket = enTicket_;
        }


        public void Create()
        {
            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_SA_ticket_CREATE]", new object[] {
                                                            _enTicket.UserFID,
                                                            _enTicket.RoleName,
                                                            _enTicket.Query,
                                                            _enTicket.Status,
                                                            _enTicket.FileName,
                                                            DateTime.Now
                                                            });
        }
        public void Read()
        {
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_SA_Ticket_READ]", new object[] {
                                                            _enTicket.ID
                                                            }))
            {
                if (idr.Read())
                {
                    ConstructObject(idr, _enTicket);
                    _enTicket.RowNumber = Convert.ToInt32(idr["RowNumber"]);
                    _enTicket.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
                    _enTicket.CustomerName = Convert.ToString(idr["CustomerName"]);
                    _enTicket.CustomerEmail = Convert.ToString(idr["CustomerEmail"]);

                }
            }
        }

        public void Update()
        {
            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_Ticket_UPDATE]", new object[] {
                                                            _enTicket.ID,
                                                            _enTicket.UserFID,
                                                            _enTicket.RoleName,
                                                            _enTicket.Status,
                                                            _enTicket.FileName,
                                                            DateTime.Now
                                                            });
        }

        public void UpdateNotification()
        {
            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_Ticket_UpdateNotification]", new object[] {
                                                            _enTicket.UserFID,
                                                            _enTicket.RoleName
                                                            });
        }


        public void CloseTicket() 
        {
            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_Ticket_Closed]", new object[] {
                                                            _enTicket.ID,
                                                            _enTicket.ManagerFID,
                                                            _enTicket.Query,
                                                            _enTicket.Status,
                                                            _enTicket.Rating,
                                                            DateTime.Now
                                                            });
        }


        public void UpdateStatus()
        {
            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_Ticket_UpdateStatus]", new object[] {
                                                            _enTicket.ID,
                                                            _enTicket.ManagerFID,
                                                            _enTicket.Query,
                                                            _enTicket.Status,
                                                            _enTicket.RoleName,
                                                            DateTime.Now
                                                            });
        }

        public List<enTicket> ReadBasedOnSearchFilter(enSearchFilter searchFilter)
        {
            List<enTicket> listOfTicket = new List<enTicket>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_SA_Ticket_CustomSearch]", new object[] {
                                                            searchFilter.Customer,
                                                            searchFilter.RoleName,
                                                            searchFilter.Status,
                                                            searchFilter.FromDate,
                                                            searchFilter.ToDate,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber
                                                            }))
            {
                while (idr.Read())
                {
                    enTicket objenTicket = new enTicket();
                    ConstructObject(idr, objenTicket);

                    objenTicket.RowNumber = Convert.ToInt32(idr["RowNumber"]);
                    objenTicket.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
                    objenTicket.CustomerName = Convert.ToString(idr["CustomerName"]);
                    objenTicket.CustomerEmail = Convert.ToString(idr["CustomerEmail"]);
                    objenTicket.IsUserRead = Convert.ToBoolean(idr["IsUserRead"]);
                    objenTicket.IsManagerRead = Convert.ToBoolean(idr["IsManagerRead"]);
                    listOfTicket.Add(objenTicket);
                }
            }

            return listOfTicket;
        }


        private void ConstructObject(IDataReader idr, enTicket enTicket_)
        {
            enTicket_.ID = Convert.ToInt32(idr["ID"]);
            enTicket_.UserFID = Convert.ToInt32(idr["UserFID"]);
            enTicket_.RoleName = Convert.ToString(idr["RoleName"]);
            enTicket_.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            enTicket_.Rating= DBNull.Value == idr["Rating"] ? (int?)null : Convert.ToInt32(idr["Rating"]);
            enTicket_.CreatedDate = Convert.ToDateTime(idr["CreatedDate"]);
            enTicket_.UpdatedDate = Convert.ToDateTime(idr["UpdatedDate"]);
            
        }
    }
}
