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
    public class TipoUsuarioMapper
    {
        public List<TipoUsuarioBE> Listar()
        {
            List<TipoUsuarioBE> myLista = new List<TipoUsuarioBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            DataTable tabla = AccesoSQL.Leer("pr_Listar_TiposUsuario", null);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    TipoUsuarioBE tipoUsuario = new TipoUsuarioBE();                    
                    tipoUsuario.Cod_Tipo = short.Parse(fila["Cod_Tipo"].ToString());
                    tipoUsuario.Descripcion_Tipo = fila["DescripcionTipo"].ToString();
                    myLista.Add(tipoUsuario);
                }
            }
            return myLista;
        }

        public int Insertar(TipoUsuarioBE tipoUsuario)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroStr("DescripcionTipo", tipoUsuario.Descripcion_Tipo));
            return AccesoSQL.Escribir("pr_Insertar_TipoUsuario", parametros);
        }
    }
}
