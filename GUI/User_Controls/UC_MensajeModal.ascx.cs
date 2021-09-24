using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUI.User_Controls
{
    public partial class UC_MensajeModal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetearMensaje(string Mensaje)
        {
            lblMensaje.Text = Mensaje;
        }
    }
}