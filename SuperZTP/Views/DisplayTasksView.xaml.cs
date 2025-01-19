using DocumentFormat.OpenXml.Office2021.DocumentTasks;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task = SuperZTP.Model.Task;

namespace SuperZTP.Views
{
    /// <summary>
    /// Interaction logic for DisplayTasksView.xaml
    /// </summary>
    public partial class DisplayTasksView : UserControl
    {
        private List<Model.Task> _tasks = [];
        public DisplayTasksView()
        {
            InitializeComponent();
        }

        public void SetTasks(List<Model.Task> taskList)
        {
            _tasks = taskList;
        }
    }
}
