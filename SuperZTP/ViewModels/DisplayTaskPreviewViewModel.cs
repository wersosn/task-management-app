using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Command;

namespace SuperZTP.ViewModels
{
    public class DisplayTaskPreviewViewModel : BaseViewModel
    {
        public Model.Task Task { get; }
        public string Title => Task.Title;
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public DisplayTaskPreviewViewModel(Model.Task task)
        {
            Task = task;
        }
    }
}
