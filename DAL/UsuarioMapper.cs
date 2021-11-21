using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BE;
using System.Drawing;
using System.IO;

namespace DAL
{
    public class UsuarioMapper
    {
        IdiomaMapper gestorIdioma = new IdiomaMapper();

        public int Insertar(BE.UsuarioBE usuario)
        {
            int retVal = 0;
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroStr("Apellido", usuario.Apellido));
            parametros.Add(AccesoSQL.CrearParametroStr("Nombre", usuario.Nombre));
            parametros.Add(AccesoSQL.CrearParametroStr("Alias", usuario.Alias));
            parametros.Add(AccesoSQL.CrearParametroStr("Contrasenia", usuario.Contraseña));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tipo", usuario.TipoUsuario.Cod_Tipo));
            parametros.Add(AccesoSQL.CrearParametroStr("Telefono", usuario.Telefono));
            parametros.Add(AccesoSQL.CrearParametroStr("Mail", usuario.Mail));
            parametros.Add(AccesoSQL.CrearParametroDate("FechaNacimiento", usuario.FechaNacimiento));
            parametros.Add(AccesoSQL.CrearParametroInt("IntentosEquivocados", usuario.IntentosEquivocados));
            parametros.Add(AccesoSQL.CrearParametroDate("UltimoLogin", usuario.UltimoLogin));
            DataTable dt = AccesoSQL.EscribirConDVH("pr_Insertar_Usuario", parametros);
            if (dt != null)
            {
                UsuarioBE newUsuario = new UsuarioBE();
                TipoUsuarioBE newTipoUsuario = new TipoUsuarioBE();
                foreach (DataRow fila in dt.Rows)
                {
                    newUsuario.Cod_Usuario = int.Parse(fila["Cod_Usuario"].ToString());
                    newUsuario.Apellido = fila["Apellido"].ToString();
                    newUsuario.Nombre = fila["Nombre"].ToString();
                    newUsuario.Alias = fila["Alias"].ToString();
                    newUsuario.Telefono = fila["Telefono"].ToString();
                    newUsuario.Mail = fila["Mail"].ToString();
                    if (!string.IsNullOrWhiteSpace(fila["FechaNacimiento"].ToString()))
                    { newUsuario.FechaNacimiento = (DateTime)fila["FechaNacimiento"]; }
                    else { newUsuario.FechaNacimiento = default(DateTime?); }
                    newUsuario.IntentosEquivocados = short.Parse(fila["IntentosEquivocados"].ToString());
                    if (!string.IsNullOrWhiteSpace(fila["UltimoLogin"].ToString()))
                    { newUsuario.UltimoLogin = (DateTime)fila["UltimoLogin"]; }
                    else { newUsuario.UltimoLogin = default(DateTime?); }
                    //newUsuario.Contraseña = SimpleDecrypt(fila["Contraseña"].ToString());
                    newUsuario.Contraseña = fila["Contraseña"].ToString();
                    newTipoUsuario.Cod_Tipo = short.Parse(fila["Cod_Tipo"].ToString());
                    newUsuario.TipoUsuario = newTipoUsuario;
                }
                retVal = Editar(newUsuario);
            }
            return retVal;
        }

