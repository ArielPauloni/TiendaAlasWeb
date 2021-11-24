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
                    tratamiento.Terapias = ObtenerTerapiasPorTratamiento(tratamiento);
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

        //public int ActualizarTratamientoTerapias(TratamientoBE tratamiento)
        //{
        //    int retVal = 0;
        //    try
        //    {
        //        foreach (Tuple<TerapiaBE, short> terapia in tratamiento.Terapias)
        //        { if (InsertarTratamientoTerapias(tratamiento, terapia) > 0) { retVal++; } }
        //    }
        //    catch (Exception)
        //    {
        //        EliminarTratamientoTerapias(tratamiento);
        //    }
        //    if (retVal == 0) { EliminarTratamientoTerapias(tratamiento); }

        //    return retVal;
        //}

        public int InsertarTratamientoTerapia(TratamientoBE tratamiento, Tuple<TerapiaBE, short> terapia)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tratamiento", tratamiento.Cod_Tratamiento));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Terapia", terapia.Item1.Cod_Terapia));
            parametros.Add(AccesoSQL.CrearParametroInt("CantidadSesiones", terapia.Item2));
            return AccesoSQL.Escribir("pr_Insertar_TratamientoTerapia", parametros);
        }

        public int InsertarPacienteTratamiento(PacienteBE paciente, TratamientoBE tratamiento)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Paciente", paciente.Cod_Usuario));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tratamiento", tratamiento.Cod_Tratamiento));
            return AccesoSQL.Escribir("pr_Insertar_PacienteTratamiento", parametros);
        }

        public int EliminarTratamientoTerapias(TratamientoBE tratamiento)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tratamiento", tratamiento.Cod_Tratamiento));
            return AccesoSQL.Escribir("pr_Eliminar_TratamientoTerapias", parametros);
        }

        public int EliminarTratamientoTerapia(TratamientoBE tratamiento, TerapiaBE terapia)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tratamiento", tratamiento.Cod_Tratamiento));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Terapia", terapia.Cod_Terapia));
            return AccesoSQL.Escribir("pr_Eliminar_TratamientoTerapia", parametros);
        }

        public int AgregarProfesionalATratamiento(TratamientoBE tratamiento, UsuarioBE us)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tratamiento", tratamiento.Cod_Tratamiento));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Profesional", us.Cod_Usuario));
            return AccesoSQL.Escribir("pr_Insertar_ProfesionalTratamiento", parametros);
        }

        public int GrabarCalificacion(TratamientoBE tratamiento, PacienteBE paciente)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tratamiento", tratamiento.Cod_Tratamiento));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Paciente", paciente.Cod_Usuario));
            parametros.Add(AccesoSQL.CrearParametroInt("Calificacion", tratamiento.Calificacion));
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

        public List<Tuple<TerapiaBE, short>> ObtenerTerapiasPorTratamiento(TratamientoBE tratamiento)
        {
            List<Tuple<TerapiaBE, short>> myLista = new List<Tuple<TerapiaBE, short>>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tratamiento", tratamiento.Cod_Tratamiento));
            DataTable tabla = AccesoSQL.Leer("pr_Listar_Terapias_PorTratamiento", parametros);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    TerapiaBE t = new TerapiaBE();
                    t.Cod_Terapia = int.Parse(fila["Cod_Terapia"].ToString());
                    t.DescripcionTerapia = fila["DescripcionTerapia"].ToString();
                    t.Duracion = int.Parse(fila["Duracion"].ToString());
                    t.Precio = float.Parse(fila["Precio"].ToString());
                    t.Activo = (Boolean)(fila["Activo"]);
                    myLista.Add(new Tuple<TerapiaBE, short>(t, short.Parse(fila["CantidadSesiones"].ToString())));
                }
            }
            return myLista;
        }

        public List<TratamientoBE> ListarTratamientosPorPaciente(PacienteBE paciente)
        {
            List<TratamientoBE> myLista = new List<TratamientoBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Usuario", paciente.Cod_Usuario));
            DataTable tabla = AccesoSQL.Leer("pr_Listar_TratamientosPorPaciente", parametros);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    TratamientoBE tratamiento = new TratamientoBE();
                    tratamiento.Cod_Tratamiento = int.Parse(fila["Cod_Tratamiento"].ToString());
                    tratamiento.DescripcionTratamiento = fila["DescripcionTratamiento"].ToString();
                    tratamiento.Activo = (Boolean)(fila["Activo"]);
                    tratamiento.Calificacion = short.Parse(fila["Calificacion"].ToString());
                    tratamiento.Terapias = ObtenerTerapiasPorTratamiento(tratamiento);
                    myLista.Add(tratamiento);
                }
            }
            return myLista;
        }
    }
}
