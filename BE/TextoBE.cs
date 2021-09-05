using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TextoBE
    {
        private int idFrase;

        public int IdFrase
        {
            get { return idFrase; }
            set { idFrase = value; }
        }

        private string texto;

        public string Texto
        {
            get { return texto; }
            set { texto = value; }
        }
    }
}
