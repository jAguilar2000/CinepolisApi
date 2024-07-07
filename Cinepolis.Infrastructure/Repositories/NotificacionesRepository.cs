using System.Net;
using System.Net.Mail;

namespace Cinepolis.Infrastructure.Repositories
{
    public class NotificacionesRepository
    {
        public Task EnviarCorreo(string subject, string body, bool isHtml, string mailTo)
        {
            var credentials = new NetworkCredential("jjaguilar@uth.hn", "P10");
            var mail = new MailMessage()
            {
                From = new MailAddress("jjaguilar@uth.hn", "La Económica | Notificaciones."),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            mail.To.Add(mailTo);

            var client = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = credentials
            };
            client.Send(mail);
            return Task.CompletedTask;
        }
    }
}
