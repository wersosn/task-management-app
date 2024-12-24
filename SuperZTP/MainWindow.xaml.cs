using SuperZTP.Controller;
using SuperZTP.Model;
using SuperZTP.Views;
using System;
using System.Text;
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
        private List<SuperZTP.Model.Task> tasks = new List<SuperZTP.Model.Task>();
        private List<Note> notes = new List<Note>();
        private int idN = 1;
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
            TasksListBox.Items.Clear();
            foreach (var task in tasks)
            {
                var taskPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };
                var taskText = new TextBlock
                {
                    Text = $"{task.Title} (Priorytet: {task.Priority}, Termin: {task.Deadline:yyyy-MM-dd})\n",
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

        // Wyświetlanie listy notatek
        private void DisplayNotes()
        {
            NotesListBox.Items.Clear();
            foreach (var note in notes)
            {
                NotesListBox.Items.Add($"{idN++}. {note}");
            }
        }

        // TODO: Naprawić usuwanie i edycję, bo jest popsute ID!!!!
        // TODO: Edycja i usuwanie notatek + wyświetlanie podobnie do zadań ich listy
    }
}