using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Model;
using Task = SuperZTP.Model.Task;

namespace SuperZTP.Stores
{
    public class SelectedTaskStore
    {
        private Task _selectedTask;
        private Note _selectedNote;

        public Task SelectedTask
        {
            get
            {
                return _selectedTask;
            }
            set
            {
                _selectedTask = value;
                SelectedTaskChanged?.Invoke();
            }
        }
        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                SelectedNoteChanged?.Invoke();
            }
        }

        public event Action SelectedTaskChanged;
        public event Action SelectedNoteChanged;
    }
}
