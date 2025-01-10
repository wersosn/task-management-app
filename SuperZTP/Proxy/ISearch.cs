using SuperZTP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP
{
    public interface ISearch
    {
        List<Model.Task> SearchTasks(string keyword);
        List<Note> SearchNotes(string keyword);
    }
}
