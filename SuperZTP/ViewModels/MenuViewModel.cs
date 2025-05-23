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
using ICommand = System.Windows.Input.ICommand;

namespace SuperZTP.ViewModels;

public class MenuViewModel : BaseViewModel
{
    public DisplayTasksViewModel DisplayTasksViewModel { get; }
    public TaskDetailsViewModel TaskDetailsViewModel { get; }
    public NoteDetailsViewModel NoteDetailsViewModel { get; }
    private readonly CommandInvoker _invoker;
    private readonly TaskFilterManager _filterManager;
    private readonly SelectedTaskStore _selectedTaskStore;
    private readonly TaskState _taskState;
    private readonly FileHandler _files;
    private bool TaskSelected => _selectedTaskStore != null ? true : false;
    public GenerateTXT Txt;
    public GeneratePDF Pdf;
    public GenerateDOCX Docx;
    public ICommand ShowAllTasksCommand { get; }

    public TaskState GetTaskState()
    {
        return _taskState;
    }

    public SelectedTaskStore GetSelectedTaskStore()
    {
        return _selectedTaskStore;
    }

    public ObservableCollection<string> CommandHistory { get; } = new();

    // Konstruktor
    public MenuViewModel(SelectedTaskStore selectedTaskStore, TaskState taskState)
    {
        ShowAllTasksCommand           = new RelayCommand(ShowAllTasks);
        _invoker                      = new CommandInvoker();
        _filterManager                = new TaskFilterManager();
        _filterManager.FilterChanged += OnFilterChanged;
        _selectedTaskStore            = selectedTaskStore;
        _taskState                    = taskState;
        _files                        = new FileHandler(taskState.Tasks, taskState.Notes, taskState.Categories, taskState.Tags);
        Txt                           = new GenerateTXT(taskState.Tasks);
        Pdf                           = new GeneratePDF(taskState.Tasks);
        Docx                          = new GenerateDOCX(taskState.Tasks);

        DisplayTasksViewModel         = new DisplayTasksViewModel(selectedTaskStore, taskState, _invoker, this);
        TaskDetailsViewModel          = new TaskDetailsViewModel(selectedTaskStore);
        NoteDetailsViewModel          = new NoteDetailsViewModel(selectedTaskStore);
        AddTaskCommand                = new RelayCommand(() => OpenAddTaskWindow(taskState));
        AddNoteCommand                = new RelayCommand(() => OpenAddNoteWindow(taskState));
        AddCategoryCommand            = new RelayCommand(() => OpenAddCategoryWindow(taskState));
        AddTagCommand                 = new RelayCommand(() => OpenAddTagWindow(taskState));
        ToggleHistoryCommand          = new RelayCommand(ToggleHistory);

        // Inicjalizacja dostępnych opcji filtrów
        AvailableCategories           = new ObservableCollection<string>(taskState.Categories.Select(c => c.Name.TrimStart()));
        AvailableTags                 = new ObservableCollection<string>(taskState.Tags.Select(t => t.Name.TrimStart()));

        // Inicjalizacja komend filtrów
        ClearFiltersCommand           = new RelayCommand(_filterManager.ClearFilters);
        ApplyAllFiltersCommand        = new RelayCommand(ApplyAllFilters);
    }

    private DateTime? _selectedCalendarDate;

    public DateTime? SelectedCalendarDate
    {
        get => _selectedCalendarDate;
        set
        {
            _selectedCalendarDate = value;
            OnPropertyChanged(nameof(SelectedCalendarDate));

            if (value != null)
                // Przekazujemy filtr tylko po dacie
                FilterChanged?.Invoke(new DateFilter(value.Value));
        }
    }

    private Visibility _isTaskSelected = Visibility.Collapsed;

    public Visibility TaskDetailsVisibility
    {
        get => _isTaskSelected;
        set
        {
            _isTaskSelected = value;
            OnPropertyChanged(nameof(TaskDetailsVisibility));
            if (_isTaskSelected == Visibility.Visible) NoteDetailsVisibility = Visibility.Collapsed;
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
            if (_isNoteSelected == Visibility.Visible) TaskDetailsVisibility = Visibility.Collapsed;
        }
    }

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
    public ObservableCollection<string> AvailableCategories { get; private set; }
    public ObservableCollection<string> AvailableTags { get; private set; }

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
    public ICommand ClearFiltersCommand    { get; }
    public ICommand ApplyAllFiltersCommand { get; }

