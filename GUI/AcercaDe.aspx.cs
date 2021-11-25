using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;

namespace GUI
{
    public partial class AcercaDe : Page, IObserver
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();

        public void ChequearPermisos() { }

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                lblTitle.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 162);
                lblHistoria.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 163) + ": ";
                lblHistoriaTexto.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 166) + "\r\n";
                lblMision.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 164) + ": ";
                lblMisionTexto.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 167) + "\r\n";
                lblVision.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 165) + ": ";
                lblVisionTexto.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 168) + "\r\n";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();
            }
        }
    }
}