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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperZTP.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        private DateTime previousDisplayDate;

        public MenuView()
        {
            InitializeComponent();
            previousDisplayDate = CalendarView.DisplayDate;
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MenuViewModel;
            if (viewModel != null)
            {
                viewModel.GenerateSelectedReport();
            }
        }

        private void GenerateSummaryButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MenuViewModel;
            if (viewModel != null)
            {
                viewModel.GenerateSelectedSummary();
            }
        }

        private void TaskDetailsView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CalendarView_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            var currentDisplayDate = DateTime.Now;

            double animationTo = currentDisplayDate > previousDisplayDate ? -10 : 10; 

            DoubleAnimation pageChangeAnimation = new DoubleAnimation
            {
                From = 0,
                To = animationTo,
                Duration = TimeSpan.FromSeconds(0.3),
                AutoReverse = true
            };
            CalendarTranslateTransform.BeginAnimation(TranslateTransform.XProperty, pageChangeAnimation);
            previousDisplayDate = currentDisplayDate;
        }
    }
}
