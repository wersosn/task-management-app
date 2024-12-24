using SuperZTP.Controller;
using SuperZTP.Model;
using SuperZTP.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperZTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<SuperZTP.Model.Task> tasks = new List<SuperZTP.Model.Task>();
        private List<Note> notes = new List<Note>();
        private int idT = 1, idN = 1;
        public MainWindow()
        {
            InitializeComponent();
        }

        // Dodawanie taska
        public void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTask addTask = new AddTask(tasks);
            addTask.ShowDialog();
            DisplayTasks();
        }

        // Dodawanie notatki
        public void AddNoteButton_Click(object sender, RoutedEventArgs e) 
        {
            AddNote addNote = new AddNote(notes);
            addNote.ShowDialog();
            DisplayNotes();
        }

        // Wyświetlanie listy zadań
        private void DisplayTasks()
        {
            TasksListBox.Items.Clear();
            foreach (var task in tasks)
            {
                TasksListBox.Items.Add($"{idT++}. {task}");
            }
        }

        // Wyświetlanie listy notatek
        private void DisplayNotes()
        {
            NotesListBox.Items.Clear();
            foreach (var note in notes)
            {
                NotesListBox.Items.Add($"{idN++}. {note}");
            }
        }
    }
}