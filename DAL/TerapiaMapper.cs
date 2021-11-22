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
    public class TerapiaMapper
    {
        public List<TerapiaBE> Listar()
        {
            List<TerapiaBE> myLista = new List<TerapiaBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            DataTable tabla = AccesoSQL.Leer("pr_Listar_Terapias", null);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    TerapiaBE terapia = new TerapiaBE();
                    terapia.Cod_Terapia = int.Parse(fila["Cod_Terapia"].ToString());
                    terapia.DescripcionTerapia = fila["DescripcionTerapia"].ToString();
                    terapia.Duracion = int.Parse(fila["Duracion"].ToString());
                    terapia.Precio = float.Parse(fila["Precio"].ToString());
                    terapia.Activo = (Boolean)(fila["Activo"]);
                    myLista.Add(terapia);
                }
            }
            return myLista;
        }

        public int Insertar(TerapiaBE terapia)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroStr("DescripcionTerapia", terapia.DescripcionTerapia));
            parametros.Add(AccesoSQL.CrearParametroInt("Duracion", terapia.Duracion));
            parametros.Add(AccesoSQL.CrearParametroDecimal("Precio", terapia.Precio));
            return AccesoSQL.Escribir("pr_Insertar_Terapia", parametros);
        }

        public int Actualizar(TerapiaBE terapia)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Terapia", terapia.Cod_Terapia));
            parametros.Add(AccesoSQL.CrearParametroStr("DescripcionTerapia", terapia.DescripcionTerapia));
            parametros.Add(AccesoSQL.CrearParametroInt("Duracion", terapia.Duracion));
            parametros.Add(AccesoSQL.CrearParametroDecimal("Precio", terapia.Precio));
            return AccesoSQL.Escribir("pr_Actualizar_Terapia", parametros);
        }
    }
}
