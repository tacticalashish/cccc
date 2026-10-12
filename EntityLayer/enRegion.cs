using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enRegion
    {
        public int ID { get; set; }

        public string RegionName { get; set; }

        public bool? Active { get; set; }

        public bool IsAllow { get; set; }
    }
}
