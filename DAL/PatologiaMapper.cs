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
    }
}
