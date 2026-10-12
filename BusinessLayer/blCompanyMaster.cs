using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = DataLayer.dlCompanyMaster;

namespace BusinessLayer
{
    public class blCompanyMaster
    {
        private enCompanyMaster _enCompanyMaster = null;
        private DAL _objDAL = null;

        public enCompanyMaster GetEntityReference
        {
            get { return this._enCompanyMaster; }
        }

        public blCompanyMaster(enCompanyMaster enCompanyMaster_)
        {
            this._enCompanyMaster = enCompanyMaster_;
        }

        public void Create()
        {
            GetDALReference().Create();
        }

        public void Update()
        {
            GetDALReference().Update();
        }

        public List<enCompanyMaster> ReadAll()
        {
            return GetDALReference().ReadAll();
        }

       

        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enCompanyMaster);
            }
            return _objDAL;
        }
    }
}
