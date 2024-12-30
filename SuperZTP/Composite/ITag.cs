using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Composite
{
    public interface ITag
    {
        string Name { get; }
        void Add(ITag tag);
        void Delete(ITag tag);
        IEnumerable<ITag> GetChildren();
    }
}
