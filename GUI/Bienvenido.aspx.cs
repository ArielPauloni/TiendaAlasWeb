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

namespace GUI
{
    public partial class Bienvenido : System.Web.UI.Page, IObserver
    {
        private UsuarioBLL gestorUsuario = new UsuarioBLL();
        private EncriptacionSL gestorEncriptacion = new EncriptacionSL();
        private IdiomaSL gestorIdioma = new IdiomaSL();

        public void TraducirTexto()
        {
            if (Session["IdiomaSel"] != null)
            {
                ViewState["ModifiqueDatos"] = gestorIdioma.TraducirTexto((IdiomaBE)Session["IdiomaSel"], 160);
            }
        }

        public void ChequearPermisos()
        { }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //IniciaSesionUsuario();
                //InicializarUsuarios();

                Subject.CleanObserversAll();
                Subject.AddObserver(this);
                Subject.Notify();
            }

            if ((Request.UrlReferrer != null) && (Request.UrlReferrer.AbsolutePath.Contains("")) &&
                (Session["UsuarioCreado"] != null) && ((Boolean)Session["UsuarioCreado"]))
            {
                UC_MensajeModal.SetearMensaje(TipoMensajeBE.Tipo.Info, ViewState["ModifiqueDatos"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "mostrarMensaje()", true);
                Session["UsuarioCreado"] = false;
            }
        }

        //private void IniciaSesionUsuario()
        //{
        //    //Session["UsuarioAutenticado"]
        //    UsuarioBE usuarioAutenticado = new UsuarioBE();
        //    PermisoBE permiso = new PermisoBE();
        //    permiso.CodPermiso = 1;
        //    permiso.DescripcionPermiso = "Crear Usuario";
        //    List<PermisoBE> permisos = new List<PermisoBE>();
        //    permisos.Add(permiso);
        //    usuarioAutenticado.Permisos = new List<PermisoBE>();
        //    usuarioAutenticado.Permisos = permisos;
        //    Session["UsuarioAutenticado"] = usuarioAutenticado;
        //}

        //protected void btnAlerta_Click(object sender, EventArgs e)
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alertaShow()", true);
        //}

