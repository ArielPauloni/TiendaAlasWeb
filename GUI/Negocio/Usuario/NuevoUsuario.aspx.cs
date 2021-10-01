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
using System.Text.RegularExpressions;

namespace GUI.Negocio.Usuario
{
    public partial class NuevoUsuario : System.Web.UI.Page, IObserver
    {
        private TipoUsuarioBLL gestorTipoUsuario = new TipoUsuarioBLL();
        private EncriptacionSL gestorEncriptacion = new EncriptacionSL();
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private UsuarioBLL gestorUsuario = new UsuarioBLL();
        private IdiomaSL gestorIdioma = new IdiomaSL();
        private string contieneNumero = @"[0-9]+";
        private string contieneMayuscula = @"[A-Z]+";
        private string contieneSeisCaracteres = @".{6,}";
        private string eMailPattern = @"^[\w._%-]+@[\w.-]+\.[a-zA-Z]{2,4}$";

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }
        }

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                ViewState["DatosGrabadosOk"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 12);
                ViewState["MailIncorrecto"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 47);
                ViewState["DatosIncorrectos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 55);
                ViewState["SinPermisos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 57);
                ViewState["SinDatosVacios"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 58);
                ViewState["NoSePudoGrabar"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 59);
                ViewState["ContraseniasNoCoinciden"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 60);
                ViewState["ContraseniaPatron"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 61);
                lblNombre.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 63);
                lblApellido.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 64);
                lblTelefono.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 66);
                lblMail.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 67);
                lblAlias.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 73);
                lblPass1.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 74);
                lblPass2.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 75);
                lblTipoUsuario.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 51);
                lblFechaNacimiento.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 76);
                btnGuardar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 19);
                btnCancelar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 20);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                ddlTipoUsuario.DataSource = gestorTipoUsuario.Listar();
                ddlTipoUsuario.DataTextField = "Descripcion_Tipo";
                ddlTipoUsuario.DataValueField = "Cod_Tipo";
                ddlTipoUsuario.DataBind();
                TipoUsuarioBE tipoUsuario = new TipoUsuarioBE
                {
                    Descripcion_Tipo = ddlTipoUsuario.SelectedItem.Text.ToString(),
                    Cod_Tipo = short.Parse(ddlTipoUsuario.SelectedItem.Value)
                };

                txtFechaNacimiento.Text = "2000-01-01";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(txtApellido.Text)) && (!string.IsNullOrWhiteSpace(txtNombre.Text)) &&
                (!string.IsNullOrWhiteSpace(txtPass1.Text)) && (!string.IsNullOrWhiteSpace(txtPass2.Text)) &&
                (!string.IsNullOrWhiteSpace(txtAlias.Text)) && (ddlTipoUsuario.SelectedIndex > -1))
            {
                if (string.Compare(txtPass1.Text, txtPass2.Text, false) != 0)
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["ContraseniasNoCoinciden"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                else if ((!Regex.IsMatch(txtPass1.Text, contieneNumero)) ||
                         (!Regex.IsMatch(txtPass1.Text, contieneMayuscula)) ||
                         (!Regex.IsMatch(txtPass1.Text, contieneSeisCaracteres)))
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["DatosIncorrectos"].ToString() + ": " +
                                                  ViewState["ContraseniaPatron"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                else
                {
                    Boolean ret = false;
                    if (!Regex.IsMatch(txtMail.Text, eMailPattern))
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["MailIncorrecto"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                        ret = true;
                    }
                    if (!ret)
                    {
                        UsuarioBE usuario = new UsuarioBE();

                        usuario.Apellido = txtApellido.Text;
                        usuario.Nombre = txtNombre.Text;
                        usuario.Alias = txtAlias.Text;
                        usuario.Contraseña = gestorEncriptacion.SimpleEncrypt(txtPass1.Text);
                        usuario.Telefono = txtTelefono.Text;
                        usuario.Mail = txtMail.Text;
                        try
                        {
                            if (!string.IsNullOrWhiteSpace(txtFechaNacimiento.Text))
                            { usuario.FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text); }
                            else { usuario.FechaNacimiento = default(DateTime?); }
                        }
                        catch (Exception) { usuario.FechaNacimiento = default(DateTime?); }
                        TipoUsuarioBE tipoUsuario = new TipoUsuarioBE
                        {
                            Descripcion_Tipo = ddlTipoUsuario.SelectedItem.Text.ToString(),
                            Cod_Tipo = short.Parse(ddlTipoUsuario.SelectedItem.Value)
                        };
                        usuario.TipoUsuario = tipoUsuario;
                        try
                        {
                            int r = gestorUsuario.Insertar(usuario, (UsuarioBE)Session["UsuarioAutenticado"]);
                            if (r == 0)
                            {
                                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                            }
                            else
                            {
                                gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.AltaDeUsuario, (short)EventosBE.Criticidad.Media);
                                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Info, ViewState["DatosGrabadosOk"].ToString());
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                            }
                        }
                        catch (SinPermisosException)
                        {
                            UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                        }
                        LimpiarDatos();
                    }
                }
            }
            else
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinDatosVacios"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarDatos();
        }

        private void LimpiarDatos()
        {

        }
    }
}