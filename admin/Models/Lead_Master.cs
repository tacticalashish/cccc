using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class Lead_Master
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [StringLength(70)]
        public string CorpEmail { get; set; }

        [StringLength(50)]
        public string Organisation { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string InterestArea { get; set; }

        [StringLength(100)]
        public string Subject { get; set; }

        [StringLength(500)]
        public string Query { get; set; }

        [StringLength(50)]
        public string UserType { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string AssignTo { get; set; }
        public string Designation { get; set; }

        public DateTime? AssignDate { get; set; }

        public string IPAddress { get; set; }

        public string module { get; set; }
        public string DiscriptionMessage { get; set; }
        public string OutboundLeadSource { get; set; }
        public string LeadType { get; set; }
        public DateTime? RequestedDate { get; set; }

        public DateTime? SetFollowUp { get; set; }
        public DateTime? ExpectedCloseDate { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string files { get; set; }
        public string InvoiceNumber { get; set; }
        public string LostReason { get; set; }

        public string PaymentTerms { get; set; }
        public string PaymentOption { get; set; }
        public DateTime? RemainingPaymentDate { get; set; }

        public string CRMLeadStatus { get; set; }
        public string LastFollowupDiscussion { get; set; }

        public string CountryName { get; set; }
        public string StateName { get; set; }
        public int CountryID { get; set; }
        public int StateId { get; set; }
        public string Mobile { get; set; }
        public string DiscussionStatus { get; set; }

        public string RemainingAmount { get; set; }
        public string ContactCountryCode { get; set; }
        public string TelephoneCountryCode { get; set; }

        public DateTime? LastDemoDate { get; set; }

        public string DemoDiscussion { get; set; }
    }
}