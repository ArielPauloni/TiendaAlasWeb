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

        public List<Tuple<TerapiaBE, short>> ObtenerTerapiasPorTratamiento(TratamientoBE tratamiento)
        {
            TratamientoMapper m = new TratamientoMapper();
            return m.ObtenerTerapiasPorTratamiento(tratamiento);
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

        public int InsertarPacienteTratamiento(PacienteBE paciente, TratamientoBE tratamiento, UsuarioBE usuarioAutenticado)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Tratamiento Paciente"), usuarioAutenticado))
            {
                TratamientoMapper m = new TratamientoMapper();
                retVal = m.InsertarPacienteTratamiento(paciente, tratamiento);
            }
            else { throw new SL.SinPermisosException(); }
            return retVal;
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

        public int InsertarTratamientoTerapia(TratamientoBE tratamiento, Tuple<TerapiaBE, short> terapia, UsuarioBE usuarioAutenticado)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Tratamiento"), usuarioAutenticado))
            {
                TratamientoMapper m = new TratamientoMapper();
                retVal = m.InsertarTratamientoTerapia(tratamiento, terapia);
            }
            else { throw new SL.SinPermisosException(); }
            return retVal;
        }

        public int EliminarTratamientoTerapia(TratamientoBE tratamiento, TerapiaBE terapia, UsuarioBE usuarioAutenticado)
        {
            int retVal = 0;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Editar Tratamiento"), usuarioAutenticado))
            {
                TratamientoMapper m = new TratamientoMapper();
                retVal = m.EliminarTratamientoTerapia(tratamiento, terapia);
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

        public int AgregarProfesionalATratamiento(TratamientoBE tratamiento, UsuarioBE us, UsuarioBE usuarioAutenticado)
        {
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Tratamiento Profesional"), usuarioAutenticado))
            {
                TratamientoMapper m = new TratamientoMapper();
                return m.AgregarProfesionalATratamiento(tratamiento, us);
            }
            else { throw new SL.SinPermisosException(); }
        }

        public int QuitarProfesionalATratamiento(TratamientoBE tratamiento, UsuarioBE us, UsuarioBE usuarioAutenticado)
        {
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Tratamiento Profesional"), usuarioAutenticado))
            {
                TratamientoMapper m = new TratamientoMapper();
                return m.QuitarProfesionalATratamiento(tratamiento, us);
            }
            else { throw new SL.SinPermisosException(); }
        }

        public int GrabarCalificacion(TratamientoBE tratamiento, PacienteBE paciente, UsuarioBE usuarioAutenticado)
        {
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Calificar tratamiento"), usuarioAutenticado))
            {
                TratamientoMapper m = new TratamientoMapper();
                return m.GrabarCalificacion(tratamiento, paciente);
            }
            else { throw new SL.SinPermisosException(); }
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

        /// <summary>
        /// Sugiere tratamiento para el paciente con más cantidad de características similares dentro de los 
        /// de la lista de evaluaciones (más de 2 coincidencias). Debe coincidir también la patología y 
        ///  tener una calificación mayor a 5 en la evaluación.
        /// </summary>
        /// <param name="paciente">Paciente al que se le quiere sugerir tratamiento</param>
        /// <param name="caracteristicas">Caracteristicas del paciente al que se le quiere sugerir tratamiento</param>
        /// <param name="evaluaciones">Lista de evaluaciones cargadas en el sistema</param>
        /// <returns></returns>
        public TratamientoBE SugerirTratamiento(PacienteBE paciente, PacienteCaracteristicaBE caracteristicas, List<EvaluacionBE> evaluaciones)
        {
            List<EvaluacionBE> evaluacionesConMismaPatologia = new List<EvaluacionBE>();
            List<Tuple<EvaluacionBE, short>> rankingEvaluacionesConMismaPatologia = new List<Tuple<EvaluacionBE, short>>();
            TratamientoBE tratamientoRecomendado = null;
            Boolean malEvaluada = false;

            foreach (EvaluacionBE evaluacion in evaluaciones)
            {
                if (string.Compare(paciente.Patologia.DescripcionPatologia, evaluacion.Paciente.Patologia.DescripcionPatologia, true) == 0)
                {
                    //Solo si el grado de Satisfacción es mayor a 5
                    if (evaluacion.GradoSatisfaccion > 5)
                    {
                        evaluacionesConMismaPatologia.Add(evaluacion);
                        malEvaluada = false;
                    }
                    else { malEvaluada = true; }
                }
            }

            if (evaluacionesConMismaPatologia.Count == 0 && !malEvaluada) { throw new BLL.PatologiaNoEvaluadaException(paciente.Patologia.DescripcionPatologia); }
            else if (evaluacionesConMismaPatologia.Count == 0 && malEvaluada) { throw new BLL.PatologiaMalEvaluadaException(paciente.Patologia.DescripcionPatologia); }
            else
            {
                //Ranking (ordenado) de pacientes con más cantidad de características similares
                foreach (EvaluacionBE eva in evaluacionesConMismaPatologia)
                {
                    short cantidadAciertos = 0;
                    if (caracteristicas.Fuma == eva.PacienteCaracteristicas.Fuma) { cantidadAciertos++; }
                    if (caracteristicas.Genero == eva.PacienteCaracteristicas.Genero) { cantidadAciertos++; }
                    if (Enumerable.Range(caracteristicas.Edad - 7, caracteristicas.Edad + 7).Contains(eva.PacienteCaracteristicas.Edad)) { cantidadAciertos++; }
                    if (caracteristicas.DiasActividadDeportiva.HasValue && eva.PacienteCaracteristicas.DiasActividadDeportiva.HasValue)
                    {
                        if (Enumerable.Range(caracteristicas.DiasActividadDeportiva.Value - 1, caracteristicas.DiasActividadDeportiva.Value + 1).Contains(eva.PacienteCaracteristicas.DiasActividadDeportiva.Value)) { cantidadAciertos++; }
                    }
                    if (caracteristicas.HorasRelax.HasValue && eva.PacienteCaracteristicas.HorasRelax.HasValue)
                    {
                        if (Enumerable.Range(caracteristicas.HorasRelax.Value - 5, caracteristicas.HorasRelax.Value + 5).Contains(eva.PacienteCaracteristicas.HorasRelax.Value)) { cantidadAciertos++; }
                    }

                    if (cantidadAciertos > 2)
                    { rankingEvaluacionesConMismaPatologia.Add(new Tuple<EvaluacionBE, short>(eva, cantidadAciertos)); }
                }
                if (rankingEvaluacionesConMismaPatologia.Count > 0)
                {
                    rankingEvaluacionesConMismaPatologia = rankingEvaluacionesConMismaPatologia.OrderByDescending(i => i.Item2).ToList();
                    Tuple<EvaluacionBE, short> mayorCoincidencia = rankingEvaluacionesConMismaPatologia.FirstOrDefault();
                    tratamientoRecomendado = mayorCoincidencia.Item1.Tratamiento;
                }
                else
                {
                    throw new BLL.PacienteSinCoincidenciasSuficientesException(paciente.Patologia.DescripcionPatologia);
                }
            }

            return tratamientoRecomendado;
        }
    }
}
