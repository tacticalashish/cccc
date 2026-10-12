using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
   public class enChemAnalystInvoice
    {
        public int InvoiceID { get; set; }
        public int RowNumber { get; set; }
        public int RecordsCount { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string ReverseCharge { get; set; }
        public string PO { get; set; }
        public string PODATE { get; set; }
        public string KindAttn { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string VAT { get; set; }
        public string PAN { get; set; }
        public string State { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string SacCode { get; set; }
        public decimal Amount { get; set; }
        public decimal Subtotal { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string AmountInWords { get; set; }
        public string PaymentTerms { get; set; }
        public string Discription { get; set; }
        public string ValidationMessage { get; set; }
        public string LutNo { get; set; }
        public string Country { get; set; }
        public string Createdby { get; set; }
         public DateTime? CreatedDate { get; set; }
        public string SupplyState { get; set; }
        public string GSTIN { get; set; }
        public string POfiles { get; set; }
        public string FileName { get; set; }
        public string Currency { get; set; }
        public decimal Discount { get; set; }


    }
}
