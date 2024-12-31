using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SuperZTP.Composite
{
    // Categroy - gałąź (grupa liści)
    public class Category : ICategory
    {
        public string Name { get; }

        public List<ICategory> categories { get; set; } = new List<ICategory>();

        public Category(string name)
        {
            Name = name;
        }

        public void AddCategory(ICategory category)
        {
            categories.Add(category);
        }

        public void DeleteCategory(ICategory category)
        {
            categories.Remove(category);
        }
    }
}
