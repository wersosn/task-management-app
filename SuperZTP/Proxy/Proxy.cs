using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SuperZTP.Model;


namespace SuperZTP.Proxy
{
    public class Proxy : ISearch
    {
        private readonly SearchEngine _searchEngine;
        private Dictionary<string, List<Model.Task>> taskCache = new Dictionary<string, List<Model.Task>>();
        private Dictionary<string, List<Note>> noteCache = new Dictionary<string, List<Note>>();

        public Proxy(List<Model.Task> tasks, List<Note> notes)
        {
            _searchEngine = new SearchEngine(tasks, notes);
        }

        public List<Model.Task> SearchTasks(string keyword)
        {
            if (taskCache.ContainsKey(keyword))
            {
                return taskCache[keyword];
            }
            else
            {
                var result =  _searchEngine.SearchTasks(keyword);
                taskCache[keyword] = result;
                return result;
            }
            
        }
        public List<Note> SearchNotes(string keyword)
        {
            if (noteCache.ContainsKey(keyword))
            {
                return noteCache[keyword];
            }

            else
            {
             var result = _searchEngine.SearchNotes(keyword);
                noteCache[keyword] = result;
                return result;
            }
          
        }

        public void ClearTaskCache()
        {
            taskCache.Clear();
        }

        public void ClearNoteCache()
        {
            noteCache.Clear();
        }
    }
}
