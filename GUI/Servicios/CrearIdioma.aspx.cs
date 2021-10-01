using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using BE;
using System.Threading.Tasks;

namespace GUI.Servicios
{
    public partial class CrearIdioma : System.Web.UI.Page, IObserver
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();
        private IdiomaBE idiomaCreado = new IdiomaBE();
        private BitacoraSL gestorBitacora = new BitacoraSL();

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }

        }

        public void TraducirTexto()
        {
            btnGuardar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 19);
            btnCancelar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 20);
            lblCodIdioma.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 22);
            lblDescripcionIdioma.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 23);
            lblTraducir.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 24);
            ViewState["OperacionExitosa"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 45);
            ViewState["CantidadFrases"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 54);
            ViewState["DatosIncorrectos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 55);
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"Idioma.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(txtCodIdioma.Text) && (!string.IsNullOrWhiteSpace(txtDescripcionIdioma.Text))))
            {
                idiomaCreado = new IdiomaBE();
                idiomaCreado.CodIdioma = txtCodIdioma.Text;
                idiomaCreado.DescripcionIdioma = txtDescripcionIdioma.Text;
                gestorIdioma.Insertar(idiomaCreado);
                if (chkTraducir.Checked)
                {
                    try
                    {
                        IdiomaBE español = new IdiomaBE
                        {
                            CodIdioma = "es",
                            DescripcionIdioma = "Español",
                            IdIdioma = 1
                        };
                        int frasesTraducidas = gestorIdioma.TraducirIdiomaCompleto(español, idiomaCreado);

                        if (frasesTraducidas > 0)
                        {
                            gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.CreaciónDeIdioma, (short)EventosBE.Criticidad.Baja);

                            UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Info, ViewState["OperacionExitosa"].ToString() + "<br>" +
                                                          ViewState["CantidadFrases"].ToString() + ": " + frasesTraducidas.ToString());
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                        }
                        else
                        {
                            UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["DatosIncorrectos"].ToString());
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Error, ViewState["ErrorMsg"].ToString() + "<br>" + ex.Message);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                }
            }
            else
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["DatosIncorrectos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }
    }
}