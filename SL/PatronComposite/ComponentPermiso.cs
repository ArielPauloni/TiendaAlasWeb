using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using BE;

namespace SL.PatronComposite
{
   public abstract class ComponentPermiso
    {
        protected PermisoBE permiso;

        public PermisoBE Permiso
        {
            get { return permiso; }
        }

        public ComponentPermiso(PermisoBE p)
        {
            permiso = p;
        }

        public abstract void Agregar(ComponentPermiso c);
        public abstract void Remover(ComponentPermiso c);
        public abstract void Mostrar(TreeNodeCollection n);

        public override string ToString()
        {
            return permiso.ToString() + " (" + permiso.CodPermiso.ToString() + ")";
        }
    }
}

