using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using BE;

namespace SL
{
    public class MailSL
    {
        public bool EnviarMailRecuperoPass(string remitenteMail, string remitentePass, UsuarioBE usuarioDestinatario, string NuevaPass)
        {
            bool retVal = true;
            try
            {
                var fromAddress = new MailAddress(remitenteMail, "Sistema Tienda Alas");
                var toAddress = new MailAddress(usuarioDestinatario.Mail, usuarioDestinatario.ToString());
                string subject = "(Trabajo Final de Ingeniería 2021) Nueva Pass";
               
                string body = string.Format("La nueva clave para su usuario <b>{0}</b> es: <b>{1}</b>", usuarioDestinatario.Alias, NuevaPass);

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Timeout = 100000,
                    Credentials = new NetworkCredential(fromAddress.Address, remitentePass)
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
            catch (Exception ex)
            {
                retVal = false;
            }
            return retVal;
        }
    }
}
