using System;

namespace EntityLayer
{
    // Backs the "Subscription history" table on the History tab of
    // CustomerDetail.cshtml. See /database/SA_SubscriptionHistory.sql
    // (USP_GetSubscriptionHistoryByCompany). One row per ToDate, with all
    // products expiring on that date merged into ProductsSubscribed.
    // Users = COUNT(DISTINCT UserId) sharing that ToDate. AmountPaid has no
    // source at all, so it isn't queried; the UI always shows "—" for it.
    public class SubscriptionHistoryItem
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ProductsSubscribed { get; set; }
        public int? Users { get; set; }
        public string Status { get; set; }
    }
}
