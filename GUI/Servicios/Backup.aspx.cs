using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL;

namespace GUI.Servicios.Seguridad
{
    public partial class Backup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CrearBkpModalTitle.InnerText = "-Generar Backup";
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
            }
            catch (SL.BackupException ex)
            {
                Session["ErrorMsg"] = ex.Message;
                Response.Redirect(@"~/ErrorPage.aspx");
            }
        }
    }
}