using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using DAL = DataLayer.dlTicket;
using EntityLayer.CustomEntity;

namespace BusinessLayer
{
   public class blTicket
    {
        private enTicket _enTicket = null;
        private DAL _objDAL = null;

        public enTicket GetEntityReference
        {
            get { return this._enTicket; }
        }

        public blTicket(enTicket enTicket_)
        {
            this._enTicket = enTicket_;
        }

        public void Create()
        {
            GetDALReference().Create();
        }

        public void Update()
        {
            GetDALReference().Update();
        }

        public void UpdateStatus()
        {
            GetDALReference().UpdateStatus();
        }

        public void UpdateNotification()
        {
            GetDALReference().UpdateNotification();
        }

        
        public void CloseTicket()
        {
            GetDALReference().CloseTicket();
        }

        public void Read()
        {
            GetDALReference().Read();
        }

        public List<enTicket> ReadBasedOnSearchFilter(enSearchFilter model)
        {
            return GetDALReference().ReadBasedOnSearchFilter(model);
        }

        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enTicket);
            }
            return _objDAL;
        }

        
    }
}

