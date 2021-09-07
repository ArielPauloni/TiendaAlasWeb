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
    public class PermisoMapper
    {
        public List<PermisoBE> ListarPermisos()
        {
            List<PermisoBE> listaPermisos = new List<PermisoBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            DataTable tabla = AccesoSQL.Leer("pr_Listar_Permisos", null);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    PermisoBE permiso = new PermisoBE();
                    permiso.CodPermiso = short.Parse(fila["Cod_Permiso"].ToString());
                    permiso.DescripcionPermiso = fila["Nombre"].ToString();
                    listaPermisos.Add(permiso);
                }
            }
            return listaPermisos;
        }

        public int InsertarPermiso(PermisoBE permiso)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroStr("Nombre", permiso.DescripcionPermiso));
            return AccesoSQL.Escribir("pr_Insertar_Permiso", parametros);
        }

        public int CargarPermisosAlUsuario(ref UsuarioBE usuario)
        {
            int retVal = 0;
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("TipoUsuario", usuario.TipoUsuario.Cod_Tipo));
            DataTable tabla = AccesoSQL.Leer("pr_Listar_PermisosPorTipoUsuario", parametros);
            if (tabla != null)
            {
                usuario.Permisos = new List<PermisoBE>();
                foreach (DataRow fila in tabla.Rows)
                {
                    PermisoBE permiso = new PermisoBE();
                    permiso.CodPermiso = int.Parse(fila["Cod_Permiso"].ToString());
                    permiso.DescripcionPermiso = fila["Nombre"].ToString();
                    usuario.Permisos.Add(permiso);
                }
                retVal = 1;
            }
            return retVal;
        }

        public static List<PermisoBE> ListarPermisosPorTipoUsuario(TipoUsuarioBE tipoUsuario)
        {
            List<PermisoBE> listaPermisos = new List<PermisoBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("TipoUsuario", tipoUsuario.Cod_Tipo));

            DataTable tabla = AccesoSQL.Leer("pr_Listar_PermisosPorTipoUsuario", parametros);

            foreach (DataRow registro in tabla.Rows)
            {
                PermisoBE permiso = new PermisoBE();
                permiso.CodPermiso = int.Parse(registro["Cod_Permiso"].ToString());
                permiso.DescripcionPermiso = registro["Nombre"].ToString();
                permiso.CodPermisoPadre = registro["Cod_Permiso_Padre"] == DBNull.Value ? null : (int?)int.Parse(registro["Cod_Permiso_Padre"].ToString());

                listaPermisos.Add(permiso);
            }
            return listaPermisos;
        }

        public int QuitarRelacionPermisos(PermisoBE permisoPadre, PermisoBE permisoHijo)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Permiso_Padre", permisoPadre.CodPermiso));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Permiso_Hijo", permisoHijo.CodPermiso));
            return AccesoSQL.Escribir("pr_Eliminar_RelacionPermisos", parametros);
        }

        public int InsertarPermisoPorTipoUsuario(TipoUsuarioBE tipoUsuario, PermisoBE permiso)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_TipoUsuario", tipoUsuario.Cod_Tipo));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Permiso", permiso.CodPermiso));
            return AccesoSQL.Escribir("pr_Insertar_PermisoPorTipoUsuario", parametros);
        }

        public int EliminarPermisoPorTipoUsuario(TipoUsuarioBE tipoUsuario, PermisoBE permiso)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_TipoUsuario", tipoUsuario.Cod_Tipo));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Permiso", permiso.CodPermiso));
            return AccesoSQL.Escribir("pr_Eliminar_PermisoPorTipoUsuario", parametros);
        }

        public int RelacionarPermisos(PermisoBE permisoPadre, PermisoBE permisoHijo)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Permiso_Padre", permisoPadre.CodPermiso));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Permiso_Hijo", permisoHijo.CodPermiso));
            return AccesoSQL.Escribir("pr_Insertar_RelacionPermisos", parametros);
        }

        /// <summary>
        /// Si Cod_Permiso es cero traigo los nodos que NO tienen padres, sino, trae los hijos directos del permiso buscado			
        /// </summary>
        /// <param name="permisoPadre"></param>
        /// <returns></returns>
        public List<PermisoBE> ListarPermisosPorPadre(PermisoBE permisoPadre)
        {
            List<PermisoBE> listaPermisos = new List<PermisoBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Permiso", permisoPadre.CodPermiso));

            DataTable tabla = AccesoSQL.Leer("pr_Listar_PermisosPorPadre", parametros);
            foreach (DataRow registro in tabla.Rows)
            {
                PermisoBE permiso = new PermisoBE();
                permiso.CodPermiso = int.Parse(registro["Cod_Permiso"].ToString());
                permiso.DescripcionPermiso = registro["Nombre"].ToString();
                if (permisoPadre.CodPermiso == 0) { permiso.CodPermisoPadre = null; }
                else { permiso.CodPermisoPadre = permisoPadre.CodPermiso; }
                listaPermisos.Add(permiso);
            }
            return listaPermisos;
        }

        public int EliminarPermiso(PermisoBE permiso)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Permiso", permiso.CodPermiso));
            return AccesoSQL.Escribir("pr_Eliminar_Permiso", parametros);
        }
    }
}
