using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Model
{
    // Builder Zadania
    public class TaskBuilder
    {
        private readonly Task task = new Task();
        public TaskBuilder setTytul(string title)
        {
            task.Title = title;
            return this;
        }

        public TaskBuilder setOpis(string description)
        {
            task.Description = description; 
            return this;
        }

        public TaskBuilder setTagi(ITag tag)
        {
            task.Tag = tag; 
            return this;
        }

        public TaskBuilder setKategorie(ICategory category)
        {
            task.Category = category;
            return this;
        }

        public Task build()
        {
            return task;
        }
    }
}
