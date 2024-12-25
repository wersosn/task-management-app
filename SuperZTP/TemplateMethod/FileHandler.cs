using SuperZTP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperZTP.TemplateMethod
{
    public class FileHandler
    {
        private List<SuperZTP.Model.Task> tasks;
        private List<Note> notes;

        public FileHandler(List<Model.Task> tasks, List<Note> notes)
        {
            this.tasks = tasks;
            this.notes = notes;
        }

        public void SaveTasksToFile(string filePath)
        {
            var lines = tasks.Select(task => task.ToCsv()).ToList();
            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }

        public void SaveNotesToFile(string filePath)
        {
            var lines = notes.Select(note => note.ToCsv()).ToList();
            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }
    }
}
