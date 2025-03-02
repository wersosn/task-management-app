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
    public class GeneratePDF : Generate // Zapis do pliku w formacie .pdf
    {
        public GeneratePDF(List<Model.Task> tasks) : base(tasks) { }

        protected override void Save(string filepath, string content)
        {
            string directoryPath = GetDirectory();
            string fullFilePath = Path.Combine(directoryPath, filepath);

            PdfDocument doc = new PdfDocument();
            doc.Info.Title = "GeneratePDF";

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
            doc.Save(fullFilePath);
        }

        private string GetDirectory()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folder = Path.Combine(documentsPath, "Task-management-app");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return folder;
        }
    }
}
