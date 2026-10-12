using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class CustomerHistoryBL
    {
        private readonly CustomerHistoryDAL _dal = new CustomerHistoryDAL();

        public CustomerHistorySummary GetCustomerHistorySummary(int companyId)
        {
            return _dal.GetCustomerHistorySummary(companyId);
        }

        public List<SubscriptionHistoryItem> GetSubscriptionHistoryByCompany(int companyId)
        {
            return _dal.GetSubscriptionHistoryByCompany(companyId);
        }
    }
}
