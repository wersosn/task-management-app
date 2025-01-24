using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class TaskPropertyFilter<T> : TaskFilterDecorator
    {
        private readonly Func<Task, T> _propertySelector;
        private readonly T _value;

        public TaskPropertyFilter(Func<Task, T> propertySelector, T value, ITaskFilter nextFilter = null)
            : base(nextFilter)
        {
            _propertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
            _value = value;
        }

        protected override IEnumerable<Task> ApplySpecificFilter(IEnumerable<Task> tasks)
        {
            return tasks.Where(task => EqualityComparer<T>.Default.Equals(_propertySelector(task), _value));
        }
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
        private readonly DateTime _dueDate;

        public DueDateTaskFilter(DateTime dueDate, ITaskFilter nextFilter = null)
            : base(nextFilter)
        {
            _dueDate = dueDate;
        }

        protected override IEnumerable<Task> ApplySpecificFilter(IEnumerable<Task> tasks)
        {
            return tasks.Where(task => task.Deadline.Date == _dueDate.Date);
        }
    }

}
