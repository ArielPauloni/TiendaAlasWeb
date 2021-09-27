using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using BLL;
using BE;

namespace GUI
{
    public partial class Bienvenido : System.Web.UI.Page, IObserver
    {
        private UsuarioBLL gestorUsuario = new UsuarioBLL();
        private EncriptacionSL gestorEncriptacion = new EncriptacionSL();

        public void TraducirTexto()
        {
        }

        public void ChequearPermisos()
        { }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //IniciaSesionUsuario();
                //InicializarUsuarios();

                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();
            }

            if ((Request.UrlReferrer != null) &&(Request.UrlReferrer.AbsolutePath.Contains("")) &&
                (Session["UsuarioCreado"] != null) && ((Boolean)Session["UsuarioCreado"]))
            {
                UC_MensajeModal.SetearMensaje("-Modifique sus datos o solicite a un administrador que los actualice");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                Session["UsuarioCreado"] = false;
            }
        }

        //private void IniciaSesionUsuario()
        //{
        //    //Session["UsuarioAutenticado"]
        //    UsuarioBE usuarioAutenticado = new UsuarioBE();
        //    PermisoBE permiso = new PermisoBE();
        //    permiso.CodPermiso = 1;
        //    permiso.DescripcionPermiso = "Crear Usuario";
        //    List<PermisoBE> permisos = new List<PermisoBE>();
        //    permisos.Add(permiso);
        //    usuarioAutenticado.Permisos = new List<PermisoBE>();
        //    usuarioAutenticado.Permisos = permisos;
        //    Session["UsuarioAutenticado"] = usuarioAutenticado;
        //}

        //protected void btnAlerta_Click(object sender, EventArgs e)
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alertaShow()", true);
        //}

        private void InicializarUsuarios()
        {
            //Doy de alta un par de usuarios:
            //UsuarioBE usuarioAdmin = new UsuarioBE();
            //usuarioAdmin.TipoUsuario = new TipoUsuarioBE();
            //usuarioAdmin.Apellido = "D'Agostino";
            //usuarioAdmin.Nombre = "Nadia";
            //usuarioAdmin.Alias = "NadiaAlas";
            //usuarioAdmin.Contraseña = gestorEncriptacion.SimpleEncrypt("Admin123");
            //usuarioAdmin.TipoUsuario.Cod_Tipo = 1;
            //int new_i = gestorUsuario.Insertar(usuarioAdmin, (UsuarioBE)Session["UsuarioAutenticado"]);

            UsuarioBE usuario = new UsuarioBE();
            usuario.TipoUsuario = new TipoUsuarioBE();
            usuario.Apellido = "Pellegrino";
            usuario.Nombre = "Luciana";
            usuario.Alias = "Luchita";
            usuario.Contraseña = gestorEncriptacion.SimpleEncrypt("Profesional123");
            usuario.TipoUsuario.Cod_Tipo = 3;
            int i = gestorUsuario.Insertar(usuario, (UsuarioBE)Session["UsuarioAutenticado"]);

            UsuarioBE usuario1 = new UsuarioBE();
            usuario1.TipoUsuario = new TipoUsuarioBE();
            usuario1.Apellido = "Perez";
            usuario1.Nombre = "Juan";
            usuario1.Alias = "Paciente1";
            usuario1.Contraseña = gestorEncriptacion.SimpleEncrypt("Paciente123");
            usuario1.TipoUsuario.Cod_Tipo = 2;
            int j = gestorUsuario.Insertar(usuario1, (UsuarioBE)Session["UsuarioAutenticado"]);

            UsuarioBE usuario2 = new UsuarioBE();
            usuario2.TipoUsuario = new TipoUsuarioBE();
            usuario2.Apellido = "Gomez";
            usuario2.Nombre = "Martin";
            usuario2.Alias = "Paciente2";
            usuario2.Contraseña = gestorEncriptacion.SimpleEncrypt("Paciente123");
            usuario2.TipoUsuario.Cod_Tipo = 2;
            int k = gestorUsuario.Insertar(usuario2, (UsuarioBE)Session["UsuarioAutenticado"]);
        }
    }
}