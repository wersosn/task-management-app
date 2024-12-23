using SuperZTP.Controller;
using SuperZTP.Model;
using SuperZTP.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperZTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTask addTask = new AddTask();
            addTask.Show();
        }

        public void AddNoteButton_Click(object sender, RoutedEventArgs e) 
        {
            AddNote addNote = new AddNote();
            addNote.Show();
        }
    }
}