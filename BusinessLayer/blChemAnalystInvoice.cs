using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = DataLayer.dlChemAnalystInvoice;
namespace BusinessLayer
{
   public  class blChemAnalystInvoice
    {
        private enChemAnalystInvoice _enInvoice = null;
        private DAL _objDAL = null;

        public enChemAnalystInvoice GetEntityReference
        {
            get { return this._enInvoice; }
        }
        public blChemAnalystInvoice(enChemAnalystInvoice enInvoice_)
        {
            this._enInvoice = enInvoice_;
        }
        public List<enChemAnalystInvoice> InsertInvoiceRecord(enChemAnalystInvoice obj)
        {
            return GetDALReference().InsertInvoiceRecord(obj);
        }
        public List<enChemAnalystInvoice> InsertProformaInvoiceRecord(enChemAnalystInvoice obj)
        {
            return GetDALReference().InsertProformaInvoiceRecord(obj);
        }
        
        public List<enChemAnalystInvoice> UpdateTaxInvoiceRecord(enChemAnalystInvoice obj)
        {
            return GetDALReference().UpdateTaxInvoiceRecord(obj);
        }
        public List<enChemAnalystInvoice> UpdateProformaInvoiceRecord(enChemAnalystInvoice obj)
        {
            return GetDALReference().UpdateProformaInvoiceRecord(obj);
        }
        
        public List<enChemAnalystInvoice> GetChemanalystTaxInvoiceList(enSearchFilter objChemical)
        {
            return GetDALReference().GetChemanalystTaxInvoiceList(objChemical);
        }
        public List<enChemAnalystInvoice> GetChemanalystProformaInvoiceList(enSearchFilter objChemical)
        {
            return GetDALReference().GetChemanalystProformaInvoiceList(objChemical);
        }
        
        public List<enChemAnalystInvoice> BindTaxInvoiceDetailsByid(enChemAnalystInvoice en)
        {
            return GetDALReference().BindTaxInvoiceDetailsByid(en);
        }
        public List<enChemAnalystInvoice> BindProformaInvoiceDetailsByid(enChemAnalystInvoice en)
        {
            return GetDALReference().BindProformaInvoiceDetailsByid(en);
        }
        public List<enChemAnalystInvoice> GetfileList(enSearchFilter objChemical)
        {
            return GetDALReference().GetfileList(objChemical);
        }
        
        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enInvoice);
            }
            return _objDAL;
        }


    }
}
