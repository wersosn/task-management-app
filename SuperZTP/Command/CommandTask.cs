using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Model;

namespace SuperZTP.Command
{
    // Dodawanie zadania
    public class AddTask : ICommand
    {
        private List<Model.Task> tasks;
        private Model.Task newTask;
        private readonly Action _onTaskAdded;

        public AddTask(List<Model.Task> tasks, Model.Task newTask, Action onTaskAdded)
        {
            this.tasks = tasks;
            this.newTask = newTask;
            _onTaskAdded = onTaskAdded;
        }

        public void Execute()
        {
            var taskCopy = new Model.Task // Praca na kopii zadania, aby uniknąć pracy na referencji
            {
                Id = newTask.Id,
                Title = newTask.Title,
                Description = newTask.Description,
                Tag = newTask.Tag,
                Category = newTask.Category,
				CurrentState = newTask.CurrentState ?? new NotStarted()
			};
            taskCopy.SetDeadline(newTask.Deadline);
            taskCopy.SetPriority(newTask.Priority);
			//taskCopy.ChangeState(newTask.CurrentState);
			//if (newTask.IsDone)
			//{
			//    taskCopy.MarkAsDone();
			//}

			tasks.Add(taskCopy);
            _onTaskAdded?.Invoke();
        }

        public void Undo()
        {
            tasks.Remove(newTask);
            _onTaskAdded?.Invoke();
        }
    }

    // Modyfikacja zadania
    public class EditTask : ICommand
    {
        private List<Model.Task> tasks;
        private Model.Task newTaskCopy;
        private Model.Task oldTask;
        private int id;

        public EditTask(List<Model.Task> tasks, Model.Task newTask, int id)
        {
            this.tasks = tasks;
            this.id = id;
            if (id > 0 && id < tasks.Count)
            {
                oldTask = new Model.Task // Kopia starej wersji zadania, aby uniknąć pracy na referencji
                {
                    Id = tasks[id].Id,
                    Title = tasks[id].Title,
                    Description = tasks[id].Description,
                    Tag = tasks[id].Tag,
                    Category = tasks[id].Category,
                    CurrentState= tasks[id].CurrentState
                };
                oldTask.SetPriority(tasks[id].Priority);
                oldTask.SetDeadline(tasks[id].Deadline);
				
				//if (tasks[id].IsDone)
				//{
				//    oldTask.MarkAsDone();
				//}

				newTaskCopy = new Model.Task // Kopia nowej wersji zadania, aby uniknąć pracy na referencji
                {
                    Id = newTask.Id,
                    Title = newTask.Title,
                    Description = newTask.Description,
                    Tag = newTask.Tag,
                    Category = newTask.Category,

										CurrentState = tasks[id].CurrentState

				};
                newTaskCopy.SetPriority(newTask.Priority);
                newTaskCopy.SetDeadline(newTask.Deadline);
                //if (newTask.IsDone)
                //{
                //    newTaskCopy.MarkAsDone();
                //}
            }
        }

        public void Execute()
        {
            if (id >= 0 && id < tasks.Count)
            {
                tasks[id] = newTaskCopy;
            }
        }

        public void Undo()
        {
            if (oldTask != null && id >= 0 && id < tasks.Count)
            {
                tasks[id] = oldTask;
            }
        }
    }

    // Usuwanie zadania
    public class DeleteTask : ICommand
    {
        private readonly List<Model.Task> _tasks;
        private readonly Model.Task _taskToDelete;
        private readonly int _index;
        private readonly Action _onTaskDeleted;

        public DeleteTask(List<Model.Task> tasks, Model.Task task, Action onTaskDeleted)
        {
            _tasks = tasks ?? throw new ArgumentNullException(nameof(tasks));
            _taskToDelete = task ?? throw new ArgumentNullException(nameof(task));
            _onTaskDeleted = onTaskDeleted ?? throw new ArgumentNullException(nameof(onTaskDeleted));
            _index = _tasks.FindIndex(t => t.Id == task.Id);
        }
        

        public void Execute()
        {
             if (_index >= 0)
             {
                _tasks.RemoveAt(_index);
                _onTaskDeleted?.Invoke(); // Powiadamiamy `DisplayTasksViewModel` o zmianie
             }
        }

        public void Undo()
        {
            if (_index >= 0)
            {
                _tasks.Insert(_index, _taskToDelete);
                _onTaskDeleted?.Invoke(); // Przywracamy taska i odświeżamy widok
            }
        }
    }

    // Grupowanie zadań
    public class GroupTasks
    {
        public List<IGrouping<string, Model.Task>> GroupTasksByCategory(List<Model.Task> tasks)
        {
            var groupedTasks = tasks
                .GroupBy(task => task.Category?.Name ?? "Brak kategorii")
                .OrderBy(group => group.Key)
                .ToList();
            return groupedTasks;
        }

        public List<IGrouping<string, Model.Task>> GroupTasksByTag(List<Model.Task> tasks)
        {
            var groupedTasks = tasks
               .GroupBy(task => task.Tag?.Name ?? "Brak tagu")
               .OrderBy(group => group.Key)
               .ToList();
            return groupedTasks;
        }
    }

    // Sortowanie zadań
    public class SortTasks
    {
        public List<Model.Task> SortTasksByTitle(List<Model.Task> tasks, bool ascending = true)
        {
            var sortedTasks = ascending
                ? tasks.OrderBy(task => task.Title).ToList()
                : tasks.OrderByDescending(task => task.Title).ToList();

            tasks = sortedTasks;
            return tasks;
        }

        public List<Model.Task> SortTasksByPriority(List<Model.Task> tasks, bool ascending = true)
        {
            var priorityOrder = new Dictionary<string, int>
            {
                { "Niski", 1 },
                { "Średni", 2 },
                { "Wysoki", 3 }
            };

            var sortedTasks = ascending
                ? tasks.OrderBy(task => priorityOrder.ContainsKey(task.Priority) ? priorityOrder[task.Priority] : 0).ToList()
                : tasks.OrderByDescending(task => priorityOrder.ContainsKey(task.Priority) ? priorityOrder[task.Priority] : 0).ToList();

            tasks = sortedTasks;
            return tasks;
        }

        public List<Model.Task> SortTasksByDeadline(List<Model.Task> tasks, bool ascending = true)
        {
            var sortedTasks = ascending
                ? tasks.OrderBy(task => task.Deadline).ToList()
                : tasks.OrderByDescending(task => task.Deadline).ToList();

            tasks = sortedTasks;
            return tasks;
        }
    }
}
