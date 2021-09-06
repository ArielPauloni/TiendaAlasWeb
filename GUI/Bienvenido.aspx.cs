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
    public partial class Bienvenido : System.Web.UI.Page, IObserver
    {
        public void TraducirTexto()
        {
            IdiomaSL gestorIdioma = new IdiomaSL();

            lblHolaMundo.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 1);
            //btnAceptar.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 2) + " " + Server.HtmlDecode("&#10003;");
            btnAlerta.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 11) + " " + Server.HtmlDecode("&#33;");
            lblAlerta.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 12);
            //btnCerrarAlerta.Text = "Cerrar";
            //btnGuardarCambios.Text = "Guardar Cambios";
        }

        public void ChequearPermisos()
        { }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IdiomaSL gestorIdioma = new IdiomaSL();
                ////Load Idiomas
                //ddlIdiomas.DataSource = gestorIdioma.ListarIdiomas((IdiomaBE)Session["IdiomaSel"]);
                //ddlIdiomas.DataTextField = "DescripcionIdioma";
                //ddlIdiomas.DataValueField = "IdIdioma";
                //ddlIdiomas.DataBind();

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
                //ddlIdiomas.SelectedValue = ((IdiomaBE)Session["IdiomaSel"]).DescripcionIdioma;

                //gestorIdioma.TraducirNombresDeIdiomas();

                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();
            }
        }

        protected void btnAlerta_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alertaShow()", true);
        }

        protected void btnGuardarCambios_click(object sender, EventArgs e)
        {
           
        }
    }
}