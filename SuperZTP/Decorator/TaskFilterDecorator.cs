using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using SuperZTP.Model;
using Task = SuperZTP.Model.Task;

namespace SuperZTP.Decorator
{
    public abstract class TaskFilterDecorator : ITaskFilter
    {
        protected readonly ITaskFilter _nextFilter;

        protected TaskFilterDecorator(ITaskFilter nextFilter)
        {
            _nextFilter = nextFilter;
        }

        public virtual IEnumerable<Task> ApplyFilter(IEnumerable<Task> tasks)
        {
            var filteredTasks = _nextFilter?.ApplyFilter(tasks) ?? tasks;
            return ApplySpecificFilter(filteredTasks);
        }

        protected abstract IEnumerable<Task> ApplySpecificFilter(IEnumerable<Task> tasks);
    }

    public class TitleTaskFilter : TaskFilterDecorator
    {
        private readonly string _title;

        public TitleTaskFilter(string title, ITaskFilter nextFilter = null)
            : base(nextFilter)
        {
            _title = title;
        }

        protected override IEnumerable<Task> ApplySpecificFilter(IEnumerable<Task> tasks)
        {
            if (_title == null)
            {
                return tasks;
            }
            return tasks.Where(task => task.Title.Trim().Contains(_title.Trim()));
        }
    }

    public class CategoryTaskFilter : TaskFilterDecorator
    {
        private readonly string _category;

        public CategoryTaskFilter(string category, ITaskFilter nextFilter = null)
            : base(nextFilter)
        {
            _category = category;
        }

        protected override IEnumerable<Task> ApplySpecificFilter(IEnumerable<Task> tasks)
        {
            if (_category == null)
            {
                return tasks;
            }
            return tasks.Where(task => task.Category.Name.Trim().Equals(_category.Trim()));
        }
    }

    public class TagTaskFilter : TaskFilterDecorator
    {
        private readonly Tag _tag;

        public TagTaskFilter(Tag tag, ITaskFilter nextFilter = null)
            : base(nextFilter)
        {
            _tag = tag;
        }

        protected override IEnumerable<Task> ApplySpecificFilter(IEnumerable<Task> tasks)
        {
            if (_tag == null)
            {
                return tasks;
            }
            return tasks.Where(task => task.Tag == _tag);
        }
    }

    public class DueDateTaskFilter : TaskFilterDecorator
    {
        private readonly DateTime? _dueDate;

        public DueDateTaskFilter(DateTime? dueDate, ITaskFilter nextFilter = null)
            : base(nextFilter)
        {

            _dueDate = dueDate;
        }

        protected override IEnumerable<Task> ApplySpecificFilter(IEnumerable<Task> tasks)
        {
            if (_dueDate == null)
            {
                return tasks;
            }
            return tasks.Where(task => task.Deadline.Date == _dueDate.GetValueOrDefault());
        }
    }

    public class GroupTaskFilter : TaskFilterDecorator
    {
        private GroupingOption _groupingOption;

        public GroupTaskFilter(GroupingOption groupingOption, ITaskFilter nextFilter = null)
            : base(nextFilter)
        {

            _groupingOption = groupingOption;
        }

        protected override IEnumerable<Task> ApplySpecificFilter(IEnumerable<Task> tasks)
        {
            if (tasks == null)
            {
                return tasks;
            }

            return _groupingOption switch
            {
                GroupingOption.GroupByCategory => tasks
                    .GroupBy(t => t.Category?.Name ?? "Brak kategorii")
                    .OrderBy(g => g.Key)
                    .SelectMany(g => GetCategoryGroupedTasks(g.Key, g))
                    .ToList(),

                GroupingOption.GroupByTag => tasks
                    .GroupBy(t => t.Tag?.Name ?? "Brak tagu")
                    .OrderBy(g => g.Key)
                    .SelectMany(g => g)
                    .ToList(),

                _ => tasks
            };
        }

        private IEnumerable<Task> GetCategoryGroupedTasks(string category, IEnumerable<Task> tasks)
        {
            var headerTask = new Task
            {
                Title = $"--- {category} ---", // Nagłówek z nazwą kategorii
                IsHeader = true // Nowa właściwość do oznaczania nagłówków
            };

            return new[] { headerTask }.Concat(tasks);
        }
    }

}
