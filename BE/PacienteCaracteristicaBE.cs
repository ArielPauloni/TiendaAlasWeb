using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PacienteCaracteristicaBE
    {
        public enum GeneroEnum
        {
            Masculino,
            Femenino,
            Otro
        }

        private UsuarioBE paciente;

        public UsuarioBE Paciente
        {
            get { return paciente; }
            set { paciente = value; }
        }

        private Boolean fuma;

        public Boolean Fuma
        {
            get { return fuma; }
            set { fuma = value; }
        }
        
        public int Edad
        {
            get
            {
                var today = DateTime.Today;
                var edad = 0;
                if (paciente.FechaNacimiento.HasValue)
                {
                    edad = today.Year - paciente.FechaNacimiento.Value.Year;
                    // En caso de que sea año bisiesto, vuelvo al año en que nació
                    if (paciente.FechaNacimiento.Value.Date > today.AddYears(-edad)) edad--;
                }
                return edad;
            }
        }

        private short genero;

        public short Genero
        {
            get { return genero; }
            set { genero = value; }
        }

        private short diasActividadDeportiva;

        public short DiasActividadDeportiva
        {
            get { return diasActividadDeportiva; }
            set { diasActividadDeportiva = value; }
        }

        private short horasRelax;

        public short HorasRelax
        {
            get { return horasRelax; }
            set { horasRelax = value; }
        }
    }
}
