using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BE;

public class IdiomaMapper
{
    public int Insertar(IdiomaBE idioma)
    {
        AccesoSQL AccesoSQL = new AccesoSQL();
        List<SqlParameter> parametros = new List<SqlParameter>();
        parametros.Add(AccesoSQL.CrearParametroStr("CodIdioma", idioma.CodIdioma));
        parametros.Add(AccesoSQL.CrearParametroStr("DescripcionIdioma", idioma.DescripcionIdioma));
        return AccesoSQL.Escribir("pr_Insertar_Idioma", parametros);
    }

    public void SetearIdioma(ref UsuarioBE usuario)
    {
        AccesoSQL AccesoSQL = new AccesoSQL();
        List<SqlParameter> parametros = new List<SqlParameter>();
        parametros.Add(AccesoSQL.CrearParametroInt("IdIdioma", usuario.Idioma.IdIdioma));
        DataTable tabla = AccesoSQL.Leer("pr_Listar_TextosPorIdioma", parametros);
        if (tabla != null)
        {
            usuario.Idioma.Textos = new List<TextoBE>();
            foreach (DataRow fila in tabla.Rows)
            {
                TextoBE frase = new TextoBE();
                frase.IdFrase = short.Parse(fila["IdFrase"].ToString());
                frase.Texto = fila["Texto"].ToString();

                usuario.Idioma.Textos.Add(frase);
            }
        }
    }

    public List<IdiomaBE> Listar(IdiomaBE idiomaActual)
    {
        List<IdiomaBE> myLista = new List<IdiomaBE>();
        AccesoSQL AccesoSQL = new AccesoSQL();
        List<SqlParameter> parametros = new List<SqlParameter>();
        if (idiomaActual != null) { parametros.Add(AccesoSQL.CrearParametroInt("IdIdiomaTraduccion", idiomaActual.IdIdioma)); }
        else { parametros.Add(AccesoSQL.CrearParametroInt("IdIdiomaTraduccion", 0)); }
        DataTable tabla = AccesoSQL.Leer("pr_Listar_Idiomas", parametros);
        if (tabla != null)
        {
            foreach (DataRow fila in tabla.Rows)
            {
                IdiomaBE idioma = new IdiomaBE();
                idioma.IdIdioma = short.Parse(fila["IdIdioma"].ToString());
                idioma.CodIdioma = fila["CodIdioma"].ToString();
                idioma.DescripcionIdioma = fila["DescripcionIdioma"].ToString();

                myLista.Add(idioma);
            }
        }
        return myLista;
    }

    public List<TextoBE> ListarTextosDelIdioma(IdiomaBE idioma)
    {
        AccesoSQL AccesoSQL = new AccesoSQL();
        List<TextoBE> textos = new List<TextoBE>();
        List<SqlParameter> parametros = new List<SqlParameter>();
        parametros.Add(AccesoSQL.CrearParametroInt("IdIdioma", idioma.IdIdioma));
        DataTable tabla = AccesoSQL.Leer("pr_Listar_TextosPorIdioma", parametros);
        if (tabla != null)
        {
            foreach (DataRow fila in tabla.Rows)
            {
                TextoBE frase = new TextoBE();
                frase.IdFrase = short.Parse(fila["IdFrase"].ToString());
                frase.Texto = fila["Texto"].ToString();
                textos.Add(frase);
            }
        }
        return textos;
    }

    public int ActualizarNombreIdioma(IdiomaBE idioOrig, IdiomaBE idioDest, string descripcionIdiomaTraducido)
    {
        AccesoSQL AccesoSQL = new AccesoSQL();
        List<SqlParameter> parametros = new List<SqlParameter>();
        parametros.Add(AccesoSQL.CrearParametroInt("IdIdiomaOriginal", idioOrig.IdIdioma));
        parametros.Add(AccesoSQL.CrearParametroInt("IdIdiomaTraduccion", idioDest.IdIdioma));
        parametros.Add(AccesoSQL.CrearParametroStr("DescripcionIdiomaTraducido", descripcionIdiomaTraducido));
        return AccesoSQL.Escribir("pr_Actualizar_IdiomasNombresTraduccion", parametros);
    }

    public List<Tuple<IdiomaBE, IdiomaBE, string>> ListarNombresDeIdiomasParaTraducir()
    {
        var retList = new List<Tuple<IdiomaBE, IdiomaBE, string>>();
        AccesoSQL AccesoSQL = new AccesoSQL();
        DataTable tabla = AccesoSQL.Leer("pr_Listar_IdiomasNombresSinTraducir", null);
        if (tabla != null)
        {
            foreach (DataRow fila in tabla.Rows)
            {
                IdiomaBE idioOrig = new IdiomaBE();
                idioOrig.IdIdioma = short.Parse(fila["IdIdiomaOriginal"].ToString());

                IdiomaBE idioDest = new IdiomaBE();
                idioDest.IdIdioma = short.Parse(fila["IdIdiomaTraduccion"].ToString());
                idioDest.CodIdioma = fila["CodIdiomaDestino"].ToString();
                
                string texto = fila["Texto"].ToString();
                retList.Add(new Tuple<IdiomaBE, IdiomaBE, string>(idioOrig, idioDest, texto));
            }
        }
        return retList;
    }

    public int InsertarTexto(IdiomaBE idioma, TextoBE texto)
    {
        AccesoSQL AccesoSQL = new AccesoSQL();
        List<SqlParameter> parametros = new List<SqlParameter>();
        parametros.Add(AccesoSQL.CrearParametroStr("CodIdioma", idioma.CodIdioma));
        parametros.Add(AccesoSQL.CrearParametroInt("IdFrase", texto.IdFrase));
        parametros.Add(AccesoSQL.CrearParametroStr("Texto", texto.Texto));
        return AccesoSQL.Escribir("pr_Insertar_Texto", parametros);
    }

    public int ActualizarTexto(IdiomaBE idioma, TextoBE texto)
    {
        AccesoSQL AccesoSQL = new AccesoSQL();
        List<SqlParameter> parametros = new List<SqlParameter>();
        parametros.Add(AccesoSQL.CrearParametroInt("IdIdioma", idioma.IdIdioma));
        parametros.Add(AccesoSQL.CrearParametroInt("IdFrase", texto.IdFrase));
        parametros.Add(AccesoSQL.CrearParametroStr("Texto", texto.Texto));
        return AccesoSQL.Escribir("pr_Actualizar_TextoIdioma", parametros);
    }

    public int EliminarTextosDeIdioma(IdiomaBE idioma)
    {
        AccesoSQL AccesoSQL = new AccesoSQL();
        List<SqlParameter> parametros = new List<SqlParameter>();
        parametros.Add(AccesoSQL.CrearParametroStr("CodIdioma", idioma.CodIdioma));
        return AccesoSQL.Escribir("pr_Eliminar_TextosDeIdioma", parametros);
    }

    public int EliminarIdioma(IdiomaBE idioma)
    {
        AccesoSQL AccesoSQL = new AccesoSQL();
        List<SqlParameter> parametros = new List<SqlParameter>();
        parametros.Add(AccesoSQL.CrearParametroStr("CodIdioma", idioma.CodIdioma));
        return AccesoSQL.Escribir("pr_Eliminar_Idioma", parametros);
    }
}
