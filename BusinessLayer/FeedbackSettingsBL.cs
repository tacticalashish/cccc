using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class FeedbackSettingsBL
    {
        private readonly FeedbackSettingsDAL _dal = new FeedbackSettingsDAL();

        public CompanyFeedbackSettings GetCompanyFeedbackSettings(int companyId)
        {
            return _dal.GetCompanyFeedbackSettings(companyId);
        }

        public void SaveCompanyFeedbackEntry(int companyId, bool enabled, DateTime? nextFeedbackEmail, DateTime? lastFeedbackDate, int? lastFeedbackRating, string notes, string userName)
        {
            _dal.SaveCompanyFeedbackEntry(companyId, enabled, nextFeedbackEmail, lastFeedbackDate, lastFeedbackRating, notes, userName);
        }

        public void ToggleQuarterlyFeedback(int companyId, bool enabled, string userName)
        {
            _dal.ToggleQuarterlyFeedback(companyId, enabled, userName);
        }

        public List<CompanyFeedbackHistory> GetCompanyFeedbackHistory(int companyId)
        {
            return _dal.GetCompanyFeedbackHistory(companyId);
        }
    }
}
