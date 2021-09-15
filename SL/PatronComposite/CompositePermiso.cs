using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using BE;

namespace SL.PatronComposite
{
    public class CompositePermiso : ComponentPermiso
    {

        private List<ComponentPermiso> _hijos = new List<ComponentPermiso>();

        public List<ComponentPermiso> Hijos
        {
            get { return _hijos; }
            set { _hijos = value; }
        }
        
        // Constructor
        public CompositePermiso(PermisoBE permiso)
          : base(permiso)
        {
        }

        public override void Agregar(ComponentPermiso componentPermiso)
        {
            _hijos.Add(componentPermiso);
        }

        public override void Remover(ComponentPermiso componentPermiso)
        {
            _hijos.Remove(componentPermiso);
        }

        public override void Mostrar(TreeNodeCollection nodo)
        {
            TreeNode nodoAux = new TreeNode()
            {
                Value = Permiso.CodPermiso.ToString(),
                Text = Permiso.DescripcionPermiso//,
                //NavigateUrl = "javascript:void(0)"
            };

            nodo.Add(nodoAux);
            int nivel = 0;

            foreach (ComponentPermiso p in _hijos.OrderBy(c => c.Permiso.DescripcionPermiso))
            {
                int indice = nodo[nivel].Parent != null ? nodo[nivel].Parent.ChildNodes.Count - 1 : nodo.Count - 1;
                p.Mostrar(nodo[indice].ChildNodes);
            }
            nivel++;
        }
    }
}
