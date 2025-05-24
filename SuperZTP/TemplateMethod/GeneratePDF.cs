using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using SuperZTP.Resources;

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
            var fontHeader = new XFont("Verdana", 14);
            var fontRow = new XFont("Verdana", 10);

            double margin = 40;
            double y = margin;
            double lineHeight = fontRow.GetHeight() + 8;
            double col1Width = 300;
            double col2Width = 100;
            double col3Width = 100;

            void DrawHeader(string headerText)
            {
                gfx.DrawString(headerText, fontHeader, XBrushes.DarkBlue, new XRect(margin, y, page.Width, lineHeight), XStringFormats.TopLeft);
                y += lineHeight;
                gfx.DrawLine(XPens.DarkBlue, margin, y, page.Width - margin, y);
                y += 6;
            }

            void DrawRow(string title, string deadline, string status)
            {
                if (y + lineHeight > page.Height - margin)
                {
                    page = doc.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    y = margin;
                }

                gfx.DrawString(title, fontRow, XBrushes.Black, new XRect(margin, y, col1Width, lineHeight), XStringFormats.TopLeft);
                gfx.DrawString(deadline, fontRow, XBrushes.Black, new XRect(margin + col1Width, y, col2Width, lineHeight), XStringFormats.TopLeft);
                gfx.DrawString(status, fontRow, XBrushes.Black, new XRect(margin + col1Width + col2Width, y, col3Width, lineHeight), XStringFormats.TopLeft);
                y += lineHeight;
            }

            var sections = content.Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var section in sections)
            {
                var lines = section.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None)
                                   .Select(l => l.TrimEnd('\r', '\n', '\uFFFD', '\u0000'))
                                   .ToArray();

                if (lines.Length == 0) continue;
                DrawHeader(lines[0]);
                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(" - ");
                    if (parts.Length >= 3)
                    {
                        DrawRow(parts[0], parts[1].Replace(Strings.Deadline, "").Trim(), parts[2]);
                    }
                    else if (parts.Length == 2)
                    {
                        DrawRow(parts[0], parts[1].Replace(Strings.Deadline, "").Trim(), "");
                    }
                }
                y += 10;
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
