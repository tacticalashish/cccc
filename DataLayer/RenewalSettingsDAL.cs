using EntityLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DataLayer
{
    // Same shape as InvoicingSetupDAL/CustomerHistoryDAL — plain ADO.NET
    // calling stored procedures against ChemAnalystContext.
    public class RenewalSettingsDAL
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

     

        public void SaveQuarterlyFeedbackSetting(int companyId, bool enabled, string userName)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("USP_SaveQuarterlyFeedbackSetting", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                cmd.Parameters.Add(new SqlParameter("@Enabled", enabled));
                cmd.Parameters.Add(new SqlParameter("@UserName", (object)userName ?? ""));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Combined save for the "Edit feedback settings" popup — upserts the
        // Quarterly feedback toggle + Next feedback email + Last feedback
        // date/rating, and (when a last-feedback date is given) appends a
        // row to SA_CompanyFeedbackHistory. See
        // /database/SA_CompanyFeedbackHistory.sql.
        public void SaveCompanyFeedbackEntry(int companyId, bool enabled, DateTime? nextFeedbackEmail, DateTime? lastFeedbackDate, int? lastFeedbackRating, string notes, string userName)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("USP_SaveCompanyFeedbackEntry", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                cmd.Parameters.Add(new SqlParameter("@Enabled", enabled));
                cmd.Parameters.Add(new SqlParameter("@NextFeedbackEmail", (object)nextFeedbackEmail ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@LastFeedbackDate", (object)lastFeedbackDate ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@LastFeedbackRating", (object)lastFeedbackRating ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Notes", (object)notes ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@UserName", (object)userName ?? ""));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<CompanyFeedbackHistory> GetCompanyFeedbackHistory(int companyId)
        {
            var list = new List<CompanyFeedbackHistory>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("USP_GetCompanyFeedbackHistory", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new CompanyFeedbackHistory
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            CompanyId = Convert.ToInt32(rdr["CompanyId"]),
                            FeedbackDate = rdr["FeedbackDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["FeedbackDate"]),
                            Rating = rdr["Rating"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["Rating"]),
                            Notes = rdr["Notes"] == DBNull.Value ? null : rdr["Notes"].ToString(),
                            CreatedBy = rdr["CreatedBy"] == DBNull.Value ? null : rdr["CreatedBy"].ToString(),
                            CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"])
                        });
                    }
                }
            }

            return list;
        }
    }
}
