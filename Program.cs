using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mail;
using System.Net;


namespace e_commerce_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;

            sc.Credentials = new NetworkCredential("teknomarkt2019@gmail.com", "teknomarkt.2019!");

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("teknomarkt@gmail.com");

            mail.To.Add("teknomarkt2019@gmail.com");

            mail.Subject = "E-Posta Konusu"; mail.IsBodyHtml = true; mail.Body="E-Posta İçeriği" ;

            sc.Send(mail);


            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
