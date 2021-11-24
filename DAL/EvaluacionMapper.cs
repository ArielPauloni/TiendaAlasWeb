using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class EvaluacionMapper
    {
        private UsuarioMapper usuarioMapper = new UsuarioMapper();
        private TratamientoMapper tratamientoMapper = new TratamientoMapper();
        private PatologiaMapper patologiaMapper = new PatologiaMapper();

        public List<EvaluacionBE> Listar()
        {
            List<EvaluacionBE> myLista = new List<EvaluacionBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            DataTable tabla = AccesoSQL.Leer("pr_Listar_Evaluaciones", null);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    EvaluacionBE evaluacion = new EvaluacionBE();
                    PacienteBE paciente = new PacienteBE();
                    paciente.Cod_Usuario = int.Parse(fila["Cod_Usuario"].ToString());
                    paciente.Apellido = fila["Apellido"].ToString();
                    paciente.Nombre = fila["Nombre"].ToString();
                    paciente.Alias = fila["Alias"].ToString();

                    PatologiaBE patologia = new PatologiaBE();
                    patologia.Cod_Patologia = int.Parse(fila["Cod_Patologia"].ToString());
                    patologia.DescripcionPatologia = fila["DescripcionPatologia"].ToString();
                    paciente.Patologia = patologia;

                    TratamientoBE tratamiento = new TratamientoBE();
                    tratamiento.Cod_Tratamiento = int.Parse(fila["Cod_Tratamiento"].ToString());
                    tratamiento.DescripcionTratamiento = fila["DescripcionTratamiento"].ToString();
                    tratamiento.Activo = (Boolean)(fila["Activo"]);
                    tratamiento.Calificacion = short.Parse(fila["Calificacion"].ToString());

                    PacienteCaracteristicaBE pacienteCaracteristica = new PacienteCaracteristicaBE();
                    pacienteCaracteristica.Paciente = paciente;
                    pacienteCaracteristica.Genero = fila["Genero"] == DBNull.Value ? null : (short?)short.Parse(fila["Genero"].ToString());
                    if (!string.IsNullOrWhiteSpace(fila["Fuma"].ToString())) { pacienteCaracteristica.Fuma = (bool)fila["Fuma"]; }
                    else { pacienteCaracteristica.Fuma = false; }
                    pacienteCaracteristica.DiasActividadDeportiva = fila["DiasActividadDeportiva"] == DBNull.Value ? null : (short?)short.Parse(fila["DiasActividadDeportiva"].ToString());
                    pacienteCaracteristica.HorasRelax = fila["HorasRelax"] == DBNull.Value ? null : (short?)short.Parse(fila["HorasRelax"].ToString());

                    evaluacion.Paciente = paciente;
                    evaluacion.Tratamiento = tratamiento;
                    evaluacion.GradoSatisfaccion = tratamiento.Calificacion;
                    evaluacion.PacienteCaracteristicas = pacienteCaracteristica;
                    myLista.Add(evaluacion);
                }
            }
            return myLista;
        }
    }
}
