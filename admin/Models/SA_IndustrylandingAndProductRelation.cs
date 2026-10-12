using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_IndustrylandingAndProductRelation
    {
        public int Id { get; set; }

        [Required]

        public int SA_IndustrylandingId { get; set; }

        [Required]

        public int SA_ProductId { get; set; }

    }
}