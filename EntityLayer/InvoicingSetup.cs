using System;

namespace EntityLayer
{
    // Backs the "Invoicing setup" form on CustomerDetail.cshtml (Billing & invoices tab).
    // One row per CompanyId in table SA_Invoicingsetup — see /database/SA_Invoicingsetup.sql.
    public class InvoicingSetup
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string HowToSendInvoice { get; set; }
        public string InvoiceEmail { get; set; }
        public string VendorRegistrationNo { get; set; }
        public bool PORequiredBeforeInvoice { get; set; }
        public string UploadPortal { get; set; }
        public bool HardCopyNeeded { get; set; }
        public string HardCopyAddress { get; set; }
        public string SpecialInstructions { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
