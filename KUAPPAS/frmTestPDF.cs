using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Drawing;
using System.Diagnostics;

namespace KUAPPAS
{
    public partial class frmTestPDF : Form
    {
        //Create PDF grid 
        PdfGrid pdfGrid2;

        //Create PDF page 
        PdfPage previousPage;
        bool SaatnyacetakKesimpulan;
        float PosisiTerakhir;
         PdfLayoutResult layoutResult = null;
        int columnCount;
        int count = 0;
        DateTime mTanggalAwal;
        DateTime mTanggalAkhir;
       string[] headerValues = { "OrderID", "CustomerID", "ShipName", "ShipAddress", "ShipCity", "ShipPostalCode" };
       bool isNewPage;
        public frmTestPDF()
        {
            InitializeComponent();
        }

        private void cmeHeaderFooter_Click(object sender, EventArgs e)
        {
            //Create a new PDF document.
            PdfDocument pdfDocument = new PdfDocument();
            //Add a page to the PDF document
            PdfPage pdfPage = pdfDocument.Pages.Add();
            //Create a header and draw the image.
            RectangleF bounds = new RectangleF(0, 0, pdfDocument.Pages[0].GetClientSize().Width, 50);
            PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);
            //Load the PDF document
            FileStream imageStream = new FileStream("Logo.png", FileMode.Open, FileAccess.Read);
            PdfImage image = new PdfBitmap(imageStream);
            //Draw the image in the header.
            header.Graphics.DrawImage(image, new PointF(0, 0), new SizeF(100, 50));
            //Add the header at the top.
            pdfDocument.Template.Top = header;
             


            //Create a Page template that can be used as footer.
            PdfPageTemplateElement footer = new PdfPageTemplateElement(bounds);
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 7);
            PdfBrush brush = new PdfSolidBrush(Color.Black);
            //Create page number field.
            PdfPageNumberField pageNumber = new PdfPageNumberField(font, brush);
            //Create page count field.
            PdfPageCountField count = new PdfPageCountField(font, brush);
            //Add the fields in composite fields.
            PdfCompositeField compositeField = new PdfCompositeField(font, brush, "Page {0} of {1}", pageNumber, count);
            compositeField.Bounds = footer.Bounds;
            //Draw the composite field in footer.
            compositeField.Draw(footer.Graphics, new PointF(470, 40));
            //Add the footer template at the bottom.
            pdfDocument.Template.Bottom = footer;

            //Save the document into stream.
            MemoryStream stream = new MemoryStream();
            pdfDocument.Save(stream);
            //Closes the document.
            pdfDocument.Save("header.pdf");
   
            pdfDocument.Close(true);
            //Save and close the document
          
            //This will open the PDF file so, the result will be seen in default PDF viewer
            System.Diagnostics.Process.Start("header.pdf");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PdfDocument doc = new PdfDocument();
            //Add a new page
            PdfPage page = doc.Pages.Add();
            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;

            #region Polygon
            //Add pen
            PdfPen pen = new PdfPen(PdfBrushes.Brown, 10f);
            PdfPen penLine = new PdfPen(PdfBrushes.Green, 1f);

            //Create a gradient brush
            PdfLinearGradientBrush brush = new PdfLinearGradientBrush(new PointF(10, 100), new PointF(100, 200), new PdfColor(Color.Red), new PdfColor(Color.Green));
            //Create polygon points
            PointF p1 = new PointF(10, 100);
            PointF p2 = new PointF(10, 200);
            PointF p3 = new PointF(100, 100);
            PointF p4 = new PointF(100, 200);
            PointF p5 = new PointF(55, 150);
            PointF[] points = { p1, p2, p3, p4, p5 };
            //Draw polygon
            graphics.DrawPolygon(pen, brush, points);
            graphics.DrawLine(penLine, p1, p3);
            #endregion

            #region Arc
            pen.Width = 11;
            pen.LineCap = PdfLineCap.Round;
            RectangleF rect = new RectangleF(300, 50, 200, 200);
            //Draw an arc
            graphics.DrawArc(pen, rect, 0, 90);

            pen.Color = Color.DarkBlue;
            rect.X -= 10;
            graphics.DrawArc(pen, rect, 90, 90);

            pen.Color = Color.Red;
            rect.Y -= 10;
            graphics.DrawArc(pen, rect, 180, 90);

            pen.Color = Color.DarkCyan;
            rect.X += 10;
            graphics.DrawArc(pen, rect, 270, 90);
            #endregion

            #region Rectangle
            pen.Color = Color.Green;
            pen.Width = 2;
            //Draw rectangle
            graphics.DrawRectangle(pen, null, 10, 300, 200, 100);
            #endregion

            //Save and close the document
            doc.Save("Shapes.pdf");
            doc.Close(true);
            //This will open the PDF file so, the result will be seen in default PDF viewer
            System.Diagnostics.Process.Start("Shapes.pdf");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PdfDocument document = new PdfDocument();
//Create a solid brush and standard font
PdfBrush brush = new PdfSolidBrush(Color.Black);
PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 14);

//Section - 1
//Add new section to the document
PdfSection section = document.Sections.Add();
//Create page settings to the section
section.PageSettings.Rotate = PdfPageRotateAngle.RotateAngle0;
section.PageSettings.Size = PdfPageSize.A5;
section.PageSettings.Width = 300;
section.PageSettings.Height = 400;
//Add page to the section and initialize graphics for the page
PdfPage page = section.Pages.Add();
PdfGraphics graphics = page.Graphics;
//Draw simple text on the page
graphics.DrawString("Rotated by 0 degrees", font, brush, new PointF(20, 20));