        public int Editar(BE.UsuarioBE usuario)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Usuario", usuario.Cod_Usuario));
            parametros.Add(AccesoSQL.CrearParametroStr("Apellido", usuario.Apellido));
            parametros.Add(AccesoSQL.CrearParametroStr("Nombre", usuario.Nombre));
            parametros.Add(AccesoSQL.CrearParametroStr("Alias", usuario.Alias));
            parametros.Add(AccesoSQL.CrearParametroStr("Contrasenia", usuario.Contraseña));
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tipo", usuario.TipoUsuario.Cod_Tipo));
            parametros.Add(AccesoSQL.CrearParametroStr("Telefono", usuario.Telefono));
            parametros.Add(AccesoSQL.CrearParametroStr("Mail", usuario.Mail));
            parametros.Add(AccesoSQL.CrearParametroDate("FechaNacimiento", usuario.FechaNacimiento));
            parametros.Add(AccesoSQL.CrearParametroBit("Inactivo", usuario.Inactivo));
            parametros.Add(AccesoSQL.CrearParametroInt("IntentosEquivocados", usuario.IntentosEquivocados));
            parametros.Add(AccesoSQL.CrearParametroDate("UltimoLogin", usuario.UltimoLogin));
            CalcularDVHUsuario(ref usuario);
            parametros.Add(AccesoSQL.CrearParametroInt64("DVH", usuario.DVH));
            return AccesoSQL.Escribir("pr_Actualizar_Usuario", parametros);
        }

        private void CalcularDVHUsuario(ref UsuarioBE usuario)
        {
            int valAscii = 0;

            //recorro cada una de las propiedades de Usuario:
            if (!string.IsNullOrEmpty(usuario.Cod_Usuario.ToString()))
            {
                valAscii += SumaASCIIString(usuario.Cod_Usuario.ToString(), 0);
            }
            if (!string.IsNullOrEmpty(usuario.Apellido))
            {
                valAscii += SumaASCIIString(usuario.Apellido, 1);
            }
            if (!string.IsNullOrEmpty(usuario.Nombre))
            {
                valAscii += SumaASCIIString(usuario.Nombre, 2);
            }
            if (!string.IsNullOrEmpty(usuario.Alias))
            {
                valAscii += SumaASCIIString(usuario.Alias, 3);
            }
            if (!string.IsNullOrEmpty(usuario.Contraseña))
            {
                valAscii += SumaASCIIString(SimpleDecrypt(usuario.Contraseña), 4);
            }
            if (!string.IsNullOrEmpty(usuario.TipoUsuario.Cod_Tipo.ToString()))
            {
                valAscii += SumaASCIIString(usuario.TipoUsuario.Cod_Tipo.ToString(), 5);
            }
            if (!string.IsNullOrEmpty(usuario.Telefono))
            {
                valAscii += SumaASCIIString(usuario.Telefono, 6);
            }
            if (!string.IsNullOrEmpty(usuario.Mail))
            {
                valAscii += SumaASCIIString(usuario.Mail, 7);
            }
            if (usuario.FechaNacimiento.HasValue)
            {
                valAscii += SumaASCIIString(usuario.FechaNacimiento?.ToString("dd/MM/yyyy"), 8);
            }
            if (!string.IsNullOrEmpty(usuario.Inactivo.ToString()))
            {
                valAscii += SumaASCIIString(usuario.Inactivo.ToString(), 9);
            }
            if (usuario.UltimoLogin.HasValue)
            {
                valAscii += SumaASCIIString(usuario.UltimoLogin?.ToString("dd/MM/yyyyHHmmss"), 10);
            }
            if (!string.IsNullOrEmpty(usuario.IntentosEquivocados.ToString()))
            {
                valAscii += SumaASCIIString(usuario.IntentosEquivocados.ToString(), 11);
            }
            usuario.DVH = valAscii;
        }

        private int SumaASCIIString(string colStr, int numCol)
        {
            int valAscii = 0;
            for (int i = 0; i < colStr.Length; i++)
            {
                valAscii += (Encoding.ASCII.GetBytes(colStr[i].ToString())[0]) * (i + 1);
            }
            return valAscii * (numCol + 1);
        }

        //public int Eliminar(BE.UsuarioBE usuario)
        //{
        //    AccesoSQL AccesoSQL = new AccesoSQL();
        //    List<SqlParameter> parametros = new List<SqlParameter>();
        //    parametros.Add(AccesoSQL.CrearParametroInt("Cod_Usuario", usuario.Cod_Usuario));
        //    return AccesoSQL.Escribir("pr_Eliminar_Usuario", parametros);
        //}

        public UsuarioBE ObtenerUsuarioLogin(UsuarioBE usuarioBE)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroStr("Alias", usuarioBE.Alias));
            parametros.Add(AccesoSQL.CrearParametroStr("Contrasenia", usuarioBE.Contraseña));
            DataTable tabla = AccesoSQL.Leer("pr_Listar_UsuarioLogin", parametros);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    UsuarioBE usuario = new UsuarioBE();
                    TipoUsuarioBE tipoUsuario = new TipoUsuarioBE();
                    usuario.Cod_Usuario = int.Parse(fila["Cod_Usuario"].ToString());
                    usuario.Apellido = fila["Apellido"].ToString();
                    usuario.Nombre = fila["Nombre"].ToString();
                    usuario.Alias = fila["Alias"].ToString();
                    //usuario.Contraseña = SimpleDecrypt(fila["Contraseña"].ToString());
                    usuario.Contraseña = fila["Contraseña"].ToString();
                    tipoUsuario.Cod_Tipo = short.Parse(fila["Cod_Tipo"].ToString());
                    tipoUsuario.Descripcion_Tipo = fila["DescripcionTipo"].ToString();
                    usuario.TipoUsuario = tipoUsuario;
                    IdiomaBE idioma = new IdiomaBE();
                    idioma.IdIdioma = short.Parse(fila["IdIdioma"].ToString());
                    idioma.CodIdioma = fila["CodIdioma"].ToString();
                    idioma.DescripcionIdioma = fila["DescripcionIdioma"].ToString();
                    usuario.Idioma = idioma;
                    gestorIdioma.SetearIdioma(ref usuario);
                    usuario.Telefono = fila["Telefono"].ToString();
                    usuario.Mail = fila["Mail"].ToString();
                    if (!string.IsNullOrWhiteSpace(fila["FechaNacimiento"].ToString()))
                    {
                        usuario.FechaNacimiento = (DateTime)fila["FechaNacimiento"];
                    }
                    else
                    {
                        usuario.FechaNacimiento = default(DateTime?);
                    }
                    usuario.Inactivo = (bool)fila["Inactivo"];
                    if (!string.IsNullOrWhiteSpace(fila["UltimoLogin"].ToString()))
                    {
                        usuario.UltimoLogin = (DateTime)fila["UltimoLogin"];
                    }
                    else
                    {
                        usuario.UltimoLogin = default(DateTime?);
                    }
                    usuario.IntentosEquivocados = short.Parse(fila["IntentosEquivocados"].ToString());
                    if (!string.IsNullOrWhiteSpace(fila["FotoPerfil"].ToString()))
                    {
                        Byte[] myByteArray = (Byte[])fila["FotoPerfil"];
                        MemoryStream ms = new MemoryStream(myByteArray);
                        usuario.FotoPerfil = (Bitmap)Image.FromStream(ms);
                    }

                    CalcularDVHUsuario(ref usuario);

                    if (int.Parse(fila["DVH"].ToString()) == usuario.DVH)
                    {
                        if (!usuario.Inactivo) { return usuario; }
                        else return null;
                    }
                    else
                    {
                        try { throw new DAL.UsuarioModificadoException("¡ATENCION! <br>Algun dato fue modificado externamente a la aplicación para el usuario " + usuario.ToString()); }
                        catch (DAL.UsuarioModificadoException) { throw; }
                    }
                }
            }
            return null;
        }

        public UsuarioBE ObtenerUsuarioPorAlias(UsuarioBE usuarioBE)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroStr("Alias", usuarioBE.Alias));
            DataTable tabla = AccesoSQL.Leer("pr_Listar_UsuarioPorAlias", parametros);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    UsuarioBE usuario = new UsuarioBE();
                    TipoUsuarioBE tipoUsuario = new TipoUsuarioBE();
                    usuario.Cod_Usuario = int.Parse(fila["Cod_Usuario"].ToString());
                    usuario.Apellido = fila["Apellido"].ToString();
                    usuario.Nombre = fila["Nombre"].ToString();
                    usuario.Alias = fila["Alias"].ToString();
                    //usuario.Contraseña = SimpleDecrypt(fila["Contraseña"].ToString());
                    usuario.Contraseña = fila["Contraseña"].ToString();
                    tipoUsuario.Cod_Tipo = short.Parse(fila["Cod_Tipo"].ToString());
                    tipoUsuario.Descripcion_Tipo = fila["DescripcionTipo"].ToString();
                    usuario.TipoUsuario = tipoUsuario;
                    IdiomaBE idioma = new IdiomaBE();
                    idioma.IdIdioma = short.Parse(fila["IdIdioma"].ToString());
                    idioma.CodIdioma = fila["CodIdioma"].ToString();
                    idioma.DescripcionIdioma = fila["DescripcionIdioma"].ToString();
                    usuario.Idioma = idioma;
                    gestorIdioma.SetearIdioma(ref usuario);
                    usuario.Telefono = fila["Telefono"].ToString();
                    usuario.Mail = fila["Mail"].ToString();
                    if (!string.IsNullOrWhiteSpace(fila["FechaNacimiento"].ToString()))
                    {
                        usuario.FechaNacimiento = (DateTime)fila["FechaNacimiento"];
                    }
                    else
                    {
                        usuario.FechaNacimiento = default(DateTime?);
                    }
                    usuario.Inactivo = (bool)fila["Inactivo"];
                    if (!string.IsNullOrWhiteSpace(fila["UltimoLogin"].ToString()))
                    {
                        usuario.UltimoLogin = (DateTime)fila["UltimoLogin"];
                    }
                    else
                    {
                        usuario.UltimoLogin = default(DateTime?);
                    }
                    usuario.IntentosEquivocados = short.Parse(fila["IntentosEquivocados"].ToString());
                    if (!string.IsNullOrWhiteSpace(fila["FotoPerfil"].ToString()))
                    {
                        Byte[] myByteArray = (Byte[])fila["FotoPerfil"];
                        MemoryStream ms = new MemoryStream(myByteArray);
                        usuario.FotoPerfil = (Bitmap)Image.FromStream(ms);
                    }

                    CalcularDVHUsuario(ref usuario);

                    if (int.Parse(fila["DVH"].ToString()) == usuario.DVH)
                    {
                        if (!usuario.Inactivo) { return usuario; }
                        else return null;
                    }
                    else
                    {
                        try { throw new DAL.UsuarioModificadoException("¡ATENCION! <br>Algun dato fue modificado externamente a la aplicación para el usuario " + usuario.ToString()); }
                        catch (DAL.UsuarioModificadoException) { throw; }
                    }
                }
            }
            return null;
        }

        public UsuarioBE ObtenerUsuarioPorCod(UsuarioBE usuarioBE)
        {
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Usuario", usuarioBE.Cod_Usuario));
            DataTable tabla = AccesoSQL.Leer("pr_Listar_UsuarioPorCod", parametros);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    UsuarioBE usuario = new UsuarioBE();
                    TipoUsuarioBE tipoUsuario = new TipoUsuarioBE();
                    usuario.Cod_Usuario = int.Parse(fila["Cod_Usuario"].ToString());
                    usuario.Apellido = fila["Apellido"].ToString();
                    usuario.Nombre = fila["Nombre"].ToString();
                    usuario.Alias = fila["Alias"].ToString();
                    //usuario.Contraseña = SimpleDecrypt(fila["Contraseña"].ToString());
                    usuario.Contraseña = fila["Contraseña"].ToString();
                    tipoUsuario.Cod_Tipo = short.Parse(fila["Cod_Tipo"].ToString());
                    tipoUsuario.Descripcion_Tipo = fila["DescripcionTipo"].ToString();
                    usuario.TipoUsuario = tipoUsuario;
                    IdiomaBE idioma = new IdiomaBE();
                    idioma.IdIdioma = short.Parse(fila["IdIdioma"].ToString());
                    idioma.CodIdioma = fila["CodIdioma"].ToString();
                    idioma.DescripcionIdioma = fila["DescripcionIdioma"].ToString();
                    usuario.Idioma = idioma;
                    gestorIdioma.SetearIdioma(ref usuario);
                    usuario.Telefono = fila["Telefono"].ToString();
                    usuario.Mail = fila["Mail"].ToString();
                    if (!string.IsNullOrWhiteSpace(fila["FechaNacimiento"].ToString()))
                    {
                        usuario.FechaNacimiento = (DateTime)fila["FechaNacimiento"];
                    }
                    else
                    {
                        Nullable<DateTime> fNull = default(DateTime?);
                        usuario.FechaNacimiento = fNull;
                    }
                    usuario.Inactivo = (bool)fila["Inactivo"];
                    if (!string.IsNullOrWhiteSpace(fila["UltimoLogin"].ToString()))
                    {
                        usuario.UltimoLogin = (DateTime)fila["UltimoLogin"];
                    }
                    else
                    {
                        usuario.UltimoLogin = default(DateTime?);
                    }
                    usuario.IntentosEquivocados = short.Parse(fila["IntentosEquivocados"].ToString());
                    if (!string.IsNullOrWhiteSpace(fila["FotoPerfil"].ToString()))
                    {
                        Byte[] myByteArray = (Byte[])fila["FotoPerfil"];
                        MemoryStream ms = new MemoryStream(myByteArray);
                        usuario.FotoPerfil = (Bitmap)Image.FromStream(ms);
                    }

                    CalcularDVHUsuario(ref usuario);

                    if (int.Parse(fila["DVH"].ToString()) == usuario.DVH)
                    {
                        if (!usuario.Inactivo) { return usuario; }
                        else return null;
                    }
                    else
                    {
                        try { throw new DAL.UsuarioModificadoException("¡ATENCION! <br>Algun dato fue modificado externamente a la aplicación para el usuario " + usuario.ToString()); }
                        catch (DAL.UsuarioModificadoException) { throw; }
                    }
                }
            }
            return null;
        }

        public List<BE.UsuarioBE> Listar()
        {
            List<BE.UsuarioBE> myLista = new List<BE.UsuarioBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            DataTable tabla = AccesoSQL.Leer("pr_Listar_Usuarios", null);
            if (tabla != null)
            {
                long SumaDVH = 0;
                foreach (DataRow fila in tabla.Rows)
                {
                    BE.UsuarioBE usuario = new BE.UsuarioBE();
                    TipoUsuarioBE tipoUsuario = new TipoUsuarioBE();
                    usuario.Cod_Usuario = int.Parse(fila["Cod_Usuario"].ToString());
                    usuario.Apellido = fila["Apellido"].ToString();
                    usuario.Nombre = fila["Nombre"].ToString();
                    usuario.Alias = fila["Alias"].ToString();
                    //usuario.Contraseña = SimpleDecrypt(fila["Contraseña"].ToString());
                    usuario.Contraseña = fila["Contraseña"].ToString();
                    tipoUsuario.Cod_Tipo = short.Parse(fila["Cod_Tipo"].ToString());
                    tipoUsuario.Descripcion_Tipo = fila["DescripcionTipo"].ToString();
                    usuario.TipoUsuario = tipoUsuario;
                    IdiomaBE idioma = new IdiomaBE();
                    idioma.IdIdioma = short.Parse(fila["IdIdioma"].ToString());
                    idioma.CodIdioma = fila["CodIdioma"].ToString();
                    idioma.DescripcionIdioma = fila["DescripcionIdioma"].ToString();
                    usuario.Idioma = idioma;
                    gestorIdioma.SetearIdioma(ref usuario);
                    usuario.Telefono = fila["Telefono"].ToString();
                    usuario.Mail = fila["Mail"].ToString();
                    if (!string.IsNullOrWhiteSpace(fila["FechaNacimiento"].ToString()))
                    {
                        usuario.FechaNacimiento = (DateTime)fila["FechaNacimiento"];
                    }
                    else
                    {
                        Nullable<DateTime> fNull = default(DateTime?);
                        usuario.FechaNacimiento = fNull;
                    }
                    usuario.Inactivo = (bool)fila["Inactivo"];
                    if (!string.IsNullOrWhiteSpace(fila["UltimoLogin"].ToString()))
                    {
                        usuario.UltimoLogin = (DateTime)fila["UltimoLogin"];
                    }
                    else
                    {
                        usuario.UltimoLogin = default(DateTime?);
                    }
                    usuario.IntentosEquivocados = short.Parse(fila["IntentosEquivocados"].ToString());
                    if (!string.IsNullOrWhiteSpace(fila["FotoPerfil"].ToString()))
                    {
                        Byte[] myByteArray = (Byte[])fila["FotoPerfil"];
                        MemoryStream ms = new MemoryStream(myByteArray);
                        usuario.FotoPerfil = (Bitmap)Image.FromStream(ms);
                    }

                    CalcularDVHUsuario(ref usuario);

                    if (int.Parse(fila["DVH"].ToString()) == usuario.DVH)
                    {
                        SumaDVH = +SumaDVH + usuario.DVH;
                        if (!usuario.Inactivo) { myLista.Add(usuario); }
                    }
                    else
                    {
                        try { throw new DAL.UsuarioModificadoException("¡ATENCION! <br>Algun dato fue modificado externamente a la aplicación para el usuario " + usuario.ToString()); }
                        catch (DAL.UsuarioModificadoException) { throw; }
                    }
                }
                //DVV:
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(AccesoSQL.CrearParametroStr("Tabla", "dbo.Usuario"));
                DataTable tablaDVV = AccesoSQL.Leer("pr_Obtener_DigitoVerificador", parametros);
                if (tablaDVV != null)
                {
                    long filaDVV = long.Parse(tablaDVV.Rows[0]["SumaDVH"].ToString());

                    if (!(filaDVV == SumaDVH))
                    {
                        try { throw new DAL.UsuarioModificadoException("¡ATENCION! <br>Algun dato de usuario fue eliminado o insertado externamente a la aplicación"); }
                        catch (DAL.UsuarioModificadoException) { throw; }
                    }
                }
            }
            return myLista;
        }

        public int ActualizarIdioma(UsuarioBE usuario)
        {
            int retVal = 0;
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Usuario", usuario.Cod_Usuario));
            parametros.Add(AccesoSQL.CrearParametroInt("IdIdioma", usuario.Idioma.IdIdioma));
            retVal = AccesoSQL.Escribir("pr_Actualizar_IdiomaUsuario", parametros);
            if (retVal > 0)
            {
                gestorIdioma.SetearIdioma(ref usuario);
            }
            return retVal;
        }

        public int ActualizarFotoPerfil(UsuarioBE usuario)
        {
            int retVal = 0;
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Usuario", usuario.Cod_Usuario));
            parametros.Add(AccesoSQL.CrearParametroVarBinary("FotoPerfil", usuario.FotoPerfil));
            retVal = AccesoSQL.Escribir("pr_Actualizar_UsuarioFotoPerfil", parametros);

            return retVal;
        }

        public List<UsuarioBE> ListarProfesionalPorTratamiento(TratamientoBE tratamiento)
        {
            List<BE.UsuarioBE> myLista = new List<BE.UsuarioBE>();
            AccesoSQL AccesoSQL = new AccesoSQL();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(AccesoSQL.CrearParametroInt("Cod_Tratamiento", tratamiento.Cod_Tratamiento));
            DataTable tabla = AccesoSQL.Leer("pr_Listar_ProfesionalesPorTratamiento", parametros);
            if (tabla != null)
            {
                long SumaDVH = 0;
                foreach (DataRow fila in tabla.Rows)
                {
                    BE.UsuarioBE usuario = new BE.UsuarioBE();
                    TipoUsuarioBE tipoUsuario = new TipoUsuarioBE();
                    usuario.Cod_Usuario = int.Parse(fila["Cod_Usuario"].ToString());
                    usuario.Apellido = fila["Apellido"].ToString();
                    usuario.Nombre = fila["Nombre"].ToString();
                    usuario.Alias = fila["Alias"].ToString();
                    //usuario.Contraseña = SimpleDecrypt(fila["Contraseña"].ToString());
                    usuario.Contraseña = fila["Contraseña"].ToString();
                    tipoUsuario.Cod_Tipo = short.Parse(fila["Cod_Tipo"].ToString());
                    tipoUsuario.Descripcion_Tipo = fila["DescripcionTipo"].ToString();
                    usuario.TipoUsuario = tipoUsuario;
                    IdiomaBE idioma = new IdiomaBE();
                    idioma.IdIdioma = short.Parse(fila["IdIdioma"].ToString());
                    idioma.CodIdioma = fila["CodIdioma"].ToString();
                    idioma.DescripcionIdioma = fila["DescripcionIdioma"].ToString();
                    usuario.Idioma = idioma;
                    gestorIdioma.SetearIdioma(ref usuario);
                    usuario.Telefono = fila["Telefono"].ToString();
                    usuario.Mail = fila["Mail"].ToString();
                    if (!string.IsNullOrWhiteSpace(fila["FechaNacimiento"].ToString()))
                    {
                        usuario.FechaNacimiento = (DateTime)fila["FechaNacimiento"];
                    }
                    else
                    {
                        Nullable<DateTime> fNull = default(DateTime?);
                        usuario.FechaNacimiento = fNull;
                    }
                    usuario.Inactivo = (bool)fila["Inactivo"];
                    if (!string.IsNullOrWhiteSpace(fila["UltimoLogin"].ToString()))
                    {
                        usuario.UltimoLogin = (DateTime)fila["UltimoLogin"];
                    }
                    else
                    {
                        usuario.UltimoLogin = default(DateTime?);
                    }
                    usuario.IntentosEquivocados = short.Parse(fila["IntentosEquivocados"].ToString());
                    if (!string.IsNullOrWhiteSpace(fila["FotoPerfil"].ToString()))
                    {
                        Byte[] myByteArray = (Byte[])fila["FotoPerfil"];
                        MemoryStream ms = new MemoryStream(myByteArray);
                        usuario.FotoPerfil = (Bitmap)Image.FromStream(ms);
                    }

                    CalcularDVHUsuario(ref usuario);

                    if (int.Parse(fila["DVH"].ToString()) == usuario.DVH)
                    {
                        SumaDVH = +SumaDVH + usuario.DVH;
                        if (!usuario.Inactivo) { myLista.Add(usuario); }
                    }
                    else
                    {
                        try { throw new DAL.UsuarioModificadoException("¡ATENCION! \r\nAlgun dato fue modificado externamente a la aplicación para el usuario " + usuario.ToString()); }
                        catch (DAL.UsuarioModificadoException) { throw; }
                    }
                }
            }
            return myLista;
        }

        #region Encriptado
        public static string SimpleEncrypt(string PlainText)
        {
            string EncryptedString = String.Empty;
            try
            {
                string EncryptionKey = "fjkfjjd/&%gkfOOOOOO3###";
                System.Security.Cryptography.RijndaelManaged AES = new System.Security.Cryptography.RijndaelManaged();
                System.Security.Cryptography.MD5CryptoServiceProvider Hash_AES = new System.Security.Cryptography.MD5CryptoServiceProvider();

                byte[] hash = new byte[32];
                byte[] temp = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(EncryptionKey));

                Array.Copy(temp, 0, hash, 0, 16);
                Array.Copy(temp, 0, hash, 15, 16);

                AES.Key = hash;
                AES.Mode = System.Security.Cryptography.CipherMode.ECB;

                System.Security.Cryptography.ICryptoTransform DESEncrypter = AES.CreateEncryptor();
                byte[] Buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(PlainText);
                EncryptedString = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception)
            {
            }

            return EncryptedString;
        }

        public static string SimpleDecrypt(string EncryptedText)
        {
            string DecryptedString = string.Empty;
            try
            {
                string EncryptionKey = "fjkfjjd/&%gkfOOOOOO3###";
                System.Security.Cryptography.RijndaelManaged AES = new System.Security.Cryptography.RijndaelManaged();
                System.Security.Cryptography.MD5CryptoServiceProvider Hash_AES = new System.Security.Cryptography.MD5CryptoServiceProvider();

                byte[] hash = new byte[32];
                byte[] temp = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(EncryptionKey));

                Array.Copy(temp, 0, hash, 0, 16);
                Array.Copy(temp, 0, hash, 15, 16);

                AES.Key = hash;
                AES.Mode = System.Security.Cryptography.CipherMode.ECB;

                System.Security.Cryptography.ICryptoTransform DESDecrypter = AES.CreateDecryptor();
                byte[] Buffer = Convert.FromBase64String(EncryptedText);
                DecryptedString = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return DecryptedString;
        }
        #endregion
    }
}
