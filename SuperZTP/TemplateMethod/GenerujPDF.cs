using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PdfSharp.Pdf;
using PdfSharp.Drawing;


namespace SuperZTP.TemplateMethod
{
    public class GenerujPDF : Generuj
    {
        public GenerujPDF(List<Model.Task> tasks) : base(tasks) { }

        protected override void Zapisz(string filepath, string content)
        {
            PdfDocument doc = new PdfDocument();
            doc.Info.Title = "GenerujPDF";

            PdfPage page = doc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 12);

            var lines = content.Split('\n');
            double lineHeight = font.GetHeight() + 4;
            double yPosition = 40;

            foreach (var line in lines)
            {
                gfx.DrawString(line, font, XBrushes.Black, new XPoint(40, yPosition));
                yPosition += lineHeight;
                if (yPosition > page.Height - 40)
                {
                    page = doc.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPosition = 40;
                }
            }
            doc.Save(filepath);
        }
    }
}
