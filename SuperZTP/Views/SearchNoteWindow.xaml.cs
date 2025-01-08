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
    /// Logika interakcji dla klasy SearchNoteWindow.xaml
    /// </summary>
    public partial class SearchNoteWindow : Window
    {

        public string Keyword { get; private set; }

        public SearchNoteWindow()
        {
            InitializeComponent();
        }

        private void Confirm_Button(object sender, RoutedEventArgs e)
        {
            Keyword = InputOfUser.Text;
            DialogResult = true;

        }

        private void CancelSearch_Button(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
