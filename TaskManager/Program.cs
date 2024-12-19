using System;
using TaskManager.Controller;
using TaskManager.Model;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            // Command + Builder taska:
            int id = 1;
            List<TaskManager.Model.Task> tasks = new List<TaskManager.Model.Task>();
            CommandInvoker invoker = new CommandInvoker();
            TaskBuilder taskBuilder = new TaskBuilder();

            // Test tworzenia zadań
            var zadanie = taskBuilder
                .setTytul("Nauka C#")
                .setOpis("Nauczyć się wzorca projektowego Builder")
                .setTagi(new Tag("Edukacja"))
                .setKategorie(new Category("Programowanie"))
                .build();
            zadanie.UstalTermin(DateTime.Now.AddDays(2));
            zadanie.UstawPriorytet("Wysoki");
            invoker.DodajOperacje(new DodajElement(tasks, zadanie));
            invoker.Wykonaj();
            Console.WriteLine("---------------------------------------");

            var zadanie2 = taskBuilder
                .setTytul("Test zadań")
                .setOpis("Sprawdzić czy dodawanie zadania działa :)")
                .setTagi(new Tag("Studia"))
                .setKategorie(new Category("ProjektUML"))
                .build();
            zadanie2.UstalTermin(DateTime.Now);
            zadanie2.UstawPriorytet("Niski");
            zadanie2.OznaczJakoWykonane();
            invoker.DodajOperacje(new DodajElement(tasks, zadanie2));
            invoker.Wykonaj();
            Console.WriteLine("---------------------------------------");

            Console.WriteLine("Aktualna lista zadań:");
            for(int i=0; i<tasks.Count; i++)
            {
                Console.WriteLine(id++ + ".\n" + tasks[i]);
                Console.WriteLine("---------------------------------------");
            }

            // Test tworzenia notatki:
            List<Note> notes = new List<Note>(); 
            NoteBuilder noteBuilder = new NoteBuilder();

            var notatka = noteBuilder
                .setTytul("Pierwsza część projektu ZTP")
                .setOpis("Należy przygotować diagram UML oraz prototyp programu")
                .setTagi(new Tag("Studia"))
                .setKategorie(new Category("Zaawansowane techniki programistyczne"))
                .build();
            invoker.DodajOperacje(new DodajNotatke(notes, notatka));
            invoker.Wykonaj();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Aktualna lista notatek:");
            id = 1;
            for (int i = 0; i < notes.Count; i++)
            {
                Console.WriteLine(id++ + ".\n" + notes[i]);
                Console.WriteLine("---------------------------------------");
            }
        }
    }
}