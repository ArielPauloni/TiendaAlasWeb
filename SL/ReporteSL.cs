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

namespace SL
{
    public class ReporteSL
    {
        IdiomaSL GestorIdioma = new IdiomaSL();

        public void GuardarPDF(string filePath, string headerText, string subHeaderText,
                               string textoLibre, GridView gridView)
        {
            try
            {
                PdfWriter writer = new PdfWriter(filePath);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, PageSize.A4, false);

                document.ShowTextAligned(new Paragraph(String
                       .Format("Tienda Alada")),
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
                //int numCol = 0;
                //foreach (DataGridViewColumn dc in gridView.Columns) { if (dc.Visible) { numCol++; } }
                //Table table = new Table(numCol, false);

                //foreach (DataGridViewColumn dc in gridView.Columns)
                //{
                //    if (dc.Visible)
                //    {
                //        Cell cell = new Cell(1, 1)
                //           .SetBackgroundColor(ColorConstants.GRAY)
                //           .SetTextAlignment(TextAlignment.CENTER)
                //           .Add(new Paragraph(dc.HeaderText));
                //        table.AddCell(cell);
                //    }
                //}

                //foreach (DataGridViewRow row in gridView.Rows)
                //{
                //    foreach (DataGridViewCell cell in row.Cells)
                //    {
                //        if (cell.Visible) { table.AddCell(cell.Value.ToString()); }
                //    }
                //}
                //table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

                //document.Add(table);
                document.Add(newline);
                //*****************************************************//
                // Page numbers
                //int n = pdf.GetNumberOfPages();
                //for (int i = 1; i <= n; i++)
                //{
                //    document.ShowTextAligned(new Paragraph(String
                //       .Format(GestorIdioma.TraducirTexto(SesionSL.Instancia.Usuario.Idioma, 167), i, n)),
                //        559, 15, i, TextAlignment.RIGHT,
                //        VerticalAlignment.BOTTOM, 0);
                //    document.ShowTextAligned(new Paragraph(String
                //       .Format("©Tienda Alada")),
                //        35, 15, i, TextAlignment.LEFT,
                //        VerticalAlignment.BOTTOM, 0);
                //}
                //*****************************************************//
                document.Close();
                System.Diagnostics.Process.Start("chrome.EXE", filePath);
            }
            catch (Exception)
            {
                throw;
            }
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
