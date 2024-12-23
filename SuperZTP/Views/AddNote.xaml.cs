using SuperZTP.Controller;
using SuperZTP.Model;
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
        // Lista notatek
        private List<Note> notes = new List<Note>();
        private CommandInvoker invoker = new CommandInvoker();
        private NoteBuilder noteBuilder = new NoteBuilder();
        private int id = 1;

        public AddNote()
        {
            InitializeComponent();
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz dane z TextBoxów
            string title = NoteTitleTextBox.Text;
            string description = NoteDescriptionTextBox.Text;

            // Stwórz notatkę
            var notatka = noteBuilder
                .setTytul(title)
                .setOpis(description)
                .setTagi(new Tag("Studia"))  // Przykładowe tagi
                .setKategorie(new Category("Zaawansowane techniki programistyczne"))  // Przykładowa kategoria
                .build();

            // Dodaj notatkę do listy
            invoker.DodajOperacje(new DodajNotatke(notes, notatka));
            invoker.Wykonaj();

            // Wyświetl zaktualizowaną listę notatek
            DisplayNotes();
        }

        // Wyświetlanie listy notatek
        private void DisplayNotes()
        {
            NotesListBox.Items.Clear();  // Czyszczenie listy przed ponownym załadowaniem
            foreach (var note in notes)
            {
                NotesListBox.Items.Add($"{id++}. {note}");
            }
        }
    }
}
