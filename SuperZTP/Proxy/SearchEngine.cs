using DocumentFormat.OpenXml.ExtendedProperties;
using SuperZTP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Proxy
{
    public class SearchEngine: ISearch
    {
        private readonly List<Model.Task> tasks;
        private readonly List<Note> notes;

        public SearchEngine(List<Model.Task> _tasks, List<Note> _notes)
        {
            tasks = _tasks;
            notes = _notes;
        }
        public List<Model.Task> SearchTasks(string keyword)
        {
            return tasks.Where(task =>
                task.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                task.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Note> SearchNotes(string keyword)
        {
            return notes.Where(note =>
                note.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                note.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
