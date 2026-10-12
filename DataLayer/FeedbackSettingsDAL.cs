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
    // calling stored procedures against ChemAnalystContext. Self-contained
    // feedback feature — no dependency on any renewal table/proc.
    // See /database/SA_CompanyFeedbackSettings.sql.
    public class FeedbackSettingsDAL
    {
        
        public CompanyFeedbackSettings GetCompanyFeedbackSettings(int companyId)
        {
            CompanyFeedbackSettings result = null;

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("USP_GetCompanyFeedbackSettings", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        result = new CompanyFeedbackSettings
                        {
                            CompanyId = Convert.ToInt32(rdr["CompanyId"]),
                            QuarterlyFeedbackEnabled = rdr["QuarterlyFeedbackEnabled"] != DBNull.Value && Convert.ToBoolean(rdr["QuarterlyFeedbackEnabled"]),
                            NextFeedbackEmail = rdr["NextFeedbackEmail"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["NextFeedbackEmail"]),
                            LastFeedbackDate = rdr["LastFeedbackDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["LastFeedbackDate"]),
                            LastFeedbackRating = rdr["LastFeedbackRating"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["LastFeedbackRating"]),
                            LastFeedbackNotes = rdr["LastFeedbackNotes"] == DBNull.Value ? null : rdr["LastFeedbackNotes"].ToString()
                        };
                    }
                }
            }

            return result;
        }

        // Combined save for the "Edit feedback settings" popup — upserts the
        // Quarterly feedback toggle + Next feedback email + Last feedback
        // date/rating, and (when a last-feedback date is given) appends a
        // row to SA_CompanyFeedbackHistory.
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

        // Lightweight toggle used by the inline switch on the panel — flips
        // only QuarterlyFeedbackEnabled, leaves Next/Last feedback fields
        // and history untouched (unlike SaveCompanyFeedbackEntry, which is
        // the full "Edit" form save).
        public void ToggleQuarterlyFeedback(int companyId, bool enabled, string userName)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("USP_ToggleQuarterlyFeedback", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                cmd.Parameters.Add(new SqlParameter("@Enabled", enabled));
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
