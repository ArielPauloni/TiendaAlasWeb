using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using BLL;
using BE;
using SL;
using System.IO;
using System.Web.UI.DataVisualization.Charting;

namespace GUI.Reportes
{
    public partial class RptPatologias : System.Web.UI.Page, IObserver
    {
        private PacienteBLL gestorPaciente = new PacienteBLL();
        private PatologiaBLL gestorPatologia = new PatologiaBLL();
        private ReporteSL GestorReportes = new ReporteSL();
        private IdiomaSL gestorIdioma = new IdiomaSL();

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }
        }

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                ViewState["ErrorMsg"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 1);
                ViewState["SinPermisos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 57);
                ViewState["DatosIncorrectos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 55);
                ViewState["NoSePudoGrabar"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 59);
                ViewState["PatologiasPresentadas"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 153);
                ViewState["PagFooter"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 99);
                ViewState["PatologiaTexto"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 146);
                lblTitle.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 152);
                btnExportarPDF.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 96);
                lblDetalle.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 161) + ": ";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();
            }
            LlenarGrafico();
        }

        private void LlenarGrafico()
        {
            int contador = 0;
            float porcentaje = 0;
            float total = 0;
            string detalle = string.Empty;
            List<PatologiaBE> patologias = gestorPatologia.Listar();
            int[] porcion = new int[patologias.Count];
            string[] nombres = new string[patologias.Count];

            foreach (PatologiaBE patologia in patologias)
            {
                List<PacienteBE> pacientes = gestorPatologia.ListarPacientesPorPatologia(patologia);
                porcion[contador] = pacientes.Count;
                nombres[contador] = patologia.DescripcionPatologia + ": " + pacientes.Count.ToString();
                total += pacientes.Count;
                contador++;
                if (pacientes.Count > 0)
                {
                    detalle += "* " + ViewState["PatologiaTexto"].ToString() + ": " + patologia.DescripcionPatologia + " \n";
                    foreach (PacienteBE paciente in pacientes)
                    { detalle += "- " + paciente.ToString() + "\n"; }
                    detalle += "\r";
                }
            }
            txtDatosPacientes.Text = detalle;
            contador = 0;
            foreach (int i in porcion)
            {
                porcentaje = 100 * porcion[contador] / total;
                nombres[contador] = nombres[contador] + " (" + porcentaje.ToString("n2") + "%)";
                contador++;
            }
            ctPatologias.Series["Series"].Points.DataBindXY(nombres, porcion);
            ctPatologias.Titles.Add(ViewState["PatologiasPresentadas"].ToString());
        }

        protected void btnExportarPDF_ServerClick(object sender, EventArgs e)
        {
            string tmpPath = Server.MapPath("~/");
            string filename = String.Format("TiendaAlas_RptPatologias_{0}." + "PDF", DateTime.Now.ToString().Replace("/", "-").Replace(":", ".").Replace(" ", "_"));

            byte[] bytes = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                ctPatologias.SaveImage(memoryStream, ChartImageFormat.Jpeg);
                bytes = memoryStream.ToArray();
            }

            //TODO: Mostrar el texto de los pacientes o alguna otra info

            GestorReportes.GuardarGraficoPDF(tmpPath + @"\" + filename, ViewState["PatologiasPresentadas"].ToString(), string.Empty, txtDatosPacientes.Text, bytes, ViewState["PagFooter"].ToString());

            Response.Redirect("~/DownloadFile.ashx?filename=" + filename);
        }
    }
}