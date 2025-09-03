//using System.Net;
//using System.Net.Mail;
//using Microsoft.Extensions.Configuration;

//namespace PermisosApi.Services
//{
//    public class EmailService
//    {
//        private readonly IConfiguration _config;

//        public EmailService(IConfiguration config)
//        {
//            _config = config;
//        }

//        public async Task EnviarCorreoAsync(string destino, string asunto, string cuerpo)
//        {
//            var smtpConfig = _config.GetSection("Smtp");

//            using var cliente = new SmtpClient(smtpConfig["Host"], int.Parse(smtpConfig["Port"]))
//            {
//                Credentials = new NetworkCredential(smtpConfig["User"], smtpConfig["Password"]),
//                EnableSsl = bool.Parse(smtpConfig["EnableSsl"])
//            };

//            var mensaje = new MailMessage(smtpConfig["User"], destino, asunto, cuerpo);
//            await cliente.SendMailAsync(mensaje);
//        }
//    }
//}

using SendGrid;
using SendGrid.Helpers.Mail;

namespace PermisosApi.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> EnviarCorreoAsync(string destino, string asunto, string cuerpo)
        {
            try
            {
                var apiKey = _config["SendGrid__ApiKey"];
                var fromEmail = _config["SendGrid__FromEmail"];
                var fromName = _config["SendGrid__FromName"];

                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(fromEmail, fromName);
                var to = new EmailAddress(destino);
                var msg = MailHelper.CreateSingleEmail(from, to, asunto, cuerpo, $"<p>{cuerpo}</p>");
                var response = await client.SendEmailAsync(msg);

                Console.WriteLine($"SendGrid Status: {response.StatusCode}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
                return false;
            }
        }
    }
}