using EntityLayer;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DataLayer
{
    // Same shape as LeadDAL.GetCompanies() — plain ADO.NET calling a stored
    // procedure against ChemAnalystContext, no EF DbSet involved since
    // SA_Invoicingsetup isn't mapped in the EF model.
    public class InvoicingSetupDAL
    {
      
        public InvoicingSetup GetInvoicingSetupByCompany(int companyId)
        {
            InvoicingSetup result = null;

            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("USP_GetInvoicingSetupByCompany", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        result = new InvoicingSetup
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            CompanyId = Convert.ToInt32(rdr["CompanyId"]),
                            HowToSendInvoice = rdr["HowToSendInvoice"] == DBNull.Value ? "" : rdr["HowToSendInvoice"].ToString(),
                            InvoiceEmail = rdr["InvoiceEmail"] == DBNull.Value ? "" : rdr["InvoiceEmail"].ToString(),
                            VendorRegistrationNo = rdr["VendorRegistrationNo"] == DBNull.Value ? "" : rdr["VendorRegistrationNo"].ToString(),
                            PORequiredBeforeInvoice = rdr["PORequiredBeforeInvoice"] != DBNull.Value && Convert.ToBoolean(rdr["PORequiredBeforeInvoice"]),
                            UploadPortal = rdr["UploadPortal"] == DBNull.Value ? "" : rdr["UploadPortal"].ToString(),
                            HardCopyNeeded = rdr["HardCopyNeeded"] != DBNull.Value && Convert.ToBoolean(rdr["HardCopyNeeded"]),
                            HardCopyAddress = rdr["HardCopyAddress"] == DBNull.Value ? "" : rdr["HardCopyAddress"].ToString(),
                            SpecialInstructions = rdr["SpecialInstructions"] == DBNull.Value ? "" : rdr["SpecialInstructions"].ToString(),
                            CreatedBy = rdr["CreatedBy"] == DBNull.Value ? "" : rdr["CreatedBy"].ToString(),
                            CreatedDate = rdr["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["CreatedDate"]),
                            UpdatedBy = rdr["UpdatedBy"] == DBNull.Value ? "" : rdr["UpdatedBy"].ToString(),
                            UpdatedDate = rdr["UpdatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["UpdatedDate"])
                        };
                    }
                }
            }

            return result;
        }

        public void SaveInvoicingSetup(InvoicingSetup model, string userName)
        {
            using (SqlConnection conn = new SqlConnection(ApplicationSettings.DefaultConnectionString))
            using (SqlCommand cmd = new SqlCommand("USP_SaveInvoicingSetup", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CompanyId", model.CompanyId));
                cmd.Parameters.Add(new SqlParameter("@HowToSendInvoice", (object)model.HowToSendInvoice ?? ""));
                cmd.Parameters.Add(new SqlParameter("@InvoiceEmail", (object)model.InvoiceEmail ?? ""));
                cmd.Parameters.Add(new SqlParameter("@VendorRegistrationNo", (object)model.VendorRegistrationNo ?? ""));
                cmd.Parameters.Add(new SqlParameter("@PORequiredBeforeInvoice", model.PORequiredBeforeInvoice));
                cmd.Parameters.Add(new SqlParameter("@UploadPortal", (object)model.UploadPortal ?? ""));
                cmd.Parameters.Add(new SqlParameter("@HardCopyNeeded", model.HardCopyNeeded));
                cmd.Parameters.Add(new SqlParameter("@HardCopyAddress", (object)model.HardCopyAddress ?? ""));
                cmd.Parameters.Add(new SqlParameter("@SpecialInstructions", (object)model.SpecialInstructions ?? ""));
                cmd.Parameters.Add(new SqlParameter("@UserName", (object)userName ?? ""));

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
