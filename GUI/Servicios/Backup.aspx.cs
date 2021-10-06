using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using BE;
using System.IO;

namespace GUI.Servicios.Seguridad
{
    public partial class Backup : System.Web.UI.Page, IObserver
    {
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();
        private IdiomaSL gestorIdioma = new IdiomaSL();

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                CrearBkpModalTitle.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 16);
                lblBackup.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 16);
                btnBackup.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 17);
                btnRestore.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 18);
                btnGuardar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 19);
                btnCancelar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 20);
                lblNombreBkp.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 21);
                btnAceptarRestore.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 28);
                btnCancelarRestore.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 20);
                RestaurarModalTitle.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 18);
                lblArchivoRestore.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 21);

                ViewState["SinPermisos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 57);
                ViewState["OperacionExitosa"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 31);
            }
        }

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }
            btnBackup.Enabled = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Generar Backup"), (UsuarioBE)Session["UsuarioAutenticado"]);
            btnRestore.Enabled = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Restaurar Backup"), (UsuarioBE)Session["UsuarioAutenticado"]);
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

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            txtNombreBkp.Text = "TiendaAlas_TFI";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "generarBackupShow()", true);
        }

        protected void btnGuardarCambios_click(object sender, EventArgs e)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyyMMdd_HH.mm.ss");
                BackupSL.realizarBackup(txtNombreBkp.Text + "_" + date, (UsuarioBE)Session["UsuarioAutenticado"]);
                gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.GenerarBackup, (short)EventosBE.Criticidad.Alta);
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Info, ViewState["OperacionExitosa"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
            catch (SL.BackupException ex)
            {
                //Session["ErrorMsg"] = ex.Message;
                //Response.Redirect(@"~/ErrorPage.aspx");
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Error, ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
            catch (SL.SinPermisosException)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            string backupPath = ConfigurationManager.AppSettings["BackupPath"];
            var fileEntries = Directory.GetFiles(backupPath);
            List<string> resultados = new List<string>();
            foreach (string fileName in fileEntries)
            {
                resultados.Add(fileName.Replace(backupPath, ""));
            }
            ddlArchivosBkp.DataSource = null;
            ddlArchivosBkp.DataSource = resultados;
            ddlArchivosBkp.DataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "restaurarModal()", true);
        }

        protected void btnAceptarRestore_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = ddlArchivosBkp.SelectedValue;
                BackupSL.restaurarBackup(fileName.Remove(fileName.Length - 4, 4), (UsuarioBE)Session["UsuarioAutenticado"]);
                gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.RestaurarBackup, (short)EventosBE.Criticidad.Alta);
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Info, ViewState["OperacionExitosa"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
            catch (SL.SinPermisosException)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
            catch (Exception ex)
            {
                //Session["ErrorMsg"] = ex.Message;
                //Response.Redirect(@"~/ErrorPage.aspx");
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Error, ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }
    }
}