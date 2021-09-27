﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using BE;
using System.Threading.Tasks;

namespace GUI.Servicios
{
    public partial class CrearIdioma : System.Web.UI.Page, IObserver
    {
        private IdiomaSL gestorIdioma = new IdiomaSL();
        private IdiomaBE idiomaCreado = new IdiomaBE();
        private BitacoraSL gestorBitacora = new BitacoraSL();

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { Response.Redirect(@"~\Bienvenido.aspx"); }

        }

        public void TraducirTexto()
        {
            btnGuardar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 19);
            btnCancelar.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 20);
            lblCodIdioma.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 22);
            lblDescripcionIdioma.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 23);
            lblTraducir.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 24);
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"Idioma.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(txtCodIdioma.Text) && (!string.IsNullOrWhiteSpace(txtDescripcionIdioma.Text))))
            {
                idiomaCreado = new IdiomaBE();
                idiomaCreado.CodIdioma = txtCodIdioma.Text;
                idiomaCreado.DescripcionIdioma = txtDescripcionIdioma.Text;
                gestorIdioma.Insertar(idiomaCreado);
                if (chkTraducir.Checked)
                {
                    try
                    {
                        //Task miTask = new Task(Traducir_EnNuevoHilo);
                        //miTask.Start();
                        //await miTask;
                        IdiomaBE español = new IdiomaBE
                        {
                            CodIdioma = "es",
                            DescripcionIdioma = "Español",
                            IdIdioma = 1
                        };
                        int frasesTraducidas = gestorIdioma.TraducirIdiomaCompleto(español, idiomaCreado);
                        //ActualizarListaIdiomas();
                        gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.CreaciónDeIdioma, (short)EventosBE.Criticidad.Baja);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, errorText, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                //datosIncorrectos
            }
        }
    }
}