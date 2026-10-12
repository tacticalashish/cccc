using ChemAnalyst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChemAnalyst.ViewModel
{
    public class ProcurementGuideDetailsViewModel
    {
        public IEnumerable<SelectListItem> LstCountry { get; set; }
        public SA_ProcurementGuide ProcurementGuide { get; set; }

        public IList<SA_ProcurementGuide> ProcurementGuideList { get; set; }

        public IList<SA_ProcurementGuide> ProcurementGuideRelatedNewsList { get; set; }
    }
}