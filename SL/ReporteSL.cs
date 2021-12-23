using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using BE;
using System.Web.UI.WebControls;
using System.Data;
using ClosedXML.Excel;

namespace SL
{
    public class ReporteSL
    {
        IdiomaSL GestorIdioma = new IdiomaSL();

        public void GuardarPDF(string filePath, string headerText, string subHeaderText,
                               string textoLibre, DataTable dt, string pageFormatStr)
        {
            try
            {
                PdfWriter writer = new PdfWriter(filePath);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, PageSize.A4, false);

                document.ShowTextAligned(new Paragraph(String
                       .Format("Tienda Alas")),
                        35, 806, 1, TextAlignment.LEFT,
                        VerticalAlignment.TOP, 0);
                document.ShowTextAligned(new Paragraph(String
                       .Format(DateTime.Now.ToString("dd/MM/yyyy"))),
                        559, 806, 1, TextAlignment.RIGHT,
                        VerticalAlignment.TOP, 0);

                //Título:
                Paragraph header = new Paragraph(headerText.ToUpper())
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(18);
                header.SetBold();
                document.Add(header);
                //*****************************************************//
                // New line
                Paragraph newline = new Paragraph(new Text("\n"));
                //*****************************************************//
                //Subtítulo:
                Paragraph subheader = new Paragraph(subHeaderText)
                     .SetTextAlignment(TextAlignment.CENTER)
                     .SetFontSize(13);
                document.Add(subheader);
                //*****************************************************//
                // Line separator
                LineSeparator ls = new LineSeparator(new SolidLine());
                document.Add(ls);
                //*****************************************************//
                // Add paragraph1
                Paragraph paragraph1 = new Paragraph(textoLibre);
                document.Add(paragraph1);
                document.Add(newline);
                //*****************************************************//
                // Table
                if (dt.Rows.Count > 0)
                {
                    int numCol = dt.Columns.Count;
                    string colName = string.Empty;
                    iText.Layout.Element.Table table = new iText.Layout.Element.Table(numCol, false);

                    //Seteo los encabezados:
                    for (int i = 0; i < 1; i++)
                    {
                        DataRow myRow = dt.Rows[i];
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            Cell cell = new Cell(1, 1)
                               .SetBackgroundColor(ColorConstants.GRAY)
                               .SetTextAlignment(TextAlignment.CENTER)
                               .Add(new Paragraph(dt.Columns[j].ColumnName));
                            table.AddCell(cell);
                        }
                    }

                    //Seteo los datos:
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow myRow = dt.Rows[i];
                        for (int j = 0; j < dt.Columns.Count; j++) { table.AddCell(myRow.ItemArray[j].ToString()); }
                    }

                    table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                    document.Add(table);
                }

