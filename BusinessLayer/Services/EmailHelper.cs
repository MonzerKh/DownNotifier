using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ModelsLayer.Business;
using ModelsLayer.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmailHelper  //: IMailService
    {
        private readonly AppSettings _appSettings;

        public EmailHelper(IOptions<AppSettings> appSetting)
        {
            this._appSettings = appSetting.Value;
        }
       
        public MailResponse Send(string Title, string Message, string to)
        {

            string MailPort = _appSettings.MailPort;
            string Mailfrom = _appSettings.MailFrom;
            string MailUser = _appSettings.MailUser;
            string MailPwd = _appSettings.MailPwd;
            string MailHost = _appSettings.MailHost;
            string MailDisplayName = _appSettings.MailDisplayName;

            SmtpClient smtp;
            MailMessage message;
            Attachment Attachment = null;

            try
            {
                smtp = new SmtpClient(MailHost, Convert.ToInt32(MailPort));
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(MailUser, MailPwd);
                message = new MailMessage();
                message.From = new MailAddress(Mailfrom, MailDisplayName);
                message.To.Add(new MailAddress(to));
                message.Subject = Title;
                message.SubjectEncoding = System.Text.Encoding.Default;
                message.IsBodyHtml = true;
                message.Body = Message;


                smtp.Send(message);

                message.Dispose();
                if (Attachment != null)
                {
                    Attachment.Dispose();
                }

                return new MailResponse()
                {
                    Result = true,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new MailResponse()
                {
                    Result = false,
                    Message = ex.Message
                };
            }
          
        }

      
       

      
     
    }
}
