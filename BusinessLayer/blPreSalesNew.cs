using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    // BL for the new PreSalesNew Leads list feature, sits between
    // admin/Controllers/PreSalesNewController.cs and DataLayer.dlPreSalesNew.
    // Thin pass-through, same pattern as blLeadManagementSubscription.
    public class blPreSalesNew
    {
        private readonly dlPreSalesNew _dal = new dlPreSalesNew();

        public List<enPreSalesFilterOption> GetSourceList(string createdBy)
        {
            return _dal.GetSourceList(createdBy);
        }

        public List<enPreSalesFilterOption> GetStatusList(string createdBy)
        {
            return _dal.GetStatusList(createdBy);
        }

        public enPreSalesNew GetLeadById(int id)
        {
            return _dal.GetLeadById(id);
        }

        public string GetAssignPersonByCountry(string country)
        {
            return _dal.GetAssignPersonByCountry(country);
        }

        public bool UpdateLead(enPreSalesNew lead)
        {
            return _dal.UpdateLead(lead);
        }

        public List<enPreSalesNew> GetLeadList(string search, string source, string status, string createdBy, System.DateTime? fromDate, System.DateTime? toDate, int pageNumber, int pageSize)
        {
            return _dal.GetLeadList(search, source, status, createdBy, fromDate, toDate, pageNumber, pageSize);
        }

        public int GetLeadListCount(string search, string source, string status, string createdBy, System.DateTime? fromDate, System.DateTime? toDate)
        {
            return _dal.GetLeadListCount(search, source, status, createdBy, fromDate, toDate);
        }

        public int InsertFollowup(enPreSalesFollowup followup)
        {
            return _dal.InsertFollowup(followup);
        }

        public enPreSalesFollowup GetFollowupByLeadId(int leadId)
        {
            return _dal.GetFollowupByLeadId(leadId);
        }

        public List<enPreSalesFollowup> GetFollowupsByLeadId(int leadId)
        {
            return _dal.GetFollowupsByLeadId(leadId);
        }

        public int InsertActivity(enPreSalesActivity activity)
        {
            return _dal.InsertActivity(activity);
        }

        public List<enPreSalesActivity> GetActivityByLeadId(int leadId)
        {
            return _dal.GetActivityByLeadId(leadId);
        }

        public List<enPreSalesActiveCustomer> GetActiveCustomerList(string search)
        {
            return _dal.GetActiveCustomerList(search);
        }

        public List<enPreSalesActiveCustomer> GetActiveCustomerListPaged(string search, int pageNumber, int pageSize, out int totalCount)
        {
            return _dal.GetActiveCustomerListPaged(search, pageNumber, pageSize, out totalCount);
        }

        public List<enPreSalesInactiveCustomer> GetInactiveCustomerListPaged(string search, int pageNumber, int pageSize, out int totalCount)
        {
            return _dal.GetInactiveCustomerListPaged(search, pageNumber, pageSize, out totalCount);
        }

        public List<enPreSalesOpenLeadRow> GetDemoedNotClosedList(string search, string country, string dateFrom, string dateTo, string createdBy, int pageNumber, int pageSize)
        {
            return _dal.GetDemoedNotClosedList(search, country, dateFrom, dateTo, createdBy, pageNumber, pageSize);
        }

        public int GetDemoedNotClosedListCount(string search, string country, string dateFrom, string dateTo, string createdBy)
        {
            return _dal.GetDemoedNotClosedListCount(search, country, dateFrom, dateTo, createdBy);
        }

        public List<enPreSalesFilterOption> GetDemoedNotClosedCountryList(string createdBy)
        {
            return _dal.GetDemoedNotClosedCountryList(createdBy);
        }

        public List<enPreSalesMonthlyPerformance> GetMonthlyPerformance(string createdBy)
        {
            return _dal.GetMonthlyPerformance(createdBy);
        }

        public List<enPreSalesDemoCompletedRow> GetDemosCompletedList(string createdBy)
        {
            return _dal.GetDemosCompletedList(createdBy);
        }

        public List<enPreSalesDemoCompletedRow> GetDemosCompletedListPaged(string createdBy, string search, int pageNumber, int pageSize)
        {
            return _dal.GetDemosCompletedListPaged(createdBy, search, pageNumber, pageSize);
        }

        public int GetDemosCompletedListPagedCount(string createdBy, string search)
        {
            return _dal.GetDemosCompletedListPagedCount(createdBy, search);
        }

        public List<enPreSalesDemoCompletedRow> GetDemosCompletedListByRange(string createdBy, string rangeType)
        {
            return _dal.GetDemosCompletedListByRange(createdBy, rangeType);
        }

        public enPreSalesIncentiveKpiCounts GetIncentiveKpiCounts(string createdBy)
        {
            return _dal.GetIncentiveKpiCounts(createdBy);
        }

        public List<enPreSalesIncentiveMonthCount> GetIncentiveLast6MonthsBreakdown(string createdBy)
        {
            return _dal.GetIncentiveLast6MonthsBreakdown(createdBy);
        }

        public List<enPreSalesMonthDemoCount> GetMonthlyDemoCountsByDemoDate(string createdBy)
        {
            return _dal.GetMonthlyDemoCountsByDemoDate(createdBy);
        }

        public List<enPreSalesDemoCompletedRow> GetDemosCompletedListByMonth(string createdBy, int monthNo, int yearNo)
        {
            return _dal.GetDemosCompletedListByMonth(createdBy, monthNo, yearNo);
        }

        public List<enPreSalesMonthlyPerformancePagedRow> GetMonthlyPerformancePaged(string createdBy, string search, int pageNumber, int pageSize)
        {
            return _dal.GetMonthlyPerformancePaged(createdBy, search, pageNumber, pageSize);
        }

        public int GetMonthlyPerformancePagedCount(string createdBy, string search)
        {
            return _dal.GetMonthlyPerformancePagedCount(createdBy, search);
        }

        public List<enPreSalesMonthlyLeadDetail> GetMonthlyPerformanceDetail(string createdBy, int monthNo, int yearNo)
        {
            return _dal.GetMonthlyPerformanceDetail(createdBy, monthNo, yearNo);
        }
    }
}
