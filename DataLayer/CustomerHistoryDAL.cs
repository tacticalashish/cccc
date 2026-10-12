using EntityLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DataLayer
{
    // Same shape as InvoicingSetupDAL/CustomerDocumentDAL — plain ADO.NET
    // calling a stored procedure against ChemAnalystContext.
    public class CustomerHistoryDAL
    {
     
        public CustomerHistorySummary GetCustomerHistorySummary(int companyId)
        {
            var result = new CustomerHistorySummary();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("USP_GetCustomerHistorySummary", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        result.CustomerSince = rdr["CustomerSince"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CustomerSince"]);
                        result.Renewals = rdr["Renewals"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["Renewals"]);
                    }
                }
            }

            // No source for lifetime value yet — always null.
            result.LifetimeValue = null;

            return result;
        }

        public List<SubscriptionHistoryItem> GetSubscriptionHistoryByCompany(int companyId)
        {
            var list = new List<SubscriptionHistoryItem>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("USP_GetSubscriptionHistoryByCompany", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new SubscriptionHistoryItem
                        {
                            FromDate = rdr["FromDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["FromDate"]),
                            ToDate = rdr["ToDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["ToDate"]),
                            ProductsSubscribed = rdr["ProductsSubscribed"] == DBNull.Value ? "" : rdr["ProductsSubscribed"].ToString(),
                            Users = rdr["Users"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["Users"]),
                            Status = rdr["Status"] == DBNull.Value ? "" : rdr["Status"].ToString()
                        });
                    }
                }
            }

            return list;
        }
    }
}
