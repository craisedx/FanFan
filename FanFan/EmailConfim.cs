using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan
{
    public class EmailConfim
    {
        public EmailConfim()
        {
           
        }
        public async Task    SendEmailDefault(string email)
        {
           
            
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("FanFan - Сервис с фанфиками", "confim@fanfan.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Подтверждение аккаунта";
                message.Body = new BodyBuilder() { HtmlBody = "<h1>Нажмите на ссылку ниже для подтвержединия регистрации</h1> <a href=\"/vk.com/\">Нажми</a>" }.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, true);
                    await client.AuthenticateAsync("fanfanconfim@gmail.com", "ycN-VmS-RPh-RUj");
                    await client.SendAsync(message);

                    await client.DisconnectAsync(true);
               

                }
            
            
            
        }
    }
}
