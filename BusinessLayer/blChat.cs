using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = DataLayer.dlChat;
namespace BusinessLayer
{
   public class blChat
    {
        private enChat _enobj = null;
        private DAL _objDAL = null;

        public enChat GetEntityReference
        {
            get { return this._enobj; }
        }
        public blChat(enChat enobj_)
        {
            this._enobj = enobj_;
        }

        public List<enChat> InsertChatdata(enChat obj)
        {
            return GetDALReference().InsertChatdata(obj);
        }



        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enobj);
            }
            return _objDAL;
        }
    }
}
