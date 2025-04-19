using SuperZTP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Resources;

namespace SuperZTP.Command
{
    // Dodawanie notatki
    public class AddNote : ICommand
    {
        private List<Note> notes;
        private Note newNote;
        private readonly Action _onNoteAdded;

        public AddNote(List<Note> notes, Note newNote, Action onNoteAdded)
        {
            this.notes = notes;
            this.newNote = newNote;
            _onNoteAdded = onNoteAdded;
        }

        public void Execute()
        {
            var noteCopy = new Note // Praca na kopii notatki, aby uniknąć pracy na referencji
            {
                Id = newNote.Id,
                Title = newNote.Title,
                Description = newNote.Description,
                Tag = newNote.Tag,
                Category = newNote.Category
            };

            notes.Add(noteCopy);
            _onNoteAdded?.Invoke();
        }

        public void Undo()
        {
            notes.Remove(newNote);
            _onNoteAdded?.Invoke();
        }

        public override string ToString()
        {
            return newNote != null
                ? string.Format(Strings.HisAddNote, newNote.Title)
                : Strings.HisAddNoteFailed;
        }
    }

    // Modyfikacja notatki
    public class EditNote : ICommand
    {
        private List<Note> notes;
        private Note newNote;
        private Note oldNote;
        private int id;

        public EditNote(List<Note> notes, Note oldNote, Note newNote, int id)
        {
            this.notes = notes;
            this.id = id;

            this.oldNote = new Note // Kopia starej wersji notatki, aby uniknąć pracy na referencji
            {
                Id = oldNote.Id,
                Title = oldNote.Title,
                Description = oldNote.Description,
                Tag = oldNote.Tag,
                Category = oldNote.Category
            };

            this.newNote = new Note // Kopia nowej wersji notatki, aby uniknąć pracy na referencji
            {
                Id = newNote.Id,
                Title = newNote.Title,
                Description = newNote.Description,
                Tag = newNote.Tag,
                Category = newNote.Category
            };
        }

        public void Execute()
        {
            if (id >= 0 && id < notes.Count)
            {
                notes[id] = newNote;
            }
        }

        public void Undo()
        {
            if (oldNote != null && id >= 0 && id <= notes.Count)
            {
                notes[id] = oldNote;
            }
        }

        public override string ToString()
        {
            return newNote != null
                ? string.Format(Strings.HisEditNote, newNote.Title)
                : Strings.HisEditNoteFailed;
        }
    }

    // Usuwanie notatki
    public class DeleteNote : ICommand
    {
        private readonly List<Note> _notes;
        private readonly Note _noteToDelete;
        private readonly int _index;
        private readonly Action _onNoteDeleted;

        public DeleteNote(List<Note> notes, Note note, Action onNoteDeleted)
        {
            _notes = notes ?? throw new ArgumentNullException(nameof(notes));
            _noteToDelete = note ?? throw new ArgumentNullException(nameof(note));
            _onNoteDeleted = onNoteDeleted ?? throw new ArgumentNullException(nameof(onNoteDeleted));
            _index = _notes.FindIndex(n => n.Id == note.Id);
        }

        public void Execute()
        {
            if (_index >= 0)
            {
                _notes.RemoveAt(_index);
                _onNoteDeleted?.Invoke();
            }
        }

        public void Undo()
        {
            if (_index >= 0)
            {
                _notes.Insert(_index, _noteToDelete);
                _onNoteDeleted?.Invoke();
            }
        }

        public override string ToString()
        {
            return _noteToDelete != null
                ? string.Format(Strings.HisDeleteNote, _noteToDelete.Title)
                : Strings.HisDeleteNoteFailed;
        }
    }

    // Grupowanie notatek
    public class GroupNotes
    {
        public List<IGrouping<string, Note>> GroupNotesByCategory(List<Note> notes)
        {
            var groupedNotes = notes
                .GroupBy(note => note.Category?.Name ?? "Brak kategorii")
                .OrderBy(group => group.Key)
                .ToList();
            return groupedNotes;
        }

        public List<IGrouping<string, Note>> GroupNotesByTag(List<Note> notes)
        {
            var groupedNotes = notes
                .GroupBy(note => note.Tag?.Name ?? "Brak tagu")
                .OrderBy(group => group.Key)
                .ToList();
            return groupedNotes;
        }
    }

    // Sortowanie notatek
    public class SortNotes
    {
        public List<Note> SortNotesByTitle(List<Note> notes, bool ascending = true)
        {
            var sortedNotes = ascending
                ? notes.OrderBy(note => note.Title).ToList()
                : notes.OrderByDescending(note => note.Title).ToList();

            notes = sortedNotes;
            return notes;
        }
    }
}
