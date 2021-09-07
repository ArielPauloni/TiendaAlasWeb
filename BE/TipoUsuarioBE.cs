using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TipoUsuarioBE
    {
        private int codTipo;

        public int Cod_Tipo
        {
            get { return codTipo; }
            set { codTipo = value; }
        }

        private string descripcionTipo;

        public string Descripcion_Tipo
        {
            get { return descripcionTipo; }
            set { descripcionTipo = value; }
        }

        public override string ToString()
        {
            return Descripcion_Tipo;
        }

    }
}
