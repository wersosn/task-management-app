using SuperZTP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Command
{
    // Dodawanie kategorii
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
            var categoryCopy = new Category(newCategory.Name); // Praca na kopii kategorii, aby uniknąć pracy na referencji
            categories.Add(categoryCopy); // Dodanie kategorii do listy
        }

        public void Undo()
        {
            categories.Remove(newCategory); // Usinięcie z listy
        }
    }
}
