using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PetsAPI.Services
{
    public class EmailService
    {
        private readonly SmtpClient smtp;

        public EmailService()
        {
            this.smtp = new SmtpClient();
            this.smtp.Host = "smtp.gmail.com";
            this.smtp.Port = 587;
            this.smtp.Credentials = new NetworkCredential("cyclepath.app123@gmail.com", "Admin123*");
            this.smtp.EnableSsl = true;
        }

        public void SendnEmail(string email, string body)
        {
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(email, "Respuesta a la solicitud de adopcion"));
            message.From = (new MailAddress("cyclepath.app123@gmail.com"));
            message.Body = body;
            message.IsBodyHtml = false;
            message.Subject = "Respuesta a la solicitud de adopcion";
            this.smtp.Send(message);
        }
    }
}
