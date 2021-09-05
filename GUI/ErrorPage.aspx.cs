using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUI
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMsg.Text = Session["ErrorMsg"].ToString();
            lblErrorMsg.Attributes["style"] = "color:red; font-weight:bold;";
            Session["ErrorMsg"] = string.Empty;
        }
    }
}