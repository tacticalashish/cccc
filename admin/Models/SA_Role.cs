namespace ChemAnalyst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SA_Role
    {
        public int id { get; set; }
        
        public string Role { get; set; }

        public string RoleDiscription { get; set; }

        public string ForwardingEmail { get; set; }

        public DateTime? CreatedTime { get; set; }

        
        
    }
}
