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
    public class TratamientoMapper
    {
        public List<TratamientoBE> Listar()
        {
            List<TratamientoBE> myLista = new List<TratamientoBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            DataTable tabla = AccesoSQL.Leer("pr_Listar_Tratamientos", null);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    TratamientoBE tratamiento = new TratamientoBE();
                    tratamiento.Cod_Tratamiento = int.Parse(fila["Cod_Tratamiento"].ToString());
                    tratamiento.DescripcionTratamiento = fila["DescripcionTratamiento"].ToString();
                    tratamiento.Activo = (Boolean)(fila["Activo"]);
                    myLista.Add(tratamiento);
                }
            }
            return myLista;
        }

        public int Insertar(TratamientoBE tratamiento)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroStr("DescripcionTratamiento", tratamiento.DescripcionTratamiento));
            return AccesoSQL.Escribir("pr_Insertar_Tratamiento", parametros);
        }

        public int AgregarProfesionalATratamiento(TratamientoBE tratamiento, UsuarioBE us)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tratamiento", tratamiento.Cod_Tratamiento));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Profesional", us.Cod_Usuario));
            return AccesoSQL.Escribir("pr_Insertar_ProfesionalTratamiento", parametros);
        }

        public int GrabarCalificacion(TratamientoBE tratamiento, UsuarioBE profesional, int calificacion, UsuarioBE us)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tratamiento", tratamiento.Cod_Tratamiento));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Profesional", profesional.Cod_Usuario));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Paciente", us.Cod_Usuario));
            parametros.Add(AccesoSQL.CrearParametroInt("Calificacion", calificacion));         
            return AccesoSQL.Escribir("pr_Insertar_ProfesionalTratamientoEvaluacion", parametros);
        }

        public List<int> ObtenerCalificaciones(TratamientoBE tratamiento, UsuarioBE profesional)
        {
            List<int> myLista = new List<int>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tratamiento", tratamiento.Cod_Tratamiento));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Profesional", profesional.Cod_Usuario));
            DataTable tabla = AccesoSQL.Leer("pr_Listar_CalificacionesTratamientoProfesional", parametros);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    int calificacion = int.Parse(fila["Calificacion"].ToString());
                    myLista.Add(calificacion);
                }
            }
            return myLista;
        }

        public int Actualizar(TratamientoBE tratamiento)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tratamiento", tratamiento.Cod_Tratamiento));
            parametros.Add(AccesoSQL.CrearParametroStr("DescripcionTratamiento", tratamiento.DescripcionTratamiento));
            return AccesoSQL.Escribir("pr_Actualizar_Tratamiento", parametros);
        }

        public int QuitarProfesionalATratamiento(TratamientoBE tratamiento, UsuarioBE us)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tratamiento", tratamiento.Cod_Tratamiento));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Profesional", us.Cod_Usuario));
            return AccesoSQL.Escribir("pr_Eliminar_ProfesionalTratamiento", parametros);
        }

    }
}
