using System;

namespace ChemAnalyst.Models
{
    // Backs the "Renewal & automated emails" panel on the Overview tab of
    // CustomerDetail.cshtml. See /database/SA_CompanyRenewalSettings.sql.
    // NextFeedbackEmail / LastFeedbackDate / LastFeedbackRating have no input
    // mechanism yet — always null until a feedback-survey feature exists to
    // populate them.
    public class RenewalEmailSettings
    {
        public int CompanyId { get; set; }
        public bool QuarterlyFeedbackEnabled { get; set; }
        public DateTime? NextFeedbackEmail { get; set; }
        public DateTime? LastFeedbackDate { get; set; }
        public int? LastFeedbackRating { get; set; }
    }
}
