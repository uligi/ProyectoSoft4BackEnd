using System;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Negocio.Repositories
{
    public interface IRecursosRepository
    {
        string GenerarClave();
        string ConvertirSha256(string texto);
        Task<bool> EnviarCorreo(string correo, string asunto, string mensaje);
        Task<bool> EnviarCorreoConAdjunto(string correo, string asunto, string mensaje, string archivoAdjunto);
    }

    public class RecursosRepository : IRecursosRepository
    {
        // Método para generar una clave aleatoria
        public string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }

        // Método para encriptar texto en SHA256
        public string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        // Método para enviar un correo simple
        public async Task<bool> EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("iamxhimx@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                using (var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("iamxhimx@gmail.com", "cpfy afmt emuk xcfv"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                })
                {
                    await smtp.SendMailAsync(mail);
                }

                resultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando correo: {ex.Message}");
                resultado = false;
            }

            return resultado;
        }

        // Método para enviar un correo con un archivo adjunto
        public async Task<bool> EnviarCorreoConAdjunto(string correo, string asunto, string mensaje, string archivoAdjunto)
        {
            bool resultado = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("iamxhimx@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                if (!string.IsNullOrEmpty(archivoAdjunto))
                {
                    Attachment attachment = new Attachment(archivoAdjunto);
                    mail.Attachments.Add(attachment);
                }

                using (var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("iamxhimx@gmail.com", "cpfy afmt emuk xcfv"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                })
                {
                    await smtp.SendMailAsync(mail);
                }

                resultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando correo: {ex.Message}");
                resultado = false;
            }

            return resultado;
        }
    }
}
