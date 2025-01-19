using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Command;
using SuperZTP.Stores;

namespace SuperZTP.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public DisplayTasksViewModel DisplayTasksViewModel { get; } 
        public TaskDetailsViewModel TaskDetailsViewModel { get; }
        public ICommand AddTaskCommand { get; }

        public MenuViewModel(SelectedTaskStore _selectedTaskStore)
        {
            DisplayTasksViewModel = new DisplayTasksViewModel(_selectedTaskStore);
            TaskDetailsViewModel = new TaskDetailsViewModel(_selectedTaskStore);
        }
    }
}
