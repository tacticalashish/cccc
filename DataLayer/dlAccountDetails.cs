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
   public class dlAccountDetails
    {
        public string Message = "";
        private enAccountDetails _enAccountDetails = null;

        public dlAccountDetails(enAccountDetails enAccountDetails_)
        {
            this._enAccountDetails = enAccountDetails_;
        }

        public List<enAccountDetails> SaveUserAccountdetails(enAccountDetails enAccountDetails)
        {
            List<enAccountDetails> ListofSaveUserAccountdetails = new List<enAccountDetails>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_InsertFreelAccountDetails]", new object[] {
                                             enAccountDetails.AccountHolderName,
                                             enAccountDetails.AccountNumber,
                                             enAccountDetails.BankName,
                                             enAccountDetails.BankAddress,
                                             enAccountDetails.AccountHolderAddress,
                                             enAccountDetails.IBAN,
                                             enAccountDetails.SWIFTcode,
                                             enAccountDetails.BICcode,
                                             enAccountDetails.BSBcode,
                                             enAccountDetails.Transitcode,
                                             enAccountDetails.Routingnumber,
                                             enAccountDetails.Identification,
                                             enAccountDetails.Email,

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAccountDetails objENChemicalPrice = new enAccountDetails();
                        ConstructObjectSaveUserAccountdetails(idr, objENChemicalPrice);
                        ListofSaveUserAccountdetails.Add(objENChemicalPrice);
                    }
                }

                return ListofSaveUserAccountdetails;
            }
            catch (Exception ex)
            {

            }
            return ListofSaveUserAccountdetails;
        }


        public List<enAccountDetails> GetFreelUserAccountDetails(enAccountDetails enAccountDetails)
        {
            List<enAccountDetails> ListofSaveUserAccountdetails = new List<enAccountDetails>();
            try
            {
                using (IDataReader idr = SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[Usp_GetFreelUserAccountDetails]", new object[] {

                                                                                   enAccountDetails.Email

                                                            }))
                {
                    while (idr.Read())
                    {
                        enAccountDetails objENChemicalPrice = new enAccountDetails();
                        ConstructObjectGetFreelUserAccountDetails(idr, objENChemicalPrice);
                        ListofSaveUserAccountdetails.Add(objENChemicalPrice);
                    }
                }

                return ListofSaveUserAccountdetails;
            }
            catch (Exception ex)
            {

            }
            return ListofSaveUserAccountdetails;
        }

        private void ConstructObjectSaveUserAccountdetails(IDataReader idr, enAccountDetails enAccountDetails_)
        {

            enAccountDetails_.ValidationMessage = Convert.ToString(idr["Message"]);

        }

        private void ConstructObjectGetFreelUserAccountDetails(IDataReader idr, enAccountDetails enAccountDetails_)
        {
            //enAccountDetails_.Id = DBNull.Value == idr["ID"] ? 0 : Convert.ToInt32(idr["ID"]);
            enAccountDetails_.AccountHolderName = DBNull.Value == idr["AccountHolderName"] ? null : Convert.ToString(idr["AccountHolderName"]);
            enAccountDetails_.AccountNumber = DBNull.Value == idr["AccountNumber"] ? null : Convert.ToString(idr["AccountNumber"]);
            enAccountDetails_.BankName = DBNull.Value == idr["BankName"] ? null : Convert.ToString(idr["BankName"]);
            enAccountDetails_.BankAddress = DBNull.Value == idr["BankAddress"] ? null : Convert.ToString(idr["BankAddress"]);
            enAccountDetails_.AccountHolderAddress = DBNull.Value == idr["AccountHolderAddress"] ? null : Convert.ToString(idr["AccountHolderAddress"]);
            enAccountDetails_.IBAN = DBNull.Value == idr["IBAN"] ? null : Convert.ToString(idr["IBAN"]);
            enAccountDetails_.SWIFTcode = DBNull.Value == idr["SWIFTcode"] ? null : Convert.ToString(idr["SWIFTcode"]);
            enAccountDetails_.BICcode = DBNull.Value == idr["BICcode"] ? null : Convert.ToString(idr["BICcode"]);
            enAccountDetails_.BSBcode = DBNull.Value == idr["BSBcode"] ? null : Convert.ToString(idr["BSBcode"]);
            enAccountDetails_.Transitcode = DBNull.Value == idr["Transitcode"] ? null : Convert.ToString(idr["Transitcode"]);
            enAccountDetails_.Routingnumber = DBNull.Value == idr["Routingnumber"] ? null : Convert.ToString(idr["Routingnumber"]);
            enAccountDetails_.Identification = DBNull.Value == idr["Identification"] ? null : Convert.ToString(idr["Identification"]);

        }
    }

}

