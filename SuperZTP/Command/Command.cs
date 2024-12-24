using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Model;

namespace SuperZTP.Command
{
    public interface ICommand
    {
        void Wykonaj();
        void Cofnij();
    }

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
            Console.WriteLine($"Dodano element: {newTask}");
        }

        public void Cofnij()
        {
            tasks.Remove(newTask);
            Console.WriteLine($"Cofnięto dodanie elementu: {newTask}");
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
                Console.WriteLine($"Zedytowano zadanie: {oldTask} -> {newTaskCopy}");
            }
        }

        public void Cofnij()
        {
            if (oldTask != null && id >= 0 && id < tasks.Count)
            {
                tasks[id] = oldTask;
                Console.WriteLine($"Cofnięto edycję: {newTaskCopy} -> {oldTask}");
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
                Console.WriteLine($"Usunięto zadanie: {tasks[id]}");
            }
        }

        public void Cofnij()
        {
            if (taskCopy != null && id >= 0 && id <= tasks.Count)
            {
                tasks.Insert(id, taskCopy);
                Console.WriteLine("Przywrócono usunięte zadanie");
            }
        }
    }

    public class DodajNotatke : ICommand
    {
        private List<Note> notes;
        private Note newNote;

        public DodajNotatke(List<Note> notes, Note newNote)
        {
            this.notes = notes;
            this.newNote = newNote;
        }

        public void Wykonaj()
        {
            var noteCopy = new Note
            {
                Title = newNote.Title,
                Description = newNote.Description,
                Tag = newNote.Tag,
                Category = newNote.Category
            };

            notes.Add(noteCopy);
            Console.WriteLine($"Dodano element: {newNote}");
        }

        public void Cofnij()
        {
            notes.Remove(newNote);
            Console.WriteLine($"Cofnięto dodanie elementu: {newNote}");
        }
    }

    public class CommandInvoker
    {
        private readonly List<ICommand> historiaOperacji = new List<ICommand>();
        private readonly Queue<ICommand> operacjeDoWykonania = new Queue<ICommand>();

        public void DodajOperacje(ICommand command)
        {
            operacjeDoWykonania.Enqueue(command);
        }

        public void Wykonaj()
        {
            if (operacjeDoWykonania.Count > 0)
            {
                ICommand command = operacjeDoWykonania.Dequeue();
                command.Wykonaj();
                historiaOperacji.Add(command);
            }
        }

        public void CofnijOstatniaOperacje()
        {
            if (historiaOperacji.Count > 0)
            {
                ICommand command = historiaOperacji.Last();
                command.Cofnij();
                historiaOperacji.RemoveAt(historiaOperacji.Count - 1);
            }
        }

        public void WyczyscHistorie()
        {
            historiaOperacji.Clear();
            Console.WriteLine("Historia operacji została wyczyszczona");
        }
    }
}
