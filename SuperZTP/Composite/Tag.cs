using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Composite
{
    // Tag - gałąź (grupa liści)
    public class Tag : ITag
    {
        public string Name { get; }
        private readonly List<ITag> tags = new List<ITag>();
        public Tag(string name)
        {
            Name = name;
        }
        public void Dodaj(ITag tag)
        {
            tags.Add(tag);
        }

        public void Usun(ITag tag)
        {
            tags.Remove(tag);
        }

        public IEnumerable<ITag> GetChildren() => tags;
    }
}
