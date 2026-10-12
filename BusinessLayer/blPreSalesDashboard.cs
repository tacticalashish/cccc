using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    // BL for the new PreSalesDashboard page, sits between
    // admin/Controllers/PreSalesDashboardController.cs and
    // DataLayer.dlPreSalesDashboard. Thin pass-through, same pattern as
    // blPreSalesNew.
    public class blPreSalesDashboard
    {
        private readonly dlPreSalesDashboard _dal = new dlPreSalesDashboard();

        public enPreSalesDashboardStats GetStats(string createdBy)
        {
            return _dal.GetStats(createdBy);
        }

        public List<enPreSalesSourceCount> GetLeadsBySource(string createdBy)
        {
            return _dal.GetLeadsBySource(createdBy);
        }

        public List<enPreSalesFollowupRow> GetFollowupsDueList(string createdBy)
        {
            return _dal.GetFollowupsDueList(createdBy);
        }

        public List<enPreSalesFollowupRow> GetFollowupsDueListPaged(string createdBy, string search, int pageNumber, int pageSize)
        {
            return _dal.GetFollowupsDueListPaged(createdBy, search, pageNumber, pageSize);
        }

        public int GetFollowupsDueListPagedCount(string createdBy, string search)
        {
            return _dal.GetFollowupsDueListPagedCount(createdBy, search);
        }

        public List<enPreSalesTransferredRow> GetTransferredLeads(string createdBy)
        {
            return _dal.GetTransferredLeads(createdBy);
        }

        public List<enPreSalesTransferredRow> GetTransferredListPaged(string createdBy, string search, int pageNumber, int pageSize)
        {
            return _dal.GetTransferredListPaged(createdBy, search, pageNumber, pageSize);
        }

        public int GetTransferredListPagedCount(string createdBy, string search)
        {
            return _dal.GetTransferredListPagedCount(createdBy, search);
        }

        public List<enPreSalesWorkTodayRow> GetWorkTodayLeads(string createdBy)
        {
            return _dal.GetWorkTodayLeads(createdBy);
        }

        public List<enPreSalesMonthDemo> GetDemosByMonth(string createdBy)
        {
            return _dal.GetDemosByMonth(createdBy);
        }

        public enPreSalesFunnel GetFunnel(string createdBy)
        {
            return _dal.GetFunnel(createdBy);
        }

        public List<enPreSalesStatDetailRow> GetLeadsGeneratedList(string createdBy)
        {
            return _dal.GetLeadsGeneratedList(createdBy);
        }

        public List<enPreSalesStatDetailRow> GetDemosFixedList(string createdBy)
        {
            return _dal.GetDemosFixedList(createdBy);
        }

        public List<enPreSalesStatDetailRow> GetPendingTransferList(string createdBy)
        {
            return _dal.GetPendingTransferList(createdBy);
        }
    }
}
