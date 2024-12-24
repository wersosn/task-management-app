using SuperZTP.Builder;
using SuperZTP.Command;
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
        private List<SuperZTP.Model.Task> tasks;
        private CommandInvoker invoker = new CommandInvoker();
        private TaskBuilder taskBuilder = new TaskBuilder();

        public AddTask(List<SuperZTP.Model.Task> tasks)
        {
            InitializeComponent();
            this.tasks = tasks;
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
                .setTagi(new Tag("Edukacja"))  // Przykładowe tagi
                .setKategorie(new Category("Programowanie"))  // Przykładowa kategoria
                .build();
            zadanie.Id = taskId;
            zadanie.UstalTermin(selectedDate);
            zadanie.UstawPriorytet(priority);
            if (isCompleted)
            {
                zadanie.OznaczJakoWykonane();
            }
            invoker.DodajOperacje(new DodajZadanie(tasks, zadanie));
            invoker.Wykonaj();
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
