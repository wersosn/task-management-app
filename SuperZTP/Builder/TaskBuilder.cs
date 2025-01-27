using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Model;

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

        public TaskBuilder setTag(Tag tag)
        {
            task.Tag = tag;
            return this;
        }

        public TaskBuilder setCategory(Category category)
        {
            task.Category = category;
            return this;
        }

		public TaskBuilder setState()
		{
			task.CurrentState = new NotStarted();
			return this;
		}

		public Model.Task build()
        {
            return task;
        }


    }
}