//Section - 2
//Add new section to the document
section = document.Sections.Add();
//Create page settings to the section
section.PageSettings.Rotate = PdfPageRotateAngle.RotateAngle90;
section.PageSettings.Width = 300;
section.PageSettings.Height = 400;
//Add page to the section and initialize graphics for the page
page = section.Pages.Add();
graphics = page.Graphics;
//Draw simple text on the page
graphics.DrawString("Rotated by 90 degrees", font, brush, new PointF(20, 20));

//Section - 3
//Add new section to the document
section = document.Sections.Add();
//Create page settings to the section
section.PageSettings.Rotate = PdfPageRotateAngle.RotateAngle180;
section.PageSettings.Width = 500;
section.PageSettings.Height = 200;
//Add page to the section and initialize graphics for the page
page = section.Pages.Add();
graphics = page.Graphics;
//Draw simple text on the page
graphics.DrawString("Rotated by 180 degrees", font, brush, new PointF(20, 20));

//Section - 4
//Add new section to the document
section = document.Sections.Add();
//Create page settings to the section
section.PageSettings.Rotate = PdfPageRotateAngle.RotateAngle270;
section.PageSettings.Width = 300;
section.PageSettings.Height = 200;
//Add page to the section and initialize graphics for the page
page = section.Pages.Add();
graphics = page.Graphics;
//Draw simple text on the page
graphics.DrawString("Rotated by 270 degrees", font, brush, new PointF(20, 20));

//Saving the PDF to the MemoryStream
MemoryStream stream = new MemoryStream();
document.Save("Section.pdf");

document.Close(true);
//Save and close the document

