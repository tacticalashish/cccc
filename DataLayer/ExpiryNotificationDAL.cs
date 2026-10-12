using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DataLayer
{
    // Expiry notifications toggle (Overview tab panel, CustomerDetail.cshtml).
    // Self-contained — reuses the existing SalesPackageSubscriptions
    // .ExpiryNotification column, no new table. See
    // /database/SA_ExpiryNotification.sql.
    public class ExpiryNotificationDAL
    {
        
        public bool GetExpiryNotificationStatus(int companyId)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("USP_GetExpiryNotificationStatusByCompany", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                conn.Open();

                var result = cmd.ExecuteScalar();
                return result != null && result != DBNull.Value && Convert.ToInt32(result) == 1;
            }
        }

        public void UpdateExpiryNotification(int companyId, bool isEnabled)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("USP_UpdateExpiryNotificationByCompany", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                cmd.Parameters.Add(new SqlParameter("@IsEnabled", isEnabled));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
