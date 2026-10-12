using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class RenewalSettingsBL
    {
        private readonly RenewalSettingsDAL _dal = new RenewalSettingsDAL();

        public bool GetExpiryNotificationStatus(int companyId)
        {
            return _dal.GetExpiryNotificationStatus(companyId);
        }

        public void UpdateExpiryNotification(int companyId, bool isEnabled)
        {
            _dal.UpdateExpiryNotification(companyId, isEnabled);
        }

        //public RenewalEmailSettings GetCompanyRenewalSettings(int companyId)
        //{
        //    return _dal.GetCompanyRenewalSettings(companyId);
        //}

        public void SaveQuarterlyFeedbackSetting(int companyId, bool enabled, string userName)
        {
            _dal.SaveQuarterlyFeedbackSetting(companyId, enabled, userName);
        }

        public void SaveCompanyFeedbackEntry(int companyId, bool enabled, DateTime? nextFeedbackEmail, DateTime? lastFeedbackDate, int? lastFeedbackRating, string notes, string userName)
        {
            _dal.SaveCompanyFeedbackEntry(companyId, enabled, nextFeedbackEmail, lastFeedbackDate, lastFeedbackRating, notes, userName);
        }

        public List<CompanyFeedbackHistory> GetCompanyFeedbackHistory(int companyId)
        {
            return _dal.GetCompanyFeedbackHistory(companyId);
        }
    }
}
