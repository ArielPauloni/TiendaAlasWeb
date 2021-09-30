using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL;
using BE;

namespace GUI.User_Controls
{
    public partial class UC_MensajeModal : System.Web.UI.UserControl
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();

        protected void Page_Load(object sender, EventArgs e)
        {
            btnConfirmar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 28);
        }

        public void SetearMensaje(TipoMensajeBE.Tipo tipo, string Mensaje)
        {
            lblMensaje.Text = Mensaje;

            switch (tipo)
            {
                case TipoMensajeBE.Tipo.Info:
                    divModal.Attributes.Add("style", "border-style:solid; border-width:thick; border-color:lightblue;");
                    MensajeModalTitle.Attributes.Add("class", "modal-title fa fa-info-circle");
                    MensajeModalTitle.Attributes.Add("style", "color: black; font-weight:bold;");
                    MensajeModalTitle.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 52);
                    divMensaje.Attributes.Add("class", "modal-body bg-info");
                    break;
                case TipoMensajeBE.Tipo.Alerta:
                    divModal.Attributes.Add("style", "border-style:solid; border-width:thick; border-color:orange;");
                    MensajeModalTitle.Attributes.Add("class", "modal-title fa fa-exclamation-circle");
                    MensajeModalTitle.Attributes.Add("style", "color: orange; font-weight:bold;");
                    MensajeModalTitle.InnerText = " " +  gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 53);
                    divMensaje.Attributes.Add("class", "modal-body bg-warning");
                    break;
                case TipoMensajeBE.Tipo.Error:
                    divModal.Attributes.Add("style", "border-style:solid; border-width:thick; border-color:red;");
                    MensajeModalTitle.Attributes.Add("class", "modal-title fa fa-exclamation-triangle");
                    MensajeModalTitle.Attributes.Add("style", "color: red; font-weight:bold;");
                    MensajeModalTitle.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 1);
                    divMensaje.Attributes.Add("class", "modal-body bg-danger");
                    break;
            }
        }
    }
}