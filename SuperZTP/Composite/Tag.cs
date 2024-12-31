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
        public List<ITag> tags { get; set; } = new List<ITag>();
        public Tag(string name)
        {
            Name = name;
        }
        public void AddTag(ITag tag)
        {
            tags.Add(tag);
        }

        public void DeleteTag(ITag tag)
        {
            tags.Remove(tag);
        }
    }
}
