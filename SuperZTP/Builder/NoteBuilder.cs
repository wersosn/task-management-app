﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Composite;
using SuperZTP.Model;

namespace SuperZTP.Builder
{
    // Builder Notatki
    public class NoteBuilder
    {
        private readonly Note note = new Note();
        public NoteBuilder setTitle(string title)
        {
            note.Title = title;
            return this;
        }

        public NoteBuilder setDescription(string description)
        {
            note.Description = description;
            return this;
        }

        public NoteBuilder setTag(ITag tag)
        {
            note.Tag = tag;
            return this;
        }

        public NoteBuilder setCategory(ICategory category)
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
