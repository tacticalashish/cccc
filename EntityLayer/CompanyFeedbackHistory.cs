using System;

namespace EntityLayer
{
    // One row per recorded quarterly-feedback entry — backs the "View
    // feedback history" popup on the Renewal & automated emails panel
    // (Overview tab, CustomerDetail.cshtml). See
    // /database/SA_CompanyFeedbackHistory.sql.
    public class CompanyFeedbackHistory
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public int? Rating { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
