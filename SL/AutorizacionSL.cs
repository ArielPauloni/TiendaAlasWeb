using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace SL
{
    public class AutorizacionSL
    {
        public int CargarPermisosAlUsuario(ref UsuarioBE usuario)
        {
            PermisoMapper m = new PermisoMapper();
            return m.CargarPermisosAlUsuario(ref usuario);           
        }

        public bool ValidarPermisoUsuario(PermisoBE permiso, UsuarioBE usuario)
        {
            bool retVal = false;
            foreach (PermisoBE per in usuario.Permisos)
            {
                if (string.Compare(per.DescripcionPermiso, permiso.DescripcionPermiso,true) == 0)
                {
                    retVal = true;
                    break;
                }
            }
            return retVal;
        }
    }
}
