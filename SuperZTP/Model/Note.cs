using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Model
{
    // Model Notatki
    public class Note
	{
        // Atrybuty:
        public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
        public Tag Tag { get; set; }
        public Category Category { get; set; }

        public Note() { }
        public Note(int id, string title, string description, Tag tag, Category category)
		{
            Id = id;
            Title = title;
			Description = description;
			Tag = tag;
			Category = category;
		}

        public override string ToString()
        {
            return $"Notatka: {Title}\nOpis: {Description}\nTag: {Tag?.Name}\nKategoria: {Category?.Name}";
        }

        // Zapisywanie do pliku (możliwe, że będzie zmienione):
        public string ToFile()
        {
            return $"{Id};{Title};{Description};{Tag?.Name};{Category?.Name}";
        }

        // Wczytywanie z pliku:
        public static Note FromFile(string line)
        {
            var values = line.Split(';');
            return new Note
            {
                Id = int.Parse(values[0]),
                Title = values[1],
                Description = values[2],
                Tag = new Tag(values[3]),
                Category = new Category(values[4])
            };
        }

        public Task ToTask()
        {
            var task = new Task()
            {
                Title = Title,
                Description = Description,
                Tag = Tag,
                Category = Category,
                Priority = "Notatka",
                Deadline = DateTime.Now,
                IsDone = true
            };

            return task;
        }
    }
}

