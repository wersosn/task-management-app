using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Model;

namespace SuperZTP.Builder
{
    // Builder Notatki
    public class NoteBuilder
    {
        private readonly Note note = new Note();
        public NoteBuilder setTytul(string title)
        {
            note.Title = title;
            return this;
        }

        public NoteBuilder setOpis(string description)
        {
            note.Description = description;
            return this;
        }

        public NoteBuilder setTagi(ITag tag)
        {
            note.Tag = tag;
            return this;
        }

        public NoteBuilder setKategorie(ICategory category)
        {
            note.Category = category;
            return this;
        }

        public Note build()
        {
            return note;
        }
    }
}
