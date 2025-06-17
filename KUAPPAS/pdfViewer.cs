using Syncfusion.Windows.PdfViewer;
using System;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;

namespace KUAPPAS
{
    public partial class pdfViewer : Form
    {
        public pdfViewer()
        {
            InitializeComponent();
        }

        private void pdfViewerControl1_Click(object sender, EventArgs e)
        {

        }
        public void HapusBlank(string namaFile)

        {
            ////Load PDF document 
            ////PdfLoadedDocument lDoc = new PdfLoadedDocument(namaFile);

            ////Disable the incremental update
            //document.FileStructure.IncrementalUpdate = false;

            //////Set the cross reference type 
            //document.FileStructure.CrossReferenceType = PdfCrossReferenceType.CrossReferenceStream;

            //int i = 0;

            //while (i < document.Pages.Count)
            //{
            //    //Extract image from loaded page 
            //    Image[] img = document.Pages[i].ExtractImages();

            //    //Extract text from existing page
            //    string text = document.Pages[i].ExtractText();

            //    //If extract images and string is empty, then remove the blank page 
            //    if (img.Length == 0 && string.IsNullOrEmpty(text))
            //        document.Pages.RemoveAt(i);

            //    i += 1;
            //}
            //Load an existing PDF document into PdfLoadedDocument
            PdfLoadedDocument document = new PdfLoadedDocument(namaFile);

            //Remove the second page
            int i = 0;

            while (i < document.Pages.Count)
            {
                //Extract image from loaded page 
                Image[] img = document.Pages[i].ExtractImages();

                //Extract text from existing page
                string text = document.Pages[i].ExtractText();

                //If extract images and string is empty, then remove the blank page 
                if (img.Length == 0 && string.IsNullOrEmpty(text))
                    document.Pages.RemoveAt(i);

                i += 1;
            }

            //Save the PDF document
            document.Save(namaFile);

            //Close the instance of PdfLoadedDocument
            document.Close(true);
        }
        public string Document
        {
            set
            {
                 HapusBlank(value);
                pdfViewerControl1.Load(value);
                
                pdfViewerControl1.Focus();
            }
        }

        private void pdfViewer_Load(object sender, EventArgs e)
        {
            pdfViewerControl1.PrinterSettings.PageSize = PdfViewerPrintSize.ActualSize;
        }
    }
}
