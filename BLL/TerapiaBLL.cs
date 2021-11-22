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
    public class TerapiaBLL
    {
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();

        public List<TerapiaBE> Listar()
        {
            TerapiaMapper m = new TerapiaMapper();
            return m.Listar();
        }

        public int Insertar(TerapiaBE terapia, UsuarioBE usuarioAutenticado)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Terapia"), usuarioAutenticado))
            {
                TerapiaMapper m = new TerapiaMapper();
                retVal = m.Insertar(terapia);
            }
            else { throw new SL.SinPermisosException(); }
            return retVal;
        }

        public int Actualizar(TerapiaBE terapia, UsuarioBE usuarioAutenticado)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Terapia"), usuarioAutenticado))
            {
                TerapiaMapper m = new TerapiaMapper();
                retVal = m.Actualizar(terapia);
            }
            else { throw new SL.SinPermisosException(); }
            return retVal;
        }

    }
}
