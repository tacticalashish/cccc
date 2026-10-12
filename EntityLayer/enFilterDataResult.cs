using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class enFilterDataResult
    {
        public List<enAdminChemicalPrice> Years { get; set; }
        public List<enAdminChemicalPrice> HSCodes { get; set; }
        public List<enAdminChemicalPrice> Grades { get; set; }
        public List<enAdminChemicalPrice> ExporterNames { get; set; }
        public List<enAdminChemicalPrice> ExporterCountries { get; set; }
        public List<enAdminChemicalPrice> PortsOfOrigin { get; set; }
        public List<enAdminChemicalPrice> ConsigneeNames { get; set; }
        public List<enAdminChemicalPrice> CountriesOfImport { get; set; }
        public List<enAdminChemicalPrice> PortsOfDestination { get; set; }
        public List<enAdminChemicalPrice> Incoterms { get; set; }
        public List<enAdminChemicalPrice> ShipmentModes { get; set; }
    }
}
