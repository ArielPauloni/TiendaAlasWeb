using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace SL
{
    public class BackupSL
    {
        public static void realizarBackup(string fileName, UsuarioBE usuarioAutenticado)
        {
            AutorizacionSL gestorAutorizacion = new AutorizacionSL();
            try
            {
                if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Generar Backup"), usuarioAutenticado))
                {
                    BackupMapper.realizarBackup(fileName);
                }
                else { throw new SL.SinPermisosException(); }
            }
            catch (DAL.BackupException ex)
            {
                throw new SL.BackupException(ex.Message);
            }
        }

        public static void restaurarBackup(string fileName, UsuarioBE usuarioAutenticado)
        {
            AutorizacionSL gestorAutorizacion = new AutorizacionSL();
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Restaurar Backup"), usuarioAutenticado))
            {
                BackupMapper.restaurarBackup(fileName);
            }
            else { throw new SL.SinPermisosException(); }
        }
    }
}
