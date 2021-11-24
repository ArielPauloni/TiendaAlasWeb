using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;
using SL.PatronObserver;
using SL;

namespace GUI.Negocio.ABMs
{
    public partial class PacientePatologia : System.Web.UI.Page, IObserver
    {
        private PacienteBLL gestorPaciente = new PacienteBLL();
        private PatologiaBLL gestorPatologia = new PatologiaBLL();
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private IdiomaSL gestorIdioma = new IdiomaSL();

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                ViewState["ErrorMsg"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 1);
                ViewState["SinPermisos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 57);
                ViewState["DatosIncorrectos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 55);
                ViewState["NoSePudoGrabar"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 59);
                ViewState["tooltipEdit"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 27);
                ViewState["tooltipUndo"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 30);
                ViewState["tooltipDelete"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 87);
                ViewState["CantidadSesiones"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 142);
                ViewState["DescripcionTerapia"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 138);
                btnGuardar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 19);
                lblPacientes.Text = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 124);
                lblPatologia.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 146);
                lblPatologiaPaciente.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 147);
                lblTitle.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 145);
            }
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

                EnlazarPacientes();
                EnlazarPatologias();
                EnlazarPacientesPatologias();
            }
        }

        private void EnlazarPacientesPatologias()
        {
            PacienteBE p = new PacienteBE()
            {
                Cod_Usuario = int.Parse(ddlPacientes.SelectedItem.Value)
            };
            PatologiaBE patologia = gestorPaciente.ObtenerPatologiaPaciente(p);
            txtPatologia.Text = patologia.DescripcionPatologia;
        }

        private void EnlazarPatologias()
        {
            ddlPatologia.DataSource = gestorPatologia.Listar();
            ddlPatologia.DataTextField = "DescripcionPatologia";
            ddlPatologia.DataValueField = "Cod_Patologia";
            ddlPatologia.DataBind();
        }

        private void EnlazarPacientes()
        {
            List<PacienteBE> pacientes = gestorPaciente.ListarPacientes();

            var datasource = from p in pacientes
                             select new
                             {
                                 p.Cod_Usuario,
                                 p.Nombre,
                                 p.Apellido,
                                 p.Alias,
                                 p.Mail,
                                 DisplayField = p.ToString()
                             };

            ddlPacientes.DataSource = datasource;
            ddlPacientes.DataTextField = "DisplayField";
            ddlPacientes.DataValueField = "Cod_Usuario";
            ddlPacientes.DataBind();
        }

        protected void ddlPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnlazarPacientesPatologias();
        }

        protected void btnGuardar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (ddlPacientes.SelectedIndex > -1 && ddlPatologia.SelectedIndex > -1)
                {
                    PatologiaBE patologia = new PatologiaBE()
                    {
                        DescripcionPatologia = ddlPatologia.SelectedItem.Text.ToString(),
                        Cod_Patologia = int.Parse(ddlPatologia.SelectedItem.Value)
                    };
                    PacienteBE paciente = new PacienteBE()
                    {
                        Cod_Usuario = int.Parse(ddlPacientes.SelectedItem.Value)
                    };

                    int i = gestorPatologia.InsertarPacientePatologia(paciente, patologia, (UsuarioBE)Session["UsuarioAutenticado"]);
                    if (i <= 0)
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    else { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.ActualizaPatologiaDelPaciente, (short)EventosBE.Criticidad.Media); }
                }
                EnlazarPacientesPatologias();
            }
            catch (SL.SinPermisosException)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }
    }
}