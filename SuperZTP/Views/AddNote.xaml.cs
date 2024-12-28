using SuperZTP.Builder;
using SuperZTP.Command;
using SuperZTP.Model;
using SuperZTP.Composite;
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
    /// Logika interakcji dla klasy AddNote.xaml
    /// </summary>
    public partial class AddNote : Window
    {
        private List<Note> notes;
        private CommandInvoker invoker = new CommandInvoker();
        private NoteBuilder noteBuilder = new NoteBuilder();
        private FileHandler fileHandler;
        private ICategory rootCategory;
        private ITag rootTag;

        public AddNote(List<Note> notes, FileHandler fileHandler)
        {
            InitializeComponent();
            this.notes = notes;
            this.fileHandler = fileHandler;

            // Kategorie:
            rootCategory = new Category("Kategoria");
            var work = new Category("Praca");
            var personal = new Category("Osobiste");
            work.Dodaj(new SubCategory("Spotkania"));
            work.Dodaj(new SubCategory("Raporty"));
            personal.Dodaj(new SubCategory("Zakupy"));
            personal.Dodaj(new SubCategory("Siłownia"));
            rootCategory.Dodaj(work);
            rootCategory.Dodaj(personal);
            LoadCategories();

            // Tagi:
            rootTag = new Tag("Tag");
            var education = new Tag("Edukacja");
            education.Dodaj(new SubTag("Kursy"));
            education.Dodaj(new SubTag("Studia"));
            education.Dodaj(new SubTag("Samorozwój"));
            rootTag.Dodaj(education);
            LoadTags();
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            string title = NoteTitleTextBox.Text;
            string description = NoteDescriptionTextBox.Text;
            var selectedCategoryItem = (ComboBoxItem)CategoryComboBox.SelectedItem;
            var selectedCategory = selectedCategoryItem?.Tag?.ToString() ?? "Inne";
            var selectedTagItem = (ComboBoxItem)TagComboBox.SelectedItem;
            var selectedTag = selectedTagItem?.Tag?.ToString() ?? "Inne";
            var notatka = noteBuilder
                .setTytul(title)
                .setOpis(description)
                .setTagi(new Tag(selectedTag))
                .setKategorie(new Category(selectedCategory))
                .build();
            notatka.Id = GetNextNoteId(notes);
            invoker.DodajOperacje(new DodajNotatke(notes, notatka));
            invoker.Wykonaj();
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

        // Wczytanie kategorii
        private void LoadCategories()
        {
            var categoryList = FlattenCategories(rootCategory);
            foreach (var (displayName, lastName) in categoryList)
            {
                CategoryComboBox.Items.Add(new ComboBoxItem
                {
                    Content = displayName,
                    Tag = lastName
                });
            }
        }

        // Metoda pomocnicza do spłaszczenia hierarchii
        private List<(string DisplayName, string LastName)> FlattenCategories(ICategory root)
        {
            var categories = new List<(string, string)>();
            void Traverse(ICategory category, string prefix)
            {
                var fullName = string.IsNullOrEmpty(prefix) ? category.Name : $"{prefix} > {category.Name}";
                categories.Add((fullName, category.Name));

                foreach (var child in category.GetChildren())
                {
                    Traverse(child, fullName);
                }
            }
            Traverse(root, "");
            return categories;
        }

        // Wczytanie tagów
        private void LoadTags()
        {
            var tagList = FlattenTags(rootTag);
            foreach (var (displayName, lastName) in tagList)
            {
                TagComboBox.Items.Add(new ComboBoxItem
                {
                    Content = displayName,
                    Tag = lastName
                });
            }
        }

        // Metoda pomocnicza do spłaszczenia hierarchii
        private List<(string DisplayName, string LastName)> FlattenTags(ITag root)
        {
            var tags = new List<(string, string)>();
            void Traverse(ITag tag, string prefix)
            {
                var fullName = string.IsNullOrEmpty(prefix) ? tag.Name : $"{prefix} > {tag.Name}";
                tags.Add((fullName, tag.Name));

                foreach (var child in tag.GetChildren())
                {
                    Traverse(child, fullName);
                }
            }
            Traverse(root, "");
            return tags;
        }
    }
}
