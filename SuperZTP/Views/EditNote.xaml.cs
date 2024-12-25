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
    public partial class EditNote : Window
    {
        public Note EditedNote { get; set; }

        public EditNote(Note noteToEdit)
        {
            InitializeComponent();
            NoteTitleTextBox.Text = noteToEdit.Title;
            NoteDescriptionTextBox.Text = noteToEdit.Description;
            EditedNote = noteToEdit;
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            EditedNote.Title = NoteTitleTextBox.Text;
            EditedNote.Description = NoteDescriptionTextBox.Text;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
