using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BitacoraBE
    {
        private int cod_Usuario;

        public int Cod_Usuario
        {
            get { return cod_Usuario; }
            set { cod_Usuario = value; }
        }

        private string nombreUsuario;

        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set { nombreUsuario = value; }
        }

        private short cod_Evento;

        public short Cod_Evento
        {
            get { return cod_Evento; }
            set { cod_Evento = value; }
        }

        private string descripcionEvento;

        public string DescripcionEvento
        {
            get { return Enum.GetName(typeof(EventosBE.Eventos), Cod_Evento); }
            set { descripcionEvento = value; }
        }

        private DateTime fechaEvento;

        public DateTime FechaEvento
        {
            get { return fechaEvento; }
            set { fechaEvento = value; }
        }

        private short criticidad;

        public short Criticidad
        {
            get { return criticidad; }
            set { criticidad = value; }
        }

        private string criticidadTexto;

        public string CriticidadTexto
        {
            get { return Enum.GetName(typeof(EventosBE.Criticidad), Criticidad); }
            set { criticidadTexto = value; }
        }

        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
    }
}
