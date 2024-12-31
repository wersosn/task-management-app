using SuperZTP.Builder;
using SuperZTP.Command;
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
    /// Logika interakcji dla klasy AddNote.xaml
    /// </summary>
    public partial class AddNoteWindow : Window
    {
        private List<Note> notes;
        private CommandInvoker invoker = new CommandInvoker();
        private NoteBuilder noteBuilder = new NoteBuilder();
        private FileHandler fileHandler;

        public AddNoteWindow(List<Note> notes, FileHandler fileHandler, ICategory categories, ITag tags)
        {
            InitializeComponent();
            this.notes = notes;
            this.fileHandler = fileHandler;
            LoadCategoriesToComboBox(categories);
            LoadTagsToComboBox(tags);
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            string title = NoteTitleTextBox.Text;
            string description = NoteDescriptionTextBox.Text;
            var selectedCategoryItem = (ComboBoxItem)CategoryComboBox.SelectedItem;
            var selectedCategory = selectedCategoryItem?.Tag as Category;
            var selectedTagItem = (ComboBoxItem)TagComboBox.SelectedItem;
            var selectedTag = selectedTagItem?.Tag as Tag;
            var notatka = noteBuilder
                .setTitle(title)
                .setDescription(description)
                .setTag(selectedTag ?? new Tag("Inna"))
                .setCategory(selectedCategory ?? new Category("Inna"))
                .build();
            notatka.Id = GetNextNoteId(notes);
            invoker.AddCommand(new AddNote(notes, notatka));
            invoker.Execute();
            fileHandler.SaveNotesToFile("notes.txt");
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public static int GetNextNoteId(List<Note> notes)
        {
            return notes.Any() ? notes.Max(t => t.Id) + 1 : 1;
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
    }
}
