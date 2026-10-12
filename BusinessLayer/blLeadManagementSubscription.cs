using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = DataLayer.dlLeadManagementSubscription;
namespace BusinessLayer
{
    public class blLeadManagementSubscription
    {
            private enLeadManagementSubscription _enLeads = null;
            private DAL _objDAL = null;

            public enLeadManagementSubscription GetEntityReference
            {
                get { return this._enLeads; }
            }
            public blLeadManagementSubscription(enLeadManagementSubscription enLeads_)
            {
                this._enLeads = enLeads_;
            }
        public List<enLeadManagementSubscription> GetSalesPackageSubscriptionsList(enSearchFilter objChemical)
        {
            return GetDALReference().GetSalesPackageSubscriptionsList(objChemical);
        }
        public List<enLeadManagementSubscription> ReadAllClintEmail(enSearchFilter objChemical)
        {
            return GetDALReference().ReadAllClintEmail(objChemical);
        }
        public List<enLeadManagementSubscription> GetCountryByModuleWise(enSearchFilter objChemical)
        {
            return GetDALReference().GetCountryByModuleWise(objChemical);
        }

        public List<enLeadManagementSubscription> ReadProductByCoutryName(enSearchFilter objChemical)
        {
            return GetDALReference().ReadProductByCoutryName(objChemical);
        }
        public List<enLeadManagementSubscription> GetListForExpireSubscriptionNotification(enSearchFilter objChemical)
        {
            return GetDALReference().GetListForExpireSubscriptionNotification(objChemical);
        }
        public List<enLeadManagementSubscription> GetListofCRMLeads(enSearchFilter objChemical)
            {
                return GetDALReference().GetListofCRMLeads(objChemical);
            }
            public List<enLeadManagementSubscription> GetListofLeads(enSearchFilter objChemical)
            {
                return GetDALReference().GetListofLeads(objChemical);
            }
        public List<enLeadManagementSubscription> UpdateLeadApprovalStatus()
        {
            return GetDALReference().UpdateLeadApprovalStatus();
        }
        public List<enLeadManagementSubscription> CheckPaymentDetailsStatus()
        {
            return GetDALReference().CheckPaymentDetailsStatus();
        }

        public List<enLeadManagementSubscription> UpdateLeadDisApprovalStatus()
        {
            return GetDALReference().UpdateLeadDisApprovalStatus();
        }
        public List<enLeadManagementSubscription> InsertSubscriptionDetailsbyId()
        {
            return GetDALReference().InsertSubscriptionDetailsbyId();
        }
        public List<enLeadManagementSubscription> GetRecordByLeadId()
        {
            return GetDALReference().GetRecordByLeadId();
        }
        public List<enLeadManagementSubscription> GetRecordBySalesPackageID()
        {
            return GetDALReference().GetRecordBySalesPackageID();
        }
        public List<enLeadManagementSubscription> InsertPreSalesLeadRecord(enLeadManagementSubscription obj)
        {
            return GetDALReference().InsertPreSalesLeadRecord(obj);
        }


        public List<enLeadManagementSubscription> GetPreSalesLeadList(enSearchFilter objChemical)
        {
            return GetDALReference().GetPreSalesLeadList(objChemical);
        }


        public List<enLeadManagementSubscription> GetPreSalesListTL(enSearchFilter objChemical)
        {
            return GetDALReference().GetPreSalesListTL(objChemical);
        }

        public List<enLeadManagementSubscription> GetPreSalesListManager(enSearchFilter objChemical)
        {
            return GetDALReference().GetPreSalesListManager(objChemical);
        }

        public List<enLeadManagementSubscription> GetCustSubscriptionDetailsbyId()
        {
            return GetDALReference().GetCustSubscriptionDetailsbyId();
        }
        public List<enLeadManagementSubscription> GetCustSubscriptionDetailsbyIdOnupdate()
        {
            return GetDALReference().GetCustSubscriptionDetailsbyIdOnupdate();
        }
        
        public List<enLeadManagementSubscription> CheckExistsUser()
        {
            return GetDALReference().CheckExistsUser();
        }
        
