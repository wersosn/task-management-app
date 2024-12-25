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
        public ITag Tag { get; set; }
        public ICategory Category { get; set; }

        public Note() { }
        public Note(int id, string title, string description, ITag tag, ICategory category)
		{
            Id = id;
            Title = title;
			Description = description;
			Tag = tag;
			Category = category;
		}

        public override string ToString()
        {
            return $"Notatka: {Title}\nOpis: {Description}\nTag: {Tag?.Name}\nKategoria: {Category?.CategoryName}";
        }

        // Zapisywanie do pliku (możliwe, że będzie zmienione):
        public string ToCsv()
        {
            return $"{Id};{Title};{Description};{Tag?.Name};{Category?.CategoryName}";
        }


    }
}

