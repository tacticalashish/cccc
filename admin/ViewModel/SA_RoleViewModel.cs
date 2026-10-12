namespace ChemAnalyst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SA_RoleViewModel
    {
        [Key]
        public int id { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        [StringLength(500)]
        public string RoleDiscription { get; set; }
        public string ForwardingEmail { get; set; }
        //public DateTime? CreatedTime { get; set; }
        public string CreatedTime { get; set; }

        public bool ChemicalPricing { get; set; }
        public bool MarketAnalysis { get; set; }
        public bool CompanyProfile { get; set; }
        public bool IndustryReports { get; set; }
        public bool News { get; set; }
        public bool Deals { get; set; }
        public bool SubscriptionManagement { get; set; }

        public bool SectoralReport { get; set; }
        public bool CommentaryManagement { get; set; }
        public bool DisruptionTracker { get; set; }
        public bool AssociateApproval { get; set; }
        public bool ConsultantApproval { get; set; }

        public bool PricingOverview { get; set; }
        public bool TicketManagement { get; set; }
        public bool ReportTool { get; set; }
        public bool CustomerManagement { get; set; }
        public bool InternalCommentaryAssociate { get; set; }
        public bool InternalCommentaryConsulatant { get; set; }
        public bool ExternalCommentaryAssociate { get; set; }
        public bool SalesLeadManagement { get; set; }
        public bool OperationLeadApproval { get; set; }
        public bool AccountsLeadApproval { get; set; }
        public bool SalesManagerLeadApproval { get; set; }
        public bool CRMDashboard { get; set; }
        public bool AssociateDisruptionTracker { get; set; }
        public bool ConsultantDisruptionTracker { get; set; }
        public bool AssociateMarketAnalysis { get; set; }
        public bool ConsultantMarketAnalysis { get; set; }
        public bool ChemicalPricingDemo { get; set; }
        public bool MarketAnalysisDemo { get; set; }
        public bool ManagerPricingandCommentary { get; set; }
        public bool QCPricing { get; set; }
        public bool QCCommentary { get; set; }
        public bool FreelPriceAssociate { get; set; }
        public bool ConsultantSales { get; set; }
        public bool FreightIndex { get; set; }
        public bool AssociatePreSales { get; set; }
        public bool ConsultantPreSales { get; set; }



    }
    

}
