using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperZTP.Composite
{
    // Tag - liść (pojedynczy tag)
    public class SubTag : ITag
    {
        public string Name { get; }

        public SubTag(string name)
        {
            Name = name;
        }
        public void AddTag(ITag tag)
        {
            MessageBox.Show("Nie można dodać nowego tagu do tego liścia");
        }

        public void DeleteTag(ITag tag)
        {
            MessageBox.Show("Nie można usunąć tagu z liścia");
        }
    }
}
