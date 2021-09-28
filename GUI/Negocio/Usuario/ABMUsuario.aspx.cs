using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using BE;
using BLL;

namespace GUI.Servicios.Usuarios
{
    public partial class ABMUsuario : System.Web.UI.Page, IObserver
    {
        private UsuarioBLL gestorUsuario = new UsuarioBLL();
        private TipoUsuarioBLL gestorTipoUsuario = new TipoUsuarioBLL();

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
            grvUsuarios.DataSource = gestorUsuario.Listar();
            grvUsuarios.DataBind();
            grvUsuarios.Columns[1].Visible = false;
        }

        protected void grvUsuarios_PageIndexChanging(object sender, EventArgs e)
        {

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



            grvUsuarios.EditIndex = -1;
            EnlazarGrillaUsuarios();
        }

        protected void grvUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


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

                    if (((UsuarioBE)e.Row.DataItem).FechaNacimiento.HasValue)
                    {
                        TextBox txtFechaNacimiento = (e.Row.FindControl("txt_FechaNacimiento") as TextBox);
                        txtFechaNacimiento.Text = ((UsuarioBE)e.Row.DataItem).FechaNacimiento.Value.ToString("dd/MM/yyyy");
                    }
                }
            }
        }
    }
}