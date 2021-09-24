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
                usuarioAutenticado = m.ObtenerUsuarioPorAlias(usuario);
                if (usuarioAutenticado != null)
                {
                    if (usuarioAutenticado.IntentosEquivocados > 2) { throw new SL.UsuarioBloqueadoException(); }
                    //Chequeo la contraseña:
                    if (string.Compare(usuario.Contraseña, usuarioAutenticado.Contraseña, false) == 0)
                    {
                        //Contraseña Correcta: Limpios IntentosEquivocados, actualizo UltimoLogin
                        usuarioAutenticado.IntentosEquivocados = 0;
                        usuarioAutenticado.UltimoLogin = DateTime.Now;
                        gestorAutorizacion.CargarPermisosAlUsuario(ref usuarioAutenticado);
                        m.Editar(usuarioAutenticado);
                    }
                    else
                    {
                        //Contraseña incorrecta, sumo IntentosEquivocados
                        usuarioAutenticado.IntentosEquivocados++;
                        m.Editar(usuarioAutenticado);
                        usuarioAutenticado = null;
                    }
                }
            }
            catch (DAL.UsuarioModificadoException ex) { throw new SL.UsuarioModificadoException(ex.Message); }
            catch (DAL.UsuarioBloqueadoException) { throw new SL.UsuarioBloqueadoException(); }
            return usuarioAutenticado;
        }

        public int ActualizarPassUsuario(UsuarioBE usuario)
        {
            int retVal = 0;

            UsuarioMapper m = new UsuarioMapper();
            retVal = m.Editar(usuario);

            return retVal;
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
