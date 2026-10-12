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
    public class dlExcelLogs
    {
        
        private enExcelLogs _enExcelLogs = null;

        public dlExcelLogs(enExcelLogs enExcelLogs_)
        {
            this._enExcelLogs = enExcelLogs_;
        }


        public List<enExcelLogs> ReportBasedOnSearchFilter(enSearchFilter searchFilter)
        {
            List<enExcelLogs> listOfExcelLogs = new List<enExcelLogs>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Report_ExcelLog_CustomSearch]", new object[] {

                                                            searchFilter.Regions,
                                                            searchFilter.Countries,
                                                            searchFilter.Product,
                                                            searchFilter.ActionType,
                                                            searchFilter.Status,
                                                            searchFilter.Type,
                                                            searchFilter.MarketType,
                                                            searchFilter.FromDate,
                                                            searchFilter.ToDate,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                            }))
            {
                while (idr.Read())
                {
                    enExcelLogs objenExcelLogs = new enExcelLogs();
                    ConstructObject(idr, objenExcelLogs);
                    listOfExcelLogs.Add(objenExcelLogs);
                }
            }

            return listOfExcelLogs;
        }

        public void Create()
        {

            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_ExcelLog_CREATE]", new object[] {
                                                                    _enExcelLogs.RegionName,
                                                                    _enExcelLogs.CountryName,
                                                                    _enExcelLogs.ProductFID,
                                                                    _enExcelLogs.FileName,
                                                                    _enExcelLogs.ActionType,
                                                                    _enExcelLogs.Status,
                                                                    _enExcelLogs.Message,
                                                                    DateTime.Now,
                                                                    _enExcelLogs.CreatedBy,
                                                                    _enExcelLogs.Type,
                                                                    _enExcelLogs.MarketType
                                                                    });


        }

        private void ConstructObject(IDataReader idr, enExcelLogs enExcelLogs_)
        {
            enExcelLogs_.ID = Convert.ToInt32(idr["ID"]);
            enExcelLogs_.RegionName = DBNull.Value == idr["RegionName"] ? null : Convert.ToString(idr["RegionName"]);
            enExcelLogs_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            enExcelLogs_.ProductFID = Convert.ToInt32(idr["ProductFID"]);
            enExcelLogs_.ProductName= DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            enExcelLogs_.FileName = Convert.ToString(idr["FileName"]);
            enExcelLogs_.ActionType = Convert.ToString(idr["ActionType"]);
            enExcelLogs_.Status = Convert.ToString(idr["Status"]);
            enExcelLogs_.Message = Convert.ToString(idr["Message"]);
            enExcelLogs_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enExcelLogs_.CreatedBy = Convert.ToString(idr["CreatedBy"]);
            enExcelLogs_.Type = Convert.ToString(idr["Type"]);
            enExcelLogs_.MarketType = Convert.ToString(idr["MarketType"]);
            enExcelLogs_.RowNumber = Convert.ToInt32(idr["RowNumber"]);
            enExcelLogs_.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);

        }
    }
}
