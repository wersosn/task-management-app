using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Command;
using SuperZTP.Model;
using Task = SuperZTP.Model.Task;

namespace SuperZTP.ViewModels
{
    public class DisplayTaskPreviewViewModel : BaseViewModel
    {
        private readonly List<Model.Task> _tasks;
        private readonly CommandInvoker _invoker;
        private readonly Action _onTaskDeleted;

        public Model.Task Task { get; }
        public string Title => Task.Title;
        public System.Windows.Input.ICommand EditCommand { get; }
        public System.Windows.Input.ICommand DeleteCommand { get; }
        public DisplayTaskPreviewViewModel(Task task, List<Task> tasks, CommandInvoker invoker, Action onTaskDeleted)
        {
            Task = task;
            _tasks = tasks;
            _invoker = invoker;
            _onTaskDeleted = onTaskDeleted;

            DeleteCommand = new RelayCommand(DeleteTaskCommand);
        }
        private void DeleteTaskCommand()
        {
            _invoker.AddCommand(new DeleteTask(_tasks, Task, _onTaskDeleted));
            _invoker.Execute();
        }

    }
}
