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
using System.Globalization;
using System.Drawing;

namespace GUI.Servicios.Bitacora
{
    public partial class Bitacora : System.Web.UI.Page, IObserver
    {
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private PersistenciaSL gestorPersistencia = new PersistenciaSL();
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();
        private ReporteSL GestorReportes = new ReporteSL();
        private IdiomaSL gestorIdioma = new IdiomaSL();

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

        private string sortDirByNombreUsuario
        {
            get { return ViewState["sortDirByNombreUsuario"] != null ? ViewState["sortDirByNombreUsuario"].ToString() : "DESC"; }
            set { ViewState["sortDirByNombreUsuario"] = value; }
        }

        private string sortDirByDescripcionEvento
        {
            get { return ViewState["sortDirByDescripcionEvento"] != null ? ViewState["sortDirByDescripcionEvento"].ToString() : "DESC"; }
            set { ViewState["sortDirByDescripcionEvento"] = value; }
        }

        private string sortDirByCriticidadTexto
        {
            get { return ViewState["sortDirByCriticidadTexto"] != null ? ViewState["sortDirByCriticidadTexto"].ToString() : "DESC"; }
            set { ViewState["sortDirByCriticidadTexto"] = value; }
        }

        private string sortDirByFechaEvento
        {
            get { return ViewState["sortDirByFechaEvento"] != null ? ViewState["sortDirByFechaEvento"].ToString() : "DESC"; }
            set { ViewState["sortDirByFechaEvento"] = value; }
        }

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                lblUsuario.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 71);
                lblFechaDesde.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 94);
                lblFechaHasta.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 95);
                lblEvento.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 92); ;
                lblCriticidad.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 89);
                btnMostrarFiltros.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 38);
                btnFiltrar.Attributes.Add("title", gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 40));
                btnLimpiarFiltros.Attributes.Add("title", gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 41));

                ViewState["SinPermisos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 57);
                ViewState["MostrarFiltros"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 39);
                ViewState["OcultarFiltros"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 42);
                btnMostrarFiltros.Attributes.Add("title", ViewState["MostrarFiltros"].ToString());
                ViewState["ErrorMsg"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 1);
                ViewState["NoSePudoGrabar"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 59);
                ViewState["NombreUsuario"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 71);
                ViewState["CriticidadTexto"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 89);
                ViewState["DescripcionEvento"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 90);
                ViewState["FechaEvento"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 91);
                ViewState["BitacoraCaption"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 93);
                ViewState["PagFooter"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 99);

                btnExportarXML.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 98);
                btnExportarJson.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 97);
                btnExportarPDF.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 96);
            }
        }

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }
            btnExportarJson.Disabled = !gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Ver Bitacora"), (UsuarioBE)Session["UsuarioAutenticado"]);
            btnExportarPDF.Disabled = !gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Ver Bitacora"), (UsuarioBE)Session["UsuarioAutenticado"]);
            btnExportarXML.Disabled = !gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Ver Bitacora"), (UsuarioBE)Session["UsuarioAutenticado"]);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                List<string> listaVacia = new List<string>();
                EnlazarGrillaBitacora(listaVacia, string.Empty);
            }
        }

        private void EnlazarGrillaBitacora(List<string> filtros, string orderBy)
        {
            grvBitacora.DataSource = null;
            List<BitacoraBE> datosBitacora = new List<BitacoraBE>();
            if (filtros.Count == 0) { datosBitacora = gestorBitacora.Listar(); }
            else
            {
                IEnumerable<BitacoraBE> filtradosPorNombre = null;
                IEnumerable<BitacoraBE> filtradosPorEvento = null;
                IEnumerable<BitacoraBE> filtradosPorFechaDesde = null;
                IEnumerable<BitacoraBE> filtradosPorFechaHasta = null;
                IEnumerable<BitacoraBE> filtradosPorCriticidad = null;

                //Filtro por nombre
                if (!string.IsNullOrWhiteSpace(filtros[0].ToString()))
                {
                    filtradosPorNombre =
                        from BitacoraBE evento in gestorBitacora.Listar()
                        where evento.NombreUsuario == filtros[0].ToString()
                        select evento;
                }
                else { filtradosPorNombre = gestorBitacora.Listar(); }

                //Filtro por evento (al previamente filtrado por nombre)
                if (!string.IsNullOrWhiteSpace(filtros[1].ToString()))
                {
                    filtradosPorEvento =
                        from BitacoraBE evento in filtradosPorNombre
                        where evento.DescripcionEvento == filtros[1].ToString()
                        select evento;
                }
                else { filtradosPorEvento = filtradosPorNombre; }

                //Filtro por Fecha DESDE (al previamente filtrado por nombre y evento)
                if (!string.IsNullOrWhiteSpace(filtros[2].ToString()))
                {
                    filtradosPorFechaDesde =
                        from BitacoraBE evento in filtradosPorEvento
                        where DateTime.Parse(evento.FechaEvento.ToString("dd/MM/yyyy"), CultureInfo.CurrentCulture) >=
                              DateTime.Parse(filtros[2].ToString(), CultureInfo.CurrentCulture)
                        select evento;
                }
                else { filtradosPorFechaDesde = filtradosPorEvento; }

                //Filtro por fecha HASTA (al previamente filtrado por fecha desde, nombre y evento)
                if (!string.IsNullOrWhiteSpace(filtros[3].ToString()))
                {
                    filtradosPorFechaHasta =
                        from BitacoraBE evento in filtradosPorFechaDesde
                        where DateTime.Parse(evento.FechaEvento.ToString("dd/MM/yyyy"), CultureInfo.CurrentCulture) <=
                              DateTime.Parse(filtros[3].ToString(), CultureInfo.CurrentCulture)
                        select evento;
                }
                else { filtradosPorFechaHasta = filtradosPorFechaDesde; }

                //Filtro por criticidad (al previamente filtrado por fecha desde, fecha hasta, nombre y evento)
                if (!string.IsNullOrWhiteSpace(filtros[4].ToString()))
                {
                    filtradosPorCriticidad =
                        from BitacoraBE evento in filtradosPorFechaHasta
                        where evento.CriticidadTexto == filtros[4].ToString()
                        select evento;
                }
                else { filtradosPorCriticidad = filtradosPorFechaHasta; }

                //En filtradosPorCriticidad tengo cada uno de los filtros
                foreach (BitacoraBE eventoFiltrado in filtradosPorCriticidad)
                { datosBitacora.Add(eventoFiltrado); }
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                if (string.Compare("CriticidadTexto", orderBy, true) == 0)
                {
                    if (string.Compare("ASC", this.sortDirByCriticidadTexto, true) == 0)
                    { datosBitacora = datosBitacora.OrderBy(r => r.CriticidadTexto).ToList(); }
                    else { datosBitacora = datosBitacora.OrderByDescending(r => r.CriticidadTexto).ToList(); } 
                }
                
                if (string.Compare("NombreUsuario", orderBy, true) == 0)
                {
                    if (string.Compare("ASC", this.sortDirByNombreUsuario, true) == 0)
                    { datosBitacora = datosBitacora.OrderBy(r => r.NombreUsuario).ToList(); }
                    else { datosBitacora = datosBitacora.OrderByDescending(r => r.NombreUsuario).ToList(); }
                }

                if (string.Compare("DescripcionEvento", orderBy, true) == 0)
                {
                    if (string.Compare("ASC", this.sortDirByDescripcionEvento, true) == 0)
                    { datosBitacora = datosBitacora.OrderBy(r => r.DescripcionEvento).ToList(); }
                    else { datosBitacora = datosBitacora.OrderByDescending(r => r.DescripcionEvento).ToList(); }
                }

                if (string.Compare("FechaEvento", orderBy, true) == 0)
                {
                    if (string.Compare("ASC", this.sortDirByFechaEvento, true) == 0)
                    { datosBitacora = datosBitacora.OrderBy(r => r.FechaEvento).ToList(); }
                    else { datosBitacora = datosBitacora.OrderByDescending(r => r.FechaEvento).ToList(); }
                }
            }

            ListaOrdenable<BitacoraBE> bitacoraOrdenable = new ListaOrdenable<BitacoraBE>();
            bitacoraOrdenable = new ListaOrdenable<BitacoraBE>(datosBitacora);
            grvBitacora.DataSource = bitacoraOrdenable;
            grvBitacora.DataBind();

           grvBitacora.Caption = ViewState["BitacoraCaption"].ToString();

            var nombresBitacora = datosBitacora.Select(o => o.NombreUsuario).Distinct().OrderBy(NombreUsuario => NombreUsuario).ToList();
            List<string> nombres = new List<string>();
            nombres.Add(string.Empty);
            foreach (string nombreBitacora in nombresBitacora)
            { nombres.Add(nombreBitacora); }
            ddlUsuarios.DataSource = nombres;
            ddlUsuarios.DataBind();
            if (ddlUsuarios.Items.Count == 2) { ddlUsuarios.SelectedIndex = 1; }

            var eventosBitacora = datosBitacora.Select(o => o.DescripcionEvento).Distinct().OrderBy(DescripcionEvento => DescripcionEvento).ToList();
            List<string> eventos = new List<string>();
            eventos.Add(string.Empty);
            foreach (string eventoBitacora in eventosBitacora) { eventos.Add(eventoBitacora); }
            ddlEventos.DataSource = eventos;
            ddlEventos.DataBind();
            if (ddlEventos.Items.Count == 2) { ddlEventos.SelectedIndex = 1; }

            var fechasBitacora = datosBitacora.Select(o => o.FechaEvento.ToString("dd/MM/yyyy")).Distinct().OrderByDescending(FechaEvento => FechaEvento).ToList();
            List<string> fechas = new List<string>();
            fechas.Add(string.Empty);
            foreach (string fechaBitacora in fechasBitacora) { fechas.Add(fechaBitacora); }
            ddlFechaDesde.DataSource = fechas;
            ddlFechaDesde.DataBind();
            if (ddlFechaDesde.Items.Count == 2) { ddlFechaDesde.SelectedIndex = 1; }
            ddlFechaHasta.DataSource = fechas;
            ddlFechaHasta.DataBind();
            if (ddlFechaHasta.Items.Count == 2) { ddlFechaHasta.SelectedIndex = 1; }

            var criticidadBitacora = datosBitacora.Select(o => o.CriticidadTexto).Distinct().OrderBy(CriticidadTexto => CriticidadTexto).ToList();
            List<string> criticidades = new List<string>();
            criticidades.Add(string.Empty);
            foreach (string criticidad in criticidadBitacora) { criticidades.Add(criticidad); }
            ddlCriticidad.DataSource = criticidades;
            ddlCriticidad.DataBind();
            if (ddlCriticidad.Items.Count == 2) { ddlCriticidad.SelectedIndex = 1; }
        }
        
        protected void btnExportarJson_Click(object sender, EventArgs e)
        {
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Ver Bitacora"), (UsuarioBE)Session["UsuarioAutenticado"]))
            { GrabarArchivoBitacora("json"); }
            else
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void btnExportarXML_Click(object sender, EventArgs e)
        {
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Ver Bitacora"), (UsuarioBE)Session["UsuarioAutenticado"]))
            { GrabarArchivoBitacora("xml"); }
            else
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Ver Bitacora"), (UsuarioBE)Session["UsuarioAutenticado"]))
                {
                    string tmpPath = Server.MapPath("~/");
                    string filename = String.Format("TiendaAlas_Bitacora_{0}." + "PDF", DateTime.Now.ToString().Replace("/", "-").Replace(":", ".").Replace(" ", "_"));

                    DataTable dt = GetDataTable(grvBitacora);
                    GestorReportes.GuardarPDF(tmpPath + @"\" + filename, ViewState["BitacoraCaption"].ToString(), string.Empty, string.Empty, dt, ViewState["PagFooter"].ToString());

                    Response.Redirect("~/DownloadFile.ashx?filename=" + filename);
                }
                else
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
            }
            catch (Exception ex)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Error, ViewState["NoSePudoGrabar"].ToString() + ". " +
                                              ViewState["ErrorMsg"].ToString() + ": " + "<br>" + ex.Message);
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
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Error, ViewState["ErrorMsg"].ToString() + "<br>" + ex.Message);
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
                    if (!listaColOcultas.Contains(i))
                    {
                        switch (i)
                        {
                            case (int)grvBitacoraColumns.CriticidadTexto:
                                dt.Columns.Add(ViewState["CriticidadTexto"].ToString());
                                break;
                            case (int)grvBitacoraColumns.NombreUsuario:
                                dt.Columns.Add(ViewState["NombreUsuario"].ToString());
                                break;
                            case (int)grvBitacoraColumns.DescripcionEvento:
                                dt.Columns.Add(ViewState["DescripcionEvento"].ToString());
                                break;
                            case (int)grvBitacoraColumns.FechaEvento:
                                dt.Columns.Add(ViewState["FechaEvento"].ToString());
                                break;
                        }
                    }
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

        protected void btnMostrarFiltros_Click(object sender, EventArgs e)
        {
            if (divFiltros.Visible)
            {
                btnMostrarFiltros.Attributes.Add("title", ViewState["MostrarFiltros"].ToString());
                divFiltros.Visible = false;
                LimpiarFiltros();
            }
            else
            {
                btnMostrarFiltros.Attributes.Add("title", ViewState["OcultarFiltros"].ToString());
                divFiltros.Visible = true;
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            List<string> filtros = new List<string>();
            filtros.Add(ddlUsuarios.SelectedItem.ToString());
            filtros.Add(ddlEventos.SelectedItem.ToString());
            filtros.Add(ddlFechaDesde.SelectedItem.ToString());
            filtros.Add(ddlFechaHasta.SelectedItem.ToString());
            filtros.Add(ddlCriticidad.SelectedItem.ToString());

            EnlazarGrillaBitacora(filtros, string.Empty);
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        private void LimpiarFiltros()
        {
            List<string> listaVacia = new List<string>();
            EnlazarGrillaBitacora(listaVacia, string.Empty);
        }

        protected void grvBitacora_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                LinkButton btnSort = (LinkButton)e.Row.Cells[(int)grvBitacoraColumns.NombreUsuario].Controls[0];
                btnSort.Text = ViewState["NombreUsuario"].ToString();
                btnSort = (LinkButton)e.Row.Cells[(int)grvBitacoraColumns.CriticidadTexto].Controls[0];
                btnSort.Text = ViewState["CriticidadTexto"].ToString();
                btnSort = (LinkButton)e.Row.Cells[(int)grvBitacoraColumns.DescripcionEvento].Controls[0];
                btnSort.Text = ViewState["DescripcionEvento"].ToString();
                btnSort = (LinkButton)e.Row.Cells[(int)grvBitacoraColumns.FechaEvento].Controls[0];
                btnSort.Text = ViewState["FechaEvento"].ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                switch ((short)(DataBinder.Eval(e.Row.DataItem, "Criticidad")))
                {
                    case (short)EventosBE.Criticidad.Baja:
                        e.Row.BackColor = Color.LightGreen;
                        e.Row.ForeColor = Color.Black;
                        break;
                    case (short)EventosBE.Criticidad.Media:
                        e.Row.BackColor = Color.Yellow;
                        e.Row.ForeColor = Color.Black;
                        break;
                    case (short)EventosBE.Criticidad.Alta:
                        e.Row.BackColor = Color.Orange;
                        e.Row.ForeColor = Color.Black;
                        break;
                    case (short)EventosBE.Criticidad.MuyAlta:
                        e.Row.BackColor = Color.Red;
                        e.Row.ForeColor = Color.White;
                        break;
                }
            }
        }

        protected void OnSorting(object sender, GridViewSortEventArgs e)
        {
            List<string> filtros = new List<string>();
            filtros.Add(ddlUsuarios.SelectedItem.ToString());
            filtros.Add(ddlEventos.SelectedItem.ToString());
            filtros.Add(ddlFechaDesde.SelectedItem.ToString());
            filtros.Add(ddlFechaHasta.SelectedItem.ToString());
            filtros.Add(ddlCriticidad.SelectedItem.ToString());

            if (string.Compare("CriticidadTexto", e.SortExpression, true) == 0)
            { this.sortDirByCriticidadTexto = this.sortDirByCriticidadTexto == "ASC" ? "DESC" : "ASC"; }

            if (string.Compare("NombreUsuario", e.SortExpression, true) == 0)
            { this.sortDirByNombreUsuario = this.sortDirByNombreUsuario == "ASC" ? "DESC" : "ASC"; }

            if (string.Compare("DescripcionEvento", e.SortExpression, true) == 0)
            { this.sortDirByDescripcionEvento = this.sortDirByDescripcionEvento == "ASC" ? "DESC" : "ASC"; ; }

            if (string.Compare("FechaEvento", e.SortExpression, true) == 0)
            { this.sortDirByFechaEvento = this.sortDirByFechaEvento == "ASC" ? "DESC" : "ASC"; }       

            EnlazarGrillaBitacora(filtros, e.SortExpression);
        }
    }
}