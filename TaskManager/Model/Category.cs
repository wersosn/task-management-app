using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Model
{
    public interface ICategory
    {
        string CategoryName { get; }
    }

    public class Category : ICategory
    {
        public string CategoryName { get; }

        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}
