using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using BE;

namespace GUI.Servicios
{
    public partial class Idioma : System.Web.UI.Page, IObserver
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();
        private BitacoraSL gestorBitacora = new BitacoraSL();

        public void TraducirTexto()
        {
            grvTexto.Caption = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 26);
            lblIdioma.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 8);
            grvTexto.Columns[2].HeaderText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 33);
            ViewState["tooltipEdit"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 27);
            ViewState["tooltipConfirm"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 28);
            ViewState["tooltipUndo"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 30);
            btnCrearNuevoIdioma.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 29);

            lblBuscarTexto.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 33);
            ViewState["ddlItemContiene"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 34);
            ViewState["ddlItemComienza"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 35);
            ViewState["ddlItemIgual"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 36);
            ViewState["ddlItemTermina"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 37);

            btnMostrarFiltros.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 38);
            ViewState["MostrarFiltros"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 39);
            ViewState["OcultarFiltros"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 42);
            btnMostrarFiltros.Attributes.Add("title", ViewState["MostrarFiltros"].ToString());
            btnFiltrar.Attributes.Add("title", gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 40));
            btnLimpiarFiltros.Attributes.Add("title", gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 41));
        }

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }
            btnCrearNuevoIdioma.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Idioma"), (UsuarioBE)Session["UsuarioAutenticado"]);
            btnMostrarFiltros.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Idioma"), (UsuarioBE)Session["UsuarioAutenticado"]);
            grvTexto.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Idioma"), (UsuarioBE)Session["UsuarioAutenticado"]);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                IdiomaBE idiomaSeleccionado = new IdiomaBE
                {
                    DescripcionIdioma = "Español",
                    IdIdioma = 1
                };

                ddlIdiomas.DataSource = gestorIdioma.ListarIdiomas(idiomaSeleccionado);
                ddlIdiomas.DataTextField = "DescripcionIdioma";
                ddlIdiomas.DataValueField = "IdIdioma";
                ddlIdiomas.DataBind();

                Session["filtrosTexto"] = false;
                MostrarDatosGrillaTexto((Boolean)Session["filtrosTexto"]);

                ddlTipoBusqueda.DataSource = null;
                ddlTipoBusqueda.Items.Add(ViewState["ddlItemContiene"].ToString());
                ddlTipoBusqueda.Items.Add(ViewState["ddlItemIgual"].ToString());
                ddlTipoBusqueda.Items.Add(ViewState["ddlItemComienza"].ToString());
                ddlTipoBusqueda.Items.Add(ViewState["ddlItemTermina"].ToString());
                ddlTipoBusqueda.DataBind();
            }
        }

        protected void ddlIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdiomaBE nuevoIdiomaSeleccionado = new IdiomaBE
            {
                DescripcionIdioma = ddlIdiomas.SelectedItem.Text.ToString(),
                IdIdioma = short.Parse(ddlIdiomas.SelectedItem.Value)
            };
            MostrarDatosGrillaTexto((Boolean)Session["filtrosTexto"]);
        }

        protected void btnCrearNuevoIdioma_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"CrearIdioma.aspx");
        }

        #region GrillaTextos

        private void EnlazarGrillaTextos(List<string> filtros)
        {
            grvTexto.DataSource = null;
            IdiomaBE idiomaSeleccionado = new IdiomaBE
            {
                DescripcionIdioma = ddlIdiomas.SelectedItem.Text.ToString(),
                IdIdioma = short.Parse(ddlIdiomas.SelectedItem.Value)
            };
            List<TextoBE> datosTexto = new List<TextoBE>();
            if (filtros.Count == 0) { datosTexto = gestorIdioma.ListarTextosDelIdioma(idiomaSeleccionado); }
            else
            {
                IEnumerable<TextoBE> filtradosPorTexto = null;

                //Filtro por Texto
                if (!string.IsNullOrWhiteSpace(filtros[0].ToString()))
                {
                    switch (ddlTipoBusqueda.SelectedIndex.ToString())
                    {
                        case "0": //Contiene
                            filtradosPorTexto =
                        from TextoBE tex in gestorIdioma.ListarTextosDelIdioma(idiomaSeleccionado)
                        where tex.Texto.ToLower().Contains(filtros[0].ToString().ToLower())
                        select tex;
                            break;
                        case "1": //Igual a 
                            filtradosPorTexto =
                        from TextoBE tex in gestorIdioma.ListarTextosDelIdioma(idiomaSeleccionado)
                        where tex.Texto.ToLower() == filtros[0].ToString().ToLower()
                        select tex;
                            break;
                        case "2": //Comienza con
                            filtradosPorTexto =
                        from TextoBE tex in gestorIdioma.ListarTextosDelIdioma(idiomaSeleccionado)
                        where tex.Texto.ToLower().StartsWith(filtros[0].ToString().ToLower())
                        select tex;
                            break;
                        case "3": //Termina con
                            filtradosPorTexto =
                        from TextoBE tex in gestorIdioma.ListarTextosDelIdioma(idiomaSeleccionado)
                        where tex.Texto.ToLower().EndsWith(filtros[0].ToString().ToLower())
                        select tex;
                            break;
                    }
                }
                else { filtradosPorTexto = gestorIdioma.ListarTextosDelIdioma(idiomaSeleccionado); }

                foreach (TextoBE textoFiltrado in filtradosPorTexto)
                { datosTexto.Add(textoFiltrado); }
            }
            ListaOrdenable<TextoBE> textosOrdenable = new ListaOrdenable<TextoBE>();
            textosOrdenable = new ListaOrdenable<TextoBE>(datosTexto);
            grvTexto.DataSource = textosOrdenable;
            grvTexto.DataBind();
            grvTexto.Columns[1].Visible = false;
        }

        protected void grvTexto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvTexto.PageIndex = e.NewPageIndex;
            grvTexto.EditIndex = -1;
            MostrarDatosGrillaTexto((Boolean)Session["filtrosTexto"]);
        }

        protected void grvTexto_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Setea EditIndex a -1: Cancela modo edición 
            grvTexto.EditIndex = -1;
            MostrarDatosGrillaTexto((Boolean)Session["filtrosTexto"]);
        }

        protected void grvTexto_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //NewEditIndex se usa para determinar el índice a editar
            grvTexto.EditIndex = e.NewEditIndex;
            MostrarDatosGrillaTexto((Boolean)Session["filtrosTexto"]);
        }

        protected void grvTexto_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Busco los controles de la grilla para la fila que voy a actualizar
            Label id = grvTexto.Rows[e.RowIndex].FindControl("lbl_idFrase") as Label;
            TextBox textoNuevo = grvTexto.Rows[e.RowIndex].FindControl("txt_Texto") as TextBox;

            if (!string.IsNullOrWhiteSpace(textoNuevo.Text))
            {
                //Grabar el nuevo texto
                TextoBE texto = new TextoBE();
                texto.IdFrase = short.Parse(id.Text);
                texto.Texto = textoNuevo.Text;
                IdiomaBE idiomaSeleccionado = new IdiomaBE
                {
                    DescripcionIdioma = ddlIdiomas.SelectedItem.Text.ToString(),
                    IdIdioma = short.Parse(ddlIdiomas.SelectedItem.Value)
                };
                int i = gestorIdioma.ActualizarTexto(idiomaSeleccionado, texto);
                if (i == 0)
                {
                   //No se Pudo Grabar
                }
                else { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.CambioTextoIdioma, (short)EventosBE.Criticidad.Baja); }
            }
            grvTexto.EditIndex = -1;
            MostrarDatosGrillaTexto((Boolean)Session["filtrosTexto"]);
        }

        private void MostrarDatosGrillaTexto(Boolean hayFiltros)
        {
            if (!hayFiltros)
            {
                IdiomaBE idiomaSeleccionado = new IdiomaBE
                {
                    DescripcionIdioma = ddlIdiomas.SelectedItem.Text.ToString(),
                    IdIdioma = short.Parse(ddlIdiomas.SelectedItem.Value)
                };
                List<string> listaVacia = new List<string>();
                EnlazarGrillaTextos(listaVacia);
            }
            else if (!string.IsNullOrWhiteSpace(txtTexto.Text))
            {
                List<string> filtros = new List<string>();
                filtros.Add(txtTexto.Text.Trim());

                EnlazarGrillaTextos(filtros);
            }
        }

        protected void grvTexto_RowDataBound(object sender, GridViewRowEventArgs e)
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

        #endregion

        #region Filtros
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
            Session["filtrosTexto"] = true;
            grvTexto.PageIndex = 0;
            grvTexto.EditIndex = -1;
            MostrarDatosGrillaTexto((Boolean)Session["filtrosTexto"]);
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        private void LimpiarFiltros()
        {
            Session["filtrosTexto"] = false;
            grvTexto.PageIndex = 0;
            grvTexto.EditIndex = -1;
            txtTexto.Text = string.Empty;
            MostrarDatosGrillaTexto((Boolean)Session["filtrosTexto"]);
        }
        #endregion
    }
}