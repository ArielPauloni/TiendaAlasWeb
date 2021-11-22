using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class TerapiaBE
    {
        private int cod_Terapia;

        public int Cod_Terapia
        {
            get { return cod_Terapia; }
            set { cod_Terapia = value; }
        }

        private string descripcionTerapia;

        public string DescripcionTerapia
        {
            get { return descripcionTerapia; }
            set { descripcionTerapia = value; }
        }

        private int duracion;

        public int Duracion
        {
            get { return duracion; }
            set { duracion = value; }
        }

        private float precio;

        public float Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        private Boolean activo;

        public Boolean Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        public override string ToString()
        {
            if (Cod_Terapia > 0) { return DescripcionTerapia + " (" + Duracion.ToString() + "')"; }
            else return string.Empty;
        }
    }
}
