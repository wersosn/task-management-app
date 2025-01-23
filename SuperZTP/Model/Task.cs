using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Model;

namespace SuperZTP.Model
{
    // Model Zadania
    public class Task
    {
        // Atrybuty:
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Tag Tag { get; set; }
        public Category Category { get; set; }
        public DateTime Deadline { get; set; }
        public string Priority { get; set; }
        
        //public bool IsDone { get; set; }
        public ITaskState taskState { get; set; }

        public Task() { }

        public Task(int id, string title, string description, Tag tag, Category category, DateTime deadline, string priority, bool isDone)
        {
            Id = id;
            Title = title;
            Description = description;
            Tag = tag;
            Category = category;
            Deadline = deadline;
            Priority = priority;
            //IsDone = false;
        }

        // Metody do ustawiania terminów, priorytetów i oznaczania jako wykonane
        public void SetDeadline(DateTime deadline)
        {
            Deadline = deadline;
        }

        public void SetPriority(string priority)
        {
            Priority = priority;
        }

        public void MarkAsDone()
        {
            IsDone = true;
        }

        public override string ToString()
        {
            return $"Zadanie: {Title}\nOpis: {Description}\nTag: {Tag?.Name}\nKategoria: {Category?.Name}\nTermin: {Deadline}\nPriorytet: {Priority}\nWykonane: {IsDone}";
        }

        // Zapisywanie do pliku (możliwe, że będzie zmienione):
        public string ToFile()
        {
            return $"{Id};{Title};{Description};{Tag?.Name};{Category?.Name};{Deadline:yyyy-MM-dd};{Priority};{IsDone}";
        }

        // Wczytywanie z pliku:
        public static Task FromFile(string line)
        {
            var values = line.Split(';');
            return new Task
            {
                Id = int.Parse(values[0]),
                Title = values[1],
                Description = values[2],
                Tag = new Tag(values[3]),
                Category = new Category(values[4]),
                Deadline = DateTime.Parse(values[5]),
                Priority = values[6],
                //IsDone = bool.Parse(values[7])
            };
        }
    }
}

