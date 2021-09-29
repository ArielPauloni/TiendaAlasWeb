using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using BE;
using BLL;
using System.Text.RegularExpressions;

namespace GUI.Servicios.Usuarios
{
    public partial class ABMUsuario : System.Web.UI.Page, IObserver
    {
        private UsuarioBLL gestorUsuario = new UsuarioBLL();
        private TipoUsuarioBLL gestorTipoUsuario = new TipoUsuarioBLL();
        private EncriptacionSL gestorEncriptacion = new EncriptacionSL();
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private string eMailPattern = @"^[\w._%-]+@[\w.-]+\.[a-zA-Z]{2,4}$";

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }

        }

        public void TraducirTexto()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                EnlazarGrillaUsuarios();
            }
        }

        private void EnlazarGrillaUsuarios()
        {
            try
            {
                grvUsuarios.DataSource = gestorUsuario.Listar();
                grvUsuarios.DataBind();
                grvUsuarios.Columns[1].Visible = false;
                grvUsuarios.Columns[9].Visible = false;
                grvUsuarios.Columns[10].Visible = false;
                grvUsuarios.Columns[12].Visible = false;
                grvUsuarios.Columns[13].Visible = false;
            }
            catch (BLL.UsuarioModificadoException ex)
            {
                UC_MensajeModal.SetearMensaje("-Error" + "\r\n" + ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void grvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvUsuarios.PageIndex = e.NewPageIndex;
            grvUsuarios.EditIndex = -1;
            EnlazarGrillaUsuarios();
        }

        protected void btnCrearNuevoUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"~/Negocio/Usuario/NuevoUsuario.aspx");
        }

        protected void grvUsuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvUsuarios.EditIndex = -1;
            EnlazarGrillaUsuarios();
        }

        protected void grvUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvUsuarios.EditIndex = e.NewEditIndex;
            EnlazarGrillaUsuarios();
        }

        protected void grvUsuarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //Busco los controles de la grilla para la fila que voy a actualizar
                Label lblCod_Usuario = grvUsuarios.Rows[e.RowIndex].FindControl("lbl_Cod_Usuario") as Label;
                Label lblPassword = grvUsuarios.Rows[e.RowIndex].FindControl("lbl_Password") as Label;
                Label lblInactivo = grvUsuarios.Rows[e.RowIndex].FindControl("lbl_Inactivo") as Label;
                Label lblIntentosEquivocados = grvUsuarios.Rows[e.RowIndex].FindControl("lbl_IntentosEquivocados") as Label;
                Label lblUltimoLogin = grvUsuarios.Rows[e.RowIndex].FindControl("lbl_UltimoLogin") as Label;

                TextBox txtApellido = grvUsuarios.Rows[e.RowIndex].FindControl("txt_Apellido") as TextBox;
                TextBox txtNombre = grvUsuarios.Rows[e.RowIndex].FindControl("txt_Nombre") as TextBox;
                TextBox txtAlias = grvUsuarios.Rows[e.RowIndex].FindControl("txt_Alias") as TextBox;
                TextBox txtTelefono = grvUsuarios.Rows[e.RowIndex].FindControl("txt_Telefono") as TextBox;
                TextBox txtMail = grvUsuarios.Rows[e.RowIndex].FindControl("txt_Mail") as TextBox;
                TextBox txtFechaNacimiento = grvUsuarios.Rows[e.RowIndex].FindControl("txt_FechaNacimiento") as TextBox;
                DropDownList ddlTipoUsuario = grvUsuarios.Rows[e.RowIndex].FindControl("ddl_TipoUsuario") as DropDownList;
                CheckBox chkBloqueado = grvUsuarios.Rows[e.RowIndex].FindControl("chk_Bloqueado") as CheckBox;

                if ((!string.IsNullOrWhiteSpace(txtApellido.Text)) && (!string.IsNullOrWhiteSpace(txtNombre.Text)) &&
                     (!string.IsNullOrWhiteSpace(txtAlias.Text)) && (!string.IsNullOrWhiteSpace(txtMail.Text)) &&
                     (ddlTipoUsuario.SelectedIndex > -1))
                {
                    if (!Regex.IsMatch(txtMail.Text, eMailPattern))
                    {
                        UC_MensajeModal.SetearMensaje("-Email Invalido");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }

                    UsuarioBE usuario = new UsuarioBE();
                    usuario.Cod_Usuario = int.Parse(lblCod_Usuario.Text);
                    usuario.Apellido = txtApellido.Text;
                    usuario.Nombre = txtNombre.Text;
                    usuario.Alias = txtAlias.Text;
                    usuario.Contraseña = lblPassword.Text;
                    usuario.Telefono = txtTelefono.Text;
                    usuario.Mail = txtMail.Text;

                    if ((chkBloqueado.Checked) && (short.Parse(lblIntentosEquivocados.Text) < 3))
                    { //Lo tengo que bloquear (IntentosEquivocados = 3)
                        usuario.IntentosEquivocados = 3;
                    }
                    else if ((!chkBloqueado.Checked) && (short.Parse(lblIntentosEquivocados.Text) > 2))
                    { //Lo tengo que desbloquear (IntentosEquivocados = 0)
                        usuario.IntentosEquivocados = 0;
                    }
                    else
                    {   //No cambia nada al respecto
                        usuario.IntentosEquivocados = short.Parse(lblIntentosEquivocados.Text);
                    }
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(lblUltimoLogin.Text))
                        { usuario.UltimoLogin = Convert.ToDateTime(lblUltimoLogin.Text); }
                        else { usuario.UltimoLogin = default(DateTime?); }
                    }
                    catch (Exception) { usuario.UltimoLogin = default(DateTime?); }

                    try
                    {
                        if (!string.IsNullOrWhiteSpace(txtFechaNacimiento.Text))
                        { usuario.FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text); }
                        else { usuario.FechaNacimiento = default(DateTime?); }
                    }
                    catch (Exception) { usuario.FechaNacimiento = default(DateTime?); }
                    TipoUsuarioBE tipoUsuario = new TipoUsuarioBE
                    {
                        Descripcion_Tipo = ddlTipoUsuario.SelectedItem.Text.ToString(),
                        Cod_Tipo = short.Parse(ddlTipoUsuario.SelectedItem.Value)
                    };
                    usuario.TipoUsuario = tipoUsuario;
                    usuario.Inactivo = Convert.ToBoolean(lblInactivo.Text);
                    try
                    {
                        int r = gestorUsuario.Editar(usuario, (UsuarioBE)Session["UsuarioAutenticado"]);
                        if (r == 0)
                        {
                            UC_MensajeModal.SetearMensaje("-No se Pudo Grabar");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                        }
                        else
                        {
                            gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.ModificaciónUsuario, (short)EventosBE.Criticidad.Baja);
                        }
                    }
                    catch (SinPermisosException)
                    {
                        UC_MensajeModal.SetearMensaje("-Sin Permisos");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    if (usuario.Cod_Usuario == ((UsuarioBE)Session["UsuarioAutenticado"]).Cod_Usuario)
                    {
                        usuario.Permisos = ((UsuarioBE)Session["UsuarioAutenticado"]).Permisos;
                        usuario.Idioma = ((UsuarioBE)Session["UsuarioAutenticado"]).Idioma;
                        Session["UsuarioAutenticado"] = usuario;
                    }
                }
                else
                {
                    UC_MensajeModal.SetearMensaje("-No se puede Grabar Con Datos Vacios");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
            }
            catch (Exception ex)
            {
                UC_MensajeModal.SetearMensaje("-Error: " + "\r\n" + ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
            grvUsuarios.EditIndex = -1;
            EnlazarGrillaUsuarios();
        }

        protected void grvUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblCod_Usuario = grvUsuarios.Rows[e.RowIndex].FindControl("lbl_Cod_Usuario") as Label;
                DropDownList ddlTipoUsuario = grvUsuarios.Rows[e.RowIndex].FindControl("ddl_TipoUsuario") as DropDownList;
                UsuarioBE usuario = new UsuarioBE();
                usuario.Cod_Usuario = int.Parse(lblCod_Usuario.Text);
                TipoUsuarioBE tipoUsuario = new TipoUsuarioBE
                {
                    Descripcion_Tipo = ddlTipoUsuario.SelectedItem.Text.ToString(),
                    Cod_Tipo = short.Parse(ddlTipoUsuario.SelectedItem.Value)
                };
                usuario.TipoUsuario = tipoUsuario;

                try
                {
                    int r = gestorUsuario.Eliminar(usuario, (UsuarioBE)Session["UsuarioAutenticado"]);
                    if (r == 0)
                    {
                        UC_MensajeModal.SetearMensaje("-No se Pudo Grabar");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    else
                    {
                        gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.BajaDeUsuario, (short)EventosBE.Criticidad.Media);
                    }
                }
                catch (SinPermisosException)
                {
                    UC_MensajeModal.SetearMensaje("-Sin Permisos");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                if (usuario.Cod_Usuario == ((UsuarioBE)Session["UsuarioAutenticado"]).Cod_Usuario)
                {
                    usuario.Permisos = ((UsuarioBE)Session["UsuarioAutenticado"]).Permisos;
                    usuario.Idioma = ((UsuarioBE)Session["UsuarioAutenticado"]).Idioma;
                    Session["UsuarioAutenticado"] = usuario;
                }
            }
            catch (Exception ex)
            {
                UC_MensajeModal.SetearMensaje("-Error: " + "\r\n" + ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }

            grvUsuarios.EditIndex = -1;
            EnlazarGrillaUsuarios();
        }

        protected void grvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton controlEdit = (LinkButton)e.Row.FindControl("btn_Edit");
                if (controlEdit != null) { controlEdit.ToolTip = "-Editar"; }

                LinkButton controlUpdate = (LinkButton)e.Row.FindControl("btn_Update");
                if (controlUpdate != null)
                { controlUpdate.ToolTip = "-Confirmar"; }

                LinkButton controlUndo = (LinkButton)e.Row.FindControl("btn_Undo");
                if (controlUndo != null)
                { controlUndo.ToolTip = "-Deshacer"; }

                LinkButton controlDelete = (LinkButton)e.Row.FindControl("btn_Delete");
                if (controlDelete != null)
                { controlDelete.ToolTip = "-Eliminar"; }

                if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
                {
                    DropDownList ddlTipoUsuario = (e.Row.FindControl("ddl_TipoUsuario") as DropDownList);
                    ddlTipoUsuario.DataSource = gestorTipoUsuario.Listar();
                    ddlTipoUsuario.DataTextField = "Descripcion_Tipo";
                    ddlTipoUsuario.DataValueField = "Cod_Tipo";
                    ddlTipoUsuario.DataBind();
                    ddlTipoUsuario.SelectedValue = ((UsuarioBE)e.Row.DataItem).TipoUsuario.Cod_Tipo.ToString();

                    CheckBox chkBloqueado = (e.Row.FindControl("chk_Bloqueado") as CheckBox);
                    chkBloqueado.Enabled = true;

                    if (((UsuarioBE)e.Row.DataItem).FechaNacimiento.HasValue)
                    {
                        TextBox txtFechaNacimiento = (e.Row.FindControl("txt_FechaNacimiento") as TextBox);
                        txtFechaNacimiento.Text = ((UsuarioBE)e.Row.DataItem).FechaNacimiento.Value.ToString("yyyy-MM-dd");
                    }
                }
            }
        }
    }
}