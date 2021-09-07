using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace SL
{
    public class BitacoraSL
    {
        public int GrabarBitacora(short evento, short criticidad)
        {
            BitacoraBE bitacoraBE = new BitacoraBE();
            //bitacoraBE.Cod_Usuario = SesionSL.Instancia.Usuario.Cod_Usuario;
            bitacoraBE.Cod_Evento = evento;
            bitacoraBE.Criticidad = criticidad;
            return Insertar(bitacoraBE);
        }

        public int Insertar(BitacoraBE bitacora)
        {
            BitacoraMapper m = new BitacoraMapper();
            return m.Insertar(bitacora);
        }

        public List<BitacoraBE> Listar()
        {
            BitacoraMapper m = new BitacoraMapper();
            return m.Listar();
        }

        public List<BitacoraBE> ObtenerBitacoraPorUsuario(UsuarioBE usuario)
        {
            BitacoraMapper m = new BitacoraMapper();
            return m.ListarBitacoraPorUsuario(usuario);
        }
    }
}
