using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Composite
{
    public interface ICategory
    {
        string Name { get; }
        void Dodaj(ICategory category);
        void Usun(ICategory category); 
        IEnumerable<ICategory> GetChildren();
    }
}
