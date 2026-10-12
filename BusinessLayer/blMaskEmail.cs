using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using DAL = DataLayer.dlMaskEmail;
using EntityLayer.CustomEntity;

namespace BusinessLayer
{
   public class blMaskEmail
    {
        private enMaskEmail _enMaskEmail = null;
        private DAL _objDAL = null;

        public enMaskEmail GetEntityReference
        {
            get { return this._enMaskEmail; }
        }

        public blMaskEmail(enMaskEmail enMaskEmail_)
        {
            this._enMaskEmail = enMaskEmail_;
        }
        public void UnSubscribe()
        {
            GetDALReference().UnSubscribe();
        }
        public void Update()
        {
            GetDALReference().Update();
        }

        public void Refresh()
        {
            GetDALReference().Refresh();
        }

        public List<enMaskEmail> ReadBasedOnSearchFilter(enSearchFilter model)
        {
            return GetDALReference().ReadBasedOnSearchFilter(model);
        }
        public List<enMaskEmail> ReadLeadBasedOnSearchFilter(enSearchFilter model)
        {
            return GetDALReference().ReadLeadBasedOnSearchFilter(model);
        }
        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enMaskEmail);
            }
            return _objDAL;
        }

        public List<enMaskEmail> getallproduct(string Email)
        {
            return GetDALReference().getallproduct(Email);
        }

        public List<enMaskEmail> uploadproductNewsAndDeals(string productlist, string Email)
        {
            return GetDALReference().uploadproductNewsAndDeals(productlist, Email);
        }

    }
}

