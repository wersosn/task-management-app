using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Model;

namespace SuperZTP.Command
{
    public class AddTag : ICommand
    {
        private List<Tag> tags;
        private Tag newTag;

        public AddTag(List<Tag> tags, Tag newTag)
        {
            this.tags = tags;
            this.newTag = newTag;
        }

        public void Execute()
        {
            var tagCopy = new Tag(newTag.Name); // Praca na kopii tagu, aby uniknąć pracy na referencji
            tags.Add(tagCopy); // Dodanie tagu do listy
        }

        public void Undo()
        {
            tags.Remove(newTag); // Usunięcie tagu z listy 
        }
    }
}
