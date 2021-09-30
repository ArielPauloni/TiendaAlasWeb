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