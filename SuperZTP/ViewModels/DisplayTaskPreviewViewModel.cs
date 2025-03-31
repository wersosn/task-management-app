using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using SuperZTP.Command;
using SuperZTP.Facade;
using SuperZTP.Model;
using SuperZTP.Views;
using Task = SuperZTP.Model.Task;

namespace SuperZTP.ViewModels
{
    public class DisplayTaskPreviewViewModel : BaseViewModel
    {
        private readonly List<Model.Task> _tasks;
        private readonly List<Model.Note> _notes;
        private readonly TaskState _taskState;
        private readonly CommandInvoker _invoker;
        private readonly Action _refreshMenu;
        private MenuViewModel _viewModel;

        public Model.Task Task { get; }
        public Model.Note Note { get; }
        public Model.Header Header { get; }
        public string Title => Task?.Title ?? Note?.Title ?? Header.Title;
        public System.Windows.Input.ICommand EditCommand { get; }
        public System.Windows.Input.ICommand DeleteCommand { get; }
        public bool IsTask => Task != null;
        public bool IsNote => Note != null;
        public bool IsHeader => Header != null;
        public DisplayTaskPreviewViewModel(Task task, TaskState state, CommandInvoker invoker, Action refreshMenu, MenuViewModel viewModel)
        {
            Task = task;
            _tasks = state.Tasks;
            _notes = state.Notes;
            _taskState = state;
            _invoker = invoker;
            _refreshMenu = refreshMenu;
            _viewModel = viewModel;

            DeleteCommand = new RelayCommand(DeleteTaskCommand);
            EditCommand = new RelayCommand(EditTaskCommand);
        }
        public DisplayTaskPreviewViewModel(Note note, TaskState state, CommandInvoker invoker, Action refreshMenu)
        {
            Note = note;
            _tasks = state.Tasks;
            _notes = state.Notes;
            _taskState = state;
            _invoker = invoker;
            _refreshMenu = refreshMenu;

            DeleteCommand = new RelayCommand(DeleteTaskCommand);
            EditCommand = new RelayCommand(EditTaskCommand);
        }

        public DisplayTaskPreviewViewModel(Header header, TaskState state, CommandInvoker invoker, Action refreshMenu)
        {
            Header = header;
            _tasks = state.Tasks;
            _notes = state.Notes;
            _taskState = state;
            _invoker = invoker;
            _refreshMenu = refreshMenu;

            DeleteCommand = new RelayCommand(DeleteTaskCommand);
            EditCommand = new RelayCommand(EditTaskCommand);
        }
        private void DeleteTaskCommand()
        {
            _invoker.AddCommand(new DeleteTask(_tasks, Task, _refreshMenu));
            _invoker.Execute();
            _taskState.FileHandler.SaveTasksToFile("tasks.txt");
        }

        private void EditTaskCommand()
        {
            var editTaskWindow = new EditTaskWindow(Task, _taskState.FileHandler, _taskState.Categories, _taskState.Tags, _invoker, _viewModel, _tasks);

            if (editTaskWindow.ShowDialog() == true)
            {
                var editedTask = editTaskWindow.EditedTask;
                var taskIndex = _taskState.Tasks.FindIndex(t => t.Id == editedTask.Id);

                if (taskIndex >= 0)
                {
                    _taskState.Tasks[taskIndex] = editedTask;
                    _invoker.AddCommand(new EditTask(_tasks, Task, editedTask, editedTask.Id));
                    _invoker.Execute();
                }

                _refreshMenu.Invoke();
            }
        }
    }
}
