using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TratamientoBE
    {
        private int cod_Tratamiento;

        public int Cod_Tratamiento
        {
            get { return cod_Tratamiento; }
            set { cod_Tratamiento = value; }
        }

        private string descripcionTratamiento;

        public string DescripcionTratamiento
        {
            get { return descripcionTratamiento; }
            set { descripcionTratamiento = value; }
        }

        private Boolean activo;

        public Boolean Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        public override string ToString()
        {
            if (Cod_Tratamiento > 0) { return DescripcionTratamiento; }
            else return string.Empty;
        }
    }
}
