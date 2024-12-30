using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperZTP.Composite
{
    // SubCategory - liść (pojedyncza kategoria)
    public class SubCategory : ICategory
    {
        public string Name { get; }

        public SubCategory(string name)
        {
            Name = name;
        }

        public void Add(ICategory category)
        {
            MessageBox.Show("Nie można dodać nowej kategorii do tego liścia");
        }

        public void Delete(ICategory category)
        {
            MessageBox.Show("Nie można Deleteąć kategorii z liścia");
        }

        public IEnumerable<ICategory> GetChildren() => Enumerable.Empty<ICategory>();
    }
}
