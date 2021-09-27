using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using BE;
using BLL;

namespace GUI.Servicios.Usuarios
{
    public partial class ABMUsuario : System.Web.UI.Page, IObserver
    {
        private UsuarioBLL gestorUsuario = new UsuarioBLL();

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }

        }

        public void TraducirTexto()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                EnlazarGrillaUsuarios();
            }
        }

        private void EnlazarGrillaUsuarios()
        {
            grvUsuarios.DataSource = gestorUsuario.Listar();
            grvUsuarios.DataBind();
        }

        protected void grvUsuarios_PageIndexChanging(object sender, EventArgs e)
        {

        }

        protected void btnCrearNuevoUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"~/Negocio/Usuario/NuevoUsuario.aspx");
        }
    }
}