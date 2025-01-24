using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly TaskState _taskState;
        private readonly CommandInvoker _invoker;
        private readonly Action _refreshMenu;

        public Model.Task Task { get; }
        public string Title => Task.Title;
        public System.Windows.Input.ICommand EditCommand { get; }
        public System.Windows.Input.ICommand DeleteCommand { get; }
        public bool IsHeader { get; set; } = false;
        public DisplayTaskPreviewViewModel(Task task, TaskState state, CommandInvoker invoker, Action refreshMenu)
        {
            Task = task;
            _tasks = state.Tasks;
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
            var editTaskWindow = new EditTaskWindow(Task, _taskState.FileHandler, _taskState.Categories, _taskState.Tags);
            if (editTaskWindow.ShowDialog() == true)
            {
                var editedTask = editTaskWindow.EditedTask;
                var taskIndex = _taskState.Tasks.FindIndex(t => t.Id == editedTask.Id);
                if (taskIndex >= 0)
                {
                    _taskState.Tasks[taskIndex] = editedTask;
                }
                _refreshMenu.Invoke();
            }
            //proxy.ClearTaskCache();
        }

    }
}
