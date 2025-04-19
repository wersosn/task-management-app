using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Resources;

namespace SuperZTP.Model
{
    public enum GroupingOption
    {
        [LocalizedDescription("NoGroup", typeof(Strings))]
        NoGroup,

        [LocalizedDescription("GroupByCategory", typeof(Strings))]
        GroupByCategory,

        [LocalizedDescription("GroupByTag", typeof(Strings))]
        GroupByTag
    }

    public enum CompletionStatus
    {
        [LocalizedDescription("Default", typeof(Strings))]
        Default,
        [LocalizedDescription("Completed", typeof(Strings))]
        Completed,
        [LocalizedDescription("NotCompleted", typeof(Strings))]
        NotCompleted,
        [LocalizedDescription("ShowAll", typeof(Strings))]
        ShowAll
    }

    public enum SortOptions
    {
        [LocalizedDescription("RandomOrder", typeof(Strings))]
        RandomOrder,
        [LocalizedDescription("Alphabetical", typeof(Strings))]
        Alphabetical,
        [LocalizedDescription("ReverseAlphabetical", typeof(Strings))]
        ReverseAlphabetical,
        [LocalizedDescription("Date", typeof(Strings))]
        Date,
        [LocalizedDescription("ReverseDare", typeof(Strings))]
        ReverseDate,
        [LocalizedDescription("DateAZ", typeof(Strings))]
        DateAZ,
        [LocalizedDescription("DateZA", typeof(Strings))]
        DateZA,
        [LocalizedDescription("PriorityHighToLow", typeof(Strings))]
        PriorityHighToLow,
        [LocalizedDescription("PriorityLowToHigh", typeof(Strings))]
        PriorityLowToHigh
    }
}
