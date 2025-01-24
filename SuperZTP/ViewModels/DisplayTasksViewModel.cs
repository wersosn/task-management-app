using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SuperZTP.Command;
using SuperZTP.Decorator;
using SuperZTP.Facade;
using SuperZTP.Model;
using SuperZTP.Stores;
using Task = SuperZTP.Model.Task;

namespace SuperZTP.ViewModels
{
    public class DisplayTasksViewModel : BaseViewModel
    {
        private readonly SelectedTaskStore _selectedTaskStore;
        private readonly TaskState _taskState;
        private readonly CommandInvoker _invoker;
        private readonly ObservableCollection<DisplayTaskPreviewViewModel> _previews;
        public IEnumerable<DisplayTaskPreviewViewModel> Previews => _previews;

        private DisplayTaskPreviewViewModel _selectedTaskViewModel;
        public DisplayTaskPreviewViewModel SelectedTaskViewModel
        {
            get => _selectedTaskViewModel;
            set
            {
                _selectedTaskViewModel = value;
                OnPropertyChanged(nameof(SelectedTaskViewModel));
                _selectedTaskStore.SelectedTask = _selectedTaskViewModel?.Task;
            }
        }
        private ITaskFilter _currentFilter;
        public DisplayTasksViewModel(SelectedTaskStore selectedTaskStore, TaskState state, CommandInvoker invoker,MenuViewModel menuViewModel)
        {
            _selectedTaskStore = selectedTaskStore;
            _taskState = state;
            _invoker = invoker;
            _previews = new ObservableCollection<DisplayTaskPreviewViewModel>();
            menuViewModel.FilterChanged += ApplyFilter;

            RefreshTasks();
        }

        /// Odświeża listę tasków w `Previews` zgodnie z aktywnym filtrem.
        public void RefreshTasks()
        {
            _previews.Clear();
            IEnumerable<Task> filteredTasks = _currentFilter?.ApplyFilter(_taskState.Tasks) ?? _taskState.Tasks;
            foreach (var task in filteredTasks)
            {
                _previews.Add(new DisplayTaskPreviewViewModel(task, _taskState, _invoker, RefreshTasks));
            }
            OnPropertyChanged(nameof(Previews));
        }

        /// <summary>
        /// Stosuje aktywny filtr.
        /// </summary>
        private void ApplyFilter(ITaskFilter filter)
        {
            _currentFilter = filter;
            RefreshTasks();
        }
    }
}
