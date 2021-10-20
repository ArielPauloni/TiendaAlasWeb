using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using SL;

namespace BLL
{
    public class TratamientoBLL
    {
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();

        public List<TratamientoBE> Listar()
        {
            TratamientoMapper m = new TratamientoMapper();
            return m.Listar();
        }

        public int Insertar(TratamientoBE tratamiento, UsuarioBE usuarioAutenticado)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Tratamiento"), usuarioAutenticado))
            {
                TratamientoMapper m = new TratamientoMapper();
                retVal = m.Insertar(tratamiento);
            }
            else { throw new SL.SinPermisosException(); }
            return retVal;
        }
        
        public List<UsuarioBE> ListarProfesionalPorTratamiento(TratamientoBE tratamiento)
        {
            UsuarioMapper m = new UsuarioMapper();
            try { return m.ListarProfesionalPorTratamiento(tratamiento); }
            catch (UsuarioModificadoException ex)
            { throw new BLL.UsuarioModificadoException(ex.Message); }
        }
       
        public int AgregarProfesionalATratamiento(TratamientoBE tratamiento, UsuarioBE us)
        {
            TratamientoMapper m = new TratamientoMapper();
            return m.AgregarProfesionalATratamiento(tratamiento, us);
        }

        public int QuitarProfesionalATratamiento(TratamientoBE tratamiento, UsuarioBE us)
        {
            TratamientoMapper m = new TratamientoMapper();
            return m.QuitarProfesionalATratamiento(tratamiento, us);
        }

        public int GrabarCalificacion(TratamientoBE tratamiento, UsuarioBE profesional, UsuarioBE usuarioAutenticado, int calificacion)
        {
            TratamientoMapper m = new TratamientoMapper();
            return m.GrabarCalificacion(tratamiento, profesional, calificacion, usuarioAutenticado);
        }

        public double ObtenerPromedioCalificacion(TratamientoBE tratamiento, UsuarioBE profesional)
        {
            try
            {
                TratamientoMapper m = new TratamientoMapper();
                double promedio = 0;
                int suma = 0;
                List<int> calificaciones = m.ObtenerCalificaciones(tratamiento, profesional);
                foreach (int cal in calificaciones)
                { suma += cal; }
                promedio = (double)suma / (double)calificaciones.Count;
                return promedio;
            }
            catch (Exception)
            { return 0; }
        }

        public int Actualizar(TratamientoBE tratamiento, UsuarioBE usuarioAutenticado)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Tratamiento"), usuarioAutenticado))
            {
                TratamientoMapper m = new TratamientoMapper();
                retVal = m.Actualizar(tratamiento);
            }
            else { throw new SL.SinPermisosException(); }
            return retVal;
        }
    }
}
