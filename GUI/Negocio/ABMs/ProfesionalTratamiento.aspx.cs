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
    public partial class ProfesionalTratamiento : System.Web.UI.Page, IObserver
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();
        private TratamientoBLL gestorTratamiento = new TratamientoBLL();
        private BitacoraSL gestorBitacora = new BitacoraSL();
        private UsuarioBLL gestorUsuario = new UsuarioBLL();

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }
            lblTitle.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 116);
        }

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                ViewState["ErrorMsg"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 1);
                ViewState["SinPermisos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 57);
                ViewState["DatosIncorrectos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 55);
                ViewState["NoSePudoGrabar"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 59);
                lblTratamiento.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 133) + ": ";
                btnRelacionarTratProf.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 105);
                btnQuitarRelTratProf.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 106);
                grvProfesionales.Caption = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 130);
                grvProfesionales.Columns[1].HeaderText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 63);
                grvProfesionales.Columns[2].HeaderText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 64);
                grvProfesionales.Columns[3].HeaderText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 132);
                grvProfesionalRelacionados.Caption = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 131);
                grvProfesionalRelacionados.Columns[1].HeaderText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 63);
                grvProfesionalRelacionados.Columns[2].HeaderText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 64);
                grvProfesionalRelacionados.Columns[3].HeaderText = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 132);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();

                EnlazarTratamientos();
                EnlazarGrillaProfesionales();
                TratamientoBE tratamiento = new TratamientoBE
                {
                    DescripcionTratamiento = ddlTratamientos.SelectedItem.Text.ToString(),
                    Cod_Tratamiento = short.Parse(ddlTratamientos.SelectedItem.Value)
                };
                EnlazarGrillaProfesionalRelacionados(tratamiento);
            }
        }

        private void EnlazarGrillaProfesionales()
        {
            grvProfesionales.DataSource = null;
            grvProfesionales.DataSource = gestorUsuario.ListarProfesionales();
            grvProfesionales.DataBind();
            grvProfesionales.Columns[0].Visible = false;
        }

        private void EnlazarTratamientos()
        {
            ddlTratamientos.DataSource = gestorTratamiento.Listar();
            ddlTratamientos.DataTextField = "DescripcionTratamiento";
            ddlTratamientos.DataValueField = "Cod_Tratamiento";
            ddlTratamientos.DataBind();
        }

        private void EnlazarGrillaProfesionalRelacionados(TratamientoBE tratamiento)
        {
            try
            {
                grvProfesionalRelacionados.DataSource = gestorTratamiento.ListarProfesionalPorTratamiento(tratamiento);
                grvProfesionalRelacionados.DataBind();
                grvProfesionalRelacionados.Columns[0].Visible = false;
            }
            catch (BLL.UsuarioModificadoException ex)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Error, ViewState["ErrorMsg"].ToString() + "<br>" + ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void ddTratamientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            TratamientoBE tratamiento = new TratamientoBE
            {
                DescripcionTratamiento = ddlTratamientos.SelectedItem.Text.ToString(),
                Cod_Tratamiento = short.Parse(ddlTratamientos.SelectedItem.Value)
            };
            EnlazarGrillaProfesionalRelacionados(tratamiento);
        }

        protected void grvProfesionalRelacionados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvProfesionalRelacionados.PageIndex = e.NewPageIndex;
            grvProfesionalRelacionados.EditIndex = -1;
            TratamientoBE tratamiento = new TratamientoBE
            {
                DescripcionTratamiento = ddlTratamientos.SelectedItem.Text.ToString(),
                Cod_Tratamiento = short.Parse(ddlTratamientos.SelectedItem.Value)
            };
            EnlazarGrillaProfesionalRelacionados(tratamiento);
        }

        protected void grvProfesionales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvProfesionales.PageIndex = e.NewPageIndex;
            grvProfesionales.EditIndex = -1;
            EnlazarGrillaProfesionales();
        }

        protected void btnRelacionarTratProf_ServerClick(object sender, EventArgs e)
        {
            try
            {
                bool rowSelProf = false;
                List<UsuarioBE> profesionalesParaTratamiento = new List<UsuarioBE>();
                
                foreach (GridViewRow row in grvProfesionales.Rows)
                {
                    CheckBox chkProfSel = (row.FindControl("chk_ProfesionalSeleccionado") as CheckBox);
                    if ((chkProfSel != null) && (chkProfSel.Checked))
                    {
                        rowSelProf = true;
                        Label lblCodUsuario = (row.FindControl("lbl_cod_Usuario") as Label);
                        UsuarioBE us = new UsuarioBE();
                        us.Cod_Usuario = int.Parse(lblCodUsuario.Text);
                        profesionalesParaTratamiento.Add(us);
                    }
                }
                if (!rowSelProf)
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["DatosIncorrectos"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                else
                {
                    if ((ddlTratamientos.SelectedIndex > -1) && (profesionalesParaTratamiento.Count > 0))
                    {
                        TratamientoBE tratamientoParaAsociar = new TratamientoBE
                        {
                            DescripcionTratamiento = ddlTratamientos.SelectedItem.Text.ToString(),
                            Cod_Tratamiento = short.Parse(ddlTratamientos.SelectedItem.Value)
                        };
                        foreach (UsuarioBE us in profesionalesParaTratamiento)
                        {
                            int r = gestorTratamiento.AgregarProfesionalATratamiento(tratamientoParaAsociar, us, (UsuarioBE)Session["UsuarioAutenticado"]);
                            if (r > 0) { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.AltaDeRelacionTratamientoProfesional, (short)EventosBE.Criticidad.Media); }
                        }
                        EnlazarGrillaProfesionalRelacionados(tratamientoParaAsociar);
                    }
                }
            }
            catch (SL.SinPermisosException)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
            catch (Exception)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }

        protected void btnQuitarRelTratProf_ServerClick(object sender, EventArgs e)
        {
            try
            {
                bool rowSelProf = false;
                List<UsuarioBE> profesionalesParaQuitarTratamiento = new List<UsuarioBE>();

                foreach (GridViewRow row in grvProfesionalRelacionados.Rows)
                {
                    CheckBox chkProfSel = (row.FindControl("chk_ProfesionalSeleccionado") as CheckBox);
                    if ((chkProfSel != null) && (chkProfSel.Checked))
                    {
                        rowSelProf = true;
                        Label lblCodUsuario = (row.FindControl("lbl_cod_Usuario") as Label);
                        UsuarioBE us = new UsuarioBE();
                        us.Cod_Usuario = int.Parse(lblCodUsuario.Text);
                        profesionalesParaQuitarTratamiento.Add(us);
                    }
                }
                if (!rowSelProf)
                {
                    UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["DatosIncorrectos"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                }
                else
                {
                    if ((ddlTratamientos.SelectedIndex > -1) && (profesionalesParaQuitarTratamiento.Count > 0))
                    {
                        TratamientoBE tratamientoParaDisociar = new TratamientoBE
                        {
                            DescripcionTratamiento = ddlTratamientos.SelectedItem.Text.ToString(),
                            Cod_Tratamiento = short.Parse(ddlTratamientos.SelectedItem.Value)
                        };
                        foreach (UsuarioBE us in profesionalesParaQuitarTratamiento)
                        {
                            int r = gestorTratamiento.QuitarProfesionalATratamiento(tratamientoParaDisociar, us, (UsuarioBE)Session["UsuarioAutenticado"]);
                            if (r > 0) { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.BajaDeRelacionTratamientoProfesional, (short)EventosBE.Criticidad.Media); }
                        }
                        EnlazarGrillaProfesionalRelacionados(tratamientoParaDisociar);
                    }
                }
            }
            catch (SL.SinPermisosException)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["SinPermisos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
            catch (Exception)
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Alerta, ViewState["NoSePudoGrabar"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
            }
        }
    }
}