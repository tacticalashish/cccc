namespace ChemAnalyst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class SA_ProductViewModel
    {
        [Key]
        public int id { get; set; }

        [StringLength(50)]
        //[Required(ErrorMessage = "Email Address is required")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required]
        //[Remote("SaveProduct", "ProductAndCategory","", HttpMethod = "POST", ErrorMessage = "Product already exist with the same name.")]
        [Remote(
            "CheckProductName",
            "ProductAndCategory",
            AdditionalFields = "ExistingProduct",
            ErrorMessage = "Product already exist with the same name.",
            HttpMethod = "POST"
        )]
        public string ProductName { get; set; }

        public string ExistingProduct { get; set; }

        [StringLength(500)]
        public string ProductDiscription { get; set; }
        [StringLength(50)]
        public string Meta { get; set; }

        [StringLength(500)]
        public string MetaDiscription { get; set; }

        [StringLength(500)]
        public string ProductImg { get; set; }

        public int? status { get; set; }

        public string Category { get; set; }

        public int RegionFID { get; set; }

        public int CountryFID { get; set; }

        public string Countries { get; set; }

        public DateTime? CreatedTime { get; set; }

        public List<SelectListItem> CategoryList { get; set; }
        public int? MarketAnalysisStatus { get; set; }



    }
}
