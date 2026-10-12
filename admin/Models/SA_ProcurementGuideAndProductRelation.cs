using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public partial class SA_ProcurementGuideAndProductRelation
    {
        public int Id { get; set; }

        [Required]

        public int SA_ProcurementGuideId { get; set; }

        [Required]

        public int SA_ProductId { get; set; }
    }
}