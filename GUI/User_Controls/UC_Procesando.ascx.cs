using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL;

namespace GUI.User_Controls
{
    public partial class UC_Procesando : System.Web.UI.UserControl
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblWait.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 25);
        }
    }
}