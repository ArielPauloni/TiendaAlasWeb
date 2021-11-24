using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioModificadoException : Exception
    {
        public UsuarioModificadoException() { }

        public UsuarioModificadoException(string message) : base(message) { }
    }

    public class UsuarioBloqueadoException : Exception
    {
        public UsuarioBloqueadoException() { }

        public UsuarioBloqueadoException(string message) : base(message) { }
    }

    public class PatologiaNoEvaluadaException : Exception
    {
        public PatologiaNoEvaluadaException() { }

        public PatologiaNoEvaluadaException(string message) : base(message) { }
    }

    public class PatologiaMalEvaluadaException : Exception
    {
        public PatologiaMalEvaluadaException() { }

        public PatologiaMalEvaluadaException(string message) : base(message) { }
    }

    public class PacienteSinCoincidenciasSuficientesException : Exception
    {
        public PacienteSinCoincidenciasSuficientesException() { }

        public PacienteSinCoincidenciasSuficientesException(string message) : base(message) { }
    }
}
