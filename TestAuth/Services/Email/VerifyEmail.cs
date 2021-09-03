using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAuth.Models;

namespace TestAuth.Services.Email
{
    public class VerifyEmail: IEmailService
    {
        public async Task<bool> Send(string tokenLink, string email)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("TestEmail", "testmail212345@gmail.com"));
                message.To.Add(new MailboxAddress("Подтверждение аккаунта", email));
                message.Subject = "Подтверждение аккаунта";
                message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    //  tokenLink
                    Text = "Здравствуйте, вы зарегистрировались на моем сайте и для того, чтобы Вы могли пользоваться аккаунтом на сайте, пожалуйста, подтвердите Ваш аккаунт по ссылке:" + "\n" + $"<a href='{tokenLink}'>Ссылка</a>"
                };
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("testmail212345@gmail.com", "fdsfnrjn/.frjrnefnSDNFR");

                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    return  true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
