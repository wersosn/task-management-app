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

        // Zapis do pliku
        public void SaveTasksToFile(string filePath)
        {
            var lines = tasks.Select(task => task.ToFile()).ToList();
            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }

        public void SaveNotesToFile(string filePath)
        {
            var lines = notes.Select(note => note.ToFile()).ToList();
            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }

        // Wczytywanie z pliku
        public void LoadTasksFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            var lines = File.ReadAllLines(filePath, Encoding.UTF8);
            tasks.Clear();
            foreach (var line in lines)
            {
                var task = Model.Task.FromFile(line);
                tasks.Add(task);
            }
        }

        public void LoadNotesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            var lines = File.ReadAllLines(filePath, Encoding.UTF8);
            notes.Clear();
            foreach (var line in lines)
            {
                var note = Note.FromFile(line);
                notes.Add(note);
            }
        }
    }
}
