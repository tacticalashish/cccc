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
   public class dlEmailActivity
    {
        private enEmailActivity _enEmailActivity = null;

        public dlEmailActivity(enEmailActivity enEmailActivity_)
        {
            this._enEmailActivity = enEmailActivity_;
        }
        public List<enEmailActivity> InsertEmailRecord(enEmailActivity obj)
        {
            List<enEmailActivity> data = new List<enEmailActivity>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_InsertEmailSendReceivedRecord]", new object[] {
                                                              obj.Subject,
                                                              obj.Sender,
                                                              obj.Recipient,
                                                              obj.Body,
                                                              obj.DateReceived,
                                                              obj.SentDate,
                                                              obj.CC_EMAil,
                                                              obj.BCC_EMail,
                                                              obj.UserEmail,
                                                              obj.attachmentFilePaths
                                                            }))
                {
                    while (idr.Read())
                    {
                        //enAdminChemicalPrice objENLeads = new enAdminChemicalPrice();
                        //ConstructObjectForTrigger(idr, objENLeads);
                        //data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public List<enEmailActivity> GetEmail(enEmailActivity obj)
        {
            List<enEmailActivity> data = new List<enEmailActivity>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_Getemail]", new object[] {
                                                      
                                                            }))
                {
                    while (idr.Read())
                    {
                        enEmailActivity objENLeads = new enEmailActivity();
                        ConstructObjectForEmail(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public List<enEmailActivity> EmailRecord()
        {
            List<enEmailActivity> data = new List<enEmailActivity>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetUserEmail]", new object[] {

                                                            }))
                {
                    while (idr.Read())
                    {
                        enEmailActivity objENLeads = new enEmailActivity();
                        ConstructObjectForEmail(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        
        public List<enEmailActivity> GetUserEmail(enEmailActivity obj)
        {
            List<enEmailActivity> data = new List<enEmailActivity>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_Getemail]", new object[] {
                                                                obj.searchKeyword,
                                                                obj.StartRowNumber,
                                                                obj.EndRowNumber
                                                            }))
                {
                    while (idr.Read())
                    {
                        enEmailActivity objENLeads = new enEmailActivity();
                        ConstructObjectForEmail(idr, objENLeads);
                        data.Add(objENLeads);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public List<enEmailActivity> GetUserEmailHistory(enSearchFilter Userdata)
        {
            List<enEmailActivity> listOfData = new List<enEmailActivity>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_GetUser_EmailHistory]", new object[] {
                                                               Userdata.Email,
                                                                Userdata.searchKeyword,
                                                                Userdata.StartRowNumber,
                                                                Userdata.EndRowNumber,
                                                            }))
                {
                    while (idr.Read())
                    {
                        enEmailActivity objENChemicalPrice = new enEmailActivity();
                        ConstructObjectListForUserChatEmails(idr, objENChemicalPrice);
                        listOfData.Add(objENChemicalPrice);
                    }
                }

                return listOfData;
            }
            catch (Exception ex)
            {

            }
            return listOfData;
        }
        public List<enEmailActivity> GetUserEmailsForDropDown(enEmailActivity obj)
        {
            List<enEmailActivity> listOfData = new List<enEmailActivity>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[USP_GetUSerEmailDropDown]", new object[] {
                                                              obj.UserType,
                                                              obj.Email
                                                            }))
                {
                    while (idr.Read())
                    {
                        enEmailActivity objENChemicalPrice = new enEmailActivity();
                        ConstructObjectListForUserEmails(idr, objENChemicalPrice);
                        listOfData.Add(objENChemicalPrice);
                    }
                }

                return listOfData;
            }
            catch (Exception ex)
            {

            }
            return listOfData;
        }

        private void ConstructObjectListForUserEmails(IDataReader idr, enEmailActivity enUser_)
        {
            //enUser_.ID = DBNull.Value == idr["Id"] ? 0 : Convert.ToInt32(idr["Id"]);
            enUser_.Email = DBNull.Value == idr["email"] ? null : Convert.ToString(idr["email"]);
         
        }

        private void ConstructObjectListForUserChatEmails(IDataReader idr, enEmailActivity enUser_)
        {
            enUser_.RowNumber = DBNull.Value == idr["RowNumber"] ? 0 : Convert.ToInt32(idr["RowNumber"]);
            enUser_.RecordsCount = DBNull.Value == idr["RecordsCount"] ? 0 : Convert.ToInt32(idr["RecordsCount"]);
            enUser_.ID = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enUser_.Subject = DBNull.Value == idr["Subject"] ? null : Convert.ToString(idr["Subject"]);
            enUser_.Sender = DBNull.Value == idr["Sender"] ? null : Convert.ToString(idr["Sender"]);
            enUser_.Recipient = DBNull.Value == idr["Recipient"] ? null : Convert.ToString(idr["Recipient"]);

            enUser_.Body = DBNull.Value == idr["Body"] ? null : Convert.ToString(idr["Body"]);
            enUser_.DateReceived = DBNull.Value == idr["DateReceived"] ? null : Convert.ToString(idr["DateReceived"]);
            enUser_.SentDate = DBNull.Value == idr["SentDate"] ? null : Convert.ToString(idr["SentDate"]);

            enUser_.CC_EMAil = DBNull.Value == idr["CC_Email"] ? null : Convert.ToString(idr["CC_Email"]);
            enUser_.BCC_EMail = DBNull.Value == idr["BCC_Email"] ? null : Convert.ToString(idr["BCC_Email"]);

            enUser_.CreatedDate = DBNull.Value == idr["CreatedDate"] ? null : Convert.ToString(idr["CreatedDate"]);
            enUser_.attachmentFilePaths = DBNull.Value == idr["AttachmentFilePath"] ? null : Convert.ToString(idr["AttachmentFilePath"]);
            enUser_.UserEmail = DBNull.Value == idr["UserEmail"] ? null : Convert.ToString(idr["UserEmail"]);
        }

        private void ConstructObjectForEmail(IDataReader idr, enEmailActivity enUser_)
        {
         
            enUser_.Email = DBNull.Value == idr["Email"] ? null : Convert.ToString(idr["Email"]);
            enUser_.Password = DBNull.Value == idr["Password"] ? null : Convert.ToString(idr["Password"]);
            
        }

    }
}
