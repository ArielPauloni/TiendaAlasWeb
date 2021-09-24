using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using BLL;
using BE;
using System.Web.Security;

namespace GUI
{
    public partial class Login : System.Web.UI.Page, IObserver
    {
        private LoginSL gestorLogin = new LoginSL();
        private ConfiguracionSL gestorConfiguracion = new ConfiguracionSL();
        private MailSL gestorMail = new MailSL();
        private UsuarioBLL gestorUsuario = new UsuarioBLL();
        private EncriptacionSL gestorEncriptacion = new EncriptacionSL();

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] != null) { Response.Redirect(@"~\Bienvenido.aspx"); }
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

                if (Request.Cookies["user"] != null)
                    txtAlias.Text = Request.Cookies["user"].Value;
                if (Request.Cookies["pwd"] != null)
                    txtPassword.Attributes.Add("value", Request.Cookies["pwd"].Value);
                if (Request.Cookies["user"] != null && Request.Cookies["pwd"] != null)
                    chkRecordarDatos.Checked = true;

                btnShowHidePass.Attributes.Add("Title", "-Mostrar");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (chkRecordarDatos.Checked)
            {
                Response.Cookies["user"].Value = txtAlias.Text;
                Response.Cookies["pwd"].Value = txtPassword.Text;
                Response.Cookies["user"].Expires = DateTime.Now.AddDays(90);
                Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(90);
            }
            else
            {
                Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);
            }

            string loginAlias = txtAlias.Text.Trim();
            string loginPass = txtPassword.Text.Trim();

            if ((!string.IsNullOrWhiteSpace(loginAlias)) && (!string.IsNullOrWhiteSpace(loginPass)))
            {
                UsuarioBE usuario = new UsuarioBE();
                UsuarioBE usuarioAutenticado = new UsuarioBE();

                usuario.Alias = loginAlias;
                usuario.Contraseña = loginPass;
                try
                {
                    usuarioAutenticado = gestorLogin.ObtenerUsuarioAutenticado(usuario);
                }
                catch (SL.UsuarioModificadoException ex)
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                    usuarioAutenticado = null;
                }
                catch (SL.UsuarioBloqueadoException)
                {
                    usuarioAutenticado = null;
                }

                if (usuarioAutenticado != null)
                {
                    //BitacoraBE bitacoraBE = new BitacoraBE();
                    //bitacoraBE.Cod_Usuario = usuarioAutenticado.Cod_Usuario;
                    //bitacoraBE.Cod_Evento = (short)EventosBE.Eventos.Login;
                    //gestorBitacora.Insertar(bitacoraBE, usuarioAutenticado);

                    Session["UsuarioAutenticado"] = usuarioAutenticado;
                    Session["IdiomaSel"] = usuarioAutenticado.Idioma;
                    Subject.Notify();

                    Response.Redirect(@"~/Bienvenido.aspx");
                }
            }
            else
            {
                //string myStringVariable = "Datos incorrectos";
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void ShowHidePass_click(object sender, EventArgs e)
        {
            if (btnShowHidePass.Attributes.Count == 2)
            {
                //Tengo 2 atributos: Está ocultando => Muestro
                btnShowHidePass.Attributes.Clear();
                btnShowHidePass.Attributes.Add("customAttribute", "true");
                btnShowHidePass.Attributes.Add("class", "btnShowHidePass fa fa-eye-slash");
                txtPassword.TextMode = TextBoxMode.SingleLine;
                btnShowHidePass.Attributes.Add("Title", "-Ocultar");
            }
            else
            {
                //Tengo 3 atributos: Está mostrando => Oculto
                btnShowHidePass.Attributes.Clear();
                btnShowHidePass.Attributes.Add("class", "btnShowHidePass fa fa-eye");
                string pass = txtPassword.Text;
                txtPassword.TextMode = TextBoxMode.Password;
                txtPassword.Attributes.Add("Value", pass);
                btnShowHidePass.Attributes.Add("Title", "-Mostrar");
            }
        }

        protected void lnkRecuperoPass_Click(object sender, EventArgs e)
        {
            //TODO: Si el usuario está bloqueado???
            try
            {
                if (!string.IsNullOrWhiteSpace(txtAlias.Text))
                {
                    string RemitenteMail = string.Empty;
                    string RemitentePass = string.Empty;
                    string NuevoPass = gestorLogin.ObtenerRandomString(8);
                    UsuarioBE unUsuario = new UsuarioBE();
                    unUsuario.Alias = txtAlias.Text.Trim();
                    UsuarioBE usuarioDestinatario = new UsuarioBE();
                    usuarioDestinatario = gestorUsuario.ObtenerUsuarioPorAlias(unUsuario);
                    usuarioDestinatario.Contraseña = gestorEncriptacion.SimpleEncrypt(NuevoPass);
                    gestorLogin.ActualizarPassUsuario(usuarioDestinatario);

                    gestorConfiguracion.ConfigurarRemitenteEnvioMail(ref RemitenteMail, ref RemitentePass);
                    gestorMail.EnviarMailRecuperoPass(RemitenteMail, RemitentePass, usuarioDestinatario, NuevoPass);
                }
                else
                {
                    //Colocar datos
                }
            }
            catch (Exception)
            {
                //Manejo de excepciones de bloqueo, etc
                throw;
            }
        }
    }
}