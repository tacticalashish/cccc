using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using DAL = DataLayer.dlCurrency;
using EntityLayer.CustomEntity;

namespace BusinessLayer
{
   public class blCurrency
    {
        private enCurrency _enCurrency = null;
        private DAL _objDAL = null;

        public enCurrency GetEntityReference
        {
            get { return this._enCurrency; }
        }

        public blCurrency(enCurrency enCurrency_)
        {
            this._enCurrency = enCurrency_;
        }

        public void Create()
        {
            GetDALReference().Create();
        }
        public void CreateINR()
        {
            GetDALReference().CreateINR();
        }
        
        public List<enCurrency> getdate()
        {
            return GetDALReference().getdate();
        }

        public List<enCurrency> ReadBasedOnSearchFilter(enSearchFilter model)
        {
            return GetDALReference().ReadBasedOnSearchFilter(model);
        }

        public List<enCurrency> GetlatestCurrencyrates()
        {
            return GetDALReference().GetlatestCurrencyrates();
        }

        public List<enCurrency> GetCurrencyratesBasedOnfilter(enSearchFilter enSearchFilter)
        {
            return GetDALReference().GetCurrencyratesBasedOnfilter(enSearchFilter);
        }

        public List<enCurrency> DownloadCurrencyratesBasedOnfilter(enSearchFilter enSearchFilter)
        {
            return GetDALReference().DownloadCurrencyratesBasedOnfilter(enSearchFilter);
        }


        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enCurrency);
            }
            return _objDAL;
        }
    }
}

