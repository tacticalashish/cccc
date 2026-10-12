using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using DAL = DataLayer.dlTicketComment;
using EntityLayer.CustomEntity;

namespace BusinessLayer
{
   public class blTicketComment
    {
        private enTicketComment _enTicketComment = null;
        private DAL _objDAL = null;

        public enTicketComment GetEntityReference
        {
            get { return this._enTicketComment; }
        }

        public blTicketComment(enTicketComment enTicketComment_)
        {
            this._enTicketComment = enTicketComment_;
        }

        public void Create()
        {
            GetDALReference().Create();
        }

        public List<enTicketComment> ReadAll()
        {
            return GetDALReference().ReadAll();
        }

        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enTicketComment);
            }
            return _objDAL;
        }
    }
}

