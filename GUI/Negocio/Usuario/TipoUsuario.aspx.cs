using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using BE;
using BLL;
using SL;

namespace GUI.Negocio.Usuario
{
    public partial class TipoUsuario : System.Web.UI.Page, IObserver
    {
        private TipoUsuarioBLL gestorTipoUsuario = new TipoUsuarioBLL();
        private IdiomaSL gestorIdioma = new IdiomaSL();

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

            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTipoUsuario.Text))
            {
                TipoUsuarioBE tipoUsuario = new TipoUsuarioBE();
                tipoUsuario.Descripcion_Tipo = txtTipoUsuario.Text;
                int r = gestorTipoUsuario.Insertar(tipoUsuario, (UsuarioBE)Session["UsuarioAutenticado"]);
                if ((r == 0) || (r == -1))
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, "-noPudoGrabar");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                else if (r == -2)
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, "-sinPermisosText");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                else
                {
                    txtTipoUsuario.Text = string.Empty;
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Info, "-operacionExitosaText");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
            }
            else
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, "-datosIncorrectosText");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }

        }

    }
}