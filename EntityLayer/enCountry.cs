using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enCountry
    {
        public int ID { get; set; }

        public string CountryName { get; set; }

        public bool Active { get; set; }

        public bool IsSelected { get; set; }

        public string CountryCode { get; set; }

        public bool IsAllow { get; set; }
    }
}
