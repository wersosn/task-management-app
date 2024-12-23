using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Model
{
    public interface ITag
    {
        string Name { get; }
    }

    public class Tag : ITag
    {
        public string Name { get; }

        public Tag(string name)
        {
            Name = name;
        }
    }
}
