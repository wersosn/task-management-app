using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;
using SuperZTP.Command;
using SuperZTP.Facade;
using SuperZTP.Model;
using SuperZTP.Stores;
using SuperZTP.Views;
using Task = SuperZTP.Model.Task;

namespace SuperZTP.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public DisplayTasksViewModel DisplayTasksViewModel { get; } 
        public TaskDetailsViewModel TaskDetailsViewModel { get; }
        private readonly CommandInvoker _invoker;
        public System.Windows.Input.ICommand AddTaskCommand { get; }

        public MenuViewModel(SelectedTaskStore _selectedTaskStore, TaskState taskState)
        {
            _invoker = new CommandInvoker();
            DisplayTasksViewModel = new DisplayTasksViewModel(_selectedTaskStore, taskState.Tasks, _invoker);
            TaskDetailsViewModel = new TaskDetailsViewModel(_selectedTaskStore);

            AddTaskCommand = new RelayCommand(() => OpenAddTaskWindow(taskState));
        }

        private void OpenAddTaskWindow(TaskState taskState)
        {
          
            AddTaskWindow addTaskWindow = new AddTaskWindow(taskState.Tasks, taskState.FileHandler, taskState.Categories, taskState.Tags);
            addTaskWindow.TaskAdded += DisplayTasksViewModel.RefreshTasks;
            addTaskWindow.ShowDialog();
            addTaskWindow.TaskAdded -= DisplayTasksViewModel.RefreshTasks;
            // proxy.ClearTaskCache();
        }
    }
}
