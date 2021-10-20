using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using SL;

namespace BLL
{
    public class PatologiaBLL
    {
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();

        public List<PatologiaBE> Listar()
        {
            PatologiaMapper m = new PatologiaMapper();
            return m.Listar();
        }

        public int Insertar(PatologiaBE Patologia, UsuarioBE usuarioAutenticado)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Patologia"), usuarioAutenticado))
            {
                PatologiaMapper m = new PatologiaMapper();
                retVal = m.Insertar(Patologia);
            }
            else { throw new SL.SinPermisosException(); }
            return retVal;
        }

        public int Actualizar(PatologiaBE Patologia, UsuarioBE usuarioAutenticado)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Patologia"), usuarioAutenticado))
            {
                PatologiaMapper m = new PatologiaMapper();
                retVal = m.Actualizar(Patologia);
            }
            else { throw new SL.SinPermisosException(); }
            return retVal;
        }
    }
}
