using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace SL
{
    public class MailSL
    {
        public bool EnviarMail(string remitente, string destinatario, string PalabraSecreta, string fromPassword)
        {
            bool retVal = true;
            try
            {
                var fromAddress = new MailAddress(remitente, "Sistema Tienda Alada");
                var toAddress = new MailAddress(destinatario, "SuperAdmin Tienda Alada");
                string subject = "(Trabajo de Diploma 2020) Clave Secreta";
               
                string body = string.Format("La clave secreta para poder acceder al restore de la base de datos es <b>{0}</b>", PalabraSecreta);

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Timeout = 100000,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception)
            {
                retVal = false;
            }
            return retVal;
        }
    }
}
