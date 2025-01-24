using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;
using SuperZTP.Command;
using SuperZTP.Decorator;
using SuperZTP.Facade;
using SuperZTP.Model;
using SuperZTP.Stores;
using SuperZTP.Views;
using Task = SuperZTP.Model.Task;
using SuperZTP.Builder;

namespace SuperZTP.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public DisplayTasksViewModel DisplayTasksViewModel { get; } 
        public TaskDetailsViewModel TaskDetailsViewModel { get; }
        private readonly CommandInvoker _invoker;
        private readonly TaskFilterManager _filterManager;

        //Enums dla filtrów
        public IList<string> AvailableCategories { get; }
        public IList<string> AvailableTags { get; }
        
        // Komendy do stosowania filtrów
        public System.Windows.Input.ICommand ApplyTitleFilterCommand { get; }
        public System.Windows.Input.ICommand ApplyCategoryFilterCommand { get; }
        public System.Windows.Input.ICommand ApplyTagFilterCommand { get; }
        public System.Windows.Input.ICommand ApplyDueDateFilterCommand { get; }
        public System.Windows.Input.ICommand ClearFiltersCommand { get; }

        public System.Windows.Input.ICommand AddTaskCommand { get; }

        // Konstruktor
        public MenuViewModel(SelectedTaskStore _selectedTaskStore, TaskState taskState)
        {
            _invoker = new CommandInvoker();
            _filterManager = new TaskFilterManager();
            _filterManager.FilterChanged += OnFilterChanged;

            DisplayTasksViewModel = new DisplayTasksViewModel(_selectedTaskStore, taskState, _invoker, this);
            TaskDetailsViewModel = new TaskDetailsViewModel(_selectedTaskStore);

            AddTaskCommand = new RelayCommand(() => OpenAddTaskWindow(taskState));

            // Inicjalizacja dostępnych opcji filtrów
            AvailableCategories = taskState.Categories.Select(c => c.Name.TrimStart()).ToList();
            AvailableTags = taskState.Tags.Select(c => c.Name.TrimStart()).ToList();

            // Inicjalizacja komend filtrów
            ApplyTitleFilterCommand = new RelayCommand(() => _filterManager.ApplyTitleFilter(SelectedTitle));
            ApplyCategoryFilterCommand = new RelayCommand(() => _filterManager.ApplyCategoryFilter(SelectedCategory));
            ApplyTagFilterCommand = new RelayCommand(() => _filterManager.ApplyTagFilter(SelectedTag));
            ApplyDueDateFilterCommand = new RelayCommand(() => _filterManager.ApplyDueDateFilter(SelectedDueDate ?? DateTime.Now));
            ClearFiltersCommand = new RelayCommand(_filterManager.ClearFilters);
        }

        private void OpenAddTaskWindow(TaskState taskState)
        {
          
            AddTaskWindow addTaskWindow = new AddTaskWindow(taskState.Tasks, taskState.FileHandler, taskState.Categories, taskState.Tags);
            addTaskWindow.TaskAdded += DisplayTasksViewModel.RefreshTasks;
            addTaskWindow.ShowDialog();
            addTaskWindow.TaskAdded -= DisplayTasksViewModel.RefreshTasks;
            // proxy.ClearTaskCache();
        }

        private void OnFilterChanged(ITaskFilter filter)
        {
            FilterChanged?.Invoke(filter);
        }

        public event Action<ITaskFilter> FilterChanged;

        // Właściwości do przechowywania wybranych wartości filtrów
        private string _selectedTitle;
        public string SelectedTitle
        {
            get => _selectedTitle;
            set
            {
                _selectedTitle = value;
                OnPropertyChanged(nameof(SelectedTitle));
            }
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        private Tag _selectedTag;
        public Tag SelectedTag
        {
            get => _selectedTag;
            set
            {
                _selectedTag = value;
                OnPropertyChanged(nameof(SelectedTag));
            }
        }

        private DateTime? _selectedDueDate;
        public DateTime? SelectedDueDate
        {
            get => _selectedDueDate;
            set
            {
                _selectedDueDate = value;
                OnPropertyChanged(nameof(SelectedDueDate));
            }
        }
    }
}
