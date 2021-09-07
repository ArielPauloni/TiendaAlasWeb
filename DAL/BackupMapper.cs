using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BackupMapper
    {
        public static void realizarBackup(string fileName)
        {
            try
            {
                AccesoSQL a = new AccesoSQL();
                a.realizarBackup(fileName);
            }
            catch (Exception ex)
            {
                throw new DAL.BackupException(ex.Message);
            }
        }

        public static void restaurarBackup(string fileName)
        {
            AccesoSQL a = new AccesoSQL();
            a.restaurarBackup(fileName);
        }
    }
}

