using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using BLL;
using BE;

namespace GUI.Negocio.ABMs
{
    public partial class PacienteTratamiento : System.Web.UI.Page, IObserver
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();
        private PacienteBLL gestorPaciente = new PacienteBLL();
        private TratamientoBLL gestorTratamiento = new TratamientoBLL();
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private EvaluacionBLL gestorEvaluaciones = new EvaluacionBLL();
        private PacienteCaracteristicaBLL gestorCaracteristicas = new PacienteCaracteristicaBLL();

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }

        }

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                ViewState["ErrorMsg"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 1);
                ViewState["SinPermisos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 57);
                ViewState["DatosIncorrectos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 55);
                ViewState["NoSePudoGrabar"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 59);
                ViewState["DescripcionTratamiento"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 133);
                ViewState["ValorTotal"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 154);
                ViewState["Calificacion"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 144);
                ViewState["Activo"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 155);
                ViewState["PatologiaNoEvaluada"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 156);
                ViewState["PatologiaMalEvaluada"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 157);
                ViewState["PacienteSinCoincidenciasSuficientes"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 158);
                ViewState["TratamientoRecomendado"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 159);
                lblTitle.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 148);
                lblPacientes.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 124);
                lblTratamiento.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 133);
                lblConsultarTratamiento.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 149);
                grvTratamientos.Caption = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 118);
                btnRecomendarTratamiento.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 150);
                btnGuardar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 19);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                EnlazarPacientes();
                EnlazarTratamientos();
                EnlazarTratamientosPaciente();
            }
        }

        private void EnlazarTratamientosPaciente()
        {
            PacienteBE p = new PacienteBE()
            {
                Cod_Usuario = int.Parse(ddlPacientes.SelectedItem.Value)
            };
            grvTratamientos.DataSource = gestorPaciente.ListarTratamientosPorPaciente(p);
            grvTratamientos.DataBind();
            grvTratamientos.Columns[0].Visible = false;
        }

        private void EnlazarTratamientos()
        {
            ddlTratamiento.DataSource = gestorTratamiento.Listar();
            ddlTratamiento.DataTextField = "DescripcionTratamiento";
            ddlTratamiento.DataValueField = "Cod_Tratamiento";
            ddlTratamiento.DataBind();
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

        protected void btnGuardar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (ddlPacientes.SelectedIndex > -1 && ddlTratamiento.SelectedIndex > -1)
                {
                    TratamientoBE tratamiento = new TratamientoBE()
                    {
                        DescripcionTratamiento = ddlTratamiento.SelectedItem.Text.ToString(),
                        Cod_Tratamiento = int.Parse(ddlTratamiento.SelectedItem.Value)
                    };
                    PacienteBE paciente = new PacienteBE()
                    {
                        Cod_Usuario = int.Parse(ddlPacientes.SelectedItem.Value)
                    };

                    int i = gestorTratamiento.InsertarPacienteTratamiento(paciente, tratamiento, (UsuarioBE)Session["UsuarioAutenticado"]);
                    if (i <= 0)
                    {
                        UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                    }
                    else { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.AsignarTratamientoPaciente, (short)EventosBE.Criticidad.Media); }
                }
                EnlazarTratamientosPaciente();
            }
            catch (SL.SinPermisosException)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void ddlPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnlazarTratamientosPaciente();
        }

        protected void btnRecomendarTratamiento_ServerClick(object sender, EventArgs e)
        {
            try
            {
                PacienteBE paciente = new PacienteBE()
                { Cod_Usuario = int.Parse(ddlPacientes.SelectedItem.Value) };
                //Obtener la patología activa del paciente
                paciente.Patologia = gestorPaciente.ObtenerPatologiaPaciente(paciente);
                //Obtener las características del paciente
                PacienteCaracteristicaBE pacienteCaracteristicas = gestorCaracteristicas.ListarCaracteristicasPaciente(paciente);
                List<EvaluacionBE> evaluacionesTodas = gestorEvaluaciones.Listar();
                TratamientoBE tratamientoSugerido = gestorTratamiento.SugerirTratamiento(paciente, pacienteCaracteristicas, evaluacionesTodas);

                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Info, string.Format(ViewState["TratamientoRecomendado"].ToString(), tratamientoSugerido.DescripcionTratamiento));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);

                ddlTratamiento.SelectedValue = tratamientoSugerido.Cod_Tratamiento.ToString();

                gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.ConsultaTratamientoRecomendado, (short)EventosBE.Criticidad.Media);
            }
            catch (PatologiaNoEvaluadaException ex)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, string.Format(ViewState["PatologiaNoEvaluada"].ToString(), ex.Message));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
            catch (PatologiaMalEvaluadaException ex)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, string.Format(ViewState["PatologiaMalEvaluada"].ToString(), ex.Message));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
            catch (PacienteSinCoincidenciasSuficientesException ex)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, string.Format(ViewState["PacienteSinCoincidenciasSuficientes"].ToString(), ex.Message));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
            catch (Exception ex)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, (ViewState["ErrorMsg"].ToString() + ": " + ex.Message));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void grvTratamientos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvTratamientos.PageIndex = e.NewPageIndex;
            EnlazarTratamientosPaciente();
        }

        protected void grvTratamientos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = ViewState["DescripcionTratamiento"].ToString();
                e.Row.Cells[2].Text = ViewState["ValorTotal"].ToString();
                e.Row.Cells[3].Text = ViewState["Calificacion"].ToString();
                e.Row.Cells[4].Text = ViewState["Activo"].ToString();
            }
        }
    }
}