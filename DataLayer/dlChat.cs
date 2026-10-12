using EntityLayer;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataLayer
{
    public class dlChat
    {
        private enChat _enobj = null;

        public dlChat(enChat enobj_)
        {
            this._enobj = enobj_;
        }


        public List<enChat> InsertChatdata(enChat enobj)
        {
            List<enChat> listofdata = new List<enChat>();

            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertChatLog]", new object[] {
                                                            enobj.Userid,
                                                            enobj.Question,
                                                            enobj.Reply,

            }))
            {
                while (idr.Read())
                {
                    enChat objENobj = new enChat();
                    Constructinsertdata(idr, enobj);
                    listofdata.Add(objENobj);
                }
            }

            return listofdata;
        }
        private void Constructinsertdata(IDataReader idr, enChat objENSalesPackageSubscriptions)
        {

            objENSalesPackageSubscriptions.Reply = DBNull.Value == idr["Result"] ? null : Convert.ToString(idr["Result"]);
        }
    }
}
