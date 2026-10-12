using ChemAnalyst.Models;
using SpeedUpCoreAPIExample.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChemAnalyst.DAL
{

    public class SubscriptionDAL
    {
        private ChemAnalystContext _context = new ChemAnalystContext();

        public async Task<bool> AddSalesPackageTrial(SalesPackageSubscription subs)
        {
            int x = 0;
            try
            {
               // subs.CustNotificationFlag = 1; 
                subs.Status = "Active";
                _context.SalesPackageSubscription.Add(subs);
                x = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

            return x == 0 ? false : true;
        }

        public static bool SendMail(string FromName, string FromEmail, string ToEmail, string Title, string Content, string cc = "") 
        {
            bool retVal = false;
            SmtpClient smtpClient = new SmtpClient();
            //MailMessage message = new MailMessage();
            //var smtpDetails = "";

            //string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString(); ;
            //string SmtpPassword = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString(); ;
            //string SmtpHost = System.Configuration.ConfigurationManager.AppSettings["HostName"].ToString(); ;


            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "mail.techsciresearch.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential("no-reply@chemanalyst.com", "TEchsc%$@"),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage("no-reply@chemanalyst.com", ToEmail, Title, Content);
                MailAddress fromAddres = new MailAddress("no-reply@chemanalyst.com", "ChemAnalyst");
                //..............Add Bcc for KAran.................
                string mailToBcc = Convert.ToString(ConfigurationManager.AppSettings["mailToBcc"]);
                mailToBcc = mailToBcc.Replace(';', ',');
                string[] arr = mailToBcc.Trim().Trim(',').Split(',');
                foreach (var item in arr)
                {
                    if (!String.IsNullOrEmpty(item))
                        message.Bcc.Add(item);
                }
                message.From = fromAddres;
                if (!string.IsNullOrEmpty(cc))
                    message.CC.Add(cc);
                message.IsBodyHtml = true;
                smtp.Send(message);

                //MailAddress fromAddres = new MailAddress(SmtpUser, "ChemAnalyst");
                //smtpClient.Host = SmtpHost;
                //smtpClient.EnableSsl = true;
                //smtpClient.UseDefaultCredentials = true;
                //smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPassword);
                //smtpClient.Port = 587;
                //message.From = fromAddres;
                //message.To.Add(ToEmail);
                //message.CC.Add(cc);
                //message.Subject = Title;
                //message.IsBodyHtml = true;
                //message.Body = Content;
                //smtpClient.Send(message);
                retVal = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return retVal;
        }
        public static bool SendMailDeals(string FromName, string FromEmail, string ToEmail, string Title, string Content, string cc = "")
        {
            bool retVal = false;
            SmtpClient smtpClient = new SmtpClient();
            //MailMessage message = new MailMessage();
            //var smtpDetails = "";

            //string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString(); ;
            //string SmtpPassword = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString(); ;
            //string SmtpHost = System.Configuration.ConfigurationManager.AppSettings["HostName"].ToString(); ;
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "mail.techsciresearch.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential("no-reply@chemanalyst.com", "TEchsc%$@"),
                    Timeout = 30000,
                };

                MailMessage message = new MailMessage("no-reply@chemanalyst.com", ToEmail, Title, Content);
                MailAddress fromAddres = new MailAddress("no-reply@chemanalyst.com", "ChemAnalyst");
                
                message.From = fromAddres;
                if (!string.IsNullOrEmpty(cc))
                    message.CC.Add(cc);
                message.IsBodyHtml = true;
                smtp.Send(message);

                //MailAddress fromAddres = new MailAddress(SmtpUser, "ChemAnalyst");
                //smtpClient.Host = SmtpHost;
                //smtpClient.EnableSsl = true;
                //smtpClient.UseDefaultCredentials = true;
                //smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPassword);
                //smtpClient.Port = 587;
                //message.From = fromAddres;
                //message.To.Add(ToEmail);
                //message.CC.Add(cc);
                //message.Subject = Title;
                //message.IsBodyHtml = true;
                //message.Body = Content;
                //smtpClient.Send(message);
                retVal = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return retVal;
        }
        public static bool SendMailNewsLetter(string FromName, string FromEmail, string ToEmail, string Title, string Content, string cc = "")
        {
            bool retVal = false;
            SmtpClient smtpClient = new SmtpClient();
            //MailMessage message = new MailMessage();
            //var smtpDetails = "";

            //string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString(); ;
            //string SmtpPassword = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString(); ;
            //string SmtpHost = System.Configuration.ConfigurationManager.AppSettings["HostName"].ToString(); ;


            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "mail.chemanalystemail.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    
                    Credentials = new System.Net.NetworkCredential("no-reply@chemanalystemail.com", "TH@ytr%452#69MLp"),
                    Timeout = 80000,
                };

                MailMessage message = new MailMessage("no-reply@chemanalystemail.com", ToEmail, Title, Content);
                MailAddress fromAddres = new MailAddress("no-reply@chemanalystemail.com", "ChemAnalyst");
                message.From = fromAddres;
                if (!string.IsNullOrEmpty(cc))
                    message.CC.Add(cc);
                message.IsBodyHtml = true;
                smtp.Send(message);

                //MailAddress fromAddres = new MailAddress(SmtpUser, "ChemAnalyst");
                //smtpClient.Host = SmtpHost;
                //smtpClient.EnableSsl = true;
                //smtpClient.UseDefaultCredentials = true;
                //smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPassword);
                //smtpClient.Port = 587;
                //message.From = fromAddres;
                //message.To.Add(ToEmail);
                //message.CC.Add(cc);
                //message.Subject = Title;
                //message.IsBodyHtml = true;
                //message.Body = Content;
                //smtpClient.Send(message);
                retVal = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return retVal;
        }

        public static string GetHtml(string argTemplateDocument)
        {
            StreamReader sr = default(StreamReader);
            string Data = null;
            string _path = HttpContext.Current.Server.MapPath("~/Templates/") + argTemplateDocument;
            sr = File.OpenText(_path);
            Data = sr.ReadToEnd();
            return Data;
        }
    }
}