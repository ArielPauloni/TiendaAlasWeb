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
            grvTexto.Caption = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 26);
            lblIdioma.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 8);
            grvTexto.Columns[2].HeaderText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 26);
            
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

                grvTexto.Columns[1].Visible = false;

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

        protected void grvTexto_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            grvTexto.EditIndex = -1;
            ShowData();
        }
        
        protected void grvTexto_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            grvTexto.EditIndex = e.NewEditIndex;
            ShowData();
        }

        protected void grvTexto_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Finding the controls from Gridview for the row which is going to update 
            Label id = grvTexto.Rows[e.RowIndex].FindControl("lbl_idFrase") as Label;
            TextBox name = grvTexto.Rows[e.RowIndex].FindControl("txt_Texto") as TextBox;
            //con = new SqlConnection(cs);
            //con.Open();
            ////updating the record  
            //SqlCommand cmd = new SqlCommand("Update tbl_Employee set Name='" + name.Text + "',City='" + city.Text + "' where ID=" + Convert.ToInt32(id.Text), con);
            //cmd.ExecuteNonQuery();
            //con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            grvTexto.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            ShowData();
        }

        private void ShowData()
        {
            IdiomaBE nuevoIdiomaSeleccionado = new IdiomaBE
            {
                DescripcionIdioma = ddlIdiomas.SelectedItem.Text.ToString(),
                IdIdioma = short.Parse(ddlIdiomas.SelectedItem.Value)
            };
            InicializaIdioma(nuevoIdiomaSeleccionado);
        }
    }
}