        public List<enLeadManagementSubscription> DisApprovalMessage()
        {
            return GetDALReference().DisApprovalMessage();
        }
        public List<enLeadManagementSubscription> SavePaymentDetails()
        {
            return GetDALReference().SavePaymentDetails();
        }
        public List<enLeadManagementSubscription> ReadChemicalPriceRange(enSearchFilter objChemical)
        {
            return GetDALReference().ReadChemicalPriceRange(objChemical);
        }
        public List<enLeadManagementSubscription> ReadLeadDetails(enSearchFilter objChemical)
        {
            return GetDALReference().ReadLeadDetails(objChemical);
        }
        
        public List<enLeadManagementSubscription> ReadMarketAnalysisRange(enSearchFilter objChemical)
        {
            return GetDALReference().ReadMarketAnalysisRange(objChemical);
        }
        public List<enLeadManagementSubscription> ReadClientPrimaryEmailIdRecord(enSearchFilter objChemical)
        {
            return GetDALReference().ReadClientPrimaryEmailIdRecord(objChemical);
        }
        
        public List<enLeadManagementSubscription> ReadStateDDL(enSearchFilter objChemical)
        {
            return GetDALReference().ReadStateDDL(objChemical);
        }
        public List<enLeadManagementSubscription> ReadProduct(enSearchFilter objChemical)
        {
            return GetDALReference().ReadProduct(objChemical);
        }
        public List<enLeadManagementSubscription> ReadClintEmail(enSearchFilter objChemical)
        {
            return GetDALReference().ReadClintEmail(objChemical);
        }
        public List<enLeadManagementSubscription> ReadClintEmailByCompanyId(enSearchFilter objChemical)
        {
            return GetDALReference().ReadClintEmailByCompanyId(objChemical);
        }
        
        public List<enLeadManagementSubscription> ReadDataAnalyst(enSearchFilter objChemical)
        {
            return GetDALReference().ReadDataAnalyst(objChemical);
        }
        public List<enLeadManagementSubscription> GetprimaryClintRecord(enSearchFilter objChemical)
        {
            return GetDALReference().GetprimaryClintRecord(objChemical);
        }
        public List<enLeadManagementSubscription> GetLeaddetailsByid(enSearchFilter objChemical)
        {
            return GetDALReference().GetLeaddetailsByid(objChemical);
        }
        
        public List<enLeadManagementSubscription> GetListofLeadDetails(enSearchFilter objChemical)
        {
            return GetDALReference().GetListofLeadDetails(objChemical);
        }
        
        public List<enLeadManagementSubscription> ReadModule(enSearchFilter objChemical)
        {
            return GetDALReference().ReadModule(objChemical);
        }
        
        public List<enLeadManagementSubscription> ReadClintCountry(enSearchFilter objChemical)
        {
            return GetDALReference().ReadClintCountry(objChemical);
        }
        
        public List<enLeadManagementSubscription> ReadClintCompany(enSearchFilter objChemical)
        {
            return GetDALReference().ReadClintCompany(objChemical);
        }
        
        public List<enLeadManagementSubscription> ReadLeadDatainGridForOperation(enSearchFilter objChemical)
        {
            return GetDALReference().ReadLeadDatainGridForOperation(objChemical);
        }
        public List<enLeadManagementSubscription> ReadLeadDatainGridForAccounts(enSearchFilter objChemical)
        {
            return GetDALReference().ReadLeadDatainGridForAccounts(objChemical);
        }
        public List<enLeadManagementSubscription> ReadSalesPackageSubscriptions(enSearchFilter objChemical)
        {
            return GetDALReference().ReadSalesPackageSubscriptions(objChemical);
        }
        
        public List<enLeadManagementSubscription> ReadLeadDatainGridForDataAnalyst(enSearchFilter objChemical)
        {
            return GetDALReference().ReadLeadDatainGridForDataAnalyst(objChemical);
        }
        public List<enLeadManagementSubscription> ReadLeadDatainGridForSalesHead(enSearchFilter objChemical)
        {
            return GetDALReference().ReadLeadDatainGridForSalesHead(objChemical);
        }
        public List<enLeadManagementSubscription> ReadLeadDatainGridForProjectManager(enSearchFilter objChemical)
        {
            return GetDALReference().ReadLeadDatainGridForProjectManager(objChemical);
        }
        
