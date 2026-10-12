using EntityLayer;
using NLog.Internal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DataLayer
{
    // DAL for SalesManagementController — plain ADO.NET calling stored
    // procedures against ChemAnalystContext, same convention as
    // InvoicingSetupDAL / CustomerDocumentDAL / CustomerHistoryDAL.
    // Methods get added here as each Sales Management feature is defined.
    public class dlSalesManagement
    {
        // Safe column-presence check so reading an optional column (e.g. StepCounter)
        // doesn't throw if a proc hasn't been updated to return it yet.
        private static bool HasColumn(IDataRecord rdr, string columnName)
        {
            for (int i = 0; i < rdr.FieldCount; i++)
            {
                if (string.Equals(rdr.GetName(i), columnName, System.StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }


        // Company dropdown for the "New Subscription" > Primary contact &
        // company step. SA_Companymaster isn't mapped in the EF model, so
        // this is a direct parameterless SELECT, same shape as the other
        // ADO.NET DALs above.
        public List<enSelectItem> GetCompanyList()
        {
            var list = new List<enSelectItem>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetCompanyList", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSelectItem
                        {
                            Value = rdr["ID"].ToString(),
                            Text = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // Drives the autofill on the New Subscription form: once a company
        // is picked, its primary contact name/email/phone (and designation)
        // get pulled from SA_Companymaster and pushed back into the form.
        public enSalesManagement GetCompanyDetailsById(int companyId)
        {
            enSalesManagement result = null;

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetCompanyDetailsById", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        result = new enSalesManagement
                        {
                            CompanyId = Convert.ToInt32(rdr["ID"]),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            PrimaryContactName = rdr["FirstContactName"] == DBNull.Value ? "" : rdr["FirstContactName"].ToString(),
                            PrimaryEmail = rdr["FirstContactEmail"] == DBNull.Value ? "" : rdr["FirstContactEmail"].ToString(),
                            Phone = rdr["FirstContactNumber"] == DBNull.Value ? "" : rdr["FirstContactNumber"].ToString(),
                            Designation = rdr["Designation"] == DBNull.Value ? "" : rdr["Designation"].ToString()
                        };
                    }
                }
            }

            return result;
        }

        // Step 2 (Package) product checklist — distinct products that have
        // an active chemical-pricing entry, per the query supplied for this
        // page: SA_Product joined to SA_ChemPriceMonthly on Product = id,
        // filtered to status = 1.
        public List<enSelectItem> GetProductList()
        {
            var list = new List<enSelectItem>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetProductList", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSelectItem
                        {
                            Value = rdr["id"].ToString(),
                            Text = rdr["ProductName"] == DBNull.Value ? "" : rdr["ProductName"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // Country dropdown for the Primary contact & company step.
        public List<enSelectItem> GetCountryList()
        {
            var list = new List<enSelectItem>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetCountryList", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSelectItem
                        {
                            Value = rdr["id"].ToString(),
                            Text = rdr["CountryName"] == DBNull.Value ? "" : rdr["CountryName"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // Secondary client emails dropdown — every non-primary, active
        // contact recorded against the selected company. Mirrors the query
        // supplied for this page: SA_Customer joined to SA_CompanyMaster on
        // CompanyPrimaryId, filtered to the chosen company, active status,
        // and excluding the primary contact (ParentPrimaryId != 1).
        public List<enSelectItem> GetSecondaryClientEmailsByCompanyId(int companyId)
        {
            var list = new List<enSelectItem>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetSecondaryClientEmailsByCompanyId", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSelectItem
                        {
                            Value = rdr["id"].ToString(),
                            Text = rdr["Email"] == DBNull.Value ? "" : rdr["Email"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // Step 3 (Coverage) — countries tracked for one specific product,
        // used for the à la carte per-product country picker. Per the query
        // supplied for this page: SA_ChemPriceMonthly joined to SA_Product
        // and SA_Country, filtered to the given product.
        public List<enSelectItem> GetCountriesByProduct(int productId)
        {
            var list = new List<enSelectItem>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetCountriesByProduct", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductId", productId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSelectItem
                        {
                            Value = rdr["id"].ToString(),
                            Text = rdr["Country"] == DBNull.Value ? "" : rdr["Country"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // Package step — Market tab product checklist. Per the query
        // supplied for this page: SA_MarketbyComp joined to SA_Product.
        public List<enSelectItem> GetMarketProductList()
        {
            var list = new List<enSelectItem>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetMarketProductList", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSelectItem
                        {
                            Value = rdr["id"].ToString(),
                            Text = rdr["ProductName"] == DBNull.Value ? "" : rdr["ProductName"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // Package step — News tab product checklist. Per the query supplied
        // for this page: SA_News joined to SA_Product via SA_NewsAndProductRelation.
        public List<enSelectItem> GetNewsProductList()
        {
            var list = new List<enSelectItem>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetNewsProductList", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSelectItem
                        {
                            Value = rdr["id"].ToString(),
                            Text = rdr["ProductName"] == DBNull.Value ? "" : rdr["ProductName"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // Package step — Capacity Tracker tab product checklist. Per the
        // query supplied for this page: SA_MaintenanceAndShutdown joined to
        // SA_Product, filtered to Year >= current year - 1.
        public List<enSelectItem> GetCapacityTrackerProductList()
        {
            var list = new List<enSelectItem>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetCapacityTrackerProductList", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSelectItem
                        {
                            Value = rdr["id"].ToString(),
                            Text = rdr["ProductName"] == DBNull.Value ? "" : rdr["ProductName"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // Step 3 (Coverage) — Market tab, countries + regions. Both were two
        // separate 24-table UNION queries filtered by the same @ProductId
        // and are always read together (GetMarketCoverageByProduct in the
        // controller calls both back-to-back for one product), so they're
        // now one stored proc returning two result sets instead of two
        // separate round trips.
        public void GetMarketCoverageByProduct(int productId, out List<enSelectItem> countries, out List<enSelectItem> regions)
        {
            countries = new List<enSelectItem>();
            regions = new List<enSelectItem>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetMarketCoverageByProduct", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductId", productId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        countries.Add(new enSelectItem
                        {
                            Value = rdr["id"].ToString(),
                            Text = rdr["Country"] == DBNull.Value ? "" : rdr["Country"].ToString()
                        });
                    }

                    if (rdr.NextResult())
                    {
                        while (rdr.Read())
                        {
                            regions.Add(new enSelectItem
                            {
                                Value = rdr["id"].ToString(),
                                Text = rdr["Region"] == DBNull.Value ? "" : rdr["Region"].ToString()
                            });
                        }
                    }
                }
            }
        }

        // Batched version of GetCountriesByProduct + GetMarketCoverageByProduct
        // for the "Full package" Coverage step, which otherwise fires 2
        // requests per product (1000s of round trips at full-catalog scale).
        // One call, one stored proc, three result sets — grouped by
        // ProductId so the caller can drop the results straight into the
        // same per-product caches the existing single-product endpoints
        // already populate (no downstream code needs to change).
        public void GetBulkCoverageByProducts(string productIdsCsv,
            out Dictionary<string, List<enSelectItem>> chemicalByProduct,
            out Dictionary<string, List<enSelectItem>> marketCountriesByProduct,
            out Dictionary<string, List<enSelectItem>> marketRegionsByProduct)
        {
            chemicalByProduct = new Dictionary<string, List<enSelectItem>>();
            marketCountriesByProduct = new Dictionary<string, List<enSelectItem>>();
            marketRegionsByProduct = new Dictionary<string, List<enSelectItem>>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetBulkCoverageByProducts", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 180;
                cmd.Parameters.Add(new SqlParameter("@ProductIds", productIdsCsv));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        string pid = rdr["ProductId"].ToString();
                        if (!chemicalByProduct.ContainsKey(pid))
                        {
                            chemicalByProduct[pid] = new List<enSelectItem>();
                        }
                        chemicalByProduct[pid].Add(new enSelectItem
                        {
                            Value = rdr["CountryId"].ToString(),
                            Text = rdr["CountryName"] == DBNull.Value ? "" : rdr["CountryName"].ToString()
                        });
                    }

                    if (rdr.NextResult())
                    {
                        while (rdr.Read())
                        {
                            string pid = rdr["ProductId"].ToString();
                            if (!marketCountriesByProduct.ContainsKey(pid))
                            {
                                marketCountriesByProduct[pid] = new List<enSelectItem>();
                            }
                            marketCountriesByProduct[pid].Add(new enSelectItem
                            {
                                Value = rdr["CountryId"].ToString(),
                                Text = rdr["CountryName"] == DBNull.Value ? "" : rdr["CountryName"].ToString()
                            });
                        }
                    }

                    if (rdr.NextResult())
                    {
                        while (rdr.Read())
                        {
                            string pid = rdr["ProductId"].ToString();
                            if (!marketRegionsByProduct.ContainsKey(pid))
                            {
                                marketRegionsByProduct[pid] = new List<enSelectItem>();
                            }
                            marketRegionsByProduct[pid].Add(new enSelectItem
                            {
                                Value = rdr["RegionId"].ToString(),
                                Text = rdr["RegionName"] == DBNull.Value ? "" : rdr["RegionName"].ToString()
                            });
                        }
                    }
                }
            }
        }

        // Subscriptions list — one row per "Transfer to CSM" submission
        // (SA_SubscriptionRequest), independent of however many Lead_Master
        // rows that submission fanned out into. Package/Coverage summaries
        // are computed client-side at save time (the wizard already knows
        // exactly what was picked) and stored as plain text here, rather
        // than re-derived later from Lead_Master.
        public int CreateSubscriptionRequest(enSalesManagement req)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_CreateSubscriptionRequest", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", req.CompanyId));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", (object)req.CompanyName ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PrimaryContactName", (object)req.PrimaryContactName ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PackageType", (object)req.PackageType ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PackageSummary", (object)req.PackageSummary ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CoverageSummary", (object)req.CoverageSummary ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CSM", (object)req.CSM ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", (object)req.CreatedBy ?? DBNull.Value));
                conn.Open();

                var result = cmd.ExecuteScalar();
                return result == null ? 0 : Convert.ToInt32(result);
            }
        }

        // List + total count used to always run as two separate queries from
        // the same caller (GetSubscriptionRequests controller action) - now
        // one proc, with RecordsCount added as a window-function column on
        // the same paged result set instead of a second COUNT(*) round trip
        // (same RecordsCount convention used for backend paging elsewhere).
        public List<enSalesManagement> GetSubscriptionRequests(string search, int pageNumber, int pageSize, out int totalCount)
        {
            var list = new List<enSalesManagement>();
            search = search ?? "";
            totalCount = 0;

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetSubscriptionRequests", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Search", search));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesManagement
                        {
                            RequestId = Convert.ToInt32(rdr["Id"]),
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            PrimaryContactName = rdr["PrimaryContactName"] == DBNull.Value ? "" : rdr["PrimaryContactName"].ToString(),
                            PackageSummary = rdr["PackageSummary"] == DBNull.Value ? "" : rdr["PackageSummary"].ToString(),
                            CoverageSummary = rdr["CoverageSummary"] == DBNull.Value ? "" : rdr["CoverageSummary"].ToString(),
                            CSM = rdr["CSM"] == DBNull.Value ? "" : rdr["CSM"].ToString(),
                            StepCounter = rdr["StepCounter"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["StepCounter"]),
                            StatusRemark = rdr["StatusRemark"] == DBNull.Value ? "" : rdr["StatusRemark"].ToString(),
                            RequestCreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                            RecordsCount = rdr["RecordsCount"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["RecordsCount"])
                        });
                    }
                }
            }

            totalCount = list.Count > 0 ? list[0].RecordsCount : 0;
            return list;
        }

        // Subscription request detail page -- no SA_SubscriptionRequest
        // header table involved (user doesn't have it); the page is keyed
        // straight off CompanyName (Lead_Master.Organisation), per the
        // exact query supplied for this page. Backend-driven search+paging,
        // same RowNumber/RecordsCount convention used everywhere else
        // (see GetSalesIndexSubscriptionRequests).
        public List<enSubscriptionRequestLine> GetSubscriptionRequestLines(string companyName, string search, int startRowNumber, int endRowNumber)
        {
            var lines = new List<enSubscriptionRequestLine>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetSubscriptionRequestDetail", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyName", (object)companyName ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Search", (object)search ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@StartRowNumber", startRowNumber));
                cmd.Parameters.Add(new SqlParameter("@EndRowNumber", endRowNumber));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lines.Add(new enSubscriptionRequestLine
                        {
                            LeadMasterId = rdr["LeadMasterId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["LeadMasterId"]),
                            Company = rdr["Company"] == DBNull.Value ? "" : rdr["Company"].ToString(),
                            PrimaryContact = rdr["Primarycontact"] == DBNull.Value ? "" : rdr["Primarycontact"].ToString(),
                            CountryName = rdr["CountryName"] == DBNull.Value ? "" : rdr["CountryName"].ToString(),
                            RegionName = rdr["RegionName"] == DBNull.Value ? "" : rdr["RegionName"].ToString(),
                            CRM = rdr["CRM"] == DBNull.Value ? "" : rdr["CRM"].ToString(),
                            CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                            SecondaryClintEmail = rdr["SecondaryClintEmail"] == DBNull.Value ? "" : rdr["SecondaryClintEmail"].ToString(),
                            Package = rdr["Package"] == DBNull.Value ? "" : rdr["Package"].ToString(),
                            Products = rdr["Products"] == DBNull.Value ? "" : rdr["Products"].ToString(),
                            SubscriptionDateFrom = rdr["SubscriptionDateFrom"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["SubscriptionDateFrom"]),
                            SubscriptionDateTo = rdr["SubscriptionDateTo"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["SubscriptionDateTo"]),
                            Trader = rdr["Trader"] == DBNull.Value ? "" : rdr["Trader"].ToString(),
                            Renew = rdr["Renew"] == DBNull.Value ? "" : rdr["Renew"].ToString(),
                            Credit = rdr["Credit"] == DBNull.Value ? "" : rdr["Credit"].ToString(),
                            BillingEmail = rdr["BillingEmail"] == DBNull.Value ? "" : rdr["BillingEmail"].ToString(),
                            UserType = rdr["UserType"] == DBNull.Value ? "" : rdr["UserType"].ToString(),
                            Phone = rdr["Phone"] == DBNull.Value ? "" : rdr["Phone"].ToString(),
                            RowNumber = rdr["RowNumber"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["RowNumber"]),
                            RecordsCount = rdr["RecordsCount"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["RecordsCount"])
                        });
                    }
                }
            }

            return lines;
        }

        // Reports — Churn & Lost Renewals. Per the query supplied for this
        // page: SA_MaskEmailOpen_CRM filtered to Lead_status = 'Lost'. There's
        // no "switched to" column anywhere in that table, so that part of the
        // Lost renewals list stays blank rather than being guessed at.
        public List<enSalesManagement> GetLostRenewals(int top)
        {
            var list = new List<enSalesManagement>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetLostRenewals", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Top", top));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesManagement
                        {
                            CompanyName = rdr["CompanyName"] == DBNull.Value ? "" : rdr["CompanyName"].ToString(),
                            LostReason = rdr["LostReason"] == DBNull.Value ? "" : rdr["LostReason"].ToString(),
                            LostArr = rdr["Amount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(rdr["Amount"]),
                            LostCurrency = rdr["Currency"] == DBNull.Value ? "" : rdr["Currency"].ToString(),
                            LostDate = rdr["LostDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["LostDate"])
                        });
                    }
                }
            }

            return list;
        }

        // "Subscriptions" list on SalesIndex.cshtml -- Lead_Master-based
        // approvals workflow, grouped per company/contact/CRM/status/package.
        // Calls usp_SalesIndex_GetSubscriptionRequests (dynamic SQL + temp
        // table + ROW_NUMBER/RecordsCount, same pattern as the existing
        // usp_GetLeads_CustomSearch this was modeled on).
        public List<enSalesManagement> GetSalesIndexSubscriptionRequests(string search, int startRowNumber, int endRowNumber, int userId)
        {
            var list = new List<enSalesManagement>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesIndex_GetSubscriptionRequests", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@searchKeyword", (object)search ?? DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@StartRowNumber", startRowNumber));
                cmd.Parameters.Add(new SqlParameter("@EndRowNumber", endRowNumber));
                cmd.Parameters.Add(new SqlParameter("@userId", userId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesManagement
                        {
                            RequestNumber = rdr["RequestNumber"] == DBNull.Value ? "" : rdr["RequestNumber"].ToString(),
                            CompanyName = rdr["Company"] == DBNull.Value ? "" : rdr["Company"].ToString(),
                            PrimaryContactName = rdr["Primarycontact"] == DBNull.Value ? "" : rdr["Primarycontact"].ToString(),
                            CSM = rdr["CRM"] == DBNull.Value ? "" : rdr["CRM"].ToString(),
                            RequestCreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                            SalesIndexStatus = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                            PackageSummary = rdr["Package"] == DBNull.Value ? "" : rdr["Package"].ToString(),
                            CoverageSummary = rdr["Coverage"] == DBNull.Value ? "" : rdr["Coverage"].ToString(),
                            Renew = rdr["Renew"] == DBNull.Value ? "" : rdr["Renew"].ToString(),
                            RowNumber = rdr["RowNumber"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["RowNumber"]),
                            RecordsCount = rdr["RecordsCount"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["RecordsCount"])
                        });
                    }
                }
            }

            return list;
        }

        // CRM dashboard "New accounts - approval pipeline". Unlike the Sales
        // SalesIndex list (scoped by CreatedBy), this is scoped to the accounts
        // ASSIGNED to this CRM (SA_CRMCustomerRelation.CRMId), latest lead per
        // company, with Renew = 'Renewal' when the company is an existing
        // customer (SalesPackageSubscriptions), else 'New'.
        public List<enSalesManagement> GetCRMApprovalPipeline(int crmId, int top)
        {
            var list = new List<enSalesManagement>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesIndex_GetCRMApprovalPipeline", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CRMId", crmId));
                cmd.Parameters.Add(new SqlParameter("@Top", top));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSalesManagement
                        {
                            RequestNumber = rdr["RequestNumber"] == DBNull.Value ? "" : rdr["RequestNumber"].ToString(),
                            CompanyName = rdr["Company"] == DBNull.Value ? "" : rdr["Company"].ToString(),
                            PrimaryContactName = rdr["Primarycontact"] == DBNull.Value ? "" : rdr["Primarycontact"].ToString(),
                            CSM = rdr["CRM"] == DBNull.Value ? "" : rdr["CRM"].ToString(),
                            RequestCreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                            SalesIndexStatus = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString(),
                            StepCounter = HasColumn(rdr, "StepCounter") && rdr["StepCounter"] != DBNull.Value ? Convert.ToInt32(rdr["StepCounter"]) : 0,
                            PackageSummary = rdr["Package"] == DBNull.Value ? "" : rdr["Package"].ToString(),
                            CoverageSummary = rdr["Coverage"] == DBNull.Value ? "" : rdr["Coverage"].ToString(),
                            Renew = rdr["Renew"] == DBNull.Value ? "" : rdr["Renew"].ToString(),
                            RowNumber = rdr["RowNumber"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["RowNumber"])
                        });
                    }
                }
            }

            return list;
        }

        public List<enSelectItem> GetTopChurnReasons()
        {
            var list = new List<enSelectItem>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetTopChurnReasons", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSelectItem
                        {
                            Text = rdr["Reason"].ToString(),
                            Value = rdr["Cnt"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // Step 4 (Additional users) on CreateSubscription.cshtml, CRM flow
        // only - once a company is picked, this pulls its already-active
        // secondary users so CRM doesn't have to retype people who are
        // already on the account. Query supplied for this page: distinct
        // Fname/Email/Phone/Designation off SA_CompanyMaster joined to
        // SA_Customer joined to SalesPackageSubscriptions, filtered to a
        // top-level company (ParentPrimaryId = 0), active customer rows
        // (Status = 1) with a still-current package (ToDate >= today).
        public List<enSecondaryUser> GetExistingSecondaryUsers(int companyId)
        {
            var list = new List<enSecondaryUser>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetExistingSecondaryUsers", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSecondaryUser
                        {
                            Name = rdr["Name"] == DBNull.Value ? "" : rdr["Name"].ToString(),
                            Email = rdr["Email"] == DBNull.Value ? "" : rdr["Email"].ToString(),
                            Phone = rdr["Phone"] == DBNull.Value ? "" : rdr["Phone"].ToString(),
                            Designation = rdr["Designation"] == DBNull.Value ? "" : rdr["Designation"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // Step 2 (Package) on CreateSubscription.cshtml, CRM flow only -
        // once a company is picked, this pulls the distinct (Product,
        // Module) pairs it already has an active subscription for, so
        // every product-checklist tab (Pricing/Market/News/Capacity
        // Tracker) can auto-check whatever's already on the account. Query
        // supplied for this page: SalesPackageSubscriptions' comma-joined
        // ProductId/MenuId split out and joined to SA_Product/sa_Module,
        // filtered to this company, an Active row, a still-current package
        // (todate >= today), excluding the Deals module.
        public List<enSubscribedProduct> GetSubscribedProductsByCompany(int companyId)
        {
            var list = new List<enSubscribedProduct>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SalesManagement_GetSubscribedProductsByCompany", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new enSubscribedProduct
                        {
                            ProductId = rdr["ProductId"].ToString(),
                            ModuleName = rdr["ModuleName"] == DBNull.Value ? "" : rdr["ModuleName"].ToString()
                        });
                    }
                }
            }

            return list;
        }
    }

    // Existing active secondary user on a company's account - see
    // GetExistingSecondaryUsers above. Kept here rather than in EntityLayer
    // since it's only ever used within this DAL/BL/Controller trio.
    public class enSecondaryUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Designation { get; set; }
    }

    // One (Product, Module) pair a company already has an active
    // subscription for - see GetSubscribedProductsByCompany above. Kept
    // here rather than in EntityLayer, same reasoning as enSecondaryUser.
    public class enSubscribedProduct
    {
        public string ProductId { get; set; }
        public string ModuleName { get; set; }
    }
}
