using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PatologiaBE
    {
        private int cod_Patologia;

        public int Cod_Patologia
        {
            get { return cod_Patologia; }
            set { cod_Patologia = value; }
        }

        private string descripcionPatologia;

        public string DescripcionPatologia
        {
            get { return descripcionPatologia; }
            set { descripcionPatologia = value; }
        }
        
        public override string ToString()
        {
            if (Cod_Patologia > 0) { return DescripcionPatologia; }
            else return string.Empty;
        }
    }
}
