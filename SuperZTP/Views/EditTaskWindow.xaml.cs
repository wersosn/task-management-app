using SuperZTP.Command;
using SuperZTP.Composite;
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

        public EditTaskWindow(Model.Task taskToEdit, FileHandler fileHandler, ICategory categories, ITag tags)
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
            IsCompletedCheckBox.IsChecked = taskToEdit.IsDone;
            EditedTask = taskToEdit;
            this.fileHandler = fileHandler;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            EditedTask.Title = TitleTextBox.Text;
            EditedTask.Description = DescriptionTextBox.Text;
            if (CategoryComboBox.SelectedItem is ComboBoxItem selectedCategoryItem && selectedCategoryItem.Tag is ICategory selectedCategory)
            {
                EditedTask.Category = selectedCategory;
            }
            if (TagComboBox.SelectedItem is ComboBoxItem selectedTagItem && selectedTagItem.Tag is ITag selectedTag)
            {
                EditedTask.Tag = selectedTag;
            }
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

        // Odczytywanie kategorii:
        private void LoadCategoriesToComboBox(ICategory rootCategory)
        {
            CategoryComboBox.Items.Clear();
            if (rootCategory is Category cat)
            {
                foreach (var subCategory in cat.categories)
                {
                    AddCategoriesToComboBox(subCategory, "");
                }
            }
        }

        private void AddCategoriesToComboBox(ICategory category, string parentPath)
        {
            string fullPath = string.IsNullOrEmpty(parentPath)
                ? category.Name
                : $"{parentPath} > {category.Name}";

            if (!string.IsNullOrEmpty(parentPath))
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = fullPath,
                    Tag = category
                };
                CategoryComboBox.Items.Add(item);
            }

            if (category is Category cat)
            {
                foreach (var subCategory in cat.categories)
                {
                    AddCategoriesToComboBox(subCategory, fullPath);
                }
            }
        }

        private void SelectCategoryInComboBox(ICategory selectedCategory)
        {
            foreach (ComboBoxItem item in CategoryComboBox.Items)
            {
                if (item.Tag is ICategory category && category.Name == selectedCategory?.Name)
                {
                    CategoryComboBox.SelectedItem = item;
                    break;
                }
            }
        }

        // Odczytywanie tagów:
        private void LoadTagsToComboBox(ITag rootTag)
        {
            TagComboBox.Items.Clear();
            if (rootTag is Tag t)
            {
                foreach (var subTag in t.tags)
                {
                    AddTagsToComboBox(subTag, "");
                }
            }
        }

        private void AddTagsToComboBox(ITag tag, string parentPath)
        {
            string fullPath = string.IsNullOrEmpty(parentPath)
                ? tag.Name
                : $"{parentPath} > {tag.Name}";

            if (!string.IsNullOrEmpty(parentPath))
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = fullPath,
                    Tag = tag
                };
                TagComboBox.Items.Add(item);
            }

            if (tag is Tag t)
            {
                foreach (var subTag in t.tags)
                {
                    AddTagsToComboBox(subTag, fullPath);
                }
            }
        }

        private void SelectTagInComboBox(ITag selectedTag)
        {
            foreach (ComboBoxItem item in TagComboBox.Items)
            {
                if (item.Tag is ITag tag && tag.Name == selectedTag?.Name)
                {
                    TagComboBox.SelectedItem = item;
                    break;
                }
            }
        }
    }
}
