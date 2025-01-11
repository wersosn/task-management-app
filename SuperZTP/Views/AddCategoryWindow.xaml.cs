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
    /// Logika interakcji dla klasy AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        private List<Category> categories;
        private FileHandler fileHandler;
        private CommandInvoker invoker = new CommandInvoker();

        public AddCategoryWindow(List<Category> categories, FileHandler fileHandler)
        {
            InitializeComponent();
            this.categories = categories;
            this.fileHandler = fileHandler;
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = CategoryNameTextBox.Text.Trim();
            if (categories.Any(cat => cat.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Kategoria o tej nazwie już istnieje.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var newCategory = new Category(categoryName);
            invoker.AddCommand(new AddCategory(categories, newCategory));
            invoker.Execute();
            fileHandler.SaveCategoriesToFile("categories.txt");
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
