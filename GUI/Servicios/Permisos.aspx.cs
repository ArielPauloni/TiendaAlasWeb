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

namespace GUI.Servicios.Permisos
{
    public partial class Permisos : System.Web.UI.Page, IObserver
    {
        private PermisoSL gestorPermiso = new PermisoSL();
        private TipoUsuarioBLL gestorTipoUsuario = new TipoUsuarioBLL();
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();
        private BitacoraSL gestorBitacora = new BitacoraSL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                EnlazarArbolPermisos();
                ddlTipoUsuarios.DataSource = gestorTipoUsuario.Listar();
                ddlTipoUsuarios.DataTextField = "Descripcion_Tipo";
                ddlTipoUsuarios.DataValueField = "Cod_Tipo";
                ddlTipoUsuarios.DataBind();
                TipoUsuarioBE tipoUsuario = new TipoUsuarioBE
                {
                    Descripcion_Tipo = ddlTipoUsuarios.SelectedItem.Text.ToString(),
                    Cod_Tipo = short.Parse(ddlTipoUsuarios.SelectedItem.Value)
                };
                EnlazarArbolPermisosPorTipoUsuario(tipoUsuario);
                //ddlTipoUsuarios.SelectedIndex = ddlTipoUsuarios.FindStringExact(SesionSL.Instancia.Usuario.TipoUsuario.Descripcion_Tipo);
                //EnlazarArbolPermisosPorTipoUsuario((TipoUsuarioBE)ddlTipoUsuarios.SelectedItem);
            }
        }

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }

        }

        public void TraducirTexto()
        {

        }

        private void EnlazarArbolPermisos()
        {
            List<SL.PatronComposite.ComponentPermiso> components = gestorPermiso.ListarPermisosArbol((UsuarioBE)Session["UsuarioAutenticado"]);
            if (components != null)
            {
                trvPermisos.Nodes.Clear();

                foreach (SL.PatronComposite.ComponentPermiso component in components)
                {
                    component.Mostrar(trvPermisos.Nodes);
                }
                trvPermisos.ExpandAll();
            }
            else
            {
                //Sin permisos:
                //TODO: Setear un ViewState para el texto sin permisos y usarlo en estos mensajes
                UC_MensajeModal.SetearMensaje("-Sin permisos");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        private void EnlazarArbolPermisosPorTipoUsuario(TipoUsuarioBE tipoUsuario)
        {
            List<SL.PatronComposite.ComponentPermiso> components = gestorPermiso.ListarPermisosPorTipoUsuario(tipoUsuario, (UsuarioBE)Session["UsuarioAutenticado"]);
            if (components != null)
            {
                trvPermisosUsuario.Nodes.Clear();

                foreach (SL.PatronComposite.ComponentPermiso component in components)
                {
                    component.Mostrar(trvPermisosUsuario.Nodes);
                }
                trvPermisosUsuario.ExpandAll();
            }
            else
            {
                //Sin permisos:

            }
        }

        protected void trvPermisos_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (divEditarPermisos.Visible)
            {
                PermisoBE permisoRelacionado = new PermisoBE();
                if (string.IsNullOrWhiteSpace(txtPermisoPadre.Text))
                {
                    permisoRelacionado.CodPermiso = int.Parse(trvPermisos.SelectedNode.Value);
                    permisoRelacionado.DescripcionPermiso = trvPermisos.SelectedNode.Text;
                    Session["PermisoPadre"] = permisoRelacionado;
                    txtPermisoPadre.Text = trvPermisos.SelectedNode.Text;
                }
                else if (string.IsNullOrWhiteSpace(txtPermisoHijo.Text))
                {
                    permisoRelacionado.CodPermiso = int.Parse(trvPermisos.SelectedNode.Value);
                    permisoRelacionado.DescripcionPermiso = trvPermisos.SelectedNode.Text;
                    Session["PermisoHijo"] = permisoRelacionado;
                    txtPermisoHijo.Text = trvPermisos.SelectedNode.Text;
                }
            }
        }

        protected void chkEditarPermisos_CheckedChanged(object sender, EventArgs e)
        {
            divEditarPermisos.Visible = chkEditarPermisos.Checked;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNuevoPermiso.Text = string.Empty;
            txtPermisoPadre.Text = string.Empty;
            txtPermisoHijo.Text = string.Empty;
            Session["PermisoPadre"] = null;
            Session["PermisoHijo"] = null;
        }

        protected void btnAsignarPermiso_Click(object sender, EventArgs e)
        {
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Asignar Permisos"), (UsuarioBE)Session["UsuarioAutenticado"]))
            {
                foreach (TreeNode nodo in trvPermisos.Nodes)
                {
                    PermisoTipoUsuario(nodo, false);
                }
                TipoUsuarioBE tipoUsuario = new TipoUsuarioBE
                {
                    Descripcion_Tipo = ddlTipoUsuarios.SelectedItem.Text.ToString(),
                    Cod_Tipo = short.Parse(ddlTipoUsuarios.SelectedItem.Value)
                };
                EnlazarArbolPermisosPorTipoUsuario(tipoUsuario);
                Subject.Notify();
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                //Sin permisos
            }
        }

        protected void btnQuitarPermiso_Click(object sender, EventArgs e)
        {
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Quitar Permisos"), (UsuarioBE)Session["UsuarioAutenticado"]))
            {
                foreach (TreeNode nodo in trvPermisosUsuario.Nodes)
                {
                    PermisoTipoUsuario(nodo, true);
                }
                TipoUsuarioBE tipoUsuario = new TipoUsuarioBE
                {
                    Descripcion_Tipo = ddlTipoUsuarios.SelectedItem.Text.ToString(),
                    Cod_Tipo = short.Parse(ddlTipoUsuarios.SelectedItem.Value)
                };
                EnlazarArbolPermisosPorTipoUsuario(tipoUsuario);
                Subject.Notify();
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                //Sin permisos
            }
        }

        private void PermisoTipoUsuario(TreeNode treeNode, Boolean eliminar)
        {
            if (treeNode.Checked)
            {
                PermisoBE permiso = new PermisoBE();
                permiso.CodPermiso = int.Parse(treeNode.Value);
                permiso.DescripcionPermiso = treeNode.Text;
                TipoUsuarioBE tipoUsuario = new TipoUsuarioBE
                {
                    Descripcion_Tipo = ddlTipoUsuarios.SelectedItem.Text.ToString(),
                    Cod_Tipo = short.Parse(ddlTipoUsuarios.SelectedItem.Value)
                };
                if (eliminar)
                {
                    if ((gestorPermiso.EliminarPermisoPorTipoUsuario(tipoUsuario, permiso, (UsuarioBE)Session["UsuarioAutenticado"])) < 0)
                    {
                        //Sin permisos:

                    }
                }
                else
                {
                    if ((gestorPermiso.InsertarPermisoPorTipoUsuario(tipoUsuario, permiso, (UsuarioBE)Session["UsuarioAutenticado"])) < 0)
                    {
                        //Sin permisos:

                    }
                }
            }
            ////TODO: Revisar esta recursividad...
            //foreach (TreeNode tn in treeNode.Nodes)
            //{
            //    PermisoTipoUsuario(tn, eliminar);
            //}
        }

        protected void btnCrearPermiso_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNuevoPermiso.Text))
            {
                PermisoBE permiso = new PermisoBE(txtNuevoPermiso.Text);
                int insPermiso = gestorPermiso.InsertarPermiso(permiso, (UsuarioBE)Session["UsuarioAutenticado"]);
                if (insPermiso == -2)
                {
                    //Sin permisos:

                }
                else if (insPermiso == -1)
                {
                    //noSePudoCrearPermiso
                }
                else
                {
                    gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.NuevoPermisoCreado, (short)EventosBE.Criticidad.Media);
                    EnlazarArbolPermisos();
                    txtNuevoPermiso.Text = string.Empty;
                }
            }
            else
            {
                //Por favor, ingrese un nombre de permiso

            }
        }

        protected void btnEliminarPermiso_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNuevoPermiso.Text))
            {
                List<PermisoBE> permisos = gestorPermiso.ListarPermisos();
                PermisoBE permisoEliminar = new PermisoBE();
                bool existe = false;
                foreach (PermisoBE permiso in permisos)
                {
                    if (string.Compare(permiso.DescripcionPermiso, txtNuevoPermiso.Text.Trim(), true) == 0)
                    {
                        permisoEliminar = permiso;
                        existe = true;
                        break;
                    }
                }
                if (existe)
                {
                    if (gestorPermiso.EliminarPermiso(permisoEliminar, (UsuarioBE)Session["UsuarioAutenticado"]) < 0)
                    {
                        //Sin permisos:

                    }
                    else
                    {
                        EnlazarArbolPermisos();
                        gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.PermisoEliminado, (short)EventosBE.Criticidad.Media);
                        TipoUsuarioBE tipoUsuario = new TipoUsuarioBE
                        {
                            Descripcion_Tipo = ddlTipoUsuarios.SelectedItem.Text.ToString(),
                            Cod_Tipo = short.Parse(ddlTipoUsuarios.SelectedItem.Value)
                        };
                        EnlazarArbolPermisosPorTipoUsuario(tipoUsuario);
                        txtNuevoPermiso.Text = string.Empty;
                    }
                }
                else
                {
                    //Por favor, ingrese un nombre de permiso válido
                }
            }
            else
            {
                //Por favor, ingrese un nombre de permiso
            }
        }

        protected void ddlTipoUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoUsuarioBE tipoUsuario = new TipoUsuarioBE
            {
                Descripcion_Tipo = ddlTipoUsuarios.SelectedItem.Text.ToString(),
                Cod_Tipo = short.Parse(ddlTipoUsuarios.SelectedItem.Value)
            };
            EnlazarArbolPermisosPorTipoUsuario(tipoUsuario);
        }

        protected void btnVincular_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(txtPermisoPadre.Text)) && (!string.IsNullOrWhiteSpace(txtPermisoHijo.Text)))
            {
                int res = gestorPermiso.RelacionarPermisos((PermisoBE)Session["PermisoPadre"], (PermisoBE)Session["PermisoHijo"], (UsuarioBE)Session["UsuarioAutenticado"]);
                if (res < 0)
                {
                    //Sin permisos:

                }
                else if (res == 0)
                {
                    //No se pudo insertar la relación. Revise la rama actual para evitar referencias circulares
                }
                else
                {
                    txtPermisoPadre.Text = string.Empty;
                    txtPermisoHijo.Text = string.Empty;
                    EnlazarArbolPermisos();
                    TipoUsuarioBE tipoUsuario = new TipoUsuarioBE
                    {
                        Descripcion_Tipo = ddlTipoUsuarios.SelectedItem.Text.ToString(),
                        Cod_Tipo = short.Parse(ddlTipoUsuarios.SelectedItem.Value)
                    };
                    EnlazarArbolPermisosPorTipoUsuario(tipoUsuario);
                    Subject.Notify();
                }
            }
        }

        protected void btnDesvincular_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(txtPermisoPadre.Text)) && (!string.IsNullOrWhiteSpace(txtPermisoHijo.Text)))
            {
                if (gestorPermiso.QuitarRelacionPermisos((PermisoBE)Session["PermisoPadre"], (PermisoBE)Session["PermisoHijo"], (UsuarioBE)Session["UsuarioAutenticado"]) < 0)
                {
                    //Sin permisos:

                }
                else
                {
                    txtPermisoPadre.Text = string.Empty;
                    txtPermisoHijo.Text = string.Empty;
                    EnlazarArbolPermisos();
                    TipoUsuarioBE tipoUsuario = new TipoUsuarioBE
                    {
                        Descripcion_Tipo = ddlTipoUsuarios.SelectedItem.Text.ToString(),
                        Cod_Tipo = short.Parse(ddlTipoUsuarios.SelectedItem.Value)
                    };
                    EnlazarArbolPermisosPorTipoUsuario(tipoUsuario);
                    Subject.Notify();
                }
            }
        }
    }
}