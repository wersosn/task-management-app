﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperZTP.Command;
using SuperZTP.Decorator;
using SuperZTP.Facade;
using SuperZTP.Model;
using SuperZTP.Stores;
using SuperZTP.Views;
using SuperZTP.Resources;
using Task = SuperZTP.Model.Task;
using SuperZTP.Builder;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using SuperZTP.TemplateMethod;
using System.Reflection.Metadata.Ecma335;
using System.Windows;
using DocumentFormat.OpenXml.Office2019.Drawing.Model3D;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using DocumentFormat.OpenXml.Office.PowerPoint.Y2022.M08.Main;
using System.Net.Sockets;

namespace SuperZTP.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public DisplayTasksViewModel DisplayTasksViewModel { get; }
        public TaskDetailsViewModel TaskDetailsViewModel { get; }
        public NoteDetailsViewModel NoteDetailsViewModel { get; }
        private readonly CommandInvoker _invoker;
        private readonly TaskFilterManager _filterManager;
        private readonly SelectedTaskStore _selectedTaskStore;
        private readonly TaskState taskState;
        private bool _isHistoryVisible;
        public GenerateTXT txt;
        public GeneratePDF pdf;
        public GenerateDOCX docx;

        public TaskState GetTaskState() => taskState;
        public SelectedTaskStore GetSelectedTaskStore() => _selectedTaskStore;
        public ObservableCollection<string> CommandHistory { get; } = new ObservableCollection<string>();

        // Konstruktor
        public MenuViewModel(SelectedTaskStore _selectedTaskStore, TaskState taskState)
        {
            _invoker = new CommandInvoker();
            _filterManager = new TaskFilterManager();
            _filterManager.FilterChanged += OnFilterChanged;
            this._selectedTaskStore = _selectedTaskStore;
            this.taskState = taskState;
            txt = new GenerateTXT(taskState.Tasks);
            pdf = new GeneratePDF(taskState.Tasks);
            docx = new GenerateDOCX(taskState.Tasks);

            DisplayTasksViewModel = new DisplayTasksViewModel(_selectedTaskStore, taskState, _invoker, this);
            TaskDetailsViewModel = new TaskDetailsViewModel(_selectedTaskStore);
            NoteDetailsViewModel = new NoteDetailsViewModel(_selectedTaskStore);
            AddTaskCommand = new RelayCommand(() => OpenAddTaskWindow(taskState));
            AddNoteCommand = new RelayCommand(() => OpenAddNoteWindow(taskState));
            ToggleHistoryCommand = new RelayCommand(ToggleHistory);
            //SelectTaskCommand = new RelayCommand(SelectTask);
            //SelectNoteCommand = new RelayCommand(SelectNote);

            // Inicjalizacja dostępnych opcji filtrów
            AvailableCategories = taskState.Categories.Select(c => c.Name.TrimStart()).ToList();
            AvailableTags = taskState.Tags.Select(c => c.Name.TrimStart()).ToList();

            // Inicjalizacja komend filtrów
            ClearFiltersCommand = new RelayCommand(_filterManager.ClearFilters);
            ApplyAllFiltersCommand = new RelayCommand(ApplyAllFilters);
        }

        /*public RelayCommand SelectTaskCommand { get; }
        private void SelectTask()
        {
            TaskDetailsVisibility = Visibility.Visible;
            NoteDetailsVisibility = Visibility.Collapsed;
        }

        public RelayCommand SelectNoteCommand { get; }
        private void SelectNote()
        {
            NoteDetailsVisibility = Visibility.Visible;
            TaskDetailsVisibility = Visibility.Collapsed;
        }

        private Visibility _isTaskSelected = Visibility.Collapsed;
        public Visibility TaskDetailsVisibility
        {
            get => _isTaskSelected;
            set
            {
                _isTaskSelected = value;
                OnPropertyChanged(nameof(TaskDetailsVisibility));
                if (_isTaskSelected == Visibility.Visible)
                {
                    NoteDetailsVisibility = Visibility.Collapsed;
                }
            }
        }

        private Visibility _isNoteSelected = Visibility.Collapsed;
        public Visibility NoteDetailsVisibility
        {
            get => _isNoteSelected;
            set
            {
                _isNoteSelected = value;
                OnPropertyChanged(nameof(TaskDetailsVisibility));
                if (_isNoteSelected == Visibility.Visible)
                {
                    TaskDetailsVisibility = Visibility.Collapsed;
                }
            }
        }*/
      
        public Visibility HistoryVisibility
        {
            get => _historyVisibility;
            set
            {
                _historyVisibility = value;
                OnPropertyChanged(nameof(HistoryVisibility));
            }
        }

        private Visibility _historyVisibility = Visibility.Collapsed;

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
        public System.Windows.Input.ICommand ToggleHistoryCommand { get; }

        private void OpenAddTaskWindow(TaskState taskState)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow(taskState.Tasks, taskState.FileHandler, taskState.Categories, taskState.Tags, this, _invoker);
            addTaskWindow.TaskAdded += DisplayTasksViewModel.RefreshTasks;
            addTaskWindow.ShowDialog();
            addTaskWindow.TaskAdded -= DisplayTasksViewModel.RefreshTasks;
            // proxy.ClearTaskCache();
        }

        private void OpenAddNoteWindow(TaskState taskState)
        {
            AddNoteWindow addNoteWindow = new AddNoteWindow(taskState.Notes, taskState.FileHandler, taskState.Categories, taskState.Tags, this, _invoker);
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

            MessageBox.Show(string.Format(Strings.ReportMessage, SelectedReportType));
        }

        public void GenerateSelectedSummary()
        {
            if (SelectedSummaryType == "TXT") txt.GenerateSummary("podsumowanieTXT.txt");
            if (SelectedSummaryType == "PDF") pdf.GenerateSummary("podsumowaniePDF.pdf");
            if (SelectedSummaryType == "DOCX") docx.GenerateSummary("podsumowanieDOCX.docx");

            MessageBox.Show(string.Format(Strings.SummaryMessage, SelectedSummaryType));
        }

        // Metody potrzebne do wyświetlania historii operacji:
        private void ToggleHistory()
        {
            HistoryVisibility = HistoryVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            OnPropertyChanged(nameof(HistoryVisibility));
        }

        public void UpdateHistory()
        {
            CommandHistory.Clear();
            var history = _invoker.GetLastFiveCommands().ToList();
            if (history == null || !history.Any())
            {
                return;
            }
            if (history.Any())
            {
                foreach (var command in history)
                {
                    CommandHistory.Add(command.ToString());
                }
            }
            OnPropertyChanged(nameof(CommandHistory));
        }

        // Zmiana języka aplikacji:
        public System.Windows.Input.ICommand ChangeLanguageToPolishCommand => new RelayCommand(() =>
        {
            LanguageManager.ChangeLanguage("pl");
            RestartUI();
        });

        public System.Windows.Input.ICommand ChangeLanguageToEnglishCommand => new RelayCommand(() =>
        {
            LanguageManager.ChangeLanguage("en");
            RestartUI();
        });

        private void RestartUI()
        {
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var currentWindow = Application.Current.MainWindow;
            if (currentWindow.DataContext is MenuViewModel oldViewModel)
            {
                var taskState = oldViewModel.GetTaskState();
                var selectedTaskStore = oldViewModel.GetSelectedTaskStore();

                var newWindow = new MainWindow(selectedTaskStore, taskState);
                Application.Current.MainWindow = newWindow;
                newWindow.Show();

                currentWindow.Close();
            }

            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }
}
