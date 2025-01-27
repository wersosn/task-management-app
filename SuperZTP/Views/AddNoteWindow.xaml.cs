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
    /// Logika interakcji dla klasy AddNote.xaml
    /// </summary>
    public partial class AddNoteWindow : Window
    {
        private List<Note> notes;
        private CommandInvoker invoker = new CommandInvoker();
        private NoteBuilder noteBuilder = new NoteBuilder();
        private FileHandler fileHandler;
        public event Action NoteAdded;

        public AddNoteWindow(List<Note> notes, FileHandler fileHandler, List<Category> categories, List<Tag> tags)
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

            invoker.AddCommand(new AddNote(notes, notatka, RefreshNotes));
            invoker.Execute();
            fileHandler.SaveNotesToFile("notes.txt");
            DialogResult = true;
        }
        private void RefreshNotes()
        {
            NoteAdded?.Invoke(); // Powiadamiamy `DisplayTasksViewModel`
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public static int GetNextNoteId(List<Note> notes)
        {
            return notes.Any() ? notes.Max(n => n.Id) + 1 : 1;
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
    }
}