        public List<enLeadManagementSubscription> DeleteGrid(enSearchFilter objChemical)
        {
            return GetDALReference().DeleteGrid(objChemical);
        }
        public List<enLeadManagementSubscription> UpdatestatusofLead(enSearchFilter objChemical)
        {
            return GetDALReference().UpdatestatusofLead(objChemical);
        }
        
        public List<enLeadManagementSubscription> InsertLeadRecord(enLeadManagementSubscription obj)
        {
            return GetDALReference().InsertLeadRecord(obj);
        }
        public List<enLeadManagementSubscription> UpdateInsertLeadRecord(enLeadManagementSubscription obj)
        {
            return GetDALReference().UpdateInsertLeadRecord(obj);
        }
        public List<enLeadManagementSubscription> UpdatePackageRecord(enLeadManagementSubscription obj)
        {
            return GetDALReference().UpdatePackageRecord(obj);
        }
        
        public List<enLeadManagementSubscription> InsertrecordForSalesPackageSubscriptions(enLeadManagementSubscription obj)
        {
            return GetDALReference().InsertrecordForSalesPackageSubscriptions(obj);
        }
        //public List<enLeadManagementSubscription> InsertrecordForSalesPackageSubscriptionsWithsecondaryEmail(enLeadManagementSubscription obj)
        //{
        //    return GetDALReference().InsertrecordForSalesPackageSubscriptionsWithsecondaryEmail(obj);
        //}
        public async Task<List<enLeadManagementSubscription>> InsertRecordAsync(enLeadManagementSubscription obj)
        {
            // Replace with your actual asynchronous logic
            await Task.Delay(1000); // Simulating an asynchronous operation
            return GetDALReference().InsertrecordForSalesPackageSubscriptionsWithsecondaryEmail(obj);
        }
        public List<enLeadManagementSubscription> InsertrecordinSAcustomer(enLeadManagementSubscription obj)
        {
            return GetDALReference().InsertrecordinSAcustomer(obj);
        }
        
        public DataSet GetAllDropdownData(string proc, Dictionary<string, object> param, int timeout = 50)
        {
            return GetDALReference().GetAllDropdownData(proc, param,timeout);
        }

        public List<enLeadManagementSubscription> GetAllOrganisation(enLeadManagementSubscription enLeadManagementSubscription)
        {
            return GetDALReference().GetAllOrganisation(enLeadManagementSubscription);
        }

        public List<enLeadManagementSubscription> GetAllEmailbasedonOrganisation(enLeadManagementSubscription enLeadManagementSubscription)
        {
            return GetDALReference().GetAllEmailbasedonOrganisation(enLeadManagementSubscription);
        }

        public List<enLeadManagementSubscription> GetCreditBasedonEmail(enLeadManagementSubscription enLeadManagementSubscription)
        {
            return GetDALReference().GetCreditBasedonEmail(enLeadManagementSubscription);
        }

        public List<enLeadManagementSubscription> DeSelectAllCheck(enSearchFilter objChemical)
        {
            return GetDALReference().DeSelectAllCheck(objChemical);
        }


        public List<enLeadManagementSubscription> InsertCustomerCreditScore(enLeadManagementSubscription enLeadManagementSubscription)
        {
            return GetDALReference().InsertCustomerCreditScore(enLeadManagementSubscription);
        }
        public List<enLeadManagementSubscription> GetListofCRMCompanyRalation(enSearchFilter objChemical)
        {
            return GetDALReference().GetListofCRMCompanyRalation(objChemical);
        }

        public List<enLeadManagementSubscription> GetCrmEmailByCustomer(enLeadManagementSubscription obj)
        {
            return GetDALReference().GetCrmEmailByCustomer(obj);
        }
        public List<enLeadManagementSubscription> Uploadfile(enLeadManagementSubscription objChemical)
        {
            return GetDALReference().Uploadfile(objChemical);
        }
        public List<enLeadManagementSubscription> GetAssignSalesEmail(enLeadManagementSubscription objChemical)
        {
            return GetDALReference().GetAssignSalesEmail(objChemical);
        }

        //-----------------------------------------------------------------New Module---------------------------------------
        public List<enLeadManagementSubscription> InsertOutboundLeadRecord(enLeadManagementSubscription obj)
        {
            return GetDALReference().InsertOutboundLeadRecord(obj);
        }

