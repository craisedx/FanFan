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
        public async Task SendEmailDefault(string email, string subject, string message)
        {
           
            
            MimeMessage emalmessage = new MimeMessage();
            emalmessage.From.Add(new MailboxAddress("FanFan - Сервис с фанфиками", "fanfanconfim@fanfan.com"));
            emalmessage.To.Add(new MailboxAddress("", email));
            emalmessage.Subject = subject;
            emalmessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
                {

                await client.ConnectAsync("smtp.gmail.com", 587);

                await client.AuthenticateAsync("fanfanconfim@gmail.com", "stbnkqraincqwfet");
                await  client.SendAsync(emalmessage);

                await client.DisconnectAsync(true);
               

                }
            
            
            
        }
    }
}
