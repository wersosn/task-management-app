using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Model
{
    // Model Zadania
    public class Task
    {
        // Atrybuty:
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ITag Tag { get; set; }
        public ICategory Category { get; set; }
        public DateTime Deadline { get; set; }
        public string Priority { get; set; }
        public bool IsDone { get; set; }

        public Task() { }

        public Task(int id, string title, string description, ITag tag, ICategory category, DateTime deadline, string priority, bool isDone)
        {
            Id = id;
            Title = title;
            Description = description;
            Tag = tag;
            Category = category;
            Deadline = deadline;
            Priority = priority;
            IsDone = false;
        }


        // Metody do ustawiania terminów, priorytetów i oznaczania jako wykonane
        public void UstalTermin(DateTime deadline)
        {
            Deadline = deadline;
        }

        public void UstawPriorytet(string priority)
        {
            Priority = priority;
        }

        public void OznaczJakoWykonane()
        {
            IsDone = true;
        }

        public override string ToString()
        {
            return $"Zadanie: {Title}\nOpis: {Description}\nTag: {Tag?.Name}\nKategoria: {Category?.CategoryName}\nTermin: {Deadline}\nPriorytet: {Priority}\nWykonane: {IsDone}";
        }
    }
}

