using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class ApiSubscription
    {

        public int UserId { get; set; }


        [StringLength(50)]
        public string UserName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        [StringLength(70)]
        public string AuthCode { get; set; }

        [StringLength(50)]
        public string UserIpAddress { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string ValidationMessage { get; set; }

        [StringLength(50)]
        public string Local_IP_Address { get; set; }

        [StringLength(50)]
        public string LatLong { get; set; }

        [StringLength(100)]
        public string CustomerCountry { get; set; }


        [StringLength(500)]
        public string City { get; set; }

        public int Yearly { get; set; }
        public int Monthly { get; set; }
        public int Weekly { get; set; }
        public int Daily { get; set; }
        public bool IsActive { get; set; }
        public int ValidYearFrom { get; set; }
        public int ValidYearTo { get; set; }

        public string searchKeyword { get; set; }

        public int PageNumber { get; set; }

        public int LeadId { get; set; }

        public string Product { get; set; }

        public string Country { get; set; }

        public int flagId { get; set; }
        
    }
}