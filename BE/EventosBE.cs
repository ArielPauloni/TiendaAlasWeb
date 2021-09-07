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
            AltaDeProducto,
            ModificaciónProducto,
            BajaDeProducto,
            CambioDeIdioma,
            CreaciónDeIdioma,
            CompraRealizada,
            CalculoDeStock,
            ErrorEnIntegridadUsuario,
            ErrorEnIntegridadProductos,
            RealizarPedidoCompra,
            GenerarBackup,
            RestaurarBackup,
            ChequeoIntegridadExitoso,
            VentaRealizada,
            ErrorEnIntegridadPromociones,
            EncuestaRespondida
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
