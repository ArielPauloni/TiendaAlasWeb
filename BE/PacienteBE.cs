using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PacienteBE: UsuarioBE
    {
        private PatologiaBE patologia;

        public PatologiaBE Patologia
        {
            get { return patologia; }
            set { patologia = value; }
        }

        private List<TratamientoBE> tratamientos;

        public List<TratamientoBE> Tratamientos
        {
            get { return tratamientos; }
            set { tratamientos = value; }
        }
    }
}
