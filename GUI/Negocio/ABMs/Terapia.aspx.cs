using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;
using SL.PatronObserver;
using SL;

namespace GUI.Negocio.ABMs
{
    public partial class Terapia : System.Web.UI.Page, IObserver
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();
        private TerapiaBLL gestorTerapia = new TerapiaBLL();
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
                ViewState["Descripcion"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 138);
                ViewState["Duracion"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 139);
                ViewState["Costo"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 141);
                btnGuardar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 19);
                btnCancelar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 20);
                lblTitle.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 136);
                lblDescripcionTerapia.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 138);
                lblDuracion.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 139);
                lblPrecio.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 141);
                grvTerapia.Caption = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 136);
                grvTerapia.Columns[2].HeaderText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 137);
                grvTerapia.Columns[3].HeaderText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 141);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                EnlazarGrillaTerapias();
            }
        }

        private void EnlazarGrillaTerapias()
        {
            grvTerapia.DataSource = null;
            grvTerapia.DataSource = gestorTerapia.Listar();
            grvTerapia.DataBind();
            grvTerapia.Columns[1].Visible = false;
        }

        protected void btnGuardar_ServerClick(object sender, EventArgs e)
        {
            float precio = 0;
            try { precio = float.Parse(txtPrecio.Text); }
            catch (Exception) { precio = 0; }

            if (!string.IsNullOrWhiteSpace(txtDescripcionTerapia.Text) && ddlDuracion.SelectedIndex > -1 && precio > 0)
            {
                TerapiaBE terapia = new TerapiaBE();
                terapia.DescripcionTerapia = txtDescripcionTerapia.Text;
                terapia.Duracion = int.Parse(ddlDuracion.SelectedValue.ToString());
                terapia.Precio = precio;
                try
                {
                    int i = gestorTerapia.Insertar(terapia, (UsuarioBE)Session["UsuarioAutenticado"]);

                    if (i <= 0)
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    else { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.TerapiaCreada, (short)EventosBE.Criticidad.Baja); }
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
            txtDescripcionTerapia.Text = string.Empty;
            EnlazarGrillaTerapias();
        }

        protected void btnCancelar_ServerClick(object sender, EventArgs e)
        {
            txtDescripcionTerapia.Text = string.Empty;
        }

        protected void grvTerapia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvTerapia.PageIndex = e.NewPageIndex;
            grvTerapia.EditIndex = -1;
            EnlazarGrillaTerapias();
        }

        protected void grvTerapia_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvTerapia.EditIndex = -1;
            EnlazarGrillaTerapias();
        }

        protected void grvTerapia_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvTerapia.EditIndex = e.NewEditIndex;
            EnlazarGrillaTerapias();
        }

        protected void grvTerapia_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //Busco los controles de la grilla para la fila que voy a actualizar
                Label id = grvTerapia.Rows[e.RowIndex].FindControl("lbl_cod_Terapia") as Label;
                TextBox descripcion = grvTerapia.Rows[e.RowIndex].FindControl("txt_Descripcion") as TextBox;
                DropDownList ddlDuracion = grvTerapia.Rows[e.RowIndex].FindControl("ddl_Duracion") as DropDownList;
                TextBox txtPrecio = grvTerapia.Rows[e.RowIndex].FindControl("txt_Precio") as TextBox;
                float precio = 0;
                try { precio = float.Parse(txtPrecio.Text); }
                catch (Exception) { precio = 0; }

                if (!string.IsNullOrWhiteSpace(descripcion.Text) && ddlDuracion.SelectedIndex > -1 && precio > 0)
                {
                    //Grabar la nuevo terapia
                    TerapiaBE terapia = new TerapiaBE();
                    terapia.Cod_Terapia = int.Parse(id.Text);
                    terapia.Duracion = short.Parse(ddlDuracion.SelectedValue.ToString());
                    terapia.DescripcionTerapia = descripcion.Text;
                    terapia.Precio = precio;

                    int i = gestorTerapia.Actualizar(terapia, (UsuarioBE)Session["UsuarioAutenticado"]);
                    if (i <= 0)
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    else { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.CambioTerapia, (short)EventosBE.Criticidad.Baja); }
                }
                else
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["DatosIncorrectos"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                grvTerapia.EditIndex = -1;
                EnlazarGrillaTerapias();
            }
            catch (SL.SinPermisosException)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void grvTerapia_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    DropDownList ddlDuracion = (e.Row.FindControl("ddl_Duracion") as DropDownList);
                    ddlDuracion.Items.Add("15");
                    ddlDuracion.Items.Add("20");
                    ddlDuracion.Items.Add("25");
                    ddlDuracion.Items.Add("30");
                    ddlDuracion.Items.Add("40");
                    ddlDuracion.Items.Add("45");
                    ddlDuracion.Items.Add("60");
                    ddlDuracion.Items.Add("75");
                    ddlDuracion.Items.Add("90");
                    ddlDuracion.Items.Add("105");
                    ddlDuracion.Items.Add("120");
                    ddlDuracion.DataBind();
                    ddlDuracion.SelectedValue = ((TerapiaBE)e.Row.DataItem).Duracion.ToString();
                }
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Text = ViewState["Descripcion"].ToString();
                e.Row.Cells[3].Text = ViewState["Duracion"].ToString();
                e.Row.Cells[4].Text = ViewState["Costo"].ToString();
            }
        }
    }
}