using System.Configuration;
using System.Data;
using System.Windows;
using SuperZTP.Command;
using SuperZTP.Facade;
using SuperZTP.Model;
using SuperZTP.Stores;
using SuperZTP.TemplateMethod;
using SuperZTP.ViewModels;
using SuperZTP.Views;

namespace SuperZTP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly SelectedTaskStore _selectedTaskStore;
        private List<Model.Task> tasks = new List<Model.Task>();
        private List<Note> notes = new List<Note>();
        private List<Category> categories = new List<Category>();
        private List<Tag> tags = new List<Tag>();
        private FileHandler fileHandler;
        private GroupTasks GroupTasks = new GroupTasks();
        private GroupNotes GroupNotes = new GroupNotes();
        private SortTasks SortTasks = new SortTasks();
        private SortNotes SortNotes = new SortNotes();
        private GenerateTXT txt;
        private GeneratePDF pdf;
        private GenerateDOCX docx;
        private SuperZTP.Proxy.Proxy proxy;
        private DisplayTasksView _displayTaskView = new DisplayTasksView();
        private TaskState taskState;

        public App()
        {
            InitializeComponent();
            fileHandler = new FileHandler(tasks, notes, categories, tags);
            fileHandler.LoadTasksFromFile("tasks.txt");
            fileHandler.LoadNotesFromFile("notes.txt");
            fileHandler.LoadCategoriesFromFile("categories.txt");
            fileHandler.LoadTagsFromFile("tags.txt");
            taskState = new TaskState(tasks, notes, categories, tags, fileHandler);
            _selectedTaskStore = new SelectedTaskStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow(_selectedTaskStore, taskState)
            {
                DataContext = new MenuViewModel(_selectedTaskStore, taskState)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
