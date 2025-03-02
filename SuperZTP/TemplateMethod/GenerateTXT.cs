using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SuperZTP.TemplateMethod
{
    public class GenerateTXT : Generate // Zapis do pliku w formacie .txt
    {
        public GenerateTXT(List<Model.Task> tasks) : base(tasks) { }

        protected override void Save(string filepath, string content)
        {
            string directoryPath = GetDirectory();
            string fullFilePath = Path.Combine(directoryPath, filepath);
            File.WriteAllText(fullFilePath, content);
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
