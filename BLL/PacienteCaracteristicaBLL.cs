using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using SL;

namespace BLL
{
    public class PacienteCaracteristicaBLL
    {
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();

        public List<PacienteCaracteristicaBE> Listar()
        {
            PacienteCaracteristicaMapper m = new PacienteCaracteristicaMapper();
            return m.Listar();
        }

        public int Actualizar(PacienteCaracteristicaBE pacienteCaracteristica, UsuarioBE usuarioAutenticado)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Caracteristicas Paciente"), usuarioAutenticado))
            {
                PacienteCaracteristicaMapper m = new PacienteCaracteristicaMapper();
                retVal = m.Actualizar(pacienteCaracteristica);
            }
            else { throw new SL.SinPermisosException(); }
            return retVal;
        }

        public PacienteCaracteristicaBE ListarCaracteristicasPaciente(PacienteBE paciente)
        {
            PacienteCaracteristicaMapper m = new PacienteCaracteristicaMapper();
            return m.ListarCaracteristicasPaciente(paciente);
        }
    }
}
