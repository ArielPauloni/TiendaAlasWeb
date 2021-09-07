using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SL
{
    public class ConfiguracionSL
    {
        private EncriptacionSL gestorEncriptacion = new EncriptacionSL();

        public void ConfigurarEnvioMails(ref string remitente, ref string mailAdmin, ref string pass, string palabraSecreta)
        {
            //Todas las variables de configuración (nombre y valor) están encriptadas en el app.config
            string NombreTagMailAdminEncriptado = gestorEncriptacion.SimpleEncrypt("MailAdmin");
            string MailAdminEncriptado = ConfigurationManager.AppSettings[NombreTagMailAdminEncriptado];
            string NombreTagRemitenteEncriptado = gestorEncriptacion.SimpleEncrypt("MailSender");
            string RemitenteEncriptado = ConfigurationManager.AppSettings[NombreTagRemitenteEncriptado];
            string NombreTagPalabraSecretaEncriptado = gestorEncriptacion.SimpleEncrypt("PalabraSecreta");
            string TagPalabraSecreta = ConfigurationManager.AppSettings[NombreTagPalabraSecretaEncriptado];
            string NombreTagPassEncriptado = gestorEncriptacion.SimpleEncrypt("EmailPassword");
            string PassEncriptado = ConfigurationManager.AppSettings[NombreTagPassEncriptado];

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (!String.IsNullOrEmpty(TagPalabraSecreta)) // Key existe
            {
                config.AppSettings.Settings[NombreTagPalabraSecretaEncriptado].Value = gestorEncriptacion.SimpleEncrypt(palabraSecreta);
                config.Save(ConfigurationSaveMode.Modified);
            }
            else // Key no existe
            {
                config.AppSettings.Settings.Add(NombreTagPalabraSecretaEncriptado, gestorEncriptacion.SimpleEncrypt(palabraSecreta));
                config.Save(ConfigurationSaveMode.Modified);
            }

            remitente = gestorEncriptacion.SimpleDecrypt(RemitenteEncriptado);
            mailAdmin = gestorEncriptacion.SimpleDecrypt(MailAdminEncriptado);
            pass = gestorEncriptacion.SimpleDecrypt(PassEncriptado);
        }
    }
}