                document.Add(newline);
                //*****************************************************//
                // Page numbers
                int n = pdf.GetNumberOfPages();
                for (int i = 1; i <= n; i++)
                {
                    document.ShowTextAligned(new Paragraph(String
                       .Format(pageFormatStr, i, n)),
                        559, 15, i, TextAlignment.RIGHT,
                        VerticalAlignment.BOTTOM, 0);
                    document.ShowTextAligned(new Paragraph(String
                       .Format("©Tienda Alas")),
                        35, 15, i, TextAlignment.LEFT,
                        VerticalAlignment.BOTTOM, 0);
                }
                //*****************************************************//
                document.Close();
                //System.Diagnostics.Process.Start("chrome.EXE", filePath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GuardarGraficoPDF(string filePath, string headerText, string subHeaderText,
                               string textoLibre, byte[] imagen, string pageFormatStr)
        {
            try
            {
                PdfWriter writer = new PdfWriter(filePath);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, PageSize.A4, false);

                document.ShowTextAligned(new Paragraph(String
                       .Format("Tienda Alas")),
                        35, 806, 1, TextAlignment.LEFT,
                        VerticalAlignment.TOP, 0);
                document.ShowTextAligned(new Paragraph(String
                       .Format(DateTime.Now.ToString("dd/MM/yyyy"))),
                        559, 806, 1, TextAlignment.RIGHT,
                        VerticalAlignment.TOP, 0);

                //Título:
                Paragraph header = new Paragraph(headerText.ToUpper())
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(18);
                header.SetBold();
                document.Add(header);
                //*****************************************************//
                // New line
                Paragraph newline = new Paragraph(new Text("\n"));
                //*****************************************************//
                //Subtítulo:
                Paragraph subheader = new Paragraph(subHeaderText)
                     .SetTextAlignment(TextAlignment.CENTER)
                     .SetFontSize(13);
                document.Add(subheader);
                //*****************************************************//
                // Line separator
                LineSeparator ls = new LineSeparator(new SolidLine());
                document.Add(ls);
                document.Add(newline);
                //*****************************************************//
                // Add image
                iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory
                   .Create(imagen))
                   .SetTextAlignment(TextAlignment.CENTER);
                img.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                document.Add(img);

                document.Add(newline);
                //*****************************************************//
                // Add paragraph1
                Paragraph paragraph1 = new Paragraph(textoLibre);
                document.Add(paragraph1);
                document.Add(newline);
                //*****************************************************//
                // Page numbers
                int n = pdf.GetNumberOfPages();
                for (int i = 1; i <= n; i++)
                {
                    document.ShowTextAligned(new Paragraph(String
                       .Format(pageFormatStr, i, n)),
                        559, 15, i, TextAlignment.RIGHT,
                        VerticalAlignment.BOTTOM, 0);
                    document.ShowTextAligned(new Paragraph(String
                       .Format("©Tienda Alas")),
                        35, 15, i, TextAlignment.LEFT,
                        VerticalAlignment.BOTTOM, 0);
                }
                //*****************************************************//
                document.Close();
                //System.Diagnostics.Process.Start("chrome.EXE", filePath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Funcion que Exporta un listado de Objetos a un Archivo Excel definido
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto a Listar</typeparam>
        /// <param name="ListaDeObjetos">Listado de Objetos a Incluir en el Excel</param>
        /// <param name="NombreDeArchivo">Nombre de archivo completo, incluyendo ruta completa y extensión .xlsx</param>
        /// <param name="NombreDeHoja">Nombre de la Hoja en el Archivo Excel</param>
        /// <param name="Titulo">Titulo que aparece en la primer celda de la hoja</param>
        /// <returns>Verdadero (true) en caso de exito y Falso (false)  en caso de error</returns>
        public bool ExportarAExcel<T>(List<T> ListaDeObjetos, string NombreDeArchivo, string NombreDeHoja, string Titulo)
        {
            bool exported = false;
            using (IXLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(NombreDeHoja).FirstCell().SetValue<string>("");
                //workbook.Worksheets.Worksheet(NombreDeHoja).FirstCell().Style.Font.Bold = true;
                //workbook.Worksheets.Worksheet(NombreDeHoja).FirstCell().Style.Font.FontSize = 14;

                workbook.Worksheets.Worksheet(NombreDeHoja).Cell(1, 1).InsertTable<T>(ListaDeObjetos, Titulo);
                workbook.Worksheets.Worksheet(NombreDeHoja).Tables.Table(Titulo).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                workbook.Worksheets.Worksheet(NombreDeHoja).Tables.Table(Titulo).Style.Border.InsideBorder = XLBorderStyleValues.Medium;

                workbook.SaveAs(NombreDeArchivo);

                exported = true;
            }
            return exported;
        }

        //public void GuardarEncuestaPDF(string filePath, string headerText, string subHeaderText,
        //                       string textoLibre, EncuestaBE encuesta)
        //{
        //    try
        //    {
        //        PdfWriter writer = new PdfWriter(filePath);
        //        PdfDocument pdf = new PdfDocument(writer);
        //        Document document = new Document(pdf, PageSize.A4, false);

        //        document.ShowTextAligned(new Paragraph(String
        //               .Format("Tienda Alada")),
        //                35, 806, 1, TextAlignment.LEFT,
        //                VerticalAlignment.TOP, 0);
        //        document.ShowTextAligned(new Paragraph(String
        //               .Format(DateTime.Now.ToString("dd/MM/yyyy"))),
        //                559, 806, 1, TextAlignment.RIGHT,
        //                VerticalAlignment.TOP, 0);

        //        //Título:
        //        Paragraph header = new Paragraph(headerText.ToUpper())
        //           .SetTextAlignment(TextAlignment.CENTER)
        //           .SetFontSize(18);
        //        header.SetBold();
        //        document.Add(header);
        //        //*****************************************************//
        //        // New line
        //        Paragraph newline = new Paragraph(new Text("\n"));
        //        //*****************************************************//
        //        //Subtítulo:
        //        Paragraph subheader = new Paragraph(subHeaderText)
        //             .SetTextAlignment(TextAlignment.CENTER)
        //             .SetFontSize(13);
        //        document.Add(subheader);
        //        //*****************************************************//
        //        // Line separator
        //        LineSeparator ls = new LineSeparator(new SolidLine());
        //        document.Add(ls);
        //        //*****************************************************//
        //        // Add paragraph1
        //        Paragraph paragraph1 = new Paragraph(textoLibre);
        //        document.Add(paragraph1);
        //        //document.Add(newline);
        //        //*****************************************************//
        //        //Preguntas
        //        foreach (PreguntaBE pregunta in encuesta.Preguntas)
        //        {
        //            paragraph1 = new Paragraph(pregunta.Pregunta);
        //            paragraph1.SetBold();
        //            document.Add(paragraph1);
        //            Table table = new Table(2, false);
        //            foreach (RespuestaBE respuesta in pregunta.Respuestas)
        //            {
        //                table.AddCell("[  ]");
        //                table.AddCell(respuesta.Respuesta);
        //            }
        //            document.Add(table);
        //            document.Add(newline);
        //        }
        //        //*****************************************************//
        //        // Hyper link
        //        Link link = new Link(GestorIdioma.TraducirTexto(SesionSL.Instancia.Usuario.Idioma, 172),
        //           PdfAction.CreateURI("https://instagram.com/alasparaelalmaholistico?igshid=xnwjtoph4r6x"));
        //        Paragraph hyperLink = new Paragraph(GestorIdioma.TraducirTexto(SesionSL.Instancia.Usuario.Idioma, 171) + " ")
        //           .Add(link.SetBold().SetUnderline()
        //           .SetItalic().SetFontColor(ColorConstants.BLUE))
        //           .Add(" " + GestorIdioma.TraducirTexto(SesionSL.Instancia.Usuario.Idioma, 173));

        //        //document.Add(newline);
        //        document.Add(hyperLink);
        //        //*****************************************************//
        //        // Page numbers
        //        int n = pdf.GetNumberOfPages();
        //        for (int i = 1; i <= n; i++)
        //        {
        //            document.ShowTextAligned(new Paragraph(String
        //               .Format(GestorIdioma.TraducirTexto(SesionSL.Instancia.Usuario.Idioma, 167), i, n)),
        //                559, 15, i, TextAlignment.RIGHT,
        //                VerticalAlignment.BOTTOM, 0);
        //            document.ShowTextAligned(new Paragraph(String
        //               .Format("©Tienda Alada")),
        //                35, 15, i, TextAlignment.LEFT,
        //                VerticalAlignment.BOTTOM, 0);
        //        }
        //        //*****************************************************//
        //        document.Close();
        //        System.Diagnostics.Process.Start("chrome.EXE", filePath);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
