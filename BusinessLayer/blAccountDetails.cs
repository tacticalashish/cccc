using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = DataLayer.dlAccountDetails;

namespace BusinessLayer
{
   public class blAccountDetails
    {
        private enAccountDetails _enAccountDetails = null;
        private DAL _objDAL = null;

        public enAccountDetails GetEntityReference
        {
            get { return this._enAccountDetails; }
        }
        public blAccountDetails(enAccountDetails enAccountDetails_)
        {
            this._enAccountDetails = enAccountDetails_;
        }


        public List<enAccountDetails> SaveUserAccountdetails(enAccountDetails enAccountDetails)
        {
            return GetDALReference().SaveUserAccountdetails(enAccountDetails);
        }

        public List<enAccountDetails> GetFreelUserAccountDetails(enAccountDetails enAccountDetails)
        {
            return GetDALReference().GetFreelUserAccountDetails(enAccountDetails);
        }

        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enAccountDetails);
            }
            return _objDAL;
        }
    }

}

