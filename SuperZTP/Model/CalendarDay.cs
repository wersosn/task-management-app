using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;

namespace SuperZTP.Model
{
    public class CalendarDay(DateTime date)
    {
        [Required]
        private readonly DateTime _date = date;
        private readonly List<Task> _tasks = [];
        private readonly List<Note> _notes = [];

        public DateTime GetDate()
        {
            return _date;
        }
        public void AddTask(Task task)
        {
            _tasks.Add(task);
        }

        public void AddNote(Note note)
        {
            _notes.Add(note);
        }

        public void RemoveTask(Task task)
        {
            _tasks.Remove(task);
        }

        public void RemoveNote(Note note)
        {
            _notes.Remove(note);
        }

        public List<Task> GetAllTasks()
        {
            return _tasks;
        }

        public List<Note> GetAllNotes()
        {
            return _notes;
        }
    }
}
