using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    // BL for the new SalesNewController "My Day" dashboard, sits between
    // admin/Controllers/SalesNewController.cs and DataLayer.dlSalesNew.
    // Thin pass-through, same pattern as blPreSalesDashboard.
    public class blSalesNew
    {
        private readonly dlSalesNew _dal = new dlSalesNew();

        public enSalesKpiSummary GetKpiSummary(string createdBy)
        {
            return _dal.GetKpiSummary(createdBy);
        }

        public List<enSalesFollowupDueRow> GetFollowupsDueToday(string createdBy, string search, int pageNumber, int pageSize, out int totalCount)
        {
            return _dal.GetFollowupsDueToday(createdBy, search, pageNumber, pageSize, out totalCount);
        }

        public List<enSalesDemoInProgressRow> GetDemosInProgress(string createdBy, string search, int pageNumber, int pageSize, out int totalCount)
        {
            return _dal.GetDemosInProgress(createdBy, search, pageNumber, pageSize, out totalCount);
        }

        public List<enSalesPipelineStageCount> GetPipelineByStage(string createdBy)
        {
            return _dal.GetPipelineByStage(createdBy);
        }

        public List<enSalesPipelineForecastRow> GetPipelineValueForecast(string createdBy)
        {
            return _dal.GetPipelineValueForecast(createdBy);
        }

        public List<enSalesWonLostRow> GetWonVsLost(string createdBy, System.DateTime? fromDate, System.DateTime? toDate)
        {
            return _dal.GetWonVsLost(createdBy, fromDate, toDate);
        }

        public List<enSalesLeadSourceRow> GetLeadsBySource(string createdBy)
        {
            return _dal.GetLeadsBySource(createdBy);
        }

        public enSalesLeadDetail GetLeadDetail(int id)
        {
            return _dal.GetLeadDetail(id);
        }

        // New-stage lead (SA_MaskEmail) - see dlSalesNew.GetLeadDetailNew.
        public enSalesLeadDetail GetLeadDetailNew(int id)
        {
            return _dal.GetLeadDetailNew(id);
        }

        public bool MarkLeadWon(int id, decimal? amount, string currency, decimal? amountINR, string paymentOption, string paymentTerms, string invoiceNumber, string OrderFrom)
        {
            return _dal.MarkLeadWon(id, amount, currency, amountINR, paymentOption, paymentTerms, invoiceNumber, OrderFrom);
        }

        public bool UpdateLeadValue(int id, decimal? amount, string currency, decimal? amountINR)
        {
            return _dal.UpdateLeadValue(id, amount, currency, amountINR);
        }

        public bool UpdateLeadDetails(int id, string leadType, string source, string message, decimal? amount,
            System.DateTime? expectedCloseDate, System.DateTime? setFollowUp, string companyName, string name,
            string corpEmail, string contactNumber, string countryName)
        {
            return _dal.UpdateLeadDetails(id, leadType, source, message, amount, expectedCloseDate, setFollowUp,
                companyName, name, corpEmail, contactNumber, countryName);
        }

        public List<enSalesOrderFormRow> GetSalesOrderFormList(string createdBy, string search, int startRowNumber, int endRowNumber)
        {
            return _dal.GetSalesOrderFormList(createdBy, search, startRowNumber, endRowNumber);
        }

        public List<enSalesWonLeadRow> GetWonLeadsWithoutSubscription(string createdBy)
        {
            return _dal.GetWonLeadsWithoutSubscription(createdBy);
        }

        public bool MarkLeadNewStatus(int id, string status)
        {
            return _dal.MarkLeadNewStatus(id, status);
        }

        public bool MarkLeadLost(int id, string reason)
        {
            return _dal.MarkLeadLost(id, reason);
        }

        public bool LogFollowup(int id, System.DateTime followUpDate)
        {
            return _dal.LogFollowup(id, followUpDate);
        }

        public List<enSalesActivityRow> GetLeadActivity(int id, string search, int startRowNumber, int endRowNumber)
        {
            return _dal.GetLeadActivity(id, search, startRowNumber, endRowNumber);
        }

        public bool LogFollowupActivity(int id, string followupType, string discussion, string stage, System.DateTime? nextFollowUpDate, bool addToMyDay, string createdBy, out string message)
        {
            return _dal.LogFollowupActivity(id, followupType, discussion, stage, nextFollowUpDate, addToMyDay, createdBy, out message);
        }

        public bool ScheduleDemo(int id, System.DateTime? demoDate, string productsToDemo, string createdBy, out string message)
        {
            return _dal.ScheduleDemo(id, demoDate, productsToDemo, createdBy, out message);
        }

        public bool SaveContractSign(int id, System.DateTime? startDate, System.DateTime? endDate, string createdBy, out string message)
        {
            return _dal.SaveContractSign(id, startDate, endDate, createdBy, out message);
        }

        public List<enContractSign> GetContractSignByLead(int leadId)
        {
            return _dal.GetContractSignByLead(leadId);
        }

        public List<string> GetStageOptions()
        {
            return _dal.GetStageOptions();
        }

        public List<enSalesLeadContactRow> GetLeadContacts(int leadId)
        {
            return _dal.GetLeadContacts(leadId);
        }

        public bool AddLeadContact(int leadId, string name, string corpEmail, string contactCountryCode, string contactNumber, string designation, string createdBy, out string message)
        {
            return _dal.AddLeadContact(leadId, name, corpEmail, contactCountryCode, contactNumber, designation, createdBy, out message);
        }

        public List<enSalesPipelineCardRow> GetPipelineNew(string createdBy)
        {
            return _dal.GetPipelineNew(createdBy);
        }

        public List<enSalesPipelineCardRow> GetPipelineContacted(string createdBy)
        {
            return _dal.GetPipelineContacted(createdBy);
        }

        public List<enSalesPipelineCardRow> GetPipelineDemo(string createdBy)
        {
            return _dal.GetPipelineDemo(createdBy);
        }

        public List<enSalesPipelineCardRow> GetPipelineWon(string createdBy)
        {
            return _dal.GetPipelineWon(createdBy);
        }

        public List<enSalesPipelineCardRow> GetPipelineLost(string createdBy)
        {
            return _dal.GetPipelineLost(createdBy);
        }

        public List<enSalesPipelineListRow> GetPipelineList(string createdBy, string stage, string leadType, string source, string search, System.DateTime? fromDate, System.DateTime? toDate, int startRowNumber, int endRowNumber)
        {
            return _dal.GetPipelineList(createdBy, stage, leadType, source, search, fromDate, toDate, startRowNumber, endRowNumber);
        }

        public List<enSalesLostReasonRow> GetLostReasonReport(string createdBy, string search, int startRowNumber, int endRowNumber)
        {
            return _dal.GetLostReasonReport(createdBy, search, startRowNumber, endRowNumber);
        }
    }
}
