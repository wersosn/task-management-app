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
    /// Logika interakcji dla klasy EditTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        public Model.Task EditedTask { get; private set; }
        private FileHandler fileHandler;

        public EditTask(Model.Task taskToEdit, FileHandler fileHandler)
        {
            InitializeComponent();
            TitleTextBox.Text = taskToEdit.Title;
            DescriptionTextBox.Text = taskToEdit.Description;
            PriorityComboBox.SelectedItem = PriorityComboBox.Items
                .Cast<ComboBoxItem>()
                .FirstOrDefault(item => item.Content.ToString() == taskToEdit.Priority);
            DeadlineDatePicker.SelectedDate = taskToEdit.Deadline;
            IsCompletedCheckBox.IsChecked = taskToEdit.IsDone;
            EditedTask = taskToEdit;
            this.fileHandler = fileHandler;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            EditedTask.Title = TitleTextBox.Text;
            EditedTask.Description = DescriptionTextBox.Text;
            EditedTask.Priority = ((ComboBoxItem)PriorityComboBox.SelectedItem)?.Content.ToString();
            EditedTask.Deadline = DeadlineDatePicker.SelectedDate ?? DateTime.Now;
            EditedTask.IsDone = IsCompletedCheckBox.IsChecked ?? false;
            fileHandler.SaveTasksToFile("tasks.txt");
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
