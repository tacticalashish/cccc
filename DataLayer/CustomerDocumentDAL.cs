using EntityLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DataLayer
{
    // Same shape as InvoicingSetupDAL / LeadDAL.GetCompanies() — plain ADO.NET
    // calling a stored procedure against ChemAnalystContext.
    public class CustomerDocumentDAL
    {
       
        public List<CustomerDocument> GetCustomerDocumentsByCompany(int companyId)
        {
            var list = new List<CustomerDocument>();

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("USP_GetCustomerDocumentsByCompany", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new CustomerDocument
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            CompanyId = Convert.ToInt32(rdr["CompanyId"]),
                            DocumentType = rdr["DocumentType"] == DBNull.Value ? "" : rdr["DocumentType"].ToString(),
                            ReferenceNote = rdr["ReferenceNote"] == DBNull.Value ? "" : rdr["ReferenceNote"].ToString(),
                            FileName = rdr["FileName"] == DBNull.Value ? "" : rdr["FileName"].ToString(),
                            UploadedDate = rdr["UploadedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["UploadedDate"]),
                            UploadedBy = rdr["UploadedBy"] == DBNull.Value ? "" : rdr["UploadedBy"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        public void InsertCustomerDocument(CustomerDocument model)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("USP_InsertCustomerDocument", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", model.CompanyId));
                cmd.Parameters.Add(new SqlParameter("@DocumentType", (object)model.DocumentType ?? ""));
                cmd.Parameters.Add(new SqlParameter("@ReferenceNote", (object)model.ReferenceNote ?? ""));
                cmd.Parameters.Add(new SqlParameter("@FileName", (object)model.FileName ?? ""));
                cmd.Parameters.Add(new SqlParameter("@UploadedBy", (object)model.UploadedBy ?? ""));

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
