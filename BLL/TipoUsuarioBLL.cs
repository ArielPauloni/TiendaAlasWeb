using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using SL;

namespace BLL
{
    public class TipoUsuarioBLL
    {
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();

        public List<TipoUsuarioBE> Listar()
        {
            TipoUsuarioMapper m = new TipoUsuarioMapper();
            return m.Listar();
        }

        public int Insertar(TipoUsuarioBE tipoUsuario, UsuarioBE usuario)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear TipoUsuario"), usuario))
            {
                TipoUsuarioMapper m = new TipoUsuarioMapper();
                retVal = m.Insertar(tipoUsuario);
            }
            else { retVal = -2; }
            return retVal;
        }
    }
}
