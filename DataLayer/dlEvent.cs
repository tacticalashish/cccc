using EntityLayer;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataLayer
{
     public class dlEvent
     {
        private enEvent _enEvent = null;

        public dlEvent(enEvent enEvent_)
        {
            this._enEvent = enEvent_;
        }

        

          public List<enEvent> InsertEventdata(enEvent enEvent)
            {
                List<enEvent> listofenEvent = new List<enEvent>();
                try
                {
                    using (SqlHelper.ExecuteReader(ApplicationSettings.DefaultConnectionString, "[dbo].[usp_InsertEventdata]", new object[] {
                                                                    enEvent.Name,
                                                                    enEvent.LastName,
                                                                    enEvent.Company,
                                                                     enEvent.Designation,
                                                                    enEvent.Location,
                                                                    enEvent.Email,
                                                                    enEvent.Mobile,
                                                                    enEvent.Question1,
                                                                    enEvent.Answer1,
                                                                    enEvent.Question2,
                                                                    enEvent.Answer2,
                                                                      enEvent.Tittle


                    }))
                    {
                        return listofenEvent;
                    }

                }
                catch (Exception ex)
                {
                }
                return listofenEvent;
          }


     }
}
