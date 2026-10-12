using DataLayer;

namespace BusinessLayer
{
    public class ExpiryNotificationBL
    {
        private readonly ExpiryNotificationDAL _dal = new ExpiryNotificationDAL();

        public bool GetExpiryNotificationStatus(int companyId)
        {
            return _dal.GetExpiryNotificationStatus(companyId);
        }

        public void UpdateExpiryNotification(int companyId, bool isEnabled)
        {
            _dal.UpdateExpiryNotification(companyId, isEnabled);
        }
    }
}
