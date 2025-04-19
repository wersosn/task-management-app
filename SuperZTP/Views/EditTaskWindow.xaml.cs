using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using SuperZTP.Command;
using SuperZTP.Model;
using SuperZTP.ViewModels;
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
using SuperZTP.Resources;

namespace SuperZTP.Views
{
    /// <summary>
    /// Logika interakcji dla klasy EditTask.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        private List<SuperZTP.Model.Task> tasks;
        public Model.Task EditedTask { get; private set; }
        private FileHandler fileHandler;
        private CommandInvoker invoker;
        private MenuViewModel _viewModel;
        private Model.Task originalTask;

        public EditTaskWindow(Model.Task taskToEdit, FileHandler fileHandler, List<Category> categories, List<Tag> tags, CommandInvoker invoker, MenuViewModel _viewModel, List<SuperZTP.Model.Task> tasks)
        {
            InitializeComponent();

            this.tasks = tasks;
            this.fileHandler = fileHandler;
            this.invoker = invoker;
            this._viewModel = _viewModel;

            // Tworzymy kopię zadania, aby nie nadpisywać oryginału przed zapisaniem
            originalTask = new Model.Task
            {
                Id = taskToEdit.Id,
                Title = taskToEdit.Title,
                Description = taskToEdit.Description,
                Tag = taskToEdit.Tag,
                Category = taskToEdit.Category,
                Priority = taskToEdit.Priority,
                Deadline = taskToEdit.Deadline,
                IsDone = taskToEdit.IsDone
            };

            EditedTask = new Model.Task
            {
                Id = taskToEdit.Id,
                Title = taskToEdit.Title,
                Description = taskToEdit.Description,
                Tag = taskToEdit.Tag,
                Category = taskToEdit.Category,
                Priority = taskToEdit.Priority,
                Deadline = taskToEdit.Deadline,
                IsDone = taskToEdit.IsDone
            };

            TitleTextBox.Text = EditedTask.Title;
            DescriptionTextBox.Text = EditedTask.Description;
            LoadCategoriesToComboBox(categories);
            SelectCategoryInComboBox(EditedTask.Category);
            LoadTagsToComboBox(tags);
            SelectTagInComboBox(EditedTask.Tag);

            // Obsługa null w PriorityComboBox
            if (!string.IsNullOrEmpty(EditedTask.Priority))
            {
                PriorityComboBox.SelectedItem = PriorityComboBox.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == EditedTask.Priority);
            }

            DeadlineDatePicker.SelectedDate = EditedTask.Deadline;
            IsCompletedCheckBox.IsChecked = EditedTask.IsDone;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TitleTextBox.Text != "")
            {
                EditedTask.Title = TitleTextBox.Text;
                EditedTask.Description = DescriptionTextBox.Text;

                if (CategoryComboBox.SelectedItem is ComboBoxItem selectedCategoryItem && selectedCategoryItem.Tag is Category selectedCategory)
                {
                    EditedTask.Category = selectedCategory;
                }

                if (TagComboBox.SelectedItem is ComboBoxItem selectedTagItem && selectedTagItem.Tag is Tag selectedTag)
                {
                    EditedTask.Tag = selectedTag;
                }

                EditedTask.Priority = ((ComboBoxItem)PriorityComboBox.SelectedItem)?.Content.ToString() ?? "Brak priorytetu";
                EditedTask.Deadline = DeadlineDatePicker.SelectedDate ?? DateTime.Now;
                EditedTask.IsDone = IsCompletedCheckBox.IsChecked ?? false;


                fileHandler.SaveTasksToFile("tasks.txt");

                var editCommand = new EditTask(tasks, originalTask, EditedTask, EditedTask.Id);
                invoker.AddCommand(editCommand);
                invoker.Execute();
                _viewModel.UpdateHistory();

                DialogResult = true;
            }
            else
            {
                MessageBox.Show(Strings.RequiredTitle);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        // Odczytywanie kategorii:
        private void LoadCategoriesToComboBox(List<Category> categories)
        {
            CategoryComboBox.Items.Clear();
            foreach (var category in categories)
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = category.Name,
                    Tag = category
                };
                CategoryComboBox.Items.Add(item);
            }
        }

        private void SelectCategoryInComboBox(Category selectedCategory)
        {
            foreach (ComboBoxItem item in CategoryComboBox.Items)
            {
                if (item.Tag is Category category && category.Name == selectedCategory?.Name)
                {
                    CategoryComboBox.SelectedItem = item;
                    break;
                }
            }
        }

        // Odczytywanie tagów:
        private void LoadTagsToComboBox(List<Tag> tags)
        {
            TagComboBox.Items.Clear();
            foreach (var tag in tags)
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = tag.Name,
                    Tag = tag
                };
                TagComboBox.Items.Add(item);
            }
        }

        private void SelectTagInComboBox(Tag selectedTag)
        {
            foreach (ComboBoxItem item in TagComboBox.Items)
            {
                if (item.Tag is Tag tag && tag.Name == selectedTag?.Name)
                {
                    TagComboBox.SelectedItem = item;
                    break;
                }
            }
        }
    }
}
