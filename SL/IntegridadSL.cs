using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using BE;
using DAL;

namespace SL
{
    public class IntegridadSL
    {
        public void ChequearIntegridad()
        {
            try
            {
                //ProductoMapper m1 = new ProductoMapper();
                UsuarioMapper m2 = new UsuarioMapper();
                //m1.Listar();
                m2.Listar();
            }
            catch (DAL.ProductoModificadoException ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry("Se modificó producto por fuera de la aplicación. " + ex.Message, EventLogEntryType.Error, 101, 1);
                }
                throw new SL.ProductoModificadoException(ex.Message);
            }
            catch (DAL.UsuarioModificadoException ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry("Se modificó usuario por fuera de la aplicación. " + ex.Message, EventLogEntryType.Error, 101, 1);
                }
                throw new SL.UsuarioModificadoException(ex.Message);
            }
        }
    }
}
