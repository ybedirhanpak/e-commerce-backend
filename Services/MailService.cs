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
//Imports
using e_commerce_api.Models;
using MongoDB.Bson;

//MongoDB
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Text;

namespace e_commerce_api.Services
{
    public interface IMailService
    {
        void sendEmail(Email email);
        void resetPasswordMail(Email email,User mailInDb);
    }

    public class MailService : IMailService
    {
        IAppSettings appSettings;
        private readonly IUserService _userService;

        public MailService(IOptions<AppSettings> settings, IUserService userService)
        {
            _userService = userService;
            appSettings = settings.Value;
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

        public void resetPasswordMail(Email email,User user)
        {
            var newPassword = GeneratePassword(8);

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
            mail.To.Add(email.senderEmail);
            mail.Subject = "Reset Password !  " + appSettings.Email;
            mail.IsBodyHtml = true;
            mail.Body = "<h2>Üyelik Bilgileriniz Aşağıdaki Gibi Güncellenmiştir. </h2><br/>"+"Email: "+ user.Email+"<br/>" +"Yeni Şifreniz: " + newPassword;
            _userService.Update(user, newPassword);
            sc.Send(mail);
        }

        public string GeneratePassword (int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