        public List<enLeadManagementSubscription> GetYourLeadList(enSearchFilter objChemical)
        {
            return GetDALReference().GetYourLeadList(objChemical);
        }
        public List<enLeadManagementSubscription> GetYourOpenLeadList(enSearchFilter objChemical)
        {
            return GetDALReference().GetYourOpenLeadList(objChemical);
        }

        public List<enLeadManagementSubscription> ChangestatusofLead(enSearchFilter objChemical)
        {
            return GetDALReference().ChangestatusofLead(objChemical);
        }

        public List<enLeadManagementSubscription> ChangestatusofLeadCRM(enSearchFilter objChemical)
        {
            return GetDALReference().ChangestatusofLeadCRM(objChemical);
        }

        public List<enLeadManagementSubscription> BindOpenLeadDetailsByid(enSearchFilter objChemical)
        {
            return GetDALReference().BindOpenLeadDetailsByid(objChemical);
        }
        public List<enLeadManagementSubscription> BindCRMOpenLeadDetailsByid(enSearchFilter objChemical)
        {
            return GetDALReference().BindCRMOpenLeadDetailsByid(objChemical);
        }


        public List<enLeadManagementSubscription> UpdateOpenLeadDetails(enLeadManagementSubscription obj)
        {
            return GetDALReference().UpdateOpenLeadDetails(obj);
        }
      
        public List<enLeadManagementSubscription> GetYourCRMOpenLeadList(enSearchFilter objChemical)
        {
            return GetDALReference().GetYourCRMOpenLeadList(objChemical);
        }

        public List<enLeadManagementSubscription> UpdateCRMOpenLeadDetails(enLeadManagementSubscription obj)
        {
            return GetDALReference().UpdateCRMOpenLeadDetails(obj);
        }
        public List<enLeadManagementSubscription> GetYourCRMRenewLeadList(enSearchFilter objChemical)
        {
            return GetDALReference().GetYourCRMRenewLeadList(objChemical);
        }
        public List<enLeadManagementSubscription> GetSalesTodaysFollowUP_CustomSearch(enSearchFilter objChemical)
        {
            return GetDALReference().GetSalesTodaysFollowUP_CustomSearch(objChemical);
        }
        public List<enLeadManagementSubscription> GetCRMTodaysFollowUP_CustomSearch(enSearchFilter objChemical)
        {
            return GetDALReference().GetCRMTodaysFollowUP_CustomSearch(objChemical);
        }

        public List<enLeadManagementSubscription> GetSalesTodaysRemainingDate_CustomSearch(enSearchFilter objChemical)
        {
            return GetDALReference().GetSalesTodaysRemainingDate_CustomSearch(objChemical);
        }
        public List<enLeadManagementSubscription> GetCRMTodaysRemainingDate_CustomSearch(enSearchFilter objChemical)
        {
            return GetDALReference().GetCRMTodaysRemainingDate_CustomSearch(objChemical);
        }
        public List<enLeadManagementSubscription> GetCRMTodaysExpiry_CustomSearch(enSearchFilter objChemical)
        {
            return GetDALReference().GetCRMTodaysExpiry_CustomSearch(objChemical);
        }
        public List<enLeadManagementSubscription> GetSalesPackageExpiry(enSearchFilter objChemical)
        {
            return GetDALReference().GetSalesPackageExpiry(objChemical);
        }
       
        public List<enLeadManagementSubscription> GetFollowupDicussion(enLeadManagementSubscription obj)
        {
            return GetDALReference().GetFollowupDicussion(obj);
        }
        public List<enLeadManagementSubscription> GetDemoDicussion(enLeadManagementSubscription obj)
        {
            return GetDALReference().GetDemoDicussion(obj);
        }
        public List<enLeadManagementSubscription> GetDemoDicussioncrm(enLeadManagementSubscription obj)
        {
            return GetDALReference().GetDemoDicussioncrm(obj);
        }

        public List<enLeadManagementSubscription> GetFollowupDicussionCRM(enLeadManagementSubscription obj)
        {
            return GetDALReference().GetFollowupDicussionCRM(obj);
        }
        public List<enLeadManagementSubscription> SaveUserRelation(enSearchFilter objChemical)
        {
            return GetDALReference().SaveUserRelation(objChemical);
        }
        public List<enLeadManagementSubscription> ReadUser(enSearchFilter objChemical)
        {
            return GetDALReference().ReadUser(objChemical);
        }

