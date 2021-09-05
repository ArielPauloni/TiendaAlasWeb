using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.PatronObserver
{
    public interface IObserver
    {
        void TraducirTexto();
        void ChequearPermisos();
    }
}
