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
        void Dodaj(ITag tag);
        void Usun(ITag tag);
        IEnumerable<ITag> GetChildren();
    }
}
