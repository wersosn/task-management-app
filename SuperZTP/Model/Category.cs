using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category(string name)
        {
            Name = name;
        }

        public string ToFile()
        {
            return Name;
        }

        public static Category FromFile(string fileLine)
        {
            return new Category(fileLine);
        }
    }
}
