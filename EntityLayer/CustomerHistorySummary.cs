using System;

namespace EntityLayer
{
    // Backs the 3 summary cards on the History tab of CustomerDetail.cshtml.
    // See /database/SA_CustomerHistory.sql (USP_GetCustomerHistorySummary).
    // LifetimeValue has no source yet — always null for now.
    public class CustomerHistorySummary
    {
        public DateTime? CustomerSince { get; set; }
        public int Renewals { get; set; }
        public decimal? LifetimeValue { get; set; }
    }
}
