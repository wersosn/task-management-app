using SuperZTP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Command
{
    public class DodajNotatke : ICommand
    {
        private List<Note> notes;
        private Note newNote;

        public DodajNotatke(List<Note> notes, Note newNote)
        {
            this.notes = notes;
            this.newNote = newNote;
        }

        public void Wykonaj()
        {
            var noteCopy = new Note
            {
                Id = newNote.Id,
                Title = newNote.Title,
                Description = newNote.Description,
                Tag = newNote.Tag,
                Category = newNote.Category
            };

            notes.Add(noteCopy);
        }

        public void Cofnij()
        {
            notes.Remove(newNote);
        }
    }

    public class EdytujNotatke : ICommand
    {
        private List<Note> notes;
        private Note newNoteCopy;
        private Note oldNote;
        private int id;

        public EdytujNotatke(List<Note> notes, Note editNote, int id)
        {
            this.notes = notes;
            this.id = id;

            if(id >= 0 && id <= notes.Count)
            {
                oldNote = new Note
                {
                    Id = notes[id].Id,
                    Title = notes[id].Title,
                    Description = notes[id].Description,
                    Tag = notes[id].Tag,
                    Category = notes[id].Category
                };

                newNoteCopy = new Note
                {
                    Id = editNote.Id,
                    Title = editNote.Title,
                    Description = editNote.Description,
                    Tag = editNote.Tag,
                    Category = editNote.Category
                };
            }
        }

        public void Wykonaj()
        {
            if(id >= 0 && id <= notes.Count)
            {
                notes[id] = newNoteCopy;
            }
        }

        public void Cofnij()
        {
            if(oldNote != null && id >= 0 && id <= notes.Count)
            {
                notes[id] = oldNote;
            }
        }
    }

    public class UsunNotatke : ICommand
    {
        private List<Note> notes;
        private Note noteCopy;
        private int id;

        public UsunNotatke(List<Note> notes, Note deleteNote, int id)
        {
            this.notes = notes;
            this.id = id;
            if(id >= 0 && id <= notes.Count)
            {
                noteCopy = new Note
                {
                    Id = deleteNote.Id,
                    Title = deleteNote.Title,
                    Description = deleteNote.Description,
                    Tag = deleteNote.Tag,
                    Category = deleteNote.Category
                };
            }
        }

        public void Wykonaj() 
        { 
            if(id >= 0 && id <= notes.Count)
            {
                notes.RemoveAt(id);
            }
        }

        public void Cofnij()
        {
            if(noteCopy != null && id >= 0 && id <= notes.Count)
            {
                notes.Insert(id, noteCopy);
            }
        }
    }
}
