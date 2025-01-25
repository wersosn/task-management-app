using SuperZTP.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Model;

namespace SuperZTP.Builder
{
    public class TaskFilterBuilder
    {
        private ITaskFilter _currentFilter;

        public TaskFilterBuilder AddTitleFilter(string title)
        {
            _currentFilter = new TitleTaskFilter(title, _currentFilter);
            return this;
        }

        public TaskFilterBuilder AddCategoryFilter(string category)
        {
            _currentFilter = new CategoryTaskFilter(category, _currentFilter);
            return this;
        }

        public TaskFilterBuilder AddTagFilter(string tag)
        {
            _currentFilter = new TagTaskFilter(tag, _currentFilter);
            return this;
        }

        public TaskFilterBuilder AddDueDateFilter(DateTime? dueDate)
        {
            _currentFilter = new DueDateTaskFilter(dueDate, _currentFilter);
            return this;
        }

        public TaskFilterBuilder AddGroupFilter(GroupingOption groupingOption)
        {
            _currentFilter = new GroupTaskFilter(groupingOption, _currentFilter);
            return this;
        }

        public TaskFilterBuilder ClearFilters()
        {
            _currentFilter = null;
            return this;
        }

        public ITaskFilter Build()
        {
            return _currentFilter;
        }
    }

}
