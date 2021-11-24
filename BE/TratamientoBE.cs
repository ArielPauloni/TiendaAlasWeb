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

        private List<Tuple<TerapiaBE, short>> terapias;

        public List<Tuple<TerapiaBE, short>> Terapias
        {
            get { return terapias; }
            set { terapias = value; }
        }

        private short calificacion;

        public short Calificacion
        {
            get { return calificacion; }
            set { calificacion = value; }
        }

        public float ValorTotal
        {
            get
            {
                float valTotal = 0;
                foreach (Tuple<TerapiaBE, short> t in terapias)
                {
                    valTotal += t.Item1.Precio * t.Item2;
                }
                return valTotal;
            }
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
