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
    /// Logika interakcji dla klasy AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        private List<SuperZTP.Model.Task> tasks;
        private CommandInvoker invoker = new CommandInvoker();
        private TaskBuilder taskBuilder = new TaskBuilder();
        private FileHandler fileHandler;

        public AddTask(List<SuperZTP.Model.Task> tasks, FileHandler fileHandler)
        {
            InitializeComponent();
            this.tasks = tasks;
            this.fileHandler = fileHandler;
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            int taskId = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
            string title = TitleTextBox.Text;
            string description = DescriptionTextBox.Text;
            string priority = ((ComboBoxItem)PriorityComboBox.SelectedItem)?.Content.ToString() ?? "Niski";
            DateTime selectedDate = TaskDatePicker.SelectedDate ?? DateTime.Now; // Domyślnie bieżąca data, jeśli brak wyboru
            bool isCompleted = IsCompletedCheckBox.IsChecked ?? false;

            var zadanie = taskBuilder
                .setTytul(title)
                .setOpis(description)
                .setTagi(new Tag("Dom"))  // Przykładowe tagi
                .setKategorie(new Category("Zabawa"))  // Przykładowa kategoria
                .build();
            zadanie.Id = GetNextTaskId(tasks);
            zadanie.UstalTermin(selectedDate);
            zadanie.UstawPriorytet(priority);
            if (isCompleted)
            {
                zadanie.OznaczJakoWykonane();
            }
            invoker.DodajOperacje(new DodajZadanie(tasks, zadanie));
            invoker.Wykonaj();
            fileHandler.SaveTasksToFile("tasks.txt");
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public static int GetNextTaskId(List<Model.Task> tasks)
        {
            return tasks.Any() ?tasks.Max(t => t.Id) + 1 : 1;
        }
    }
}
