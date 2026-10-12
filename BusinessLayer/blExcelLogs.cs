using EntityLayer;
using EntityLayer.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = DataLayer.dlExcelLogs;

namespace BusinessLayer
{
    public class blExcelLogs
    {
        private enExcelLogs _enExcelLogs = null;
        private DAL _objDAL = null;

        public enExcelLogs GetEntityReference
        {
            get { return this._enExcelLogs; }
        }

        public blExcelLogs(enExcelLogs enExcelLogs_)
        {
            this._enExcelLogs = enExcelLogs_;
        }

        public List<enExcelLogs> ReportBasedOnSearchFilter(enSearchFilter searchFilter)
        {
            return GetDALReference().ReportBasedOnSearchFilter(searchFilter);
        }

        public void Create()
        {
            GetDALReference().Create();
        }

        private DAL GetDALReference()
        {
            if (_objDAL == null)
            {
                _objDAL = new DAL(this._enExcelLogs);
            }
            return _objDAL;
        }
    }
}
