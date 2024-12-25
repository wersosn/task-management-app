using SuperZTP.Controller;
using SuperZTP.Model;
using SuperZTP.TemplateMethod;
using SuperZTP.Views;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private List<Model.Task> tasks = new List<Model.Task>();
        private List<Note> notes = new List<Note>();
        private FileHandler fileHandler;

        public MainWindow()
        {
            InitializeComponent();
            fileHandler = new FileHandler(tasks, notes);
        }

        // TASKI
        // Dodawanie taska
        public void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTask addTask = new AddTask(tasks, fileHandler);
            addTask.ShowDialog();
            DisplayTasks();
        }

        // Edycja zadania
        public void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var taskToEdit = tasks.FirstOrDefault(t => t.Id == (int)button.Tag);

            if (taskToEdit == null)
                return;

            var editTaskWindow = new EditTask(taskToEdit);
            if (editTaskWindow.ShowDialog() == true)
            {
                var editedTask = editTaskWindow.EditedTask;
                var taskIndex = tasks.FindIndex(t => t.Id == editedTask.Id);
                if (taskIndex >= 0)
                {
                    tasks[taskIndex] = editedTask;
                }
                DisplayTasks();
            }
        }

        // Usuwanie zadania
        public void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var taskIdToDelete = (int)button.Tag;

            var taskToDelete = tasks.FirstOrDefault(t => t.Id == taskIdToDelete);

            if (taskToDelete != null)
            {
                if (MessageBox.Show($"Czy na pewno chcesz usunąć zadanie: {taskToDelete.Title}?", "Usuń zadanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    tasks.Remove(taskToDelete);
                    DisplayTasks();
                }
            }
        }

        // Wyświetlanie listy zadań
        private void DisplayTasks()
        {
            string status;
            TasksListBox.Items.Clear();
            foreach (var task in tasks)
            {
                var taskPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };
                if(task.IsDone)
                {
                    status = "Ukończone";
                }
                else
                {
                    status = "Nie ukończone";
                }

                var taskText = new TextBlock
                {
                    Text = $"{task.Id}. {task.Title} (Priorytet: {task.Priority}, Termin: {task.Deadline:yyyy-MM-dd})\n{status}\n",
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 270
                };

                // Przyciski
                var editButton = new Button
                {
                    Content = "Edytuj",
                    Tag = task.Id,
                    Margin = new Thickness(5),
                    Width = 75
                };
                editButton.Click += EditTaskButton_Click;

                var deleteButton = new Button
                {
                    Content = "Usuń",
                    Tag = task.Id,
                    Margin = new Thickness(5),
                    Width = 75
                };
                deleteButton.Click += DeleteTaskButton_Click;
                taskPanel.Children.Add(taskText);
                taskPanel.Children.Add(editButton);
                taskPanel.Children.Add(deleteButton);
                TasksListBox.Items.Add(taskPanel);
            }
        }

        // NOTATKI
        // Dodawanie notatki
        public void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            AddNote addNote = new AddNote(notes);
            addNote.ShowDialog();
            DisplayNotes();
        }

        // Edycja notatki
        public void EditNoteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var noteToEdit = notes.FirstOrDefault(t => t.Id == (int)button.Tag);

            if (noteToEdit == null)
            {
                return;
            }

            var editNoteWindow = new EditNote(noteToEdit);
            if (editNoteWindow.ShowDialog() == true)
            {
                var editedNote = editNoteWindow.EditedNote;
                var noteIndex = tasks.FindIndex(t => t.Id == editedNote.Id);
                if (noteIndex >= 0)
                {
                    notes[noteIndex] = editedNote;
                }
                DisplayNotes();
            }
        }

        // Usuwanie notatki
        public void DeleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var noteIdToDelete = (int)button.Tag;

            var noteToDelete = notes.FirstOrDefault(t => t.Id == noteIdToDelete);

            if (noteToDelete != null)
            {
                if (MessageBox.Show($"Czy na pewno chcesz usunąć zadanie: {noteToDelete.Title}?", "Usuń zadanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    notes.Remove(noteToDelete);
                    DisplayNotes();
                }
            }
        }

        // Wyświetlanie listy notatek
        private void DisplayNotes()
        {
            NotesListBox.Items.Clear();
            foreach (var note in notes)
            {
                var notePanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };
                var noteText = new TextBlock
                {
                    Text = $"{note.Id}. {note.Title} {note.Description}",
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 270
                };

                // Przyciski
                var editButton = new Button
                {
                    Content = "Edytuj",
                    Tag = note.Id,
                    Margin = new Thickness(5),
                    Width = 75
                };
                editButton.Click += EditNoteButton_Click;

                var deleteButton = new Button
                {
                    Content = "Usuń",
                    Tag = note.Id,
                    Margin = new Thickness(5),
                    Width = 75
                };
                deleteButton.Click += DeleteNoteButton_Click;
                notePanel.Children.Add(noteText);
                notePanel.Children.Add(editButton);
                notePanel.Children.Add(deleteButton);
                NotesListBox.Items.Add(notePanel);
            }
        }
    }
}