using System;

namespace EntityLayer
{
    // Backs the Quarterly feedback email / Next feedback email / Last
    // feedback rows on the Overview tab's panel (CustomerDetail.cshtml).
    // Self-contained — does NOT depend on any renewal table. See
    // /database/SA_CompanyFeedbackSettings.sql.
    public class CompanyFeedbackSettings
    {
        public int CompanyId { get; set; }
        public bool QuarterlyFeedbackEnabled { get; set; }
        public DateTime? NextFeedbackEmail { get; set; }
        public DateTime? LastFeedbackDate { get; set; }
        public int? LastFeedbackRating { get; set; }
        public string LastFeedbackNotes { get; set; }
    }
}
