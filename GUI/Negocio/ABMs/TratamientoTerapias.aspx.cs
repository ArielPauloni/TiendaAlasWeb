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
    public partial class TratamientoTerapias : System.Web.UI.Page, IObserver
    {
        private TratamientoBLL gestorTratamiento = new TratamientoBLL();
        private TerapiaBLL gestorTerapia = new TerapiaBLL();
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private IdiomaSL gestorIdioma = new IdiomaSL();

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                ViewState["ErrorMsg"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 1);
                ViewState["SinPermisos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 57);
                ViewState["DatosIncorrectos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 55);
                ViewState["NoSePudoGrabar"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 59);
                ViewState["tooltipEdit"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 27);
                ViewState["tooltipUndo"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 30);
                ViewState["tooltipDelete"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 87);
                ViewState["CantidadSesiones"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 142);
                ViewState["DescripcionTerapia"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 138);
                btnGuardar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 19);
                lblTratamientos.Text = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 118);
                lblDescripcionTerapia.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 138);
                lblCantidadSesiones.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 142);
                grvTerapias.Caption = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 136);
            }
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

                EnlazarTratamientos();
                EnlazarTerapias();
            }
        }

        private void EnlazarTerapias()
        {
            ddlTerapias.DataSource = gestorTerapia.Listar();
            ddlTerapias.DataTextField = "DescripcionTerapia";
            ddlTerapias.DataValueField = "Cod_Terapia";
            ddlTerapias.DataBind();
        }

        private void EnlazarTratamientos()
        {
            ddlTratamientos.DataSource = gestorTratamiento.Listar();
            ddlTratamientos.DataTextField = "DescripcionTratamiento";
            ddlTratamientos.DataValueField = "Cod_Tratamiento";
            ddlTratamientos.DataBind();
            EnlazarGrillaTratamientoTerapias();
        }

        private void EnlazarGrillaTratamientoTerapias()
        {
            TratamientoBE trat = new TratamientoBE()
            {
                DescripcionTratamiento = ddlTratamientos.SelectedItem.Text.ToString(),
                Cod_Tratamiento = int.Parse(ddlTratamientos.SelectedItem.Value)
            };
            grvTerapias.DataSource = gestorTratamiento.ObtenerTerapiasPorTratamiento(trat);
            grvTerapias.DataBind();
            grvTerapias.Columns[1].Visible = false;
        }

        protected void grvTerapias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvTerapias.PageIndex = e.NewPageIndex;
            grvTerapias.EditIndex = -1;
            EnlazarGrillaTratamientoTerapias();
        }

        protected void ddlTratamientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnlazarGrillaTratamientoTerapias();
        }

        protected void btnGuardar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (ddlTerapias.SelectedIndex > -1 && ddlTratamientos.SelectedIndex > -1 && ddlCantidadSesiones.SelectedIndex > -10)
                {
                    TratamientoBE trat = new TratamientoBE()
                    {
                        DescripcionTratamiento = ddlTratamientos.SelectedItem.Text.ToString(),
                        Cod_Tratamiento = int.Parse(ddlTratamientos.SelectedItem.Value)
                    };
                    TerapiaBE terapia = new TerapiaBE()
                    {
                        DescripcionTerapia = ddlTerapias.SelectedItem.Text.ToString(),
                        Cod_Terapia = int.Parse(ddlTerapias.SelectedItem.Value)
                    };

                    Tuple<TerapiaBE, short> ter = new Tuple<TerapiaBE, short>(terapia, short.Parse(ddlCantidadSesiones.SelectedValue));

                    int i = gestorTratamiento.InsertarTratamientoTerapia(trat, ter, (UsuarioBE)Session["UsuarioAutenticado"]);
                    if (i <= 0)
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    else { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.CambioTerapia, (short)EventosBE.Criticidad.Baja); }
                }
                EnlazarGrillaTratamientoTerapias();
            }
            catch (SL.SinPermisosException)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void grvTerapias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvTerapias.EditIndex = e.NewEditIndex;
            EnlazarGrillaTratamientoTerapias();
        }

        protected void grvTerapias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //Busco los controles de la grilla para la fila que voy a actualizar
                Label id = grvTerapias.Rows[e.RowIndex].FindControl("lbl_Cod_Terapia") as Label;
                TerapiaBE terapia = new TerapiaBE();
                terapia.Cod_Terapia = int.Parse(id.Text);

                TratamientoBE trat = new TratamientoBE()
                {
                    DescripcionTratamiento = ddlTratamientos.SelectedItem.Text.ToString(),
                    Cod_Tratamiento = int.Parse(ddlTratamientos.SelectedItem.Value)
                };

                int i = gestorTratamiento.EliminarTratamientoTerapia(trat, terapia, (UsuarioBE)Session["UsuarioAutenticado"]);
                if (i <= 0)
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                else { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.CambioRelacionTratamientoTerapia, (short)EventosBE.Criticidad.Media); }

                grvTerapias.EditIndex = -1;
                EnlazarGrillaTratamientoTerapias();
            }
            catch (SL.SinPermisosException)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void grvTerapias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton controlEdit = (LinkButton)e.Row.FindControl("btn_Edit");
                if (controlEdit != null) { controlEdit.ToolTip = ViewState["tooltipEdit"].ToString(); }

                LinkButton controlUndo = (LinkButton)e.Row.FindControl("btn_Undo");
                if (controlUndo != null)
                { controlUndo.ToolTip = ViewState["tooltipUndo"].ToString(); }

                LinkButton controlDelete = (LinkButton)e.Row.FindControl("btn_Delete");
                if (controlDelete != null)
                { controlDelete.ToolTip = ViewState["tooltipDelete"].ToString(); }
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Text = ViewState["CantidadSesiones"].ToString();
                e.Row.Cells[3].Text = ViewState["DescripcionTerapia"].ToString();
            }
        }

        protected void grvTerapias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvTerapias.EditIndex = -1;
            EnlazarGrillaTratamientoTerapias();
        }
    }
}