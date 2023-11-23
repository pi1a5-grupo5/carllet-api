using Domain.Entities;
using Domain.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Services
{
    public class EmailService : IEmailService
    {
        public void SendConfirmationEmail(User user)
        {
            string html = EmailConfirmationHtml(user);
            string to = user.Email;
            string subject = "Confirmação de Email - Carllet";
            SendEmail(html, to, subject);
        }

        public void SendResetPasswordEmail(User user)
        {
            string html = ResetPasswordHtml(user);
            string to = user.Email;
            string subject = "Redefinição de senha - Carllet";
            SendEmail(html, to, subject);
        }

        public void SendEmail(string html, string to, string subject)
        {

            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("app.carllet@gmail.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("app.carllet@gmail.com", "aogo dndu ixqf lipj");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public string EmailConfirmationHtml(User user)
        {
           return @"
           <!DOCTYPE html>
<html lang = 'en'>
<head>
    <meta charset = 'UTF-8'>
    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
    <style>
        body {
                font - family: 'Arial', sans - serif;
                background - color: #f4f4f4;
            margin: 0;
            padding: 0;
            }

        .container {
                max - width: 600px;
            margin: 0 auto;
            padding: 20px;
                background - color: #ffffff;
            border - radius: 8px;
                box - shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            }

            h1 {
            color: #333;
        }

            p {
            color: #555;
            line - height: 1.6;
            }

            a {
            display: inline - block;
            padding: 10px 20px;
                margin - top: 20px;
                text - decoration: none;
                background - color: #3498db;
            color: #ffffff;
            border - radius: 4px;
            transition: background - color 0.3s ease;
            }

        a: hover {
                background - color: #1b6ca8;
        }
    </style>
</head>
<body>
    <div class='container'>
        <h1>Confirme seu e-mail</h1>
        <p>Olá motorista,</p>
        <p> Para completar seu registro, por favor, confirme seu e - mail clicando no botão abaixo:</p>
        <a href = 'https://api.carllet.com/api/User/verificationToken/"+ user.VerificationToken.ToString() + @"' target = '_blank' > Confirmar E - mail </a>
        <p> Se o botão acima não funcionar, você também pode copiar e colar o seguinte link no seu navegador:</p>
        <p>https://api.carllet.com/api/User/verificationToken/" + user.VerificationToken.ToString() +  @" </p>
        <p> Obrigado! </p>
    </div>
</body>
</html>";

        }

        public string ResetPasswordHtml(User user)
        {
            return @"
           <!DOCTYPE html>
<html lang = 'en'>
<head>
    <meta charset = 'UTF-8'>
    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
    <style>
        body {
                font - family: 'Arial', sans - serif;
                background - color: #f4f4f4;
            margin: 0;
            padding: 0;
            }

        .container {
                max - width: 600px;
            margin: 0 auto;
            padding: 20px;
                background - color: #ffffff;
            border - radius: 8px;
                box - shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            }

            h1 {
            color: #333;
        }

            p {
            color: #555;
            line - height: 1.6;
            }

            a {
            display: inline - block;
            padding: 10px 20px;
                margin - top: 20px;
                text - decoration: none;
                background - color: #3498db;
            color: #ffffff;
            border - radius: 4px;
            transition: background - color 0.3s ease;
            }

        a: hover {
                background - color: #1b6ca8;
        }
    </style>
</head>
<body>
    <div class='container'>
        <h1>Troca de senha</h1>
        <p>Olá motorista,</p>
        <p> Para trocar sua senha, utilize o token abaixo:</p>
        <p>"+ user.ResetPasswordToken.ToString() + @" </p>
        <p> Obrigado! </p>
    </div>
</body>
</html>";

        }

    }
}
