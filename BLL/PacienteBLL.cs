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
    public class PacienteBLL
    {
        //public PacienteBE ObtenerPaciente(PacienteBE paciente)
        //{
        //    UsuarioMapper mU = new UsuarioMapper();
        //    UsuarioBE uRet = new UsuarioBE();
        //    PacienteBE pRet = new PacienteBE();
        //    try
        //    {
        //        uRet = mU.ObtenerUsuarioPorCod(paciente);
        //        pRet = (PacienteBE)uRet;
        //        pRet.Patologia = ObtenerPatologiaPaciente(paciente);
        //        pRet.Tratamientos = ListarTratamientosPorPaciente(paciente);
        //    }
        //    catch (DAL.UsuarioModificadoException ex)
        //    {
        //        using (EventLog eventLog = new EventLog("Application"))
        //        {
        //            eventLog.Source = "Application";
        //            eventLog.WriteEntry("Se modificó usuario por fuera de la aplicación. " + ex.Message, EventLogEntryType.Error, 101, 1);
        //        }
        //        throw new BLL.UsuarioModificadoException(ex.Message);
        //    }
        //    return pRet;
        //}

        public List<TratamientoBE> ListarTratamientosPorPaciente(PacienteBE paciente)
        {
            TratamientoMapper m = new TratamientoMapper();
            return m.ListarTratamientosPorPaciente(paciente);
        }

        public PatologiaBE ObtenerPatologiaPaciente(PacienteBE paciente)
        {
            PatologiaMapper m = new PatologiaMapper();
            return m.ObtenerPatologiaPaciente(paciente);
        }

        public List<PacienteBE> ListarPacientes()
        {
            UsuarioMapper m = new UsuarioMapper();
            try
            {
                List<PacienteBE> listaPacientes = new List<PacienteBE>();
                List<UsuarioBE> listaUsuarios = m.Listar();
                foreach (UsuarioBE us in listaUsuarios)
                {
                    if (us.TipoUsuario.ToString() == "Paciente")
                    {
                        PacienteBE p = new PacienteBE
                        {
                            Cod_Usuario = us.Cod_Usuario,
                            Nombre = us.Nombre,
                            Apellido = us.Apellido,
                            Alias = us.Alias
                        };
                        listaPacientes.Add(p);
                    }
                }
                return listaPacientes;
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
    }
}
