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
    public partial class Tratamiento : System.Web.UI.Page, IObserver
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();
        private TratamientoBLL gestorTratamiento = new TratamientoBLL();
        private BitacoraSL gestorBitacora = new BitacoraSL();

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
                btnGuardar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 19);
                btnCancelar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 20);
                lblTitle.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 118);
                lblDescripcionTratamiento.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 119);
                grvTratamiento.Caption = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 118);
                grvTratamiento.Columns[2].HeaderText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 63);
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
            grvTratamiento.DataSource = null;
            grvTratamiento.DataSource = gestorTratamiento.Listar();
            grvTratamiento.DataBind();
            grvTratamiento.Columns[1].Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDescripcionTratamiento.Text))
            {
                TratamientoBE tratamiento = new TratamientoBE();
                tratamiento.DescripcionTratamiento = txtDescripcionTratamiento.Text;
                try
                {
                    int i = gestorTratamiento.Insertar(tratamiento, (UsuarioBE)Session["UsuarioAutenticado"]);

                    if (i <= 0)
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    else { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.TratamientoCreado, (short)EventosBE.Criticidad.Baja); }
                }
                catch (SL.SinPermisosException)
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                catch (Exception ex)
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Error, ViewState["ErrorMsg"].ToString() + "<br>" + ex.Message);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
            }
            else
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["DatosIncorrectos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
            txtDescripcionTratamiento.Text = string.Empty;
            EnlazarGrillaTratamientos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtDescripcionTratamiento.Text = string.Empty;
        }

        protected void grvTratamiento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvTratamiento.PageIndex = e.NewPageIndex;
            grvTratamiento.EditIndex = -1;
            EnlazarGrillaTratamientos();
        }

        protected void grvTratamiento_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Setea EditIndex a -1: Cancela modo edición 
            grvTratamiento.EditIndex = -1;
            EnlazarGrillaTratamientos();
        }

        protected void grvTratamiento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //NewEditIndex se usa para determinar el índice a editar
            grvTratamiento.EditIndex = e.NewEditIndex;
            EnlazarGrillaTratamientos();
        }

        protected void grvTratamiento_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //Busco los controles de la grilla para la fila que voy a actualizar
                Label id = grvTratamiento.Rows[e.RowIndex].FindControl("lbl_cod_Tratamiento") as Label;
                TextBox descripcion = grvTratamiento.Rows[e.RowIndex].FindControl("txt_Descripcion") as TextBox;

                if (!string.IsNullOrWhiteSpace(descripcion.Text))
                {
                    //Grabar el nuevo tratamiento
                    TratamientoBE tratamiento = new TratamientoBE();
                    tratamiento.Cod_Tratamiento = short.Parse(id.Text);
                    tratamiento.DescripcionTratamiento = descripcion.Text;
                   
                    int i = gestorTratamiento.Actualizar(tratamiento, (UsuarioBE)Session["UsuarioAutenticado"]);
                    if (i <= 0)
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    else { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.CambioDescripcionTratamiento, (short)EventosBE.Criticidad.Baja); }
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
            }
        }
    }
}