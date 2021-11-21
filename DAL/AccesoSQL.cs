using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;

internal class AccesoSQL
{
    private SqlConnection myConnection = new SqlConnection();
    private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

    private void Abrir()
    {
        myConnection.ConnectionString = ConnectionString;
        myConnection.Open();
    }

    private void Cerrar()
    {
        if (myConnection != null && myConnection.State == ConnectionState.Open)
            myConnection.Close();
    }

    public int Escribir(string NombreSP, List<SqlParameter> Parametros)
    {
        int ret = 0;
        Abrir();
        using (SqlCommand myCommand = new SqlCommand())
        {
            myCommand.Connection = myConnection;
            myCommand.CommandText = NombreSP;
            myCommand.CommandType = CommandType.StoredProcedure;

            if (Parametros != null && Parametros.Count > 0)
                myCommand.Parameters.AddRange(Parametros.ToArray());
            try
            {
                ret = myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ret = -1;
            }
        }
        Cerrar();
        return ret;
    }

    public byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        return ms.ToArray();
    }

    public DataTable EscribirConDVH(string NombreSP, List<SqlParameter> Parametros)
    {
        DataTable TablaRet = new DataTable();
        TablaRet = Leer(NombreSP, Parametros);

        return TablaRet;
    }


    public DataTable Leer(string NombreSP, List<SqlParameter> Parametros)
    {
        DataTable TablaRet = new DataTable();
        Abrir();
        using (SqlDataAdapter myAdaptador = new SqlDataAdapter())
        {
            myAdaptador.SelectCommand = new SqlCommand();
            myAdaptador.SelectCommand.CommandText = NombreSP;
            myAdaptador.SelectCommand.Connection = myConnection;
            myAdaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (Parametros != null && Parametros.Count > 0)
                myAdaptador.SelectCommand.Parameters.AddRange(Parametros.ToArray());

            try
            {
                myAdaptador.Fill(TablaRet);
            }
            catch (Exception)
            {
                TablaRet = null;
            }
        }
        Cerrar();
        return TablaRet;
    }

    public SqlParameter CrearParametroStr(string nombre, string valor)
    {
        SqlParameter parametro = new SqlParameter();
        parametro.ParameterName = nombre;
        parametro.DbType = DbType.String;
        parametro.Value = valor;
        return parametro;
    }

    public SqlParameter CrearParametroInt(string nombre, int valor)
    {
        SqlParameter parametro = new SqlParameter();
        parametro.ParameterName = nombre;
        parametro.DbType = DbType.Int16;
        parametro.Value = valor;
        return parametro;
    }

    public SqlParameter CrearParametroShort(string nombre, short? valor)
    {
        SqlParameter parametro = new SqlParameter();
        parametro.ParameterName = nombre;
        parametro.DbType = DbType.Int16;
        parametro.Value = valor;
        return parametro;
    }

    public SqlParameter CrearParametroInt64(string nombre, Int64 valor)
    {
        SqlParameter parametro = new SqlParameter();
        parametro.ParameterName = nombre;
        parametro.DbType = DbType.Int64;
        parametro.Value = valor;
        return parametro;
    }

    public SqlParameter CrearParametroBit(string nombre, Boolean valor)
    {
        SqlParameter parametro = new SqlParameter();
        parametro.ParameterName = nombre;
        parametro.DbType = DbType.Boolean;
        parametro.Value = valor;
        return parametro;
    }

    public SqlParameter CrearParametroDate(string nombre, DateTime? valor)
    {
        SqlParameter parametro = new SqlParameter();
        parametro.ParameterName = nombre;
        parametro.DbType = DbType.DateTime;
        if (valor != null)
        {
            parametro.Value = valor;
        }
        else
        {
            parametro.Value = DBNull.Value;
        }
        return parametro;
    }

    public SqlParameter CrearParametroDecimal(string nombre, double valor)
    {
        SqlParameter parametro = new SqlParameter();
        parametro.ParameterName = nombre;
        parametro.DbType = DbType.Decimal;
        parametro.Value = valor;
        return parametro;
    }

    public SqlParameter CrearParametroVarBinary(string nombre, Bitmap valor)
    {
        SqlParameter parametro = new SqlParameter();
        parametro.ParameterName = nombre;
        parametro.DbType = DbType.Binary;
        parametro.Value = imageToByteArray(valor);
        return parametro;
    }

    public void realizarBackup(string fileName)
    {
        Abrir();
        string backupPath = ConfigurationManager.AppSettings["BackupPath"];
        string nombreBase = ConfigurationManager.AppSettings["NombreBaseDatos"];
        fileName = backupPath + fileName;
        SqlCommand command = new SqlCommand(string.Format(@"BACKUP DATABASE {1} TO disk='{0}.bak' WITH NOFORMAT, NOINIT, NAME = N'backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10", fileName, nombreBase), myConnection);
        command.ExecuteNonQuery();
        Cerrar();
    }

    public void restaurarBackup(string fileName)
    {
        Abrir();
        string backupPath = ConfigurationManager.AppSettings["BackupPath"];
        string nombreBase = ConfigurationManager.AppSettings["NombreBaseDatos"];
        fileName = backupPath + fileName;
        SqlCommand command = new SqlCommand(string.Format(@"ALTER DATABASE {1} SET SINGLE_USER WITH ROLLBACK IMMEDIATE; USE master; RESTORE DATABASE {1} FROM disk='{0}.bak' WITH REPLACE, NOUNLOAD, STATS = 10", fileName, nombreBase), myConnection);
        command.ExecuteNonQuery();
        Cerrar();
    }
}