using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public partial class SA_Customer_LoginDetail
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? Login { get; set; }
        public DateTime? Logout { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}