        public List<enLeadManagementSubscription> getMoreContactDetails(enLeadManagementSubscription obj)
        {
            return GetDALReference().getMoreContactDetails(obj);
        }

        public List<enLeadManagementSubscription> SaveLeadContact(int Id, string Name, string Email, string Phone, string Designation, string CreatedBy, string EditID)
        {
            return GetDALReference().SaveLeadContact(Id, Name, Email, Phone, Designation, CreatedBy, EditID);
        }

        public List<enLeadManagementSubscription> GetAllOpenLeadListManager(enSearchFilter objChemical)
        {
            return GetDALReference().GetAllOpenLeadListManager(objChemical);
        }


        public List<enLeadManagementSubscription> ReadAllUserOpenLead()
        {
            return GetDALReference().ReadAllUserOpenLead();
        }


        public List<enLeadManagementSubscription> InsertEmployeeMasterOpenLeadDat(int ChildId, int ParentId)
        {
            return GetDALReference().InsertEmployeeMasterOpenLeadDat(ChildId, ParentId);
        }

        public List<enLeadManagementSubscription> GetAllOpenLeadListTL(enSearchFilter objChemical)
        {
            return GetDALReference().GetAllOpenLeadListTL(objChemical);
        }

        public List<enLeadManagementSubscription> GetAllEmployeeRecordsChart()
        {
            return GetDALReference().GetAllEmployeeRecordsChart();
        }

        public List<enLeadManagementSubscription> GetLeadStatusReport(enSearchFilter objChemical)
        {
            return GetDALReference().GetLeadStatusReport(objChemical);
        }


        public List<enLeadManagementSubscription> TransferLeadPreSales(string CorpEmail, string MailChain)
        {
            return GetDALReference().TransferLeadPreSales(CorpEmail, MailChain);
        }


        public List<enLeadManagementSubscription> BindPreLeadDetailsByid(enSearchFilter objChemical)
        {
            return GetDALReference().BindPreLeadDetailsByid(objChemical);
        }


        public List<enLeadManagementSubscription> UpdatePreSalesLeadRecord(enLeadManagementSubscription obj)
        {
            return GetDALReference().UpdatePreSalesLeadRecord(obj);
        }


        public List<enLeadManagementSubscription> SalesEmployeeName(int UserId)
        {
            return GetDALReference().SalesEmployeeName(UserId);
        }


        public List<enLeadManagementSubscription> InsertEmployeeMasterPreSales(int ChildId, int ParentId)
        {
            return GetDALReference().InsertEmployeeMasterPreSales(ChildId, ParentId);
        }

        public List<enLeadManagementSubscription> GetAllEmployeeRecordsChartPreSales()
        {
            return GetDALReference().GetAllEmployeeRecordsChartPreSales();
        }

        public List<enLeadManagementSubscription> GetLeadStatusReportPreSales(enSearchFilter objChemical)
        {
            return GetDALReference().GetLeadStatusReportPreSales(objChemical);
        }

        public List<enLeadManagementSubscription> GetRemainingPaymentdetails()
        {
            return GetDALReference().GetRemainingPaymentdetails();
        }


        public List<enLeadManagementSubscription> PreSalesDemocountDetails(enSearchFilter objChemical)
        {
            return GetDALReference().PreSalesDemocountDetails(objChemical);
        }

        public List<enLeadManagementSubscription> PreSalesDemocountDetailsByCreatedDate(enSearchFilter objChemical)
        {
            return GetDALReference().PreSalesDemocountDetailsByCreatedDate(objChemical);
        }

        public List<enLeadManagementSubscription> PreSalesWonCountDetils(enSearchFilter objChemical)
        {
            return GetDALReference().PreSalesWonCountDetils(objChemical);
        }

        public List<enLeadManagementSubscription> PreSalesUnusedCountDetils(enSearchFilter objChemical)
        {
            return GetDALReference().PreSalesUnusedCountDetils(objChemical);
        }

        public List<enLeadManagementSubscription> PreSalesTransferCountDetils(enSearchFilter objChemical)
        {
            return GetDALReference().PreSalesTransferCountDetils(objChemical);
        }

