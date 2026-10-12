using System;

namespace EntityLayer
{
    // Backs the Documents tab on CustomerDetail.cshtml.
    // One row per uploaded file, in table SA_CustomerDocuments — see
    // /database/SA_CustomerDocuments.sql. The physical file lives under
    // ~/CustomerDocs/{CompanyId}/{FileName} — same name on disk as in the DB.
    public class CustomerDocument
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string DocumentType { get; set; }
        public string ReferenceNote { get; set; }
        public string FileName { get; set; }
        public DateTime? UploadedDate { get; set; }
        public string UploadedBy { get; set; }
    }
}
