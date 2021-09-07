using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Data;
using System.Reflection;
using BE;

namespace DAL
{
    public class XML_Mapper
    {
        DataSet ds = new DataSet();
        //string XmlFile = string.Format(@"{0}\", @"D:\Repositorio\Logs");

        public int EscribirBitacoraXML(BitacoraBE bitacora)
        {
            ds.Dispose();
            ds = null;

            //XmlFile = XmlFile + bitacora.FileName;

            if (!File.Exists(bitacora.FileName))
            {
                ds = new DataSet("Eventos");

                ds.Tables.Add(new DataTable("UnEvento"));

                DataTable tabla = ds.Tables[0];
                DataRow registro = tabla.NewRow();

                ds.Tables[0].Columns.Add(new DataColumn("FechaEvento"));
                ds.Tables[0].Columns.Add(new DataColumn("Cod_Evento"));
                ds.Tables[0].Columns.Add(new DataColumn("DescripcionEvento"));
                ds.Tables[0].Columns.Add(new DataColumn("Cod_Usuario"));
                ds.Tables[0].Columns.Add(new DataColumn("NombreUsuario"));
                ds.Tables[0].Columns.Add(new DataColumn("Criticidad"));
                ds.Tables[0].Columns.Add(new DataColumn("TextoCriticidad"));

                registro[0] = bitacora.FechaEvento.ToString();
                registro[1] = bitacora.Cod_Evento.ToString();
                registro[2] = bitacora.DescripcionEvento;
                registro[3] = bitacora.Cod_Usuario.ToString();
                registro[4] = bitacora.NombreUsuario;
                registro[5] = bitacora.Criticidad.ToString();
                registro[6] = bitacora.CriticidadTexto;

                ds.WriteXmlSchema(bitacora.FileName);

                tabla.Rows.Add(registro);
                ds.WriteXml(bitacora.FileName);
            }
            else
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(bitacora.FileName);
                XmlElement xelement = xdoc.CreateElement("UnEvento");

                XmlElement it1 = xdoc.CreateElement("FechaEvento");
                XmlElement it2 = xdoc.CreateElement("Cod_Evento");
                XmlElement it3 = xdoc.CreateElement("DescripcionEvento");
                XmlElement it4 = xdoc.CreateElement("Cod_Usuario");
                XmlElement it5 = xdoc.CreateElement("NombreUsuario");
                XmlElement it6 = xdoc.CreateElement("Criticidad");
                XmlElement it7 = xdoc.CreateElement("CriticidadTexto");
                it1.InnerText = bitacora.FechaEvento.ToString();
                it2.InnerText = bitacora.Cod_Evento.ToString();
                it3.InnerText = bitacora.DescripcionEvento;
                it4.InnerText = bitacora.Cod_Usuario.ToString();
                it5.InnerText = bitacora.NombreUsuario;
                it6.InnerText = bitacora.Criticidad.ToString();
                it7.InnerText = bitacora.CriticidadTexto;
                xelement.AppendChild(it1);
                xelement.AppendChild(it2);
                xelement.AppendChild(it3);
                xelement.AppendChild(it4);
                xelement.AppendChild(it5);
                xelement.AppendChild(it6);
                xelement.AppendChild(it7);

                xdoc.DocumentElement.AppendChild(xelement);
                xdoc.Save(bitacora.FileName);
            }
            return 1;
        }

        public int EscribirIdiomaXML(IdiomaBE idioma, TextoBE texto, string fileName)
        {
            ds.Dispose();
            ds = null;

            if (!File.Exists(fileName))
            {
                ds = new DataSet("Idioma");

                ds.Tables.Add(new DataTable("unaFrase"));

                DataTable tabla = ds.Tables[0];
                DataRow registro = tabla.NewRow();

                ds.Tables[0].Columns.Add(new DataColumn("Idioma"));
                ds.Tables[0].Columns.Add(new DataColumn("IdFrase"));
                ds.Tables[0].Columns.Add(new DataColumn("Texto"));

                registro[0] = idioma.ToString();
                registro[1] = texto.IdFrase.ToString();
                registro[2] = texto.Texto;

                ds.WriteXmlSchema(fileName);

                tabla.Rows.Add(registro);
                ds.WriteXml(fileName);
            }
            else
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(fileName);
                XmlElement xelement = xdoc.CreateElement("unaFrase");

                XmlElement it1 = xdoc.CreateElement("Idioma");
                XmlElement it2 = xdoc.CreateElement("IdFrase");
                XmlElement it3 = xdoc.CreateElement("Texto");
                it1.InnerText = idioma.ToString();
                it2.InnerText = texto.IdFrase.ToString();
                it3.InnerText = texto.Texto;
                xelement.AppendChild(it1);
                xelement.AppendChild(it2);
                xelement.AppendChild(it3);

                xdoc.DocumentElement.AppendChild(xelement);
                xdoc.Save(fileName);
            }
            return 1;
        }

        public DataSet LeerXML(BitacoraBE bitacora)
        {
            //filename = XmlFile + filename;
            DataSet salida = new DataSet();
            ds.Dispose();
            ds = null;
            ds = new DataSet();

            if (File.Exists(bitacora.FileName))
            {
                ds.ReadXml(bitacora.FileName);
            }
            salida = ds;
            return salida;
        }
    }
}

