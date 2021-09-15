using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using SL;

namespace BLL
{
    public class UsuarioBLL
    {
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();
        private EncriptacionSL gestorEncriptacion = new EncriptacionSL();

        public int Insertar(UsuarioBE usuario, UsuarioBE usuarioAutenticado)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Usuario"), usuarioAutenticado))
            {
                if ((usuario.TipoUsuario.ToString() == "Administrador") &&
                    !(gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("ABM Administrador"), usuarioAutenticado)))
                { throw new SL.SinPermisosException(); }
                else
                {
                    UsuarioMapper m = new UsuarioMapper();
                    retVal = m.Insertar(usuario);
                }
            }
            else { throw new SL.SinPermisosException(); }
            return retVal;
        }

        public int InsertarDatosBasicosSinPermisos(UsuarioBE usuario)
        {
            int retVal = 0;
            UsuarioMapper m = new UsuarioMapper();
            retVal = m.Insertar(usuario);
            return retVal;
        }

        public int Editar(UsuarioBE usuario, UsuarioBE usuarioAutenticado)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Usuario"), usuarioAutenticado))
            {
                if ((usuario.TipoUsuario.ToString() == "Administrador") &&
                    !(gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("ABM Administrador"), usuarioAutenticado)))
                { throw new SL.SinPermisosException(); }
                else
                {
                    UsuarioMapper m = new UsuarioMapper();
                    retVal = m.Editar(usuario);
                }
            }
            else { throw new SL.SinPermisosException(); }
            return retVal;
        }

        public int Eliminar(UsuarioBE usuario, UsuarioBE usuarioAutenticado)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Eliminar Usuario"), usuarioAutenticado))
            {
                if ((usuario.TipoUsuario.ToString() == "Administrador") &&
                    !(gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("ABM Administrador"), usuarioAutenticado)))
                { throw new SL.SinPermisosException(); }
                else
                {
                    UsuarioMapper m = new UsuarioMapper();
                    retVal = m.Eliminar(usuario);
                }
            }
            else { throw new SL.SinPermisosException(); }
            return retVal;
        }

        public UsuarioBE ObtenerUsuarioPorCod(UsuarioBE usuario)
        {
            UsuarioMapper m = new UsuarioMapper();
            try { return m.ObtenerUsuarioPorCod(usuario); }
            catch (DAL.UsuarioModificadoException ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry("Se modificó usuario por fuera de la aplicación. " + ex.Message, EventLogEntryType.Error, 101, 1);
                }
                throw new BLL.UsuarioModificadoException(ex.Message);
            }
        }

        public UsuarioBE ObtenerUsuarioPorAlias(UsuarioBE usuario)
        {
            UsuarioMapper m = new UsuarioMapper();
            try { return m.ObtenerUsuarioPorAlias(usuario); }
            catch (DAL.UsuarioModificadoException ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry("Se modificó usuario por fuera de la aplicación. " + ex.Message, EventLogEntryType.Error, 101, 1);
                }
                throw new BLL.UsuarioModificadoException(ex.Message);
            }
        }

        public List<UsuarioBE> ListarProfesionales()
        {
            UsuarioMapper m = new UsuarioMapper();
            try
            {
                List<UsuarioBE> listaProfesionales = new List<UsuarioBE>();
                List<UsuarioBE> listaUsuarios = m.Listar();
                foreach (UsuarioBE us in listaUsuarios)
                {
                    if ((us.TipoUsuario.ToString() == "Administrador") || (us.TipoUsuario.ToString() == "Profesional"))
                    { listaProfesionales.Add(us); }
                }
                return listaProfesionales;
            }
            catch (DAL.UsuarioModificadoException ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry("Se modificó usuario por fuera de la aplicación. " + ex.Message, EventLogEntryType.Error, 101, 1);
                }
                throw new BLL.UsuarioModificadoException(ex.Message);
            }
        }

        public List<UsuarioBE> Listar()
        {
            UsuarioMapper m = new UsuarioMapper();
            try { return m.Listar(); }
            catch (DAL.UsuarioModificadoException ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry("Se modificó usuario por fuera de la aplicación. " + ex.Message, EventLogEntryType.Error, 101, 1);
                }
                throw new BLL.UsuarioModificadoException(ex.Message);
            }
        }
    }
}
