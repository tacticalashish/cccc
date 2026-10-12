using EntityLayer;
using EntityLayer.CustomEntity;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataLayer
{
    public class dlLeadManagementSubscription
    {
        public string Message = "";
        private enLeadManagementSubscription _enLeads = null;
        public SqlCommand sqlCmd;
        public SqlConnection con;
        public dlLeadManagementSubscription(enLeadManagementSubscription enLeads_)
        {
            this._enLeads = enLeads_;
        }
        public void connection()
        {

            string constr = ApplicationSettings.DefaultConnectionString; //ConfigurationManager.ConnectionStrings["ChemWinContext"].ToString();
            con = new SqlConnection(constr);
        }
        
       public List<enLeadManagementSubscription> GetListofCRMLeads(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfLeads = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetLeadsofCRM_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForCRMLeads(idr, objENLeads);
                        listOfLeads.Add(objENLeads);
                    }
                }

                return listOfLeads;
            }
            catch (Exception ex)
            {

            }
            return listOfLeads;
        }
        public List<enLeadManagementSubscription> GetSalesPackageSubscriptionsList(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetSubscription_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForsubscription(idr, objENLeads);
                        listOfdata.Add(objENLeads);
                    }
                }

                return listOfdata;
            }
            catch (Exception ex)
            {

            }
            return listOfdata;
        }
        public List<enLeadManagementSubscription> ReadAllClintEmail(enSearchFilter searchFilter)
        {
            searchFilter.ActionType = "AllClintEmail";
            List<enLeadManagementSubscription> listClintEmail = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetDDLRecord]", new object[] {
                                                            searchFilter.ActionType,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForClintEmail(idr, objENLeads);
                        listClintEmail.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listClintEmail;
        }
        public List<enLeadManagementSubscription> GetListForExpireSubscriptionNotification(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetListForExpireSubscriptionNotification]", new object[] {
                                                                searchFilter.UserId,
                                                             searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForsubscriptionAll(idr, objENLeads);
                        listOfdata.Add(objENLeads);
                    }
                }

                return listOfdata;
            }
            catch (Exception ex)
            {

            }
            return listOfdata;
        }

        public List<enLeadManagementSubscription> GetListofLeads(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfLeads = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetLeads_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForLeads(idr, objENLeads);
                        listOfLeads.Add(objENLeads);
                    }
                }

                return listOfLeads;
            }
            catch (Exception ex)
            {

            }
            return listOfLeads;
        }
        public List<enLeadManagementSubscription> UpdateLeadApprovalStatus()
        {
            List<enLeadManagementSubscription> ApprovalStatus = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateStatus_StepCounter]", new object[] {
                                                            _enLeads.Id,
                                                            _enLeads.ApprovedBy,
                                                            _enLeads.Query

                                                           }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenProduct);
                    ApprovalStatus.Add(objenProduct);
                }
            }

            return ApprovalStatus;
        }
        public List<enLeadManagementSubscription> CheckPaymentDetailsStatus()
        {
            List<enLeadManagementSubscription> Status = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_CheckPaymentStatus]", new object[] {
                                                            _enLeads.CorpEmail,

                                                           }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenProduct);
                    Status.Add(objenProduct);
                }
            }

            return Status;
        }


        public List<enLeadManagementSubscription> UpdateLeadDisApprovalStatus()
        {
            List<enLeadManagementSubscription> ApprovalStatus = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateStatus_DisApprovedLead]", new object[] {
                                                             _enLeads.Id,
                                                            _enLeads.createdby,
                                                            _enLeads.Query,

                                                           }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenProduct);
                    ApprovalStatus.Add(objenProduct);
                }
            }

            return ApprovalStatus;
        }
        public List<enLeadManagementSubscription> InsertSubscriptionDetailsbyId()
        {
            List<enLeadManagementSubscription> ApprovalStatus = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertSubscriptionbyLeadId]", new object[] {
                                                             _enLeads.Id,
                                                              _enLeads.CorpEmail,
                                                           }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenProduct);
                    ApprovalStatus.Add(objenProduct);
                }
            }

            return ApprovalStatus;
        }

        public List<enLeadManagementSubscription> InsertPreSalesLeadRecord(enLeadManagementSubscription enObj)
        {
            List<enLeadManagementSubscription> Status = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_InsertPreSalesLeadDetails]", new object[] {
                                                                enObj.Name,
                                                                enObj.CorpEmail,
                                                                enObj.Phone,
                                                                enObj.CompanyName,
                                                                enObj.CountryName,
                                                                enObj.StateName,
                                                                enObj.DiscriptionMessage,
                                                                enObj.OutboundLeadSource,
                                                                enObj.LeadType,
                                                                enObj.createdby,
                                                                enObj.IPAddress,
                                                                enObj.RequestedDate,
                                                                enObj.Mobile,
                                                                enObj.Designation,
                                                                enObj.ContactCountryCode,
                                                                enObj.TelephoneCountryCode

                                              }))

            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenProduct);
                    Status.Add(objenProduct);
                }
            }
            return Status;
        }


        public List<enLeadManagementSubscription> GetPreSalesLeadList(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPreSalesLeadList_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                            searchFilter.UserName,
                                                            searchFilter.DateFrom,
                                                            searchFilter.DateTo,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectGetYourLeadList(idr, objEN, "");
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }


        public List<enLeadManagementSubscription> GetPreSalesListTL(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPreSalesLeadListTL_CustomSearch]", new object[] {
                                                             searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserId,
                                                             searchFilter.DateFrom,
                                                             searchFilter.DateTo,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectGetPresalesManager(idr, objEN, "OpenLead");
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }


        public List<enLeadManagementSubscription> GetPreSalesListManager(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetPreSalesLeadListManager_CustomSearch]", new object[] {
                                                             searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserId,
                                                             searchFilter.DateFrom,
                                                             searchFilter.DateTo,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectGetPresalesManager(idr, objEN, "OpenLead");
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }

        public List<enLeadManagementSubscription> GetRecordByLeadId()
        {
            List<enLeadManagementSubscription> ListLeadDetails = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetRecordByLeadID]", new object[] {
                                                             _enLeads.Id,
                                                            
                                                           }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectLeadList(idr, objenProduct);
                    ListLeadDetails.Add(objenProduct);
                }
            }

            return ListLeadDetails;
        }
        public List<enLeadManagementSubscription> GetRecordBySalesPackageID()
        {
            List<enLeadManagementSubscription> ListSalesPackage = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetRecordOfSalesPackageSubscriptions]", new object[] {
                                                             _enLeads.Id,

                                                           }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectForsalespackageById(idr, objenProduct);
                    ListSalesPackage.Add(objenProduct);
                }
            }

            return ListSalesPackage;
        }
        
       public List<enLeadManagementSubscription> GetCustSubscriptionDetailsbyId()
        {
            List<enLeadManagementSubscription> ListCustDetails = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetSubsDetailsForEmail]", new object[] {
                                                             _enLeads.Id,
                                                             _enLeads.CorpEmail
                                                           }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectCustSubscriptionDetails(idr, objenProduct);
                    ListCustDetails.Add(objenProduct);
                }
            }

            return ListCustDetails;
        }
        public List<enLeadManagementSubscription> GetCustSubscriptionDetailsbyIdOnupdate()
        {
            List<enLeadManagementSubscription> ListCustDetails = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetSubsDetailsForEmailonUpdate]", new object[] {
                                                             _enLeads.Id,
                                                             _enLeads.CorpEmail
                                                           }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectCustSubscriptionDetails(idr, objenProduct);
                    ListCustDetails.Add(objenProduct);
                }
            }

            return ListCustDetails;
        }
        
        public List<enLeadManagementSubscription> CheckExistsUser()
        {
            List<enLeadManagementSubscription> CheckExistsUser = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_CheckExistsUser]", new object[] {
                                                             _enLeads.Id,
                                                           }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenProduct);
                    CheckExistsUser.Add(objenProduct);
                }
            }

            return CheckExistsUser;
        }
        

        public List<enLeadManagementSubscription> DisApprovalMessage()
        {
            List<enLeadManagementSubscription> ApprovalStatus = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_InsertDisapproved_MessageForLead]", new object[] {
                                                            _enLeads.Id,
                                                            _enLeads.DisapprovedRemark,
                                                            _enLeads.createdby,
                                                            _enLeads.Query,
                                                          
                                                           }))



            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenProduct);
                    ApprovalStatus.Add(objenProduct);
                }
            }
            return ApprovalStatus;
        }
        public List<enLeadManagementSubscription> SavePaymentDetails()
        {
            List<enLeadManagementSubscription> Status = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_InsertPaymentDetails]", new object[] {
                                                            _enLeads.CompanyName,
                                                            _enLeads.Invoice,
                                                            _enLeads.Amount,
                                                            _enLeads.PaymentMode,
                                                            _enLeads.PaymentDate,
                                                            _enLeads.CorpEmail,
                                                            _enLeads.Organisation,
                                                            _enLeads.BillingEmail,
                                                            _enLeads.LeadId ,

                                              }))



            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenProduct);
                    Status.Add(objenProduct);
                }
            }
            return Status;
        }

        public List<enLeadManagementSubscription> ReadChemicalPriceRange(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfChemicalPriceRange = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetdropdownforChemicalrange]", new object[] {
                                                            searchFilter.ProductIdBulk,
                                                            searchFilter.CountryName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForChemicalPriceRange(idr, objENLeads);
                        listOfChemicalPriceRange.Add(objENLeads);
                    }
                }

                return listOfChemicalPriceRange;
            }
            catch (Exception ex)
            {

            }
            return listOfChemicalPriceRange;
        }
        public List<enLeadManagementSubscription> ReadLeadDetails(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfLeadDetails = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetLeadDetailsByID]", new object[] {
                                                            searchFilter.LeadId,
                                                            searchFilter.Status
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectLeadListDetails(idr, objENLeads);
                        listOfLeadDetails.Add(objENLeads);
                    }
                }

                return listOfLeadDetails;
            }
            catch (Exception ex)
            {

            }
            return listOfLeadDetails;
        }

        public List<enLeadManagementSubscription> ReadMarketAnalysisRange(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfMarketAnalysisRange = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetdropdownforMarketrange]", new object[] {
                                                            searchFilter.ProductIdBulk,
                                                              searchFilter.Regions,
                                                             searchFilter.CountryName,

                                              }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForMarketanalysisRange(idr, objENLeads);
                        listOfMarketAnalysisRange.Add(objENLeads);
                    }
                }

                return listOfMarketAnalysisRange;
            }
            catch (Exception ex)
            {

            }
            return listOfMarketAnalysisRange;
        }

        public List<enLeadManagementSubscription> ReadClientPrimaryEmailIdRecord(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfrecord = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetUserRecordByid]", new object[] {
                                                            searchFilter.UserFID,
                                                              
                                              }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForClintEmailbyid(idr, objENLeads);
                        listOfrecord.Add(objENLeads);
                    }
                }

                return listOfrecord;
            }
            catch (Exception ex)
            {

            }
            return listOfrecord;
        }
        
        public List<enLeadManagementSubscription> ReadStateDDL(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfStateDDL = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetStateDDL]", new object[] {
                                                            searchFilter.Country,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForState(idr, objENLeads);
                        listOfStateDDL.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listOfStateDDL;
        }
        public List<enLeadManagementSubscription> ReadProduct(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadProduct = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetDDLRecord]", new object[] {
                                                            searchFilter.ActionType,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForProduct(idr, objENLeads);
                        listReadProduct.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadProduct;
        }

        public List<enLeadManagementSubscription> ReadClintEmail(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listClintEmail = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetDDLRecord]", new object[] {
                                                            searchFilter.ActionType,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForClintEmail(idr, objENLeads);
                        listClintEmail.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listClintEmail;
        }

        public List<enLeadManagementSubscription> ReadClintEmailByCompanyId(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listClintEmail = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetSecondaryEmailBycompanyId]", new object[] {
                                                            searchFilter.Id,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForClintEmailByCompnayId(idr, objENLeads);
                        listClintEmail.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listClintEmail;
        }
        
        public List<enLeadManagementSubscription> ReadDataAnalyst(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listofrecord = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetDDLRecord]", new object[] {
                                                            searchFilter.ActionType,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListFordataanalyst(idr, objENLeads);
                        listofrecord.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listofrecord;
        }
        public List<enLeadManagementSubscription> GetprimaryClintRecord(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listofrecord = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetPrimaryClintRecord]", new object[] {
                                                            searchFilter.UserId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectForsalespackageByUserID(idr, objENLeads);
                        listofrecord.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listofrecord;
        }

        public List<enLeadManagementSubscription> GetLeaddetailsByid(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listofrecord = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_Getrecordforinsert]", new object[] {
                                                            searchFilter.LeadId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectForsalespackageByUserID(idr, objENLeads);
                        listofrecord.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listofrecord;
        }
        
        public List<enLeadManagementSubscription> GetListofLeadDetails(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listofrecord = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetLeadRecordByLeadId]", new object[] {
                                                            searchFilter.LeadId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectForClintEmails(idr, objENLeads);
                        listofrecord.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listofrecord;
        }
        
        public List<enLeadManagementSubscription> ReadModule(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listofrecord = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetDDLRecord]", new object[] {
                                                            searchFilter.ActionType,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForModule(idr, objENLeads);
                        listofrecord.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listofrecord;
        }

        
        public List<enLeadManagementSubscription> ReadClintCountry(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listClintCountry = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetDDLRecord]", new object[] {
                                                            searchFilter.ActionType,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForCountry(idr, objENLeads);
                        listClintCountry.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listClintCountry;
        }
        
        public List<enLeadManagementSubscription> ReadClintCompany(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> ReadClintCompany = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetDDLRecord]", new object[] {
                                                            searchFilter.ActionType,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForCompany(idr, objENLeads);
                        ReadClintCompany.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return ReadClintCompany;
        }
        
        public List<enLeadManagementSubscription> ReadLeadDatainGridForOperation(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadLeadDatainGrid = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetLeadDataforOperations]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                            searchFilter.UserId
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectLeadDatainGrid(idr, objENLeads);
                        listReadLeadDatainGrid.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadLeadDatainGrid;
        }

        public List<enLeadManagementSubscription> ReadLeadDatainGridForAccounts(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadLeadDatainGrid = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetLeadDataforAccountsTeams]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                            searchFilter.UserId
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectLeadDatainGrid(idr, objENLeads);
                        listReadLeadDatainGrid.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadLeadDatainGrid;
        }
        public List<enLeadManagementSubscription> ReadSalesPackageSubscriptions(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listSalesPackageSubscriptions = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_ReadSalesPackageSubscriptions]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber                                                      
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectForsalespackage(idr, objENLeads);
                        listSalesPackageSubscriptions.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listSalesPackageSubscriptions;
        }        
        public List<enLeadManagementSubscription> ReadLeadDatainGridForDataAnalyst(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadLeadDatainGrid = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetLeadDataforDataAnalyst]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                            searchFilter.UserId
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectLeadDatainGrid(idr, objENLeads);
                        listReadLeadDatainGrid.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadLeadDatainGrid;
        }     
        public List<enLeadManagementSubscription> ReadLeadDatainGridForProjectManager(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadLeadDatainGrid = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetLeadDataforProjectManager]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                            searchFilter.UserId
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectLeadDatainGrid(idr, objENLeads);
                        listReadLeadDatainGrid.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadLeadDatainGrid;
        }
        public List<enLeadManagementSubscription> ReadLeadDatainGridForSalesHead(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadLeadDatainGrid = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetLeadDataforSalesHead]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                            searchFilter.UserId
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectLeadDatainGrid(idr, objENLeads);
                        listReadLeadDatainGrid.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadLeadDatainGrid;
        }
        public List<enLeadManagementSubscription> DeleteGrid(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> List = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_DeleteLead]", new object[] {
                                                            searchFilter.LeadId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectInsertStatus(idr, objENLeads);
                        List.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return List;
        }
        public List<enLeadManagementSubscription> UpdatestatusofLead(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> List = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateLeadStatus]", new object[] {
                                                            searchFilter.LeadId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectInsertStatus(idr, objENLeads);
                        List.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return List;
        }
        
        //-----------------------------Insert Lead---------------------------------------------------------
        public List<enLeadManagementSubscription> InsertLeadRecord(enLeadManagementSubscription objComm)
        {
            List<enLeadManagementSubscription> Insertdata = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertLeadDetails]", new object[] {
                                             objComm.Name,
                                             objComm.CorpEmail,
                                             objComm.Organisation,
                                             objComm.CountryID,
                                             objComm.StateId,
                                             objComm.Phone,
                                             objComm.module,
                                             objComm.UserType,
                                             objComm.Status,
                                             objComm.IPAddress,
                                             objComm.BillingEmail,
                                             objComm.SubscriptionType,
                                             objComm.AssignAnalystId,
                                             objComm.SecondaryClintEmail,
                                             objComm.SubscriptionDateFrom,
                                             objComm.SubscriptionDateTo,
                                             objComm.Subscription_Country,
                                             objComm.Subscription_Product,
                                             objComm.Chemical_Range,
                                             objComm.Market_Analysis_Range,
                                             objComm.SubSubscriptionRemark,
                                             objComm.createdby,
                                             objComm.Password,
                                             objComm.RegionId,
                                             objComm.PaymentMode,
                                             objComm.files,
                                             objComm.CRM,
                                             objComm.Trader,
                                             objComm.Credit,
                                             objComm.Renew
                                             }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenChemicalPricingWebApi = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenChemicalPricingWebApi);
                    Insertdata.Add(objenChemicalPricingWebApi);
                }
            }

            return Insertdata;
        }
        public List<enLeadManagementSubscription> UpdateInsertLeadRecord(enLeadManagementSubscription objComm)
        {
            List<enLeadManagementSubscription> Insertdata = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateInsertLeadDetails]", new object[] {
                                              objComm.Id,
                                              objComm.Name,
                                             objComm.CorpEmail,
                                             objComm.Organisation,
                                             objComm.CountryID,
                                             objComm.StateId,
                                             objComm.Phone,
                                             objComm.module,
                                             objComm.UserType,
                                             objComm.Status,
                                             objComm.IPAddress,
                                             objComm.BillingEmail,
                                             objComm.SubscriptionType,
                                             objComm.AssignAnalystId,
                                             objComm.SecondaryClintEmail,
                                             objComm.SubscriptionDateFrom,
                                             objComm.SubscriptionDateTo,
                                             objComm.Subscription_Country,
                                             objComm.Subscription_Product,
                                             objComm.Chemical_Range,
                                             objComm.Market_Analysis_Range,
                                             objComm.SubSubscriptionRemark,
                                             objComm.createdby,
                                             objComm.Password,
                                             objComm.RegionId
                                             }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenChemicalPricingWebApi = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenChemicalPricingWebApi);
                    Insertdata.Add(objenChemicalPricingWebApi);
                }
            }

            return Insertdata;
        }
        public List<enLeadManagementSubscription> UpdatePackageRecord(enLeadManagementSubscription objComm)
        {
            List<enLeadManagementSubscription> UpdatePackageRecord = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdatePackageDetails]", new object[] {
                                              objComm.Id,
                                             objComm.SubscriptionDateFrom,
                                             objComm.SubscriptionDateTo,
                                             objComm.Subscription_Product,
                                             objComm.module,
                                             objComm.Chemical_Range,
                                             objComm.Market_Analysis_Range,
                                             }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenChemicalPricingWebApi = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenChemicalPricingWebApi);
                    UpdatePackageRecord.Add(objenChemicalPricingWebApi);
                }
            }

            return UpdatePackageRecord;
        }
        public List<enLeadManagementSubscription> InsertrecordForSalesPackageSubscriptions(enLeadManagementSubscription objComm)
        {
            List<enLeadManagementSubscription> Insertdata = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_Insert_SalesPackageSubscriptions]", new object[] {
                                           //objComm.Subscription_Product,
                                           //objComm.module,
                                           objComm.Id,
                                           objComm.CorpEmail,
                                           objComm.UserId
                                           //objComm.SubscriptionType,
                                           //objComm.SubscriptionDateFrom,
                                           //objComm.SubscriptionDateTo,
                                           //objComm.RegionId,
                                           //objComm.Subscription_Country,
                                           //objComm.Chemical_Range,
                                           //objComm.Market_Analysis_Range
                                             }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenChemicalPricingWebApi = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenChemicalPricingWebApi);
                    Insertdata.Add(objenChemicalPricingWebApi);
                }
            }

            return Insertdata;
        }


        public List<enLeadManagementSubscription> InsertrecordForSalesPackageSubscriptionsWithsecondaryEmail(enLeadManagementSubscription objComm)
        {
            List<enLeadManagementSubscription> Insertdata = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_Insert_SalesPackageSubscriptionsWithsecondaryEmail]", new object[] {
                                           //objComm.Subscription_Product,
                                           //objComm.module,
                                           objComm.Id,
                                           objComm.CorpEmail,
                                           //objComm.SubscriptionType,
                                           //objComm.SubscriptionDateFrom,
                                           //objComm.SubscriptionDateTo,
                                           //objComm.RegionId,
                                           //objComm.Subscription_Country,
                                           //objComm.Chemical_Range,
                                           //objComm.Market_Analysis_Range
                                             }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenChemicalPricingWebApi = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenChemicalPricingWebApi);
                    Insertdata.Add(objenChemicalPricingWebApi);
                }
            }

            return Insertdata;
        }


        public List<enLeadManagementSubscription> InsertrecordinSAcustomer(enLeadManagementSubscription objComm)
        {
            List<enLeadManagementSubscription> Insertdata = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertCustomerRecord]", new object[] {
                                           objComm.Name,
                                           objComm.CorpEmail,
                                           objComm.Phone,
                                            objComm.Designation,
                                           objComm.Password,
                                           objComm.CompanyId,
                                           objComm.chkboxPrimaryId
                                         }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenChemicalPricingWebApi = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenChemicalPricingWebApi);
                    Insertdata.Add(objenChemicalPricingWebApi);
                }
            }

            return Insertdata;
        }

        //-----------------------------------------------------------------------------------
        public DataSet GetAllDropdownData(string ProcName, Dictionary<string, object> param, int timeout = 50)
        {
             DataSet ds = new DataSet();
            try
            {
                connection();
                SqlCommand sqlcomm = new SqlCommand(ProcName, con);
                sqlcomm.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> entry in param)
                {
                    sqlcomm.Parameters.AddWithValue(Convert.ToString(entry.Key), entry.Value);

                }

                sqlcomm.CommandTimeout = timeout;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlcomm;

                da.Fill(ds);
                con.Close();
                sqlcomm.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
        }

        public List<enLeadManagementSubscription> GetAllOrganisation(enLeadManagementSubscription enLeadManagementSubscription)
        {
            List<enLeadManagementSubscription> listofenLeadManagementSubscription = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAllLead_organisation]", new object[] {

                                         }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenLeadManagementSubscription = new enLeadManagementSubscription();
                    ConstructObjectlistofOrganisation(idr, objenLeadManagementSubscription);
                    listofenLeadManagementSubscription.Add(objenLeadManagementSubscription);
                }
            }

            return listofenLeadManagementSubscription;
        }


        public List<enLeadManagementSubscription> GetAllEmailbasedonOrganisation(enLeadManagementSubscription enLeadManagementSubscription)
        {
            List<enLeadManagementSubscription> listofenLeadManagementSubscription = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetAllEmailbasedonOrganisation]", new object[] {
                                                            enLeadManagementSubscription.Organisation

                                         }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenLeadManagementSubscription = new enLeadManagementSubscription();
                    ConstructObjectEmailbasedonOrganisation(idr, objenLeadManagementSubscription);
                    listofenLeadManagementSubscription.Add(objenLeadManagementSubscription);
                }
            }

            return listofenLeadManagementSubscription;
        }


        public List<enLeadManagementSubscription> InsertCustomerCreditScore(enLeadManagementSubscription enLeadManagementSubscription)
        {
            List<enLeadManagementSubscription> listofenLeadManagementSubscription = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertCustomerCreditScore]", new object[] {
                                                            enLeadManagementSubscription.Organisation,
                                                             enLeadManagementSubscription.CorpEmail,
                                                              enLeadManagementSubscription.Credit
                                         }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenLeadManagementSubscription = new enLeadManagementSubscription();
                    ConstructObjectInsertCustomerCreditScore(idr, objenLeadManagementSubscription);
                    listofenLeadManagementSubscription.Add(objenLeadManagementSubscription);
                }
            }

            return listofenLeadManagementSubscription;
        }


        public List<enLeadManagementSubscription> GetCreditBasedonEmail(enLeadManagementSubscription enLeadManagementSubscription)
        {
            List<enLeadManagementSubscription> listofenLeadManagementSubscription = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetCreditBasedonEmail]", new object[] {
                                                            enLeadManagementSubscription.Organisation,
                                                             enLeadManagementSubscription.CorpEmail
                                         }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenLeadManagementSubscription = new enLeadManagementSubscription();
                    ConstructObjectGetCreditBasedonEmail(idr, objenLeadManagementSubscription);
                    listofenLeadManagementSubscription.Add(objenLeadManagementSubscription);
                }
            }

            return listofenLeadManagementSubscription;
        }
        public List<enLeadManagementSubscription> GetCountryByModuleWise(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listClintCountry = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetCountryByRangesWise]", new object[] {
                                                            searchFilter.ActionType,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForCountry(idr, objENLeads);
                        listClintCountry.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listClintCountry;
        }

        public List<enLeadManagementSubscription> ReadProductByCoutryName(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadProduct = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetProductByCountry]", new object[] {
                                                            searchFilter.ActionType,
                                                            searchFilter.Type,
                                                            searchFilter.ModuleName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForProduct(idr, objENLeads);
                        listReadProduct.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadProduct;
        }



        public List<enLeadManagementSubscription> DeSelectAllCheck(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdata = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_DeSelectAllExpiryNotification]", new object[] {
                                                                searchFilter.UserId,


                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListDeSelectAllCheck(idr, objENLeads);
                        listOfdata.Add(objENLeads);
                    }
                }

                return listOfdata;
            }
            catch (Exception ex)
            {

            }
            return listOfdata;
        }

        public List<enLeadManagementSubscription> GetListofCRMCompanyRalation(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfUser = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetCustomerCRMRelation_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForCRMCustomerRelation(idr, objENLeads);
                        listOfUser.Add(objENLeads);
                    }
                }

                return listOfUser;
            }
            catch (Exception ex)
            {

            }
            return listOfUser;
        }
        public List<enLeadManagementSubscription> GetCrmEmailByCustomer(enLeadManagementSubscription obj)
        {
            List<enLeadManagementSubscription> ListCustDetails = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetCrmEmailByCustomerEmail]", new object[] {
                                                             obj.CorpEmail
                                                           }))
            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectCRMEMail(idr, objenProduct);
                    ListCustDetails.Add(objenProduct);
                }
            }

            return ListCustDetails;
        }
        public List<enLeadManagementSubscription> Uploadfile(enLeadManagementSubscription searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_FileUpload]", new object[] {
                                                              searchFilter.files,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectMessage(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }

        public List<enLeadManagementSubscription> GetAssignSalesEmail(enLeadManagementSubscription enObj)
        {
            List<enLeadManagementSubscription> Status = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetSalesPersonEmail]", new object[] {
                                                                enObj.CountryName,
                                                                 enObj.StateName,
                                                    }))

            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectGetAssignPerson(idr, objenProduct);
                    Status.Add(objenProduct);
                }
            }
            return Status;
        }
        //--------------------------------New Module-----------------------------------------------------------------------------------
        public List<enLeadManagementSubscription> InsertOutboundLeadRecord(enLeadManagementSubscription enObj)
        {
            List<enLeadManagementSubscription> Status = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_InsertOutbondLeadDetails]", new object[] {
                                                                enObj.Name,
                                                                enObj.CorpEmail,
                                                                enObj.Phone,
                                                                enObj.CompanyName,
                                                                enObj.CountryName,
                                                                enObj.StateName,
                                                                enObj.DiscriptionMessage,
                                                                enObj.OutboundLeadSource,
                                                                enObj.LeadType,
                                                                enObj.createdby,
                                                                enObj.IPAddress,
                                                                enObj.RequestedDate,
                                                                enObj.Mobile,
                                                                enObj.Designation,
                                                                enObj.ContactCountryCode,
                                                                enObj.TelephoneCountryCode

                                              }))

            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenProduct);
                    Status.Add(objenProduct);
                }
            }
            return Status;
        }
        public List<enLeadManagementSubscription> GetYourLeadList(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetYouLeadList_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                            searchFilter.UserName,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectGetYourLeadListWithMailChain(idr, objEN, "");
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }
        public List<enLeadManagementSubscription> GetYourOpenLeadList(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetYouOpenLeadList_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserName,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectGetYourLeadList(idr, objEN, "OpenLead");
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }
        public List<enLeadManagementSubscription> GetSalesTodaysFollowUP_CustomSearch(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetSalesTodaysFollowUp_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserName,
                                                             searchFilter.DateFrom,
                                                             searchFilter.DateTo
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectGetYourLeadList(idr, objEN, "OpenLead");
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }
        public List<enLeadManagementSubscription> GetCRMTodaysFollowUP_CustomSearch(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetCRMTodaysFollowUp_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserId,
                                                               searchFilter.DateFrom,
                                                                 searchFilter.DateTo
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectGetYourLeadList(idr, objEN, "OpenLead");
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }



        public List<enLeadManagementSubscription> GetSalesTodaysRemainingDate_CustomSearch(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetSalesTodayRemainingDate_CustomSearch]", new object[] {

                                                             searchFilter.UserName,
                                                              searchFilter.DateFrom,
                                                                 searchFilter.DateTo
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectGetYourLeadList(idr, objEN, "OpenLead");
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }
        public List<enLeadManagementSubscription> GetCRMTodaysRemainingDate_CustomSearch(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetCRMTodayRemainingDate_CustomSearch]", new object[] {
                                                             searchFilter.UserName,
                                                             searchFilter.DateFrom,
                                                             searchFilter.DateTo
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectGetYourLeadList(idr, objEN, "OpenLead");
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }


        public List<enLeadManagementSubscription> GetYourCRMOpenLeadList(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetYouOpenLeadListForCRM_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserName,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectGetYourLeadList(idr, objEN, "OpenLead");
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }


        public List<enLeadManagementSubscription> ChangestatusofLead(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> List = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_ChangeMaskMailLeadStatus]", new object[] {
                                                            searchFilter.LeadId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectInsertStatus(idr, objENLeads);
                        List.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return List;
        }

        public List<enLeadManagementSubscription> ChangestatusofLeadCRM(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> List = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_ChangeMaskMailLeadStatusCRM]", new object[] {
                                                            searchFilter.LeadId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectInsertStatus(idr, objENLeads);
                        List.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return List;
        }
        public List<enLeadManagementSubscription> BindOpenLeadDetailsByid(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> List = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_BindOpenLeadDetailsByid]", new object[] {
                                                            searchFilter.Id,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectGetYourLeadListbyidnew(idr, objENLeads, "OpenLead");
                        List.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return List;
        }
        public List<enLeadManagementSubscription> BindCRMOpenLeadDetailsByid(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> List = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_BindCRMOpenLeadDetailsByid]", new object[] {
                                                            searchFilter.Id,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectGetYourCrmLeadListbyidnew(idr, objENLeads, "OpenLead");
                        List.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return List;
        }

        public List<enLeadManagementSubscription> UpdateOpenLeadDetails(enLeadManagementSubscription enObj)
        {
            List<enLeadManagementSubscription> Status = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateopenLeadDetails]", new object[] {
                                                                enObj.SetFollowUp,
                                                                enObj.Amount,
                                                                enObj.ExpectedCloseDate,
                                                                enObj.Currency,
                                                                enObj.Id,
                                                                enObj.files,
                                                                enObj.Status,
                                                                enObj.Invoice,
                                                                enObj.LostReason,
                                                                enObj.PaymentTerms ,
                                                                enObj.PaymentOption,
                                                                enObj.RemainingPaymentDate,
                                                                enObj.DiscriptionMessage,
                                                                enObj.createdby,
                                                                enObj.CountryID,
                                                                enObj.StateId ,
                                                                enObj.CountryName,
                                                                enObj.StateName,
                                                                enObj.Phone,
                                                                enObj.Mobile,
                                                                enObj.Name,
                                                                enObj.CorpEmail,
                                                                enObj.RemainingAmount,
                                                                 enObj.Designation,
                                                                 enObj.Organisation,
                                                                 enObj.ContactCountryCode,
                                                                 enObj.TelephoneCountryCode,
                                                                 enObj.DemoDiscussion,
                                                                 enObj.LastDemoDate


        }))

            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenProduct);
                    Status.Add(objenProduct);
                }
            }
            return Status;
        }
        public List<enLeadManagementSubscription> UpdateCRMOpenLeadDetails(enLeadManagementSubscription enObj)
        {
            List<enLeadManagementSubscription> Status = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertMaskEmailOpenCRM]", new object[] {
                                                                enObj.Id,
                                                                enObj.SetFollowUp,
                                                                enObj.Amount,
                                                                enObj.ExpectedCloseDate,
                                                                enObj.Currency,
                                                                enObj.Status,
                                                                 enObj.Invoice,
                                                                enObj.PaymentOption,
                                                                enObj.RemainingPaymentDate,
                                                                enObj.PaymentTerms ,
                                                                enObj.LostReason,
                                                                enObj.files,
                                                                enObj.Phone,
                                                                enObj.DiscriptionMessage,
                                                                enObj.Organisation,
                                                                enObj.createdby,
                                                                "INSERT",
                                                                enObj.CountryID,
                                                                enObj.StateId ,
                                                                enObj.CountryName,
                                                                enObj.StateName,
                                                                enObj.Mobile,
                                                                enObj.Name,
                                                                enObj.CorpEmail,
                                                                enObj.RemainingAmount,
                                                                enObj.Designation,
                                                                enObj.ContactCountryCode,
                                                                enObj.TelephoneCountryCode,
                                                                enObj.DemoDiscussion,
                                                                enObj.LastDemoDate





        }))

            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenProduct);
                    Status.Add(objenProduct);
                }
            }
            return Status;
        }

        public List<enLeadManagementSubscription> GetYourCRMRenewLeadList(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetYouRenewLeadList_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserName,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectGetYourLeadList(idr, objEN, "OpenLead");
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }
        public List<enLeadManagementSubscription> GetCRMTodaysExpiry_CustomSearch(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_Getsalespackage_expiry_CustomSearch]", new object[] {
                                                              searchFilter.UserId,
                                                              searchFilter.DateFrom,
                                                              searchFilter.DateTo

                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObject(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }

        public List<enLeadManagementSubscription> GetSalesPackageExpiry(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetSubscriptiodatabyFilter]", new object[] {
                                                              searchFilter.searchKeyword,
                                                             searchFilter.StartRowNumber,
                                                             searchFilter.EndRowNumber,
                                                             searchFilter.Type,
                                                             searchFilter.UserId

                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectSalesPackageExpiry(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }
   
        public List<enLeadManagementSubscription> GetFollowupDicussion(enLeadManagementSubscription obj)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetLastFollowUpDiscussion_CustomSearch]", new object[] {
                                                            obj.Id,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectFollowupDicussion(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }

        public List<enLeadManagementSubscription> GetDemoDicussion(enLeadManagementSubscription obj)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetLastDemoDiscussion_CustomSearch]", new object[] {
                                                            obj.Id,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectDEMODicussion(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }

        public List<enLeadManagementSubscription> GetDemoDicussioncrm(enLeadManagementSubscription obj)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetLastDemoDiscussionCRM_CustomSearch]", new object[] {
                                                            obj.Id,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectDEMODicussion(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }


        public List<enLeadManagementSubscription> GetFollowupDicussionCRM(enLeadManagementSubscription obj)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetLastFollowUpDiscussionCRM_CustomSearch]", new object[] {
                                                            obj.Id,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectFollowupDicussion(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }
       
        public List<enLeadManagementSubscription> SaveUserRelation(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadUser = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_SaveUserRelation]", new object[] {
                                                            searchFilter.Email,
                                                            searchFilter.CountryName,
                                                            searchFilter.StateName
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForUser(idr, objENLeads);
                        listReadUser.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadUser;
        }
        public List<enLeadManagementSubscription> ReadUser(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadUser = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetDDLRecord]", new object[] {
                                                            searchFilter.ActionType,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForUser(idr, objENLeads);
                        listReadUser.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadUser;
        }



        public List<enLeadManagementSubscription> getMoreContactDetails(enLeadManagementSubscription obj)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_getMoreLeadContactClientDetails]", new object[] {
                                                            obj.Id,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectgetMoreContactDetails(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }


        public List<enLeadManagementSubscription> SaveLeadContact(int Id, string Name, string Email, string Phone, string Designation, string CreatedBy, string EditID)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_SaveLeadCRMC_ClientDetail]", new object[] {
                                                             Id,
                                                             Name,
                                                             Email,
                                                             Phone,
                                                             Designation,
                                                             CreatedBy,
                                                             EditID
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectMessage(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }


        public List<enLeadManagementSubscription> GetAllOpenLeadListManager(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetAllOpenLeadListManager_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserName,
                                                             searchFilter.DateFrom,
                                                             searchFilter.DateTo,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectGetAllLeadListManager(idr, objEN, "OpenLead");
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }


        public List<enLeadManagementSubscription> ReadAllUserOpenLead()
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_EmployeeMaster_OpenLeadForDropdown]", new object[] {
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectUserDetails(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }



        public List<enLeadManagementSubscription> InsertEmployeeMasterOpenLeadDat(int ChildId, int ParentId)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_InsertEmployeeMaster_OpenLead]", new object[] {
                                                           ChildId,
                                                           ParentId
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectMessage(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }





        public List<enLeadManagementSubscription> GetAllOpenLeadListTL(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetAllOpenLeadListTL_CustomSearch]", new object[] {
                                                             searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserId,
                                                             searchFilter.DateFrom,
                                                             searchFilter.DateTo,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectGetAllLeadListManager(idr, objEN, "OpenLead");
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }


        public List<enLeadManagementSubscription> GetAllEmployeeRecordsChart()
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetEmployeeHierarchy_Sales]", new object[] {
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectListForEmployeeRecords(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }


        public List<enLeadManagementSubscription> GetLeadStatusReport(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadUser = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetSalesLeadStatusRepory]", new object[] {
                                                            searchFilter.Name,
                                                            searchFilter.DateFrom,
                                                            searchFilter.DateTo
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForLeadStatusReport(idr, objENLeads);
                        listReadUser.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadUser;
        }


        public List<enLeadManagementSubscription> TransferLeadPreSales(string CorpEmail, string MailChain)
        {
            List<enLeadManagementSubscription> List = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_TransferLeadPreSales]", new object[] {
                                                            CorpEmail,
                                                            MailChain
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectInsertStatus(idr, objENLeads);
                        List.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return List;
        }


        public List<enLeadManagementSubscription> BindPreLeadDetailsByid(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> List = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_BindPreSalesLeadDetailsByid]", new object[] {
                                                            searchFilter.Id,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectGetYourPreSalesLeadListbyidnew(idr, objENLeads, "OpenLead");
                        List.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return List;
        }



        public List<enLeadManagementSubscription> UpdatePreSalesLeadRecord(enLeadManagementSubscription enObj)
        {
            List<enLeadManagementSubscription> Status = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_UpdatePreSalesLeadDetails]", new object[] {
                                                                 enObj.Id,
                                                                enObj.Name,
                                                                enObj.CorpEmail,
                                                                enObj.Phone,
                                                                enObj.CompanyName,
                                                                enObj.CountryName,
                                                                enObj.StateName,
                                                                enObj.DiscriptionMessage,
                                                                enObj.OutboundLeadSource,
                                                                enObj.LeadType,
                                                                enObj.createdby,
                                                                enObj.IPAddress,
                                                                enObj.RequestedDate,
                                                                enObj.Mobile,
                                                                enObj.Designation,
                                                                enObj.ContactCountryCode,
                                                                enObj.TelephoneCountryCode

                                              }))

            {
                while (idr.Read())
                {
                    enLeadManagementSubscription objenProduct = new enLeadManagementSubscription();
                    ConstructObjectInsertStatus(idr, objenProduct);
                    Status.Add(objenProduct);
                }
            }
            return Status;
        }



        public List<enLeadManagementSubscription> SalesEmployeeName(int UserId)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_SalesEmployeeName]", new object[] {
                                                             UserId
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectUserDetails(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }



        public List<enLeadManagementSubscription> InsertEmployeeMasterPreSales(int ChildId, int ParentId)
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_InsertEmployeeMaster_PreSalesLead]", new object[] {
                                                           ChildId,
                                                           ParentId
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectMessage(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }



        public List<enLeadManagementSubscription> GetAllEmployeeRecordsChartPreSales()
        {
            List<enLeadManagementSubscription> listOfdatas = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetEmployeeHierarchy_PreSales]", new object[] {
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objEN = new enLeadManagementSubscription();
                        ConstructObjectListForEmployeeRecords(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }


        public List<enLeadManagementSubscription> GetLeadStatusReportPreSales(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadUser = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetSalesLeadStatusReporyPreSales]", new object[] {
                                                            searchFilter.Name,
                                                            searchFilter.FromDate,
                                                            searchFilter.ToDate
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForLeadStatusReportPreSales(idr, objENLeads, searchFilter.Type);
                        listReadUser.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadUser;
        }


        public List<enLeadManagementSubscription> GetRemainingPaymentdetails()
        {
            List<enLeadManagementSubscription> listReadUser = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetRemainingPaymentdetails]", new object[] {
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForRemainingPaymentdetails(idr, objENLeads);
                        listReadUser.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadUser;
        }

        public List<enLeadManagementSubscription> PreSalesDemocountDetails(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadUser = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_PreSalesDemocountDetails]", new object[] {
                                                            searchFilter.Name,
                                                            searchFilter.FromDate,
                                                            searchFilter.ToDate,
                                                            searchFilter.MonthofMonth,
                                                            searchFilter.Year
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForPreSalesonlyDemocountDetails(idr, objENLeads, searchFilter.Type);
                        listReadUser.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadUser;
        }


        public List<enLeadManagementSubscription> PreSalesDemocountDetailsByCreatedDate(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadUser = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_PreSalesDemocountDetailsByCreatedDate]", new object[] {
                                                            searchFilter.Name,
                                                            searchFilter.FromDate,
                                                            searchFilter.ToDate,
                                                            searchFilter.MonthofMonth,
                                                            searchFilter.Year
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForPreSalesonlyDemocountDetails(idr, objENLeads, searchFilter.Type);
                        listReadUser.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadUser;
        }

        public List<enLeadManagementSubscription> PreSalesWonCountDetils(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadUser = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_PreSalesWonCountDetils]", new object[] {
                                                            searchFilter.Name,
                                                            searchFilter.FromDate,
                                                            searchFilter.ToDate,
                                                            searchFilter.MonthofMonth,
                                                            searchFilter.Year
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForPreSalesDemocountDetails(idr, objENLeads, searchFilter.Type);
                        listReadUser.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadUser;
        }


        public List<enLeadManagementSubscription> PreSalesUnusedCountDetils(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadUser = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_PreSalesUnusedCountDetils]", new object[] {
                                                            searchFilter.Name,
                                                            searchFilter.FromDate,
                                                            searchFilter.ToDate,
                                                            searchFilter.MonthofMonth,
                                                            searchFilter.Year
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForPreSalesDemocountDetails(idr, objENLeads, searchFilter.Type);
                        listReadUser.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadUser;
        }


        public List<enLeadManagementSubscription> PreSalesTransferCountDetils(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> listReadUser = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_PreSalesTransferCountDetils]", new object[] {
                                                            searchFilter.Name,
                                                            searchFilter.FromDate,
                                                            searchFilter.ToDate,
                                                            searchFilter.MonthofMonth,
                                                            searchFilter.Year
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectListForPreSalesDemocountDetails(idr, objENLeads, searchFilter.Type);
                        listReadUser.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadUser;
        }

        public List<enLeadManagementSubscription> SearchDomain(enSearchFilter searchFilter)
        {
            List<enLeadManagementSubscription> List = new List<enLeadManagementSubscription>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_SearchDomainPreSales]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enLeadManagementSubscription objENLeads = new enLeadManagementSubscription();
                        ConstructObjectSearchDomain(idr, objENLeads);
                        List.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return List;

        }





        #region new implementation by Ashish
        //----------------new implementation by Ashish--------------------------------------------

        
        public List<enUsersDetails> GetUsersdata(enSearchFilter searchFilter)
        {
            List<enUsersDetails> listReadUser = new List<enUsersDetails>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetCRM_userData]", new object[] {
                                                            searchFilter.CrmId,
                                                           
                                                            }))
                {
                    while (idr.Read())
                    {
                        enUsersDetails objENLeads = new enUsersDetails();
                        ConstructObjectListUserAcountDetails(idr, objENLeads);
                        listReadUser.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadUser;
        }
        public List<enUsersDetails> Getcompanywiseleadstatus(enSearchFilter searchFilter)
        {
            List<enUsersDetails> listReadUser = new List<enUsersDetails>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_Getcompanywiseleadstatus]", new object[] {
                                                            searchFilter.CrmId,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enUsersDetails objENLeads = new enUsersDetails();
                        ConstructObjectListstepcounter(idr, objENLeads);
                        listReadUser.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return listReadUser;
        }

        public List<enUsersDetailsbyCompany> GetCustomersDetailsByCompany(enSearchFilter searchFilter)
        {
            List<enUsersDetailsbyCompany> listOfdatas = new List<enUsersDetailsbyCompany>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_CustomersDetailsByCompany]", new object[] {
                                                              searchFilter.searchKeyword,
                                                             searchFilter.StartRowNumber,
                                                             searchFilter.EndRowNumber,
                                                             searchFilter.UserId,
                                                            searchFilter.FilterType
                                                            }))
                {
                    while (idr.Read())
                    {
                        enUsersDetailsbyCompany objEN = new enUsersDetailsbyCompany();
                        ConstructObjectGetUsersDetailsbyCompany(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }
        public List<enCustomerDetailByCompany> GetCustomerDetailById(enSearchFilter searchFilter)
        {
            List<enCustomerDetailByCompany> listOfdatas = new List<enCustomerDetailByCompany>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetCompanyUsersdetilsById]", new object[] {
                                                             searchFilter.Id,  //COMPANY ID
                                                             searchFilter.CrmId
                                                            }))
                {
                    while (idr.Read())
                    {
                        enCustomerDetailByCompany objEN = new enCustomerDetailByCompany();
                        ConstructObjectGetCustomerDetailById(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }
        public List<enReminder> InsertReminder(enReminder r)
        {
            List<enReminder> list = new List<enReminder>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_InsertReminder",
                new SqlParameter("@CompanyId", r.CompanyId),
                new SqlParameter("@ReminderType", r.ReminderType),
                new SqlParameter("@Note", r.Note),
                new SqlParameter("@ReminderDate", r.ReminderDate),
                new SqlParameter("@ReminderTime", (object)r.ReminderTime ?? DBNull.Value),
                new SqlParameter("@ShowOnMyDay", r.ShowOnMyDay),
                new SqlParameter("@CreatedBy", r.CreatedBy)))
            {
                while (idr.Read())
                {
                    enReminder saved = new enReminder();
                    saved.Id = Convert.ToInt32(idr["Id"]);
                    saved.CompanyId = Convert.ToInt32(idr["CompanyId"]);
                    saved.ReminderType = DBNull.Value == idr["ReminderType"] ? null : Convert.ToString(idr["ReminderType"]);
                    saved.Note = DBNull.Value == idr["Note"] ? null : Convert.ToString(idr["Note"]);
                    saved.ReminderDate = Convert.ToDateTime(idr["ReminderDate"]);
                    saved.ReminderTime = DBNull.Value == idr["ReminderTime"] ? (TimeSpan?)null : (TimeSpan)idr["ReminderTime"];
                    saved.ShowOnMyDay = Convert.ToBoolean(idr["ShowOnMyDay"]);
                    saved.IsCompleted = Convert.ToBoolean(idr["IsCompleted"]);
                    saved.CreatedDate = Convert.ToDateTime(idr["CreatedDate"]);
                    list.Add(saved);
                }
            }
            return list;
        }
        public List<enReminder> GetRemindersByCompany(enSearchFilter searchFilter)
        {
            List<enReminder> list = new List<enReminder>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetRemindersByCompany",
                new SqlParameter("@CompanyId", searchFilter.Id)))
            {
                while (idr.Read())
                {
                    enReminder r = new enReminder();
                    r.Id = Convert.ToInt32(idr["Id"]);
                    r.CompanyId = Convert.ToInt32(idr["CompanyId"]);
                    r.ReminderType = DBNull.Value == idr["ReminderType"] ? null : Convert.ToString(idr["ReminderType"]);
                    r.Note = DBNull.Value == idr["Note"] ? null : Convert.ToString(idr["Note"]);
                    r.ReminderDate = Convert.ToDateTime(idr["ReminderDate"]);
                    r.ReminderTime = DBNull.Value == idr["ReminderTime"] ? (TimeSpan?)null : (TimeSpan)idr["ReminderTime"];
                    r.ShowOnMyDay = Convert.ToBoolean(idr["ShowOnMyDay"]);
                    r.IsCompleted = Convert.ToBoolean(idr["IsCompleted"]);
                    r.CreatedDate = Convert.ToDateTime(idr["CreatedDate"]);
                    list.Add(r);
                }
            }
            return list;
        }
        public List<enUserContact> GetContactsByCompany(enSearchFilter searchFilter)
        {
            List<enUserContact> list = new List<enUserContact>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetContactsByCompany",
                new SqlParameter("@CompanyId", searchFilter.Id),
                new SqlParameter("@searchKeyword", searchFilter.searchKeyword),
                new SqlParameter("@StartRowNumber", searchFilter.StartRowNumber),
                new SqlParameter("@EndRowNumber", searchFilter.EndRowNumber)))
            {
                while (idr.Read())
                {
                    enUserContact c = new enUserContact();
                    c.Id = Convert.ToInt32(idr["id"]);
                    c.Fname = DBNull.Value == idr["Fname"] ? null : Convert.ToString(idr["Fname"]);
                    c.Lname = DBNull.Value == idr["Lname"] ? null : Convert.ToString(idr["Lname"]);
                    c.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
                    c.Role = DBNull.Value == idr["Role"] ? null : Convert.ToString(idr["Role"]);
                    c.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
                    c.LastLogin = DBNull.Value == idr["LastLogin"] ? (DateTime?)null : Convert.ToDateTime(idr["LastLogin"]);
                    c.RowNumber = Convert.ToInt32(idr["RowNumber"]);
                    c.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
                    list.Add(c);
                }
            }
            return list;
        }
        public List<enSubscriptionItem> GetSubscriptionsByCompany(enSearchFilter searchFilter)
        {
            List<enSubscriptionItem> list = new List<enSubscriptionItem>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetSubscriptionsByCrmCompany",
                new SqlParameter("@CompanyId", searchFilter.Id),
                new SqlParameter("@CRMId", searchFilter.CrmId),
                new SqlParameter("@searchKeyword", searchFilter.searchKeyword),
                new SqlParameter("@StartRowNumber", searchFilter.StartRowNumber),
                new SqlParameter("@EndRowNumber", searchFilter.EndRowNumber)))
            {
                while (idr.Read())
                {
                    enSubscriptionItem s = new enSubscriptionItem();
                    s.Email = DBNull.Value == idr["email"] ? null : Convert.ToString(idr["email"]);
                    s.ModuleName = DBNull.Value == idr["ModuleName"] ? null : Convert.ToString(idr["ModuleName"]);
                    s.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
                    s.Role = DBNull.Value == idr["Role"] ? null : Convert.ToString(idr["Role"]);
                    s.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
                    s.RowNumber = Convert.ToInt32(idr["RowNumber"]);
                    s.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
                    list.Add(s);
                }
            }
            return list;
        }
        public List<enLoginRecord> GetRecentLoginsByCompany(enSearchFilter searchFilter)
        {
            List<enLoginRecord> list = new List<enLoginRecord>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetRecentLoginsByCompany",
                new SqlParameter("@CompanyId", searchFilter.Id),
                new SqlParameter("@Top", 10)))
            {
                while (idr.Read())
                {
                    enLoginRecord r = new enLoginRecord();
                    r.UserName = DBNull.Value == idr["UserName"] ? null : Convert.ToString(idr["UserName"]);
                    r.Login = DBNull.Value == idr["Login"] ? (DateTime?)null : Convert.ToDateTime(idr["Login"]);
                    r.Logout = DBNull.Value == idr["Logout"] ? (DateTime?)null : Convert.ToDateTime(idr["Logout"]);
                    r.CreatedTime = Convert.ToDateTime(idr["CreatedTime"]);
                    r.DeviceType = DBNull.Value == idr["DeviceType"] ? null : Convert.ToString(idr["DeviceType"]);
                    list.Add(r);
                }
            }
            return list;
        }

        // Full login history for a company - paged + searchable (backs the
        // "Full history" popup on CustomerDetail).
        public List<enLoginRecord> GetLoginHistoryByCompany(enSearchFilter searchFilter)
        {
            List<enLoginRecord> list = new List<enLoginRecord>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetLoginHistoryByCompany",
                new SqlParameter("@CompanyId", searchFilter.Id),
                new SqlParameter("@searchKeyword", (object)searchFilter.searchKeyword ?? DBNull.Value),
                new SqlParameter("@StartRowNumber", searchFilter.StartRowNumber),
                new SqlParameter("@EndRowNumber", searchFilter.EndRowNumber)))
            {
                while (idr.Read())
                {
                    enLoginRecord r = new enLoginRecord();
                    r.UserName = DBNull.Value == idr["UserName"] ? null : Convert.ToString(idr["UserName"]);
                    r.Login = DBNull.Value == idr["Login"] ? (DateTime?)null : Convert.ToDateTime(idr["Login"]);
                    r.Logout = DBNull.Value == idr["Logout"] ? (DateTime?)null : Convert.ToDateTime(idr["Logout"]);
                    r.CreatedTime = Convert.ToDateTime(idr["CreatedTime"]);
                    r.DeviceType = DBNull.Value == idr["DeviceType"] ? null : Convert.ToString(idr["DeviceType"]);
                    r.RowNumber = Convert.ToInt64(idr["RowNumber"]);
                    r.RecordsCount = Convert.ToInt64(idr["RecordsCount"]);
                    list.Add(r);
                }
            }
            return list;
        }

        // NOTE: this SP returns a single summary row — DAL still returns it wrapped in a
        // List<enUsageSummary> (matching your convention), and the controller does
        // .FirstOrDefault() to unwrap it, exactly like GetCustomerDetailById does.
        public List<enUsageSummary> GetLoginUsageSummaryByCompany(enSearchFilter searchFilter)
        {
            List<enUsageSummary> list = new List<enUsageSummary>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetLoginUsageSummaryByCompany",
                new SqlParameter("@CompanyId", searchFilter.Id)))
            {
                if (idr.Read())
                {
                    enUsageSummary data = new enUsageSummary();
                    data.LoginsThisMonth = DBNull.Value == idr["LoginsThisMonth"] ? 0 : Convert.ToInt32(idr["LoginsThisMonth"]);
                    data.LoginsThisQuarter = DBNull.Value == idr["LoginsThisQuarter"] ? 0 : Convert.ToInt32(idr["LoginsThisQuarter"]);
                    data.LoginsThisYear = DBNull.Value == idr["LoginsThisYear"] ? 0 : Convert.ToInt32(idr["LoginsThisYear"]);
                    data.ActiveUserCount = DBNull.Value == idr["ActiveUserCount"] ? 0 : Convert.ToInt32(idr["ActiveUserCount"]);
                    list.Add(data);
                }
            }
            return list;
        }

        public List<enMonthlyLoginCount> GetLoginsByMonthByCompany(enSearchFilter searchFilter)
        {
            List<enMonthlyLoginCount> list = new List<enMonthlyLoginCount>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetLoginsByMonthByCompany",
                new SqlParameter("@CompanyId", searchFilter.Id),
                new SqlParameter("@MonthsBack", 10)))
            {
                while (idr.Read())
                {
                    enMonthlyLoginCount m = new enMonthlyLoginCount();
                    m.MonthLabel = Convert.ToString(idr["MonthLabel"]);
                    m.MonthStart = Convert.ToDateTime(idr["MonthStart"]);
                    m.LoginCount = DBNull.Value == idr["LoginCount"] ? 0 : Convert.ToInt32(idr["LoginCount"]);
                    list.Add(m);
                }
            }
            return list;
        }

        public List<enProductViewCount> GetTopPricingProductsByCompany(enSearchFilter searchFilter)
        {
            List<enProductViewCount> list = new List<enProductViewCount>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetTopPricingProductsByCompany",
                new SqlParameter("@CompanyId", searchFilter.Id),
                new SqlParameter("@Top", 5)))
            {
                while (idr.Read())
                {
                    enProductViewCount p = new enProductViewCount();
                    p.ProductName = Convert.ToString(idr["ProductName"]);
                    p.ViewCount = Convert.ToInt32(idr["ViewCount"]);
                    list.Add(p);
                }
            }
            return list;
        }

        public List<enProductViewCount> GetTopDisruptionProductsByCompany(enSearchFilter searchFilter)
        {
            List<enProductViewCount> list = new List<enProductViewCount>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetTopDisruptionProductsByCompany",
                new SqlParameter("@CompanyId", searchFilter.Id),
                new SqlParameter("@Top", 5)))
            {
                while (idr.Read())
                {
                    enProductViewCount p = new enProductViewCount();
                    p.ProductName = Convert.ToString(idr["ProductName"]);
                    p.ViewCount = Convert.ToInt32(idr["ViewCount"]);
                    list.Add(p);
                }
            }
            return list;
        }

        public List<enProductViewCount> GetTopMarketAnalysisProductsByCompany(enSearchFilter searchFilter)
        {
            List<enProductViewCount> list = new List<enProductViewCount>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetTopMarketAnalysisProductsByCompany",
                new SqlParameter("@CompanyId", searchFilter.Id),
                new SqlParameter("@Top", 5)))
            {
                while (idr.Read())
                {
                    enProductViewCount p = new enProductViewCount();
                    p.ProductName = Convert.ToString(idr["ProductName"]);
                    p.ViewCount = Convert.ToInt32(idr["ViewCount"]);
                    list.Add(p);
                }
            }
            return list;
        }

        public List<enProductViewCount> GetTopCombinedProductsByCompany(enSearchFilter searchFilter)
        {
            List<enProductViewCount> list = new List<enProductViewCount>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetTopCombinedProductsByCompany",
                new SqlParameter("@CompanyId", searchFilter.Id),
                new SqlParameter("@Top", 10)))
            {
                while (idr.Read())
                {
                    enProductViewCount p = new enProductViewCount();
                    p.ProductName = Convert.ToString(idr["ProductName"]);
                    p.PricingViews = DBNull.Value == idr["PricingViews"] ? 0 : Convert.ToInt32(idr["PricingViews"]);
                    p.DisruptionViews = DBNull.Value == idr["DisruptionViews"] ? 0 : Convert.ToInt32(idr["DisruptionViews"]);
                    p.MarketAnalysisViews = DBNull.Value == idr["MarketAnalysisViews"] ? 0 : Convert.ToInt32(idr["MarketAnalysisViews"]);
                    p.TotalViews = DBNull.Value == idr["TotalViews"] ? 0 : Convert.ToInt32(idr["TotalViews"]);
                    p.ViewCount = p.TotalViews;
                    list.Add(p);
                }
            }
            return list;
        }


        public List<enSupportTicket> GetTicketsByCompany(enSearchFilter searchFilter)
        {
            List<enSupportTicket> list = new List<enSupportTicket>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetTicketsByCompany",
                new SqlParameter("@CompanyId", searchFilter.Id),
                new SqlParameter("@searchKeyword", searchFilter.searchKeyword),
                new SqlParameter("@StartRowNumber", searchFilter.StartRowNumber),
                new SqlParameter("@EndRowNumber", searchFilter.EndRowNumber)))
            {
                while (idr.Read())
                {
                    enSupportTicket t = new enSupportTicket();
                    t.ID = Convert.ToInt32(idr["ID"]);
                    t.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
                    t.CreatedDate = Convert.ToDateTime(idr["CreatedDate"]);
                    t.UpdatedDate = DBNull.Value == idr["UpdatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdatedDate"]);
                    t.Rating = DBNull.Value == idr["Rating"] ? (int?)null : Convert.ToInt32(idr["Rating"]);
                    t.Subject = DBNull.Value == idr["Subject"] ? null : Convert.ToString(idr["Subject"]);
                    t.RoleName = DBNull.Value == idr["RoleName"] ? null : Convert.ToString(idr["RoleName"]);
                    t.RowNumber = Convert.ToInt32(idr["RowNumber"]);
                    t.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
                    list.Add(t);
                }
            }
            return list;
        }

        public List<enSupportTicketComment> GetTicketHistoryById(enSearchFilter searchFilter)
        {
            List<enSupportTicketComment> list = new List<enSupportTicketComment>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetTicketHistoryById",
                new SqlParameter("@TicketId", searchFilter.Id)))
            {
                while (idr.Read())
                {
                    enSupportTicketComment c = new enSupportTicketComment();
                    c.ID = Convert.ToInt32(idr["ID"]);
                    c.UserFID = DBNull.Value == idr["UserFID"] ? (int?)null : Convert.ToInt32(idr["UserFID"]);
                    c.ManagerFID = DBNull.Value == idr["ManagerFID"] ? (int?)null : Convert.ToInt32(idr["ManagerFID"]);
                    c.Comment = DBNull.Value == idr["Comment"] ? null : Convert.ToString(idr["Comment"]);
                    c.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
                    c.CreatedDate = Convert.ToDateTime(idr["CreatedDate"]);
                    list.Add(c);
                }
            }
            return list;
        }
        public List<enProformaInvoiceSummary> GetProformaInvoicesByCompanyName(string companyName)
        {
            List<enProformaInvoiceSummary> list = new List<enProformaInvoiceSummary>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetProformaInvoicesByCompanyName",
                new SqlParameter("@CompanyName", companyName)))
            {
                while (idr.Read())
                {
                    enProformaInvoiceSummary i = new enProformaInvoiceSummary();
                    i.ID = Convert.ToInt32(idr["ID"]);
                    i.InvoiceNo = DBNull.Value == idr["InvoiceNo"] ? null : Convert.ToString(idr["InvoiceNo"]);
                    i.CreatedDate = Convert.ToDateTime(idr["CreatedDate"]);
                    i.TotalAmount = DBNull.Value == idr["TotalAmount"] ? (decimal?)null : Convert.ToDecimal(idr["TotalAmount"]);
                    i.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
                    list.Add(i);
                }
            }
            return list;
        }
        public List<enTodayReminder> GetTodaysRemindersForUser(enSearchFilter searchFilter)
        {
            List<enTodayReminder> list = new List<enTodayReminder>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetTodaysRemindersForUser",
                new SqlParameter("@CreatedBy", searchFilter.CrmId)))
            {
                while (idr.Read())
                {
                    enTodayReminder r = new enTodayReminder();
                    r.Id = Convert.ToInt32(idr["Id"]);
                    r.CompanyId = Convert.ToInt32(idr["CompanyId"]);
                    r.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
                    r.ReminderType = DBNull.Value == idr["ReminderType"] ? null : Convert.ToString(idr["ReminderType"]);
                    r.Note = DBNull.Value == idr["Note"] ? null : Convert.ToString(idr["Note"]);
                    r.ReminderTime = DBNull.Value == idr["ReminderTime"] ? null : Convert.ToString(idr["ReminderTime"]);
                    list.Add(r);
                }
            }
            return list;
        }

        public void MarkReminderComplete(int reminderId)
        {
            SqlHelper.ExecuteNonQuery(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_MarkReminderComplete",
                new SqlParameter("@ReminderId", reminderId));
        }

        public List<enWorkSpaceFile> GetDeliverablesByCompany(enSearchFilter searchFilter)
        {
            List<enWorkSpaceFile> list = new List<enWorkSpaceFile>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetDeliverablesByCompany",
                new SqlParameter("@CompanyId", searchFilter.Id),
                new SqlParameter("@StartRowNumber", searchFilter.StartRowNumber),
                new SqlParameter("@EndRowNumber", searchFilter.EndRowNumber)))
            {
                while (idr.Read())
                {
                    enWorkSpaceFile f = new enWorkSpaceFile();
                    f.ID = Convert.ToInt32(idr["ID"]);
                    f.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
                    f.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
                    f.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
                    f.CreatedDate = Convert.ToDateTime(idr["CreatedDate"]);
                    f.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
                    f.RowNumber = Convert.ToInt32(idr["RowNumber"]);
                    f.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
                    list.Add(f);
                }
            }
            return list;
        }
        public List<enRecommendationSignals> GetRecommendationSignalsByCompany(enSearchFilter searchFilter)
        {
            List<enRecommendationSignals> list = new List<enRecommendationSignals>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetRecommendationSignalsByCompany",
                new SqlParameter("@CompanyId", searchFilter.Id)))
            {
                if (idr.Read())
                {
                    enRecommendationSignals s = new enRecommendationSignals();
                    s.TopDisruptionProduct = DBNull.Value == idr["TopDisruptionProduct"] ? null : Convert.ToString(idr["TopDisruptionProduct"]);
                    s.TopDisruptionViews = DBNull.Value == idr["TopDisruptionViews"] ? (int?)null : Convert.ToInt32(idr["TopDisruptionViews"]);
                    s.AlreadySubscribed = DBNull.Value == idr["AlreadySubscribed"] ? false : Convert.ToBoolean(idr["AlreadySubscribed"]);
                    s.TotalSeats = DBNull.Value == idr["TotalSeats"] ? 0 : Convert.ToInt32(idr["TotalSeats"]);
                    s.ActiveSeats = DBNull.Value == idr["ActiveSeats"] ? 0 : Convert.ToInt32(idr["ActiveSeats"]);
                    s.InactiveUserName = DBNull.Value == idr["InactiveUserName"] ? null : Convert.ToString(idr["InactiveUserName"]);
                    s.InactiveDays = DBNull.Value == idr["InactiveDays"] ? (int?)null : Convert.ToInt32(idr["InactiveDays"]);
                    s.OpenTicketCount = DBNull.Value == idr["OpenTicketCount"] ? 0 : Convert.ToInt32(idr["OpenTicketCount"]);
                    s.OldestOpenTicketDays = DBNull.Value == idr["OldestOpenTicketDays"] ? (int?)null : Convert.ToInt32(idr["OldestOpenTicketDays"]);
                    s.DaysSinceLastDeliverable = DBNull.Value == idr["DaysSinceLastDeliverable"] ? (int?)null : Convert.ToInt32(idr["DaysSinceLastDeliverable"]);
                    s.LoginsThisMonth = DBNull.Value == idr["LoginsThisMonth"] ? (int?)null : Convert.ToInt32(idr["LoginsThisMonth"]);
                    s.LoginsLastMonth = DBNull.Value == idr["LoginsLastMonth"] ? (int?)null : Convert.ToInt32(idr["LoginsLastMonth"]);
                    s.StaleReminderCount = DBNull.Value == idr["StaleReminderCount"] ? 0 : Convert.ToInt32(idr["StaleReminderCount"]);
                    list.Add(s);
                }
            }
            return list;
        }

        public List<enNeedsAttentionItem> GetNeedsAttentionToday(enSearchFilter searchFilter)
        {
            List<enNeedsAttentionItem> list = new List<enNeedsAttentionItem>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetNeedsAttentionToday",
                new SqlParameter("@CRMId", searchFilter.CrmId),
                new SqlParameter("@Top", 10)))
            {
                while (idr.Read())
                {
                    enNeedsAttentionItem i = new enNeedsAttentionItem();
                    i.ID = Convert.ToInt32(idr["ID"]);
                    i.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
                    i.PrimaryContact = DBNull.Value == idr["PrimaryContact"] ? null : Convert.ToString(idr["PrimaryContact"]);
                    i.RenewsDate = DBNull.Value == idr["RenewsDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RenewsDate"]);
                    i.DaysToRenewal = DBNull.Value == idr["DaysToRenewal"] ? (int?)null : Convert.ToInt32(idr["DaysToRenewal"]);
                    i.LastLogin = DBNull.Value == idr["LastLogin"] ? (DateTime?)null : Convert.ToDateTime(idr["LastLogin"]);
                    i.DaysInactive = DBNull.Value == idr["DaysInactive"] ? (int?)null : Convert.ToInt32(idr["DaysInactive"]);
                    i.Reason = DBNull.Value == idr["Reason"] ? null : Convert.ToString(idr["Reason"]);
                    list.Add(i);
                }
            }
            return list;
        }

        public List<enRenewalThisQuarter> GetRenewalsThisQuarter(enSearchFilter searchFilter)
        {
            List<enRenewalThisQuarter> list = new List<enRenewalThisQuarter>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetRenewalsThisQuarter",
                new SqlParameter("@CRMId", searchFilter.CrmId)))
            {
                while (idr.Read())
                {
                    enRenewalThisQuarter i = new enRenewalThisQuarter();
                    i.ID = Convert.ToInt32(idr["ID"]);
                    i.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
                    i.RenewsDate = DBNull.Value == idr["RenewsDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RenewsDate"]);
                    i.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
                    list.Add(i);
                }
            }
            return list;
        }

        public List<enLabelCount> GetCustomersByCountry(enSearchFilter searchFilter)
        {
            List<enLabelCount> list = new List<enLabelCount>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetCustomersByCountry",
                new SqlParameter("@CRMId", searchFilter.CrmId)))
            {
                while (idr.Read())
                {
                    enLabelCount c = new enLabelCount();
                    c.Label = DBNull.Value == idr["Label"] ? "Unknown" : Convert.ToString(idr["Label"]);
                    c.CustomerCount = Convert.ToInt32(idr["CustomerCount"]);
                    list.Add(c);
                }
            }
            return list;
        }

        public List<enLabelCount> GetCustomersByIndustry(enSearchFilter searchFilter)
        {
            List<enLabelCount> list = new List<enLabelCount>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetCustomersByIndustry",
                new SqlParameter("@CRMId", searchFilter.CrmId)))
            {
                while (idr.Read())
                {
                    enLabelCount c = new enLabelCount();
                    c.Label = DBNull.Value == idr["Label"] ? "Unknown" : Convert.ToString(idr["Label"]);
                    c.CustomerCount = Convert.ToInt32(idr["CustomerCount"]);
                    list.Add(c);
                }
            }
            return list;
        }

        public List<enMonthlyAddedLost> GetCustomersAddedVsLostByMonth(enSearchFilter searchFilter)
        {
            List<enMonthlyAddedLost> list = new List<enMonthlyAddedLost>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetCustomersAddedVsLostByMonth",
                new SqlParameter("@CRMId", searchFilter.CrmId)))
            {
                while (idr.Read())
                {
                    enMonthlyAddedLost m = new enMonthlyAddedLost();
                    m.Yr = Convert.ToInt32(idr["Yr"]);
                    m.Mo = Convert.ToInt32(idr["Mo"]);
                    m.AddedCount = Convert.ToInt32(idr["AddedCount"]);
                    m.LostCount = Convert.ToInt32(idr["LostCount"]);
                    list.Add(m);
                }
            }
            return list;
        }

        public List<enMonthlyRevenue> GetRenewalRevenueVsLostByMonth(enSearchFilter searchFilter)
        {
            List<enMonthlyRevenue> list = new List<enMonthlyRevenue>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetRenewalRevenueVsLostByMonth",
                new SqlParameter("@CRMId", searchFilter.CrmId)))
            {
                while (idr.Read())
                {
                    enMonthlyRevenue m = new enMonthlyRevenue();
                    m.Yr = Convert.ToInt32(idr["Yr"]);
                    m.Mo = Convert.ToInt32(idr["Mo"]);
                    m.RenewalRevenue = idr["RenewalRevenue"] == DBNull.Value ? 0 : Convert.ToDecimal(idr["RenewalRevenue"]);
                    m.RevenueLost = idr["RevenueLost"] == DBNull.Value ? 0 : Convert.ToDecimal(idr["RevenueLost"]);
                    list.Add(m);
                }
            }
            return list;
        }

        // CRM "Subscriptions" page list — same proc/paging as the Sales-side
        // SalesIndex (usp_SalesIndex_GetSubscriptionRequests), moved into this
        // controller/BL/DAL so the whole page lives under LeadManagementSubscription.
        public List<enSalesManagement> GetSubscriptionRequestsList(string search, int startRowNumber, int endRowNumber, int userId)
        {
            var list = new List<enSalesManagement>();
            using (IDataReader rdr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "usp_LeadManagementSubscription_GetSubscriptionRequests",
                new SqlParameter("@searchKeyword", (object)search ?? DBNull.Value),
                new SqlParameter("@StartRowNumber", startRowNumber),
                new SqlParameter("@EndRowNumber", endRowNumber),
                new SqlParameter("@userId", userId)))
            {
                while (rdr.Read())
                {
                    list.Add(new enSalesManagement
                    {
                        RequestNumber = rdr["RequestNumber"] == DBNull.Value ? "" : rdr["RequestNumber"].ToString(),
                        CompanyName = rdr["Company"] == DBNull.Value ? "" : rdr["Company"].ToString(),
                        PrimaryContactName = rdr["Primarycontact"] == DBNull.Value ? "" : rdr["Primarycontact"].ToString(),
                        CSM = rdr["CRM"] == DBNull.Value ? "" : rdr["CRM"].ToString(),
                        RequestCreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                        SalesIndexStatus = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                        PackageSummary = rdr["Package"] == DBNull.Value ? "" : rdr["Package"].ToString(),
                        CoverageSummary = rdr["Coverage"] == DBNull.Value ? "" : rdr["Coverage"].ToString(),
                        Renew = rdr["Renew"] == DBNull.Value ? "" : rdr["Renew"].ToString(),
                        RowNumber = rdr["RowNumber"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["RowNumber"]),
                        RecordsCount = rdr["RecordsCount"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["RecordsCount"])
                    });
                }
            }
            return list;
        }

        // CRM subscription-request detail (per-company coverage lines) — same
        // proc as Sales, exposed here so the CRM detail page is self-contained.
        public List<enSubscriptionRequestLine> GetSubscriptionRequestLines(string companyName, string search, int startRowNumber, int endRowNumber)
        {
            var lines = new List<enSubscriptionRequestLine>();
            using (IDataReader rdr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "usp_SalesManagement_GetSubscriptionRequestDetail",
                new SqlParameter("@CompanyName", (object)companyName ?? DBNull.Value),
                new SqlParameter("@Search", (object)search ?? DBNull.Value),
                new SqlParameter("@StartRowNumber", startRowNumber),
                new SqlParameter("@EndRowNumber", endRowNumber)))
            {
                while (rdr.Read())
                {
                    lines.Add(new enSubscriptionRequestLine
                    {
                        LeadMasterId = rdr["LeadMasterId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["LeadMasterId"]),
                        Company = rdr["Company"] == DBNull.Value ? "" : rdr["Company"].ToString(),
                        PrimaryContact = rdr["Primarycontact"] == DBNull.Value ? "" : rdr["Primarycontact"].ToString(),
                        CountryName = rdr["CountryName"] == DBNull.Value ? "" : rdr["CountryName"].ToString(),
                        RegionName = rdr["RegionName"] == DBNull.Value ? "" : rdr["RegionName"].ToString(),
                        CRM = rdr["CRM"] == DBNull.Value ? "" : rdr["CRM"].ToString(),
                        CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                        SecondaryClintEmail = rdr["SecondaryClintEmail"] == DBNull.Value ? "" : rdr["SecondaryClintEmail"].ToString(),
                        Package = rdr["Package"] == DBNull.Value ? "" : rdr["Package"].ToString(),
                        Products = rdr["Products"] == DBNull.Value ? "" : rdr["Products"].ToString(),
                        SubscriptionDateFrom = rdr["SubscriptionDateFrom"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["SubscriptionDateFrom"]),
                        SubscriptionDateTo = rdr["SubscriptionDateTo"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["SubscriptionDateTo"]),
                        Trader = rdr["Trader"] == DBNull.Value ? "" : rdr["Trader"].ToString(),
                        Renew = rdr["Renew"] == DBNull.Value ? "" : rdr["Renew"].ToString(),
                        Credit = rdr["Credit"] == DBNull.Value ? "" : rdr["Credit"].ToString(),
                        BillingEmail = rdr["BillingEmail"] == DBNull.Value ? "" : rdr["BillingEmail"].ToString(),
                        UserType = rdr["UserType"] == DBNull.Value ? "" : rdr["UserType"].ToString(),
                        Phone = rdr["Phone"] == DBNull.Value ? "" : rdr["Phone"].ToString(),
                        RowNumber = rdr["RowNumber"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["RowNumber"]),
                        RecordsCount = rdr["RecordsCount"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["RecordsCount"])
                    });
                }
            }
            return lines;
        }

        public List<enMonthlyAvg> GetAvgRevenueUsersPerCustomerByMonth(enSearchFilter searchFilter)
        {
            List<enMonthlyAvg> list = new List<enMonthlyAvg>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetAvgRevenueUsersPerCustomerByMonth",
                new SqlParameter("@CRMId", searchFilter.CrmId)))
            {
                while (idr.Read())
                {
                    enMonthlyAvg m = new enMonthlyAvg();
                    m.Yr = Convert.ToInt32(idr["Yr"]);
                    m.Mo = Convert.ToInt32(idr["Mo"]);
                    m.AvgRevenue = idr["AvgRevenue"] == DBNull.Value ? 0 : Convert.ToDecimal(idr["AvgRevenue"]);
                    m.AvgUsers = idr["AvgUsers"] == DBNull.Value ? 0 : Convert.ToDecimal(idr["AvgUsers"]);
                    list.Add(m);
                }
            }
            return list;
        }

        public List<enMonthlyAddedLost> GetUsersAddedVsLostByMonth(enSearchFilter searchFilter)
        {
            List<enMonthlyAddedLost> list = new List<enMonthlyAddedLost>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetUsersAddedVsLostByMonth",
                new SqlParameter("@CRMId", searchFilter.CrmId)))
            {
                while (idr.Read())
                {
                    enMonthlyAddedLost m = new enMonthlyAddedLost();
                    m.Yr = Convert.ToInt32(idr["Yr"]);
                    m.Mo = Convert.ToInt32(idr["Mo"]);
                    m.AddedCount = Convert.ToInt32(idr["AddedCount"]);
                    m.LostCount = Convert.ToInt32(idr["LostCount"]);
                    list.Add(m);
                }
            }
            return list;
        }

        public List<enAllTicket> GetAllTicketsByCRM(enSearchFilter searchFilter)
        {
            List<enAllTicket> list = new List<enAllTicket>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "USP_GetAllTicketsByCRM",
                new SqlParameter("@CRMId", searchFilter.CrmId),
                new SqlParameter("@StatusFilter", searchFilter.FilterType),
                new SqlParameter("@searchKeyword", searchFilter.searchKeyword),
                new SqlParameter("@StartRowNumber", searchFilter.StartRowNumber),
                new SqlParameter("@EndRowNumber", searchFilter.EndRowNumber)))
            {
                while (idr.Read())
                {
                    enAllTicket t = new enAllTicket();
                    t.ID = Convert.ToInt32(idr["ID"]);
                    t.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
                    t.ContactName = DBNull.Value == idr["ContactName"] ? null : Convert.ToString(idr["ContactName"]);
                    t.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
                    t.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
                    t.CreatedDate = Convert.ToDateTime(idr["CreatedDate"]);
                    t.UpdatedDate = DBNull.Value == idr["UpdatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdatedDate"]);
                    t.Subject = DBNull.Value == idr["Subject"] ? null : Convert.ToString(idr["Subject"]);
                    t.RowNumber = Convert.ToInt32(idr["RowNumber"]);
                    t.RecordsCount = Convert.ToInt32(idr["RecordsCount"]);
                    t.RoleName = DBNull.Value == idr["RoleName"] ? null : Convert.ToString(idr["RoleName"]);
                    
                    list.Add(t);
                }
            }
            return list;
        }
        private void ConstructObjectGetCustomerDetailById(IDataReader idr, enCustomerDetailByCompany enObj_)
        {
            enObj_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enObj_.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enObj_.FirstContactEmail = DBNull.Value == idr["FirstContactEmail"] ? null : Convert.ToString(idr["FirstContactEmail"]);
            enObj_.PrimaryContact = DBNull.Value == idr["PrimaryContact"] ? null : Convert.ToString(idr["PrimaryContact"]);
            enObj_.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            enObj_.ModuleName = DBNull.Value == idr["ModuleName"] ? null : Convert.ToString(idr["ModuleName"]);
            enObj_.api = DBNull.Value == idr["api"] ? null : Convert.ToString(idr["api"]);
            enObj_.RenewsDate = DBNull.Value == idr["RenewsDate"] ? null : Convert.ToString(idr["RenewsDate"]);
            enObj_.ApiStatus = DBNull.Value == idr["ApiStatus"] ? null : Convert.ToString(idr["ApiStatus"]);
            enObj_.Activeseats = DBNull.Value == idr["Activeseats"] ? (int?)null : Convert.ToInt32(idr["Activeseats"]);
            enObj_.AssignedCSM = DBNull.Value == idr["AssignedCSM"] ? null : Convert.ToString(idr["AssignedCSM"]);
            enObj_.Assignedanalyst = DBNull.Value == idr["Assignedanalyst"] ? null : Convert.ToString(idr["Assignedanalyst"]);
            enObj_.LastLogin = DBNull.Value == idr["LastLogin"] ? null : Convert.ToString(idr["LastLogin"]);
            enObj_.Created = DBNull.Value == idr["Created"] ? null : Convert.ToString(idr["Created"]);
            enObj_.Currenttermends = DBNull.Value == idr["Currenttermends"] ? null : Convert.ToString(idr["Currenttermends"]);
            enObj_.Health = DBNull.Value == idr["Health"] ? null : Convert.ToString(idr["Health"]);
        }


        private void ConstructObjectGetUsersDetailsbyCompany(IDataReader idr, enUsersDetailsbyCompany enObj_)
        {
            enObj_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enObj_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            enObj_.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            
            enObj_.RenewsDate = DBNull.Value == idr["RenewsDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RenewsDate"]);
            enObj_.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enObj_.FirstContactEmail = DBNull.Value == idr["FirstContactEmail"] ? null : Convert.ToString(idr["FirstContactEmail"]);
            enObj_.PrimaryContact = DBNull.Value == idr["PrimaryContact"] ? null : Convert.ToString(idr["PrimaryContact"]);
            enObj_.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            enObj_.ModuleName = DBNull.Value == idr["ModuleName"] ? null : Convert.ToString(idr["ModuleName"]);
            enObj_.api = DBNull.Value == idr["api"] ? null : Convert.ToString(idr["api"]);
            enObj_.ApiStatus = DBNull.Value == idr["ApiStatus"] ? null : Convert.ToString(idr["ApiStatus"]);
            enObj_.Health = DBNull.Value == idr["Health"] ? null : Convert.ToString(idr["Health"]);
            enObj_.LastLogin = DBNull.Value == idr["LastLogin"] ? (DateTime?)null : Convert.ToDateTime(idr["LastLogin"]);
        }



            private void ConstructObjectListUserAcountDetails(IDataReader idr, enUsersDetails enUser_)
        {
            enUser_.ActiveCustomer = DBNull.Value == idr["ActiveCustomer"] ? null : Convert.ToString(idr["ActiveCustomer"]);
            enUser_.Expthirtydays = DBNull.Value == idr["Expthirtydays"] ? null : Convert.ToString(idr["Expthirtydays"]);
            enUser_.PaymentsPending = DBNull.Value == idr["PaymentsPending"] ? null : Convert.ToString(idr["PaymentsPending"]);
            enUser_.inActivesixtydays = DBNull.Value == idr["inActivesixtydays"] ? null : Convert.ToString(idr["inActivesixtydays"]);
            enUser_.opentickets = DBNull.Value == idr["opentickets"] ? null : Convert.ToString(idr["opentickets"]);
            enUser_.ThisMonthAdd = DBNull.Value == idr["ThisMonthAdd"] ? null : Convert.ToString(idr["ThisMonthAdd"]);
            enUser_.Overdues = DBNull.Value == idr["Overdues"] ? null : Convert.ToString(idr["Overdues"]);
            enUser_.Awaitingreply = DBNull.Value == idr["Awaitingreply"] ? null : Convert.ToString(idr["Awaitingreply"]);

            
        }
        private void ConstructObjectListstepcounter(IDataReader idr, enUsersDetails enUser_)
        {
            enUser_.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enUser_.LeadStatus  = DBNull.Value == idr["LeadStatus"] ? null : Convert.ToString(idr["LeadStatus"]);
            enUser_.CreatedBy   = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? null : Convert.ToString(idr["CreatedDate"]);
        }

            //---------------------------------------------------------------------------------END NEWS IMPLEMENTATION------------------------------------------
            #endregion
            private void ConstructObjectSearchDomain(IDataReader idr, enLeadManagementSubscription enUser_)
        {
            enUser_.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            enUser_.CorpEmail = DBNull.Value == idr["Domain"] ? null : Convert.ToString(idr["Domain"]);
            enUser_.createdby = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            enUser_.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            enUser_.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);

        }

        private void ConstructObjectListForPreSalesDemocountDetails(IDataReader idr, enLeadManagementSubscription objENLeads, string Type)
        {
            objENLeads.Id = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.createdby = DBNull.Value == idr["createdby"] ? null : Convert.ToString(idr["createdby"]);
            objENLeads.OutboundLeadSource = DBNull.Value == idr["OutboundLeadSource"] ? null : Convert.ToString(idr["OutboundLeadSource"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objENLeads.DemoDiscussion = DBNull.Value == idr["Demo"] ? null : Convert.ToString(idr["Demo"]);
        }

        private void ConstructObjectListForPreSalesonlyDemocountDetails(IDataReader idr, enLeadManagementSubscription objENLeads, string Type)
        {
            objENLeads.Id = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.createdby = DBNull.Value == idr["createdby"] ? null : Convert.ToString(idr["createdby"]);
            objENLeads.OutboundLeadSource = DBNull.Value == idr["OutboundLeadSource"] ? null : Convert.ToString(idr["OutboundLeadSource"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objENLeads.DemoDiscussion = DBNull.Value == idr["Demo"] ? null : Convert.ToString(idr["Demo"]);
        }



        private void ConstructObjectListForLeadStatusReportPreSales(IDataReader idr, enLeadManagementSubscription objENLeads, string Type)
        {
                objENLeads.TotalLead = DBNull.Value == idr["TotalLead"] ? 0 : Convert.ToInt32(idr["TotalLead"]);
                objENLeads.TotalOpenLead = DBNull.Value == idr["TotalOpenLead"] ? 0 : Convert.ToInt32(idr["TotalOpenLead"]);
                objENLeads.LeadTransfer = DBNull.Value == idr["LeadTransfer"] ? 0 : Convert.ToInt32(idr["LeadTransfer"]);
                objENLeads.WonLead = DBNull.Value == idr["WonLead"] ? 0 : Convert.ToInt32(idr["WonLead"]);
                objENLeads.DemoCount = DBNull.Value == idr["DemoCount"] ? 0 : Convert.ToInt32(idr["DemoCount"]);
                objENLeads.NewDemoCount = DBNull.Value == idr["NewDemoCount"] ? 0 : Convert.ToInt32(idr["NewDemoCount"]);
                objENLeads.UnusedCount = DBNull.Value == idr["UnusedLead"] ? 0 : Convert.ToInt32(idr["UnusedLead"]);
                objENLeads.Year = DBNull.Value == idr["YearNo"] ? null : Convert.ToString(idr["YearNo"]);
                objENLeads.Month = DBNull.Value == idr["MonthName"] ? null : Convert.ToString(idr["MonthName"]);
                objENLeads.createdby = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
        }	
        private void ConstructObjectGetYourPreSalesLeadListbyidnew(IDataReader idr, enLeadManagementSubscription objENLeads, string Action)
        {
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.CountryID = DBNull.Value == idr["CountryId"] ? 0 : Convert.ToInt32(idr["CountryId"]);
            objENLeads.StateId = DBNull.Value == idr["StateId"] ? 0 : Convert.ToInt32(idr["StateId"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.Mobile = DBNull.Value == idr["ContactNumber"] ? null : Convert.ToString(idr["ContactNumber"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.Designation = DBNull.Value == idr["Designation"] ? null : Convert.ToString(idr["Designation"]);
            objENLeads.StateName = DBNull.Value == idr["StateName"] ? null : Convert.ToString(idr["StateName"]);
            objENLeads.DiscriptionMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
            objENLeads.RequestedDate = DBNull.Value == idr["RequestedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RequestedDate"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.OutboundLeadSource = DBNull.Value == idr["OutboundLeadSource"] ? null : Convert.ToString(idr["OutboundLeadSource"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objENLeads.LeadType = DBNull.Value == idr["LeadType"] ? null : Convert.ToString(idr["LeadType"]);
            objENLeads.UpdatedDate = DBNull.Value == idr["UpdateDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdateDate"]);
            objENLeads.ContactCountryCode = DBNull.Value == idr["ContactCountryCode"] ? null : Convert.ToString(idr["ContactCountryCode"]);
            objENLeads.TelephoneCountryCode = DBNull.Value == idr["TelephoneCountryCode"] ? null : Convert.ToString(idr["TelephoneCountryCode"]);
            objENLeads.Phone = DBNull.Value == idr["Telephone"] ? null : Convert.ToString(idr["Telephone"]);


        }

        private void ConstructObjectListForLeadStatusReport(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.TotalLead = DBNull.Value == idr["TotalLead"] ? 0 : Convert.ToInt32(idr["TotalLead"]);
            objENLeads.TotalOpenLead = DBNull.Value == idr["TotalOpenLead"] ? 0 : Convert.ToInt32(idr["TotalOpenLead"]);
            objENLeads.OpenLead = DBNull.Value == idr["OpenLead"] ? 0 : Convert.ToInt32(idr["OpenLead"]);
            objENLeads.LostLead = DBNull.Value == idr["LostLead"] ? 0 : Convert.ToInt32(idr["LostLead"]);
            objENLeads.WonLead = DBNull.Value == idr["WonLead"] ? 0 : Convert.ToInt32(idr["WonLead"]);
            objENLeads.UnusedCount = DBNull.Value == idr["UnusedLead"] ? 0 : Convert.ToInt32(idr["UnusedLead"]);
            objENLeads.FollowUpLead = DBNull.Value == idr["FollowUpLead"] ? 0 : Convert.ToInt32(idr["FollowUpLead"]);
            objENLeads.noFollowUpLead = DBNull.Value == idr["noFollowUpLead"] ? 0 : Convert.ToInt32(idr["noFollowUpLead"]);            
            objENLeads.INRInvoice = DBNull.Value == idr["INRInvoice"] ? 0 : Convert.ToInt32(idr["INRInvoice"]);
            objENLeads.DemoCount = DBNull.Value == idr["DemoCount"] ? 0 : Convert.ToInt32(idr["DemoCount"]);
            objENLeads.INRTotalInvoiceAmount = DBNull.Value == idr["INRTotalInvoiceAmount"] ? null : Convert.ToString(idr["INRTotalInvoiceAmount"]);
            objENLeads.USDInvoice = DBNull.Value == idr["USDInvoice"] ? 0 : Convert.ToInt32(idr["USDInvoice"]);
            objENLeads.USDTotalInvoiceAmount = DBNull.Value == idr["USDTotalInvoiceAmount"] ? null : Convert.ToString(idr["USDTotalInvoiceAmount"]);
            objENLeads.WONInvoice = DBNull.Value == idr["WONInvoice"] ? 0 : Convert.ToInt32(idr["WONInvoice"]);
            objENLeads.WONTotalInvoiceAmount = DBNull.Value == idr["WONTotalInvoiceAmount"] ? null : Convert.ToString(idr["WONTotalInvoiceAmount"]);
            objENLeads.YENInvoice = DBNull.Value == idr["YENInvoice"] ? 0 : Convert.ToInt32(idr["YENInvoice"]);
            objENLeads.YENTotalInvoiceAmount = DBNull.Value == idr["YENTotalInvoiceAmount"] ? null : Convert.ToString(idr["YENTotalInvoiceAmount"]);
            objENLeads.EUROInvoice = DBNull.Value == idr["EUROInvoice"] ? 0 : Convert.ToInt32(idr["EUROInvoice"]);
            objENLeads.EUROTotalInvoiceAmount = DBNull.Value == idr["EUROTotalInvoiceAmount"] ? null : Convert.ToString(idr["EUROTotalInvoiceAmount"]);

        }

        private void ConstructObjectListForEmployeeRecords(IDataReader idr, enLeadManagementSubscription enUser_)
        {
            enUser_.Id = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enUser_.Name = DBNull.Value == idr["Fname"] ? null : Convert.ToString(idr["Fname"]);
            enUser_.ReportTo = DBNull.Value == idr["ReportTo"] ? 0 : Convert.ToInt32(idr["ReportTo"]);
            enUser_.Title = DBNull.Value == idr["Role"] ? null : Convert.ToString(idr["Role"]);
            enUser_.UniqID = DBNull.Value == idr["UniqID"] ? 0 : Convert.ToInt32(idr["UniqID"]);

        }

        private void ConstructObjectUserDetails(IDataReader idr, enLeadManagementSubscription enUser_)
        {
            enUser_.Id = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            enUser_.Name = DBNull.Value == idr["Fname"] ? null : Convert.ToString(idr["Fname"]);



        }

        private void ConstructObjectDEMODicussion(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.DemoDiscussion = DBNull.Value == idr["demodescussion"] ? null : Convert.ToString(idr["demodescussion"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.SAMaskEmail_OpenID = DBNull.Value == idr["maskemailopenid"] ? 0 : Convert.ToInt32(idr["maskemailopenid"]);
            objENLeads.LastDemoDate = DBNull.Value == idr["lastdemodate"] ? (DateTime?)null : Convert.ToDateTime(idr["lastdemodate"]);
            objENLeads.createdby = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);

        }

        private void ConstructObjectSalesPackageExpiry(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            objENLeads.DateTo = DBNull.Value == idr["ToDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ToDate"]);
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
        }
        private void ConstructObject(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            objENLeads.DateTo = DBNull.Value == idr["ToDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ToDate"]);
        }
        private void ConstructObjectGetYourLeadList(IDataReader idr, enLeadManagementSubscription objENLeads, string Action)
        {
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.Phone = DBNull.Value == idr["ContactNumber"] ? null : Convert.ToString(idr["ContactNumber"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.StateName = DBNull.Value == idr["StateName"] ? null : Convert.ToString(idr["StateName"]);
            objENLeads.DiscriptionMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
            objENLeads.RequestedDate = DBNull.Value == idr["RequestedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RequestedDate"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.OutboundLeadSource = DBNull.Value == idr["OutboundLeadSource"] ? null : Convert.ToString(idr["OutboundLeadSource"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objENLeads.LeadType = DBNull.Value == idr["LeadType"] ? null : Convert.ToString(idr["LeadType"]);
            objENLeads.UpdatedDate = DBNull.Value == idr["UpdateDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdateDate"]);
            if (Action == "OpenLead")
            {
                objENLeads.SetFollowUp = DBNull.Value == idr["Set_Follow_up"] ? (DateTime?)null : Convert.ToDateTime(idr["Set_Follow_up"]);
                objENLeads.ExpectedCloseDate = DBNull.Value == idr["Expected_Close_Date"] ? (DateTime?)null : Convert.ToDateTime(idr["Expected_Close_Date"]);
                objENLeads.Amount = DBNull.Value == idr["Amount"] ? null : Convert.ToString(idr["Amount"]);
                objENLeads.files = DBNull.Value == idr["Attechments"] ? null : Convert.ToString(idr["Attechments"]);
                objENLeads.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
                objENLeads.Invoice = DBNull.Value == idr["InvoiceNumber"] ? null : Convert.ToString(idr["InvoiceNumber"]);
                objENLeads.LostReason = DBNull.Value == idr["LostReason"] ? null : Convert.ToString(idr["LostReason"]);
                objENLeads.CRMLeadStatus = DBNull.Value == idr["CRM_LeadStatus"] ? null : Convert.ToString(idr["CRM_LeadStatus"]);
                objENLeads.RemainingPaymentDate = DBNull.Value == idr["RemainingPaymentDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RemainingPaymentDate"]);
            }
        }




        private void ConstructObjectGetYourLeadListWithMailChain(IDataReader idr, enLeadManagementSubscription objENLeads, string Action)
        {
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.Phone = DBNull.Value == idr["ContactNumber"] ? null : Convert.ToString(idr["ContactNumber"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.StateName = DBNull.Value == idr["StateName"] ? null : Convert.ToString(idr["StateName"]);
            objENLeads.DiscriptionMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
            objENLeads.RequestedDate = DBNull.Value == idr["RequestedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RequestedDate"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.OutboundLeadSource = DBNull.Value == idr["OutboundLeadSource"] ? null : Convert.ToString(idr["OutboundLeadSource"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objENLeads.LeadType = DBNull.Value == idr["LeadType"] ? null : Convert.ToString(idr["LeadType"]);
            objENLeads.MailChain = DBNull.Value == idr["MailChain"] ? null : Convert.ToString(idr["MailChain"]);
            objENLeads.ProductType = DBNull.Value == idr["ProductType"] ? null : Convert.ToString(idr["ProductType"]);
            objENLeads.UpdatedDate = DBNull.Value == idr["UpdateDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdateDate"]);
            if (Action == "OpenLead")
            {
                objENLeads.SetFollowUp = DBNull.Value == idr["Set_Follow_up"] ? (DateTime?)null : Convert.ToDateTime(idr["Set_Follow_up"]);
                objENLeads.ExpectedCloseDate = DBNull.Value == idr["Expected_Close_Date"] ? (DateTime?)null : Convert.ToDateTime(idr["Expected_Close_Date"]);
                objENLeads.Amount = DBNull.Value == idr["Amount"] ? null : Convert.ToString(idr["Amount"]);
                objENLeads.files = DBNull.Value == idr["Attechments"] ? null : Convert.ToString(idr["Attechments"]);
                objENLeads.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
                objENLeads.Invoice = DBNull.Value == idr["InvoiceNumber"] ? null : Convert.ToString(idr["InvoiceNumber"]);
                objENLeads.LostReason = DBNull.Value == idr["LostReason"] ? null : Convert.ToString(idr["LostReason"]);
                objENLeads.CRMLeadStatus = DBNull.Value == idr["CRM_LeadStatus"] ? null : Convert.ToString(idr["CRM_LeadStatus"]);
                objENLeads.RemainingPaymentDate = DBNull.Value == idr["RemainingPaymentDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RemainingPaymentDate"]);
            }
        }

        private void ConstructObjectGetYourLeadListbyid(IDataReader idr, enLeadManagementSubscription objENLeads, string Action)
        {
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.CountryID = DBNull.Value == idr["CountryId"] ? 0 : Convert.ToInt32(idr["CountryId"]);
            objENLeads.StateId = DBNull.Value == idr["StateId"] ? 0 : Convert.ToInt32(idr["StateId"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.Mobile = DBNull.Value == idr["ContactNumber"] ? null : Convert.ToString(idr["ContactNumber"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.StateName = DBNull.Value == idr["StateName"] ? null : Convert.ToString(idr["StateName"]);
            objENLeads.DiscriptionMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
            objENLeads.RequestedDate = DBNull.Value == idr["RequestedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RequestedDate"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.OutboundLeadSource = DBNull.Value == idr["OutboundLeadSource"] ? null : Convert.ToString(idr["OutboundLeadSource"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objENLeads.LeadType = DBNull.Value == idr["LeadType"] ? null : Convert.ToString(idr["LeadType"]);
            objENLeads.UpdatedDate = DBNull.Value == idr["UpdateDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdateDate"]);

            objENLeads.SetFollowUp = DBNull.Value == idr["Set_Follow_up"] ? (DateTime?)null : Convert.ToDateTime(idr["Set_Follow_up"]);
            objENLeads.ExpectedCloseDate = DBNull.Value == idr["Expected_Close_Date"] ? (DateTime?)null : Convert.ToDateTime(idr["Expected_Close_Date"]);
            objENLeads.Amount = DBNull.Value == idr["Amount"] ? null : Convert.ToString(idr["Amount"]);
            objENLeads.files = DBNull.Value == idr["Attechments"] ? null : Convert.ToString(idr["Attechments"]);
            objENLeads.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
            objENLeads.Invoice = DBNull.Value == idr["InvoiceNumber"] ? null : Convert.ToString(idr["InvoiceNumber"]);
            objENLeads.LostReason = DBNull.Value == idr["LostReason"] ? null : Convert.ToString(idr["LostReason"]);
            objENLeads.CRMLeadStatus = DBNull.Value == idr["CRM_LeadStatus"] ? null : Convert.ToString(idr["CRM_LeadStatus"]);
            objENLeads.RemainingPaymentDate = DBNull.Value == idr["RemainingPaymentDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RemainingPaymentDate"]);
            objENLeads.Phone = DBNull.Value == idr["Telephone"] ? null : Convert.ToString(idr["Telephone"]);
            objENLeads.PaymentOption = DBNull.Value == idr["PaymentOption"] ? null : Convert.ToString(idr["PaymentOption"]);
            objENLeads.PaymentTerms = DBNull.Value == idr["PaymentTerms"] ? null : Convert.ToString(idr["PaymentTerms"]);
            objENLeads.RemainingAmount = DBNull.Value == idr["RemainingAmount"] ? null : Convert.ToString(idr["RemainingAmount"]);
        }


        private void ConstructObjectGetYourLeadListbyidnew(IDataReader idr, enLeadManagementSubscription objENLeads, string Action)
        {
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.CountryID = DBNull.Value == idr["CountryId"] ? 0 : Convert.ToInt32(idr["CountryId"]);
            objENLeads.StateId = DBNull.Value == idr["StateId"] ? 0 : Convert.ToInt32(idr["StateId"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.Mobile = DBNull.Value == idr["ContactNumber"] ? null : Convert.ToString(idr["ContactNumber"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.Designation = DBNull.Value == idr["Designation"] ? null : Convert.ToString(idr["Designation"]);
            objENLeads.StateName = DBNull.Value == idr["StateName"] ? null : Convert.ToString(idr["StateName"]);
            objENLeads.DiscriptionMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
            objENLeads.RequestedDate = DBNull.Value == idr["RequestedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RequestedDate"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.OutboundLeadSource = DBNull.Value == idr["OutboundLeadSource"] ? null : Convert.ToString(idr["OutboundLeadSource"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objENLeads.LeadType = DBNull.Value == idr["LeadType"] ? null : Convert.ToString(idr["LeadType"]);
            objENLeads.UpdatedDate = DBNull.Value == idr["UpdateDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdateDate"]);

            objENLeads.SetFollowUp = DBNull.Value == idr["Set_Follow_up"] ? (DateTime?)null : Convert.ToDateTime(idr["Set_Follow_up"]);
            objENLeads.ExpectedCloseDate = DBNull.Value == idr["Expected_Close_Date"] ? (DateTime?)null : Convert.ToDateTime(idr["Expected_Close_Date"]);
            objENLeads.Amount = DBNull.Value == idr["Amount"] ? null : Convert.ToString(idr["Amount"]);
            objENLeads.files = DBNull.Value == idr["Attechments"] ? null : Convert.ToString(idr["Attechments"]);
            objENLeads.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
            objENLeads.Invoice = DBNull.Value == idr["InvoiceNumber"] ? null : Convert.ToString(idr["InvoiceNumber"]);
            objENLeads.LostReason = DBNull.Value == idr["LostReason"] ? null : Convert.ToString(idr["LostReason"]);
            objENLeads.CRMLeadStatus = DBNull.Value == idr["CRM_LeadStatus"] ? null : Convert.ToString(idr["CRM_LeadStatus"]);
            objENLeads.RemainingPaymentDate = DBNull.Value == idr["RemainingPaymentDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RemainingPaymentDate"]);
            objENLeads.Phone = DBNull.Value == idr["Telephone"] ? null : Convert.ToString(idr["Telephone"]);
            objENLeads.PaymentOption = DBNull.Value == idr["PaymentOption"] ? null : Convert.ToString(idr["PaymentOption"]);
            objENLeads.PaymentTerms = DBNull.Value == idr["PaymentTerms"] ? null : Convert.ToString(idr["PaymentTerms"]);
            objENLeads.RemainingAmount = DBNull.Value == idr["RemainingAmount"] ? null : Convert.ToString(idr["RemainingAmount"]);

            objENLeads.ContactCountryCode = DBNull.Value == idr["ContactCountryCode"] ? null : Convert.ToString(idr["ContactCountryCode"]);
            objENLeads.TelephoneCountryCode = DBNull.Value == idr["TelephoneCountryCode"] ? null : Convert.ToString(idr["TelephoneCountryCode"]);
            objENLeads.ProductType = DBNull.Value == idr["ProductType"] ? null : Convert.ToString(idr["ProductType"]);
            objENLeads.LeadMessage = DBNull.Value == idr["LeadMessage"] ? null : Convert.ToString(idr["LeadMessage"]);


        }


        private void ConstructObjectGetYourCrmLeadListbyidnew(IDataReader idr, enLeadManagementSubscription objENLeads, string Action)
        {
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.CountryID = DBNull.Value == idr["CountryId"] ? 0 : Convert.ToInt32(idr["CountryId"]);
            objENLeads.StateId = DBNull.Value == idr["StateId"] ? 0 : Convert.ToInt32(idr["StateId"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.Mobile = DBNull.Value == idr["ContactNumber"] ? null : Convert.ToString(idr["ContactNumber"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.Designation = DBNull.Value == idr["Designation"] ? null : Convert.ToString(idr["Designation"]);
            objENLeads.StateName = DBNull.Value == idr["StateName"] ? null : Convert.ToString(idr["StateName"]);
            objENLeads.DiscriptionMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
            objENLeads.RequestedDate = DBNull.Value == idr["RequestedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RequestedDate"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.OutboundLeadSource = DBNull.Value == idr["OutboundLeadSource"] ? null : Convert.ToString(idr["OutboundLeadSource"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objENLeads.LeadType = DBNull.Value == idr["LeadType"] ? null : Convert.ToString(idr["LeadType"]);
            objENLeads.UpdatedDate = DBNull.Value == idr["UpdateDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdateDate"]);

            objENLeads.SetFollowUp = DBNull.Value == idr["Set_Follow_up"] ? (DateTime?)null : Convert.ToDateTime(idr["Set_Follow_up"]);
            objENLeads.ExpectedCloseDate = DBNull.Value == idr["Expected_Close_Date"] ? (DateTime?)null : Convert.ToDateTime(idr["Expected_Close_Date"]);
            objENLeads.Amount = DBNull.Value == idr["Amount"] ? null : Convert.ToString(idr["Amount"]);
            objENLeads.files = DBNull.Value == idr["Attechments"] ? null : Convert.ToString(idr["Attechments"]);
            objENLeads.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
            objENLeads.Invoice = DBNull.Value == idr["InvoiceNumber"] ? null : Convert.ToString(idr["InvoiceNumber"]);
            objENLeads.LostReason = DBNull.Value == idr["LostReason"] ? null : Convert.ToString(idr["LostReason"]);
            objENLeads.CRMLeadStatus = DBNull.Value == idr["CRM_LeadStatus"] ? null : Convert.ToString(idr["CRM_LeadStatus"]);
            objENLeads.RemainingPaymentDate = DBNull.Value == idr["RemainingPaymentDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RemainingPaymentDate"]);
            objENLeads.Phone = DBNull.Value == idr["Telephone"] ? null : Convert.ToString(idr["Telephone"]);
            objENLeads.PaymentOption = DBNull.Value == idr["PaymentOption"] ? null : Convert.ToString(idr["PaymentOption"]);
            objENLeads.PaymentTerms = DBNull.Value == idr["PaymentTerms"] ? null : Convert.ToString(idr["PaymentTerms"]);
            objENLeads.RemainingAmount = DBNull.Value == idr["RemainingAmount"] ? null : Convert.ToString(idr["RemainingAmount"]);

            objENLeads.ContactCountryCode = DBNull.Value == idr["ContactCountryCode"] ? null : Convert.ToString(idr["ContactCountryCode"]);
            objENLeads.TelephoneCountryCode = DBNull.Value == idr["TelephoneCountryCode"] ? null : Convert.ToString(idr["TelephoneCountryCode"]);


        }




        private void ConstructObjectFollowupDicussion(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Last_Followup_Discussion = DBNull.Value == idr["Last_Followup_Discussion"] ? null : Convert.ToString(idr["Last_Followup_Discussion"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.SAMaskEmail_OpenID = DBNull.Value == idr["SAMaskEmail_OpenID"] ? 0 : Convert.ToInt32(idr["SAMaskEmail_OpenID"]);
            objENLeads.SetFollowUp = DBNull.Value == idr["Set_Follow_up"] ? (DateTime?)null : Convert.ToDateTime(idr["Set_Follow_up"]);
            objENLeads.createdby = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            objENLeads.files = DBNull.Value == idr["Attechments"] ? null : Convert.ToString(idr["Attechments"]);
        }
        private void ConstructObjectListForUser(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
        }

        private void ConstructObjectGetAssignPerson(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.AssignSalesEmail = DBNull.Value == idr["AssignPerson"] ? null : Convert.ToString(idr["AssignPerson"]);
            objENLeads.CCperson = DBNull.Value == idr["CCPerson"] ? null : Convert.ToString(idr["CCPerson"]);

        }

        private void ConstructObjectCRMEMail(IDataReader idr, enLeadManagementSubscription objENLeads)
        {

            objENLeads.CRM = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);

        }
        private void ConstructObjectMessage(IDataReader idr, enLeadManagementSubscription objENLeads)
        {

            objENLeads.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);

        }
        private void ConstructObjectListForCRMCustomerRelation(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);

            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.CorpEmail = DBNull.Value == idr["FirstContactEmail"] ? null : Convert.ToString(idr["FirstContactEmail"]);
            objENLeads.Name = DBNull.Value == idr["FirstContactName"] ? null : Convert.ToString(idr["FirstContactName"]);
            objENLeads.Phone = DBNull.Value == idr["FirstContactNumber"] ? null : Convert.ToString(idr["FirstContactNumber"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedOn"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedOn"]);

        }

        private void ConstructObjectListForRemainingPaymentdetails(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Id = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.CountryID = DBNull.Value == idr["CountryFID"] ? 0 : Convert.ToInt32(idr["CountryFID"]);
            objENLeads.AssignSalesEmail = DBNull.Value == idr["CreatedByEmail"] ? null : Convert.ToString(idr["CreatedByEmail"]);
            objENLeads.CRMEmail = DBNull.Value == idr["CRMEmail"] ? null : Convert.ToString(idr["CRMEmail"]);
            objENLeads.RemainingAmount = DBNull.Value == idr["RemainingAmount"] ? null : Convert.ToString(idr["RemainingAmount"]);
            objENLeads.createdby = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            objENLeads.RemainingPaymentDate = DBNull.Value == idr["RemainingPaymentDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RemainingPaymentDate"]);

        }

        //--------------------------------------------------------------------------------------
        private void ConstructObjectListForLeads(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);

            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.Organisation = DBNull.Value == idr["Organisation"] ? null : Convert.ToString(idr["Organisation"]);
            objENLeads.CountryID = DBNull.Value == idr["Country"] ? 0 : Convert.ToInt32(idr["Country"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);


            objENLeads.StepCounter = DBNull.Value == idr["stepcounter"] ? 0 : Convert.ToInt32(idr["stepcounter"]);
            objENLeads.Phone = DBNull.Value == idr["Phone"] ? null : Convert.ToString(idr["Phone"]);
            objENLeads.module = DBNull.Value == idr["module"] ? null : Convert.ToString(idr["module"]);
            objENLeads.InterestArea = DBNull.Value == idr["InterestArea"] ? null : Convert.ToString(idr["InterestArea"]);
            objENLeads.Subject = DBNull.Value == idr["Subject"] ? null : Convert.ToString(idr["Subject"]);
           

            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.AssignDate = DBNull.Value == idr["AssignDate"] ? (DateTime?)null : Convert.ToDateTime(idr["AssignDate"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);

            objENLeads.AssignTo = DBNull.Value == idr["AssignTo"] ? null : Convert.ToString(idr["AssignTo"]);
            objENLeads.Designation = DBNull.Value == idr["Designation"] ? null : Convert.ToString(idr["Designation"]);
            objENLeads.IPAddress = DBNull.Value == idr["IPAddress"] ? null : Convert.ToString(idr["IPAddress"]);
            objENLeads.createdby = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);        
            objENLeads.DisapprovedRemark = DBNull.Value == idr["DisApproveRemark"] ? null : Convert.ToString(idr["DisApproveRemark"]);
            objENLeads.PaymentMode = DBNull.Value == idr["PaymentMode"] ? null : Convert.ToString(idr["PaymentMode"]);
            objENLeads.files = DBNull.Value == idr["SourceDocument"] ? null : Convert.ToString(idr["SourceDocument"]);
            objENLeads.ModuleName = DBNull.Value == idr["ModuleName"] ? null : Convert.ToString(idr["ModuleName"]);
            objENLeads.RegionName = DBNull.Value == idr["RegionName"] ? null : Convert.ToString(idr["RegionName"]);
            
        }
        private void ConstructObjectListForCRMLeads(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);

            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.Organisation = DBNull.Value == idr["Organisation"] ? null : Convert.ToString(idr["Organisation"]);
            objENLeads.CountryID = DBNull.Value == idr["Country"] ? 0 : Convert.ToInt32(idr["Country"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);


            objENLeads.StepCounter = DBNull.Value == idr["stepcounter"] ? 0 : Convert.ToInt32(idr["stepcounter"]);
            objENLeads.Phone = DBNull.Value == idr["Phone"] ? null : Convert.ToString(idr["Phone"]);
            objENLeads.module = DBNull.Value == idr["module"] ? null : Convert.ToString(idr["module"]);
            objENLeads.InterestArea = DBNull.Value == idr["InterestArea"] ? null : Convert.ToString(idr["InterestArea"]);
            objENLeads.Subject = DBNull.Value == idr["Subject"] ? null : Convert.ToString(idr["Subject"]);


            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.AssignDate = DBNull.Value == idr["AssignDate"] ? (DateTime?)null : Convert.ToDateTime(idr["AssignDate"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);

            objENLeads.AssignTo = DBNull.Value == idr["AssignTo"] ? null : Convert.ToString(idr["AssignTo"]);
            objENLeads.Designation = DBNull.Value == idr["Designation"] ? null : Convert.ToString(idr["Designation"]);
            objENLeads.IPAddress = DBNull.Value == idr["IPAddress"] ? null : Convert.ToString(idr["IPAddress"]);
            objENLeads.createdby = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            objENLeads.DisapprovedRemark = DBNull.Value == idr["DisApproveRemark"] ? null : Convert.ToString(idr["DisApproveRemark"]);
            objENLeads.PaymentMode = DBNull.Value == idr["PaymentMode"] ? null : Convert.ToString(idr["PaymentMode"]);
            objENLeads.files = DBNull.Value == idr["SourceDocument"] ? null : Convert.ToString(idr["SourceDocument"]);
            objENLeads.Trader = DBNull.Value == idr["Trader"] ? null : Convert.ToString(idr["Trader"]);
            objENLeads.Credit = DBNull.Value == idr["Credit"] ? null : Convert.ToString(idr["Credit"]);
            objENLeads.Renew = DBNull.Value == idr["Renew"] ? null : Convert.ToString(idr["Renew"]);

        }
        private void ConstructObjectListForChemicalPriceRange(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.ProductId = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            objENLeads.Chemical_Range = DBNull.Value == idr["ChemicalRange"] ? null : Convert.ToString(idr["ChemicalRange"]);
            objENLeads.Chemical_RangeID = DBNull.Value == idr["RangeID"] ? 0 : Convert.ToInt32(idr["RangeID"]);
        }
        private void ConstructObjectListForMarketanalysisRange(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.ProductId = DBNull.Value == idr["Product"] ? 0 : Convert.ToInt32(idr["Product"]);
            objENLeads.MarketRangeName = DBNull.Value == idr["MarketRange"] ? null : Convert.ToString(idr["MarketRange"]);
            objENLeads.MarketRangeID = DBNull.Value == idr["RangeID"] ? 0 : Convert.ToInt32(idr["RangeID"]);
        }
        private void ConstructObjectListForState(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.StateId = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            objENLeads.StateName = DBNull.Value == idr["State"] ? null : Convert.ToString(idr["State"]);
        }
        private void ConstructObjectListForProduct(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.ProductId = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            objENLeads.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
        }
        private void ConstructObjectListFordataanalyst(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.AssignAnalystId = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            objENLeads.AssignAnalyst = DBNull.Value == idr["Fname"] ? null : Convert.ToString(idr["Fname"]);
        }
        private void ConstructObjectListForModule(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.ModuleId = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            objENLeads.ModuleName = DBNull.Value == idr["ModuleName"] ? null : Convert.ToString(idr["ModuleName"]);
        }
        private void ConstructObjectListForClintEmail(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.UserId = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            objENLeads.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            objENLeads.Name = DBNull.Value == idr["ClintName"] ? null : Convert.ToString(idr["ClintName"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
        }
        private void ConstructObjectListForClintEmailbyid(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.UserId = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            objENLeads.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            objENLeads.Name = DBNull.Value == idr["ClintName"] ? null : Convert.ToString(idr["ClintName"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.Phone = DBNull.Value == idr["FirstContactNumber"] ? null : Convert.ToString(idr["FirstContactNumber"]);
            objENLeads.CrmId = DBNull.Value == idr["CrmId"] ? 0 : Convert.ToInt32(idr["CrmId"]);
            objENLeads.CRM = DBNull.Value == idr["CRM"] ? null : Convert.ToString(idr["CRM"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);

        }

        private void ConstructObjectListForClintEmailByCompnayId(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.UserId = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            objENLeads.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            
        }
        private void ConstructObjectListForCompany(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.CompanyId = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
        }
        private void ConstructObjectListForCountry(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.CountryID = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
        }

        private void ConstructObjectGetPresalesManager(IDataReader idr, enLeadManagementSubscription objENLeads, string Action)
        {
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.Phone = DBNull.Value == idr["ContactNumber"] ? null : Convert.ToString(idr["ContactNumber"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.StateName = DBNull.Value == idr["StateName"] ? null : Convert.ToString(idr["StateName"]);
            objENLeads.DiscriptionMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
            objENLeads.RequestedDate = DBNull.Value == idr["RequestedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RequestedDate"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.OutboundLeadSource = DBNull.Value == idr["OutboundLeadSource"] ? null : Convert.ToString(idr["OutboundLeadSource"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objENLeads.LeadType = DBNull.Value == idr["LeadType"] ? null : Convert.ToString(idr["LeadType"]);
            objENLeads.UpdatedDate = DBNull.Value == idr["UpdateDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdateDate"]);
            objENLeads.createdby = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);

        }
        private void ConstructObjectInsertStatus(IDataReader idr, enLeadManagementSubscription enUser_)
        {
            enUser_.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);

        }
        private void ConstructObjectLeadDatainGrid(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.DisapprovedRemark = DBNull.Value == idr["Remark"] ? null : Convert.ToString(idr["Remark"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.ProductName = DBNull.Value == idr["ProductName"] ? null : Convert.ToString(idr["ProductName"]);
            objENLeads.ModuleName = DBNull.Value == idr["ModuleName"] ? null : Convert.ToString(idr["ModuleName"]);
            objENLeads.createdby = DBNull.Value == idr["createdby"] ? null : Convert.ToString(idr["createdby"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.DateFrom = DBNull.Value == idr["FromDate"] ? (DateTime?)null : Convert.ToDateTime(idr["FromDate"]);
            objENLeads.DateTo = DBNull.Value == idr["ToDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ToDate"]);
            objENLeads.UpdatedModulesName = DBNull.Value == idr["UpdatedModulesName"] ? null : Convert.ToString(idr["UpdatedModulesName"]);
            objENLeads.UpdateProductsName = DBNull.Value == idr["UpdateProductsName"] ? null : Convert.ToString(idr["UpdateProductsName"]);
            objENLeads.SubSubscriptionRemark = DBNull.Value == idr["SubSubscriptionRemark"] ? null : Convert.ToString(idr["SubSubscriptionRemark"]);
            objENLeads.SecondaryClintEmail = DBNull.Value == idr["SecondaryClintEmail"] ? null : Convert.ToString(idr["SecondaryClintEmail"]);
            objENLeads.RegionName = DBNull.Value == idr["RegionName"] ? null : Convert.ToString(idr["RegionName"]);
            objENLeads.Organisation = DBNull.Value == idr["Organisation"] ? null : Convert.ToString(idr["Organisation"]);
            objENLeads.PaymentMode = DBNull.Value == idr["PaymentMode"] ? null : Convert.ToString(idr["PaymentMode"]);
            objENLeads.files = DBNull.Value == idr["SourceDocument"] ? null : Convert.ToString(idr["SourceDocument"]);
            objENLeads.BillingEmail = DBNull.Value == idr["BillingEmail"] ? null : Convert.ToString(idr["BillingEmail"]);
            objENLeads.Trader = DBNull.Value == idr["Trader"] ? null : Convert.ToString(idr["Trader"]);
            objENLeads.Credit = DBNull.Value == idr["Credit"] ? null : Convert.ToString(idr["Credit"]);
            objENLeads.Renew = DBNull.Value == idr["Renew"] ? null : Convert.ToString(idr["Renew"]);
        }
        private void ConstructObjectCustSubscriptionDetails(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.FName = DBNull.Value == idr["fname"] ? null : Convert.ToString(idr["fname"]);
            objENLeads.LName = DBNull.Value == idr["Lname"] ? null : Convert.ToString(idr["Lname"]);
            objENLeads.SubscriptionType = DBNull.Value == idr["subscriptionType"] ? null : Convert.ToString(idr["subscriptionType"]);
            objENLeads.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            objENLeads.CountryOrRegionNames = DBNull.Value == idr["countryOrRegionNames"] ? null : Convert.ToString(idr["countryOrRegionNames"]);
            objENLeads.DateFrom = DBNull.Value == idr["FromDate"] ? (DateTime?)null : Convert.ToDateTime(idr["FromDate"]);
            objENLeads.DateTo = DBNull.Value == idr["ToDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ToDate"]);
            objENLeads.ProductName = DBNull.Value == idr["products"] ? null : Convert.ToString(idr["products"]);
            objENLeads.ModuleName = DBNull.Value == idr["Modules"] ? null : Convert.ToString(idr["Modules"]);
           
        }
        private void ConstructObjectLeadList(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);

            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.Organisation = DBNull.Value == idr["Organisation"] ? null : Convert.ToString(idr["Organisation"]);
            objENLeads.RegionId = DBNull.Value == idr["subscriptionType"] ? null : Convert.ToString(idr["subscriptionType"]);
            objENLeads.SubscriptionType = DBNull.Value == idr["RegionName"] ? null : Convert.ToString(idr["RegionName"]);
            objENLeads.AssignAnalystId = DBNull.Value == idr["AssignAnalyst"] ? 0 : Convert.ToInt32(idr["AssignAnalyst"]);
            objENLeads.AssignAnalyst = DBNull.Value == idr["AssignAnalystName"] ? null : Convert.ToString(idr["AssignAnalystName"]);
            objENLeads.DateFrom = DBNull.Value == idr["SubscriptionDateFrom"] ? (DateTime?)null : Convert.ToDateTime(idr["SubscriptionDateFrom"]);
            objENLeads.DateTo = DBNull.Value == idr["SubscriptionDateTo"] ? (DateTime?)null : Convert.ToDateTime(idr["SubscriptionDateTo"]);
            objENLeads.Subscription_Product = DBNull.Value == idr["Subscription_Product"] ? null : Convert.ToString(idr["Subscription_Product"]);
            objENLeads.CountryID = DBNull.Value == idr["Subscription_Country"] ? 0 : Convert.ToInt32(idr["Subscription_Country"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.Chemical_Range = DBNull.Value == idr["Chemical_Range"] ? null : Convert.ToString(idr["Chemical_Range"]);
            objENLeads.Market_Analysis_Range = DBNull.Value == idr["Market_Analysis_Range"] ? null : Convert.ToString(idr["Market_Analysis_Range"]);
            objENLeads.ProductName = DBNull.Value == idr["ProductsName"] ? null : Convert.ToString(idr["ProductsName"]);
            objENLeads.ModuleName = DBNull.Value == idr["ModulesName"] ? null : Convert.ToString(idr["ModulesName"]);
            objENLeads.SubSubscriptionRemark = DBNull.Value == idr["SubSubscriptionRemark"] ? null : Convert.ToString(idr["SubSubscriptionRemark"]);
            objENLeads.SecondaryClintEmail = DBNull.Value == idr["SecondaryClintEmail"] ? null : Convert.ToString(idr["SecondaryClintEmail"]);
            
        }
        private void ConstructObjectLeadListDetails(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.Phone = DBNull.Value == idr["Phone"] ? null : Convert.ToString(idr["Phone"]);
            objENLeads.Organisation = DBNull.Value == idr["Organisation"] ? null : Convert.ToString(idr["Organisation"]);
            objENLeads.UserType = DBNull.Value == idr["UserType"] ? null : Convert.ToString(idr["UserType"]);
            objENLeads.createdby = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.SubscriptionType = DBNull.Value == idr["subscriptionType"] ? null : Convert.ToString(idr["subscriptionType"]);
            objENLeads.AssignAnalystId = DBNull.Value == idr["AssignAnalyst"] ? 0 : Convert.ToInt32(idr["AssignAnalyst"]);
            objENLeads.AssignAnalyst = DBNull.Value == idr["AssignAnalystName"] ? null : Convert.ToString(idr["AssignAnalystName"]);
            objENLeads.DateFrom = DBNull.Value == idr["SubscriptionDateFrom"] ? (DateTime?)null : Convert.ToDateTime(idr["SubscriptionDateFrom"]);
            objENLeads.DateTo = DBNull.Value == idr["SubscriptionDateTo"] ? (DateTime?)null : Convert.ToDateTime(idr["SubscriptionDateTo"]);
            objENLeads.Subscription_Product = DBNull.Value == idr["Subscription_Product"] ? null : Convert.ToString(idr["Subscription_Product"]);
            objENLeads.CountryID = DBNull.Value == idr["Subscription_Country"] ? 0 : Convert.ToInt32(idr["Subscription_Country"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.Chemical_Range = DBNull.Value == idr["Chemical_Range"] ? null : Convert.ToString(idr["Chemical_Range"]);
            objENLeads.Market_Analysis_Range = DBNull.Value == idr["Market_Analysis_Range"] ? null : Convert.ToString(idr["Market_Analysis_Range"]);
            objENLeads.ProductName = DBNull.Value == idr["ProductsName"] ? null : Convert.ToString(idr["ProductsName"]);
            objENLeads.ModuleName = DBNull.Value == idr["ModulesName"] ? null : Convert.ToString(idr["ModulesName"]);
            objENLeads.SubSubscriptionRemark = DBNull.Value == idr["SubSubscriptionRemark"] ? null : Convert.ToString(idr["SubSubscriptionRemark"]);
            objENLeads.SecondaryClintEmail = DBNull.Value == idr["SecondaryClintEmail"] ? null : Convert.ToString(idr["SecondaryClintEmail"]);
            objENLeads.PaymentMode = DBNull.Value == idr["PaymentMode"] ? null : Convert.ToString(idr["PaymentMode"]);
            objENLeads.ClintCountry = DBNull.Value == idr["clintcountry"] ? null : Convert.ToString(idr["clintcountry"]);
        }
        private void ConstructObjectForsalespackage(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);

            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.ProductName = DBNull.Value == idr["ProductId"] ? null : Convert.ToString(idr["ProductId"]);
            objENLeads.module = DBNull.Value == idr["MenuId"] ? null : Convert.ToString(idr["MenuId"]);
            objENLeads.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.RegionName = DBNull.Value == idr["RegionName"] ? null : Convert.ToString(idr["RegionName"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.DateFrom = DBNull.Value == idr["FromDate"] ? (DateTime?)null : Convert.ToDateTime(idr["FromDate"]);
            objENLeads.DateTo = DBNull.Value == idr["ToDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ToDate"]);
            objENLeads.Trader = DBNull.Value == idr["Trader"] ? null : Convert.ToString(idr["Trader"]);
            objENLeads.Credit = DBNull.Value == idr["Credit"] ? null : Convert.ToString(idr["Credit"]);
            objENLeads.Renew = DBNull.Value == idr["Renew"] ? null : Convert.ToString(idr["Renew"]);
        }
        private void ConstructObjectForsalespackageById(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.ProductName = DBNull.Value == idr["ProductId"] ? null : Convert.ToString(idr["ProductId"]);
            objENLeads.module = DBNull.Value == idr["MenuId"] ? null : Convert.ToString(idr["MenuId"]);
            objENLeads.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.RegionName = DBNull.Value == idr["RegionName"] ? null : Convert.ToString(idr["RegionName"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.DateFrom = DBNull.Value == idr["FromDate"] ? (DateTime?)null : Convert.ToDateTime(idr["FromDate"]);
            objENLeads.DateTo = DBNull.Value == idr["ToDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ToDate"]);
            objENLeads.Chemical_Range = DBNull.Value == idr["ChemicalPricingRange"] ? null : Convert.ToString(idr["ChemicalPricingRange"]);
            objENLeads.Market_Analysis_Range = DBNull.Value == idr["MarketAnalysisRange"] ? null : Convert.ToString(idr["MarketAnalysisRange"]);
        }
        private void ConstructObjectForsalespackageByUserID(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.Subscription_Product = DBNull.Value == idr["ProductId"] ? null : Convert.ToString(idr["ProductId"]);
            objENLeads.module = DBNull.Value == idr["MenuId"] ? null : Convert.ToString(idr["MenuId"]);
            objENLeads.LeadId = DBNull.Value == idr["LeadId"] ? 0 : Convert.ToInt32(idr["LeadId"]);
            objENLeads.UserId = DBNull.Value == idr["UserId"] ? 0 : Convert.ToInt32(idr["UserId"]);
            objENLeads.SubscriptionType = DBNull.Value == idr["SubscriptionType"] ? null : Convert.ToString(idr["SubscriptionType"]);
            objENLeads.DateFrom = DBNull.Value == idr["FromDate"] ? (DateTime?)null : Convert.ToDateTime(idr["FromDate"]);
            objENLeads.DateTo = DBNull.Value == idr["ToDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ToDate"]);
            objENLeads.RegionId = DBNull.Value == idr["RegionsID"] ? null : Convert.ToString(idr["RegionsID"]);
            objENLeads.CountryName = DBNull.Value == idr["CountriesID"] ? null : Convert.ToString(idr["CountriesID"]);
            objENLeads.Chemical_Range = DBNull.Value == idr["ChemicalPricingRange"] ? null : Convert.ToString(idr["ChemicalPricingRange"]);
            objENLeads.Market_Analysis_Range = DBNull.Value == idr["MarketAnalysisRange"] ? null : Convert.ToString(idr["MarketAnalysisRange"]);
            objENLeads.Trader = DBNull.Value == idr["Trader"] ? null : Convert.ToString(idr["Trader"]);
            objENLeads.Renew = DBNull.Value == idr["Renew"] ? null : Convert.ToString(idr["Renew"]);
        }
        private void ConstructObjectForClintEmails(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]); 
        }
        private void ConstructObjectlistofOrganisation(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Organisation = DBNull.Value == idr["Organisation"] ? null : Convert.ToString(idr["Organisation"]);
        }

        private void ConstructObjectEmailbasedonOrganisation(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
        }

        private void ConstructObjectInsertCustomerCreditScore(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
        }
        private void ConstructObjectGetCreditBasedonEmail(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Credit = DBNull.Value == idr["Credit"] ? null : Convert.ToString(idr["Credit"]);
        }
        private void ConstructObjectListForsubscription(IDataReader idr, enLeadManagementSubscription objEN)
        {
            objEN.UserId = DBNull.Value == idr["UserId"] ? 0 : Convert.ToInt32(idr["UserId"]);
            objEN.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objEN.Phone = DBNull.Value == idr["Phone"] ? null : Convert.ToString(idr["Phone"]);
            objEN.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objEN.Subscribe = DBNull.Value == idr["Subscribe"] ? 0 : Convert.ToInt32(idr["Subscribe"]);
            objEN.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objEN.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objEN.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
        }
        private void ConstructObjectListForsubscriptionAll(IDataReader idr, enLeadManagementSubscription objEN)
        {
            objEN.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objEN.UserId = DBNull.Value == idr["UserId"] ? 0 : Convert.ToInt32(idr["UserId"]);
            objEN.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objEN.Phone = DBNull.Value == idr["Phone"] ? null : Convert.ToString(idr["Phone"]);
            objEN.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objEN.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objEN.DateFrom = DBNull.Value == idr["FromDate"] ? (DateTime?)null : Convert.ToDateTime(idr["FromDate"]);
            objEN.DateTo = DBNull.Value == idr["ToDate"] ? (DateTime?)null : Convert.ToDateTime(idr["ToDate"]);
            objEN.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objEN.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objEN.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objEN.ExpiryNotification = DBNull.Value == idr["ExpiryNotification"] ? 0 : Convert.ToInt32(idr["ExpiryNotification"]);

        }


        private void ConstructObjectListDeSelectAllCheck(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
        }


        private void ConstructObjectgetMoreContactDetails(IDataReader idr, enLeadManagementSubscription objENLeads)
        {
            objENLeads.Id = DBNull.Value == idr["id"] ? 0 : Convert.ToInt32(idr["id"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.Phone = DBNull.Value == idr["Phone"] ? null : Convert.ToString(idr["Phone"]);
            objENLeads.CorpEmail = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            objENLeads.Designation = DBNull.Value == idr["Designation"] ? null : Convert.ToString(idr["Designation"]);

        }



        private void ConstructObjectGetAllLeadListManager(IDataReader idr, enLeadManagementSubscription objENLeads, string Action)
        {
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
            objENLeads.Phone = DBNull.Value == idr["ContactNumber"] ? null : Convert.ToString(idr["ContactNumber"]);
            objENLeads.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
            objENLeads.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
            objENLeads.StateName = DBNull.Value == idr["StateName"] ? null : Convert.ToString(idr["StateName"]);
            objENLeads.DiscriptionMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
            objENLeads.RequestedDate = DBNull.Value == idr["RequestedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RequestedDate"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.OutboundLeadSource = DBNull.Value == idr["OutboundLeadSource"] ? null : Convert.ToString(idr["OutboundLeadSource"]);
            objENLeads.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
            objENLeads.LeadType = DBNull.Value == idr["LeadType"] ? null : Convert.ToString(idr["LeadType"]);
            objENLeads.UpdatedDate = DBNull.Value == idr["UpdateDate"] ? (DateTime?)null : Convert.ToDateTime(idr["UpdateDate"]);
            objENLeads.createdby = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
            objENLeads.DemoDiscussion = DBNull.Value == idr["DemoDiscussion"] ? null : Convert.ToString(idr["DemoDiscussion"]);
            if (Action == "OpenLead")
            {
                objENLeads.SetFollowUp = DBNull.Value == idr["Set_Follow_up"] ? (DateTime?)null : Convert.ToDateTime(idr["Set_Follow_up"]);
                objENLeads.ExpectedCloseDate = DBNull.Value == idr["Expected_Close_Date"] ? (DateTime?)null : Convert.ToDateTime(idr["Expected_Close_Date"]);
                objENLeads.Amount = DBNull.Value == idr["Amount"] ? null : Convert.ToString(idr["Amount"]);
                objENLeads.files = DBNull.Value == idr["Attechments"] ? null : Convert.ToString(idr["Attechments"]);
                objENLeads.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
                objENLeads.Invoice = DBNull.Value == idr["InvoiceNumber"] ? null : Convert.ToString(idr["InvoiceNumber"]);
                objENLeads.LostReason = DBNull.Value == idr["LostReason"] ? null : Convert.ToString(idr["LostReason"]);
                objENLeads.CRMLeadStatus = DBNull.Value == idr["CRM_LeadStatus"] ? null : Convert.ToString(idr["CRM_LeadStatus"]);
                objENLeads.RemainingPaymentDate = DBNull.Value == idr["RemainingPaymentDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RemainingPaymentDate"]);
            }
        }

        // CRM Pipeline page - one proc, four SEPARATE result sets, each tagged
        // with its column stage: Open For Renewal / In discussion / Renewal
        // Lost / Renewal Won. Result 1 is renewal-due companies; results 2-4
        // are SA_MaskEmailOpen_CRM rows with Status Open/Lost/Won.
        public List<enLeadManagementSubscription> GetCRMPipeline(int crmId)
        {
            List<enLeadManagementSubscription> list = new List<enLeadManagementSubscription>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "usp_LeadManagementSubscription_GetCRMPipeline",
                new SqlParameter("@CRMId", crmId)))
            {
                // Result 1 - Open For Renewal: CompanyName / CompanyId /
                // FirstContactName / FirstContactEmail / Expiredate.
                while (idr.Read())
                {
                    enLeadManagementSubscription o = new enLeadManagementSubscription();
                    o.Stage = "Open For Renewal";
                    o.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
                    o.CompanyId = DBNull.Value == idr["CompanyId"] ? 0 : Convert.ToInt32(idr["CompanyId"]);
                    o.Name = DBNull.Value == idr["FirstContactName"] ? null : Convert.ToString(idr["FirstContactName"]);
                    o.CorpEmail = DBNull.Value == idr["FirstContactEmail"] ? null : Convert.ToString(idr["FirstContactEmail"]);
                    o.ExpectedCloseDate = DBNull.Value == idr["Expiredate"] ? (DateTime?)null : Convert.ToDateTime(idr["Expiredate"]);
                    list.Add(o);
                }

                // Results 2-4 - SA_MaskEmailOpen_CRM rows, same column shape.
                if (idr.NextResult()) { ReadCRMOpenRows(idr, "In discussion", list); }
                if (idr.NextResult()) { ReadCRMOpenRows(idr, "Renewal Lost", list); }
                if (idr.NextResult()) { ReadCRMOpenRows(idr, "Renewal Won", list); }
            }

            return list;
        }

        private void ReadCRMOpenRows(IDataReader idr, string stage, List<enLeadManagementSubscription> list)
        {
            while (idr.Read())
            {
                enLeadManagementSubscription o = new enLeadManagementSubscription();
                o.Stage = stage;
                o.Id = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
                o.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
                o.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
                o.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
                o.Phone = DBNull.Value == idr["ContactNumber"] ? null : Convert.ToString(idr["ContactNumber"]);
                o.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
                o.LeadType = DBNull.Value == idr["LeadType"] ? null : Convert.ToString(idr["LeadType"]);
                o.OutboundLeadSource = DBNull.Value == idr["OutboundLeadSource"] ? null : Convert.ToString(idr["OutboundLeadSource"]);
                o.Amount = DBNull.Value == idr["Amount"] ? null : Convert.ToString(idr["Amount"]);
                o.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
                o.SetFollowUp = DBNull.Value == idr["Set_Follow_up"] ? (DateTime?)null : Convert.ToDateTime(idr["Set_Follow_up"]);
                o.ExpectedCloseDate = DBNull.Value == idr["Expected_Close_Date"] ? (DateTime?)null : Convert.ToDateTime(idr["Expected_Close_Date"]);
                o.CRMLeadStatus = DBNull.Value == idr["Lead_status"] ? null : Convert.ToString(idr["Lead_status"]);
                list.Add(o);
            }
        }

        // "Open" on the Customer detail page (reached from the Pipeline
        // "Open For Renewal" column) - inserts this company's renewal into
        // SA_MaskEmailOpen_CRM so it moves into the "In discussion" column.
        public int OpenRenewalCompany(int companyId, int crmId, string createdBy)
        {
            return SqlHelper.ExecuteNonQuery(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "usp_LeadManagementSubscription_OpenRenewalCompany",
                new SqlParameter("@CompanyId", companyId),
                new SqlParameter("@CRMId", crmId),
                new SqlParameter("@CreatedBy", (object)createdBy ?? DBNull.Value));
        }

        // Detail page for an In-discussion lead (one SA_MaskEmailOpen_CRM row
        // by ID), opened from the Pipeline "In discussion" column.
        public enLeadManagementSubscription GetCRMOpenLeadById(int id)
        {
            enLeadManagementSubscription o = null;

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "usp_LeadManagementSubscription_GetCRMOpenLeadById",
                new SqlParameter("@Id", id)))
            {
                if (idr.Read())
                {
                    o = new enLeadManagementSubscription();
                    o.Id = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
                    o.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
                    o.CorpEmail = DBNull.Value == idr["CorpEmail"] ? null : Convert.ToString(idr["CorpEmail"]);
                    o.CompanyName = DBNull.Value == idr["CompanyName"] ? null : Convert.ToString(idr["CompanyName"]);
                    o.ContactCountryCode = DBNull.Value == idr["ContactCountryCode"] ? null : Convert.ToString(idr["ContactCountryCode"]);
                    o.Phone = DBNull.Value == idr["ContactNumber"] ? null : Convert.ToString(idr["ContactNumber"]);
                    o.TelephoneCountryCode = DBNull.Value == idr["TelephoneCountryCode"] ? null : Convert.ToString(idr["TelephoneCountryCode"]);
                    o.Mobile = DBNull.Value == idr["Telephone"] ? null : Convert.ToString(idr["Telephone"]);
                    o.Designation = DBNull.Value == idr["Designation"] ? null : Convert.ToString(idr["Designation"]);
                    o.CountryName = DBNull.Value == idr["CountryName"] ? null : Convert.ToString(idr["CountryName"]);
                    o.StateName = DBNull.Value == idr["StateName"] ? null : Convert.ToString(idr["StateName"]);
                    o.DiscriptionMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);
                    o.OutboundLeadSource = DBNull.Value == idr["Source"] ? null : Convert.ToString(idr["Source"]);
                    o.LeadType = DBNull.Value == idr["LeadType"] ? null : Convert.ToString(idr["LeadType"]);
                    o.Status = DBNull.Value == idr["Status"] ? null : Convert.ToString(idr["Status"]);
                    o.CRMLeadStatus = DBNull.Value == idr["Lead_status"] ? null : Convert.ToString(idr["Lead_status"]);
                    o.Amount = DBNull.Value == idr["Amount"] ? null : Convert.ToString(idr["Amount"]);
                    o.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
                    o.SetFollowUp = DBNull.Value == idr["Set_Follow_up"] ? (DateTime?)null : Convert.ToDateTime(idr["Set_Follow_up"]);
                    o.ExpectedCloseDate = DBNull.Value == idr["Expected_Close_Date"] ? (DateTime?)null : Convert.ToDateTime(idr["Expected_Close_Date"]);
                    o.RequestedDate = DBNull.Value == idr["RequestedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["RequestedDate"]);
                    o.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
                    o.createdby = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
                    o.LostReason = DBNull.Value == idr["LostReason"] ? null : Convert.ToString(idr["LostReason"]);
                    o.files = DBNull.Value == idr["Attechments"] ? null : Convert.ToString(idr["Attechments"]);
                }
            }

            return o;
        }

        // Activity & follow-ups list for the CRM lead-detail page -
        // reads SA_MaskEmailOpen_LastFollowUpDiscussionCRM by open id.
        public List<enCRMFollowUp> GetCRMFollowUps(int openId)
        {
            List<enCRMFollowUp> list = new List<enCRMFollowUp>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "usp_LeadManagementSubscription_GetCRMFollowUps",
                new SqlParameter("@OpenId", openId)))
            {
                while (idr.Read())
                {
                    enCRMFollowUp f = new enCRMFollowUp();
                    f.Id = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
                    f.OpenId = DBNull.Value == idr["SAMaskEmail_OpenID"] ? 0 : Convert.ToInt32(idr["SAMaskEmail_OpenID"]);
                    f.Discussion = DBNull.Value == idr["Last_Followup_Discussion"] ? null : Convert.ToString(idr["Last_Followup_Discussion"]);
                    f.CreatedBy = DBNull.Value == idr["CreatedBy"] ? null : Convert.ToString(idr["CreatedBy"]);
                    f.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
                    f.DiscussionStatus = DBNull.Value == idr["DiscussionStatus"] ? null : Convert.ToString(idr["DiscussionStatus"]);
                    f.SetFollowUp = DBNull.Value == idr["Set_Follow_up"] ? (DateTime?)null : Convert.ToDateTime(idr["Set_Follow_up"]);
                    list.Add(f);
                }
            }

            return list;
        }

        // Contacts tab on the CRM lead-detail page - SA_Customer joined to
        // SA_CompanyMaster by company name (Primary / Secondary).
        public List<enCRMContact> GetCRMContacts(string companyName)
        {
            List<enCRMContact> list = new List<enCRMContact>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "usp_LeadManagementSubscription_GetCRMContacts",
                new SqlParameter("@CompanyName", (object)companyName ?? DBNull.Value)))
            {
                while (idr.Read())
                {
                    enCRMContact c = new enCRMContact();
                    c.Name = DBNull.Value == idr["Fname"] ? null : Convert.ToString(idr["Fname"]);
                    c.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
                    c.Phone = DBNull.Value == idr["Phone"] ? null : Convert.ToString(idr["Phone"]);
                    c.Usertype = DBNull.Value == idr["Usertype"] ? null : Convert.ToString(idr["Usertype"]);
                    list.Add(c);
                }
            }

            return list;
        }

        // Add-follow-up on the CRM lead-detail page - inserts one row into
        // SA_MaskEmailOpen_LastFollowUpDiscussionCRM.
        public int AddCRMFollowUp(enCRMFollowUp f)
        {
            return SqlHelper.ExecuteNonQuery(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "usp_LeadManagementSubscription_AddCRMFollowUp",
                new SqlParameter("@OpenId", f.OpenId),
                new SqlParameter("@Discussion", (object)f.Discussion ?? DBNull.Value),
                new SqlParameter("@DiscussionStatus", (object)f.DiscussionStatus ?? DBNull.Value),
                new SqlParameter("@SetFollowUp", f.SetFollowUp.HasValue ? (object)f.SetFollowUp.Value : DBNull.Value),
                new SqlParameter("@CreatedBy", (object)f.CreatedBy ?? DBNull.Value));
        }

        // "Mark won - start closing" modal on the CRM lead-detail page -
        // updates the deal-close columns on the SA_MaskEmailOpen_CRM row and
        // flips Status to Won.
        public bool MarkCRMWon(int id, decimal? amount, string currency, decimal? amountINR, string paymentOption, string paymentTerms, string invoiceNumber, string orderFrom)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_LeadManagementSubscription_MarkCRMWon", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                cmd.Parameters.Add(new SqlParameter("@Amount", (object)amount ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Currency", (object)currency ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@AmountINR", (object)amountINR ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PaymentOption", (object)paymentOption ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PaymentTerms", (object)paymentTerms ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@invoiceNumber", (object)invoiceNumber ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@OrderFrom", (object)orderFrom ?? DBNull.Value));
                var updatedRowsParam = new SqlParameter("@UpdatedRows", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(updatedRowsParam);
                conn.Open();
                cmd.ExecuteNonQuery();
                return updatedRowsParam.Value != DBNull.Value && Convert.ToInt32(updatedRowsParam.Value) > 0;
            }
        }

        // Lead summary "Edit" on the CRM lead-detail page - updates the
        // editable fields on one SA_MaskEmailOpen_CRM row.
        public int UpdateCRMOpenLead(enLeadManagementSubscription o)
        {
            return SqlHelper.ExecuteNonQuery(ApplicationSettings.DefaultConnectionString,
                CommandType.StoredProcedure, "usp_LeadManagementSubscription_UpdateCRMOpenLead",
                new SqlParameter("@Id", o.Id),
                new SqlParameter("@Status", (object)o.Status ?? DBNull.Value),
                new SqlParameter("@LeadType", (object)o.LeadType ?? DBNull.Value),
                new SqlParameter("@OutboundLeadSource", (object)o.OutboundLeadSource ?? DBNull.Value),
                new SqlParameter("@Message", (object)o.DiscriptionMessage ?? DBNull.Value),
                new SqlParameter("@Amount", string.IsNullOrEmpty(o.Amount) ? (object)DBNull.Value : o.Amount),
                new SqlParameter("@Currency", (object)o.Currency ?? DBNull.Value),
                new SqlParameter("@ExpectedCloseDate", o.ExpectedCloseDate.HasValue ? (object)o.ExpectedCloseDate.Value : DBNull.Value),
                new SqlParameter("@SetFollowUp", o.SetFollowUp.HasValue ? (object)o.SetFollowUp.Value : DBNull.Value),
                new SqlParameter("@LostReason", (object)o.LostReason ?? DBNull.Value));
        }
    }
}
