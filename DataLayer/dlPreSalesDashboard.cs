using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DataLayer
{
    // DAL for the new PreSalesDashboard page — every query goes through a
    // stored procedure (see chat for the usp_PreSalesNew_Dashboard* scripts),
    // no inline SQL. All methods are scoped to the logged-in presales user
    // (createdBy), same convention as dlPreSalesNew.
    public class dlPreSalesDashboard
    {
        public enPreSalesDashboardStats GetStats(string createdBy)
        {
            var stats = new enPreSalesDashboardStats();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetDashboardStats", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        stats.LeadsGenerated = Convert.ToInt32(rdr["LeadsGenerated"]);
                        stats.DemosFixed = Convert.ToInt32(rdr["DemosFixed"]);
                        stats.FollowUpsDue = Convert.ToInt32(rdr["FollowUpsDue"]);
                        stats.PendingTransfer = Convert.ToInt32(rdr["PendingTransfer"]);
                        stats.AwaitingDemo = Convert.ToInt32(rdr["AwaitingDemo"]);
                    }
                }
            }

            return stats;
        }

        public List<enPreSalesSourceCount> GetLeadsBySource(string createdBy)
        {
            var list = new List<enPreSalesSourceCount>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetLeadsBySource", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesSourceCount
                        {
                            Source = rdr["Source"] == DBNull.Value ? "Other" : rdr["Source"].ToString(),
                            Count = Convert.ToInt32(rdr["Cnt"])
                        });
                    }
                }
            }

            return list;
        }

        // Paged/searchable version of the Follow-ups list, backing the full
        // "Follow-ups" table on the Dashboard page (the KPI popup keeps using
        // the unpaged GetFollowupsDueList above for its quick top-10 view).
        // NOTE: as of the latest redefinition, "Follow-ups due" no longer
        // means "has an open follow-up record" -- it means the opposite:
        // active (Status='New') leads that have NEVER had a follow-up set on
        // them at all. FollowUpDate/Note no longer apply since there's no
        // follow-up row to read from for these leads.
        public List<enPreSalesFollowupRow> GetFollowupsDueListPaged(string createdBy, string search, int pageNumber, int pageSize)
        {
            var list = new List<enPreSalesFollowupRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetFollowupsDueListPaged", conn))
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
                        list.Add(new enPreSalesFollowupRow
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            LeadID = Convert.ToInt32(rdr["LeadID"]),
                            LeadName = rdr["LeadName"] == DBNull.Value ? "" : rdr["LeadName"].ToString(),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        public int GetFollowupsDueListPagedCount(string createdBy, string search)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetFollowupsDueListPagedCount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Search", search ?? ""));
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<enPreSalesFollowupRow> GetFollowupsDueList(string createdBy)
        {
            var list = new List<enPreSalesFollowupRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetFollowupsDueList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesFollowupRow
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            LeadID = Convert.ToInt32(rdr["LeadID"]),
                            LeadName = rdr["LeadName"] == DBNull.Value ? "" : rdr["LeadName"].ToString(),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        public List<enPreSalesTransferredRow> GetTransferredLeads(string createdBy)
        {
            var list = new List<enPreSalesTransferredRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetTransferredLeads", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(ConstructTransferredFromReader(rdr));
                    }
                }
            }

            return list;
        }

        private enPreSalesTransferredRow ConstructTransferredFromReader(IDataReader rdr)
        {
            return new enPreSalesTransferredRow
            {
                ID = Convert.ToInt32(rdr["ID"]),
                Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                OutboundLeadSource = rdr["OutboundLeadSource"] == DBNull.Value ? "" : rdr["OutboundLeadSource"].ToString(),
                MailChain = rdr["MailChain"] == DBNull.Value ? "" : rdr["MailChain"].ToString(),
                Status = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                UpdateDate = rdr["UpdateDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["UpdateDate"])
            };
        }

        // True server-side page (10/page) for the Dashboard's "Transferred —
        // Awaiting sales demo" table -- GetTransferredLeads above stays as a
        // small quick-glance list for the KPI card's popup, this one is the
        // real paginated/searchable table.
        public List<enPreSalesTransferredRow> GetTransferredListPaged(string createdBy, string search, int pageNumber, int pageSize)
        {
            var list = new List<enPreSalesTransferredRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetTransferredListPaged", conn))
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
                        list.Add(ConstructTransferredFromReader(rdr));
                    }
                }
            }

            return list;
        }

        public int GetTransferredListPagedCount(string createdBy, string search)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetTransferredListPagedCount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Search", search ?? ""));
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<enPreSalesWorkTodayRow> GetWorkTodayLeads(string createdBy)
        {
            var list = new List<enPreSalesWorkTodayRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetWorkTodayLeads", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesWorkTodayRow
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            CorpEmail = rdr["CorpEmail"] == DBNull.Value ? "" : rdr["CorpEmail"].ToString(),
                            OutboundLeadSource = rdr["OutboundLeadSource"] == DBNull.Value ? "" : rdr["OutboundLeadSource"].ToString(),
                            Status = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                            LastOutcome = rdr["LastOutcome"] == DBNull.Value ? "" : rdr["LastOutcome"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // Real demo/won numbers — this calls the EXISTING usp_PreSalesNew_GetDemosByMonth
        // (already in the database, not created/altered by this app) using its
        // real parameters: @searchKeyword (comma-separated CreatedBy names —
        // we just pass the single logged-in user's name) and @DateFrom/@DateTo
        // (strings; empty string means "all time", matching the proc's own
        // @DateFrom != '' check). Only the most recent 6 rows (the proc already
        // orders by Year/Month ascending) are kept here for a readable chart.
        public List<enPreSalesMonthDemo> GetDemosByMonth(string createdBy)
        {
            var list = new List<enPreSalesMonthDemo>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetDemosByMonth", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@searchKeyword", (object)createdBy ?? DBNull.Value));
                // NULL (not "") for both, matching the proc's @DateFrom/@DateTo
                // datetime params -- NULL makes its IF check fall through to the
                // "all months" branch cleanly, same convention as
                // GetMonthlyPerformance's call to usp_GetSalesLeadStatusReporyPreSales.
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DBNull.Value));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesMonthDemo
                        {
                            MonthName = rdr["MonthName"] == DBNull.Value ? "" : rdr["MonthName"].ToString(),
                            YearNo = rdr["YearNo"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["YearNo"]),
                            DemoCount = rdr["DemoCount"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["DemoCount"]),
                            WonLead = rdr["WonLead"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["WonLead"])
                        });
                    }
                }
            }

            if (list.Count > 6)
            {
                list = list.GetRange(list.Count - 6, 6);
            }

            return list;
        }

        public enPreSalesFunnel GetFunnel(string createdBy)
        {
            var funnel = new enPreSalesFunnel();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_PreSalesNew_GetLeadFunnel", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        funnel.Generated = Convert.ToInt32(rdr["Generated"]);
                        funnel.Contacted = Convert.ToInt32(rdr["Contacted"]);
                        funnel.DemoAgreed = Convert.ToInt32(rdr["DemoAgreed"]);
                        funnel.Transferred = Convert.ToInt32(rdr["Transferred"]);
                    }
                }
            }

            return funnel;
        }

        // ---- KPI card "click to see details" popups ------------------------

        public List<enPreSalesStatDetailRow> GetLeadsGeneratedList(string createdBy)
        {
            return RunStatDetailQuery("usp_PreSalesNew_GetLeadsGeneratedList", createdBy);
        }

        public List<enPreSalesStatDetailRow> GetDemosFixedList(string createdBy)
        {
            return RunStatDetailQuery("usp_PreSalesNew_GetDemosFixedList", createdBy);
        }

        public List<enPreSalesStatDetailRow> GetPendingTransferList(string createdBy)
        {
            return RunStatDetailQuery("usp_PreSalesNew_GetPendingTransferList", createdBy);
        }

        private List<enPreSalesStatDetailRow> RunStatDetailQuery(string procName, string createdBy)
        {
            var list = new List<enPreSalesStatDetailRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand(procName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enPreSalesStatDetailRow
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            DetailDate = rdr["DetailDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["DetailDate"]),
                            DetailNote = rdr["DetailNote"] == DBNull.Value ? "" : rdr["DetailNote"].ToString()
                        });
                    }
                }
            }

            return list;
        }
    }
}
