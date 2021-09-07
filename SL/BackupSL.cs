using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace SL
{
    public class BackupSL
    {
        public static void realizarBackup(string fileName)
        {
            try
            {
                BackupMapper.realizarBackup(fileName);
            }
            catch (DAL.BackupException ex)
            {
                throw new SL.BackupException(ex.Message);
            }
        }

        public static void restaurarBackup(string fileName)
        {
            BackupMapper.restaurarBackup(fileName);
        }
    }
}
