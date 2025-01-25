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
    /// Logika interakcji dla klasy EditTask.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        public Model.Task EditedTask { get; private set; }
        private FileHandler fileHandler;

        public EditTaskWindow(Model.Task taskToEdit, FileHandler fileHandler, List<Category> categories, List<Tag> tags)
        {
            InitializeComponent();
            TitleTextBox.Text = taskToEdit.Title;
            DescriptionTextBox.Text = taskToEdit.Description;
            LoadCategoriesToComboBox(categories);
            SelectCategoryInComboBox(taskToEdit.Category);
            LoadTagsToComboBox(tags);
            SelectTagInComboBox(taskToEdit.Tag);
            PriorityComboBox.SelectedItem = PriorityComboBox.Items
                .Cast<ComboBoxItem>()
                .FirstOrDefault(item => item.Content.ToString() == taskToEdit.Priority);
            DeadlineDatePicker.SelectedDate = taskToEdit.Deadline;
            EditedTask = taskToEdit;
            this.fileHandler = fileHandler;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
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
            EditedTask.Priority = ((ComboBoxItem)PriorityComboBox.SelectedItem)?.Content.ToString();
            EditedTask.Deadline = DeadlineDatePicker.SelectedDate ?? DateTime.Now;
            fileHandler.SaveTasksToFile("tasks.txt");
            DialogResult = true;
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
