using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Stores
{
    public class TaskStore
    {
        public event Action<Model.Task> TaskAdded;

        // public Task Add(Model.Task task)
        // {
        //     TaskAdded?.Invoke(task);
        // }
    }
}
