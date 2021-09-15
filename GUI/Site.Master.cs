using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using BE;

namespace GUI
{
    public partial class SiteMaster : MasterPage, IObserver
    {
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();

        public void ChequearPermisos()
        {
            aLogout.Visible = false;
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
            aSecurity.Visible = ((aPermisos.Visible) || (aIdiomas.Visible) || (aBackup.Visible));
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
            span1.Attributes["class"] = "glyphicon glyphicon-chevron-down";
            aSecurity.Controls.Add(span1);

            aPermisos.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 6);
            aBackup.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 7);
            aIdiomas.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 8);

            aSignUp.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 9);
            aLogin.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 10) + " ";
            aLogout.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 13) + " ";
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

            Subject.Notify();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}