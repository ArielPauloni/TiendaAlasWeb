using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using BE;
using SL;
using System.IO;
using System.Data;
using System.Net;

namespace GUI.Servicios.Bitacora
{
    public partial class Bitacora : System.Web.UI.Page, IObserver
    {
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private PersistenciaSL gestorPersistencia = new PersistenciaSL();
        private ReporteSL GestorReportes = new ReporteSL();

        public enum grvBitacoraColumns
        {
            Cod_Usuario,
            Cod_Evento,
            CriticidadTexto,
            NombreUsuario,
            DescripcionEvento,
            FechaEvento,
            Criticidad
        }

        public void TraducirTexto()
        {

        }

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                EnlazarGrilla();
            }
        }

        private void EnlazarGrilla()
        {
            grvBitacora.DataSource = gestorBitacora.Listar();
            grvBitacora.DataBind();
            grvBitacora.Columns[(int)grvBitacoraColumns.Cod_Usuario].Visible = false;
            grvBitacora.Columns[(int)grvBitacoraColumns.Cod_Evento].Visible = false;
            grvBitacora.Columns[(int)grvBitacoraColumns.Criticidad].Visible = false;
        }

        protected void grvBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvBitacora.PageIndex = e.NewPageIndex;
            grvBitacora.EditIndex = -1;
            EnlazarGrilla();
        }

        protected void btnExportarJson_Click(object sender, EventArgs e)
        {
            GrabarArchivoBitacora("json");
        }

        protected void btnExportarXML_Click(object sender, EventArgs e)
        {
            GrabarArchivoBitacora("xml");
        }

        protected void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                string tmpPath = Server.MapPath("~/");
                string filename = String.Format("TiendaAlas_Bitacora_{0}." + "PDF", DateTime.Now.ToString().Replace("/", "-").Replace(":", ".").Replace(" ", "_"));

                DataTable dt = GetDataTable(grvBitacora);
                GestorReportes.GuardarPDF(tmpPath + @"\" + filename, "-Bitacora", string.Empty, string.Empty, dt, "-Pág. {0} de {1}");

                Response.Redirect("~/DownloadFile.ashx?filename=" + filename);
            }
            catch (Exception ex)
            {
                UC_MensajeModal.SetearMensaje("-No se pudo grabar. Error: " + "\r\n" + ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        private void GrabarArchivoBitacora(string tipoArchivo)
        {
            try
            {
                string tmpPath = Server.MapPath("~/");
                string filename = String.Format("TiendaAlas_Bitacora_{0}." + tipoArchivo, DateTime.Now.ToString().Replace("/", "-").Replace(":", ".").Replace(" ", "_"));
                List<BitacoraBE> datosBitacora = new List<BitacoraBE>();
                foreach (GridViewRow r in grvBitacora.Rows)
                {
                    BitacoraBE bitacora = new BitacoraBE
                    {
                        Cod_Usuario = int.Parse(r.Cells[(int)grvBitacoraColumns.Cod_Usuario].Text),
                        NombreUsuario = HttpUtility.HtmlDecode(r.Cells[(int)grvBitacoraColumns.NombreUsuario].Text),
                        Cod_Evento = short.Parse(r.Cells[(int)grvBitacoraColumns.Cod_Evento].Text),
                        DescripcionEvento = r.Cells[(int)grvBitacoraColumns.DescripcionEvento].Text,
                        Criticidad = short.Parse(r.Cells[(int)grvBitacoraColumns.Criticidad].Text),
                        CriticidadTexto = r.Cells[(int)grvBitacoraColumns.CriticidadTexto].Text,
                        FechaEvento = DateTime.Parse(r.Cells[(int)grvBitacoraColumns.FechaEvento].Text),
                        FileName = tmpPath + @"\" + filename
                    };
                    datosBitacora.Add(bitacora);
                }

                if (datosBitacora != null)
                {
                    switch (tipoArchivo)
                    {
                        case "xml":
                            foreach (BitacoraBE bitacora in datosBitacora)
                            {
                                int x = gestorPersistencia.EscribirBitacoraXML(bitacora);
                            }
                            break;
                        case "json":
                            int j = gestorPersistencia.EscribirBitacoraJSON(datosBitacora, tmpPath + @"\" + filename);
                            break;
                    }
                    Response.Redirect("~/DownloadFile.ashx?filename=" + filename);
                }
            }
            catch (Exception ex)
            {
                UC_MensajeModal.SetearMensaje("-Error: " + "\r\n" + ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();
            var listaColOcultas = new List<int> { (int)grvBitacoraColumns.Cod_Usuario, (int)grvBitacoraColumns.Cod_Evento, (int)grvBitacoraColumns.Criticidad };

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {
                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    if (!listaColOcultas.Contains(i)) { dt.Columns.Add(dtg.HeaderRow.Cells[i].Text); }
                }
            }
            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();
                int j = 0;
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (!listaColOcultas.Contains(i))
                    {
                        dr[j] = HttpUtility.HtmlDecode(row.Cells[i].Text);
                        j++;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}