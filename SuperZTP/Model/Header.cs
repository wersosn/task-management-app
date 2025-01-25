using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Model
{
    public class Header
    {
        public string Title { get; set; }
        public Category? Category { get; set; }
        public Tag? Tag { get; set; }

        public Header() { }
        public Header(string title)
        {
            Title = title;
        }
        public Header(string title, Category? category, Tag? tag)
        {
            Title = title;
            Category = category;
            Tag = tag;
        }
    }
}
