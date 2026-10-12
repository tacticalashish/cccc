using EntityLayer;
using EntityLayer.CustomEntity;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataLayer
{
    public class dlChemAnalystInvoice
    {
        private enChemAnalystInvoice _enInVoice = null;
        public dlChemAnalystInvoice(enChemAnalystInvoice enInVoice_)
        {
            this._enInVoice = enInVoice_;
        }
        public List<enChemAnalystInvoice> InsertInvoiceRecord(enChemAnalystInvoice enObj)
        {
            List<enChemAnalystInvoice> Insertdata = new List<enChemAnalystInvoice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertChemAnalystTaxInvoice]", new object[] {
                                             enObj.InvoiceNo,
                                            enObj.Country ,
                                            enObj.InvoiceDate ,
                                            enObj.ReverseCharge ,
                                            enObj.PO ,
                                            enObj.PODATE,
                                            enObj.KindAttn,
                                            enObj.Name,
                                            enObj.Address,
                                            enObj.VAT,
                                            enObj.PAN,
                                            enObj.State,
                                            enObj.Telephone,
                                            enObj.Email ,
                                            enObj.SacCode ,
                                            enObj.Amount,
                                            enObj.Subtotal,
                                            enObj.CGST,
                                            enObj.SGST ,
                                            enObj.IGST,
                                            //enObj.LutNo,
                                            enObj.TotalTaxAmount,
                                            enObj.TotalAmount ,
                                            enObj.AmountInWords,
                                            enObj.PaymentTerms,
                                            enObj.Discription,
                                            enObj.Createdby,
                                            enObj.SupplyState,
                                            enObj.GSTIN,
                                            enObj.POfiles,

                                      }))
                {
                    while (idr.Read())
                    {
                        enChemAnalystInvoice objenINvoice = new enChemAnalystInvoice();
                        ConstructObjectInsertStatus(idr, objenINvoice);
                        Insertdata.Add(objenINvoice);
                    }
                }

                return Insertdata;
            }
            catch(Exception ex)
            {


            }
            return Insertdata;
        }
        public List<enChemAnalystInvoice> InsertProformaInvoiceRecord(enChemAnalystInvoice enObj)
        {
            List<enChemAnalystInvoice> Insertdata = new List<enChemAnalystInvoice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertChemAnalystProformaInvoice]", new object[] {
                                             enObj.InvoiceNo,
                                            enObj.Country ,
                                            enObj.InvoiceDate,
                                            enObj.ReverseCharge,
                                            enObj.PO,
                                            enObj.PODATE,
                                            enObj.KindAttn,
                                            enObj.Name,
                                            enObj.Address,
                                            enObj.GSTIN,
                                            enObj.PAN,
                                            enObj.State,
                                            enObj.Telephone,
                                            enObj.Email ,
                                            enObj.SacCode ,
                                           enObj.Currency,
                                            enObj.Amount,
                                            enObj.Discount,
                                            enObj.Subtotal,
                                            enObj.CGST,
                                            enObj.SGST ,
                                            enObj.IGST,
                                            //enObj.LutNo,
                                            enObj.TotalTaxAmount,
                                            enObj.TotalAmount ,
                                            enObj.AmountInWords,
                                            enObj.PaymentTerms,
                                            enObj.Discription,
                                            enObj.Createdby,
                                              enObj.POfiles


                                      }))
                {
                    while (idr.Read())
                    {
                        enChemAnalystInvoice objenINvoice = new enChemAnalystInvoice();
                        ConstructObjectInsertStatus(idr, objenINvoice);
                        Insertdata.Add(objenINvoice);
                    }
                }

                return Insertdata;
            }
            catch (Exception ex)
            {


            }
            return Insertdata;
        }
        
       public List<enChemAnalystInvoice> UpdateTaxInvoiceRecord(enChemAnalystInvoice enObj)
        {
            List<enChemAnalystInvoice> Updatedata = new List<enChemAnalystInvoice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateChemAnalystTaxInvoice]", new object[] {
                                            enObj.InvoiceID,
                                            enObj.InvoiceNo,
                                            enObj.Country ,
                                            enObj.InvoiceDate ,
                                            enObj.ReverseCharge ,
                                            enObj.PO ,
                                            enObj.PODATE,
                                            enObj.KindAttn,
                                            enObj.Name,
                                            enObj.Address,
                                            enObj.VAT,
                                            enObj.PAN,
                                            enObj.State,
                                            enObj.Telephone,
                                            enObj.Email ,
                                            enObj.SacCode ,
                                            enObj.Amount,
                                            enObj.Subtotal,
                                            enObj.CGST,
                                            enObj.SGST ,
                                            enObj.IGST,
                                            //enObj.LutNo,
                                            enObj.TotalTaxAmount,
                                            enObj.TotalAmount ,
                                            enObj.AmountInWords,
                                            enObj.PaymentTerms,
                                            enObj.Discription,
                                            enObj.Createdby,
                                            enObj.SupplyState,
                                            enObj.GSTIN,
                                           
                                            
                                      }))
                {
                    while (idr.Read())
                    {
                        enChemAnalystInvoice objenINvoice = new enChemAnalystInvoice();
                        ConstructObjectInsertStatus(idr, objenINvoice);
                        Updatedata.Add(objenINvoice);
                    }
                }

                return Updatedata;
            }
            catch(Exception ex)
            {


            }
            return Updatedata;
        }
       public List<enChemAnalystInvoice> UpdateProformaInvoiceRecord(enChemAnalystInvoice enObj)
        {
            List<enChemAnalystInvoice> Updatedata = new List<enChemAnalystInvoice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_UpdateChemAnalystProformaInvoice]", new object[] {
                                            enObj.InvoiceID,
                                            enObj.InvoiceNo,
                                            enObj.Country ,
                                            enObj.InvoiceDate ,
                                            enObj.ReverseCharge ,
                                            enObj.PO ,
                                            enObj.PODATE,
                                            enObj.KindAttn,
                                            enObj.Name,
                                            enObj.Address,
                                            enObj.GSTIN,
                                            enObj.PAN,
                                            enObj.State,
                                            enObj.Telephone,
                                            enObj.Email ,
                                            enObj.SacCode ,
                                            enObj.Currency,
                                            enObj.Amount,
                                             enObj.Discount,
                                            enObj.Subtotal,
                                            enObj.CGST,
                                            enObj.SGST ,
                                            enObj.IGST,
                                            //enObj.LutNo,
                                            enObj.TotalTaxAmount,
                                            enObj.TotalAmount ,
                                            enObj.AmountInWords,
                                            enObj.PaymentTerms,
                                            enObj.Discription,
                                            enObj.Createdby
                                           


                                      }))
                {
                    while (idr.Read())
                    {
                        enChemAnalystInvoice objenINvoice = new enChemAnalystInvoice();
                        ConstructObjectInsertStatus(idr, objenINvoice);
                        Updatedata.Add(objenINvoice);
                    }
                }

                return Updatedata;
            }
            catch (Exception ex)
            {


            }
            return Updatedata;
        }
        public List<enChemAnalystInvoice> GetChemanalystTaxInvoiceList(enSearchFilter searchFilter)
        {
            List<enChemAnalystInvoice> listOfdatas = new List<enChemAnalystInvoice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetChemAnalystTaxInVoice_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enChemAnalystInvoice objEN = new enChemAnalystInvoice();
                        ConstructObjectListtaxInvoice(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }

       
        
        public List<enChemAnalystInvoice> GetChemanalystProformaInvoiceList(enSearchFilter searchFilter)
        {
            List<enChemAnalystInvoice> listOfdatas = new List<enChemAnalystInvoice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetChemAnalystProformaInVoice_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enChemAnalystInvoice objEN = new enChemAnalystInvoice();
                        ConstructObjectListtaxInvoice(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }

        public List<enChemAnalystInvoice> BindTaxInvoiceDetailsByid(enChemAnalystInvoice en)
        {
            List<enChemAnalystInvoice> listOfdata = new List<enChemAnalystInvoice>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetTaxInvoiceDetailsByid]", new object[] {
                                                                  en.InvoiceID,
                                                                }))

            {
                while (idr.Read())
                {
                    enChemAnalystInvoice objENProduct = new enChemAnalystInvoice();
                    ConstructObjectListtaxInvoice(idr, objENProduct);
                    listOfdata.Add(objENProduct);
                }
            }

            return listOfdata;
        }
        public List<enChemAnalystInvoice> BindProformaInvoiceDetailsByid(enChemAnalystInvoice en)
        {
            List<enChemAnalystInvoice> listOfdata = new List<enChemAnalystInvoice>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetProformaInvoiceDetailsByid]", new object[] {
                                                                  en.InvoiceID,
                                                                }))

            {
                while (idr.Read())
                {
                    enChemAnalystInvoice objENProduct = new enChemAnalystInvoice();
                    ConstructObjectListtaxInvoicenew(idr, objENProduct);
                    listOfdata.Add(objENProduct);
                }
            }

            return listOfdata;
        }
        public List<enChemAnalystInvoice> GetfileList(enSearchFilter searchFilter)
        {
            List<enChemAnalystInvoice> listOfdatas = new List<enChemAnalystInvoice>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetFile_CustomSearch]", new object[] {
                                                            searchFilter.searchKeyword,
                                                            searchFilter.StartRowNumber,
                                                            searchFilter.EndRowNumber,
                                                             searchFilter.UserId,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enChemAnalystInvoice objEN = new enChemAnalystInvoice();
                        ConstructObjectGetfileList(idr, objEN);
                        listOfdatas.Add(objEN);
                    }
                }

                return listOfdatas;
            }
            catch (Exception ex)
            {

            }
            return listOfdatas;
        }

        private void ConstructObjectListtaxInvoice(IDataReader idr, enChemAnalystInvoice objENLeads)
        {
            objENLeads.InvoiceID = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.InvoiceNo = DBNull.Value == idr["InvoiceNo"] ? null : Convert.ToString(idr["InvoiceNo"]);
            objENLeads.InvoiceDate = DBNull.Value == idr["InvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(idr["InvoiceDate"]);
            objENLeads.ReverseCharge = DBNull.Value == idr["ReverseCharge"] ? null : Convert.ToString(idr["ReverseCharge"]);
            objENLeads.PO = DBNull.Value == idr["PO"] ? null : Convert.ToString(idr["PO"]);
            objENLeads.PODATE = DBNull.Value == idr["PODATE"] ? null : Convert.ToString(idr["PODATE"]);
            objENLeads.KindAttn = DBNull.Value == idr["KindAttn"] ? null : Convert.ToString(idr["KindAttn"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.Address = DBNull.Value == idr["Address"] ? null : Convert.ToString(idr["Address"]);
          
            objENLeads.PAN = DBNull.Value == idr["PAN"] ? null : Convert.ToString(idr["PAN"]);
            objENLeads.State = DBNull.Value == idr["State"] ? null : Convert.ToString(idr["State"]);
            objENLeads.SupplyState = DBNull.Value == idr["Supply_Name_Of_State"] ? null : Convert.ToString(idr["Supply_Name_Of_State"]);
            objENLeads.Telephone = DBNull.Value == idr["Telephone"] ? null : Convert.ToString(idr["Telephone"]);
            objENLeads.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            objENLeads.SacCode = DBNull.Value == idr["SacCode"] ? null : Convert.ToString(idr["SacCode"]);
            objENLeads.Amount = DBNull.Value == idr["Amount"] ? 0 : Convert.ToDecimal(idr["Amount"]);
            objENLeads.Subtotal = DBNull.Value == idr["Subtotal"] ? 0 : Convert.ToDecimal(idr["Subtotal"]);
            objENLeads.CGST = DBNull.Value == idr["CGST"] ? null : Convert.ToString(idr["CGST"]);
            objENLeads.SGST = DBNull.Value == idr["SGST"] ? null : Convert.ToString(idr["SGST"]);
            objENLeads.IGST = DBNull.Value == idr["IGST"] ? null : Convert.ToString(idr["IGST"]);
            objENLeads.TotalTaxAmount = DBNull.Value == idr["TotalTaxAmount"] ? 0 : Convert.ToDecimal(idr["TotalTaxAmount"]);
            objENLeads.TotalAmount = DBNull.Value == idr["TotalAmount"] ? 0 : Convert.ToDecimal(idr["TotalAmount"]);
            objENLeads.AmountInWords = DBNull.Value == idr["AmountInWords"] ? null : Convert.ToString(idr["AmountInWords"]);
            objENLeads.PaymentTerms = DBNull.Value == idr["PaymentTerms"] ? null : Convert.ToString(idr["PaymentTerms"]);
            objENLeads.Discription = DBNull.Value == idr["Discription"] ? null : Convert.ToString(idr["Discription"]);
            objENLeads.IGST = DBNull.Value == idr["IGST"] ? null : Convert.ToString(idr["IGST"]);
            objENLeads.GSTIN = DBNull.Value == idr["GSTIN"] ? null : Convert.ToString(idr["GSTIN"]);
            objENLeads.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            objENLeads.Createdby = DBNull.Value == idr["Createdby"] ? null : Convert.ToString(idr["Createdby"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.POfiles = DBNull.Value == idr["POfiles"] ? null : Convert.ToString(idr["POfiles"]);
        }


        private void ConstructObjectListtaxInvoicenew(IDataReader idr, enChemAnalystInvoice objENLeads)
        {
            objENLeads.InvoiceID = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.InvoiceNo = DBNull.Value == idr["InvoiceNo"] ? null : Convert.ToString(idr["InvoiceNo"]);
            objENLeads.InvoiceDate = DBNull.Value == idr["InvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(idr["InvoiceDate"]);
            objENLeads.ReverseCharge = DBNull.Value == idr["ReverseCharge"] ? null : Convert.ToString(idr["ReverseCharge"]);
            objENLeads.PO = DBNull.Value == idr["PO"] ? null : Convert.ToString(idr["PO"]);
            objENLeads.PODATE = DBNull.Value == idr["PODATE"] ? null : Convert.ToString(idr["PODATE"]);
            objENLeads.KindAttn = DBNull.Value == idr["KindAttn"] ? null : Convert.ToString(idr["KindAttn"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.Address = DBNull.Value == idr["Address"] ? null : Convert.ToString(idr["Address"]);

            objENLeads.PAN = DBNull.Value == idr["PAN"] ? null : Convert.ToString(idr["PAN"]);
            objENLeads.State = DBNull.Value == idr["State"] ? null : Convert.ToString(idr["State"]);
            objENLeads.SupplyState = DBNull.Value == idr["Supply_Name_Of_State"] ? null : Convert.ToString(idr["Supply_Name_Of_State"]);
            objENLeads.Telephone = DBNull.Value == idr["Telephone"] ? null : Convert.ToString(idr["Telephone"]);
            objENLeads.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            objENLeads.SacCode = DBNull.Value == idr["SacCode"] ? null : Convert.ToString(idr["SacCode"]);
            objENLeads.Currency = DBNull.Value == idr["Currency"] ? null : Convert.ToString(idr["Currency"]);
            objENLeads.Amount = DBNull.Value == idr["Amount"] ? 0 : Convert.ToDecimal(idr["Amount"]);
            objENLeads.Discount = DBNull.Value == idr["Discount"] ? 0 : Convert.ToDecimal(idr["Discount"]);
            objENLeads.Subtotal = DBNull.Value == idr["Subtotal"] ? 0 : Convert.ToDecimal(idr["Subtotal"]);
            objENLeads.CGST = DBNull.Value == idr["CGST"] ? null : Convert.ToString(idr["CGST"]);
            objENLeads.SGST = DBNull.Value == idr["SGST"] ? null : Convert.ToString(idr["SGST"]);
            objENLeads.IGST = DBNull.Value == idr["IGST"] ? null : Convert.ToString(idr["IGST"]);
            objENLeads.TotalTaxAmount = DBNull.Value == idr["TotalTaxAmount"] ? 0 : Convert.ToDecimal(idr["TotalTaxAmount"]);
            objENLeads.TotalAmount = DBNull.Value == idr["TotalAmount"] ? 0 : Convert.ToDecimal(idr["TotalAmount"]);
            objENLeads.AmountInWords = DBNull.Value == idr["AmountInWords"] ? null : Convert.ToString(idr["AmountInWords"]);
            objENLeads.PaymentTerms = DBNull.Value == idr["PaymentTerms"] ? null : Convert.ToString(idr["PaymentTerms"]);
            objENLeads.Discription = DBNull.Value == idr["Discription"] ? null : Convert.ToString(idr["Discription"]);
            objENLeads.IGST = DBNull.Value == idr["IGST"] ? null : Convert.ToString(idr["IGST"]);
            objENLeads.GSTIN = DBNull.Value == idr["GSTIN"] ? null : Convert.ToString(idr["GSTIN"]);
            objENLeads.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            objENLeads.Createdby = DBNull.Value == idr["Createdby"] ? null : Convert.ToString(idr["Createdby"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.POfiles = DBNull.Value == idr["POfiles"] ? null : Convert.ToString(idr["POfiles"]);
        }


        private void ConstructObjectListProformaInvoice(IDataReader idr, enChemAnalystInvoice objENLeads)
        {
            objENLeads.InvoiceID = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.InvoiceNo = DBNull.Value == idr["InvoiceNo"] ? null : Convert.ToString(idr["InvoiceNo"]);
            objENLeads.InvoiceDate = DBNull.Value == idr["InvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(idr["InvoiceDate"]);
            objENLeads.ReverseCharge = DBNull.Value == idr["ReverseCharge"] ? null : Convert.ToString(idr["ReverseCharge"]);
            objENLeads.PO = DBNull.Value == idr["PO"] ? null : Convert.ToString(idr["PO"]);
            objENLeads.PODATE = DBNull.Value == idr["PODATE"] ? null : Convert.ToString(idr["PODATE"]);
            objENLeads.KindAttn = DBNull.Value == idr["KindAttn"] ? null : Convert.ToString(idr["KindAttn"]);
            objENLeads.Name = DBNull.Value == idr["Name"] ? null : Convert.ToString(idr["Name"]);
            objENLeads.Address = DBNull.Value == idr["Address"] ? null : Convert.ToString(idr["Address"]);
            objENLeads.VAT = DBNull.Value == idr["VAT"] ? null : Convert.ToString(idr["VAT"]);
            objENLeads.PAN = DBNull.Value == idr["PAN"] ? null : Convert.ToString(idr["PAN"]);
            objENLeads.State = DBNull.Value == idr["State"] ? null : Convert.ToString(idr["State"]);
            objENLeads.Telephone = DBNull.Value == idr["Telephone"] ? null : Convert.ToString(idr["Telephone"]);
            objENLeads.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            objENLeads.SacCode = DBNull.Value == idr["SacCode"] ? null : Convert.ToString(idr["SacCode"]);
            objENLeads.Amount = DBNull.Value == idr["Amount"] ? 0 : Convert.ToDecimal(idr["Amount"]);
            objENLeads.Subtotal = DBNull.Value == idr["Subtotal"] ? 0 : Convert.ToDecimal(idr["Subtotal"]);
            objENLeads.CGST = DBNull.Value == idr["CGST"] ? null : Convert.ToString(idr["CGST"]);
            objENLeads.SGST = DBNull.Value == idr["SGST"] ? null : Convert.ToString(idr["SGST"]);
            objENLeads.IGST = DBNull.Value == idr["IGST"] ? null : Convert.ToString(idr["IGST"]);
            objENLeads.TotalTaxAmount = DBNull.Value == idr["TotalTaxAmount"] ? 0 : Convert.ToDecimal(idr["TotalTaxAmount"]);
            objENLeads.TotalAmount = DBNull.Value == idr["TotalAmount"] ? 0 : Convert.ToDecimal(idr["TotalAmount"]);
            objENLeads.AmountInWords = DBNull.Value == idr["AmountInWords"] ? null : Convert.ToString(idr["AmountInWords"]);
            objENLeads.PaymentTerms = DBNull.Value == idr["PaymentTerms"] ? null : Convert.ToString(idr["PaymentTerms"]);
            objENLeads.Discription = DBNull.Value == idr["Discription"] ? null : Convert.ToString(idr["Discription"]);
            objENLeads.IGST = DBNull.Value == idr["IGST"] ? null : Convert.ToString(idr["IGST"]);
            objENLeads.Country = DBNull.Value == idr["Country"] ? null : Convert.ToString(idr["Country"]);
            objENLeads.Createdby = DBNull.Value == idr["Createdby"] ? null : Convert.ToString(idr["Createdby"]);
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
        }

        private void ConstructObjectInsertStatus(IDataReader idr, enChemAnalystInvoice enUser_)
        {
            enUser_.ValidationMessage = DBNull.Value == idr["Message"] ? null : Convert.ToString(idr["Message"]);

        }
        private void ConstructObjectGetfileList(IDataReader idr, enChemAnalystInvoice objENLeads)
        {
            objENLeads.CreatedDate = DBNull.Value == idr["CreatedDate"] ? (DateTime?)null : Convert.ToDateTime(idr["CreatedDate"]);
            objENLeads.InvoiceID = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            objENLeads.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            objENLeads.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            objENLeads.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
        }

    }
    
}
