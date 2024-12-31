using SuperZTP.Model;
using SuperZTP.Composite;
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

        public FileHandler(List<Model.Task> tasks, List<Note> notes)
        {
            this.tasks = tasks;
            this.notes = notes;
        }

        // Zapis do pliku zadań i notatek
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

        // Wczytywanie z pliku zadań i notatek
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

        // Zapis i odczyt kategorii z pliku
        public void SaveCategoriesToFile(string filePath, Category rootCategory)
        {
            using (var writer = new StreamWriter(filePath))
            {
                WriteCategory(writer, rootCategory, 0);
            }
        }

        private void WriteCategory(StreamWriter writer, ICategory category, int indentLevel)
        {
            writer.WriteLine(new string('\t', indentLevel) + category.Name);
            if (category is Category cat)
            {
                foreach (var subCategory in cat.categories)
                {
                    WriteCategory(writer, subCategory, indentLevel + 1);
                }
            }
        }

        public Category LoadCategoriesFromFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var rootCategory = new Category("Root");
            var stack = new Stack<(Category, int)>();
            stack.Push((rootCategory, -1));

            foreach (var line in lines)
            {
                var indentLevel = line.TakeWhile(char.IsWhiteSpace).Count();
                var name = line.Trim();

                var newCategory = new Category(name);
                while (stack.Peek().Item2 >= indentLevel)
                {
                    stack.Pop();
                }
                stack.Peek().Item1.AddCategory(newCategory);
                stack.Push((newCategory, indentLevel));
            }
            return rootCategory;
        }

        // Zapis i odczyt tagów z pliku
        public void SaveTagsToFile(string filePath, Tag rootTag)
        {
            using (var writer = new StreamWriter(filePath))
            {
                WriteTag(writer, rootTag, 0);
            }
        }

        private void WriteTag(StreamWriter writer, ITag tag, int indentLevel)
        {
            writer.WriteLine(new string('\t', indentLevel) + tag.Name);
            if (tag is Tag t)
            {
                foreach (var subTag in t.tags)
                {
                    WriteTag(writer, subTag, indentLevel + 1);
                }
            }
        }

        public Tag LoadTagsFromFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var rootTag = new Tag("Root");
            var stack = new Stack<(Tag, int)>();
            stack.Push((rootTag, -1));

            foreach (var line in lines)
            {
                var indentLevel = line.TakeWhile(char.IsWhiteSpace).Count();
                var name = line.Trim();

                var newTag = new Tag(name);
                while (stack.Peek().Item2 >= indentLevel)
                {
                    stack.Pop();
                }
                stack.Peek().Item1.AddTag(newTag);
                stack.Push((newTag, indentLevel));
            }
            return rootTag;
        }
    }
}
