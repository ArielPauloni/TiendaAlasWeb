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
    public partial class RegistroUsuario : System.Web.UI.Page, IObserver
    {
        private string contieneNumero = @"[0-9]+";
        private string contieneMayuscula = @"[A-Z]+";
        private string contieneSeisCaracteres = @".{6,}";
        private string eMailPattern = @"^[\w._%-]+@[\w.-]+\.[a-zA-Z]{2,4}$";
        private EncriptacionSL gestorEncriptacion = new EncriptacionSL();
        private UsuarioBLL gestorUsuario = new UsuarioBLL();
        private LoginSL gestorLogin = new LoginSL();
        private IdiomaSL gestorIdioma = new IdiomaSL();
        private BitacoraSL gestorBitacora = new BitacoraSL();

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                ViewState["MailIncorrecto"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 47);
                ViewState["DatosIncorrectos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 55);
                ViewState["SinDatosVacios"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 58);
                ViewState["NoSePudoGrabar"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 59);
                ViewState["ContraseniasNoCoinciden"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 60);
                ViewState["ContraseniaPatron"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 61);
                ViewState["Mostrar"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 79);
                ViewState["Ocultar"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 80);

                lblRegistrarse.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 9);
                lblMail.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 67);
                lblAlias.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 73);
                lblPassword1.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 74);
                lblPassword2.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 75);
                btnGrabar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 19);
                btnCancelar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 20);
            }
        }

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] != null) { Response.Redirect(@"~\Bienvenido.aspx"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                btnShowHidePass1.Attributes.Add("Title", ViewState["Mostrar"].ToString());
                btnShowHidePass2.Attributes.Add("Title", ViewState["Mostrar"].ToString());
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            Session["UsuarioCreado"] = false;
            if ((!string.IsNullOrWhiteSpace(txtPassword1.Text)) || (!string.IsNullOrWhiteSpace(txtPassword2.Text)) ||
              (!string.IsNullOrWhiteSpace(txtAlias.Text)) || (!string.IsNullOrWhiteSpace(txtMail.Text)))
            {
                if (string.Compare(txtPassword1.Text, txtPassword2.Text, false) != 0)
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["ContraseniasNoCoinciden"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                else if ((!Regex.IsMatch(txtPassword1.Text, contieneNumero)) ||
                         (!Regex.IsMatch(txtPassword1.Text, contieneMayuscula)) ||
                         (!Regex.IsMatch(txtPassword1.Text, contieneSeisCaracteres)))
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["DatosIncorrectos"].ToString() + ": " +
                                                  ViewState["ContraseniaPatron"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                else if ((!Regex.IsMatch(txtMail.Text, eMailPattern)))
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["MailIncorrecto"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                else
                {
                    UsuarioBE usuario = new UsuarioBE();
                    Nullable<DateTime> fNull = default(DateTime?);

                    usuario.Apellido = string.Empty;
                    usuario.Nombre = string.Empty;
                    usuario.Alias = txtAlias.Text;
                    usuario.Contraseña = gestorEncriptacion.SimpleEncrypt(txtPassword1.Text);
                    usuario.Telefono = string.Empty;
                    usuario.Mail = txtMail.Text;
                    usuario.FechaNacimiento = fNull;
                    TipoUsuarioBE tipoUsuario = new TipoUsuarioBE();
                    tipoUsuario.Cod_Tipo = 2;
                    tipoUsuario.Descripcion_Tipo = "Paciente";
                    usuario.TipoUsuario = tipoUsuario;
                    int r = gestorUsuario.InsertarDatosBasicosSinPermisos(usuario);
                    if (r <= 0)
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    else
                    {
                        usuario.Contraseña = gestorEncriptacion.SimpleDecrypt(usuario.Contraseña);
                        Session["UsuarioAutenticado"] = gestorLogin.ObtenerUsuarioAutenticado(usuario);
                        if ((Session["UsuarioAutenticado"] != null) && ((UsuarioBE)Session["UsuarioAutenticado"] != null))
                        {
                            gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.AltaDeUsuario, (short)EventosBE.Criticidad.Media);
                            gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.Login, (short)EventosBE.Criticidad.Baja);
                        }
                        Session["UsuarioCreado"] = true;
                        Response.Redirect(@"~\Bienvenido.aspx");
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
            Response.Redirect(@"~\Bienvenido.aspx");
        }

        protected void ShowHidePass1_click(object sender, EventArgs e)
        {
            if (btnShowHidePass1.Attributes.Count == 2)
            {
                //Tengo 2 atributos: Está ocultando => Muestro
                btnShowHidePass1.Attributes.Clear();
                btnShowHidePass1.Attributes.Add("customAttribute", "true");
                btnShowHidePass1.Attributes.Add("class", "btnShowHidePass fa fa-eye-slash");
                txtPassword1.TextMode = TextBoxMode.SingleLine;
                btnShowHidePass1.Attributes.Add("Title", ViewState["Ocultar"].ToString());
            }
            else
            {
                //Tengo 3 atributos: Está mostrando => Oculto
                btnShowHidePass1.Attributes.Clear();
                btnShowHidePass1.Attributes.Add("class", "btnShowHidePass fa fa-eye");
                string pass = txtPassword1.Text;
                txtPassword1.TextMode = TextBoxMode.Password;
                txtPassword1.Attributes.Add("Value", pass);
                btnShowHidePass1.Attributes.Add("Title", ViewState["Mostrar"].ToString());
            }
        }

        protected void ShowHidePass2_click(object sender, EventArgs e)
        {
            if (btnShowHidePass2.Attributes.Count == 2)
            {
                //Tengo 2 atributos: Está ocultando => Muestro
                btnShowHidePass2.Attributes.Clear();
                btnShowHidePass2.Attributes.Add("customAttribute", "true");
                btnShowHidePass2.Attributes.Add("class", "btnShowHidePass fa fa-eye-slash");
                txtPassword2.TextMode = TextBoxMode.SingleLine;
                btnShowHidePass2.Attributes.Add("Title", ViewState["Ocultar"].ToString());
            }
            else
            {
                //Tengo 3 atributos: Está mostrando => Oculto
                btnShowHidePass2.Attributes.Clear();
                btnShowHidePass2.Attributes.Add("class", "btnShowHidePass fa fa-eye");
                string pass = txtPassword2.Text;
                txtPassword2.TextMode = TextBoxMode.Password;
                txtPassword2.Attributes.Add("Value", pass);
                btnShowHidePass2.Attributes.Add("Title", ViewState["Mostrar"].ToString());
            }
        }
    }
}