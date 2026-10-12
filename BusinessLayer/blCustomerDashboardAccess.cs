using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.CustomEntity;
using EntityLayer;
using DAL = DataLayer.dlCustomerDashboardAccess;
using EntityLayer.CustomEntity;

namespace BusinessLayer
{
   public class blCustomerDashboardAccess
    {
        private enCustomerDashboardAccess _enCustomerDashboardAccess = null;
        private DAL _objDAL = null;

        public enCustomerDashboardAccess GetEntityReference
        {
            get { return this._enCustomerDashboardAccess; }
        }

        public blCustomerDashboardAccess(enCustomerDashboardAccess enCustomerDashboardAccess_)
        {
            this._enCustomerDashboardAccess = enCustomerDashboardAccess_;
        }
        public void ReadBasedOnCustomerDashboardAccess()
        {
            GetDALReference().ReadBasedOnCustomerDashboardAccess();
        }
        //public void ReadBasedOnCustomerDashboardAccessUpdate(objENSearchFilter)
        //{
        //    GetDALReference().ReadBasedOnCustomerDashboardAccessUpdate();
        //}
        public List<enCustomerDashboardAccess> ReadBasedOnCustomerDashboardAccessUpdate(enSearchFilter searchFilter)
        {
            return GetDALReference().ReadBasedOnCustomerDashboardAccessUpdate(searchFilter);
        }

        public List<enCustomerDashboardAccess> ReadAll()
        {
            return GetDALReference().ReadAll();
        }
        public List<enCustomerDashboardAccess> ReadAllProduct()
        {
            return GetDALReference().ReadAllProduct();
        }
        public List<enCustomerDashboardAccess> InsertLogindetails(enSearchFilter objChemical)
        {
            return GetDALReference().InsertLogindetails(objChemical);
        }

        public List<enCustomerDashboardAccess> NewsReadAll()
        {
            return GetDALReference().NewsReadAll();
        }
        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enCustomerDashboardAccess);
            }
            return _objDAL;
        }
    }
}
