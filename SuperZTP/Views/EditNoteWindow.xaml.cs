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

namespace SuperZTP.Views
{
    /// <summary>
    /// Logika interakcji dla klasy EditNote.xaml
    /// </summary>
    public partial class EditNoteWindow : Window
    {
        public Note EditedNote { get; set; }
        private Note originalNote;
        private FileHandler fileHandler;
        private CommandInvoker invoker;
        private MenuViewModel _viewModel;
        private List<Note> notes;

        public EditNoteWindow(Note noteToEdit, FileHandler fileHandler, List<Category> categories, List<Tag> tags, CommandInvoker invoker, MenuViewModel _viewModel, List<Note> notes)
        {
            InitializeComponent();

            this.notes = notes;
            this.fileHandler = fileHandler;
            this.invoker = invoker;
            this._viewModel = _viewModel;

            originalNote = new Note
            {
                Id = noteToEdit.Id,
                Title = noteToEdit.Title,
                Description = noteToEdit.Description,
                Tag = noteToEdit.Tag,
                Category = noteToEdit.Category
            };

            EditedNote = new Note
            {
                Id = noteToEdit.Id,
                Title = noteToEdit.Title,
                Description = noteToEdit.Description,
                Tag = noteToEdit.Tag,
                Category = noteToEdit.Category
            };

            NoteTitleTextBox.Text = EditedNote.Title;
            NoteDescriptionTextBox.Text = EditedNote.Description;
            LoadCategoriesToComboBox(categories);
            SelectCategoryInComboBox(EditedNote.Category);
            LoadTagsToComboBox(tags);
            SelectTagInComboBox(EditedNote.Tag);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NoteTitleTextBox.Text != "")
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

                var noteIndex = notes.FindIndex(n => n.Id == EditedNote.Id);
                if (noteIndex >= 0)
                {
                    notes[noteIndex] = EditedNote;
                }

                fileHandler.SaveNotesToFile("notes.txt");
                var editCommand = new EditNote(notes, originalNote, EditedNote, EditedNote.Id);
                invoker.AddCommand(editCommand);
                invoker.Execute();
                _viewModel.UpdateHistory();
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Tytuł jest wymagany!");
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
