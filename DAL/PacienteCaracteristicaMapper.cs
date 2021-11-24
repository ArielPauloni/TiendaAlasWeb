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
    public class PacienteCaracteristicaMapper
    {
        public List<PacienteCaracteristicaBE> Listar()
        {
            List<PacienteCaracteristicaBE> myLista = new List<PacienteCaracteristicaBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            DataTable tabla = AccesoSQL.Leer("pr_Listar_PacientesCaracteristicas", null);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    UsuarioBE paciente = new UsuarioBE();
                    paciente.Cod_Usuario = int.Parse(fila["Cod_Usuario"].ToString());
                    paciente.Apellido = fila["Apellido"].ToString();
                    paciente.Nombre = fila["Nombre"].ToString();
                    paciente.Alias = fila["Alias"].ToString();
                    paciente.Mail = fila["Mail"].ToString();
                    if (!string.IsNullOrWhiteSpace(fila["FechaNacimiento"].ToString()))
                    { paciente.FechaNacimiento = (DateTime)fila["FechaNacimiento"]; }
                    else { paciente.FechaNacimiento = default(DateTime?); }

                    PacienteCaracteristicaBE pacienteCaracteristica = new PacienteCaracteristicaBE();
                    pacienteCaracteristica.Paciente = paciente;
                    pacienteCaracteristica.Genero = fila["Genero"] == DBNull.Value ? null : (short?)short.Parse(fila["Genero"].ToString());
                    if (!string.IsNullOrWhiteSpace(fila["Fuma"].ToString()))
                    { pacienteCaracteristica.Fuma = (bool)fila["Fuma"]; }
                    else { pacienteCaracteristica.Fuma = false; }
                    //pacienteCaracteristica.DiasActividadDeportiva = short.Parse(fila["DiasActividadDeportiva"].ToString());
                    pacienteCaracteristica.DiasActividadDeportiva = fila["DiasActividadDeportiva"] == DBNull.Value ? null : (short?)short.Parse(fila["DiasActividadDeportiva"].ToString());
                    //pacienteCaracteristica.HorasRelax = short.Parse(fila["HorasRelax"].ToString());
                    pacienteCaracteristica.HorasRelax = fila["HorasRelax"] == DBNull.Value ? null : (short?)short.Parse(fila["HorasRelax"].ToString());
                    myLista.Add(pacienteCaracteristica);
                }
            }
            return myLista;
        }

        public PacienteCaracteristicaBE ListarCaracteristicasPaciente(PacienteBE paciente)
        {
           PacienteCaracteristicaBE pacienteCaracteristica = new PacienteCaracteristicaBE();
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Paciente", paciente.Cod_Usuario));
            DataTable tabla = AccesoSQL.Leer("pr_Listar_CaracteristicasDelPaciente", parametros);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    UsuarioBE pacienteRet = new UsuarioBE();
                    pacienteRet.Cod_Usuario = int.Parse(fila["Cod_Usuario"].ToString());
                    pacienteRet.Apellido = fila["Apellido"].ToString();
                    pacienteRet.Nombre = fila["Nombre"].ToString();
                    pacienteRet.Alias = fila["Alias"].ToString();
                    pacienteRet.Mail = fila["Mail"].ToString();
                    if (!string.IsNullOrWhiteSpace(fila["FechaNacimiento"].ToString()))
                    { pacienteRet.FechaNacimiento = (DateTime)fila["FechaNacimiento"]; }
                    else { pacienteRet.FechaNacimiento = default(DateTime?); }

                    pacienteCaracteristica.Paciente = pacienteRet;
                    pacienteCaracteristica.Genero = fila["Genero"] == DBNull.Value ? null : (short?)short.Parse(fila["Genero"].ToString());
                    if (!string.IsNullOrWhiteSpace(fila["Fuma"].ToString()))
                    { pacienteCaracteristica.Fuma = (bool)fila["Fuma"]; }
                    else { pacienteCaracteristica.Fuma = false; }
                    pacienteCaracteristica.DiasActividadDeportiva = fila["DiasActividadDeportiva"] == DBNull.Value ? null : (short?)short.Parse(fila["DiasActividadDeportiva"].ToString());
                    pacienteCaracteristica.HorasRelax = fila["HorasRelax"] == DBNull.Value ? null : (short?)short.Parse(fila["HorasRelax"].ToString());
                }
            }
            return pacienteCaracteristica;
        }

        public int Actualizar(PacienteCaracteristicaBE pacienteCaracteristica)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Paciente", pacienteCaracteristica.Paciente.Cod_Usuario));
            parametros.Add(AccesoSQL.CrearParametroShort("Genero", pacienteCaracteristica.Genero));
            parametros.Add(AccesoSQL.CrearParametroBit("Fuma", pacienteCaracteristica.Fuma));
            parametros.Add(AccesoSQL.CrearParametroShort("DiasActividadDeportiva", pacienteCaracteristica.DiasActividadDeportiva));
            parametros.Add(AccesoSQL.CrearParametroShort("HorasRelax", pacienteCaracteristica.HorasRelax));
            return AccesoSQL.Escribir("pr_Actualizar_PacienteCaracteristicas", parametros);
        }
    }
}
