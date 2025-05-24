using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using SuperZTP.Resources;

namespace SuperZTP.TemplateMethod
{
    public class GenerateDOCX : Generate // Zapis do pliku w formacie .docx
    {
        public GenerateDOCX(List<Model.Task> tasks) : base(tasks) { }

        protected override void Save(string filepath, string content)
        {
            string directoryPath = GetDirectory();
            string fullFilePath = Path.Combine(directoryPath, filepath);

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(fullFilePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = new Body();

                var sections = content.Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var section in sections)
                {
                    var lines = section.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                    if (lines.Length == 0) continue;

                    string header = lines[0];

                    // Nagłówek
                    Paragraph headerParagraph = new Paragraph(
                        new Run(
                            new RunProperties(
                                new Bold(),
                                new Color { Val = "00008B" }, // dark blue
                                new FontSize { Val = "28" } // 14 pt
                            ),
                            new Text(header)
                        )
                    );
                    docBody.Append(headerParagraph);

                    // Lista zadań
                    for (int i = 1; i < lines.Length; i++)
                    {
                        var parts = lines[i].Split(" - ");
                        string lineText;

                        if (parts.Length == 3)
                        {
                            string title = parts[0].Trim();
                            string deadline = parts[1].Replace(Strings.Deadline, "").Trim();
                            string status = parts[2].Trim();

                            lineText = $"{title,-35}\t{deadline,-12}\t{status}";
                        }
                        else if (parts.Length == 2)
                        {
                            string title = parts[0].Trim();
                            string deadline = parts[1].Replace(Strings.Deadline, "").Trim();

                            lineText = $"{title,-35}\t{deadline}";
                        }
                        else
                        {
                            lineText = lines[i].Trim(); // fallback
                        }

                        Paragraph taskParagraph = new Paragraph(
                            new Run(new Text(lineText))
                        )
                        {
                            ParagraphProperties = new ParagraphProperties(
                                new Tabs(
                                    new TabStop() { Val = TabStopValues.Left, Position = 5000 },
                                    new TabStop() { Val = TabStopValues.Left, Position = 9000 }
                                )
                            )
                        };

                        docBody.Append(taskParagraph);
                    }

                    // Odstęp między sekcjami
                    docBody.Append(new Paragraph(new Run(new Text(""))));
                }

                mainPart.Document.Append(docBody);
                mainPart.Document.Save();
            }
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
