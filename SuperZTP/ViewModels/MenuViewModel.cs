using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.EMMA;
using SuperZTP.Command;
using SuperZTP.Decorator;
using SuperZTP.Facade;
using SuperZTP.Model;
using SuperZTP.Stores;
using SuperZTP.Views;
using Task = SuperZTP.Model.Task;
using SuperZTP.Builder;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

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
        public IList<GroupingOption> AvailableGroups { get; } = new List<GroupingOption>
        {
            GroupingOption.NoGroup,
            GroupingOption.GroupByCategory,
            GroupingOption.GroupByTag
        };
        public static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute != null ? attribute.Description : value.ToString();
        }
       
        // Komendy do stosowania filtrów
        public System.Windows.Input.ICommand ClearFiltersCommand { get; }
        public System.Windows.Input.ICommand ApplyAllFiltersCommand { get; }

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
            ClearFiltersCommand = new RelayCommand(_filterManager.ClearFilters);
            ApplyAllFiltersCommand = new RelayCommand(ApplyAllFilters);
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

        /// <summary>
        /// Zastosowanie wszystkich wybranych filtrów jednocześnie
        /// </summary>
        private void ApplyAllFilters()
        {
            _filterManager.ApplyTitleFilter(SelectedTitle);
            _filterManager.ApplyCategoryFilter(SelectedCategory);
            _filterManager.ApplyTagFilter(SelectedTag);
            _filterManager.ApplyDueDateFilter(SelectedDueDate);
            _filterManager.ApplayGroupFilter(SelectedGroupingOption);
        }

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
        private GroupingOption _selectedGroupingOption;
        public GroupingOption SelectedGroupingOption
        {
            get => _selectedGroupingOption;
            set
            {
                _selectedGroupingOption = value;
                OnPropertyChanged(nameof(SelectedGroupingOption));
            }
        }
    }
}
