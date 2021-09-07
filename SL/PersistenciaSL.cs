using System;
using System.Data;
using System.Collections.Generic;
using BE;
using DAL;
using Newtonsoft.Json;


namespace SL
{
    public class PersistenciaSL
    {
        public int EscribirBitacoraXML(BitacoraBE bitacora)
        {
            XML_Mapper m = new XML_Mapper();
            return m.EscribirBitacoraXML(bitacora);
        }

        public int EscribirBitacoraJSON(List<BitacoraBE> datosBitacora, string fileName)
        {
            try
            {
                string json = JsonConvert.SerializeObject(datosBitacora.ToArray(), Formatting.Indented);
                System.IO.File.WriteAllText(fileName, "{\"Bitacora\": " + json + "}");
                return 1;
            }
            catch (Exception) { return 0; }
        }

        public DataSet LeerXML(BitacoraBE bitacora)
        {
            XML_Mapper m = new XML_Mapper();
            return m.LeerXML(bitacora);
        }

        public int EscribirIdiomaJSON(IdiomaBE idioma, List<TextoBE> datosLenguaje, string fileName)
        {
            try
            {
                string json = JsonConvert.SerializeObject(datosLenguaje.ToArray(), Formatting.Indented);
                System.IO.File.WriteAllText(fileName, "{\"" + idioma.ToString() + "\": " + json + "}");
                return 1;
            }
            catch (Exception) { return 0; }
        }

        public int EscribirIdiomaXML(IdiomaBE idioma, TextoBE texto, string fileName)
        {
            XML_Mapper m = new XML_Mapper();
            return m.EscribirIdiomaXML(idioma, texto, fileName);
        }
    }
}
