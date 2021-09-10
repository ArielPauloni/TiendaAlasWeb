using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL;
using BE;

namespace GUI.Servicios.Permisos
{
    public partial class Permisos : System.Web.UI.Page
    {
        private PermisoSL gestorPermiso = new PermisoSL();

        protected void Page_Load(object sender, EventArgs e)
        {
            EnlazarArbolPermisos();
        }

        private void EnlazarArbolPermisos()
        {
            UsuarioBE usuarioAutenticado = new UsuarioBE();

            List<SL.PatronComposite.ComponentPermiso> components = gestorPermiso.ListarPermisosArbol(usuarioAutenticado);
            if (components != null)
            {
                trvPermisos.Nodes.Clear();

                foreach (SL.PatronComposite.ComponentPermiso component in components)
                {
                    component.Mostrar(trvPermisos.Nodes);
                }
                trvPermisos.ExpandAll();
            }
            else
            {
                //Sin permisos:

            }
        }
    }
}