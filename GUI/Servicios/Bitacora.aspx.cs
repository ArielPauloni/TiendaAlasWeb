﻿using System;
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

namespace GUI.Servicios.Bitacora
{
    public partial class Bitacora : System.Web.UI.Page, IObserver
    {
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private PersistenciaSL gestorPersistencia = new PersistenciaSL();
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

        public void TraducirTexto()
        {
            lblUsuario.Text = "-Usuario";
            lblFechaDesde.Text = "-Fecha Desde";
            lblFechaHasta.Text = "-Fecha Hasta";
            lblEvento.Text = "-Evento";
            lblCriticidad.Text = "-Criticidad";

            ViewState["MostrarFiltros"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 39);
            ViewState["OcultarFiltros"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 42);
            btnMostrarFiltros.Attributes.Add("title", ViewState["MostrarFiltros"].ToString());
            ViewState["ErrorMsg"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 1);
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

                List<string> listaVacia = new List<string>();
                EnlazarGrillaBitacora(listaVacia);
            }
        }

        private void EnlazarGrillaBitacora(List<string> filtros)
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
            ListaOrdenable<BitacoraBE> bitacoraOrdenable = new ListaOrdenable<BitacoraBE>();
            bitacoraOrdenable = new ListaOrdenable<BitacoraBE>(datosBitacora);
            grvBitacora.DataSource = bitacoraOrdenable;
            grvBitacora.DataBind();

            grvBitacora.Columns[(int)grvBitacoraColumns.Cod_Usuario].Visible = false;
            grvBitacora.Columns[(int)grvBitacoraColumns.Cod_Evento].Visible = false;
            grvBitacora.Columns[(int)grvBitacoraColumns.Criticidad].Visible = false;

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

        //protected void grvBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    grvBitacora.PageIndex = e.NewPageIndex;
        //    grvBitacora.EditIndex = -1;
        //    EnlazarGrillaBitacora();
        //}

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
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Error, "-No se pudo grabar. Error: " + "<br>" + ex.Message);
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

            EnlazarGrillaBitacora(filtros);
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        private void LimpiarFiltros()
        {
            List<string> listaVacia = new List<string>();
            EnlazarGrillaBitacora(listaVacia);
        }
    }
}