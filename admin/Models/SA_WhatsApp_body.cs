using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.Models
{
    public class SA_WhatsApp_body
    {


        public class Rootobject
        {
            public string messaging_product { get; set; }
            public string to { get; set; }
            public string type { get; set; }
            public Template template { get; set; }
        }

        public class Template
        {
            public string name { get; set; }
            public Language language { get; set; }
            public Component[] components { get; set; }
        }

        public class Language
        {
            public string code { get; set; }
        }

        public class Component
        {
            public string type { get; set; }
            public Parameter[] parameters { get; set; }
        }

        public class Parameter
        {
            public string type { get; set; }
            public string text { get; set; }
        }


    }
}