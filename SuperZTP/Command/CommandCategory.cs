using SuperZTP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Command
{
    public class AddCategory : ICommand 
    {
        private List<Category> categories;
        private Category newCategory;

        public AddCategory(List<Category> categories, Category newCategory)
        {
            this.categories = categories;
            this.newCategory = newCategory;
        }

        public void Execute()
        {
            var categoryCopy = new Category(newCategory.Name);
            categories.Add(categoryCopy);
        }

        public void Undo()
        {
            categories.Remove(newCategory);
        }
    }
}
