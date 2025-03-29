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
using SuperZTP.TemplateMethod;
using System.Reflection.Metadata.Ecma335;
using System.Windows;

namespace SuperZTP.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public DisplayTasksViewModel DisplayTasksViewModel { get; }
        public TaskDetailsViewModel TaskDetailsViewModel { get; }
        public NoteDetailsViewModel NoteDetailsViewModel { get; }
        private readonly CommandInvoker _invoker;
        private readonly TaskFilterManager _filterManager;
        public GenerateTXT txt;
        public GeneratePDF pdf;
        public GenerateDOCX docx;

        //Enums dla filtrów
        public IList<string> AvailableCategories { get; }
        public IList<string> AvailableTags { get; }
        public IList<GroupingOption> AvailableGroups { get; } = new List<GroupingOption>
        {
            GroupingOption.NoGroup,
            GroupingOption.GroupByCategory,
            GroupingOption.GroupByTag
        };

        public IList<CompletionStatus> AvailableCompletionStatusList { get; } = new List<CompletionStatus>
        {
            CompletionStatus.Default,
            CompletionStatus.Completed,
            CompletionStatus.NotCompleted,
            CompletionStatus.ShowAll
        };

        public IList<SortOptions> AvailableSortOptionsList { get; } =
            Enum.GetValues(typeof(SortOptions))
                .Cast<SortOptions>()
                .ToList();
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
        public System.Windows.Input.ICommand AddNoteCommand { get; }


        // Konstruktor
        public MenuViewModel(SelectedTaskStore _selectedTaskStore, TaskState taskState)
        {
            _invoker = new CommandInvoker();
            _filterManager = new TaskFilterManager();
            _filterManager.FilterChanged += OnFilterChanged;
            txt = new GenerateTXT(taskState.Tasks);
            pdf = new GeneratePDF(taskState.Tasks);
            docx = new GenerateDOCX(taskState.Tasks);

            DisplayTasksViewModel = new DisplayTasksViewModel(_selectedTaskStore, taskState, _invoker, this);
            TaskDetailsViewModel = new TaskDetailsViewModel(_selectedTaskStore);
            NoteDetailsViewModel = new NoteDetailsViewModel(_selectedTaskStore);
            AddTaskCommand = new RelayCommand(() => OpenAddTaskWindow(taskState));
            AddNoteCommand = new RelayCommand(() => OpenAddNoteWindow(taskState));

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
        private void OpenAddNoteWindow(TaskState taskState)
        {

            AddNoteWindow addNoteWindow = new AddNoteWindow(taskState.Notes, taskState.FileHandler, taskState.Categories, taskState.Tags);
            addNoteWindow.NoteAdded += DisplayTasksViewModel.RefreshTasks;
            addNoteWindow.ShowDialog();
            addNoteWindow.NoteAdded -= DisplayTasksViewModel.RefreshTasks;
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
            _filterManager.ClearFilters();
            _filterManager.ApplyTitleFilter(SelectedTitle);
            _filterManager.ApplyCategoryFilter(SelectedCategory);
            _filterManager.ApplyTagFilter(SelectedTag);
            _filterManager.ApplyDueDateFilter(SelectedDueDate);
            _filterManager.ApplayCompletionFilter(SelectedCompletionStatus);
            _filterManager.ApllySorting(SelectedSort);
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

        private SortOptions _selectedSort;
        public SortOptions SelectedSort
        {
            get => _selectedSort;
            set
            {
                _selectedSort = value;
                OnPropertyChanged(nameof(SelectedSort));
            }
        }

        private string _selectedTag;
        public string SelectedTag
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

        private CompletionStatus _selectedCompletionStatus;
        public CompletionStatus SelectedCompletionStatus
        {
            get => _selectedCompletionStatus;
            set
            {
                _selectedCompletionStatus = value;
                OnPropertyChanged(nameof(SelectedCompletionStatus));
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

        // Opcje raportów
        public IList<string> AvailableReportTypes { get; } = new List<string>
    {
        "TXT",
        "PDF",
        "DOCX"
    };

        private string _selectedReportType;
        public string SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                _selectedReportType = value;
                OnPropertyChanged(nameof(SelectedReportType));
            }
        }

        // Opcje podsumowań
        public IList<string> AvailableSummaryTypes { get; } = new List<string>
    {
        "TXT",
        "PDF",
        "DOCX"
    };

        private string _selectedSummaryType;
        public string SelectedSummaryType
        {
            get => _selectedSummaryType;
            set
            {
                _selectedSummaryType = value;
                OnPropertyChanged(nameof(SelectedSummaryType));
            }
        }

        // Metody do generowania raportów
        public void GenerateSelectedReport()
        {
            if (SelectedReportType == "TXT") txt.GenerateRaport("raportTXT.txt");
            if (SelectedReportType == "PDF") pdf.GenerateRaport("raportPDF.pdf");
            if (SelectedReportType == "DOCX") docx.GenerateRaport("raportDOCX.docx");

            MessageBox.Show($"Wygenerowano raport w formacie {SelectedReportType}");
        }

        public void GenerateSelectedSummary()
        {
            if (SelectedSummaryType == "TXT") txt.GenerateSummary("podsumowanieTXT.txt");
            if (SelectedSummaryType == "PDF") pdf.GenerateSummary("podsumowaniePDF.pdf");
            if (SelectedSummaryType == "DOCX") docx.GenerateSummary("podsumowanieDOCX.docx");

            MessageBox.Show($"Wygenerowano podsumowanie w formacie {SelectedSummaryType}");
        }
    }
}
