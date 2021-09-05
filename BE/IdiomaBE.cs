using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

public class IdiomaBE
{
    private short idIdioma;

    public short IdIdioma
    {
        get { return idIdioma; }
        set { idIdioma = value; }
    }

    private string codIdioma;

    public string CodIdioma
    {
        get { return codIdioma; }
        set { codIdioma = value; }
    }

    private string descripcionIdioma;

    public string DescripcionIdioma
    {
        get { return descripcionIdioma; }
        set { descripcionIdioma = value; }
    }

    private List<TextoBE> textos;

    public List<TextoBE> Textos
    {
        get { return textos; }
        set { textos = value; }
    }

    public override string ToString()
    {
        return DescripcionIdioma;
    }
}
