using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;

namespace GUI
{
    public partial class SiteMaster : MasterPage, IObserver
    {
        public void ChequearPermisos()
        {
            aLogout.Visible = false;
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
            //lblNombreSitio.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 3);
            //Page.Title = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 3);
            Page.Title = "Tienda Alas";
            aAbout.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 14);
            aContact.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 15);
            aHome.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 4);

            aSecurity.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 5) + " ";
            var span1 = new HtmlGenericControl("span");
            span1.Attributes["class"] = "glyphicon glyphicon-chevron-down";
            aSecurity.Controls.Add(span1);

            aPermisos.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 6);
            aBackup.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 7);
            aIdiomas.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 8);

            aSignUp.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 9) + " ";
            var span = new HtmlGenericControl("span");
            span.Attributes["class"] = "glyphicon glyphicon-user";
            aSignUp.Controls.Add(span);

            aLogin.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 10) + " ";
            var span2 = new HtmlGenericControl("span");
            span2.Attributes["class"] = "glyphicon glyphicon-log-in";
            aLogin.Controls.Add(span2);

            aLogout.InnerText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 13) + " ";
            var span3 = new HtmlGenericControl("span");
            span3.Attributes["class"] = "glyphicon glyphicon-log-out";
            aLogout.Controls.Add(span3);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IdiomaSL gestorIdioma = new IdiomaSL();
                //Load Idiomas
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