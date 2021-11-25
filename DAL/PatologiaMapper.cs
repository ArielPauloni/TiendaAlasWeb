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
    public class PatologiaMapper
    {
        public List<PatologiaBE> Listar()
        {
            List<PatologiaBE> myLista = new List<PatologiaBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            DataTable tabla = AccesoSQL.Leer("pr_Listar_Patologias", null);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    PatologiaBE Patologia = new PatologiaBE();
                    Patologia.Cod_Patologia = int.Parse(fila["Cod_Patologia"].ToString());
                    Patologia.DescripcionPatologia = fila["DescripcionPatologia"].ToString();
                    myLista.Add(Patologia);
                }
            }
            return myLista;
        }

        public PatologiaBE ObtenerPatologiaPaciente(PacienteBE paciente)
        {
            PatologiaBE p = new PatologiaBE();
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Usuario", paciente.Cod_Usuario));
            DataTable tabla = AccesoSQL.Leer("pr_Listar_PacientePatologia", parametros);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    p.Cod_Patologia = int.Parse(fila["Cod_Patologia"].ToString());
                    p.DescripcionPatologia = fila["DescripcionPatologia"].ToString();
                }
            }
            return p;
        }

        public int Insertar(PatologiaBE Patologia)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroStr("DescripcionPatologia", Patologia.DescripcionPatologia));
            return AccesoSQL.Escribir("pr_Insertar_Patologia", parametros);
        }

        public int Actualizar(PatologiaBE Patologia)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Patologia", Patologia.Cod_Patologia));
            parametros.Add(AccesoSQL.CrearParametroStr("DescripcionPatologia", Patologia.DescripcionPatologia));
            return AccesoSQL.Escribir("pr_Actualizar_Patologia", parametros);
        }

        public int InsertarPacientePatologia(PacienteBE paciente, PatologiaBE patologia)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Paciente", paciente.Cod_Usuario));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Patologia", patologia.Cod_Patologia));
            return AccesoSQL.Escribir("pr_Insertar_PacientePatologia", parametros);
        }

        public List<PacienteBE> ListarPacientesPorPatologia(PatologiaBE patologia)
        {
            List<PacienteBE> myLista = new List<PacienteBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Patologia", patologia.Cod_Patologia));
            DataTable tabla = AccesoSQL.Leer("pr_Listar_PacientesPorPatologia", parametros);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    PacienteBE paciente = new PacienteBE();
                    paciente.Cod_Usuario = int.Parse(fila["Cod_Usuario"].ToString());
                    paciente.Apellido = fila["Apellido"].ToString();
                    paciente.Nombre = fila["Nombre"].ToString();
                    paciente.Alias = fila["Alias"].ToString();
                    myLista.Add(paciente);
                }
            }
            return myLista;
        }
    }
}
