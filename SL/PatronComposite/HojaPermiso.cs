using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using BE;

namespace SL.PatronComposite
{
    public class HojaPermiso : ComponentPermiso
    {
        public HojaPermiso(PermisoBE permiso)
          : base(permiso)
        {
        }

        public override void Agregar(ComponentPermiso componentePermiso)
        {
            //no se puede agregar si es hoja
        }


        public override void Remover(ComponentPermiso componentePermiso)
        {
            //no se puede remover si es hoja
        }


        public override void Mostrar(TreeNodeCollection nodo)
        {
            TreeNode treeNode = new TreeNode()
            {
                Value = Permiso.CodPermiso.ToString(),
                Text = Permiso.DescripcionPermiso,
                NavigateUrl = "javascript:void(0)"
                //Tag = Permiso.DescripcionPermiso
            };
            nodo.Add(treeNode);
        }
    }
}
