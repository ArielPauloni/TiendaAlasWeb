﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL
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

    public class SinPermisosException : Exception
    {
        public SinPermisosException()
        {
        }
    }

    public class BackupException : Exception
    {
        public BackupException() { }

        public BackupException(string message) : base(message) { }
    }
}
