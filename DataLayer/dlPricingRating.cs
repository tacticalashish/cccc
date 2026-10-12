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
   public class dlPricingRating
    {
        public string Message = "";
        private enPricingRating _enPricingRating = null;

        public dlPricingRating(enPricingRating enPricingRating_)
        {
            this._enPricingRating = enPricingRating_;
        }

        public List<enPricingRating> GetPricingAssociateName()
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetPricingAssociateName_Rating]", new object[] {                                                          

                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForGetPricingAssociateNamed(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }

        public List<enPricingRating> GetPricingassignAssociateName(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetPricingQCAssignAssociateName_Rating]", new object[] {
                                                          enPricingRating.UserId,
                                                          enPricingRating.Month,
                                                          enPricingRating.Year
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForGetPricingAssociateNamed(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enPricingRating> GetPricingQCName()
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetPricingQCName_Rating]", new object[] {

                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForGetPricingQCName(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }

        public List<enPricingRating> GetQCTimelineRating(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetQCTimelineRating]", new object[] {
                                                        enPricingRating.Month,
                                                         enPricingRating.Year,
                                                         enPricingRating.Id
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }

        public List<enPricingRating> GetRatingDetails(int id)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetRatingDetails_Rating]", new object[] {
                                                               id
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForGetRatingDetails(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enPricingRating> GetPricingLatestTotalRating(int UserId, string Type,string Type2)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[GetPricingLatestTotalRating]", new object[] {
                                                               UserId,
                                                               Type,
                                                               Type2
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForGetTotlaRatingDetail(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }

        public List<enPricingRating> GetManagerViewListForRating(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetManagerViewList_Rating]", new object[] {
                                                          enPricingRating.searchKeyword,
                                                          enPricingRating.UserId,
                                                          enPricingRating.StartRowNumber,
                                                          enPricingRating.EndRowNumber,
                                                          enPricingRating.Year,
                                                          enPricingRating.Month,
                                                          
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForGetManagerViewLis(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enPricingRating> AllEmpRatingListView(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_AllEmpRatingListView_Rating]", new object[] {
                                                          enPricingRating.searchKeyword,
                                                          enPricingRating.UserId,
                                                          enPricingRating.StartRowNumber,
                                                          enPricingRating.EndRowNumber,
                                                          enPricingRating.FromDate,
                                                          enPricingRating.ToDate,
                                                           enPricingRating.Type,
                                                            enPricingRating.Type2,

                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForGetAllRating(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }



        public List<enPricingRating> TLViewAssociate(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_TLViewAssociate_Rating]", new object[] {
                                                          enPricingRating.searchKeyword,
                                                          enPricingRating.UserId,
                                                          enPricingRating.StartRowNumber,
                                                          enPricingRating.EndRowNumber,
                                                          enPricingRating.Year,
                                                          enPricingRating.Month,

                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForGetManagerViewLis(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enPricingRating> GetAssociateViewRatingList(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAssociateViewList_Rating]", new object[] {
                                                          enPricingRating.searchKeyword,
                                                          enPricingRating.UserId,
                                                          enPricingRating.StartRowNumber,
                                                          enPricingRating.EndRowNumber
                                                          
                                                         

                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForGetManagerViewLis(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enPricingRating> GetTLRatingViewRatingList(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetTLRatingListForManager]", new object[] {
                                                          enPricingRating.searchKeyword,
                                                          enPricingRating.UserId,
                                                          enPricingRating.StartRowNumber,
                                                          enPricingRating.EndRowNumber,
                                                          enPricingRating.Year,
                                                          enPricingRating.Month,
                                                          enPricingRating.Type
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForGetTLRatingManager(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enPricingRating> GetTLRatingView(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetTLRatingViewList_Rating]", new object[] {
                                                          enPricingRating.searchKeyword,
                                                          enPricingRating.UserId,
                                                          enPricingRating.StartRowNumber,
                                                          enPricingRating.EndRowNumber                                                      
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForGetTLRatingViewList(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }

        public List<enPricingRating> PendingAssociateQC(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_PendingAssociateQC_Rating]", new object[] {
                                                        enPricingRating.Month,
                                                         enPricingRating.Year,
                                                         enPricingRating.Id,
                                                          enPricingRating.Type
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForGetPricingQCName(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enPricingRating> GetQCSelfRatingList(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetQCSeltRatingList_Rating]", new object[] {
                                                          enPricingRating.searchKeyword,
                                                          enPricingRating.UserId,
                                                          enPricingRating.StartRowNumber,
                                                          enPricingRating.EndRowNumber,
                                                           enPricingRating.Type
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForGetQCselfLRatingViewList(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }



        public List<enPricingRating> insertAssociateRating(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_insertAssociate_Rating]", new object[] {
                                                           enPricingRating.AssociateID,
                                                           enPricingRating.AssociateName,
                                                           enPricingRating.Year,
                                                           enPricingRating.Month,
                                                           enPricingRating.Details,
                                                           enPricingRating.AssociateRating,
                                                           enPricingRating.CreatedBy
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enPricingRating> insertQCRating(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_insertQCPricing_Rating]", new object[] {
                                                           enPricingRating.AssociateID,
                                                           enPricingRating.AssociateName,
                                                           enPricingRating.Year,
                                                           enPricingRating.Month,                                                        
                                                           enPricingRating.QualityRating,
                                                           enPricingRating.TimelineRating,
                                                           enPricingRating.TotalRating,
                                                           enPricingRating.CreatedBy
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enPricingRating> insertQCAssociatePricingRelation(string QCid, string Associateid, string EmployeeYear, string EmployeeMonth,string Createdby)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_insertQCAssocaitePricingRelation_Rating]", new object[] {
                                                         QCid,
                                                         Associateid,
                                                         EmployeeYear,
                                                         EmployeeMonth,
                                                         Createdby
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enPricingRating> InsertManagerRating(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_insertManager_Rating]", new object[] {
                                                           enPricingRating.Id,
                                                           enPricingRating.ProductQualityQuery,
                                                           enPricingRating.ProductQualityQueryFeedback,
                                                           enPricingRating.Discipline,
                                                           enPricingRating.DisciplineFeedback,
                                                           enPricingRating.ProductAddon,
                                                           enPricingRating.ProductAddonFeedback,
                                                           enPricingRating.TotalRating,
                                                           enPricingRating.CreatedBy
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        public List<enPricingRating> InsertTLRating(enPricingRating enPricingRating)
        {
            List<enPricingRating> ObjInsert = new List<enPricingRating>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_insertTL_Rating]", new object[] {
                                                              enPricingRating.Id,
                                                            enPricingRating.Name,
                                                           enPricingRating.TotalRating,
                                                           enPricingRating.Month,
                                                           enPricingRating.Year,
                                                           enPricingRating.CreatedBy,
                                                           enPricingRating.Type,
                                                           enPricingRating.Productivity
                                                           }))
            {
                while (idr.Read())
                {
                    enPricingRating objenProduct = new enPricingRating();
                    ConstructObjectForInsertEmployeeMaster(idr, objenProduct);
                    ObjInsert.Add(objenProduct);
                }
            }

            return ObjInsert;
        }


        private void ConstructObjectForInsertEmployeeMaster(IDataReader idr, enPricingRating enUser_)
        {
            enUser_.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
        }

        private void ConstructObjectForGetPricingQCName(IDataReader idr, enPricingRating enUser_)
        {
            enUser_.AssociateID = DBNull.Value == idr["EmpID"] ? 0 : Convert.ToInt32(idr["EmpID"]);
            enUser_.AssociateName = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            enUser_.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);


        }

        private void ConstructObjectForGetPricingAssociateNamed(IDataReader idr, enPricingRating enUser_)
        {
            enUser_.AssociateID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enUser_.AssociateName = DBNull.Value == idr["Fname"] ? null : Convert.ToString(idr["Fname"]);
            enUser_.AssociateEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enUser_.AssociateRole = DBNull.Value == idr["Role"] ? null : Convert.ToString(idr["Role"]);
            enUser_.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);


        }

        private void ConstructObjectForGetRatingDetails(IDataReader idr, enPricingRating enUser_)
        {
            enUser_.AssociateName = DBNull.Value == idr["AssociateName"] ? null : Convert.ToString(idr["AssociateName"]);
            enUser_.Details = DBNull.Value == idr["Details"] ? null : Convert.ToString(idr["Details"]);           
            enUser_.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);


        }

        private void ConstructObjectForGetTotlaRatingDetail(IDataReader idr, enPricingRating enUser_)
        {
            enUser_.AssociateName = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);  
            enUser_.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            enUser_.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            enUser_.TotalRating = DBNull.Value == idr["TotalRating"] ? null : Convert.ToString(idr["TotalRating"]);


        }

        private void ConstructObjectForGetAllRating(IDataReader idr, enPricingRating enUser_)
        {
            enUser_.AssociateName = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            enUser_.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            enUser_.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            enUser_.TotalRating = DBNull.Value == idr["TotalRating"] ? null : Convert.ToString(idr["TotalRating"]);
            enUser_.AVGRating = DBNull.Value == idr["avgRating"] ? null : Convert.ToString(idr["avgRating"]);
            enUser_.Amount = DBNull.Value == idr["Amount"] ? null : Convert.ToString(idr["Amount"]);
            enUser_.Incentive = DBNull.Value == idr["Incentive"] ? null : Convert.ToString(idr["Incentive"]);
            enUser_.Percent = DBNull.Value == idr["percents"] ? null : Convert.ToString(idr["percents"]);
            enUser_.TotalAmount = DBNull.Value == idr["TotalAmount"] ? null : Convert.ToString(idr["TotalAmount"]);
            enUser_.IncentiveAmount = DBNull.Value == idr["IncentiveAmount"] ? null : Convert.ToString(idr["IncentiveAmount"]);
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);


        }



        private void ConstructObjectForGetManagerViewLis(IDataReader idr, enPricingRating enUser_)
        {
            enUser_.Id = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enUser_.AssociateName = DBNull.Value == idr["AssociateName"] ? null : Convert.ToString(idr["AssociateName"]);
            enUser_.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            enUser_.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            enUser_.Details = DBNull.Value == idr["Details"] ? null : Convert.ToString(idr["Details"]);
            enUser_.AssociateRating = DBNull.Value == idr["AssociateRating"] ? null : Convert.ToString(idr["AssociateRating"]);
            enUser_.ProductQualityQuery = DBNull.Value == idr["ProductQualityQuery"] ? (decimal?)null : Convert.ToDecimal(idr["ProductQualityQuery"]);
            enUser_.ProductQualityQueryFeedback = DBNull.Value == idr["ProductQualityQueryFeedback"] ? null : Convert.ToString(idr["ProductQualityQueryFeedback"]);
            enUser_.Discipline = DBNull.Value == idr["Discipline"] ? (decimal?)null : Convert.ToDecimal(idr["Discipline"]);
            enUser_.DisciplineFeedback = DBNull.Value == idr["DisciplineFeedback"] ? null : Convert.ToString(idr["DisciplineFeedback"]);
            enUser_.ProductAddon = DBNull.Value == idr["ProductAddon"] ? (decimal?)null : Convert.ToDecimal(idr["ProductAddon"]);
            enUser_.ProductAddonFeedback = DBNull.Value == idr["ProductAddonFeedback"] ? null : Convert.ToString(idr["ProductAddonFeedback"]);
            enUser_.TotalRating = DBNull.Value == idr["TotalRating"] ? null : Convert.ToString(idr["TotalRating"]);
            enUser_.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);  
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]); 
           


        }

        private void ConstructObjectForGetTLRatingManager(IDataReader idr, enPricingRating enUser_)
        {
            enUser_.Id = DBNull.Value == idr["TLID"] ? 0 : Convert.ToInt32(idr["TLID"]);
            enUser_.Name = DBNull.Value == idr["TLName"] ? null : Convert.ToString(idr["TLName"]);
            enUser_.TotalRating = DBNull.Value == idr["TotalRating"] ? null : Convert.ToString(idr["TotalRating"]);
            enUser_.AVGRating = DBNull.Value == idr["AVGRating"] ? null : Convert.ToString(idr["AVGRating"]);
            enUser_.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            enUser_.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);       

        }

          private void ConstructObjectForGetTLRatingViewList(IDataReader idr, enPricingRating enUser_)
        {
           
            enUser_.Name = DBNull.Value == idr["TLName"] ? null : Convert.ToString(idr["TLName"]);
            enUser_.TotalRating = DBNull.Value == idr["TotalRating"] ? null : Convert.ToString(idr["TotalRating"]);          
            enUser_.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            enUser_.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
        }


        private void ConstructObjectForGetQCselfLRatingViewList(IDataReader idr, enPricingRating enUser_)
        {

            enUser_.Name = DBNull.Value == idr["QCName"] ? null : Convert.ToString(idr["QCName"]);
            enUser_.TotalRating = DBNull.Value == idr["TotalRating"] ? null : Convert.ToString(idr["TotalRating"]);
            enUser_.Month = DBNull.Value == idr["Month"] ? null : Convert.ToString(idr["Month"]);
            enUser_.Year = DBNull.Value == idr["Year"] ? null : Convert.ToString(idr["Year"]);
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.TimelineRating = DBNull.Value == idr["TimeLine"] ? 0 : Convert.ToInt32(idr["TimeLine"]);
            enUser_.QualityRating = DBNull.Value == idr["Quality"] ? 0 : Convert.ToInt32(idr["Quality"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
        }
    }
}
