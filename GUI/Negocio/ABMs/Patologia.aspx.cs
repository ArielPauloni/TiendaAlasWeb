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
    public partial class Patologia : System.Web.UI.Page, IObserver
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();
        private PatologiaBLL gestorPatologia = new PatologiaBLL();
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
                ViewState["Accion"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 77);
                btnGuardar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 19);
                btnCancelar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 20);
                lblDescripcionPatologia.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 120);
                lblTitle.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 121);
                grvPatologia.Caption = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 121);
                grvPatologia.Columns[2].HeaderText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 63);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                EnlazarGrillaPatologias();
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        private void EnlazarGrillaPatologias()
        {
            grvPatologia.DataSource = null;
            grvPatologia.DataSource = gestorPatologia.Listar();
            grvPatologia.DataBind();
            grvPatologia.Columns[1].Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDescripcionPatologia.Text))
            {
                PatologiaBE Patologia = new PatologiaBE();
                Patologia.DescripcionPatologia = txtDescripcionPatologia.Text;
                try
                {
                    int i = gestorPatologia.Insertar(Patologia, (UsuarioBE)Session["UsuarioAutenticado"]);

                    if (i <= 0)
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    else { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.PatologiaCreada, (short)EventosBE.Criticidad.Baja); }
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
            txtDescripcionPatologia.Text = string.Empty;
            EnlazarGrillaPatologias();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtDescripcionPatologia.Text = string.Empty;
        }

        protected void grvPatologia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvPatologia.PageIndex = e.NewPageIndex;
            grvPatologia.EditIndex = -1;
            EnlazarGrillaPatologias();
        }

        protected void grvPatologia_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Setea EditIndex a -1: Cancela modo edición 
            grvPatologia.EditIndex = -1;
            EnlazarGrillaPatologias();
        }

        protected void grvPatologia_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //NewEditIndex se usa para determinar el índice a editar
            grvPatologia.EditIndex = e.NewEditIndex;
            EnlazarGrillaPatologias();
        }

        protected void grvPatologia_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //Busco los controles de la grilla para la fila que voy a actualizar
                Label id = grvPatologia.Rows[e.RowIndex].FindControl("lbl_cod_Patologia") as Label;
                TextBox descripcion = grvPatologia.Rows[e.RowIndex].FindControl("txt_Descripcion") as TextBox;

                if (!string.IsNullOrWhiteSpace(descripcion.Text))
                {
                    //Grabar el nuevo Patologia
                    PatologiaBE Patologia = new PatologiaBE();
                    Patologia.Cod_Patologia = short.Parse(id.Text);
                    Patologia.DescripcionPatologia = descripcion.Text;

                    int i = gestorPatologia.Actualizar(Patologia, (UsuarioBE)Session["UsuarioAutenticado"]);
                    if (i <= 0)
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    else { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.CambioDescripcionPatologia, (short)EventosBE.Criticidad.Baja); }
                }
                else
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["DatosIncorrectos"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                grvPatologia.EditIndex = -1;
                EnlazarGrillaPatologias();
            }
            catch (SL.SinPermisosException)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void grvPatologia_RowDataBound(object sender, GridViewRowEventArgs e)
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
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = ViewState["Accion"].ToString();
            }
        }
    }
}