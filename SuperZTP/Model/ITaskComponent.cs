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
        bool IsDone { get; }
        void MarkAsDone();
        public string ToFile();
    }
}
