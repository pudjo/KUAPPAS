using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using KUAPPAS.SP2DOnline;

using System.Drawing;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using NPOI.SS.Formula.Functions;

namespace KUAPPAS
{
    public class CetakPDF
    {
        public float TulisItem(PdfGraphics graphics, 
            string text=null, 
            float sizeFornt=10, 
            float x=10, 
            float y=10, 
            float widthSpace=10, 
            PdfStringFormat stringFormat=null, 
            bool changeLine = false, 
            bool underline = false, 
            bool bold = false,
            bool textHigh = false )
        {
            float yPos = y;

            PdfFont lfont;

            FontStyle style;
            style = FontStyle.Regular;
            if (bold == true)
                style = style | FontStyle.Bold;
            if (underline == true)
                style = style | FontStyle.Underline;

            if (text == null)
            {
                text = "";

            }
            text = text.Replace("\t", "").Replace("\r\n", "");
            lfont = new PdfTrueTypeFont(new Font("Arial", sizeFornt, style));

            SizeF size = lfont.MeasureString(text);
            float height = size.Height;
            int pengali = CariPengali(size.Width, widthSpace);
            if (text.Length == 0)
            {

                size = lfont.MeasureString("Test");
                height = size.Height;
                pengali = 1;
            }
            if (size.Width > widthSpace)
            {

                height = size.Height * pengali;
            }

            //stringFormat.Alignment=PdfAlignmentStyle.
            RectangleF rect = new RectangleF(x, y, widthSpace, height);
            if (text == null)
            {
                text = " ";
            }
            if (textHigh)
            {
                graphics.DrawString(text, lfont, PdfBrushes.White, rect, stringFormat);
            } else 
            graphics.DrawString(text, lfont, PdfBrushes.Black, rect, stringFormat);

            if (changeLine)
            {

                yPos = y + (pengali * size.Height) + 3;

            }

            return yPos;
        }

        private int CariPengali(float x, float y)
        {

            if (x > 15 * y)
                return 20;
            else
            {
                if (x > 14 * y)
                    return 19;
                else
                {
                    if (x > 13 * y)
                        return 17;
                    else
                    {
                        if (x > 12 * y)
                            return 15;
                        else
                        {
                            if (x > 11 * y)
                                return 13;
                            else
                            {
                                if (x > 10 * y)
                                    return 20;
                                else
                                {
                                    if (x > 9 * y)
                                        return 12;
                                    else
                                    {

                                        if (x > 8 * y)
                                            return 10;
                                        else
                                        {

                                            if (x > 7 * y)
                                                return 9;
                                            else
                                            {

                                                if (x > 5 * y)
                                                    return 8;
                                                else
                                                {
                                                    if (x > (4 * y))
                                                        return 7;
                                                    else
                                                    {
                                                        if (x > (3 * y))
                                                            return 6;
                                                        else
                                                        {
                                                            if (x > (2 * y))
                                                                return 3;
                                                            else
                                                            {
                                                                if (x > (1 * y))
                                                                    return 2;
                                                                else
                                                                    return 1;
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
