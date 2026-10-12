using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = DataLayer.dlProduct;

namespace BusinessLayer
{
    public class blProduct
    {
        private enProduct _enProduct = null;
        private DAL _objDAL = null;

        public enProduct GetEntityReference
        {
            get { return this._enProduct; }
        }

        public blProduct(enProduct enProduct_)
        {
            this._enProduct = enProduct_;
        }

        public List<enProduct> ReportBasedOnSearchFilter(enSearchFilter searchFilter)
        {
            return GetDALReference().ReportBasedOnSearchFilter(searchFilter);
        }

        public List<enProduct> SchedulerNewsProduct()
        {
            return GetDALReference().SchedulerNewsProduct();
        }

        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enProduct);
            }
            return _objDAL;
        }
    }
}
