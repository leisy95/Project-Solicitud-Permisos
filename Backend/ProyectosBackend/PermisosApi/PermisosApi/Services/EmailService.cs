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

using System.Net.Mail;
using System.Net;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpo)
    {
        var host = _config["SMTP_HOST"] ?? "smtp.gmail.com";
        var port = int.Parse(_config["SMTP_PORT"] ?? "587");
        var user = _config["SMTP_USER"];
        var pass = _config["SMTP_PASS"];

        using var client = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(user, pass),
            EnableSsl = true
        };

        var mensaje = new MailMessage(user, destinatario, asunto, cuerpo);

        try
        {
            await client.SendMailAsync(mensaje);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error enviando correo: {ex.Message}");
            throw;
        }
    }
}