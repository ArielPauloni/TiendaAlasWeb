using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class UsuarioBE
    {
        private int cod_Usuario;

        [SkipProperty]
        public int Cod_Usuario
        {
            get { return cod_Usuario; }
            set { cod_Usuario = value; }
        }

        private string apellido;

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string alias;

        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }

        private string contraseña;

        [SkipProperty]
        public string Contraseña
        {
            get { return contraseña; }
            set { contraseña = value; }
        }

        private TipoUsuarioBE tipoUsuario;

        public TipoUsuarioBE TipoUsuario
        {
            get { return tipoUsuario; }
            set { tipoUsuario = value; }
        }

        private List<PermisoBE> permisos;

        [SkipProperty]
        public List<PermisoBE> Permisos
        {
            get { return permisos; }
            set { permisos = value; }
        }

        private Int64 dvh;

        [SkipProperty]
        public Int64 DVH
        {
            get { return dvh; }
            set { dvh = value; }
        }

        private IdiomaBE idioma;

        [SkipProperty]
        public IdiomaBE Idioma
        {
            get { return idioma; }
            set { idioma = value; }
        }

        private string telefono;

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        private string mail;

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        private DateTime? fechaNacimiento;

        [SkipProperty]
        public DateTime? FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }

        private Boolean inactivo;

        [SkipProperty]
        public Boolean Inactivo
        {
            get { return inactivo; }
            set { inactivo = value; }
        }

        private short intentosEquivocados;

        [SkipProperty]
        public short IntentosEquivocados
        {
            get { return intentosEquivocados; }
            set { intentosEquivocados = value; }
        }

        private DateTime? ultimoLogin;

        [SkipProperty]
        public DateTime? UltimoLogin
        {
            get { return ultimoLogin; }
            set { ultimoLogin = value; }
        }

        private Bitmap fotoPerfil;

        [SkipProperty]
        public Bitmap FotoPerfil
        {
            get { return fotoPerfil; }
            set { fotoPerfil = value; }
        }

        public override string ToString()
        {
            if (Cod_Usuario > 0) { return Apellido + ", " + Nombre + " (" + Alias + ")"; }
            else return string.Empty;
        }
    }
}
