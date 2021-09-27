using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using BE;
using SL;

namespace GUI.Servicios.Bitacora
{
    public partial class Bitacora : System.Web.UI.Page, IObserver
    {
        private BitacoraSL gestorBitacora = new BitacoraSL();

        public void TraducirTexto()
        {

        }

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                grvBitacora.DataSource = gestorBitacora.Listar();
                grvBitacora.DataBind();
            }
        }

        protected void grvBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvBitacora.PageIndex = e.NewPageIndex;
            grvBitacora.EditIndex = -1;
            grvBitacora.DataSource = gestorBitacora.Listar();
            grvBitacora.DataBind();
        }
    }
}