using SuperZTP.Builder;
using SuperZTP.Command;
using SuperZTP.Model;
using SuperZTP.TemplateMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuperZTP.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AddNote.xaml
    /// </summary>
    public partial class AddNote : Window
    {
        private List<Note> notes;
        private CommandInvoker invoker = new CommandInvoker();
        private NoteBuilder noteBuilder = new NoteBuilder();
        private FileHandler fileHandler;

        public AddNote(List<Note> notes, FileHandler fileHandler)
        {
            InitializeComponent();
            this.notes = notes;
            this.fileHandler = fileHandler;
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            string title = NoteTitleTextBox.Text;
            string description = NoteDescriptionTextBox.Text;
            var notatka = noteBuilder
                .setTytul(title)
                .setOpis(description)
                .setTagi(new Tag("Studia"))  // Przykładowe tagi
                .setKategorie(new Category("Zaawansowane techniki programistyczne"))  // Przykładowa kategoria
                .build();
            notatka.Id = GetNextNoteId(notes);
            invoker.DodajOperacje(new DodajNotatke(notes, notatka));
            invoker.Wykonaj();
            fileHandler.SaveNotesToFile("notes.txt");
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public static int GetNextNoteId(List<Note> notes)
        {
            return notes.Any() ? notes.Max(t => t.Id) + 1 : 1;
        }
    }
}
