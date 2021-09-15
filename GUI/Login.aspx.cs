using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using System.Web.Security;

namespace GUI
{
    public partial class Login : System.Web.UI.Page, IObserver
    {
        public void ChequearPermisos()
        {

        }

        public void TraducirTexto()
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["user"] != null)
                    txtAlias.Text = Request.Cookies["user"].Value;
                if (Request.Cookies["pwd"] != null)
                    txtPassword.Attributes.Add("value", Request.Cookies["pwd"].Value);
                if (Request.Cookies["userid"] != null && Request.Cookies["pwd"] != null)
                    chkRecordarDatos.Checked = true;

                btnShowHidePass.Attributes.Add("Title", "-Mostrar");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (chkRecordarDatos.Checked)
            {
                Response.Cookies["user"].Value = txtAlias.Text;
                Response.Cookies["pwd"].Value = txtPassword.Text;
                Response.Cookies["user"].Expires = DateTime.Now.AddDays(30);
                Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(30);
            }
            else
            {
                Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void ShowHidePass_click(object sender, EventArgs e)
        {
            if (btnShowHidePass.Attributes.Count == 2)
            {
                //Tengo 2 atributos: Está ocultando => Muestro
                btnShowHidePass.Attributes.Clear();
                btnShowHidePass.Attributes.Add("customAttribute", "true");
                btnShowHidePass.Attributes.Add("class", "btnShowHidePass fa fa-eye-slash");
                txtPassword.TextMode = TextBoxMode.SingleLine;
                btnShowHidePass.Attributes.Add("Title", "-Ocultar");
            }
            else
            {
                //Tengo 3 atributos: Está mostrando => Oculto
                btnShowHidePass.Attributes.Clear();
                btnShowHidePass.Attributes.Add("class", "btnShowHidePass fa fa-eye");
                string pass = txtPassword.Text;
                txtPassword.TextMode = TextBoxMode.Password;
                txtPassword.Attributes.Add("Value", pass);
                btnShowHidePass.Attributes.Add("Title", "-Mostrar");
            }
        }
    }
}