        public List<enLeadManagementSubscription> SearchDomain(enSearchFilter objChemical)
        {
            return GetDALReference().SearchDomain(objChemical);
        }

        //------------------------new module-------------------------------------------------------------------------
        public List<enUsersDetails> GetUsersdata(enSearchFilter objChemical)
        {
            return GetDALReference().GetUsersdata(objChemical);
        }
        public List<enUsersDetails> Getcompanywiseleadstatus(enSearchFilter objChemical)
        {
            return GetDALReference().Getcompanywiseleadstatus(objChemical);
        }
        public List<enUsersDetailsbyCompany> GetCustomersDetailsByCompany(enSearchFilter objChemical)
        {
            return GetDALReference().GetCustomersDetailsByCompany(objChemical);
        }
        public List<enCustomerDetailByCompany> GetCustomerDetailById(enSearchFilter objChemical)
        {
            return GetDALReference().GetCustomerDetailById(objChemical);
        }
        public List<enReminder> InsertReminder(enReminder objChemical)
        {
            return GetDALReference().InsertReminder(objChemical);
        }
        public List<enReminder> GetRemindersByCompany(enSearchFilter objChemical)
        {
            return GetDALReference().GetRemindersByCompany(objChemical);
        }
        public List<enUserContact> GetContactsByCompany(enSearchFilter objChemical)
        {
            return GetDALReference().GetContactsByCompany(objChemical);
        }
        public List<enSubscriptionItem> GetSubscriptionsByCompany(enSearchFilter objChemical)
        {
            return GetDALReference().GetSubscriptionsByCompany(objChemical);
        }
        public List<enLoginRecord> GetRecentLoginsByCompany(enSearchFilter companyid)
        {
            return GetDALReference().GetRecentLoginsByCompany(companyid);
        }
        public List<enLoginRecord> GetLoginHistoryByCompany(enSearchFilter companyid)
        {
            return GetDALReference().GetLoginHistoryByCompany(companyid);
        }
        public List<enUsageSummary> GetLoginUsageSummaryByCompany(enSearchFilter companyid)
        {
            return GetDALReference().GetLoginUsageSummaryByCompany(companyid);
        }

        public List<enMonthlyLoginCount> GetLoginsByMonthByCompany(enSearchFilter companyid)
        {
            return GetDALReference().GetLoginsByMonthByCompany(companyid);
        }
        public List<enProductViewCount> GetTopPricingProductsByCompany(enSearchFilter companyid)
        {
            return GetDALReference().GetTopPricingProductsByCompany(companyid);
        }
        public List<enProductViewCount> GetTopDisruptionProductsByCompany(enSearchFilter companyid)
        {
            return GetDALReference().GetTopDisruptionProductsByCompany(companyid);
        }
        public List<enProductViewCount> GetTopMarketAnalysisProductsByCompany(enSearchFilter companyid)
        {
            return GetDALReference().GetTopMarketAnalysisProductsByCompany(companyid);
        }
        public List<enProductViewCount> GetTopCombinedProductsByCompany(enSearchFilter companyid)
        {
            return GetDALReference().GetTopCombinedProductsByCompany(companyid);
        }
        public List<enSupportTicket> GetTicketsByCompany(enSearchFilter searchFilter)
        {
            return GetDALReference().GetTicketsByCompany(searchFilter);
        }
        public List<enSupportTicketComment> GetTicketHistoryById(enSearchFilter searchFilter)
        {
            return GetDALReference().GetTicketHistoryById(searchFilter);
        }
        public List<enProformaInvoiceSummary> GetProformaInvoicesByCompanyName(string companyName)
        {
            return GetDALReference().GetProformaInvoicesByCompanyName(companyName);
        }
        public List<enTodayReminder> GetTodaysRemindersForUser(enSearchFilter searchFilter)
        {
            return GetDALReference().GetTodaysRemindersForUser(searchFilter);
        }
        public void MarkReminderComplete(int reminderId)
        {
            GetDALReference().MarkReminderComplete(reminderId);
        }
        public List<enWorkSpaceFile> GetDeliverablesByCompany(enSearchFilter searchFilter)
        {
            return GetDALReference().GetDeliverablesByCompany(searchFilter);
        }

