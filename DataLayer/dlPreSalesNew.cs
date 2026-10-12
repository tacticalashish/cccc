using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DataLayer
{
    // DAL for the new PreSalesNew Leads list feature — every query goes
    // through a stored procedure (see database/usp_PreSalesNew.sql in the
    // admin project), no inline SQL text. Same connection string as the
    // rest of this project (ApplicationSettings.DefaultConnectionString ->
    // ChemAnalystContext).
    public class dlPreSalesNew
    {
        // "All sources" filter dropdown — distinct OutboundLeadSource
        // values actually present among THIS user's own leads (e.g. Chat,
        // WebsiteForms), so a selected filter always has matching rows.
        public List<enPreSalesFilterOption> GetSourceList(string createdBy)
        {
            var list = new List<enPreSalesFilterOption>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetSourceList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var val = rdr["OutboundLeadSource"].ToString();
                        list.Add(new enPreSalesFilterOption { Value = val, Text = val });
                    }
                }
            }

            return list;
        }

        // "All statuses" filter dropdown — distinct Status values actually
        // present among THIS user's own leads.
        public List<enPreSalesFilterOption> GetStatusList(string createdBy)
        {
            var list = new List<enPreSalesFilterOption>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetStatusList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var val = rdr["Status"].ToString();
                        list.Add(new enPreSalesFilterOption { Value = val, Text = val });
                    }
                }
            }

            return list;
        }

        // Shared reader -> entity mapping, used by every method below that
        // reads a SA_MaskEmail_PreSales row (GetLeadById, GetLeadList) so
        // the column list only has to be maintained in one place instead
        // of being copy-pasted per method. Same naming convention as the
        // legacy DAL's ConstructObjectXxx helpers (dlLeadManagementSubscription.cs).
        private static bool HasColumn(IDataRecord rdr, string columnName)
        {
            for (int i = 0; i < rdr.FieldCount; i++)
            {
                if (string.Equals(rdr.GetName(i), columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private enPreSalesNew ConstructLeadFromReader(IDataReader rdr)
        {
            return new enPreSalesNew
            {
                ID = Convert.ToInt32(rdr["ID"]),
                Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                CorpEmail = rdr["CorpEmail"] == DBNull.Value ? "" : rdr["CorpEmail"].ToString(),
                ContactCountryCode = rdr["ContactCountryCode"] == DBNull.Value ? "" : rdr["ContactCountryCode"].ToString(),
                ContactNumber = rdr["ContactNumber"] == DBNull.Value ? "" : rdr["ContactNumber"].ToString(),
                CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                Designation = rdr["Designation"] == DBNull.Value ? "" : rdr["Designation"].ToString(),
                CountryFID = rdr["CountryFID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["CountryFID"]),
                CountryName = rdr["CountryName"] == DBNull.Value ? "" : rdr["CountryName"].ToString(),
                StateFID = rdr["StateFID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["StateFID"]),
                StateName = rdr["StateName"] == DBNull.Value ? "" : rdr["StateName"].ToString(),
                Message = rdr["Message"] == DBNull.Value ? "" : rdr["Message"].ToString(),
                Source = rdr["Source"] == DBNull.Value ? "" : rdr["Source"].ToString(),
                OutboundLeadSource = rdr["OutboundLeadSource"] == DBNull.Value ? "" : rdr["OutboundLeadSource"].ToString(),
                Status = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                LeadType = rdr["LeadType"] == DBNull.Value ? "" : rdr["LeadType"].ToString(),
                RequestedDate = rdr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["RequestedDate"]),
                CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                // Not every proc that shares this mapper selects UpdateDate
                // (GetLeadList's doesn't), so this only reads it when present.
                UpdateDate = HasColumn(rdr, "UpdateDate") && rdr["UpdateDate"] != DBNull.Value ? Convert.ToDateTime(rdr["UpdateDate"]) : (DateTime?)null,
                CreatedBy = rdr["CreatedBy"] == DBNull.Value ? "" : rdr["CreatedBy"].ToString(),
                TelephoneCountryCode = rdr["TelephoneCountryCode"] == DBNull.Value ? "" : rdr["TelephoneCountryCode"].ToString(),
                Telephone = rdr["Telephone"] == DBNull.Value ? "" : rdr["Telephone"].ToString(),
                MailChain = rdr["MailChain"] == DBNull.Value ? "" : rdr["MailChain"].ToString()
                // Sector / Size / Industry / Employees / Revenue intentionally
                // left null — no backing column on SA_MaskEmail_PreSales.
            };
        }

        // Single lead — backs the LeadDetail page. Same column set as
        // GetLeadList, just one row by ID, no filters/paging.
        public enPreSalesNew GetLeadById(int id)
        {
            enPreSalesNew lead = null;

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetLeadById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        lead = ConstructLeadFromReader(rdr);
                    }
                }
            }

            return lead;
        }

        // LeadDetail page — the sales person assigned to the lead's country
        // (SA_AssignSalesPersonCountry), matched by country name. Returns ""
        // when there is no assignment rule for that country.
        public string GetAssignPersonByCountry(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) return "";

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetAssignPersonByCountry", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Country", country));
                conn.Open();

                object result = cmd.ExecuteScalar();
                return (result == null || result == DBNull.Value) ? "" : result.ToString();
            }
        }

        // "Edit lead" drawer on the LeadDetail page -- the proc itself
        // refuses the update once Status = 'Transferred' (WHERE clause),
        // and reports back via @UpdatedRows whether anything actually
        // changed, so the UI can tell "saved" apart from "blocked, already
        // transferred" even if a stale page bypasses the client-side check.
        public bool UpdateLead(enPreSalesNew lead)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_UpdateLead", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", lead.ID));
                cmd.Parameters.Add(new SqlParameter("@Name", lead.Name ?? ""));
                cmd.Parameters.Add(new SqlParameter("@CorpEmail", lead.CorpEmail ?? ""));
                cmd.Parameters.Add(new SqlParameter("@ContactCountryCode", lead.ContactCountryCode ?? ""));
                cmd.Parameters.Add(new SqlParameter("@ContactNumber", lead.ContactNumber ?? ""));
                cmd.Parameters.Add(new SqlParameter("@TelephoneCountryCode", lead.TelephoneCountryCode ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Telephone", lead.Telephone ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Designation", lead.Designation ?? ""));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", lead.CompanyName ?? ""));
                cmd.Parameters.Add(new SqlParameter("@CountryFID", (object)lead.CountryFID ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CountryName", lead.CountryName ?? ""));
                cmd.Parameters.Add(new SqlParameter("@StateFID", (object)lead.StateFID ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@StateName", lead.StateName ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Message", lead.Message ?? ""));
                cmd.Parameters.Add(new SqlParameter("@OutboundLeadSource", lead.OutboundLeadSource ?? ""));

                var updatedRowsParam = new SqlParameter("@UpdatedRows", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(updatedRowsParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return updatedRowsParam.Value != DBNull.Value && Convert.ToInt32(updatedRowsParam.Value) > 0;
            }
        }

        // Leads list — scoped to the logged-in presales user (CreatedBy),
        // same as usp_GetPreSalesLeadList_CustomSearch's @Createdby filter,
        // plus search + source + status filters, paged, newest first
        // (CreatedDate, falling back to RequestedDate when null).
        public List<enPreSalesNew> GetLeadList(string search, string source, string status, string createdBy, DateTime? fromDate, DateTime? toDate, int pageNumber, int pageSize)
        {
            var list = new List<enPreSalesNew>();
            search = search ?? "";
            source = source ?? "";
            status = status ?? "";
            createdBy = createdBy ?? "";

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetLeadList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Search", search));
                cmd.Parameters.Add(new SqlParameter("@Source", source));
                cmd.Parameters.Add(new SqlParameter("@Status", status));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy));
                cmd.Parameters.Add(new SqlParameter("@FromDate", (object)fromDate ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ToDate", (object)toDate ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(ConstructLeadFromReader(rdr));
                    }
                }
            }

            return list;
        }

        public int GetLeadListCount(string search, string source, string status, string createdBy, DateTime? fromDate, DateTime? toDate)
        {
            search = search ?? "";
            source = source ?? "";
            status = status ?? "";
            createdBy = createdBy ?? "";

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetLeadListCount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Search", search));
                cmd.Parameters.Add(new SqlParameter("@Source", source));
                cmd.Parameters.Add(new SqlParameter("@Status", status));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy));
                cmd.Parameters.Add(new SqlParameter("@FromDate", (object)fromDate ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ToDate", (object)toDate ?? DBNull.Value));
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Follow-up box on the LeadDetail page — backed by the new
        // SA_MaskEmail_PreSalesFollowup table.
        public int InsertFollowup(enPreSalesFollowup followup)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_InsertFollowup", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LeadID", followup.LeadID));
                cmd.Parameters.Add(new SqlParameter("@FollowUpDate", (object)followup.FollowUpDate ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Note", (object)followup.Note ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", (object)followup.CreatedBy ?? DBNull.Value));
                conn.Open();

                var result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
        }

        // Most recent not-yet-completed follow-up for a lead, if any.
        public enPreSalesFollowup GetFollowupByLeadId(int leadId)
        {
            enPreSalesFollowup followup = null;

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetFollowupByLeadId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LeadID", leadId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        followup = new enPreSalesFollowup
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            LeadID = Convert.ToInt32(rdr["LeadID"]),
                            FollowUpDate = rdr["FollowUpDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["FollowUpDate"]),
                            Note = rdr["Note"] == DBNull.Value ? "" : rdr["Note"].ToString(),
                            IsCompleted = rdr["IsCompleted"] != DBNull.Value && Convert.ToBoolean(rdr["IsCompleted"]),
                            CreatedBy = rdr["CreatedBy"] == DBNull.Value ? "" : rdr["CreatedBy"].ToString(),
                            CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"])
                        };
                    }
                }
            }

            return followup;
        }

        // Full follow-up history for a lead, newest first — the LeadDetail
        // page's Follow-up box shows this whole list (scrollable) instead of
        // just the single most-recent one GetFollowupByLeadId returns.
        public List<enPreSalesFollowup> GetFollowupsByLeadId(int leadId)
        {
            var list = new List<enPreSalesFollowup>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetFollowupsByLeadId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LeadID", leadId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesFollowup
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            LeadID = Convert.ToInt32(rdr["LeadID"]),
                            FollowUpDate = rdr["FollowUpDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["FollowUpDate"]),
                            Note = rdr["Note"] == DBNull.Value ? "" : rdr["Note"].ToString(),
                            IsCompleted = rdr["IsCompleted"] != DBNull.Value && Convert.ToBoolean(rdr["IsCompleted"]),
                            CreatedBy = rdr["CreatedBy"] == DBNull.Value ? "" : rdr["CreatedBy"].ToString(),
                            CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"])
                        });
                    }
                }
            }

            return list;
        }

        // Activity timeline on the LeadDetail page — backed by the new
        // SA_MaskEmail_PreSales_LogActivity table. Same pattern as the
        // Follow-up methods above.
        public int InsertActivity(enPreSalesActivity activity)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_InsertActivity", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LeadID", activity.LeadID));
                cmd.Parameters.Add(new SqlParameter("@ActivityType", (object)activity.ActivityType ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ActivityNote", (object)activity.ActivityNote ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Outcome", (object)activity.Outcome ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", (object)activity.CreatedBy ?? DBNull.Value));
                conn.Open();

                var result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
        }

        // Full activity timeline for a lead, newest first.
        public List<enPreSalesActivity> GetActivityByLeadId(int leadId)
        {
            var list = new List<enPreSalesActivity>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetActivityByLeadId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LeadID", leadId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesActivity
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            LeadID = Convert.ToInt32(rdr["LeadID"]),
                            ActivityType = rdr["ActivityType"] == DBNull.Value ? "" : rdr["ActivityType"].ToString(),
                            ActivityNote = rdr["ActivityNote"] == DBNull.Value ? "" : rdr["ActivityNote"].ToString(),
                            Outcome = rdr["Outcome"] == DBNull.Value ? "" : rdr["Outcome"].ToString(),
                            CreatedBy = rdr["CreatedBy"] == DBNull.Value ? "" : rdr["CreatedBy"].ToString(),
                            CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"])
                        });
                    }
                }
            }

            return list;
        }

        // "Companies" page's "Active customers — don't pitch" tab — real
        // paying customers cross-checked from SalesPackageSubscriptions /
        // SA_Customer / SA_CompanyMaster (active subscription, ToDate in
        // the future), not from SA_MaskEmail_PreSales. Company-wide list,
        // not scoped to a single presales rep.
        public List<enPreSalesActiveCustomer> GetActiveCustomerList(string search)
        {
            var list = new List<enPreSalesActiveCustomer>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetActiveCustomerList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Search", search ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesActiveCustomer
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            FirstContactName = rdr["FirstContactName"] == DBNull.Value ? "" : rdr["FirstContactName"].ToString(),
                            FirstContactEmail = rdr["FirstContactEmail"] == DBNull.Value ? "" : rdr["FirstContactEmail"].ToString(),
                            FirstContactNumber = rdr["FirstContactNumber"] == DBNull.Value ? "" : rdr["FirstContactNumber"].ToString(),
                            SecondContactName = rdr["SecondContactName"] == DBNull.Value ? "" : rdr["SecondContactName"].ToString(),
                            SecondContactEmail = rdr["SecondContactEmail"] == DBNull.Value ? "" : rdr["SecondContactEmail"].ToString(),
                            SecondContactNumber = rdr["SecondContactNumber"] == DBNull.Value ? "" : rdr["SecondContactNumber"].ToString(),
                            CustomerSince = rdr["CustomerSince"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CustomerSince"]),
                            ModuleNames = rdr["ModuleNames"] == DBNull.Value ? "" : rdr["ModuleNames"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        private enPreSalesActiveCustomer ConstructActiveCustomerFromReader(IDataReader rdr)
        {
            return new enPreSalesActiveCustomer
            {
                ID = Convert.ToInt32(rdr["ID"]),
                CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                FirstContactName = rdr["FirstContactName"] == DBNull.Value ? "" : rdr["FirstContactName"].ToString(),
                FirstContactEmail = rdr["FirstContactEmail"] == DBNull.Value ? "" : rdr["FirstContactEmail"].ToString(),
                FirstContactNumber = rdr["FirstContactNumber"] == DBNull.Value ? "" : rdr["FirstContactNumber"].ToString(),
                SecondContactName = rdr["SecondContactName"] == DBNull.Value ? "" : rdr["SecondContactName"].ToString(),
                SecondContactEmail = rdr["SecondContactEmail"] == DBNull.Value ? "" : rdr["SecondContactEmail"].ToString(),
                SecondContactNumber = rdr["SecondContactNumber"] == DBNull.Value ? "" : rdr["SecondContactNumber"].ToString(),
                CustomerSince = rdr["CustomerSince"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CustomerSince"]),
                ModuleNames = rdr["ModuleNames"] == DBNull.Value ? "" : rdr["ModuleNames"].ToString(),
                CountryName = HasColumn(rdr, "CountryName") && rdr["CountryName"] != DBNull.Value ? rdr["CountryName"].ToString() : ""
            };
        }

        // True server-side page (10/page from the UI) instead of fetching
        // every active customer to the browser -- this list is company-wide,
        // not scoped to one rep, so it can legitimately get very large.
        // Single proc call -- TotalCount rides along on every row via
        // COUNT(*) OVER(), so no separate ...Count proc/round-trip is needed
        // (same consolidation as GetInactiveCustomerListPaged).
        public List<enPreSalesActiveCustomer> GetActiveCustomerListPaged(string search, int pageNumber, int pageSize, out int totalCount)
        {
            var list = new List<enPreSalesActiveCustomer>();
            totalCount = 0;

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetActiveCustomerListPaged", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Search", search ?? ""));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(ConstructActiveCustomerFromReader(rdr));

                        if (totalCount == 0 && HasColumn(rdr, "TotalCount") && rdr["TotalCount"] != DBNull.Value)
                        {
                            totalCount = Convert.ToInt32(rdr["TotalCount"]);
                        }
                    }
                }
            }

            return list;
        }

        // "Inactive customers" tab -- opposite of Active customers: company-
        // wide, true server-side page (10/page), not scoped to one rep.
        // Single proc call -- TotalCount rides along on every row via
        // COUNT(*) OVER(), so no separate ...Count proc/round-trip is needed.
        public List<enPreSalesInactiveCustomer> GetInactiveCustomerListPaged(string search, int pageNumber, int pageSize, out int totalCount)
        {
            var list = new List<enPreSalesInactiveCustomer>();
            totalCount = 0;

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetInactiveCustomerListPaged", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Search", search ?? ""));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesInactiveCustomer
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            FirstContactName = rdr["FirstContactName"] == DBNull.Value ? "" : rdr["FirstContactName"].ToString(),
                            FirstContactEmail = rdr["FirstContactEmail"] == DBNull.Value ? "" : rdr["FirstContactEmail"].ToString(),
                            FirstContactNumber = rdr["FirstContactNumber"] == DBNull.Value ? "" : rdr["FirstContactNumber"].ToString(),
                            SecondContactName = rdr["SecondContactName"] == DBNull.Value ? "" : rdr["SecondContactName"].ToString(),
                            SecondContactEmail = rdr["SecondContactEmail"] == DBNull.Value ? "" : rdr["SecondContactEmail"].ToString(),
                            SecondContactNumber = rdr["SecondContactNumber"] == DBNull.Value ? "" : rdr["SecondContactNumber"].ToString(),
                            CustomerSince = rdr["CustomerSince"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CustomerSince"]),
                            ExpiredOn = rdr["ExpiredOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["ExpiredOn"]),
                            ModuleNames = rdr["ModuleNames"] == DBNull.Value ? "" : rdr["ModuleNames"].ToString()
                        });

                        if (totalCount == 0 && rdr["TotalCount"] != DBNull.Value)
                        {
                            totalCount = Convert.ToInt32(rdr["TotalCount"]);
                        }
                    }
                }
            }

            return list;
        }

        // "Companies" page's "Demoed, not closed — re-target" tab — every
        // SA_MaskEmail_Open row for companies THIS presales rep already
        // transferred to sales (joined on CorpEmail via SA_MaskEmail_PreSales,
        // scoped to Status='Transferred'), per the original query the user
        // gave:
        //   select smo.* from SA_MaskEmail_PreSales smps
        //   inner join SA_MaskEmail_Open smo on smo.CorpEmail = smps.CorpEmail
        //   where smps.CreatedBy = @CreatedBy and smps.Status = 'Transferred'
        // now extended with search/country/date-range filters + paging via
        // usp_PreSalesNew_GetDemoedNotClosedList. DateFrom/DateTo are plain
        // ISO (yyyy-MM-dd) strings straight from an <input type="date">,
        // filtered on smo.CreatedDate specifically (not the presales lead's
        // own CreatedDate).
        private enPreSalesOpenLeadRow ConstructOpenLeadFromReader(IDataReader rdr)
        {
            return new enPreSalesOpenLeadRow
            {
                ID = Convert.ToInt32(rdr["ID"]),
                IsEmail = rdr["IsEmail"] == DBNull.Value ? "" : rdr["IsEmail"].ToString(),
                Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                CorpEmail = rdr["CorpEmail"] == DBNull.Value ? "" : rdr["CorpEmail"].ToString(),
                TelephoneCountryCode = rdr["TelephoneCountryCode"] == DBNull.Value ? "" : rdr["TelephoneCountryCode"].ToString(),
                Telephone = rdr["Telephone"] == DBNull.Value ? "" : rdr["Telephone"].ToString(),
                ContactCountryCode = rdr["ContactCountryCode"] == DBNull.Value ? "" : rdr["ContactCountryCode"].ToString(),
                ContactNumber = rdr["ContactNumber"] == DBNull.Value ? "" : rdr["ContactNumber"].ToString(),
                CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                Designation = rdr["Designation"] == DBNull.Value ? "" : rdr["Designation"].ToString(),
                CountryFID = rdr["CountryFID"] == DBNull.Value ? "" : rdr["CountryFID"].ToString(),
                CountryName = rdr["CountryName"] == DBNull.Value ? "" : rdr["CountryName"].ToString(),
                StateFID = rdr["StateFID"] == DBNull.Value ? "" : rdr["StateFID"].ToString(),
                StateName = rdr["StateName"] == DBNull.Value ? "" : rdr["StateName"].ToString(),
                IPAddress = rdr["IPAddress"] == DBNull.Value ? "" : rdr["IPAddress"].ToString(),
                Message = rdr["Message"] == DBNull.Value ? "" : rdr["Message"].ToString(),
                Source = rdr["Source"] == DBNull.Value ? "" : rdr["Source"].ToString(),
                RequestedDate = rdr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["RequestedDate"]),
                CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                CreatedBy = rdr["CreatedBy"] == DBNull.Value ? "" : rdr["CreatedBy"].ToString(),
                OutboundLeadSource = rdr["OutboundLeadSource"] == DBNull.Value ? "" : rdr["OutboundLeadSource"].ToString(),
                CRM_LeadStatus = rdr["CRM_LeadStatus"] == DBNull.Value ? "" : rdr["CRM_LeadStatus"].ToString(),
                Status = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                LeadType = rdr["LeadType"] == DBNull.Value ? "" : rdr["LeadType"].ToString(),
                UpdateDate = rdr["UpdateDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["UpdateDate"]),
                IsActive = rdr["IsActive"] == DBNull.Value ? "" : rdr["IsActive"].ToString(),
                MaskMail_Id = rdr["MaskMail_Id"] == DBNull.Value ? "" : rdr["MaskMail_Id"].ToString(),
                Lead_status = rdr["Lead_status"] == DBNull.Value ? "" : rdr["Lead_status"].ToString(),
                Set_Follow_up = rdr["Set_Follow_up"] == DBNull.Value ? "" : rdr["Set_Follow_up"].ToString(),
                Amount = rdr["Amount"] == DBNull.Value ? "" : rdr["Amount"].ToString(),
                Expected_Close_Date = rdr["Expected_Close_Date"] == DBNull.Value ? "" : rdr["Expected_Close_Date"].ToString(),
                Currency = rdr["Currency"] == DBNull.Value ? "" : rdr["Currency"].ToString(),
                OpenCreatedDate = rdr["OpenCreatedDate"] == DBNull.Value ? "" : rdr["OpenCreatedDate"].ToString(),
                LostReason = rdr["LostReason"] == DBNull.Value ? "" : rdr["LostReason"].ToString(),
                InvoiceNumber = rdr["InvoiceNumber"] == DBNull.Value ? "" : rdr["InvoiceNumber"].ToString(),
                OpenUpdateddate = rdr["OpenUpdateddate"] == DBNull.Value ? "" : rdr["OpenUpdateddate"].ToString(),
                Attechments = rdr["Attechments"] == DBNull.Value ? "" : rdr["Attechments"].ToString(),
                PaymentTerms = rdr["PaymentTerms"] == DBNull.Value ? "" : rdr["PaymentTerms"].ToString(),
                PaymentOption = rdr["PaymentOption"] == DBNull.Value ? "" : rdr["PaymentOption"].ToString(),
                RemainingPaymentDate = rdr["RemainingPaymentDate"] == DBNull.Value ? "" : rdr["RemainingPaymentDate"].ToString(),
                RemainingAmount = rdr["RemainingAmount"] == DBNull.Value ? "" : rdr["RemainingAmount"].ToString()
            };
        }

        public List<enPreSalesOpenLeadRow> GetDemoedNotClosedList(string search, string country, string dateFrom, string dateTo, string createdBy, int pageNumber, int pageSize)
        {
            var list = new List<enPreSalesOpenLeadRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetDemoedNotClosedList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Search", search ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Country", country ?? ""));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", dateFrom ?? ""));
                cmd.Parameters.Add(new SqlParameter("@DateTo", dateTo ?? ""));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(ConstructOpenLeadFromReader(rdr));
                    }
                }
            }

            return list;
        }

        public int GetDemoedNotClosedListCount(string search, string country, string dateFrom, string dateTo, string createdBy)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetDemoedNotClosedListCount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Search", search ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Country", country ?? ""));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", dateFrom ?? ""));
                cmd.Parameters.Add(new SqlParameter("@DateTo", dateTo ?? ""));
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // "All countries" filter dropdown on the "Demoed, not closed" tab —
        // distinct CountryName values actually present among THIS user's
        // demoed-not-closed rows, same scoping convention as
        // GetSourceList/GetStatusList.
        public List<enPreSalesFilterOption> GetDemoedNotClosedCountryList(string createdBy)
        {
            var list = new List<enPreSalesFilterOption>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetDemoedNotClosedCountryList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var val = rdr["CountryName"] == DBNull.Value ? "" : rdr["CountryName"].ToString();
                        if (!string.IsNullOrEmpty(val))
                        {
                            list.Add(new enPreSalesFilterOption { Value = val, Text = val });
                        }
                    }
                }
            }

            return list;
        }

        // "Incentives" page — every month this user has leads for, via the
        // EXISTING usp_GetSalesLeadStatusReporyPreSales proc (given by the
        // user, never altered here). That proc takes @searchKeyword as a
        // comma-separated list matched against CreatedBy (dbo.StringSplit),
        // so a single name still works fine as a one-item list. Passing
        // DBNull for @DateFrom/@DateTo makes the proc take its "all months,
        // no date filter" branch, which is what the Monthly performance
        // table + KPI cards need (they do their own month/quarter bucketing
        // over the full result client-side).
        public List<enPreSalesMonthlyPerformance> GetMonthlyPerformance(string createdBy)
        {
            var list = new List<enPreSalesMonthlyPerformance>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_GetSalesLeadStatusReporyPreSales", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@searchKeyword", (object)createdBy ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DBNull.Value));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesMonthlyPerformance
                        {
                            MonthName = rdr["MonthName"] == DBNull.Value ? "" : rdr["MonthName"].ToString(),
                            YearNo = rdr["YearNo"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["YearNo"]),
                            CreatedBy = HasColumn(rdr, "CreatedBy") && rdr["CreatedBy"] != DBNull.Value ? rdr["CreatedBy"].ToString() : "",
                            TotalLead = rdr["TotalLead"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["TotalLead"]),
                            TotalOpenLead = rdr["TotalOpenLead"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["TotalOpenLead"]),
                            LeadTransfer = rdr["LeadTransfer"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["LeadTransfer"]),
                            WonLead = rdr["WonLead"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["WonLead"]),
                            DemoCount = rdr["DemoCount"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["DemoCount"]),
                            UnusedLead = rdr["UnusedLead"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["UnusedLead"])
                        });
                    }
                }
            }

            return list;
        }

        // "Incentives" page's "Demos completed" table — real leads, via
        // usp_PreSalesNew_GetDemosCompletedList (new proc, built from
        // SA_MaskEmail_Open_DemoDetails's real columns: maskemailopenid,
        // lastdemodate, demodescussion, createdby, createddate).
        public List<enPreSalesDemoCompletedRow> GetDemosCompletedList(string createdBy)
        {
            var list = new List<enPreSalesDemoCompletedRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetDemosCompletedList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(ConstructDemoCompletedFromReader(rdr));
                    }
                }
            }

            return list;
        }

        private enPreSalesDemoCompletedRow ConstructDemoCompletedFromReader(IDataReader rdr)
        {
            return new enPreSalesDemoCompletedRow
            {
                CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                ContactName = rdr["ContactName"] == DBNull.Value ? "" : rdr["ContactName"].ToString(),
                CorpEmail = rdr["CorpEmail"] == DBNull.Value ? "" : rdr["CorpEmail"].ToString(),
                Source = rdr["Source"] == DBNull.Value ? "" : rdr["Source"].ToString(),
                DemoDate = rdr["DemoDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["DemoDate"]),
                DemoBySales = rdr["DemoBySales"] == DBNull.Value ? "" : rdr["DemoBySales"].ToString(),
                Status = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString()
            };
        }

        // True server-side page (10/page) for the Incentives page's "Demos
        // completed" table -- this can legitimately grow very large over
        // time, so the browser should never be asked to hold the whole
        // history in memory.
        public List<enPreSalesDemoCompletedRow> GetDemosCompletedListPaged(string createdBy, string search, int pageNumber, int pageSize)
        {
            var list = new List<enPreSalesDemoCompletedRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetDemosCompletedListPaged", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Search", search ?? ""));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(ConstructDemoCompletedFromReader(rdr));
                    }
                }
            }

            return list;
        }

        public int GetDemosCompletedListPagedCount(string createdBy, string search)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetDemosCompletedListPagedCount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Search", search ?? ""));
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Every distinct demoed lead within one specific range (this month /
        // this quarter / last 6 months) -- naturally bounded by the date
        // window regardless of how big the overall history gets, so it's
        // safe to fetch in full for the KPI card's "see the leads" popup.
        public List<enPreSalesDemoCompletedRow> GetDemosCompletedListByRange(string createdBy, string rangeType)
        {
            var list = new List<enPreSalesDemoCompletedRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetDemosCompletedListByRange", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@RangeType", rangeType ?? "month"));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(ConstructDemoCompletedFromReader(rdr));
                    }
                }
            }

            return list;
        }

        // The 3 incentive KPI numbers (This month / This quarter / Last 6
        // months), computed and counted entirely in SQL -- replaces fetching
        // every demoed lead to the browser just to count date ranges there.
        public enPreSalesIncentiveKpiCounts GetIncentiveKpiCounts(string createdBy)
        {
            var counts = new enPreSalesIncentiveKpiCounts();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetIncentiveKpiCounts", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        counts.ThisMonth = Convert.ToInt32(rdr["ThisMonth"]);
                        counts.ThisQuarter = Convert.ToInt32(rdr["ThisQuarter"]);
                        counts.SixMonths = Convert.ToInt32(rdr["SixMonths"]);
                    }
                }
            }

            return counts;
        }

        // Small, bounded (max 6 rows) month-by-month breakdown for the
        // "Incentive earned" popup on the Dashboard -- grouped server-side.
        public List<enPreSalesIncentiveMonthCount> GetIncentiveLast6MonthsBreakdown(string createdBy)
        {
            var list = new List<enPreSalesIncentiveMonthCount>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetIncentiveLast6MonthsBreakdown", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesIncentiveMonthCount
                        {
                            MonthLabel = rdr["MonthLabel"] == DBNull.Value ? "" : rdr["MonthLabel"].ToString(),
                            Count = Convert.ToInt32(rdr["Count"])
                        });
                    }
                }
            }

            return list;
        }

        // Monthly "Demos done" counts by ACTUAL demo date (lastdemodate),
        // not RequestedDate -- keeps the Monthly Performance table's Demos
        // done / Incentive earned columns consistent with the top Incentive
        // KPI cards and the Demos completed table, which are also demo-date
        // based. Naturally bounded by number of distinct months, safe to
        // fetch in full.
        public List<enPreSalesMonthDemoCount> GetMonthlyDemoCountsByDemoDate(string createdBy)
        {
            var list = new List<enPreSalesMonthDemoCount>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetMonthlyDemoCountsByDemoDate", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesMonthDemoCount
                        {
                            MonthNo = Convert.ToInt32(rdr["MonthNo"]),
                            YearNo = Convert.ToInt32(rdr["YearNo"]),
                            DemoCount = Convert.ToInt32(rdr["DemoCount"])
                        });
                    }
                }
            }

            return list;
        }

        // Demos completed in one specific calendar month (demo date basis) --
        // powers the Monthly Performance table's "Demos done" drill-down,
        // kept consistent with GetMonthlyDemoCountsByDemoDate above.
        public List<enPreSalesDemoCompletedRow> GetDemosCompletedListByMonth(string createdBy, int monthNo, int yearNo)
        {
            var list = new List<enPreSalesDemoCompletedRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetDemosCompletedListByMonth", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@MonthNo", monthNo));
                cmd.Parameters.Add(new SqlParameter("@YearNo", yearNo));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(ConstructDemoCompletedFromReader(rdr));
                    }
                }
            }

            return list;
        }

        // True server-side page/search for the Monthly Performance table --
        // wraps the real usp_GetSalesLeadStatusReporyPreSales report (via
        // usp_PreSalesNew_GetMonthlyPerformancePaged's INSERT...EXEC capture,
        // untouched) merged with the demo-date based count, entirely in SQL.
        public List<enPreSalesMonthlyPerformancePagedRow> GetMonthlyPerformancePaged(string createdBy, string search, int pageNumber, int pageSize)
        {
            var list = new List<enPreSalesMonthlyPerformancePagedRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetMonthlyPerformancePaged", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Search", search ?? ""));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesMonthlyPerformancePagedRow
                        {
                            MonthName = rdr["MonthName"] == DBNull.Value ? "" : rdr["MonthName"].ToString(),
                            YearNo = Convert.ToInt32(rdr["YearNo"]),
                            MonthNo = Convert.ToInt32(rdr["MonthNo"]),
                            TotalLead = Convert.ToInt32(rdr["TotalLead"]),
                            LeadTransfer = Convert.ToInt32(rdr["LeadTransfer"]),
                            WonLead = Convert.ToInt32(rdr["WonLead"]),
                            DemoDateBasedCount = Convert.ToInt32(rdr["DemoDateBasedCount"])
                        });
                    }
                }
            }

            return list;
        }

        public int GetMonthlyPerformancePagedCount(string createdBy, string search)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetMonthlyPerformancePagedCount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Search", search ?? ""));
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // "Incentives" page's Monthly performance row click-through popup
        // — every lead this user generated in the clicked month, via
        // usp_PreSalesNew_GetMonthlyPerformanceDetail (new proc).
        public List<enPreSalesMonthlyLeadDetail> GetMonthlyPerformanceDetail(string createdBy, int monthNo, int yearNo)
        {
            var list = new List<enPreSalesMonthlyLeadDetail>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetMonthlyPerformanceDetail", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@MonthNo", monthNo));
                cmd.Parameters.Add(new SqlParameter("@YearNo", yearNo));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesMonthlyLeadDetail
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                            CorpEmail = rdr["CorpEmail"] == DBNull.Value ? "" : rdr["CorpEmail"].ToString(),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            OutboundLeadSource = rdr["OutboundLeadSource"] == DBNull.Value ? "" : rdr["OutboundLeadSource"].ToString(),
                            PreSalesStatus = rdr["PreSalesStatus"] == DBNull.Value ? "" : rdr["PreSalesStatus"].ToString(),
                            RequestedDate = rdr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["RequestedDate"]),
                            OpenStatus = rdr["OpenStatus"] == DBNull.Value ? "" : rdr["OpenStatus"].ToString(),
                            HasDemo = rdr["HasDemo"] != DBNull.Value && Convert.ToInt32(rdr["HasDemo"]) == 1
                        });
                    }
                }
            }

            return list;
        }
    }
}
