using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Decorator
{
    public interface ITaskFilter
    {
        IEnumerable<Model.Task> ApplyFilter(IEnumerable<Model.Task> tasks);
    }
}
