using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Model
{
    public interface ITaskComponent
    {
        int Id { get; }
        string Title { get; }
        //bool IsDone { get; }
        ITaskState State { get; }
        //void MarkAsDone();
        public string ToFile();
    }
}
