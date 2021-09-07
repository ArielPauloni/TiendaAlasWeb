using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace SL
{
    public class LoginSL
    {
        EncriptacionSL gestorEncriptacion = new EncriptacionSL();
        AutorizacionSL gestorAutorizacion = new AutorizacionSL();
        UsuarioBE usuarioAutenticado = new UsuarioBE();
        private static Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public UsuarioBE ObtenerUsuarioAutenticado(UsuarioBE usuario)
        {
            UsuarioMapper m = new UsuarioMapper();
            usuario.Contraseña = gestorEncriptacion.SimpleEncrypt(usuario.Contraseña);
            try
            {
                usuarioAutenticado = m.ObtenerUsuarioLogin(usuario);
                if (usuarioAutenticado != null)
                {
                    //SesionSL.Instancia.Login(usuarioAutenticado);
                    gestorAutorizacion.CargarPermisosAlUsuario(ref usuarioAutenticado);
                }
            }
            catch (DAL.UsuarioModificadoException ex) { throw new SL.UsuarioModificadoException(ex.Message); }
            return usuarioAutenticado;
        }
        
        private static string RandomString(int largoCadena)
        {
            return new string(Enumerable.Repeat(chars, largoCadena)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string ObtenerRandomString(int largoCadena)
        {
            return RandomString(largoCadena);
        }
    }
}
