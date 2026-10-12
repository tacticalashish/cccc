using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = DataLayer.dlEmailActivity;
namespace BusinessLayer
{
   public class blEmailActivity
    {
        private enEmailActivity _enEmailActivity = null;
        private DAL _objDAL = null;

        public enEmailActivity GetEntityReference
        {
            get { return this._enEmailActivity; }
        }
        public blEmailActivity(enEmailActivity enEmailActivity_)
        {
            this._enEmailActivity = enEmailActivity_;
        }
        public List<enEmailActivity> InsertEmailRecord(enEmailActivity obj)
        {
            return GetDALReference().InsertEmailRecord(obj);
        }
        public List<enEmailActivity> EmailRecord()
        {
            return GetDALReference().EmailRecord();
        }
        
        public List<enEmailActivity> GetEmail(enEmailActivity obj)
        {
            return GetDALReference().GetEmail(obj);
        }
        public List<enEmailActivity> GetUserEmail(enEmailActivity obj)
        {
            return GetDALReference().GetUserEmail(obj);
        }
        public List<enEmailActivity> GetUserEmailHistory(enSearchFilter obj)
        {
            return GetDALReference().GetUserEmailHistory(obj);
        }
        public List<enEmailActivity> GetUserEmailsForDropDown(enEmailActivity obj)
        {
            return GetDALReference().GetUserEmailsForDropDown(obj);
        }
        

        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enEmailActivity);
            }
            return _objDAL;
        }
    }
}
