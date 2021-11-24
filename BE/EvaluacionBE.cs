using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class EvaluacionBE
    {
        private PacienteBE paciente;

        public PacienteBE Paciente
        {
            get { return paciente; }
            set { paciente = value; }
        }

        private TratamientoBE tratamiento;

        public TratamientoBE Tratamiento
        {
            get { return tratamiento; }
            set { tratamiento = value; }
        }

        private short gradoSatisfaccion;

        public short GradoSatisfaccion
        {
            get { return gradoSatisfaccion; }
            set { gradoSatisfaccion = value; }
        }

        private PacienteCaracteristicaBE pacienteCaracteristicas;

        public PacienteCaracteristicaBE PacienteCaracteristicas
        {
            get { return pacienteCaracteristicas; }
            set { pacienteCaracteristicas = value; }
        }

    }
}
