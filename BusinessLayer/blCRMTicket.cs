using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = DataLayer.dlCRMTicket;
namespace BusinessLayer
{
    public class blCRMTicket
    {
        private enCRMTicket _enCRMTicket = null;
        private DAL _objDAL = null;
        public enCRMTicket GetEntityReference
        {
            get { return this._enCRMTicket; }
        }


        public blCRMTicket(enCRMTicket enCRMTicket_)
        {
            this._enCRMTicket = enCRMTicket_;
        }

        public List<enCRMTicket> GetCRMDetails(int id)
        {
            return GetDALReference().GetCRMDetails(id);
        }
        public List<enCRMTicket> GetProductList()
        {
            return GetDALReference().GetProductList();
        }

        public List<enCRMTicket> insertCRMTicket(enCRMTicket enCRMTicket)
        {
            return GetDALReference().insertCRMTicket(enCRMTicket);
        }

        public List<enCRMTicket> CRMTicketViewRatingList(enCRMTicket enCRMTicket)
        {
            return GetDALReference().CRMTicketViewRatingList(enCRMTicket);
        }

        public List<enCRMTicket> CRMTicketViewRatingListFormanager(enCRMTicket enCRMTicket)
        {
            return GetDALReference().CRMTicketViewRatingListFormanager(enCRMTicket);
        }
        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enCRMTicket);
            }
            return _objDAL;
        }

    }
}
