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
    public class BitacoraMapper
    {
        public int Insertar(BitacoraBE bitacora)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Usuario", bitacora.Cod_Usuario));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Evento", bitacora.Cod_Evento));
            parametros.Add(AccesoSQL.CrearParametroInt("Criticidad", bitacora.Criticidad));
            return AccesoSQL.Escribir("pr_Insertar_Bitacora", parametros);
        }

        public List<BitacoraBE> Listar()
        {
            List<BitacoraBE> myLista = new List<BitacoraBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            DataTable tabla = AccesoSQL.Leer("pr_Listar_Bitacora", null);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    BitacoraBE bitacora = new BitacoraBE();
                    bitacora.Cod_Usuario = int.Parse(fila["Cod_Usuario"].ToString());
                    bitacora.NombreUsuario = fila["NombreUsuario"].ToString();
                    bitacora.Cod_Evento = short.Parse(fila["Cod_Evento"].ToString());
                    bitacora.Criticidad = short.Parse(fila["Criticidad"].ToString());
                    bitacora.FechaEvento = (DateTime)fila["FechaEvento"];

                    myLista.Add(bitacora);
                }
            }
            return myLista;
        }

        public List<BitacoraBE> ListarBitacoraPorUsuario(UsuarioBE usuario)
        {
            List<BitacoraBE> myLista = new List<BitacoraBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Usuario", usuario.Cod_Usuario));
            DataTable tabla = AccesoSQL.Leer("pr_Listar_BitacoraXUsuario", parametros);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    BitacoraBE bitacora = new BitacoraBE();
                    bitacora.Cod_Usuario = int.Parse(fila["Cod_Usuario"].ToString());
                    bitacora.NombreUsuario = fila["NombreUsuario"].ToString();
                    bitacora.Cod_Evento = short.Parse(fila["Cod_Evento"].ToString());
                    bitacora.Criticidad = short.Parse(fila["Criticidad"].ToString());
                    bitacora.FechaEvento = (DateTime)fila["FechaEvento"];

                    myLista.Add(bitacora);
                }
            }
            return myLista;
        }
    }
}
