using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class EventosBE
    {
        public enum Eventos
        {
            NoAplica,
            Login,
            Logout,
            AltaDeUsuario,
            ModificaciónUsuario,
            BajaDeUsuario,
            CambioDeIdioma,
            CreaciónDeIdioma,
            ErrorEnIntegridadUsuario,
            GenerarBackup,
            RestaurarBackup,
            ChequeoIntegridadExitoso,
            NuevoPermisoCreado,
            RecuperoDePass,
            CambioTextoIdioma,
            PermisoEliminado,
            TratamientoCreado,
            CambioDescripcionTratamiento,
            PatologiaCreada,
            CambioDescripcionPatologia,
            ActualizaCaracteristicasDelPaciente,
            AltaDeRelacionTratamientoProfesional,
            BajaDeRelacionTratamientoProfesional,
            TerapiaCreada,
            CambioTerapia,
            CambioRelacionTratamientoTerapia,
            ActualizaPatologiaDelPaciente,
            AsignarTratamientoPaciente,
            CalificarTratamiento,
            ConsultaTratamientoRecomendado
        }

        public enum Criticidad
        {
            NoAplica,
            Baja,
            Media,
            Alta,
            MuyAlta
        }

        private string descripcionEvento;
        public string DescripcionEvento
        {
            get { return descripcionEvento; }
            set { descripcionEvento = value; }
        }

        private int cantidad;
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

    }
}
