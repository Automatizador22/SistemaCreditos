using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SistemaCreditos.Application.Interfaces.ExternalServices;

namespace SistemaCreditos.Infrastructure.ExternalServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarEmailAsync(string destinatario, string asunto, string cuerpoHtml)
        {
            var email = new MimeMessage();

            // 1. Configuramos quién envía
            email.From.Add(new MailboxAddress(
                _config["EmailSettings:SenderName"],
                _config["EmailSettings:SenderEmail"]));

            // 2. Configuramos quién recibe
            email.To.Add(MailboxAddress.Parse(destinatario));
            email.Subject = asunto;

            // 3. Armamos el cuerpo del correo
            var builder = new BodyBuilder { HtmlBody = cuerpoHtml };
            email.Body = builder.ToMessageBody();

            // 4. Nos conectamos y enviamos usando MailKit
            using var smtp = new SmtpClient();

            // Nos conectamos al servidor SMTP
            await smtp.ConnectAsync(
                _config["EmailSettings:SmtpServer"],
                int.Parse(_config["EmailSettings:Port"]),
                SecureSocketOptions.StartTls);

            // Autenticacion
            await smtp.AuthenticateAsync(
                _config["EmailSettings:SenderEmail"],
                _config["EmailSettings:Password"]);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}