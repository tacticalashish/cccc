using EntityLayer;
using EntityLayer.CustomEntity;
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
    public class dlTicketComment
    {
        private enTicketComment _enTicketComment = null;

        public dlTicketComment(enTicketComment enTicketComment_)
        {
            this._enTicketComment = enTicketComment_;
        }


        public void Create()
        {
            SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_SA_TicketComment_CREATE]", new object[] {
                                                            _enTicketComment.TicketFID,
                                                            _enTicketComment.UserFID,
                                                            _enTicketComment.ManagerFID,
                                                            _enTicketComment.FileName,
                                                            _enTicketComment.Comment,
                                                            DateTime.Now,
                                                            _enTicketComment.Status
                                                            });
        }


        public List<enTicketComment> ReadAll()
        {
            List<enTicketComment> listOfTicketComment = new List<enTicketComment>();
            using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_tbl_SA_TicketComment_READ]", new object[] {
                                                            _enTicketComment.TicketFID
                                                            }))
            {
                while (idr.Read())
                {
                    enTicketComment objENenTicketComment = new enTicketComment();
                    ConstructObject(idr, objENenTicketComment);
                    objENenTicketComment.CustomerName = Convert.ToString(idr["CustomerName"]);
                    objENenTicketComment.ManagerName = Convert.ToString(idr["ManagerName"]);
                    listOfTicketComment.Add(objENenTicketComment);
                }
            }


            return listOfTicketComment;
        }
        private void ConstructObject(IDataReader idr, enTicketComment enTicketComment_)
        {
            enTicketComment_.ID = Convert.ToInt32(idr["ID"]);
            enTicketComment_.TicketFID = Convert.ToInt32(idr["TicketFID"]);
            enTicketComment_.FileName = DBNull.Value == idr["FileName"] ? null : Convert.ToString(idr["FileName"]);
            enTicketComment_.UserFID = DBNull.Value == idr["UserFID"] ? (int?)null : Convert.ToInt32(idr["UserFID"]);
            enTicketComment_.ManagerFID = DBNull.Value == idr["ManagerFID"] ? (int?)null : Convert.ToInt32(idr["ManagerFID"]);
            enTicketComment_.Comment = DBNull.Value == idr["Comment"] ? null : Convert.ToString(idr["Comment"]);
            enTicketComment_.CreatedDate = Convert.ToDateTime(idr["CreatedDate"]);
        }
    }
}