//This will open the PDF file so, the result will be seen in default PDF viewer
System.Diagnostics.Process.Start("Section.pdf");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Create a PDF document instance.
            PdfDocument document = new PdfDocument();
            //Add the event.
            document.Pages.PageAdded += Pages_PageAdded;
            //Create a new page and add it as the last page of the document.
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            //Read the long text from the text file.
            FileStream inputStream = new FileStream("Input.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inputStream, Encoding.ASCII);
            string text = reader.ReadToEnd();
            reader.Dispose();
            const int paragraphGap = 10;
            //Create a text element with the text and font.
            PdfTextElement textElement = new PdfTextElement(text, new PdfStandardFont(PdfFontFamily.TimesRoman, 14));
            PdfLayoutFormat layoutFormat = new PdfLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;
            layoutFormat.Break = PdfLayoutBreakType.FitPage;
            //Draw the first paragraph.
            PdfLayoutResult result = textElement.Draw(page, new RectangleF(0, 0, page.GetClientSize().Width / 2, page.GetClientSize().Height), layoutFormat);
            //Draw the second paragraph from the first paragraph’s end position.
            result = textElement.Draw(result.Page, new RectangleF(0, result.Bounds.Bottom + paragraphGap, page.GetClientSize().Width / 2, page.GetClientSize().Height), layoutFormat);

            //Creating the stream object.
            MemoryStream stream = new MemoryStream();
            //Save the document into memory stream.
            document.Save(stream);
            document.Save("OnAddPage.pdf");


            document.Close(true);
            //Save and close the document

            //This will open the PDF file so, the result will be seen in default PDF viewer
            System.Diagnostics.Process.Start("OnAddPage.pdf");
            
        }
        //Event handler for PageAdded event.
        void Pages_PageAdded(object sender, PageAddedEventArgs args)
        {
        PdfPage page = args.Page;
        page.Graphics.DrawRectangle(PdfPens.Black, new RectangleF(0, 0, page.GetClientSize().Width, page.GetClientSize().Height));
        }
        public DataTable CreateFooterDataTable(List<float> sumOfCells)
        {
            //Create a DataTable
            DataTable dataTable = new DataTable();

           

            //Add rows to the DataTable
            dataTable.Rows.Add(new object[] { "Hanari Carne", "189081.19", "10949.75", "45880.24", "50306.60", "312013.78", "189739.24", "133186.24", "10911.70" });
            dataTable.Rows.Add(new object[] { "Sum of cells", sumOfCells[0], sumOfCells[1], sumOfCells[2], sumOfCells[3], sumOfCells[4], sumOfCells[5], sumOfCells[6], sumOfCells[7] });

            return dataTable;
        }
        private void DrawFooter(PdfPage page, PdfGrid pdfGrid, List<float> sumOfCells)
        {
            //Create a PDF Template
            PdfTemplate template = new PdfTemplate(page.GetClientSize().Width, 80);

            //Create table in footer 
            PdfGrid footerGrid = new PdfGrid();

            //Get a DataTable
            //DataTable dataTable = CreateFooterDataTable(sumOfCells);
            List<object> data = new List<object>();
        
            data = new List<object>();
            Object grid2row1 = new { Name = "Andrew", Age = "21", Sex = "Male" };
            Object grid2row2 = new { Name = "Steven", Age = "22", Sex = "Female" };
            Object grid2row3 = new { Name = "Michael", Age = "24", Sex = "Male" };

            data.Add(grid2row1);
            data.Add(grid2row2);
            data.Add(grid2row3);
            //IEnumerable<object> dataTable = data;
            ////Add list to IEnumerable.
            //dataTable = data;
            //Assign data source
            footerGrid.DataSource = data;// dataTable;

            //Assign column with same as existing grid width 
          //  footerGrid.Columns[0].Width = pdfGrid.Columns[0].Width + pdfGrid.Columns[1].Width;

            ////Create grid style to apply padding for entire grid 
            PdfGridStyle gridStyle = new PdfGridStyle();
            gridStyle.CellPadding = new PdfPaddings(3, 3, 3, 3);

            //Apply style to grid 
            footerGrid.Style = gridStyle;

            //Call BeginCellLayout event handler to apply style and string format 
            //footerGrid.BeginCellLayout += FooterGrid_BeginCellLayout; ;

            //Draw table in template graphics 
            //footerGrid.Draw(template.Graphics, new RectangleF(0, 0, page.GetClientSize().Width, 80));
            if (SaatnyacetakKesimpulan)
            footerGrid.Draw(page, new PointF(10, PosisiTerakhir+20));
            //Draw the template on the page graphics of the document
//            page.Graphics.DrawPdfTemplate(template, new PointF(0, page.GetClientSize().Height - 80), new SizeF(page.GetClientSize().Width, 80));
            //page.Graphics.DrawPdfTemplate(template, new PointF(0, page.GetClientSize().Height - 80), new SizeF(page.GetClientSize().Width, 80));

        }
        private void XPages_PageAdded(object sender, PageAddedEventArgs args)
        {
            //Add calculated cell values to list 
            List<float> sumOfCells = new List<float>();
          

         //   if (args.Page.==)
            //Draw footer table 
            DrawFooter(previousPage, pdfGrid2, sumOfCells);

            //Assignn current page
            previousPage = args.Page;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            PdfSection section = document.Sections.Add();
            section.PageSettings.Size = PdfPageSize.Legal;
            SaatnyacetakKesimpulan = false;
            //Add a page.
            PdfPage page = document.Pages.Add();
            previousPage = page;
            document.Pages.PageAdded += XPages_PageAdded; 
            //Create a new PdfGrid instance.
            PdfGrid pdfGrid = new PdfGrid();
            //Add values to the list.
            List<object> data = new List<object>();
            Object grid1row1 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row2 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row3 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row11 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row12 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row13 = new { ID = "E03", Name = "Simon", Salary = "$12,000" }; 
            Object grid1row22 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row33 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row41 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row42 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row43 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row51 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row52 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row53 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row61 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row62 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row63 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row71 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row72 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row73 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row81 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row82 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row83 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row91 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row92 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row93 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row101 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row102 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row103 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row201 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row202 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row203 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };

            Object grid1row19 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row29 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row39 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row119 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row129 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row139 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row229 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row339 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row419 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row429 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row439 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row519 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row529 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row539 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row619 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row629 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row639 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row719 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row729 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row739 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row819 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row829 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row839 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row919 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row929 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row939 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row1019 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row1029 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row1039 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            Object grid1row2019 = new { ID = "E01", Name = "Clay", Salary = "$10,000" };
            Object grid1row2029 = new { ID = "E02", Name = "Thomas", Salary = "$10,500" };
            Object grid1row2039 = new { ID = "E03", Name = "Simon", Salary = "$12,000" };
            
           
            data.Add(grid1row1);
            data.Add(grid1row2);
            data.Add(grid1row3);
            data.Add(grid1row11);
            data.Add(grid1row12);
            data.Add(grid1row13 );
            data.Add(grid1row22 );
            data.Add(grid1row33 );
            data.Add(grid1row41 );
            data.Add(grid1row42);
            data.Add(grid1row43);
            data.Add(grid1row51);
            data.Add(grid1row52);
            data.Add(grid1row53 );
            data.Add(grid1row61);
            data.Add(grid1row62 );
            data.Add(grid1row63 );
            data.Add(grid1row71 );
            data.Add(grid1row72 );
            data.Add(grid1row73 );
            data.Add(grid1row81 );
            data.Add(grid1row82);
            data.Add(grid1row83);
            data.Add(grid1row91 );
            data.Add(grid1row92 );
            data.Add(grid1row93 );
            data.Add(grid1row101 );
            data.Add(grid1row102 );
            data.Add(grid1row103 );
            data.Add(grid1row201 );
            data.Add(grid1row202 );
            data.Add(grid1row203);
            data.Add(grid1row19);
            data.Add(grid1row29);
            data.Add(grid1row39);
            data.Add(grid1row119);
            data.Add(grid1row129);
            data.Add(grid1row139);
            data.Add(grid1row229);
            data.Add(grid1row339);
            data.Add(grid1row419);
            data.Add(grid1row429);
            data.Add(grid1row439);
            data.Add(grid1row519);
            data.Add(grid1row529);
            data.Add(grid1row539);
            data.Add(grid1row619);
            data.Add(grid1row629);
            data.Add(grid1row639);
            data.Add(grid1row719);
            data.Add(grid1row729);
            data.Add(grid1row739);
            data.Add(grid1row819);
            data.Add(grid1row829);
            data.Add(grid1row839);
            data.Add(grid1row919);
            data.Add(grid1row929);
            data.Add(grid1row939);
            data.Add(grid1row1019);
            data.Add(grid1row1029);
            data.Add(grid1row1039);
            data.Add(grid1row2019);
            data.Add(grid1row2029);
            data.Add(grid1row2039);
            
            //Add list to IEnumerable.
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Draw the grid on the page of a PDF document and store the grid position in PdfGridLayoutResult.
            PdfGridLayoutResult pdfGridLayoutResult = pdfGrid.Draw(page, new PointF(10, 10));
            //section = document.Sections.Add();
            PosisiTerakhir = pdfGridLayoutResult.Bounds.Bottom;
            SaatnyacetakKesimpulan = true;

            page = document.Pages.Add();
            

            ////Initialize PdfGrid and list.
            //pdfGrid2 = new PdfGrid();
            //data = new List<object>();
            ////Add values to the list.
            //Object grid2row1 = new { Name = "Andrew", Age = "21", Sex = "Male" };
            //Object grid2row2 = new { Name = "Steven", Age = "22", Sex = "Female" };
            //Object grid2row3 = new { Name = "Michael", Age = "24", Sex = "Male" };

            //data.Add(grid2row1);
            //data.Add(grid2row2);
            //data.Add(grid2row3);

            ////Add list to IEnumerable.
            //dataTable = data;
            ////Assign data source.
            //pdfGrid.DataSource = dataTable;
            ////Draw the grid on the page using previous result.
            //pdfGrid.Draw(page, new PointF(10, pdfGridLayoutResult.Bounds.Bottom + 20));

            //Saving the PDF to the MemoryStream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Close the document.
     
            document.Save("BanyakTable.pdf");

            document.Close(true);

            
            //This will open the PDF file so, the result will be seen in default PDF viewer
            System.Diagnostics.Process.Start("BanyakTable.pdf");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Create a new PDF document.
            PdfDocument pdfDocument = new PdfDocument();
            //Create the page.
            PdfPage pdfPage = pdfDocument.Pages.Add();
            //Create the parent grid.
            PdfGrid parentPdfGrid = new PdfGrid();

            //Add the rows.
            PdfGridRow row1 = parentPdfGrid.Rows.Add();
            PdfGridRow row2 = parentPdfGrid.Rows.Add();
            row2.Height = 58;
            //Add the columns.
            parentPdfGrid.Columns.Add(3);
            //Set the value to the specific cell.
            parentPdfGrid.Rows[0].Cells[0].Value = "Nested Table";
            parentPdfGrid.Rows[0].Cells[1].RowSpan = 2;
            parentPdfGrid.Rows[0].Cells[1].ColumnSpan = 2;
            //Create the child table.
            PdfGrid childPdfGrid = new PdfGrid();
            //Set the column and rows for the child grid.
            childPdfGrid.Columns.Add(5);
            for (int i = 0; i < 5; i++)
            {
                PdfGridRow row = childPdfGrid.Rows.Add();
                for (int j = 0; j < 5; j++)
                {
                    row.Cells[j].Value = String.Format("Cell [{0} {1}]", j, i);
                }
            }
            //Set the value as another PdfGrid in a cell.
            parentPdfGrid.Rows[0].Cells[1].Value = childPdfGrid;
            //Specify the style for the PdfGridCell.
            PdfGridCellStyle pdfGridCellStyle = new PdfGridCellStyle();
            pdfGridCellStyle.TextPen = PdfPens.Red;
            pdfGridCellStyle.Borders.All = PdfPens.Red;
            pdfGridCellStyle.CellPadding.Left = 2; 
            //Load the image as a stream.
            //FileStream imageStream = new FileStream("Logo.BMP", FileMode.Open, FileAccess.Read);
            //pdfGridCellStyle.BackgroundImage = new PdfBitmap(imageStream);
            PdfGridCell pdfGridCell = parentPdfGrid.Rows[0].Cells[0];
            //Apply style.
            pdfGridCell.Style = pdfGridCellStyle;
            //Set image position for the background image in style.
            pdfGridCell.ImagePosition = PdfGridImagePosition.Fit;
            //Draw the PdfGrid.
            parentPdfGrid.Draw(pdfPage, PointF.Empty);

            //Creating the stream object.
            MemoryStream stream = new MemoryStream();
            //Save the PDF document to stream.
            pdfDocument.Save(stream);
            //Close the document.
            pdfDocument.Save("PdfGridCustom.pdf");

            pdfDocument.Close(true);


            //This will open the PDF file so, the result will be seen in default PDF viewer
            System.Diagnostics.Process.Start("PdfGridCustom.pdf");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Create a new PDF document.
            PdfDocument pdfDocument = new PdfDocument();
            //Add page. 
            PdfPage pdfPage = pdfDocument.Pages.Add();

            //Create a new PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Add values to the list.
            List<object> data = new List<object>();
            Object row1 = new { ID = "E01", Name = "John" };
            Object row2 = new { ID = "E02", Name = "Thomas" };
            Object row3 = new { ID = "E03", Name = "Peter" };
            data.Add(row1);
            data.Add(row2);
            data.Add(row3);

            //Add list to IEnumerable.
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Create an instance of PdfGridRowStyle.
            PdfGridRowStyle pdfGridRowStyle = new PdfGridRowStyle();
            pdfGridRowStyle.BackgroundBrush = PdfBrushes.LightYellow;
            pdfGridRowStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
            pdfGridRowStyle.TextBrush = PdfBrushes.Blue;
            pdfGridRowStyle.TextPen = PdfPens.Pink;
            //Set the height.
            pdfGrid.Rows[2].Height = 50;
            //Set style for the PdfGridRow.
            pdfGrid.Rows[0].Style = pdfGridRowStyle;
            //Draw the PdfGrid.
            PdfGridLayoutResult result = pdfGrid.Draw(pdfPage, PointF.Empty);

            //Creating the stream object.
            MemoryStream stream = new MemoryStream();
            //Save the PDF document to stream.
            pdfDocument.Save(stream);
            pdfDocument.Save("PdfGridRowCustom.pdf");

            pdfDocument.Close(true);


            //This will open the PDF file so, the result will be seen in default PDF viewer
            System.Diagnostics.Process.Start("PdfGridRowCustom.pdf");
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Create a new PDF document.
            PdfDocument pdfDocument = new PdfDocument();
            //Add a page. 
            PdfPage pdfPage = pdfDocument.Pages.Add();

            //Create a new PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Add values to the list.
            List<object> data = new List<object>();
            Object row1 = new { ID = "E01", Name = "Clay" };
            Object row2 = new { ID = "E02", Name = "Thomas" };
            Object row3 = new { ID = "E02", Name = "Peter" };
            data.Add(row1);
            data.Add(row2);
            data.Add(row3);
            //Add list to IEnumerable.
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Set the column width.
            pdfGrid.Columns[1].Width = 50;

            //Create and customize the string formats.
            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;
            format.LineAlignment = PdfVerticalAlignment.Bottom;
            //Set the column text format.
            pdfGrid.Columns[0].Format = format;
            //Draw the PdfGrid.
            PdfGridLayoutResult result = pdfGrid.Draw(pdfPage, PointF.Empty);

            //Creating the stream object.
            MemoryStream stream = new MemoryStream();
            //Save the PDF document to stream.
            pdfDocument.Save(stream);
            pdfDocument.Save("PdfGridColCustom.pdf");

            pdfDocument.Close(true);


            //This will open the PDF file so, the result will be seen in default PDF viewer
            System.Diagnostics.Process.Start("PdfGridColCustom.pdf");
        }

        private void button8_Click(object sender, EventArgs e)
        {

            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            //Add a page.
            PdfPage page = document.Pages.Add();

            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Add values to the list.
            List<object> data = new List<object>();
            Object row1 = new { ID = "E01", Name = "Clay" };
            Object row2 = new { ID = "E02", Name = "Thomas" };
            data.Add(row1);
            data.Add(row2);
            //Add list to IEnumerable.
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;

            //Declare and define the grid style.
            PdfGridStyle gridStyle = new PdfGridStyle();
            //Set cell padding, which specifies the space between the border and content of the cell.
            gridStyle.CellPadding = new PdfPaddings(2, 2, 2, 2);
            
            //Set cell spacing, which specifies the space between the adjacent cells.
            gridStyle.CellSpacing = 2;
            //Enable to adjust PDF table row width based on the text length.
            gridStyle.AllowHorizontalOverflow = true;
            //Apply style.
            pdfGrid.Style = gridStyle;
            //Draw the grid to the page of a PDF document.
            pdfGrid.Draw(page, new PointF(10, 10));

            //Creating the stream object.
            MemoryStream stream = new MemoryStream();
            //Save the document as a stream.
            document.Save(stream);
            document.Save("PdfGridTableCustom.pdf");

            document.Close(true);


            //This will open the PDF file so, the result will be seen in default PDF viewer
            System.Diagnostics.Process.Start("PdfGridTableCustom.pdf");
        }

        private void cmdCellPadding_Click(object sender, EventArgs e)
        {
            //Create a new PDF document.
            PdfDocument doc = new PdfDocument();
            //Add a page.
            PdfPage page = doc.Pages.Add();
            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Create a DataTable.
            DataTable dataTable = new DataTable();
            //Add columns to the DataTable
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name");
            //Add rows to the DataTable.
            dataTable.Rows.Add(new object[] { "E01", "Clay" });
            dataTable.Rows.Add(new object[] { "E02", "Thomas" });
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Using the Column collection
            pdfGrid.Columns[0].Width = 100;
            //Adding grid style
            PdfGridStyle gridStyle = new PdfGridStyle();
            //Adding cell padding
            gridStyle.CellPadding = new PdfPaddings(5, 5, 5, 5);
            //Applying style to grid
            pdfGrid.Style = gridStyle;
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(10, 10));
            //Save the document.
            doc.Save("CellPadding.pdf");
            //close the document
            doc.Close(true);
            System.Diagnostics.Process.Start("CellPadding.pdf");
        }

        private void cmdMoreTable_Click(object sender, EventArgs e)
        {
            //           //Create a new PDF document
            PdfDocument document = new PdfDocument();

            //Add a page
            PdfPage page = document.Pages.Add();

            //Create a PdfGrid
            PdfGrid pdfGrid1 = new PdfGrid();

            //Create a DataTable
            DataTable dataTable1 = new DataTable();

            //Add columns to the DataTable
            dataTable1.Columns.Add("OrderID");
            dataTable1.Columns.Add("CustomerID");
            dataTable1.Columns.Add("ShipName");
            dataTable1.Columns.Add("ShipAddress");
            dataTable1.Columns.Add("ShipCity");
            dataTable1.Columns.Add("ShipPostalCode");
            dataTable1.Columns.Add("ShipCountry");

            for (int i = 0; i < 10; i++)
            {
                //Add rows to the DataTable
                dataTable1.Rows.Add(new object[] { "10248", "VINET", "Vins et alcools Chevalier", "59 rue de l'Abbaye", "Reims", "51100", "France" });
                dataTable1.Rows.Add(new object[] { "10249", "TOMSP", "Toms Spezialitäten", "Luisenstr. 48", "Münster", "44087", "Germany" });
                dataTable1.Rows.Add(new object[] { "10250", "HANAR", "Hanari Carnes", "Rua do Paço, 67", "Rio de Janeiro", "05454-876", "Brazil" });
                dataTable1.Rows.Add(new object[] { "10251", "VICTE", "Victuailles en stock", "2, rue du Commerce", "Lyon", "69004", "France" });
                dataTable1.Rows.Add(new object[] { "10252", "SUPRD", "Suprêmes délices", "Boulevard Tirou, 255", "Charleroi", "B-6000", "Belgium" });
                dataTable1.Rows.Add(new object[] { "10253", "HANAR", "Hanari Carnes", "Rua do Paço, 67", "Rio de Janeiro", "05454-876", "Brazil" });
            }

            //Assign data source
            pdfGrid1.DataSource = dataTable1;

            //Add layout format for grid pagination
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;

            //Apply built-in table style
            pdfGrid1.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

            //Draw grid to the page of PDF document
            PdfLayoutResult result = pdfGrid1.Draw(page, new PointF(10, 10), layoutFormat);

            //Create a PdfGrid
            PdfGrid pdfGrid2 = new PdfGrid();

            //Create a DataTable
            DataTable dataTable2 = new DataTable();

            //Add columns to the DataTable
            dataTable2.Columns.Add("CustomerID");
            dataTable2.Columns.Add("CompanyName");
            dataTable2.Columns.Add("ContactName");
            dataTable2.Columns.Add("Address");
            dataTable2.Columns.Add("City");
            dataTable2.Columns.Add("PostalCode");
            dataTable2.Columns.Add("Country");
            dataTable2.Columns.Add("Phone");
            dataTable2.Columns.Add("Fax");
            for (int n = 0; n < 30; n++)
            {
                //Add rows to the DataTable
                dataTable2.Rows.Add(new object[] { "ALFKI", "Alfreds Futterkiste", "Maria Anders", "Obere Str. 57", "Berlin", "12209", "Germany", "030-0074321", "030-0076545" });
                dataTable2.Rows.Add(new object[] { "ANATR", "Ana Trujillo Emparedados yhelados", "Ana Trujillo", "Avda. de la Constitución 2222", "México D.F.", "05021", "Mexico", "(5) 555-4729", "(5) 555-3745" });
                dataTable2.Rows.Add(new object[] { "ANTON", "Antonio Moreno Taquería", "Antonio Moreno", "Mataderos 2312", "México D.F.", "05023", "Mexico", "(5) 555-3932", "" });
                dataTable2.Rows.Add(new object[] { "BLAUS", "Blauer See Delikatessen", "Hanna Moos", "Forsterstr. 57", "Mannheim", "68306", "Germany", "0621-08460", "0621-08924" });
                dataTable2.Rows.Add(new object[] { "DRACD", "Drachenblut Delikatessen", "Sven Ottlieb", "Walserweg 21", "Aachen", "52066", "Germany", "0241-039123", "0241-059428" });
            }
            //Assign data source
            pdfGrid2.DataSource = dataTable2;

            //Apply built-in table style
            pdfGrid2.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable6Colorful);

            //Draw grid to the resultant page of the first grid
            result = pdfGrid2.Draw(result.Page, new PointF(10, result.Bounds.Height + 30));

            //Save the PDF document
            document.Save("SequentTables.pdf");

            //Close the PDF document 
            document.Close(true);

            //This will open the PDF file and the result will be seen in the default PDF Viewer
            Process.Start("SequentTables.pdf");
        }

        private void cmdJudulKompex_Click(object sender, EventArgs e)
        {
            try
            {
                //           //Create a new PDF document
                PdfDocument document = new PdfDocument();

                //Add a page
                PdfPage page = document.Pages.Add();

                //Create a PdfGrid
                PdfGrid pdfGridHeader = new PdfGrid();

                //Create a DataTable
                DataTable dataHeader = new DataTable();

                //Add columns to the DataTable
                dataHeader.Columns.Add("OrderID");
                dataHeader.Columns.Add("CustomerID");
                dataHeader.Columns.Add("ShipName");
                dataHeader.Columns.Add("ShipAddress");
                dataHeader.Columns.Add("ShipCity");
                dataHeader.Columns.Add("ShipPostalCode");
                dataHeader.Columns.Add("ShipCountry");


                //Add rows to the DataTable
                dataHeader.Rows.Add(new object[] { "10248", "10248", "Alamat", "Alamat", "Reims", "51100", "France" });
                dataHeader.Rows.Add(new object[] { "10249", "TOMSP", "Toms Spezialitäten", "Luisenstr. 48", "Münster", "44087", "Germany" });
                pdfGridHeader.DataSource = dataHeader;
                pdfGridHeader.Headers.Clear();
                pdfGridHeader.Rows[0].Cells[0].ColumnSpan = 2;
                pdfGridHeader.Rows[0].Cells[0].StringFormat.Alignment = PdfTextAlignment.Center;
                PdfLayoutResult resultHeader = pdfGridHeader.Draw(page, new PointF(10, 10));

                PdfGrid pdfGrid1 = new PdfGrid();

                //Create a DataTable
                DataTable dataTable1 = new DataTable();

                //Add columns to the DataTable
                dataTable1.Columns.Add("OrderID");
                dataTable1.Columns.Add("CustomerID");
                dataTable1.Columns.Add("ShipName");
                dataTable1.Columns.Add("ShipAddress");
                dataTable1.Columns.Add("ShipCity");
                dataTable1.Columns.Add("ShipPostalCode");
                dataTable1.Columns.Add("ShipCountry");
                for (int i = 0; i < 100; i++)
                {
                    //Add rows to the DataTable
                    dataTable1.Rows.Add(new object[] { "10248", "VINET", "Vins et alcools Chevalier", "59 rue de l'Abbaye", "Reims", "51100", "France" });
                    dataTable1.Rows.Add(new object[] { "10249", "TOMSP", "Toms Spezialitäten", "Luisenstr. 48", "Münster", "44087", "Germany" });
                    dataTable1.Rows.Add(new object[] { "10250", "HANAR", "Hanari Carnes", "Rua do Paço, 67", "Rio de Janeiro", "05454-876", "Brazil" });
                    dataTable1.Rows.Add(new object[] { "10251", "VICTE", "Victuailles en stock", "2, rue du Commerce", "Lyon", "69004", "France" });
                    dataTable1.Rows.Add(new object[] { "10252", "SUPRD", "Suprêmes délices", "Boulevard Tirou, 255", "Charleroi", "B-6000", "Belgium" });
                    dataTable1.Rows.Add(new object[] { "10253", "HANAR", "Hanari Carnes", "Rua do Paço, 67", "Rio de Janeiro", "05454-876", "Brazil" });
                }

            
                pdfGrid1.DataSource = dataTable1;

                //Add layout format for grid pagination
                PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
                layoutFormat.Layout = PdfLayoutType.Paginate;

                //Apply built-in table style
                pdfGrid1.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

                //Draw grid to the page of PDF document
                PdfLayoutResult result = pdfGrid1.Draw(resultHeader.Page, new PointF(10, resultHeader.Bounds.Bottom), layoutFormat);

                //Create a PdfGrid
                PdfGrid pdfGrid2 = new PdfGrid();

                //Create a DataTable
                DataTable dataTable2 = new DataTable();

                //Add columns to the DataTable
                dataTable2.Columns.Add("CustomerID");
                dataTable2.Columns.Add("CompanyName");
                dataTable2.Columns.Add("ContactName");
                dataTable2.Columns.Add("Address");
                dataTable2.Columns.Add("City");
                dataTable2.Columns.Add("PostalCode");
                dataTable2.Columns.Add("Country");
                dataTable2.Columns.Add("Phone");
                dataTable2.Columns.Add("Fax");
                for (int i = 0; i < 10; i++)
                {
                    //Add rows to the DataTable
                    dataTable2.Rows.Add(new object[] { "10248", "VINET", "Vins et alcools Chevalier", "59 rue de l'Abbaye", "Reims", "51100", "France" });
                    dataTable2.Rows.Add(new object[] { "10249", "TOMSP", "Toms Spezialitäten", "Luisenstr. 48", "Münster", "44087", "Germany" });
                    dataTable2.Rows.Add(new object[] { "10250", "HANAR", "Hanari Carnes", "Rua do Paço, 67", "Rio de Janeiro", "05454-876", "Brazil" });
                    dataTable2.Rows.Add(new object[] { "10251", "VICTE", "Victuailles en stock", "2, rue du Commerce", "Lyon", "69004", "France" });
                    dataTable2.Rows.Add(new object[] { "10252", "SUPRD", "Suprêmes délices", "Boulevard Tirou, 255", "Charleroi", "B-6000", "Belgium" });
                    dataTable2.Rows.Add(new object[] { "10253", "HANAR", "Hanari Carnes", "Rua do Paço, 67", "Rio de Janeiro", "05454-876", "Brazil" });
                }
                //Assign data source
                pdfGrid2.DataSource = dataTable2;

                //Apply built-in table style
                pdfGrid2.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable6Colorful);

                //Draw grid to the resultant page of the first grid
                PdfLayoutResult result2;
                result2 = pdfGrid2.Draw(result.Page, new PointF(10, result.Bounds.Bottom));

                //Save the PDF document
                document.Save("SequentTables.pdf");

                //Close the PDF document 
                document.Close(true);

                //This will open the PDF file and the result will be seen in the default PDF Viewer
                Process.Start("SequentTables.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdBorder_Click(object sender, EventArgs e)
        {
            //Create a new PDF document.
            PdfDocument doc = new PdfDocument();
            //Add a page.
            PdfPage page = doc.Pages.Add();
            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Create a DataTable.
            DataTable dataTable = new DataTable();
            //Add columns to the DataTable
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name");
            //Add rows to the DataTable.
            dataTable.Rows.Add(new object[] { "E01", "Clay" });
            dataTable.Rows.Add(new object[] { "E02", "Thomas" });
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Using the Column collection
            pdfGrid.Columns[0].Width = 100;
            //Adding grid cell style
            PdfGridCellStyle rowStyle = new PdfGridCellStyle();
            //Creating Border
            PdfBorders border = new PdfBorders();
            border.Bottom.DashStyle = PdfDashStyle.Dot ;
            border.All = PdfPens.Blue;
            //setting border to the style
            rowStyle.Borders = border;
            //Applying style to grid
            for (int row = 0; row < pdfGrid.Rows.Count;row++ )
                pdfGrid.Rows[row].Cells[0].Style = rowStyle;
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(10, 10));
            //Save the document.
            doc.Save("Border.pdf");
            //close the document
            doc.Close(true);
            Process.Start("Border.pdf");
        }

        private void cmdBorder2_Click(object sender, EventArgs e)
        {
            //Create a new PDF document
            PdfDocument doc = new PdfDocument();

            //Add a page
            PdfPage page = doc.Pages.Add();

            // Create a PdfGrid
            PdfGrid pdfGrid = new PdfGrid();

            // Initialize DataTable to assign as DateSource to the pdf grid
            DataTable table = new DataTable();

            //Include columns to the DataTable
            table.Columns.Add("Name");
            table.Columns.Add("Age");
            table.Columns.Add("Gender");

            //Include rows to the DataTable
            table.Rows.Add(new string[] { "abc", "21", "Male" });
            table.Rows.Add(new string[] { "def", "23", "Female" });

            //Assign data source
            pdfGrid.DataSource = table;

            //Set the border thickness and dashstyle to cells
            for (int c =0; c <pdfGrid.Columns.Count;c++){
               pdfGrid.Rows[0].Cells[c].Style.Borders.Bottom.Width = 0.1F;
               pdfGrid.Rows[0].Cells[c].Style.Borders.Top.Width = 0.1F;
               pdfGrid.Rows[0].Cells[c].Style.Borders.Left.Width = 0.1F;


            }

            pdfGrid.Rows[1].Cells[0].Style.Borders.Bottom.Width = 3;

            pdfGrid.Rows[1].Cells[1].Style.Borders.Bottom.Width = 0.5F;
            pdfGrid.Rows[1].Cells[2].Style.Borders.Bottom.Width = 4;

            pdfGrid.Rows[1].Cells[2].Style.Borders.Bottom.DashStyle = PdfDashStyle.Dot;


            //Draw pdfGrid
            pdfGrid.Draw(page, new PointF(0, 0));

            //Save the document
            doc.Save("Output.pdf");

            //Close the document
            doc.Close(true);

            Process.Start("Output.pdf");
        }

        private void cmdTest_Click(object sender, EventArgs e)
        {

            //Create new PDF document
            PdfDocument document = new PdfDocument();
            //Add a page to the document 
            PdfPage page = document.Pages.Add();
            //Create a PdfGrid
            PdfGrid pdfGrid = new PdfGrid();
            for (int i = 0; i <= 3; i++)
            {
                count = 0;
                //Create a DataTable
                DataTable table = new DataTable();
                //Add columns to table
                table.Columns.Add("Line No");
                table.Columns.Add("Item Name");
                table.Columns.Add("Item Group");
                table.Columns.Add("Description");
                table.Columns.Add("Qty");
                table.Columns.Add("Price");
                table.Columns.Add("Line Total");
                table.Columns.Add("Doc Entry");
                //Assign Column count
                columnCount = table.Columns.Count;
                for (int j = 0; j < 40; j++)
                {
                    //Add rows to table
                    table.Rows.Add(new string[] { j.ToString(), "Coil", "Row material", "coil", "12", "100", "1200", i.ToString() });
                }
                //Assign data source
                pdfGrid.DataSource = table;
                pdfGrid.RepeatHeader = true;
                pdfGrid.BeginCellLayout += PdfGrid_BeginCellLayout;
                //Create a second PdfGrid
                PdfGrid grid = new PdfGrid();
                //Create a data table for second PdfGrid
                DataTable grid_dataTable = new DataTable();
                //Add columns to second table
                grid_dataTable.Columns.Add("Header");
                //Add rows
                grid_dataTable.Rows.Add(new string[] { "Group " + (i + 1).ToString() });
                //Assign data table
                grid.DataSource = grid_dataTable;
                grid.BeginCellLayout += Grid_BeginCellLayout;
                //Set style
                grid.Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 8, PdfFontStyle.Bold);
                //Draw group information
                if (layoutResult == null)
                    layoutResult = grid.Draw(page, new PointF(0, 0));
                else
                    layoutResult = grid.Draw(layoutResult.Page, new PointF(0, layoutResult.Bounds.Bottom - grid.Rows[0].Height));
                PointF point = new PointF(0, layoutResult.Bounds.Bottom);
                // Value(50 - used defined) indicate whether the header should draw into page or not
                if (layoutResult.Bounds.Bottom > 50)
                {
                    point = new PointF(0, layoutResult.Bounds.Bottom - layoutResult.Bounds.Height);
                }
                //Draw the table 
                if (layoutResult == null)
                    layoutResult = pdfGrid.Draw(page, new PointF(0, 0));
                // else
                //  layoutResult = pdfGrid.Draw(layoutResult.Page, point);
            }
            //Save the document 
            document.Save("Table.pdf");
            //Close the document
            document.Close(true);
            //This will open the PDF file so, the result will be seen in default PDF Viewer
            pdfViewer pV = new pdfViewer();
            pV.Document = Path.GetFullPath("Table.pdf");
            pV.Show();
        }


        private void Grid_BeginCellLayout(object sender, PdfGridBeginCellLayoutEventArgs args)
        {
            count++;
            PdfGrid grid = (sender as PdfGrid);
            if (count <= grid.Headers.Count * grid.Columns.Count)
            {
                args.Skip = true;
            }
        }
        private void PdfGrid_BeginCellLayout(object sender, PdfGridBeginCellLayoutEventArgs args)
        {
            if (args.RowIndex == 0 && args.CellIndex <= columnCount && layoutResult != null)
            {
                if (layoutResult.Bounds.Y > args.Bounds.Height)
                {
                    args.Skip = true;
                }
                else
                {
                    args.Skip = false;
                }
            }
        }

        
    }
}
