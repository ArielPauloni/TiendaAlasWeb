using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using BE;
using SL;

namespace GUI.Servicios.Seguridad
{
    public partial class Integridad : System.Web.UI.Page, IObserver
    {
        private IntegridadSL gestorIntegridad = new IntegridadSL();
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private IdiomaSL gestorIdioma = new IdiomaSL();

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                ViewState["MensajeOk"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 45);
                ViewState["ContacteAdministrador"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 44);
                btnChequearIntegridad.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 43);
                lblIntegridad.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 46);
            }
        }

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }
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

        protected void btnCheckIntegridad_Click(object sender, EventArgs e)
        {
            try
            {
                gestorIntegridad.ChequearIntegridad();
                gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.ChequeoIntegridadExitoso, (short)EventosBE.Criticidad.Baja);
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Info, ViewState["MensajeOk"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
            catch (SL.UsuarioModificadoException ex)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Error, ex.Message + "<br>" + ViewState["ContacteAdministrador"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.ErrorEnIntegridadUsuario, (short)EventosBE.Criticidad.MuyAlta);
            }
        }
    }
}