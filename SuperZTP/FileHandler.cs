using SuperZTP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace SuperZTP
{
    public class FileHandler
    {
        private List<Model.Task> tasks;
        private List<Note> notes;
        private List<Category> categories;
        private List<Tag> tags;

        public FileHandler(List<Model.Task> tasks, List<Note> notes, List<Category> categories, List<Tag> tags)
        {
            this.tasks = tasks;
            this.notes = notes;
            this.categories = categories;
            this.tags = tags;
        }

        // Zapis do pliku zadań, notatek, kategorii i tagów
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

        public void SaveCategoriesToFile(string filePath)
        {
            var lines = categories.Select(category => category.ToFile()).ToList();
            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }

        public void SaveTagsToFile(string filePath)
        {
            var lines = tags.Select(tag => tag.ToFile()).ToList();
            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }

        // Wczytywanie z pliku zadań, notatek, kategorii i tagów
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

        public void LoadCategoriesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            var lines = File.ReadAllLines(filePath, Encoding.UTF8);
            categories.Clear();
            foreach (var line in lines)
            {
                var category = Category.FromFile(line);
                categories.Add(category);
            }
        }

        public void LoadTagsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            var lines = File.ReadAllLines(filePath, Encoding.UTF8);
            tags.Clear();
            foreach (var line in lines)
            {
                var tag = Tag.FromFile(line);
                tags.Add(tag);
            }
        }
    }
}
