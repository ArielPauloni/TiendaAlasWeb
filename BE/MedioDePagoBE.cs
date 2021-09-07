using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel;

namespace BE
{
    public class MedioDePagoBE
    {
        public enum MedioDePago
        {
            //[Description("Pago en efectivo")]
            Efectivo,
            //[Description("Tarjeta de crédito")]
            TarjetaCredito,
            //[Description("Tarjeta de débito")]
            TarjetaDebito,
            //[Description("Mercado Pago")]
            MercadoPago
        }

        private string descripcionMedio;
        public string DescripcionMedio
        {
            get { return descripcionMedio; }
            set { descripcionMedio = value; }
        }
    }
}
