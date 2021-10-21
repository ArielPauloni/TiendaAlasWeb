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
using System.Web.Configuration;

namespace GUI.Negocio.ABMs
{
    public partial class Caracteristica : System.Web.UI.Page, IObserver
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private PacienteCaracteristicaBLL gestorCaracteristicas = new PacienteCaracteristicaBLL();

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }
            lblTitle.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 123);
        }

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                ViewState["ErrorMsg"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 1);
                ViewState["SinPermisos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 57);
                ViewState["DatosIncorrectos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 55);
                ViewState["NoSePudoGrabar"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 59);
                ViewState["tooltipEdit"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 27);
                ViewState["tooltipConfirm"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 28);
                ViewState["tooltipUndo"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 30);
                ViewState["Paciente"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 124);
                ViewState["Genero"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 125);
                ViewState["Fuma"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 126);
                ViewState["DiasActividadDeportiva"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 127);
                ViewState["HorasRelax"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 128);
                ViewState["Edad"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 129);
                grvCaracteristicasPaciente.Caption = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 123);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                EnlazarGrillaPacientes();
            }
        }

        private void EnlazarGrillaPacientes()
        {
            grvCaracteristicasPaciente.DataSource = null;
            grvCaracteristicasPaciente.DataSource = gestorCaracteristicas.Listar();
            grvCaracteristicasPaciente.DataBind();
            grvCaracteristicasPaciente.Columns[1].Visible = false;
        }

        protected void grvCaracteristicasPaciente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvCaracteristicasPaciente.PageIndex = e.NewPageIndex;
            grvCaracteristicasPaciente.EditIndex = -1;
            EnlazarGrillaPacientes();
        }

        protected void grvCaracteristicasPaciente_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Setea EditIndex a -1: Cancela modo edición 
            grvCaracteristicasPaciente.EditIndex = -1;
            EnlazarGrillaPacientes();
        }

        protected void grvCaracteristicasPaciente_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //NewEditIndex se usa para determinar el índice a editar
            grvCaracteristicasPaciente.EditIndex = e.NewEditIndex;
            EnlazarGrillaPacientes();
        }

        protected void grvCaracteristicasPaciente_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //Busco los controles de la grilla para la fila que voy a actualizar
                Label id = grvCaracteristicasPaciente.Rows[e.RowIndex].FindControl("lbl_cod_Paciente") as Label;
                DropDownList ddlGenero = grvCaracteristicasPaciente.Rows[e.RowIndex].FindControl("ddl_Genero") as DropDownList;
                CheckBox chkFuma = grvCaracteristicasPaciente.Rows[e.RowIndex].FindControl("chk_Fuma") as CheckBox;
                DropDownList ddlDiasActivDep = grvCaracteristicasPaciente.Rows[e.RowIndex].FindControl("ddl_DiasActividadDeportiva") as DropDownList;
                DropDownList ddlHorasRel = grvCaracteristicasPaciente.Rows[e.RowIndex].FindControl("ddl_HorasRelax") as DropDownList;

                if ((ddlGenero.SelectedIndex > -1) && (ddlDiasActivDep.SelectedIndex > -1) && (ddlHorasRel.SelectedIndex > -1))
                {
                    UsuarioBE paciente = new UsuarioBE();
                    paciente.Cod_Usuario = short.Parse(id.Text);
                    PacienteCaracteristicaBE pacienteCaracteristica = new PacienteCaracteristicaBE();
                    pacienteCaracteristica.Paciente = paciente;
                    pacienteCaracteristica.Genero = short.Parse(ddlGenero.SelectedIndex.ToString());
                    pacienteCaracteristica.Fuma = chkFuma.Checked;
                    pacienteCaracteristica.DiasActividadDeportiva = short.Parse(ddlDiasActivDep.SelectedItem.Value);
                    pacienteCaracteristica.HorasRelax = short.Parse(ddlHorasRel.SelectedItem.Value);

                    int i = gestorCaracteristicas.Actualizar(pacienteCaracteristica, (UsuarioBE)Session["UsuarioAutenticado"]);
                    if (i <= 0)
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    else { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.ActualizaCaracteristicasDelPaciente, (short)EventosBE.Criticidad.Media); }
                }
                else
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["DatosIncorrectos"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                grvCaracteristicasPaciente.EditIndex = -1;
                EnlazarGrillaPacientes();
            }
            catch (SL.SinPermisosException)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void grvCaracteristicasPaciente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton controlEdit = (LinkButton)e.Row.FindControl("btn_Edit");
                if (controlEdit != null) { controlEdit.ToolTip = ViewState["tooltipEdit"].ToString(); }

                LinkButton controlUpdate = (LinkButton)e.Row.FindControl("btn_Update");
                if (controlUpdate != null)
                { controlUpdate.ToolTip = ViewState["tooltipConfirm"].ToString(); }

                LinkButton controlUndo = (LinkButton)e.Row.FindControl("btn_Undo");
                if (controlUndo != null)
                { controlUndo.ToolTip = ViewState["tooltipUndo"].ToString(); }

                Label lblGenero = (Label)e.Row.FindControl("lbl_Genero");
                if ((lblGenero != null) && (!string.IsNullOrWhiteSpace(lblGenero.Text)))
                { lblGenero.Text = Enum.GetName(typeof(PacienteCaracteristicaBE.GeneroEnum), ((PacienteCaracteristicaBE)e.Row.DataItem).Genero); }

                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    DropDownList ddlDiasActividad = (e.Row.FindControl("ddl_DiasActividadDeportiva") as DropDownList);
                    List<int> dias = new List<int>();
                    for (int i = 0; i <= 7; i++) { dias.Add(i); }
                    ddlDiasActividad.DataSource = dias;
                    ddlDiasActividad.DataBind();
                    if (!string.IsNullOrWhiteSpace(((PacienteCaracteristicaBE)e.Row.DataItem).DiasActividadDeportiva.ToString()))
                    { ddlDiasActividad.SelectedValue = ((PacienteCaracteristicaBE)e.Row.DataItem).DiasActividadDeportiva.ToString(); }
                    else { ddlDiasActividad.SelectedValue = 0.ToString(); }

                    DropDownList ddlHorasRelax = (e.Row.FindControl("ddl_HorasRelax") as DropDownList);
                    List<int> horas = new List<int>();
                    for (int i = 0; i <= 42; i++) { horas.Add(i); }
                    ddlHorasRelax.DataSource = horas;
                    ddlHorasRelax.DataBind();
                    if (!string.IsNullOrWhiteSpace(((PacienteCaracteristicaBE)e.Row.DataItem).HorasRelax.ToString()))
                    { ddlHorasRelax.SelectedValue = ((PacienteCaracteristicaBE)e.Row.DataItem).HorasRelax.ToString(); }
                    else { ddlHorasRelax.SelectedValue = 0.ToString(); }

                    DropDownList ddlGenero = (e.Row.FindControl("ddl_Genero") as DropDownList);
                    var statusStates = (from PacienteCaracteristicaBE.GeneroEnum genero in Enum.GetValues(typeof(PacienteCaracteristicaBE.GeneroEnum))
                                        select genero);
                    ddlGenero.DataSource = statusStates;
                    ddlGenero.DataBind();
                    if (((PacienteCaracteristicaBE)e.Row.DataItem).Genero != null)
                    { ddlGenero.SelectedValue = Enum.GetName(typeof(PacienteCaracteristicaBE.GeneroEnum), ((PacienteCaracteristicaBE)e.Row.DataItem).Genero); }

                    CheckBox chkFuma = (e.Row.FindControl("chk_Fuma") as CheckBox);
                    chkFuma.Enabled = true;
                }
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Text = ViewState["Paciente"].ToString();
                e.Row.Cells[3].Text = ViewState["Genero"].ToString();
                e.Row.Cells[4].Text = ViewState["Fuma"].ToString();
                e.Row.Cells[5].Text = ViewState["DiasActividadDeportiva"].ToString();
                e.Row.Cells[6].Text = ViewState["HorasRelax"].ToString();
                e.Row.Cells[7].Text = ViewState["Edad"].ToString();
            }
        }
    }
}