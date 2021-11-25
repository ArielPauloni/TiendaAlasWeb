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

namespace GUI.Negocio.ABMs
{
    public partial class CalificarTratamiento : System.Web.UI.Page, IObserver
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private PacienteBLL gestorPaciente = new PacienteBLL();
        private TratamientoBLL gestorTratamiento = new TratamientoBLL();

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
                ViewState["tooltipEdit"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 27);
                ViewState["tooltipConfirm"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 28);
                ViewState["tooltipUndo"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 30);
                ViewState["Tratamiento"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 133);
                ViewState["Calificacion"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 144);
                lblTitle.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 143);
                grvTratamiento.Caption = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 118);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                EnlazarGrillaTratamientos();
            }
        }

        private void EnlazarGrillaTratamientos()
        {
            PacienteBE p = new PacienteBE()
            {
                Cod_Usuario = ((UsuarioBE)Session["UsuarioAutenticado"]).Cod_Usuario
            };
            grvTratamiento.DataSource = gestorPaciente.ListarTratamientosPorPaciente(p);
            grvTratamiento.DataBind();
            grvTratamiento.Columns[1].Visible = false;
        }

        protected void grvTratamiento_RowDataBound(object sender, GridViewRowEventArgs e)
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

                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    DropDownList ddlCalificacion = (e.Row.FindControl("ddl_Calificacion") as DropDownList);
                    List<int> calificacion = new List<int>();
                    for (int i = 0; i <= 10; i++) { calificacion.Add(i); }
                    ddlCalificacion.DataSource = calificacion;
                    ddlCalificacion.DataBind();
                    if (!string.IsNullOrWhiteSpace(((TratamientoBE)e.Row.DataItem).Calificacion.ToString()))
                    { ddlCalificacion.SelectedValue = ((TratamientoBE)e.Row.DataItem).Calificacion.ToString(); }
                    else { ddlCalificacion.SelectedValue = 5.ToString(); }
                }
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Text = ViewState["Tratamiento"].ToString();
                e.Row.Cells[3].Text = ViewState["Calificacion"].ToString();
            }
        }

        protected void grvTratamiento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvTratamiento.PageIndex = e.NewPageIndex;
            grvTratamiento.EditIndex = -1;
            EnlazarGrillaTratamientos();
        }

        protected void grvTratamiento_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvTratamiento.EditIndex = -1;
            EnlazarGrillaTratamientos();
        }

        protected void grvTratamiento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvTratamiento.EditIndex = e.NewEditIndex;
            EnlazarGrillaTratamientos();
        }

        protected void grvTratamiento_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //Busco los controles de la grilla para la fila que voy a actualizar
                Label id = grvTratamiento.Rows[e.RowIndex].FindControl("lbl_Cod_Tratamiento") as Label;
                DropDownList ddlCalificacion = grvTratamiento.Rows[e.RowIndex].FindControl("ddl_Calificacion") as DropDownList;
               
                if (ddlCalificacion.SelectedIndex > -1)
                {
                    PacienteBE paciente = new PacienteBE();
                    paciente.Cod_Usuario = ((UsuarioBE)Session["UsuarioAutenticado"]).Cod_Usuario;
                    TratamientoBE tratamiento = new TratamientoBE();
                    tratamiento.Cod_Tratamiento = int.Parse(id.Text);
                    tratamiento.Calificacion = short.Parse(ddlCalificacion.SelectedIndex.ToString());
                    
                    int i = gestorTratamiento.GrabarCalificacion(tratamiento, paciente, (UsuarioBE)Session["UsuarioAutenticado"]);
                    if (i <= 0)
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    else { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.CalificarTratamiento, (short)EventosBE.Criticidad.Media); }
                }
                else
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["DatosIncorrectos"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                grvTratamiento.EditIndex = -1;
                EnlazarGrillaTratamientos();
            }
            catch (SL.SinPermisosException)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }
    }
}