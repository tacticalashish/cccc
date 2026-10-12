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
    public class dlCompanyMaster
    {
        private enCompanyMaster _enCompanyMaster = null;

        public dlCompanyMaster(enCompanyMaster enCompanyMaster_)
        {
            this._enCompanyMaster = enCompanyMaster_;
        }

        public void Create()
        {
            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_CompanyMaster_CREATE]", new object[] {
                                                              _enCompanyMaster.CompanyName,
                                                              _enCompanyMaster.FirstContactEmail,
                                                              _enCompanyMaster.FirstContactName,
                                                              _enCompanyMaster.FirstContactNumber,
                                                              _enCompanyMaster.SecondContactEmail,
                                                              _enCompanyMaster.SecondContactName,
                                                              _enCompanyMaster.SecondContactNumber,
                                                              DateTime.Now,
                                                              _enCompanyMaster.Designation,
                                                              _enCompanyMaster.CRM
                                                            });
        }

        public void Update()
        {
            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_CompanyMaster_UPDATE]", new object[] {
                                                             _enCompanyMaster.ID,
                                                             _enCompanyMaster.CompanyName,
                                                              _enCompanyMaster.FirstContactEmail,
                                                              _enCompanyMaster.FirstContactName,
                                                              _enCompanyMaster.FirstContactNumber,
                                                              _enCompanyMaster.SecondContactEmail,
                                                              _enCompanyMaster.SecondContactName,
                                                              _enCompanyMaster.SecondContactNumber,
                                                              DateTime.Now,
                                                               _enCompanyMaster.Designation,
                                                            });
        }



        public List<enCompanyMaster> ReadAll()
        {
            int rowNumber = 1;
            List<enCompanyMaster> listOfSalesPackageSubscriptions = new List<enCompanyMaster>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_CompanyMaster_READ]", new object[] { }))
            {
                while (idr.Read())
                {
                    enCompanyMaster objENSalesPackageSubscriptions = new enCompanyMaster();
                    ConstructObject(idr, objENSalesPackageSubscriptions);
                    objENSalesPackageSubscriptions.RowNumber = rowNumber;
                    listOfSalesPackageSubscriptions.Add(objENSalesPackageSubscriptions);
                    rowNumber++;
                }
            }

            return listOfSalesPackageSubscriptions;
        }



        private void ConstructObject(IDataReader idr, enCompanyMaster enCompanyMaster_)
        {
            enCompanyMaster_.ID = Convert.ToInt32(idr["ID"]);
            enCompanyMaster_.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enCompanyMaster_.FirstContactEmail = DBNull.Value == idr["FirstContactEmail"] ? null : Convert.ToString(idr["FirstContactEmail"]);
            enCompanyMaster_.FirstContactName = DBNull.Value == idr["FirstContactName"] ? null : Convert.ToString(idr["FirstContactName"]);
            enCompanyMaster_.FirstContactNumber = DBNull.Value == idr["FirstContactNumber"] ? null : Convert.ToString(idr["FirstContactNumber"]);
            enCompanyMaster_.SecondContactEmail = DBNull.Value == idr["SecondContactEmail"] ? null : Convert.ToString(idr["SecondContactEmail"]);
            enCompanyMaster_.SecondContactName = DBNull.Value == idr["SecondContactName"] ? null : Convert.ToString(idr["SecondContactName"]);
            enCompanyMaster_.SecondContactNumber = DBNull.Value == idr["SecondContactNumber"] ? null : Convert.ToString(idr["SecondContactNumber"]);
            enCompanyMaster_.CreatedTime = DBNull.Value == idr["CreatedOn"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedOn"]);
            enCompanyMaster_.UpdatedTime = DBNull.Value == idr["UpdatedOn"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdatedOn"]);
        }
    }
}
