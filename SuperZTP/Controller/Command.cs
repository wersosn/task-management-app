using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Model;

namespace SuperZTP.Controller
{
    public interface ICommand
    {
        void Wykonaj();
        void Cofnij();
    }

    public class DodajElement : ICommand
    {
        private List<SuperZTP.Model.Task> tasks;
        private SuperZTP.Model.Task newTask;

        public DodajElement(List<SuperZTP.Model.Task> tasks, SuperZTP.Model.Task newTask)
        { 
            this.tasks = tasks; 
            this.newTask = newTask;
        }

        public void Wykonaj()
        {
            var taskCopy = new SuperZTP.Model.Task
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

    //TODO - Zmienić edycję i usuwanie, aby pracowały na kopii elementu a nie referencji!!!

    public class EdytujElement : ICommand
    {
        private List<SuperZTP.Model.Task> tasks;
        private SuperZTP.Model.Task newTask;
        private SuperZTP.Model.Task oldTask;
        private int id;

        public EdytujElement(List<SuperZTP.Model.Task> tasks, SuperZTP.Model.Task newTask, SuperZTP.Model.Task oldTask, int id)
        {
            this.tasks = tasks;
            this.newTask = newTask;
            this.oldTask = oldTask;
            this.id = id;
        }

        public void Wykonaj()
        {
            if (id > 0 && id < tasks.Count)
            {
                oldTask = tasks[id];
                tasks[id] = newTask;
                Console.WriteLine($"Zedytowano element: {oldTask} -> {newTask}");
            }
        }

        public void Cofnij()
        {
            if(oldTask != null)
            {
                tasks[id] = oldTask;
                Console.WriteLine($"Cofnięto edycję: {newTask} -> {oldTask}");
            }
        }
    }

    public class UsunElement : ICommand
    {
        private List<SuperZTP.Model.Task> tasks;
        private SuperZTP.Model.Task task;
        private int id;

        public UsunElement(List<SuperZTP.Model.Task> tasks, SuperZTP.Model.Task newTask, int id)
        {
            this.tasks = tasks;
            this.task = newTask;
            this.id = id;
        }

        public void Wykonaj()
        {
            if (id > 0 && id < tasks.Count)
            {
                task = tasks[id];
                tasks.RemoveAt(id);
                Console.WriteLine($"usunięto element: {task}");
            }
        }

        public void Cofnij()
        {
            if(task  != null)
            {
                tasks.Insert(id, task);
                Console.WriteLine("Przywrócono usunięty element");
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
