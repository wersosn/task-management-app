using SuperZTP.Model;
using SuperZTP.TemplateMethod;
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

        public EditNoteWindow(Note noteToEdit, FileHandler fileHandler)
        {
            InitializeComponent();
            NoteTitleTextBox.Text = noteToEdit.Title;
            NoteDescriptionTextBox.Text = noteToEdit.Description;
            EditedNote = noteToEdit;
            this.fileHandler = fileHandler;
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            EditedNote.Title = NoteTitleTextBox.Text;
            EditedNote.Description = NoteDescriptionTextBox.Text;
            fileHandler.SaveNotesToFile("notes.txt");
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
