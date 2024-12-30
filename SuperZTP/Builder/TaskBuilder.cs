using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Composite;

namespace SuperZTP.Builder
{
    // Builder Zadania
    public class TaskBuilder
    {
        private readonly Model.Task task = new Model.Task();
        public TaskBuilder setTitle(string title)
        {
            task.Title = title;
            return this;
        }

        public TaskBuilder setDescription(string description)
        {
            task.Description = description;
            return this;
        }

        public TaskBuilder setTag(ITag tag)
        {
            task.Tag = tag;
            return this;
        }

        public TaskBuilder setCategory(ICategory category)
        {
            task.Category = category;
            return this;
        }

        public Model.Task build()
        {
            return task;
        }
    }
}
