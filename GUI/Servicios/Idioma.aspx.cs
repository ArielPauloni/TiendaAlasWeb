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

        public void TraducirTexto()
        {
        }

        public void ChequearPermisos()
        {
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

                InicializaIdioma(idiomaSeleccionado);
            }
        }

        protected void ddlIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdiomaBE nuevoIdiomaSeleccionado = new IdiomaBE
            {
                DescripcionIdioma = ddlIdiomas.SelectedItem.Text.ToString(),
                IdIdioma = short.Parse(ddlIdiomas.SelectedItem.Value)
            };
            InicializaIdioma(gestorIdioma.ListarIdioma(nuevoIdiomaSeleccionado));
        }

        protected void grvTexto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IdiomaBE nuevoIdiomaSeleccionado = new IdiomaBE
            {
                DescripcionIdioma = ddlIdiomas.SelectedItem.Text.ToString(),
                IdIdioma = short.Parse(ddlIdiomas.SelectedItem.Value)
            };

            List<TextoBE> datosTexto = new List<TextoBE>();
            datosTexto = gestorIdioma.ListarTextosDelIdioma(nuevoIdiomaSeleccionado);
            grvTexto.PageIndex = e.NewPageIndex;
            grvTexto.DataSource = datosTexto;
            grvTexto.DataBind();
        }

        private void InicializaIdioma(IdiomaBE idioma)
        {
            try
            {
                grvTexto.DataSource = null;
                List<TextoBE> datosTexto = new List<TextoBE>();
                datosTexto = gestorIdioma.ListarTextosDelIdioma(idioma);
                grvTexto.DataSource = datosTexto;
                grvTexto.DataBind();

                grvTexto.Columns[0].Visible = false;

                grvTexto.Visible = true;
            }
            catch (Exception ex)
            {
               
            }
        }

        protected void btnCrearNuevoIdioma_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"CrearIdioma.aspx");
        }
    }
}