        private void InicializarUsuarios()
        {
            //Doy de alta un par de usuarios:
            //UsuarioBE usuarioAdmin = new UsuarioBE();
            //usuarioAdmin.TipoUsuario = new TipoUsuarioBE();
            //usuarioAdmin.Apellido = "D'Agostino";
            //usuarioAdmin.Nombre = "Nadia";
            //usuarioAdmin.Alias = "NadiaAlas";
            //usuarioAdmin.Contraseña = gestorEncriptacion.SimpleEncrypt("Admin123");
            //usuarioAdmin.TipoUsuario.Cod_Tipo = 1;
            //int new_i = gestorUsuario.Insertar(usuarioAdmin, (UsuarioBE)Session["UsuarioAutenticado"]);

            PacienteCaracteristicaBLL gestorCaracteristicas = new PacienteCaracteristicaBLL();
            PatologiaBLL gestorPatologia = new PatologiaBLL();
            TratamientoBLL gestorTratamiento = new TratamientoBLL();
            //Usuario:
            UsuarioBE usuario = new UsuarioBE();
            usuario.TipoUsuario = new TipoUsuarioBE();
            usuario.Apellido = "Paciente 19";
            usuario.Nombre = "Paciente 19";
            usuario.Alias = "Paciente19";
            usuario.FechaNacimiento = DateTime.Now.AddDays(-90).AddYears(-27);
            usuario.Mail = "paciente19@mail.com";
            usuario.Contraseña = gestorEncriptacion.SimpleEncrypt("Paciente123");
            usuario.TipoUsuario.Cod_Tipo = 2;
            int i = gestorUsuario.Insertar(usuario, (UsuarioBE)Session["UsuarioAutenticado"]);
            //Características:
            usuario.Cod_Usuario = 24;
            PacienteCaracteristicaBE pacienteCaracteristica = new PacienteCaracteristicaBE();
            pacienteCaracteristica.Paciente = usuario;
            pacienteCaracteristica.Genero = 1;
            pacienteCaracteristica.Fuma = true;
            pacienteCaracteristica.DiasActividadDeportiva = 1;
            pacienteCaracteristica.HorasRelax = 11;
            i = gestorCaracteristicas.Actualizar(pacienteCaracteristica, (UsuarioBE)Session["UsuarioAutenticado"]);
            //Patología:
            PatologiaBE patologia = new PatologiaBE() { Cod_Patologia = 1 };
            PacienteBE paciente = new PacienteBE() { Cod_Usuario = usuario.Cod_Usuario };
            i = gestorPatologia.InsertarPacientePatologia(paciente, patologia, (UsuarioBE)Session["UsuarioAutenticado"]);
            //Tratamiento y Calificación:
            TratamientoBE tratamiento = new TratamientoBE() { Cod_Tratamiento = 6, Calificacion = 4 };
            i = gestorTratamiento.InsertarPacienteTratamiento(paciente, tratamiento, (UsuarioBE)Session["UsuarioAutenticado"]);
            i = gestorTratamiento.GrabarCalificacion(tratamiento, paciente, (UsuarioBE)Session["UsuarioAutenticado"]);
            //*********************************************************************//
            //Usuario:
            UsuarioBE usuario1 = new UsuarioBE();
            usuario1.TipoUsuario = new TipoUsuarioBE();
            usuario1.Apellido = "Paciente 20";
            usuario1.Nombre = "Paciente 20";
            usuario1.Alias = "Paciente20";
            usuario1.FechaNacimiento = DateTime.Now.AddDays(-80).AddYears(-54);
            usuario1.Mail = "paciente20@mail.com";
            usuario1.Contraseña = gestorEncriptacion.SimpleEncrypt("Paciente123");
            usuario1.TipoUsuario.Cod_Tipo = 2;
            int j = gestorUsuario.Insertar(usuario1, (UsuarioBE)Session["UsuarioAutenticado"]);
            //Características:
            usuario1.Cod_Usuario = 25;
            PacienteCaracteristicaBE pacienteCaracteristica1 = new PacienteCaracteristicaBE();
            pacienteCaracteristica1.Paciente = usuario1;
            pacienteCaracteristica1.Genero = 0;
            pacienteCaracteristica1.Fuma = false;
            pacienteCaracteristica1.DiasActividadDeportiva = 4;
            pacienteCaracteristica1.HorasRelax = 16;
            j = gestorCaracteristicas.Actualizar(pacienteCaracteristica1, (UsuarioBE)Session["UsuarioAutenticado"]);
            //Patología:
            PatologiaBE patologia1 = new PatologiaBE() { Cod_Patologia = 6 };
            PacienteBE paciente1 = new PacienteBE() { Cod_Usuario = usuario1.Cod_Usuario };
            j = gestorPatologia.InsertarPacientePatologia(paciente1, patologia1, (UsuarioBE)Session["UsuarioAutenticado"]);
            //Tratamiento y Calificación:
            TratamientoBE tratamiento1 = new TratamientoBE() { Cod_Tratamiento = 1, Calificacion = 7 };
            j = gestorTratamiento.InsertarPacienteTratamiento(paciente1, tratamiento1, (UsuarioBE)Session["UsuarioAutenticado"]);
            j = gestorTratamiento.GrabarCalificacion(tratamiento1, paciente1, (UsuarioBE)Session["UsuarioAutenticado"]);
            //*********************************************************************//
            //Usuario:
            UsuarioBE usuario2 = new UsuarioBE();
            usuario2.TipoUsuario = new TipoUsuarioBE();
            usuario2.Apellido = "Paciente 21";
            usuario2.Nombre = "Paciente 21";
            usuario2.Alias = "Paciente21";
            usuario2.FechaNacimiento = DateTime.Now.AddDays(-22).AddYears(-19);
            usuario2.Mail = "paciente21@mail.com";
            usuario2.Contraseña = gestorEncriptacion.SimpleEncrypt("Paciente123");
            usuario2.TipoUsuario.Cod_Tipo = 2;
            int k = gestorUsuario.Insertar(usuario2, (UsuarioBE)Session["UsuarioAutenticado"]);
            //Características:
            usuario2.Cod_Usuario = 26;
            PacienteCaracteristicaBE pacienteCaracteristica2 = new PacienteCaracteristicaBE();
            pacienteCaracteristica2.Paciente = usuario2;
            pacienteCaracteristica2.Genero = 1;
            pacienteCaracteristica2.Fuma = false;
            pacienteCaracteristica2.DiasActividadDeportiva = 0;
            pacienteCaracteristica2.HorasRelax = 10;
            k = gestorCaracteristicas.Actualizar(pacienteCaracteristica2, (UsuarioBE)Session["UsuarioAutenticado"]);
            //Patología:
            PatologiaBE patologia2 = new PatologiaBE() { Cod_Patologia = 1 };
            PacienteBE paciente2 = new PacienteBE() { Cod_Usuario = usuario2.Cod_Usuario };
            k = gestorPatologia.InsertarPacientePatologia(paciente2, patologia2, (UsuarioBE)Session["UsuarioAutenticado"]);
            //Tratamiento y Calificación:
            TratamientoBE tratamiento2 = new TratamientoBE() { Cod_Tratamiento = 4, Calificacion = 6 };
            k = gestorTratamiento.InsertarPacienteTratamiento(paciente2, tratamiento2, (UsuarioBE)Session["UsuarioAutenticado"]);
            k = gestorTratamiento.GrabarCalificacion(tratamiento2, paciente2, (UsuarioBE)Session["UsuarioAutenticado"]);
            //*********************************************************************//
            //Usuario:
            UsuarioBE usuario3 = new UsuarioBE();
            usuario3.TipoUsuario = new TipoUsuarioBE();
            usuario3.Apellido = "Paciente 22";
            usuario3.Nombre = "Paciente 22";
            usuario3.Alias = "Paciente22";
            usuario3.FechaNacimiento = DateTime.Now.AddDays(-8).AddYears(-45);
            usuario3.Mail = "paciente22@mail.com";
            usuario3.Contraseña = gestorEncriptacion.SimpleEncrypt("Paciente123");
            usuario3.TipoUsuario.Cod_Tipo = 2;
            int l = gestorUsuario.Insertar(usuario3, (UsuarioBE)Session["UsuarioAutenticado"]);
            //Características:
            usuario3.Cod_Usuario = 27;
            PacienteCaracteristicaBE pacienteCaracteristica3 = new PacienteCaracteristicaBE();
            pacienteCaracteristica3.Paciente = usuario3;
            pacienteCaracteristica3.Genero = 1;
            pacienteCaracteristica3.Fuma = false;
            pacienteCaracteristica3.DiasActividadDeportiva = 3;
            pacienteCaracteristica3.HorasRelax = 14;
            l = gestorCaracteristicas.Actualizar(pacienteCaracteristica3, (UsuarioBE)Session["UsuarioAutenticado"]);
            //Patología:
            PatologiaBE patologia3 = new PatologiaBE() { Cod_Patologia = 4 };
            PacienteBE paciente3 = new PacienteBE() { Cod_Usuario = usuario3.Cod_Usuario };
            l = gestorPatologia.InsertarPacientePatologia(paciente3, patologia3, (UsuarioBE)Session["UsuarioAutenticado"]);
            //Tratamiento y Calificación:
            TratamientoBE tratamiento3 = new TratamientoBE() { Cod_Tratamiento = 6, Calificacion = 6 };
            l = gestorTratamiento.InsertarPacienteTratamiento(paciente3, tratamiento3, (UsuarioBE)Session["UsuarioAutenticado"]);
            l = gestorTratamiento.GrabarCalificacion(tratamiento3, paciente3, (UsuarioBE)Session["UsuarioAutenticado"]);
        }
    }
}