        public List<enRecommendationSignals> GetRecommendationSignalsByCompany(enSearchFilter searchFilter)
        {
            return GetDALReference().GetRecommendationSignalsByCompany(searchFilter);
        }
        public List<enNeedsAttentionItem> GetNeedsAttentionToday(enSearchFilter searchFilter)
        {
            return GetDALReference().GetNeedsAttentionToday(searchFilter);
        }
        public List<enRenewalThisQuarter> GetRenewalsThisQuarter(enSearchFilter searchFilter)
        {
            return GetDALReference().GetRenewalsThisQuarter(searchFilter);
        }

        public List<enLabelCount> GetCustomersByCountry(enSearchFilter searchFilter)
        {
            return GetDALReference().GetCustomersByCountry(searchFilter);
        }
        public List<enLabelCount> GetCustomersByIndustry(enSearchFilter searchFilter)
        {
            return GetDALReference().GetCustomersByIndustry(searchFilter);
        }
        public List<enMonthlyAddedLost> GetCustomersAddedVsLostByMonth(enSearchFilter searchFilter)
        {
            return GetDALReference().GetCustomersAddedVsLostByMonth(searchFilter);
        }
        public List<enMonthlyAddedLost> GetUsersAddedVsLostByMonth(enSearchFilter searchFilter)
        {
            return GetDALReference().GetUsersAddedVsLostByMonth(searchFilter);
        }
        public List<enMonthlyRevenue> GetRenewalRevenueVsLostByMonth(enSearchFilter searchFilter)
        {
            return GetDALReference().GetRenewalRevenueVsLostByMonth(searchFilter);
        }
        public List<enMonthlyAvg> GetAvgRevenueUsersPerCustomerByMonth(enSearchFilter searchFilter)
        {
            return GetDALReference().GetAvgRevenueUsersPerCustomerByMonth(searchFilter);
        }
        public List<enSalesManagement> GetSubscriptionRequestsList(string search, int startRowNumber, int endRowNumber, int userId)
        {
            return GetDALReference().GetSubscriptionRequestsList(search, startRowNumber, endRowNumber, userId);
        }
        public List<enSubscriptionRequestLine> GetSubscriptionRequestLines(string companyName, string search, int startRowNumber, int endRowNumber)
        {
            return GetDALReference().GetSubscriptionRequestLines(companyName, search, startRowNumber, endRowNumber);
        }
        public List<enAllTicket> GetAllTicketsByCRM(enSearchFilter searchFilter)
        {
            return GetDALReference().GetAllTicketsByCRM(searchFilter);
        }

        // CRM Pipeline page (LeadManagementSubscription/Pipeline.cshtml).
        public List<enLeadManagementSubscription> GetCRMPipeline(int crmId)
        {
            return GetDALReference().GetCRMPipeline(crmId);
        }

        public int OpenRenewalCompany(int companyId, int crmId, string createdBy)
        {
            return GetDALReference().OpenRenewalCompany(companyId, crmId, createdBy);
        }

        public enLeadManagementSubscription GetCRMOpenLeadById(int id)
        {
            return GetDALReference().GetCRMOpenLeadById(id);
        }

        public int UpdateCRMOpenLead(enLeadManagementSubscription o)
        {
            return GetDALReference().UpdateCRMOpenLead(o);
        }

        public List<enCRMFollowUp> GetCRMFollowUps(int openId)
        {
            return GetDALReference().GetCRMFollowUps(openId);
        }

        public int AddCRMFollowUp(enCRMFollowUp f)
        {
            return GetDALReference().AddCRMFollowUp(f);
        }

        public List<enCRMContact> GetCRMContacts(string companyName)
        {
            return GetDALReference().GetCRMContacts(companyName);
        }

        public bool MarkCRMWon(int id, decimal? amount, string currency, decimal? amountINR, string paymentOption, string paymentTerms, string invoiceNumber, string orderFrom)
        {
            return GetDALReference().MarkCRMWon(id, amount, currency, amountINR, paymentOption, paymentTerms, invoiceNumber, orderFrom);
        }

        private DAL GetDALReference()
            {
                if (_objDAL == null)
                {
                    _objDAL = new DAL(this._enLeads);
                }
                return _objDAL;
            }

        }
    }
