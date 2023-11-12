using Domain.Entities;
using Domain.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailService : IEmailService
    {
        public void SendConfirmationEmail(User user)
        {
            string html = $"<a>Confirme seu e-mail através do link com o Token {user.VerificationToken}</a>";
            string to = user.Email;
            string subject = "Confirmação de Email - Carllet";
            SendEmail(html, to, subject);
        }
         
        public void SendResetPasswordEmail(User user)
        {
            string html = $"<p>Seu Token para redefinição de senha é{user.ResetPasswordToken}</p>" +
                $"<p>Este link será valido pelos proximos 15 minutos, se não usado neste periodo, será necessario solicitar novamente.</p>";
            string to = user.Email;
            string subject = "Redefinição de senha - Carllet";
            SendEmail(html, to, subject);
        }

        public void SendEmail(string html, string to, string subject) {

            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("app.carllet@gmail.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("app.carllet@gmail.com", "aogo dndu ixqf lipj");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
