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
using SuperZTP.Command;
using SuperZTP.Model;

namespace SuperZTP.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AddTagWindow.xaml
    /// </summary>
    public partial class AddTagWindow : Window
    {
        private List<Tag> tags;
        private FileHandler fileHandler;
        private CommandInvoker invoker = new CommandInvoker();

        public AddTagWindow(List<Tag> tags, FileHandler fileHandler)
        {
            InitializeComponent();
            this.tags = tags;
            this.fileHandler = fileHandler;
        }

        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            string tagName = TagNameTextBox.Text.Trim();
            if (tags.Any(tag => tag.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Tag o tej nazwie już istnieje.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var newTag = new Tag(tagName);
            invoker.AddCommand(new AddTag(tags, newTag));
            invoker.Execute();
            fileHandler.SaveTagsToFile("tags.txt");
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