    public ICommand AddTaskCommand         { get; }
    public ICommand AddNoteCommand         { get; }
    public ICommand AddCategoryCommand     { get; }
    public ICommand AddTagCommand          { get; }
    public ICommand ToggleHistoryCommand   { get; }

    private void OpenAddTaskWindow(TaskState taskState)
    {
        var addTaskWindow = new AddTaskWindow(taskState.Tasks, taskState.FileHandler, taskState.Categories,
            taskState.Tags, this, _invoker);
        addTaskWindow.TaskAdded += DisplayTasksViewModel.RefreshTasks;
        addTaskWindow.ShowDialog();
        addTaskWindow.TaskAdded -= DisplayTasksViewModel.RefreshTasks;
        // proxy.ClearTaskCache();
    }

    private void OpenAddNoteWindow(TaskState taskState)
    {
        var addNoteWindow = new AddNoteWindow(taskState.Notes, taskState.FileHandler, taskState.Categories,
            taskState.Tags, this, _invoker);
        addNoteWindow.NoteAdded += DisplayTasksViewModel.RefreshTasks;
        addNoteWindow.ShowDialog();
        addNoteWindow.NoteAdded -= DisplayTasksViewModel.RefreshTasks;
        // proxy.ClearTaskCache();
    }

    private void OpenAddCategoryWindow(TaskState taskState)
    {
        var addCategoryWindow = new AddCategoryWindow(taskState.Categories, taskState.FileHandler);
        addCategoryWindow.ShowDialog();
        RefreshAvailableCategories();
    }

    private void OpenAddTagWindow(TaskState taskState)
    {
        var addTagWindow = new AddTagWindow(taskState.Tags, taskState.FileHandler);
        addTagWindow.ShowDialog();
        RefreshAvailableTags();
    }

    private void OnFilterChanged(ITaskFilter filter)
    {
        FilterChanged?.Invoke(filter);
    }

    public event Action<ITaskFilter> FilterChanged;

    /// <summary>
    ///     Zastosowanie wszystkich wybranych filtrów jednocześnie
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
        if (SelectedReportType == "TXT") Txt.GenerateRaport("raportTXT.txt");
        if (SelectedReportType == "PDF") Pdf.GenerateRaport("raportPDF.pdf");
        if (SelectedReportType == "DOCX") Docx.GenerateRaport("raportDOCX.docx");

        MessageBox.Show(string.Format(Strings.ReportMessage, SelectedReportType));
    }

    public void GenerateSelectedSummary()
    {
        if (SelectedSummaryType == "TXT") Txt.GenerateSummary("podsumowanieTXT.txt");
        if (SelectedSummaryType == "PDF") Pdf.GenerateSummary("podsumowaniePDF.pdf");
        if (SelectedSummaryType == "DOCX") Docx.GenerateSummary("podsumowanieDOCX.docx");

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
        if (history == null || !history.Any()) return;
        if (history.Any())
            foreach (var command in history)
                CommandHistory.Add(command);

        OnPropertyChanged(nameof(CommandHistory));
    }

    // Zmiana języka aplikacji:
    public ICommand ChangeLanguageToPolishCommand => new RelayCommand(() =>
    {
        LanguageManager.ChangeLanguage("pl");
        RestartUi();
    });

    public ICommand ChangeLanguageToEnglishCommand => new RelayCommand(() =>
    {
        LanguageManager.ChangeLanguage("en");
        RestartUi();
    });

    private void RestartUi()
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

    private void ShowAllTasks()
    {
        _filterManager.ClearFilters(); // tylko czyści
        DisplayTasksViewModel.ClearFilter(); // resetuje filtr w ViewModelu
        DisplayTasksViewModel.RefreshTasks(); // ręcznie odśwież widok
    }

    // Restart filtrów po dodaniu nowej kategorii/tagu:
    private void RefreshAvailableCategories()
    {
        AvailableCategories.Clear();
        foreach (var category in _taskState.Categories.Select(c => c.Name.TrimStart()))
            AvailableCategories.Add(category);
        OnPropertyChanged(nameof(AvailableCategories));
    }

    private void RefreshAvailableTags()
    {
        AvailableTags.Clear();
        foreach (var tag in _taskState.Tags.Select(t => t.Name.TrimStart()))
            AvailableTags.Add(tag);
        OnPropertyChanged(nameof(AvailableTags));
    }
}