using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Controller;
using SuperZTP.Model;

namespace SuperZTP
{
    public class Program
    {
        /*static void Main(string[] args)
        {
            // Command + Builder taska:
            int id = 1;
            List<SuperZTP.Model.Task> tasks = new List<SuperZTP.Model.Task>();
            CommandInvoker invoker = new CommandInvoker();
            TaskBuilder taskBuilder = new TaskBuilder();

            // Test tworzenia zadań
            var zadanie = taskBuilder
                .setTitle("Nauka C#")
                .setDescription("Nauczyć się wzorca projektowego Builder")
                .setTag(new Tag("Edukacja"))
                .setCategory(new Category("Programowanie"))
                .build();
            zadanie.SetDeadline(DateTime.Now.AddDays(2));
            zadanie.SetPriority("Wysoki");
            invoker.AddCommand(new AddTask(tasks, zadanie));
            invoker.Execute();
            Console.WriteLine("---------------------------------------");

            var zadanie2 = taskBuilder
                .setTitle("Test zadań")
                .setDescription("Sprawdzić czy dodawanie zadania działa :)")
                .setTag(new Tag("Studia"))
                .setCategory(new Category("ProjektUML"))
                .build();
            zadanie2.SetDeadline(DateTime.Now);
            zadanie2.SetPriority("Niski");
            zadanie2.MarkAsDone();
            invoker.AddCommand(new AddTask(tasks, zadanie2));
            invoker.Execute();
            Console.WriteLine("---------------------------------------");

            Console.WriteLine("Aktualna lista zadań:");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine(id++ + ".\n" + tasks[i]);
                Console.WriteLine("---------------------------------------");
            }

            // Test tworzenia notatki:
            List<Note> notes = new List<Note>();
            NoteBuilder noteBuilder = new NoteBuilder();

            var notatka = noteBuilder
                .setTitle("Pierwsza część projektu ZTP")
                .setDescription("Należy przygotować diagram UML oraz prototyp programu")
                .setTag(new Tag("Studia"))
                .setCategory(new Category("Zaawansowane techniki programistyczne"))
                .build();
            invoker.AddCommand(new AddNote(notes, notatka));
            invoker.Execute();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Aktualna lista notatek:");
            id = 1;
            for (int i = 0; i < notes.Count; i++)
            {
                Console.WriteLine(id++ + ".\n" + notes[i]);
                Console.WriteLine("---------------------------------------");
            }
        }*/
    }
}
