using SuperZTP.Model;
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
    /// Logika interakcji dla klasy EditNote.xaml
    /// </summary>
    public partial class EditNoteWindow : Window
    {
        public Note EditedNote { get; set; }
        private FileHandler fileHandler;

        public EditNoteWindow(Note noteToEdit, FileHandler fileHandler, ICategory categories, ITag tags)
        {
            InitializeComponent();
            NoteTitleTextBox.Text = noteToEdit.Title;
            NoteDescriptionTextBox.Text = noteToEdit.Description;
            LoadCategoriesToComboBox(categories);
            SelectCategoryInComboBox(noteToEdit.Category);
            LoadTagsToComboBox(tags);
            SelectTagInComboBox(noteToEdit.Tag);
            EditedNote = noteToEdit;
            this.fileHandler = fileHandler;
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            EditedNote.Title = NoteTitleTextBox.Text;
            EditedNote.Description = NoteDescriptionTextBox.Text;
            if (CategoryComboBox.SelectedItem is ComboBoxItem selectedCategoryItem && selectedCategoryItem.Tag is ICategory selectedCategory)
            {
                EditedNote.Category = selectedCategory;
            }
            if (TagComboBox.SelectedItem is ComboBoxItem selectedTagItem && selectedTagItem.Tag is ITag selectedTag)
            {
                EditedNote.Tag = selectedTag;
            }
            fileHandler.SaveNotesToFile("notes.txt");
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
