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
        void AddTag(ITag tag);
        void DeleteTag(ITag tag);
    }
}
