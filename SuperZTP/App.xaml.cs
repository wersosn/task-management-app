using System.Configuration;
using System.Data;
using System.Windows;
using SuperZTP.Stores;
using SuperZTP.ViewModels;

namespace SuperZTP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly SelectedTaskStore _selectedTaskStore;

        public App()
        {
            _selectedTaskStore = new SelectedTaskStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MenuViewModel(_selectedTaskStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
