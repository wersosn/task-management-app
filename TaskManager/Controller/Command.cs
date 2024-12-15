﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Controller
{
    public interface ICommand
    {
        void Wykonaj();
        void Cofnij();
    }

    public class DodajElement : ICommand
    {
        private List<TaskManager.Model.Task> tasks;
        private TaskManager.Model.Task newTask;

        public DodajElement(List<TaskManager.Model.Task> tasks, TaskManager.Model.Task newTask)
        { 
            this.tasks = tasks; 
            this.newTask = newTask;
        }

        public void Wykonaj()
        {
            var taskCopy = new TaskManager.Model.Task
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
        private List<TaskManager.Model.Task> tasks;
        private TaskManager.Model.Task newTask;
        private TaskManager.Model.Task oldTask;
        private int id;

        public EdytujElement(List<TaskManager.Model.Task> tasks, TaskManager.Model.Task newTask, TaskManager.Model.Task oldTask, int id)
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
        private List<TaskManager.Model.Task> tasks;
        private TaskManager.Model.Task task;
        private int id;

        public UsunElement(List<TaskManager.Model.Task> tasks, TaskManager.Model.Task newTask, int id)
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
