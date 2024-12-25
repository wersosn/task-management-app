using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Model;

namespace SuperZTP.Command
{
    public class DodajZadanie : ICommand
    {
        private List<Model.Task> tasks;
        private Model.Task newTask;

        public DodajZadanie(List<Model.Task> tasks, Model.Task newTask)
        {
            this.tasks = tasks;
            this.newTask = newTask;
        }

        public void Wykonaj()
        {
            var taskCopy = new Model.Task
            {
                Id = newTask.Id,
                Title = newTask.Title,
                Description = newTask.Description,
                Tag = newTask.Tag,
                Category = newTask.Category
            };
            taskCopy.UstalTermin(newTask.Deadline);
            taskCopy.UstawPriorytet(newTask.Priority);
            if (newTask.IsDone)
            {
                taskCopy.OznaczJakoWykonane();
            }
            tasks.Add(taskCopy);
        }

        public void Cofnij()
        {
            tasks.Remove(newTask);
        }
    }

    public class EdytujZadanie : ICommand
    {
        private List<Model.Task> tasks;
        private Model.Task newTaskCopy;
        private Model.Task oldTask;
        private int id;

        public EdytujZadanie(List<Model.Task> tasks, Model.Task newTask, int id)
        {
            this.tasks = tasks;
            this.id = id;
            if (id > 0 && id < tasks.Count)
            {
                oldTask = new Model.Task
                {
                    Id = tasks[id].Id,
                    Title = tasks[id].Title,
                    Description = tasks[id].Description,
                    Tag = tasks[id].Tag,
                    Category = tasks[id].Category
                };
                oldTask.UstawPriorytet(tasks[id].Priority);
                oldTask.UstalTermin(tasks[id].Deadline);
                if (tasks[id].IsDone)
                {
                    oldTask.OznaczJakoWykonane();
                }

                newTaskCopy = new Model.Task
                {
                    Id = newTask.Id,
                    Title = newTask.Title,
                    Description = newTask.Description,
                    Tag = newTask.Tag,
                    Category = newTask.Category
                };
                newTaskCopy.UstawPriorytet(newTask.Priority);
                newTaskCopy.UstalTermin(newTask.Deadline);
                if (newTask.IsDone)
                {
                    newTaskCopy.OznaczJakoWykonane();
                }
            }
        }

        public void Wykonaj()
        {
            if (id >= 0 && id < tasks.Count)
            {
                tasks[id] = newTaskCopy;
            }
        }

        public void Cofnij()
        {
            if (oldTask != null && id >= 0 && id < tasks.Count)
            {
                tasks[id] = oldTask;
            }
        }
    }

    public class UsunZadanie : ICommand
    {
        private List<Model.Task> tasks;
        private Model.Task taskCopy;
        private int id;

        public UsunZadanie(List<Model.Task> tasks, Model.Task task, int id)
        {
            this.tasks = tasks;
            this.id = id;
            if (id >= 0 && id < tasks.Count)
            {
                taskCopy = new Model.Task
                {
                    Id = tasks[id].Id,
                    Title = tasks[id].Title,
                    Description = tasks[id].Description,
                    Tag = tasks[id].Tag,
                    Category = tasks[id].Category,
                };
                taskCopy.UstawPriorytet(task.Priority);
                taskCopy.UstalTermin(task.Deadline);
                if (task.IsDone)
                {
                    taskCopy.OznaczJakoWykonane();
                }
            }
        }

        public void Wykonaj()
        {
            if (id > 0 && id < tasks.Count)
            {
                tasks.RemoveAt(id);
            }
        }

        public void Cofnij()
        {
            if (taskCopy != null && id >= 0 && id <= tasks.Count)
            {
                tasks.Insert(id, taskCopy);
            }
        }
    }
}
