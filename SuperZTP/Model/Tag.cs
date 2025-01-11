using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Model
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Tag(string name)
        {
            Name = name;
        }

        public string ToFile()
        {
            return Name;
        }

        public static Tag FromFile(string fileLine)
        {
            return new Tag(fileLine);
        }
    }
}
