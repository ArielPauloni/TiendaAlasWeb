using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SL.PatronObserver;
using SL;
using BLL;
using BE;
using System.Web.Services;
using System.Drawing;

namespace GUI
{
    public partial class SiteMaster : MasterPage, IObserver
    {
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();
        private UsuarioBLL gestorUsuario = new UsuarioBLL();
        private BitacoraSL gestorBitacora = new BitacoraSL();

        public void ChequearPermisos()
        {
            if ((UsuarioBE)Session["UsuarioAutenticado"] == null) { aLogout.Visible = aUserName.Visible = aImagenPerfil.Visible = false; aSignUp.Visible = aLogin.Visible = true; }
            else { aLogout.Visible = aUserName.Visible = aImagenPerfil.Visible = true; aSignUp.Visible = aLogin.Visible = false; }

            aBackup.Visible = ((gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Generar Backup"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Restaurar Backup"), (UsuarioBE)Session["UsuarioAutenticado"])));
            aIdiomas.Visible = ((gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Idioma"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Idioma"), (UsuarioBE)Session["UsuarioAutenticado"])));
            aPermisos.Visible = ((gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Gestionar Permisos"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Asignar Permisos"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Permisos"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Quitar permisos"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Permisos Usuario"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Eliminar Permisos"), (UsuarioBE)Session["UsuarioAutenticado"])));
            aBitacora.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Ver Bitacora"), (UsuarioBE)Session["UsuarioAutenticado"]);
            aIntegridad.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Ver Integridad"), (UsuarioBE)Session["UsuarioAutenticado"]);
            aSecurity.Visible = ((aPermisos.Visible) || (aIdiomas.Visible) || (aBackup.Visible) || (aBitacora.Visible) || (aIntegridad.Visible));
            aABMUsuarios.Visible = ((gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("ABM Usuarios"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Usuario"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Usuario"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Eliminar Usuario"), (UsuarioBE)Session["UsuarioAutenticado"])));
            aNuevoUsuario.Visible = ((gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("ABM Usuarios"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Usuario"), (UsuarioBE)Session["UsuarioAutenticado"])));
            aTipoUsuario.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear TipoUsuario"), (UsuarioBE)Session["UsuarioAutenticado"]);
            aUsuarios.Visible = ((aABMUsuarios.Visible) || (aNuevoUsuario.Visible) || (aTipoUsuario.Visible));
            aAdminTerapias.Visible = ((gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Terapia"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Terapia"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Eliminar Terapia"), (UsuarioBE)Session["UsuarioAutenticado"])));
            aAdminTratamientos.Visible = aTratamientoTerapia.Visible = ((gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Tratamiento"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Tratamiento"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
                (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Eliminar Tratamiento"), (UsuarioBE)Session["UsuarioAutenticado"])));
            aProfesionalTratamiento.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Tratamiento Profesional"), (UsuarioBE)Session["UsuarioAutenticado"]);
            aCaracteristicasPaciente.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Caracteristicas Paciente"), (UsuarioBE)Session["UsuarioAutenticado"]);
            aCalificarTratamiento.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Calificar tratamiento"), (UsuarioBE)Session["UsuarioAutenticado"]);
            aAdminPatologias.Visible = ((gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Patologia"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
               (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Patologia"), (UsuarioBE)Session["UsuarioAutenticado"])) ||
               (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Eliminar Patologia"), (UsuarioBE)Session["UsuarioAutenticado"])));
            aPacienteTratamiento.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Tratamiento Paciente"), (UsuarioBE)Session["UsuarioAutenticado"]);
            aAdminPacientePatologias.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Paciente Patologia"), (UsuarioBE)Session["UsuarioAutenticado"]);
            aTerapias.Visible = ((aProfesionalTratamiento.Visible) || (aAdminTratamientos.Visible) || (aCalificarTratamiento.Visible) || (aAdminPatologias.Visible) ||
                (aAdminTerapias.Visible) || (aCaracteristicasPaciente.Visible) || (aPacienteTratamiento.Visible) || aAdminPacientePatologias.Visible);
            aRptPatologias.Visible = gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Visualizar Reportes"), (UsuarioBE)Session["UsuarioAutenticado"]);
            aReportes.Visible = aRptPatologias.Visible;
        }

        public void TraducirTexto()
        {
            IdiomaSL gestorIdioma = new IdiomaSL();

            if (Session["IdiomaSel"] == null)
            {
                IdiomaBE idiomaSeleccionado = new IdiomaBE
                {
                    CodIdioma = "es",
                    DescripcionIdioma = "Español",
                    IdIdioma = 1
                };
                idiomaSeleccionado.Textos = gestorIdioma.ListarTextosDelIdioma(idiomaSeleccionado);
                Session["IdiomaSel"] = idiomaSeleccionado;
            }

            lblNombreSitio.Text = "Tienda Alas";
            Page.Title = "Tienda Alas";
            aAbout.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 14);
            aContact.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 15);
            aHome.Title = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 4);

            aSecurity.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 5) + " ";
            var span1 = new HtmlGenericControl("span");
            span1.Attributes["class"] = "caret";
            aSecurity.Controls.Add(span1);

            aPermisos.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 6);
            aBackup.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 7);
            aIdiomas.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 8);
            aBitacora.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 3);
            aIntegridad.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 46);

            aUsuarios.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 48) + " ";
            var span2 = new HtmlGenericControl("span");
            span2.Attributes["class"] = "caret";
            aUsuarios.Controls.Add(span2);

            aABMUsuarios.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 49);
            aNuevoUsuario.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 50);
            aTipoUsuario.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 51);

            aTerapias.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 115) + " ";
            var span4 = new HtmlGenericControl("span");
            span4.Attributes["class"] = "caret";
            aTerapias.Controls.Add(span4);

            aAdminTerapias.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 140);
            aAdminTratamientos.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 117);
            aTratamientoTerapia.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 133) +
                                            " / " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 136);
            aProfesionalTratamiento.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 116);
            aAdminPatologias.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 122);
            aCaracteristicasPaciente.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 123);
            aPacienteTratamiento.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 148);
            aAdminPacientePatologias.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 145);
            aCalificarTratamiento.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 143);

            aReportes.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 151) + " ";
            aRptPatologias.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 121);

            var span5 = new HtmlGenericControl("span");
            span5.Attributes["class"] = "caret";
            aReportes.Controls.Add(span5);

            aSignUp.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 9);
            aLogin.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 10);
            aLogout.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 13);
            aMisDatos.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 56);

            btnConfirmarLogout.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 2);
            btnCancelarLogout.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 20);
            lblMensajeConfirmacion.Text = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 86);
            ConfirmaLogoutTitle.Attributes.Add("class", "modal-title fa fa-info-circle");
            ConfirmaLogoutTitle.Attributes.Add("style", "color: black; font-weight:bold; vertical-align:central; text-align: center;");
            ConfirmaLogoutTitle.InnerText = " " + gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 52);

            if ((UsuarioBE)Session["UsuarioAutenticado"] != null)
            {
                aUserName.InnerText = ((UsuarioBE)Session["UsuarioAutenticado"]).ToString() + " ";
                var span3 = new HtmlGenericControl("span");
                span3.Attributes["class"] = "caret";
                aUserName.Controls.Add(span3);

                if (((UsuarioBE)Session["UsuarioAutenticado"]).FotoPerfil != null)
                {
                    ImageConverter converter = new ImageConverter();
                    byte[] imageToByte = (byte[])converter.ConvertTo(((UsuarioBE)Session["UsuarioAutenticado"]).FotoPerfil, typeof(byte[]));

                    imgFotoPerfil.Src = @String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String((byte[])imageToByte));
                }
                else { imgFotoPerfil.Src = "Imagenes/img_user_default.jpg"; }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IdiomaSL gestorIdioma = new IdiomaSL();
                //Cargo Idiomas
                ddlIdiomas.DataSource = gestorIdioma.ListarIdiomas((IdiomaBE)Session["IdiomaSel"]);
                ddlIdiomas.DataTextField = "DescripcionIdioma";
                ddlIdiomas.DataValueField = "IdIdioma";
                ddlIdiomas.DataBind();

                if (Session["IdiomaSel"] == null)
                {
                    IdiomaBE idiomaSeleccionado = new IdiomaBE
                    {
                        CodIdioma = "es",
                        DescripcionIdioma = "Español",
                        IdIdioma = 1
                    };
                    idiomaSeleccionado.Textos = gestorIdioma.ListarTextosDelIdioma(idiomaSeleccionado);
                    Session["IdiomaSel"] = idiomaSeleccionado;
                }
                ddlIdiomas.SelectedValue = ((IdiomaBE)Session["IdiomaSel"]).IdIdioma.ToString();

                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();
            }
        }

        protected void ddlIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdiomaSL gestorIdioma = new IdiomaSL();

            IdiomaBE nuevoIdiomaSeleccionado = new IdiomaBE
            {
                DescripcionIdioma = ddlIdiomas.SelectedItem.Text.ToString(),
                IdIdioma = short.Parse(ddlIdiomas.SelectedItem.Value)
            };
            Session["IdiomaSel"] = gestorIdioma.ListarIdioma(nuevoIdiomaSeleccionado);

            if ((UsuarioBE)Session["UsuarioAutenticado"] != null)
            {
                ((UsuarioBE)Session["UsuarioAutenticado"]).Idioma = (IdiomaBE)Session["IdiomaSel"];
                int i = gestorUsuario.ActualizarIdioma((UsuarioBE)Session["UsuarioAutenticado"]);
                if (i > 0) { gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.CambioDeIdioma, (short)EventosBE.Criticidad.Baja); }
            }

            Subject.Notify();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            gestorBitacora.GrabarBitacora((UsuarioBE)Session["UsuarioAutenticado"], (short)EventosBE.Eventos.Logout, (short)EventosBE.Criticidad.Baja);
            Session["UsuarioAutenticado"] = null;
            Session.Remove("UsuarioAutenticado");

            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();

            HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            HttpContext.Current.Response.AddHeader("Expires", "0");

            Response.Redirect(@"~/Bienvenido.aspx");
        }
    }
}