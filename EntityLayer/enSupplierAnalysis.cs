using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
   public class enSupplierAnalysis
    {

        // Core identifiers
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CompanyProductId { get; set; }

        // ===============================
        // Field / Category Mapping
        // ===============================
        public string FieldName { get; set; }
        public string CategoryName { get; set; }
        public string CompanyValue { get; set; }

        // ===============================
        // Company Basic Information
        // ===============================
        public string CompanyName { get; set; }
        public string BusinessType { get; set; }
        public string YearEstablished { get; set; }
        public string HQAddress { get; set; }
        public string Country { get; set; }
        public string CountryOfRegistration { get; set; }
        public string Website { get; set; }
        public string TaxVATID { get; set; }

        // ===============================
        // Contact Information
        // ===============================
        public string ContactPerson { get; set; }
        public string ContactInfoSalesPerson { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LanguagesSpoken { get; set; }

        // ===============================
        // Business & Operations
        // ===============================
        public string MainProducts { get; set; }
        public string IndustriesServed { get; set; }
        public string Employees { get; set; }
        public string AnnualTurnover { get; set; }
        public string Revenue { get; set; }
        public string RDCapabilities { get; set; }
        public string OEMODMServices { get; set; }

        // ===============================
        // Certifications & Compliance
        // ===============================
        public string ISO9001 { get; set; }
        public string ISO14001 { get; set; }
        public string OtherCertifications { get; set; }
        public string CEFDA_RoHS { get; set; }

        // ===============================
        // Trade & Logistics
        // ===============================
        public string ExportRegions { get; set; }
        public string IncotermsUsed { get; set; }
        public string IncotermsSupported { get; set; }
        public string LeadTime { get; set; }
        public string MOQ { get; set; }
        public string ShippingMethods { get; set; }

        // ===============================
        // ESG & Policies
        // ===============================
        public string EnvironmentalPolicy { get; set; }
        public string SocialResponsibilityPolicy { get; set; }
        public string ConflictMineralsPolicy { get; set; }
        public string CodeOfConduct { get; set; }
        public string AntiBriberyPolicy { get; set; }
        public string WhistleblowerPolicy { get; set; }

        // ===============================
        // Clients & Projects
        // ===============================
        public string MajorClients { get; set; }
        public string ReferenceProjects { get; set; }

        // ===============================
        // Factory & Production
        // ===============================
        public string FactoryName { get; set; }
        public string FactoryAddress { get; set; }
        public string FactorySize { get; set; }
        public string ProductionLines { get; set; }
        public string KeyMachineryEquipment { get; set; }
        public string MonthlyProductionCapacity { get; set; }
        public string InHouseQCQA { get; set; }
        public string QualityControlMethods { get; set; }
        public string ThirdPartyAuditPassed { get; set; }
        public string exportlicensenumber { get; set; }
        public string oemodmcapability { get; set; }
        public string factoryphotosdocumentsavailable { get; set; }

        // ===============================
        // Legal & Financial
        // ===============================
        public string BusinessRegistrationNumber { get; set; }
        public string CIN { get; set; }
        public string DUNSNumber { get; set; }
        public string ParentCompany { get; set; }
        public string BankName { get; set; }
        public string Bankaccountnumber { get; set; }
        public string swiftcode { get; set; }
        public string CurrencyUsed { get; set; }
        public string PaymentTerms { get; set; }
        public string incotermssupported { get; set; }

        // ===============================
        // Risk, IT & Governance
        // ===============================
        public string SanctionsCheckStatus { get; set; }    
        public string GDPRCompliance { get; set; }
        public string CyberSecurity { get; set; }
        public string BusinessContinuityPlan { get; set; }

        // ===============================
        // Awards & Digital
        // ===============================
        public string IndustryAwards { get; set; }
        public string TradeFairParticipation { get; set; }
        public string CertificationsOfExcellence { get; set; }
        public string ERPSystemUsed { get; set; }
        public string OnlineOrderingPlatform { get; set; }
        public string EDICapable { get; set; }
        public string Cad3dSupport { get; set; }
        public string ApiIntegration { get; set; }

        // ===============================
        // Documents & Media
        // ===============================
        public string CompanyProfilePDF { get; set; }
        public string ProductCatalog { get; set; }
        public string RoHS_REACHDeclarations { get; set; }
        public string TestReports { get; set; }
        public string MSDSAvailable { get; set; }
        public string PublicPdfLink { get; set; }
        public string InsuranceCertificate { get; set; }

        // ===============================
        // Products & Market Info
        // ===============================
        public string AllProducts { get; set; }
        public string SupplierType { get; set; }
        public string ImportantCompanyNews { get; set; }
        public string FraudCases { get; set; }

        // ===============================
        // Ownership & Registry
        // ===============================
        public string OwnerName { get; set; }
        public string ShareholdingDetails { get; set; }
        public string JurisdictionCode { get; set; }
        public string CompanyNumber { get; set; }
        public string OpenCorporatesURL { get; set; }
        public string RegistryURL { get; set; }
        public string TradeNames { get; set; }
        public string CompanyStatus { get; set; }
        public string IncorporationDate { get; set; }
        public string PrimaryLegalNames { get; set; }
        public string NonPrimaryLegalNames { get; set; }
        public string CompanyType { get; set; }
        public string DissolutionDate { get; set; }
        public string NonProfitFlag { get; set; }
        public string BranchFlag { get; set; }
        public string BranchHomeEntity { get; set; }

        public string classification { get; set; }

        // ===============================
        // Global Identifiers
        // ===============================
        public string NAICSCodes { get; set; }
        public string TaxIds { get; set; }
        public string RegisteredAddresses { get; set; }
        public string officers { get; set; }
        public string LEI { get; set; }
        public string ValidationSources { get; set; }
        public string RegistrationStatus { get; set; }
        public string RegistrationDate { get; set; }
        public string LastUpdateDate { get; set; }
        public string NextRenewalDate { get; set; }
        public string GLEIFURL { get; set; }
        public string CompanyDescription { get; set; }
        public string UltimateParentCompany { get; set; }

        public string CorporateRelationCompanyName { get; set; }
        public string CorporateRelationCompanyType { get; set; }
        public string CorporateRelationPrimaryOfficeLocation { get; set; }
        public string CorporateRelationCompanyWebpage { get; set; }
        public string CorporateRelationCompanyDescription { get; set; }
        public string CorporateRelationUltimateParentCompanyName { get; set; }
        public string CorporateRelationUltimateParentCompanyType { get; set; }
        public string CorporateRelationUltimateParentOfficeLocation { get; set; }
        public string CorporateRelationUltimateParentWebpage { get; set; }
        public string CorporateRelationUltimateParentDescription { get; set; }

        // ===============================
        // Validation & Pagination
        // ===============================
        public string ValidationMessage { get; set; }
        public int PageNumber { get; set; }
        public int StartRowNumber { get; set; }
        public int EndRowNumber { get; set; }
        public int RowNumber { get; set; }
        public int RecordsCount { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int WishlistId { get; set; }
        public Dictionary<string, List<string>> Filters { get; set; }
        public DataTable FilterTable { get; set; }
        public string WishlistName { get; set; }
        public string About { get; set; }
        public string IpAddress { get; set; }
        public string CompositeScore { get; set; }
        public string RiskCategory { get; set; }
        public string Industry { get; set; }
        public string Description { get; set; }
        public string AnalysisDate { get; set; }
        public int RiskID { get; set; }
        public class SupplierRequestModel
        {
            public string SupplierName { get; set; }
            public string Country { get; set; }
            public string Website { get; set; }
            public string About { get; set; }
           
        }

    }
}
