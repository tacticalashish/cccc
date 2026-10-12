using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_ChemAnalystInvoice
    {
        public string GSTIN { get; set; }
        public string Country { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
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
        public string Supply_Name_Of_State { get; set; }
        public int InvoiceID { get; set; }
        public string POfiles { get; set; }
        public string Currency { get; set; }
        public decimal Discount { get; set; }




    }
}