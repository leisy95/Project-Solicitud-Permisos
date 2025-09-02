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

/*using System.Net;
using System.Net.Mail;

namespace PermisosApi.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarCorreoAsync(string destino, string asunto, string cuerpo)
        {
            var smtpConfig = _config.GetSection("Smtp");

            // Lee las credenciales desde variables de entorno
            var smtpUser = Environment.GetEnvironmentVariable("SMTP_USER");
            var smtpPass = Environment.GetEnvironmentVariable("SMTP_PASS");

            if (string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPass))
                throw new InvalidOperationException("Las credenciales SMTP no están definidas en el entorno.");

            using var cliente = new SmtpClient(smtpConfig["Host"], int.Parse(smtpConfig["Port"]))
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = bool.Parse(smtpConfig["EnableSsl"])
            };

            var mensaje = new MailMessage(smtpUser, destino, asunto, cuerpo);
            await cliente.SendMailAsync(mensaje);
        }
    }
}
*/

using System.Net;
using System.Net.Mail;

namespace PermisosApi.Services
{
    public class EmailService
    {
        public async Task EnviarCorreoAsync(string destino, string asunto, string cuerpo)
        {
            // Lee todas las configuraciones desde variables de entorno
            var smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST") ?? "smtp.gmail.com";
            var smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");
            var smtpUser = Environment.GetEnvironmentVariable("SMTP_USER")
                           ?? throw new InvalidOperationException("SMTP_USER no definido");
            var smtpPass = Environment.GetEnvironmentVariable("SMTP_PASS")
                           ?? throw new InvalidOperationException("SMTP_PASS no definido");
            var enableSsl = true; // Gmail siempre usa SSL

            using var cliente = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = enableSsl
            };

            var mensaje = new MailMessage(smtpUser, destino, asunto, cuerpo)
            {
                IsBodyHtml = true // Para poder enviar HTML
            };

            await cliente.SendMailAsync(mensaje);
        }
    }
}