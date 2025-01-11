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
    /// Logika interakcji dla klasy EditNote.xaml
    /// </summary>
    public partial class EditNoteWindow : Window
    {
        public Note EditedNote { get; set; }
        private FileHandler fileHandler;

        public EditNoteWindow(Note noteToEdit, FileHandler fileHandler, List<Category> categories, List<Tag> tags)
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
            if (CategoryComboBox.SelectedItem is ComboBoxItem selectedCategoryItem && selectedCategoryItem.Tag is Category selectedCategory)
            {
                EditedNote.Category = selectedCategory;
            }
            if (TagComboBox.SelectedItem is ComboBoxItem selectedTagItem && selectedTagItem.Tag is Tag selectedTag)
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
