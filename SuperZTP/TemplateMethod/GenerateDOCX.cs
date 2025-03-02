using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

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

                var lines = content.Split('\n');
                foreach (var line in lines)
                {
                    Paragraph para = new Paragraph(new Run(new Text(line)));
                    docBody.Append(para);
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
