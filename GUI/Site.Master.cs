using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using BLL;
using BE;
using System.Web.Services;

namespace GUI
{
    public partial class SiteMaster : MasterPage, IObserver
    {
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();
        private UsuarioBLL gestorUsuario = new UsuarioBLL();
        private BitacoraSL gestorBitacora = new BitacoraSL();

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { aLogout.Visible = aUserName.Visible = false; aSignUp.Visible = aLogin.Visible = true; }
            else { aLogout.Visible = aUserName.Visible = true; aSignUp.Visible = aLogin.Visible = false; }

            aBackup.Visible = ((gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Generar Backup"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Restaurar Backup"), (UsuarioBE)Session["UsuarioAutenticado"])));
            aIdiomas.Visible = ((gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Idioma"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Idioma"), (UsuarioBE)Session["UsuarioAutenticado"])));
            aPermisos.Visible = ((gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Gestionar Permisos"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Asignar Permisos"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Permisos"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Quitar permisos"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Permisos Usuario"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Eliminar Permisos"), (UsuarioBE)Session["UsuarioAutenticado"])));
            aBitacora.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Ver Bitacora"), (UsuarioBE)Session["UsuarioAutenticado"]);
            aIntegridad.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Ver Integridad"), (UsuarioBE)Session["UsuarioAutenticado"]);
            aSecurity.Visible = ((aPermisos.Visible) || (aIdiomas.Visible) || (aBackup.Visible) || (aBitacora.Visible) || (aIntegridad.Visible));
            aABMUsuarios.Visible = ((gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("ABM Usuarios"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Usuario"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Usuario"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Eliminar Usuario"), (UsuarioBE)Session["UsuarioAutenticado"])));
            aNuevoUsuario.Visible = ((gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("ABM Usuarios"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Usuario"), (UsuarioBE)Session["UsuarioAutenticado"])));
            aTipoUsuario.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear TipoUsuario"), (UsuarioBE)Session["UsuarioAutenticado"]);
            aUsuarios.Visible = ((aABMUsuarios.Visible) || (aNuevoUsuario.Visible) || (aTipoUsuario.Visible));
        }

        public void TraducirTexto()
        {
            IdiomaSL gestorIdioma = new IdiomaSL();

            if (Session["IdiomaSel"] == null)
            {
                IdiomaBE idiomaSeleccionado = new IdiomaBE
                {
                    CodIdioma = "es",
                    DescripcionIdioma = "Español",
                    IdIdioma = 1
                };
                idiomaSeleccionado.Textos = gestorIdioma.ListarTextosDelIdioma(idiomaSeleccionado);
                Session["IdiomaSel"] = idiomaSeleccionado;
            }

            lblNombreSitio.Text = "Tienda Alas";
            Page.Title = "Tienda Alas";
            aAbout.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 14);
            aContact.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 15);
            aHome.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 4);

            aSecurity.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 5) + " ";
            var span1 = new HtmlGenericControl("span");
            span1.Attributes["class"] = "caret";
            aSecurity.Controls.Add(span1);

            aPermisos.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 6);
            aBackup.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 7);
            aIdiomas.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 8);
            aBitacora.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 3);
            aIntegridad.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 46);

            aUsuarios.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 48) + " ";
            var span2 = new HtmlGenericControl("span");
            span2.Attributes["class"] = "caret";
            aUsuarios.Controls.Add(span2);

            aABMUsuarios.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 49);
            aNuevoUsuario.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 50);
            aTipoUsuario.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 51);

            aSignUp.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 9);
            aLogin.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 10);
            aLogout.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 13);
            aMisDatos.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 56);

            btnConfirmarLogout.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 2);
            btnCancelarLogout.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 20);
            lblMensajeConfirmacion.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 86);
            ConfirmaLogoutTitle.Attributes.Add("class", "modal-title fa fa-info-circle");
            ConfirmaLogoutTitle.Attributes.Add("style", "color: black; font-weight:bold; vertical-align:central; text-align: center;");
            ConfirmaLogoutTitle.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 52);

            if ((UsuarioBE)Session["UsuarioAutenticado"] != null)
            {
                aUserName.InnerText = " " + ((UsuarioBE)Session["UsuarioAutenticado"]).ToString() + " ";
                var span3 = new HtmlGenericControl("span");
                span3.Attributes["class"] = "caret";
                aUserName.Controls.Add(span3);
            }


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IdiomaSL gestorIdioma = new IdiomaSL();
                //Cargo Idiomas
                ddlIdiomas.DataSource = gestorIdioma.ListarIdiomas((IdiomaBE)Session["IdiomaSel"]);
                ddlIdiomas.DataTextField = "DescripcionIdioma";
                ddlIdiomas.DataValueField = "IdIdioma";
                ddlIdiomas.DataBind();

                if (Session["IdiomaSel"] == null)
                {
                    IdiomaBE idiomaSeleccionado = new IdiomaBE
                    {
                        CodIdioma = "es",
                        DescripcionIdioma = "Español",
                        IdIdioma = 1
                    };
                    idiomaSeleccionado.Textos = gestorIdioma.ListarTextosDelIdioma(idiomaSeleccionado);
                    Session["IdiomaSel"] = idiomaSeleccionado;
                }
                ddlIdiomas.SelectedValue = ((IdiomaBE)Session["IdiomaSel"]).IdIdioma.ToString();

                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();
            }
        }

        protected void ddlIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdiomaSL gestorIdioma = new IdiomaSL();

            IdiomaBE nuevoIdiomaSeleccionado = new IdiomaBE
            {
                DescripcionIdioma = ddlIdiomas.SelectedItem.Text.ToString(),
                IdIdioma = short.Parse(ddlIdiomas.SelectedItem.Value)
            };
            Session["IdiomaSel"] = gestorIdioma.ListarIdioma(nuevoIdiomaSeleccionado);

            if ((UsuarioBE)Session["UsuarioAutenticado"] != null)
            {
                ((UsuarioBE)Session["UsuarioAutenticado"]).Idioma = (IdiomaBE)Session["IdiomaSel"];
                int i = gestorUsuario.ActualizarIdioma((UsuarioBE)Session["UsuarioAutenticado"]);
                if (i > 0) { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.CambioDeIdioma, (short)EventosBE.Criticidad.Baja); }
            }

            Subject.Notify();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.Logout, (short)EventosBE.Criticidad.Baja);
            Session["UsuarioAutenticado"] = null;
            Session.Remove("UsuarioAutenticado");
            Response.Redirect(@"~/Bienvenido.aspx");
        }
    }
}