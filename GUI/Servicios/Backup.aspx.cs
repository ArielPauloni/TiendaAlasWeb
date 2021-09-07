using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using System.IO;

namespace GUI.Servicios.Seguridad
{
    public partial class Backup : System.Web.UI.Page, IObserver
    {
        public void TraducirTexto()
        {
            IdiomaSL gestorIdioma = new IdiomaSL();

            CrearBkpModalTitle.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 16);
            lblBackup.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 16);
            btnBackup.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 17);
            btnRestore.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 18);
            btnGuardar.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 19);
            btnCancelar.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 20);
            lblNombreBkp.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 21);
            lblMensaje.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 31);
            btnAceptarRestore.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 28);
            btnCancelarRestore.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 20);
            RestaurarModalTitle.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 18);
            MensajeModalTitle.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 32);
            lblArchivoRestore.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 21);
        }

        public void ChequearPermisos()
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
                BackupSL.realizarBackup(txtNombreBkp.Text + "_" + date);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "modalMensaje()", true);
            }
            catch (SL.BackupException ex)
            {
                Session["ErrorMsg"] = ex.Message;
                Response.Redirect(@"~/ErrorPage.aspx");
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
                BackupSL.restaurarBackup(fileName.Remove(fileName.Length - 4, 4));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "modalMensaje()", true);
            }
            catch (Exception ex)
            {
                Session["ErrorMsg"] = ex.Message;
                Response.Redirect(@"~/ErrorPage.aspx");
            }
        }
    }
}