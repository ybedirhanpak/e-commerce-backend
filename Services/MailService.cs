using System;
using System.Linq;
using e_commerce_api.Helpers;
using e_commerce_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace e_commerce_api.Services
{
    public interface IMailService
    {
        void sendEmail(Email email);
    }

    public class MailService : IMailService
    {
         IAppSettings appSettings ;
        public MailService(IOptions<AppSettings> settings)
        {
            appSettings  = settings.Value;
        }

        public void sendEmail(Email email)
        {
            SmtpClient sc = new SmtpClient();
            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;
            sc.UseDefaultCredentials = false;
            sc.Credentials = new NetworkCredential(appSettings.Email, appSettings.EmailPassword);
            sc.Timeout = 20000;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(appSettings.Email);
            mail.To.Add(appSettings.Email);
            mail.Subject = "Message From a guest - Email: " + email.senderEmail;
            mail.IsBodyHtml = true;
            mail.Body = email.mailContent;

            sc.Send(mail);
        }
    }
}
