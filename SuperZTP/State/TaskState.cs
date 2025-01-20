using SuperZTP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Model;
using Task = SuperZTP.Model.Task;

namespace SuperZTP.Facade
{
    public class TaskState
    {
        public List<Task> Tasks { get; }
        public List<Note> Notes { get; }
        public List<Category> Categories { get; }
        public List<Tag> Tags { get; }
        public FileHandler FileHandler { get; }

        public TaskState(List<Task> tasks, List<Note> notes, List<Category> categories, List<Tag> tags, FileHandler fileHandler)
        {
            Tasks = tasks;
            Notes = notes;
            Categories = categories;
            Tags = tags;
            FileHandler = fileHandler;
        }
    }
}
