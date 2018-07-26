using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Katalog
{
    public static class PrintPDF
    {

        public static Document PrintTable(string Name, string[,] table)
        {
            // Create a new MigraDoc document

            Document document = new Document();

            document.Info.Title = "A sample invoice";
            document.Info.Subject = "Demonstrates how to create an invoice.";
            document.Info.Author = "Stefan Lange";

            DefineStyles(document);
            CreatePage(document, Name, table);
            //FillContent(document);

            PdfDocumentRenderer render = new PdfDocumentRenderer(true);
            render.Document = document;
            render.RenderDocument();
            render.PdfDocument.Save("test.pdf");
            Process.Start("test.pdf");
            return document;
        }



        public static Document CreateTemplate(List<LInfo> list)
        {
            // Create a new MigraDoc document

            Document document = new Document();

            document.Info.Title = "A sample invoice";
            document.Info.Subject = "Demonstrates how to create an invoice.";
            document.Info.Author = "Stefan Lange";

            DefineStyles(document);
            CreatePage(document, list);
            //FillContent(document);

            PdfDocumentRenderer render = new PdfDocumentRenderer(true);
            render.Document = document;
            render.RenderDocument();
            render.PdfDocument.Save("test.pdf");
            Process.Start("test.pdf");
            return document;
        }

        public static void DefineStyles(Document document)
        {
            // Get the predefined style Normal.
            Style style = document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Verdana";

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            // Create a new style called Reference based on style Normal
            style = document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        }


        public static void CreatePage(Document document, string Name, string[,] data)
        {
            // Each MigraDoc document needs at least one section.
            Section section = document.AddSection();

            // Put a logo in the header
            Image image = section.Headers.Primary.AddImage("../../PowerBooks.png");
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Right;
            image.WrapFormat.Style = WrapStyle.Through;

            // Create footer
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("Na potoce 390/27 · Třebíč");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Create the text frame for the address
            TextFrame addressFrame = section.AddTextFrame();
            addressFrame.Height = "3.0cm";
            addressFrame.Width = "7.0cm";
            addressFrame.Left = ShapePosition.Left;
            addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            addressFrame.Top = "5.0cm";
            addressFrame.RelativeVertical = RelativeVertical.Page;

            // Put sender in address frame
            paragraph = addressFrame.AddParagraph("Na potoce 390/27, Třebíč");
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 7;
            paragraph.Format.SpaceAfter = 3;

            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "8cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText(Name, TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddText("Třebíč, ");
            paragraph.AddDateField("dd.MM.yyyy");

            if (data != null && data.GetLength(0) >= 2)
            {
                // Create the item table
                Table table = section.AddTable();
                table.Style = "Table";
                //table.Borders.Color = TableBorder;
                table.Borders.Width = 0.25;
                table.Borders.Left.Width = 0.5;
                table.Borders.Right.Width = 0.5;
                table.Rows.LeftIndent = 0;

                // Before you can add a row, you must define the columns
                Column column;
                for (int i = 0; i < data.GetLength(1); i++)
                {
                    column = table.AddColumn(data[0, i]);
                    column.Format.Alignment = ParagraphAlignment.Center;
                }

                // Create the header of the table
                Row row = table.AddRow();
                row.HeadingFormat = true;
                row.Format.Alignment = ParagraphAlignment.Center;
                row.Format.Font.Bold = true;
                //row.Shading.Color = TableBlue;
                for (int i = 0; i < data.GetLength(1); i++)
                {
                    row.Cells[i].AddParagraph(data[1, i]);
                    row.Cells[i].Format.Alignment = ParagraphAlignment.Left;
                    row.Cells[i].VerticalAlignment = VerticalAlignment.Bottom;
                }
                
                //row.Cells[0].MergeDown = 1;
                //row.Cells[1].MergeRight = 3;

                table.SetEdge(0, 0, data.GetLength(1), 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);

                // Fill Table

                for (int i = 2; i < data.GetLength(0); i++)
                {
                    row = table.AddRow();
                    row.HeadingFormat = true;
                    row.Format.Alignment = ParagraphAlignment.Center;
                    row.Format.Font.Bold = false;

                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        row.Cells[j].AddParagraph(data[i, j]);
                        row.Cells[j].Format.Alignment = ParagraphAlignment.Left;
                        row.Cells[j].VerticalAlignment = VerticalAlignment.Bottom;
                    }
                }

                table.SetEdge(0, 0, data.GetLength(1), data.GetLength(0) - 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
            }
            
        }


        public static void CreatePage(Document document, List<LInfo> list)
        {
            // Each MigraDoc document needs at least one section.
            Section section = document.AddSection();

            // Put a logo in the header
            Image image = section.Headers.Primary.AddImage("../../PowerBooks.png");
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Right;
            image.WrapFormat.Style = WrapStyle.Through;

            // Create footer
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("Na potoce 390/27 · Třebíč");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Create the text frame for the address
            TextFrame addressFrame = section.AddTextFrame();
            addressFrame.Height = "3.0cm";
            addressFrame.Width = "7.0cm";
            addressFrame.Left = ShapePosition.Left;
            addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            addressFrame.Top = "5.0cm";
            addressFrame.RelativeVertical = RelativeVertical.Page;

            // Put sender in address frame
            paragraph = addressFrame.AddParagraph("Na potoce 390/27, Třebíč");
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 7;
            paragraph.Format.SpaceAfter = 3;

            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "8cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText(Lng.Get("Lended"), TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddText("Třebíč, ");
            paragraph.AddDateField("dd.MM.yyyy");

            // Create the item table
            Table table = section.AddTable();
            table.Style = "Table";
            //table.Borders.Color = TableBorder;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            // Before you can add a row, you must define the columns
            Column column = table.AddColumn("1cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn("2.5cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn("6cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            
            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            //row.Shading.Color = TableBlue;
            row.Cells[0].AddParagraph(Lng.Get("Number"));
            row.Cells[0].Format.Font.Bold = false;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
            row.Cells[1].AddParagraph(Lng.Get("Type"));
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].AddParagraph(Lng.Get("ItemName", "Name"));
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[3].AddParagraph(Lng.Get("LendFrom", "Lend from"));
            row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[4].AddParagraph(Lng.Get("LendTo", "Lend to"));
            row.Cells[4].Format.Alignment = ParagraphAlignment.Left;

            //row.Cells[0].MergeDown = 1;
            //row.Cells[1].MergeRight = 3;

            // Fill Table
            int i = 0;
            if (list != null && list.Count > 0)
            {
                foreach(var item in list)
                {
                    row = table.AddRow();
                    row.HeadingFormat = true;
                    row.Format.Alignment = ParagraphAlignment.Center;
                    row.Format.Font.Bold = true;
                    row.Cells[0].AddParagraph((i+1).ToString());
                    row.Cells[0].Format.Font.Bold = false;
                    row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
                    row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
                    row.Cells[1].AddParagraph(global.GetItemTypeName(item.ItemType));
                    row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                    row.Cells[2].AddParagraph(item.Name);
                    row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
                    row.Cells[3].AddParagraph(item.LendFrom.ToShortDateString());
                    row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
                    row.Cells[4].AddParagraph(item.LendTo.ToShortDateString());
                    row.Cells[4].Format.Alignment = ParagraphAlignment.Left;
                    i++;
                }
            }
            
            table.SetEdge(0, 0, 5, 1 + i, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
        }

        /*public static void FillContent(Document document)
        {
            // Fill address in address text frame
                        Paragraph paragraph = this.addressFrame.AddParagraph();
            paragraph.AddText(GetValue(item, "name/singleName"));
            paragraph.AddLineBreak();
            paragraph.AddText(GetValue(item, "address/line1"));
            paragraph.AddLineBreak();
            paragraph.AddText(GetValue(item, "address/postalCode") + " " + GetValue(item, "address/city"));

            // Iterate the invoice items
            double totalExtendedPrice = 0;
            XPathNodeIterator iter = this.navigator.Select("/invoice/items/*");
            while (iter.MoveNext())
            {
                item = iter.Current;
                double quantity = GetValueAsDouble(item, "quantity");
                double price = GetValueAsDouble(item, "price");
                double discount = GetValueAsDouble(item, "discount");

                // Each item fills two rows
                Row row1 = this.table.AddRow();
                Row row2 = this.table.AddRow();
                row1.TopPadding = 1.5;
                row1.Cells[0].Shading.Color = TableGray;
                row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row1.Cells[0].MergeDown = 1;
                row1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                row1.Cells[1].MergeRight = 3;
                row1.Cells[5].Shading.Color = TableGray;
                row1.Cells[5].MergeDown = 1;

                row1.Cells[0].AddParagraph(GetValue(item, "itemNumber"));
                paragraph = row1.Cells[1].AddParagraph();
                paragraph.AddFormattedText(GetValue(item, "title"), TextFormat.Bold);
                paragraph.AddFormattedText(" by ", TextFormat.Italic);
                paragraph.AddText(GetValue(item, "author"));
                row2.Cells[1].AddParagraph(GetValue(item, "quantity"));
                row2.Cells[2].AddParagraph(price.ToString("0.00") + " €");
                row2.Cells[3].AddParagraph(discount.ToString("0.0"));
                row2.Cells[4].AddParagraph();
                row2.Cells[5].AddParagraph(price.ToString("0.00"));
                double extendedPrice = quantity * price;
                extendedPrice = extendedPrice * (100 - discount) / 100;
                row1.Cells[5].AddParagraph(extendedPrice.ToString("0.00") + " €");
                row1.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
                totalExtendedPrice += extendedPrice;

                this.table.SetEdge(0, this.table.Rows.Count - 2, 6, 2, Edge.Box, BorderStyle.Single, 0.75);
            }

            // Add an invisible row as a space line to the table
            Row row = this.table.AddRow();
            row.Borders.Visible = false;

            // Add the total price row
            row = this.table.AddRow();
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("Total Price");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].MergeRight = 4;
            row.Cells[5].AddParagraph(totalExtendedPrice.ToString("0.00") + " €");

            // Add the VAT row
            row = this.table.AddRow();
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("VAT (19%)");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].MergeRight = 4;
            row.Cells[5].AddParagraph((0.19 * totalExtendedPrice).ToString("0.00") + " €");

            // Add the additional fee row
            row = this.table.AddRow();
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("Shipping and Handling");
            row.Cells[5].AddParagraph(0.ToString("0.00") + " €");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].MergeRight = 4;

            // Add the total due row
            row = this.table.AddRow();
            row.Cells[0].AddParagraph("Total Due");
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].MergeRight = 4;
            totalExtendedPrice += 0.19 * totalExtendedPrice;
            row.Cells[5].AddParagraph(totalExtendedPrice.ToString("0.00") + " €");

            // Set the borders of the specified cell range
            this.table.SetEdge(5, this.table.Rows.Count - 4, 1, 4, Edge.Box, BorderStyle.Single, 0.75);

            // Add the notes paragraph
            paragraph = this.document.LastSection.AddParagraph();
            paragraph.Format.SpaceBefore = "1cm";
            paragraph.Format.Borders.Width = 0.75;
            paragraph.Format.Borders.Distance = 3;
            paragraph.Format.Borders.Color = TableBorder;
            paragraph.Format.Shading.Color = TableGray;
            item = SelectItem("/invoice");
            paragraph.AddText(GetValue(item, "notes"));
        }*/

        public static void PrintHelloWord()
        {
            // ----- Create PDF -----
            PdfDocument document = new PdfDocument();

            // ----- New Page -----
            PdfPage page = document.AddPage();

            // ----- Create graphics -----
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // ----- Create font -----
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            // ----- Draw text -----

            gfx.DrawString("Hello, World!", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormat.Center);

            string filename = "HelloWorld.pdf";
            document.Save(filename);
            document.Close();
            Process.Start(filename);
        }
    }
}
