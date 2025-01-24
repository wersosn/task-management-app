using SuperZTP.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Model;

namespace SuperZTP.Decorator
{
    public class TaskFilterManager
    {
        private readonly TaskFilterBuilder _filterBuilder = new TaskFilterBuilder();
        public event Action<ITaskFilter> FilterChanged;

        public void ApplyTitleFilter(string title)
        {
            _filterBuilder.AddTitleFilter(title);
            NotifyFilterChanged();
        }

        public void ApplyCategoryFilter(string category)
        {
            _filterBuilder.AddCategoryFilter(category);
            NotifyFilterChanged();
        }

        public void ApplyTagFilter(Tag tag)
        {
            _filterBuilder.AddTagFilter(tag);
            NotifyFilterChanged();
        }

        public void ApplyDueDateFilter(DateTime? dueDate)
        {
            _filterBuilder.AddDueDateFilter(dueDate);
            NotifyFilterChanged();
        }

        public void ApplayGroupFilter(GroupingOption groupingOption)
        {
            _filterBuilder.AddGroupFilter(groupingOption);
            NotifyFilterChanged();
        }

        public void ClearFilters()
        {
            _filterBuilder.ClearFilters();
            NotifyFilterChanged();
        }

        private void NotifyFilterChanged()
        {
            FilterChanged?.Invoke(_filterBuilder.Build());
        }
    }

}
