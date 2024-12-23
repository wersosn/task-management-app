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
    /// Logika interakcji dla klasy AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        // Lista zadań
        private List<SuperZTP.Model.Task> tasks = new List<SuperZTP.Model.Task>();
        private CommandInvoker invoker = new CommandInvoker();
        private TaskBuilder taskBuilder = new TaskBuilder();
        private int id = 1;

        public AddTask()
        {
            InitializeComponent();
        }

        // Kliknięcie przycisku Dodaj zadanie
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz dane z TextBoxów
            string title = TitleTextBox.Text;
            string description = DescriptionTextBox.Text;

            // Pobierz wybrany priorytet
            string priority = ((ComboBoxItem)PriorityComboBox.SelectedItem)?.Content.ToString() ?? "Niski";

            // Pobierz wybraną datę
            DateTime selectedDate = TaskDatePicker.SelectedDate ?? DateTime.Now; // domyślnie bieżąca data, jeśli brak wyboru

            // Pobierz status ukończenia
            bool isCompleted = IsCompletedCheckBox.IsChecked ?? false;

            // Stwórz zadanie
            var zadanie = taskBuilder
                .setTytul(title)
                .setOpis(description)
                .setTagi(new Tag("Edukacja"))  // Przykładowe tagi
                .setKategorie(new Category("Programowanie"))  // Przykładowa kategoria
                .build();
            zadanie.UstalTermin(selectedDate);
            zadanie.UstawPriorytet(priority);

            // Ustaw status ukończenia
            if (isCompleted)
            {
                zadanie.OznaczJakoWykonane();
            }

            // Dodaj zadanie do listy
            invoker.DodajOperacje(new DodajElement(tasks, zadanie));
            invoker.Wykonaj();

            // Wyświetl zaktualizowaną listę zadań
            DisplayTasks();
        }

        // Wyświetlanie listy zadań
        private void DisplayTasks()
        {
            TasksListBox.Items.Clear();  // Czyszczenie listy przed ponownym załadowaniem
            foreach (var task in tasks)
            {
                TasksListBox.Items.Add($"{id++}. {task}");
            }
        }
    }
}
