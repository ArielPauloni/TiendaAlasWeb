using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using BE;

namespace GUI
{
    public partial class Contact : Page, IObserver
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();

        public void ChequearPermisos() { }

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                btnEnviar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 62);
                ViewState["OperacionExitosa"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 45);
                ViewState["NoGrabarDatosVacios"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 58);
                ViewState["IngreseSumaCorrecta"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 70);
                lblNombre.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 63) + " " +
                    gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 64);
                lblEmpresa.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 65);
                lblTelefono.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 66);
                lblMail.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 67);
                lblMensaje.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 68);
                lblTitle.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 69);
            }
            Random rnd = new Random();
            lblNumeroA.Text = rnd.Next(1, 9).ToString();
            lblNumeroB.Text = rnd.Next(1, 9).ToString();
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

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(txtNombre.Text)) && (!string.IsNullOrWhiteSpace(txtEmpresa.Text)) &&
                (!string.IsNullOrWhiteSpace(txtMail.Text)) && (!string.IsNullOrWhiteSpace(txtMensaje.Text)) &&
                (!string.IsNullOrWhiteSpace(txtTelefono.Text)))
            {
                int resultadoReal = int.Parse(lblNumeroA.Text) + int.Parse(lblNumeroB.Text);
                if ((!string.IsNullOrWhiteSpace(txtResultado.Text)) && (resultadoReal == int.Parse(txtResultado.Text)))
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Info, ViewState["OperacionExitosa"].ToString() + @"<br>//TODO: PENDIENTE");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                else
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["IngreseSumaCorrecta"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
            }
            else
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoGrabarDatosVacios"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }
    }
}