using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Composite
{
    // Categroy - gałąź (grupa liści)
    public class Category : ICategory
    {
        public string Name { get; }
        private readonly List<ICategory> categories = new List<ICategory>();

        public Category(string name)
        {
            Name = name;
        }

        public void Add(ICategory category)
        {
            categories.Add(category);
        }

        public void Delete(ICategory category)
        {
            categories.Remove(category);
        }

        public IEnumerable<ICategory> GetChildren() => categories;
    }
}
