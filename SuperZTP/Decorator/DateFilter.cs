using System;
using System.Collections.Generic;
using System.Linq;
using SuperZTP.Model;

namespace SuperZTP.Decorator
{
    public class DateFilter : ITaskFilter
    {
        private readonly DateTime selectedDate;

        public DateFilter(DateTime date)
        {
            selectedDate = date.Date;
        }

        public IEnumerable<Model.Task> ApplyFilter(IEnumerable<Model.Task> tasks)
        {
            return tasks.Where(task => task.Deadline.Date == selectedDate);
        }
    }
}
