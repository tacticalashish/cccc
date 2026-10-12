using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DataLayer
{
    // DAL for the new SalesNewController "My Day" dashboard — every query
    // goes through a stored procedure, no inline SQL text, same pattern as
    // DataLayer.dlPreSalesDashboard. All 7 procs are scoped to the
    // logged-in sales rep (CreatedBy on SA_MaskEmail_Open), same connection
    // string as the rest of this project
    // (ApplicationSettings.DefaultConnectionString -> ChemAnalystContext).
    public class dlSalesNew
    {
        // The 6 top KPI cards (Open leads / Pipeline value / Closing this
        // month / Overdue follow-ups / Win rate), computed and counted
        // entirely in SQL rather than fetching every open lead to the
        // browser just to count/sum them there.
        public enSalesKpiSummary GetKpiSummary(string createdBy)
        {
            var summary = new enSalesKpiSummary();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetKpiSummary", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        summary.OpenLeadsCount = rdr["OpenLeadsCount"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["OpenLeadsCount"]);
                        summary.PipelineValue = rdr["PipelineValue"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PipelineValue"]);
                        summary.ClosingThisMonthCount = rdr["ClosingThisMonthCount"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ClosingThisMonthCount"]);
                        summary.ClosingThisMonthAmount = rdr["ClosingThisMonthAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["ClosingThisMonthAmount"]);
                        summary.OverdueFollowupsCount = rdr["OverdueFollowupsCount"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["OverdueFollowupsCount"]);
                        summary.WinRatePercent = rdr["WinRatePercent"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["WinRatePercent"]);
                    }
                }
            }

            return summary;
        }

        // "Follow-ups due today" table — true server-side search + paging,
        // same pattern as PreSalesNew's paged lists (single proc,
        // COUNT(*) OVER() for the total).
        public List<enSalesFollowupDueRow> GetFollowupsDueToday(string createdBy, string search, int pageNumber, int pageSize, out int totalCount)
        {
            var list = new List<enSalesFollowupDueRow>();
            totalCount = 0;

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetFollowupsDueToday", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Search", (object)search ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesFollowupDueRow
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                            CorpEmail = rdr["CorpEmail"] == DBNull.Value ? "" : rdr["CorpEmail"].ToString(),
                            Status = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                            Amount = rdr["Amount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(rdr["Amount"]),
                            Currency = rdr["Currency"] == DBNull.Value ? "" : rdr["Currency"].ToString()
                        });
                        if (totalCount == 0 && rdr["TotalCount"] != DBNull.Value)
                            totalCount = Convert.ToInt32(rdr["TotalCount"]);
                    }
                }
            }

            return list;
        }

        // "Demos in progress" list — true server-side search + paging, same
        // pattern as the Follow-ups list above.
        public List<enSalesDemoInProgressRow> GetDemosInProgress(string createdBy, string search, int pageNumber, int pageSize, out int totalCount)
        {
            var list = new List<enSalesDemoInProgressRow>();
            totalCount = 0;

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetDemosInProgress", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Search", (object)search ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesDemoInProgressRow
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            CorpEmail = rdr["CorpEmail"] == DBNull.Value ? "" : rdr["CorpEmail"].ToString(),
                            CountryName = rdr["CountryName"] == DBNull.Value ? "" : rdr["CountryName"].ToString(),
                            Status = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                            LastDemoDate = rdr["LastDemoDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["LastDemoDate"])
                        });
                        if (totalCount == 0 && rdr["TotalCount"] != DBNull.Value)
                            totalCount = Convert.ToInt32(rdr["TotalCount"]);
                    }
                }
            }

            return list;
        }

        // "Pipeline by stage" bar chart.
        public List<enSalesPipelineStageCount> GetPipelineByStage(string createdBy)
        {
            var list = new List<enSalesPipelineStageCount>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetPipelineByStage", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesPipelineStageCount
                        {
                            Status = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                            Count = rdr["Count"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["Count"])
                        });
                    }
                }
            }

            return list;
        }

        // "Pipeline value & forecast" chart -- Committed ($K) bars only, no
        // weighted-forecast line (no query was given for that series), and
        // scoped to just the current + previous calendar year.
        public List<enSalesPipelineForecastRow> GetPipelineValueForecast(string createdBy)
        {
            var list = new List<enSalesPipelineForecastRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetPipelineValueForecast", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesPipelineForecastRow
                        {
                            YearNo = Convert.ToInt32(rdr["Year"]),
                            MonthName = rdr["Month"] == DBNull.Value ? "" : rdr["Month"].ToString(),
                            Currency = rdr["Currency"] == DBNull.Value ? "" : rdr["Currency"].ToString(),
                            TotalAmount = rdr["TotalAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["TotalAmount"])
                        });
                    }
                }
            }

            return list;
        }

        // "Won vs lost" chart.
        public List<enSalesWonLostRow> GetWonVsLost(string createdBy, DateTime? fromDate, DateTime? toDate)
        {
            var list = new List<enSalesWonLostRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetWonVsLost", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@FromDate", (object)fromDate ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ToDate", (object)toDate ?? DBNull.Value));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesWonLostRow
                        {
                            Status = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                            YearNo = Convert.ToInt32(rdr["Year"]),
                            MonthName = rdr["Month"] == DBNull.Value ? "" : rdr["Month"].ToString(),
                            Count = rdr["Count"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["Count"])
                        });
                    }
                }
            }

            return list;
        }

        // "Leads by source" donut -- percentage share already computed
        // server-side.
        public List<enSalesLeadSourceRow> GetLeadsBySource(string createdBy)
        {
            var list = new List<enSalesLeadSourceRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetLeadsBySource", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesLeadSourceRow
                        {
                            Source = rdr["Source"] == DBNull.Value ? "" : rdr["Source"].ToString(),
                            SourceCount = rdr["SourceCount"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SourceCount"]),
                            TotalCount = rdr["TotalCount"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["TotalCount"]),
                            LeadPercentage = rdr["LeadPercentage"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["LeadPercentage"])
                        });
                    }
                }
            }

            return list;
        }

        // Single-lead detail page.
        public enSalesLeadDetail GetLeadDetail(int id)
        {
            enSalesLeadDetail detail = null;

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetLeadDetail", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        detail = new enSalesLeadDetail
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            CorpEmail = rdr["CorpEmail"] == DBNull.Value ? "" : rdr["CorpEmail"].ToString(),
                            ContactCountryCode = rdr["ContactCountryCode"] == DBNull.Value ? "" : rdr["ContactCountryCode"].ToString(),
                            ContactNumber = rdr["ContactNumber"] == DBNull.Value ? "" : rdr["ContactNumber"].ToString(),
                            CountryName = rdr["CountryName"] == DBNull.Value ? "" : rdr["CountryName"].ToString(),
                            Status = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                            LeadStatus = rdr["Lead_status"] == DBNull.Value ? "" : rdr["Lead_status"].ToString(),
                            LeadType = rdr["LeadType"] == DBNull.Value ? "" : rdr["LeadType"].ToString(),
                            Source = rdr["Source"] == DBNull.Value ? "" : rdr["Source"].ToString(),
                            Amount = rdr["Amount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(rdr["Amount"]),
                            Currency = rdr["Currency"] == DBNull.Value ? "" : rdr["Currency"].ToString(),
                            ExpectedCloseDate = rdr["Expected_Close_Date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["Expected_Close_Date"]),
                            SetFollowUp = rdr["Set_Follow_up"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["Set_Follow_up"]),
                            CreatedBy = rdr["CreatedBy"] == DBNull.Value ? "" : rdr["CreatedBy"].ToString(),
                            Message = rdr["Message"] == DBNull.Value ? "" : rdr["Message"].ToString(),
                            CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                            // HasColumn-guarded: written by MarkLeadWon/MarkLeadLost, but
                            // this SELECT list needs updating (chat-text proc) before
                            // they'll actually come back - guarded so nothing breaks
                            // if that hasn't been run yet.
                            PaymentOption = HasColumn(rdr, "PaymentOption") && rdr["PaymentOption"] != DBNull.Value ? rdr["PaymentOption"].ToString() : "",
                            PaymentTerms = HasColumn(rdr, "PaymentTerms") && rdr["PaymentTerms"] != DBNull.Value ? rdr["PaymentTerms"].ToString() : "",
                            LostReason = HasColumn(rdr, "LostReason") && rdr["LostReason"] != DBNull.Value ? rdr["LostReason"].ToString() : ""
                        };
                    }
                }
            }

            return detail;
        }

        // Same as GetLeadDetail, but for a lead that's still New-stage - it
        // lives in SA_MaskEmail, not SA_MaskEmail_Open (the two tables have
        // independent identity columns, so an ID from one table's Pipeline
        // card means nothing looked up against the other). The proc selects
        // *, same as usp_SalesNew_GetPipelineNew, so every field is read
        // defensively via HasColumn - a column SA_MaskEmail doesn't have
        // just comes back blank instead of throwing.
        public enSalesLeadDetail GetLeadDetailNew(int id)
        {
            enSalesLeadDetail detail = null;

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetLeadDetailNew", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        detail = new enSalesLeadDetail
                        {
                            ID = HasColumn(rdr, "ID") && rdr["ID"] != DBNull.Value ? Convert.ToInt32(rdr["ID"]) : id,
                            Name = HasColumn(rdr, "Name") && rdr["Name"] != DBNull.Value ? rdr["Name"].ToString() : "",
                            CompanyName = HasColumn(rdr, "CompanyName") && rdr["CompanyName"] != DBNull.Value ? rdr["CompanyName"].ToString() : "",
                            CorpEmail = HasColumn(rdr, "CorpEmail") && rdr["CorpEmail"] != DBNull.Value ? rdr["CorpEmail"].ToString() : "",
                            ContactCountryCode = HasColumn(rdr, "ContactCountryCode") && rdr["ContactCountryCode"] != DBNull.Value ? rdr["ContactCountryCode"].ToString() : "",
                            ContactNumber = HasColumn(rdr, "ContactNumber") && rdr["ContactNumber"] != DBNull.Value ? rdr["ContactNumber"].ToString() : "",
                            CountryName = HasColumn(rdr, "CountryName") && rdr["CountryName"] != DBNull.Value ? rdr["CountryName"].ToString() : "",
                            Status = HasColumn(rdr, "Status") && rdr["Status"] != DBNull.Value ? rdr["Status"].ToString() : "",
                            LeadStatus = HasColumn(rdr, "Lead_status") && rdr["Lead_status"] != DBNull.Value ? rdr["Lead_status"].ToString() : "",
                            LeadType = HasColumn(rdr, "LeadType") && rdr["LeadType"] != DBNull.Value ? rdr["LeadType"].ToString() : "",
                            Source = HasColumn(rdr, "Source") && rdr["Source"] != DBNull.Value ? rdr["Source"].ToString() : "",
                            Amount = HasColumn(rdr, "Amount") && rdr["Amount"] != DBNull.Value ? Convert.ToDecimal(rdr["Amount"]) : (decimal?)null,
                            Currency = HasColumn(rdr, "Currency") && rdr["Currency"] != DBNull.Value ? rdr["Currency"].ToString() : "",
                            ExpectedCloseDate = HasColumn(rdr, "Expected_Close_Date") && rdr["Expected_Close_Date"] != DBNull.Value ? Convert.ToDateTime(rdr["Expected_Close_Date"]) : (DateTime?)null,
                            SetFollowUp = HasColumn(rdr, "Set_Follow_up") && rdr["Set_Follow_up"] != DBNull.Value ? Convert.ToDateTime(rdr["Set_Follow_up"]) : (DateTime?)null,
                            CreatedBy = HasColumn(rdr, "CreatedBy") && rdr["CreatedBy"] != DBNull.Value ? rdr["CreatedBy"].ToString() : "",
                            Message = HasColumn(rdr, "Message") && rdr["Message"] != DBNull.Value ? rdr["Message"].ToString() : "",
                            CreatedDate = HasColumn(rdr, "CreatedDate") && rdr["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(rdr["CreatedDate"]) : (DateTime?)null
                        };
                    }
                }
            }

            return detail;
        }

        // "Mark won - start closing" action button. Writes straight onto
        // the same SA_MaskEmail_Open row (by ID): Amount, Currency,
        // PaymentTerms, PaymentOption, plus Status='Won'. InvoiceNumber is
        // captured in the modal but not persisted yet - pending confirmation
        // of which column on SA_MaskEmail_Open it maps to.
        public bool MarkLeadWon(int id, decimal? amount, string currency, decimal? amountINR, string paymentOption, string paymentTerms , string invoiceNumber, string OrderFrom)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_MarkLeadWon", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                cmd.Parameters.Add(new SqlParameter("@Amount", (object)amount ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Currency", (object)currency ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@AmountINR", (object)amountINR ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PaymentOption", (object)paymentOption ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PaymentTerms", (object)paymentTerms ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@invoiceNumber", (object)invoiceNumber ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@OrderFrom", (object)OrderFrom ?? DBNull.Value));
                var updatedRowsParam = new SqlParameter("@UpdatedRows", SqlDbType.Int) { Direction = ParameterDirection.Output };

                cmd.Parameters.Add(updatedRowsParam);
                conn.Open();
                cmd.ExecuteNonQuery();
                return updatedRowsParam.Value != DBNull.Value && Convert.ToInt32(updatedRowsParam.Value) > 0;
            }
        }

        // "Update" on the Lead summary card's Value row (Open/Contacted-stage
        // lead only, in SA_MaskEmail_Open) - lets a rep correct the deal
        // Amount/Currency without going through the full "Mark won" flow.
        public bool UpdateLeadValue(int id, decimal? amount, string currency, decimal? amountINR)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_UpdateLeadValue", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                cmd.Parameters.Add(new SqlParameter("@Amount", (object)amount ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Currency", (object)currency ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@AmountINR", (object)amountINR ?? DBNull.Value));
                var updatedRowsParam = new SqlParameter("@UpdatedRows", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(updatedRowsParam);
                conn.Open();
                cmd.ExecuteNonQuery();
                return updatedRowsParam.Value != DBNull.Value && Convert.ToInt32(updatedRowsParam.Value) > 0;
            }
        }

        // Inline edit (Overview tab) - updates editable fields on the open lead
        // (SA_MaskEmail_Open); the proc blocks the update once Status = 'Won'.
        public bool UpdateLeadDetails(int id, string leadType, string source, string message, decimal? amount,
            DateTime? expectedCloseDate, DateTime? setFollowUp, string companyName, string name,
            string corpEmail, string contactNumber, string countryName)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_UpdateLeadDetails", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                cmd.Parameters.Add(new SqlParameter("@LeadType", (object)leadType ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Source", (object)source ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Message", (object)message ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Amount", (object)amount ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ExpectedCloseDate", expectedCloseDate.HasValue ? (object)expectedCloseDate.Value : DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@SetFollowUp", setFollowUp.HasValue ? (object)setFollowUp.Value : DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", (object)companyName ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Name", (object)name ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CorpEmail", (object)corpEmail ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ContactNumber", (object)contactNumber ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CountryName", (object)countryName ?? DBNull.Value));
                var updatedRowsParam = new SqlParameter("@UpdatedRows", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(updatedRowsParam);
                conn.Open();
                cmd.ExecuteNonQuery();
                return updatedRowsParam.Value != DBNull.Value && Convert.ToInt32(updatedRowsParam.Value) > 0;
            }
        }

        // "Mark lost" action button (Open/Contacted-stage lead, in
        // SA_MaskEmail_Open). Reason is written to that table's LostReason
        // column, confirmed by the user.
        public bool MarkLeadLost(int id, string reason)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_MarkLeadLost", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                cmd.Parameters.Add(new SqlParameter("@LostReason", (object)reason ?? DBNull.Value));
                var updatedRowsParam = new SqlParameter("@UpdatedRows", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(updatedRowsParam);
                conn.Open();
                cmd.ExecuteNonQuery();
                return updatedRowsParam.Value != DBNull.Value && Convert.ToInt32(updatedRowsParam.Value) > 0;
            }
        }

        // New-stage "Lost"/"Junk" buttons - single UPDATE on SA_MaskEmail,
        // no promotion into SA_MaskEmail_Open (that only happens for
        // "Open" - a separate, not-yet-built action pending confirmation
        // of SA_MaskEmail_Open's full insertable column list).
        public bool MarkLeadNewStatus(int id, string status)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_MarkLeadNewStatus", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                cmd.Parameters.Add(new SqlParameter("@Status", status));
                var updatedRowsParam = new SqlParameter("@UpdatedRows", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(updatedRowsParam);
                conn.Open();
                cmd.ExecuteNonQuery();
                return updatedRowsParam.Value != DBNull.Value && Convert.ToInt32(updatedRowsParam.Value) > 0;
            }
        }

        // "Log follow-up" action button -- updates the next follow-up date.
        public bool LogFollowup(int id, DateTime followUpDate)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_LogFollowup", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                cmd.Parameters.Add(new SqlParameter("@FollowUpDate", followUpDate));
                var updatedRowsParam = new SqlParameter("@UpdatedRows", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(updatedRowsParam);
                conn.Open();
                cmd.ExecuteNonQuery();
                return updatedRowsParam.Value != DBNull.Value && Convert.ToInt32(updatedRowsParam.Value) > 0;
            }
        }

        // "Activity & follow-ups" tab -- same StartRowNumber/EndRowNumber +
        // RowNumber/RecordsCount convention as
        // usp_GetAllOpenLeadListManager_CustomSearch / dlLeadManagementSubscription:
        // the proc alone decides total rows, page count and which slice of
        // rows to return. The C# layer and the browser never compute a page
        // count themselves -- they only relay what the proc already worked out.
        public List<enSalesActivityRow> GetLeadActivity(int id, string search, int startRowNumber, int endRowNumber)
        {
            var list = new List<enSalesActivityRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetLeadActivity", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                cmd.Parameters.Add(new SqlParameter("@searchKeyword", search ?? ""));
                cmd.Parameters.Add(new SqlParameter("@StartRowNumber", startRowNumber));
                cmd.Parameters.Add(new SqlParameter("@EndRowNumber", endRowNumber));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesActivityRow
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Discussion = rdr["Discussion"] == DBNull.Value ? "" : rdr["Discussion"].ToString(),
                            CreatedBy = rdr["CreatedBy"] == DBNull.Value ? "" : rdr["CreatedBy"].ToString(),
                            CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                            DiscussionStatus = rdr["DiscussionStatus"] == DBNull.Value ? "" : rdr["DiscussionStatus"].ToString(),
                            FollowupType = rdr["FollowupType"] == DBNull.Value ? "" : rdr["FollowupType"].ToString(),
                            SetFollowUp = HasColumn(rdr, "Set_Follow_up") && rdr["Set_Follow_up"] != DBNull.Value ? Convert.ToDateTime(rdr["Set_Follow_up"]) : (DateTime?)null,
                            RowNumber = Convert.ToInt32(rdr["RowNumber"]),
                            RecordsCount = Convert.ToInt32(rdr["RecordsCount"])
                        });
                    }
                }
            }

            return list;
        }

        // "Log follow-up" modal Save -- inserts an activity row and, per the
        // "Move to stage" dropdown / "Add to My Day" checkbox, updates the
        // lead's own Status / Set_Follow_up too.
        // Proc now validates LeadID/Discussion/Stage/FollowupType itself and
        // reports back via @Success/@Message -- this method only relays that,
        // it doesn't decide validity here.
        public bool LogFollowupActivity(int id, string followupType, string discussion, string stage, DateTime? nextFollowUpDate, bool addToMyDay, string createdBy, out string message)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_LogFollowupActivity", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LeadID", id));
                cmd.Parameters.Add(new SqlParameter("@FollowupType", (object)followupType ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Discussion", (object)discussion ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Stage", (object)stage ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NextFollowUpDate", (object)nextFollowUpDate ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@AddToMyDay", addToMyDay));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                // Proc's real signature only outputs @UpdatedRows (the INSERT's
                // @@ROWCOUNT) - there's no @Success/@Message pair server-side,
                // which is what "too many arguments specified" was complaining
                // about. Success/message are derived here instead.
                var updatedRowsParam = new SqlParameter("@UpdatedRows", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(updatedRowsParam);
                conn.Open();
                cmd.ExecuteNonQuery();

                int updatedRows = updatedRowsParam.Value == DBNull.Value ? 0 : Convert.ToInt32(updatedRowsParam.Value);
                bool success = updatedRows > 0;
                message = success ? "Follow-up logged." : "Could not log follow-up - lead not found.";
                return success;
            }
        }

        // "Schedule demo" modal - the table (SA_MaskEmail_Open_DemoDetails)
        // turned out to already exist, with its own real (lowercase)
        // columns: maskemailopenid, lastdemodate, demodescussion, createdby,
        // createddate. No column exists for demo time or "send calendar
        // invite" - those stay UI-only for now, not persisted.
        public bool SaveContractSign(int id, DateTime? startDate, DateTime? endDate, string createdBy, out string message)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_SaveContractSign", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LeadID", id));
                cmd.Parameters.Add(new SqlParameter("@StartDate", (object)startDate ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@EndDate", (object)endDate ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                var updatedRowsParam = new SqlParameter("@UpdatedRows", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(updatedRowsParam);
                conn.Open();
                cmd.ExecuteNonQuery();

                int updatedRows = updatedRowsParam.Value == DBNull.Value ? 0 : Convert.ToInt32(updatedRowsParam.Value);
                message = updatedRows > 0 ? "Contract sign saved." : "Could not save contract sign dates.";
                return updatedRows > 0;
            }
        }

        public List<enContractSign> GetContractSignByLead(int leadId)
        {
            var list = new List<enContractSign>();
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetContractSignByLead", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LeadID", leadId));
                conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enContractSign
                        {
                            StartDate = rdr["StartDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["StartDate"]),
                            EndDate = rdr["EndDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["EndDate"]),
                            CreatedBy = rdr["CreatedBy"] == DBNull.Value ? "" : rdr["CreatedBy"].ToString(),
                            CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"])
                        });
                    }
                }
            }
            return list;
        }

        public bool ScheduleDemo(int id, DateTime? demoDate, string productsToDemo, string createdBy, out string message)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_ScheduleDemo", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LeadID", id));
                cmd.Parameters.Add(new SqlParameter("@DemoDate", (object)demoDate ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DemoDescription", (object)productsToDemo ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                var updatedRowsParam = new SqlParameter("@UpdatedRows", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(updatedRowsParam);
                conn.Open();
                cmd.ExecuteNonQuery();

                int updatedRows = updatedRowsParam.Value == DBNull.Value ? 0 : Convert.ToInt32(updatedRowsParam.Value);
                bool success = updatedRows > 0;
                message = success ? "Demo scheduled." : "Could not schedule demo - lead not found.";
                return success;
            }
        }

        // "Move to stage" dropdown options -- the real distinct Status
        // values already in use, rather than a guessed hardcoded list.
        public List<string> GetStageOptions()
        {
            var list = new List<string>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetStageOptions", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        if (rdr["Status"] != DBNull.Value)
                            list.Add(rdr["Status"].ToString());
                    }
                }
            }

            return list;
        }

        // "Contacts" tab -- primary contact (SA_MaskEmail_Open) unioned with
        // additional contacts (SA_MaskEmail_OpenContact), IsPrimary flag and
        // ordering all decided by the proc.
        public List<enSalesLeadContactRow> GetLeadContacts(int leadId)
        {
            var list = new List<enSalesLeadContactRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetLeadContacts", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LeadID", leadId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesLeadContactRow
                        {
                            IsPrimary = rdr["IsPrimary"] != DBNull.Value && Convert.ToInt32(rdr["IsPrimary"]) == 1,
                            Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                            CorpEmail = rdr["CorpEmail"] == DBNull.Value ? "" : rdr["CorpEmail"].ToString(),
                            ContactCountryCode = rdr["ContactCountryCode"] == DBNull.Value ? "" : rdr["ContactCountryCode"].ToString(),
                            ContactNumber = rdr["ContactNumber"] == DBNull.Value ? "" : rdr["ContactNumber"].ToString(),
                            Designation = rdr["Designation"] == DBNull.Value ? "" : rdr["Designation"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // "Add contact" drawer Save -- the proc itself validates Name/
        // CorpEmail are not null/empty before inserting; this just relays
        // whatever it decided via the output params.
        public bool AddLeadContact(int leadId, string name, string corpEmail, string contactCountryCode, string contactNumber, string designation, string createdBy, out string message)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_AddLeadContact", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LeadID", leadId));
                cmd.Parameters.Add(new SqlParameter("@Name", (object)name ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CorpEmail", (object)corpEmail ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ContactCountryCode", (object)contactCountryCode ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ContactNumber", (object)contactNumber ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Designation", (object)designation ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                var successParam = new SqlParameter("@Success", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(successParam);
                cmd.Parameters.Add(messageParam);
                conn.Open();
                cmd.ExecuteNonQuery();

                message = messageParam.Value == DBNull.Value ? "" : messageParam.Value.ToString();
                return successParam.Value != DBNull.Value && Convert.ToBoolean(successParam.Value);
            }
        }

        // ------------------------------------------------------------
        // "Pipeline" board -- New/Contacted/Demo/Won/Lost lists, each
        // top 30, each its own proc (New reads from SA_MaskEmail, which
        // has fewer columns than SA_MaskEmail_Open -- so every column
        // read below is defensive/optional rather than assumed present).
        // ------------------------------------------------------------
        private static bool HasColumn(SqlDataReader rdr, string name)
        {
            for (int i = 0; i < rdr.FieldCount; i++)
            {
                if (string.Equals(rdr.GetName(i), name, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private static enSalesPipelineCardRow ReadPipelineCardRow(SqlDataReader rdr)
        {
            return new enSalesPipelineCardRow
            {
                ID = HasColumn(rdr, "ID") && rdr["ID"] != DBNull.Value ? Convert.ToInt32(rdr["ID"]) : 0,
                Name = HasColumn(rdr, "Name") && rdr["Name"] != DBNull.Value ? rdr["Name"].ToString() : "",
                CompanyName = HasColumn(rdr, "CompanyName") && rdr["CompanyName"] != DBNull.Value ? rdr["CompanyName"].ToString() : "",
                CorpEmail = HasColumn(rdr, "CorpEmail") && rdr["CorpEmail"] != DBNull.Value ? rdr["CorpEmail"].ToString() : "",
                CountryName = HasColumn(rdr, "CountryName") && rdr["CountryName"] != DBNull.Value ? rdr["CountryName"].ToString() : "",
                Status = HasColumn(rdr, "Status") && rdr["Status"] != DBNull.Value ? rdr["Status"].ToString() : "",
                LeadType = HasColumn(rdr, "LeadType") && rdr["LeadType"] != DBNull.Value ? rdr["LeadType"].ToString() : "",
                Source = HasColumn(rdr, "Source") && rdr["Source"] != DBNull.Value ? rdr["Source"].ToString() : "",
                Amount = HasColumn(rdr, "Amount") && rdr["Amount"] != DBNull.Value ? Convert.ToDecimal(rdr["Amount"]) : (decimal?)null,
                Currency = HasColumn(rdr, "Currency") && rdr["Currency"] != DBNull.Value ? rdr["Currency"].ToString() : "",
                SetFollowUp = HasColumn(rdr, "Set_Follow_up") && rdr["Set_Follow_up"] != DBNull.Value ? Convert.ToDateTime(rdr["Set_Follow_up"]) : (DateTime?)null,
                CreatedDate = HasColumn(rdr, "CreatedDate") && rdr["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(rdr["CreatedDate"]) : (DateTime?)null
            };
        }

        private List<enSalesPipelineCardRow> RunPipelineProc(string procName, string createdBy)
        {
            var list = new List<enSalesPipelineCardRow>();

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
                        list.Add(ReadPipelineCardRow(rdr));
                    }
                }
            }

            return list;
        }

        public List<enSalesPipelineCardRow> GetPipelineNew(string createdBy)
        {
            return RunPipelineProc("usp_SalesNew_GetPipelineNew", createdBy);
        }

        public List<enSalesPipelineCardRow> GetPipelineContacted(string createdBy)
        {
            // Latest-opened lead first: SA_MaskEmail_Open rows get their
            // identity ID at open-time, so the highest ID is the most
            // recently opened lead -> sort ID descending for the Open column.
            var list = RunPipelineProc("usp_SalesNew_GetPipelineContacted", createdBy);
            list.Sort(delegate (enSalesPipelineCardRow a, enSalesPipelineCardRow b)
            {
                return b.ID.CompareTo(a.ID);
            });
            return list;
        }

        public List<enSalesPipelineCardRow> GetPipelineDemo(string createdBy)
        {
            return RunPipelineProc("usp_SalesNew_GetPipelineDemo", createdBy);
        }

        public List<enSalesPipelineCardRow> GetPipelineWon(string createdBy)
        {
            return RunPipelineProc("usp_SalesNew_GetPipelineWon", createdBy);
        }

        public List<enSalesPipelineCardRow> GetPipelineLost(string createdBy)
        {
            return RunPipelineProc("usp_SalesNew_GetPipelineLost", createdBy);
        }

        // "List" tab -- combined New + everything-else, backend-driven
        // paging, same StartRowNumber/EndRowNumber + RowNumber/RecordsCount
        // contract as GetLeadActivity.
        //
        // "Junk" is a special case: per MarkLeadNewStatus above, marking a
        // New-stage lead Junk is a single UPDATE straight on SA_MaskEmail
        // (Status = 'Junk') -- it's never promoted into SA_MaskEmail_Open,
        // so usp_SalesNew_GetPipelineList (which only knows about "genuinely
        // new" SA_MaskEmail rows + everything in SA_MaskEmail_Open) can't see
        // it. Routed to a dedicated proc instead of guessing at a rewrite of
        // that existing, working proc.
        public List<enSalesPipelineListRow> GetPipelineList(string createdBy, string stage, string leadType, string source, string search, DateTime? fromDate, DateTime? toDate, int startRowNumber, int endRowNumber)
        {
            if (string.Equals(stage, "Junk", StringComparison.OrdinalIgnoreCase))
            {
                return GetPipelineListJunk(createdBy, leadType, source, search, startRowNumber, endRowNumber);
            }

            var list = new List<enSalesPipelineListRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetPipelineList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@Stage", (object)stage ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@LeadType", (object)leadType ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Source", (object)source ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@searchKeyword", (object)search ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@FromDate", (object)fromDate ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ToDate", (object)toDate ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@StartRowNumber", startRowNumber));
                cmd.Parameters.Add(new SqlParameter("@EndRowNumber", endRowNumber));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesPipelineListRow
                        {
                            ID = rdr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ID"]),
                            Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            CorpEmail = rdr["CorpEmail"] == DBNull.Value ? "" : rdr["CorpEmail"].ToString(),
                            CountryName = rdr["CountryName"] == DBNull.Value ? "" : rdr["CountryName"].ToString(),
                            Source = rdr["Source"] == DBNull.Value ? "" : rdr["Source"].ToString(),
                            LeadType = rdr["LeadType"] == DBNull.Value ? "" : rdr["LeadType"].ToString(),
                            Status = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                            Amount = rdr["Amount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(rdr["Amount"]),
                            Currency = rdr["Currency"] == DBNull.Value ? "" : rdr["Currency"].ToString(),
                            ExpectedCloseDate = rdr["Expected_Close_Date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["Expected_Close_Date"]),
                            SetFollowUp = rdr["Set_Follow_up"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["Set_Follow_up"]),
                            CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                            RowNumber = Convert.ToInt32(rdr["RowNumber"]),
                            RecordsCount = Convert.ToInt32(rdr["RecordsCount"])
                        });
                    }
                }
            }

            return list;
        }

        // Junk-stage rows for the "List" tab -- straight off SA_MaskEmail
        // (Status = 'Junk'), see the comment on GetPipelineList above for
        // why this is a separate proc rather than a branch inside
        // usp_SalesNew_GetPipelineList. SA_MaskEmail has no Amount/Currency/
        // Expected_Close_Date/Set_Follow_up columns, so the proc returns
        // those as NULL and the reader below just carries the nulls through.
        private List<enSalesPipelineListRow> GetPipelineListJunk(string createdBy, string leadType, string source, string search, int startRowNumber, int endRowNumber)
        {
            var list = new List<enSalesPipelineListRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetPipelineListJunk", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@LeadType", (object)leadType ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Source", (object)source ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@searchKeyword", (object)search ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@StartRowNumber", startRowNumber));
                cmd.Parameters.Add(new SqlParameter("@EndRowNumber", endRowNumber));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesPipelineListRow
                        {
                            ID = rdr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ID"]),
                            Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            CorpEmail = rdr["CorpEmail"] == DBNull.Value ? "" : rdr["CorpEmail"].ToString(),
                            CountryName = rdr["CountryName"] == DBNull.Value ? "" : rdr["CountryName"].ToString(),
                            Source = rdr["Source"] == DBNull.Value ? "" : rdr["Source"].ToString(),
                            LeadType = rdr["LeadType"] == DBNull.Value ? "" : rdr["LeadType"].ToString(),
                            Status = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                            Amount = rdr["Amount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(rdr["Amount"]),
                            Currency = rdr["Currency"] == DBNull.Value ? "" : rdr["Currency"].ToString(),
                            ExpectedCloseDate = rdr["Expected_Close_Date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["Expected_Close_Date"]),
                            SetFollowUp = rdr["Set_Follow_up"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["Set_Follow_up"]),
                            CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                            RowNumber = Convert.ToInt32(rdr["RowNumber"]),
                            RecordsCount = Convert.ToInt32(rdr["RecordsCount"])
                        });
                    }
                }
            }

            return list;
        }

        // "Lost-reason analysis" report -- same StartRowNumber/EndRowNumber +
        // RowNumber/RecordsCount contract as GetLeadActivity/GetPipelineList.
        // "New subscription" popup (Sales role only - SalesManagement/
        // SalesIndex.cshtml) - Won leads for the logged-in sales rep that
        // don't have a subscription/company yet (CompanyName not already in
        // SA_CompanyMaster). Unpaged, same convention as RunPipelineProc.
        public List<enSalesWonLeadRow> GetWonLeadsWithoutSubscription(string createdBy)
        {
            var list = new List<enSalesWonLeadRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetWonLeadsWithoutSubscription", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesWonLeadRow
                        {
                            ID = rdr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ID"]),
                            Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            CorpEmail = rdr["CorpEmail"] == DBNull.Value ? "" : rdr["CorpEmail"].ToString(),
                            Amount = rdr["Amount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(rdr["Amount"]),
                            Currency = rdr["Currency"] == DBNull.Value ? "" : rdr["Currency"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // "Orders & invoices" -> Sales Order Forms table (Invoice/
        // ChemAnalystProformaInvoiceList.cshtml) -- SA_MaskEmail_Open leads
        // with an uploaded Sales Order Form attachment, scoped to the
        // logged-in sales rep. Same paging convention as GetLostReasonReport.
        public List<enSalesOrderFormRow> GetSalesOrderFormList(string createdBy, string search, int startRowNumber, int endRowNumber)
        {
            var list = new List<enSalesOrderFormRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetSalesOrderFormList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@searchKeyword", search ?? ""));
                cmd.Parameters.Add(new SqlParameter("@StartRowNumber", startRowNumber));
                cmd.Parameters.Add(new SqlParameter("@EndRowNumber", endRowNumber));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesOrderFormRow
                        {
                            ID = rdr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ID"]),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            Signatory = rdr["Signatory"] == DBNull.Value ? "" : rdr["Signatory"].ToString(),
                            Amount = rdr["Amount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(rdr["Amount"]),
                            PaymentOption = rdr["PaymentOption"] == DBNull.Value ? "" : rdr["PaymentOption"].ToString(),
                            Attechments = rdr["Attechments"] == DBNull.Value ? "" : rdr["Attechments"].ToString(),
                            RowNumber = Convert.ToInt32(rdr["RowNumber"]),
                            RecordsCount = Convert.ToInt32(rdr["RecordsCount"])
                        });
                    }
                }
            }

            return list;
        }

        public List<enSalesLostReasonRow> GetLostReasonReport(string createdBy, string search, int startRowNumber, int endRowNumber)
        {
            var list = new List<enSalesLostReasonRow>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesNew_GetLostReasonReport", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy ?? ""));
                cmd.Parameters.Add(new SqlParameter("@searchKeyword", search ?? ""));
                cmd.Parameters.Add(new SqlParameter("@StartRowNumber", startRowNumber));
                cmd.Parameters.Add(new SqlParameter("@EndRowNumber", endRowNumber));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesLostReasonRow
                        {
                            Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                            CorpEmail = rdr["CorpEmail"] == DBNull.Value ? "" : rdr["CorpEmail"].ToString(),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            LostReason = rdr["LostReason"] == DBNull.Value ? "" : rdr["LostReason"].ToString(),
                            CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                            RowNumber = Convert.ToInt32(rdr["RowNumber"]),
                            RecordsCount = Convert.ToInt32(rdr["RecordsCount"])
                        });
                    }
                }
            }

            return list;
        }
    }

}
