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
    public partial class MisDatos : System.Web.UI.Page, IObserver
    {
        private UsuarioBLL gestorUsuario = new UsuarioBLL();
        private EncriptacionSL gestorEncriptacion = new EncriptacionSL();
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private TipoUsuarioBLL gestorTipoUsuario = new TipoUsuarioBLL();
        private IdiomaSL gestorIdioma = new IdiomaSL();
        private string eMailPattern = @"^[\w._%-]+@[\w.-]+\.[a-zA-Z]{2,4}$";
        private string contieneNumero = @"[0-9]+";
        private string contieneMayuscula = @"[A-Z]+";
        private string contieneSeisCaracteres = @".{6,}";

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }
        }

        public void TraducirTexto()
        {
            ViewState["DatosGrabadosOk"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 12);
            ViewState["MailIncorrecto"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 47);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                CargarDatosUsuarioAutenticado();
            }
        }

        private void CargarDatosUsuarioAutenticado()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] != null)
            {
                txtApellido.Text = ((UsuarioBE)Session["UsuarioAutenticado"]).Apellido;
                txtNombre.Text = ((UsuarioBE)Session["UsuarioAutenticado"]).Nombre;
                txtAlias.Text = ((UsuarioBE)Session["UsuarioAutenticado"]).Alias;
                txtMail.Text = ((UsuarioBE)Session["UsuarioAutenticado"]).Mail;
                txtTelefono.Text = ((UsuarioBE)Session["UsuarioAutenticado"]).Telefono;
                txtPass1.Attributes["value"] = gestorEncriptacion.SimpleDecrypt(((UsuarioBE)Session["UsuarioAutenticado"]).Contraseña);
                txtPass2.Attributes["value"] = gestorEncriptacion.SimpleDecrypt(((UsuarioBE)Session["UsuarioAutenticado"]).Contraseña);
                if (((UsuarioBE)Session["UsuarioAutenticado"]).FechaNacimiento.HasValue)
                { txtFechaNacimiento.Text = ((UsuarioBE)Session["UsuarioAutenticado"]).FechaNacimiento.Value.ToString("yyyy-MM-dd"); }

                ddlTipoUsuario.DataSource = gestorTipoUsuario.Listar();
                ddlTipoUsuario.DataTextField = "Descripcion_Tipo";
                ddlTipoUsuario.DataValueField = "Cod_Tipo";
                ddlTipoUsuario.DataBind();
                ddlTipoUsuario.SelectedValue = ((UsuarioBE)Session["UsuarioAutenticado"]).TipoUsuario.Cod_Tipo.ToString();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(txtApellido.Text)) && (!string.IsNullOrWhiteSpace(txtNombre.Text)) &&
                (!string.IsNullOrWhiteSpace(txtPass1.Text)) && (!string.IsNullOrWhiteSpace(txtPass2.Text)) &&
                (!string.IsNullOrWhiteSpace(txtAlias.Text)))
            {
                if (string.Compare(txtPass1.Text, txtPass2.Text, false) != 0)
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, "-Contraseñas No Coinciden");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                else if ((!Regex.IsMatch(txtPass1.Text, contieneNumero)) ||
                         (!Regex.IsMatch(txtPass1.Text, contieneMayuscula)) ||
                         (!Regex.IsMatch(txtPass1.Text, contieneSeisCaracteres)))
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, "-Datos Incorrectos + " + ": " + "-Contraseña debe contener mayúscula, número y longitud mayor a seis");
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

                        usuario.Cod_Usuario = ((UsuarioBE)Session["UsuarioAutenticado"]).Cod_Usuario;
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
                        usuario.Inactivo = ((UsuarioBE)Session["UsuarioAutenticado"]).Inactivo;
                        usuario.IntentosEquivocados = ((UsuarioBE)Session["UsuarioAutenticado"]).IntentosEquivocados;
                        if (((UsuarioBE)Session["UsuarioAutenticado"]).UltimoLogin.HasValue)
                        { usuario.UltimoLogin = ((UsuarioBE)Session["UsuarioAutenticado"]).UltimoLogin.Value; }

                        try
                        {
                            int r = gestorUsuario.EditarSinPermisos(usuario);
                            if (r == 0)
                            {
                                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, "-No se pudo grabar");
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                            }
                            else
                            {
                                usuario.Permisos = ((UsuarioBE)Session["UsuarioAutenticado"]).Permisos;
                                usuario.Idioma = ((UsuarioBE)Session["UsuarioAutenticado"]).Idioma;
                                Session["UsuarioAutenticado"] = usuario;
                                gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.AltaDeUsuario, (short)EventosBE.Criticidad.Media);
                                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Info, ViewState["DatosGrabadosOk"].ToString());
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                            }
                        }
                        catch (SinPermisosException)
                        {
                            UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, "-Sin Permisos");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                        }
                        CargarDatosUsuarioAutenticado();
                    }
                }
            }
            else
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, "-No se puede Grabar Con Datos Vacios");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }
    }
}