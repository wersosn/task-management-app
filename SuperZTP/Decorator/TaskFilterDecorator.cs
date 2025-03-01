﻿using System;
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
        public readonly string _title;

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
            if (_category == null || _category == "")
            {
                return tasks;
            }
            return tasks.Where(task => task.Category.Name.Trim().Equals(_category.Trim()));
        }
    }

    public class TagTaskFilter : TaskFilterDecorator
    {
        private readonly string _tag;

        public TagTaskFilter(string tag, ITaskFilter nextFilter = null)
            : base(nextFilter)
        {
            _tag = tag;
        }

        protected override IEnumerable<Task> ApplySpecificFilter(IEnumerable<Task> tasks)
        {
            if (_tag == null || _tag == "")
            {
                return tasks;
            }
            return tasks.Where(task => task.Tag.Name.Trim().Equals(_tag.Trim()));
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

    public class IsDoneFilter : TaskFilterDecorator
    {
        private readonly CompletionStatus? _isDone;

        public IsDoneFilter(CompletionStatus? isDone, ITaskFilter nextFilter = null)
            : base(nextFilter)
        {
            _isDone = isDone;
        }

        protected override IEnumerable<Task> ApplySpecificFilter(IEnumerable<Task> tasks)
        {
            if (_isDone == null)
            {
                return tasks;
            }
            return _isDone switch
            {
                CompletionStatus.Default => tasks,

                CompletionStatus.Completed => tasks
                    .Where(task => task.IsDone),

                CompletionStatus.NotCompleted => tasks
                    .Where(task => task.IsDone == false),

                CompletionStatus.ShowAll => tasks,

                _ => tasks
            };
        }
    }

    public class SortOptionDecorator : TaskFilterDecorator
    {
        private readonly SortOptions _sortOption;

        public SortOptionDecorator(SortOptions sortOption, ITaskFilter nextFilter = null)
            : base(nextFilter)
        {
            _sortOption = sortOption;
        }

        protected override IEnumerable<Task> ApplySpecificFilter(IEnumerable<Task> tasks)
        {

            return _sortOption switch
            {
                SortOptions.Alphabetical => tasks.OrderBy(task => task.Title),
                SortOptions.ReverseAlphabetical => tasks.OrderByDescending(task => task.Title),
                SortOptions.Date => tasks.OrderBy(task => task.Deadline),
                SortOptions.ReverseDate => tasks.OrderByDescending(task => task.Deadline),
                SortOptions.DateAZ => tasks.OrderBy(task => task.Deadline).ThenBy(task => task.Title),
                SortOptions.DateZA => tasks.OrderBy(task => task.Deadline).ThenByDescending(task => task.Title),
                SortOptions.PriorityHighToLow => tasks.OrderByDescending(task => task.Priority),
                SortOptions.PriorityLowToHigh => tasks.OrderBy(task => task.Priority),
                _ => tasks 
            };
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
                    .SelectMany(g => GetTagGroupedTasks(g.Key, g))
                    .ToList(),

                _ => tasks
            };
        }

        private IEnumerable<Task> GetCategoryGroupedTasks(string category, IEnumerable<Task> tasks)
        {
            var headerTask = new Task
            {
                Title = $"--- {category.Trim()} ---", 
                IsHeader = true, 
                Category = new Category(category),
                Tag = new Tag("")
            };

            return new[] { headerTask }.Concat(tasks);
        }
        private IEnumerable<Task> GetTagGroupedTasks(string tag, IEnumerable<Task> tasks)
        {
            var headerTask = new Task
            {
                Title = $"--- {tag.Trim()} ---",
                IsHeader = true, 
                Tag = new Tag(tag),
                Category = new Category("")
            };

            return new[] { headerTask }.Concat(tasks);
        }
    }

}
