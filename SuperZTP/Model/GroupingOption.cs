using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Model
{
    public enum GroupingOption
    {
        [Description("Brak grupowania")]
        NoGroup,
        [Description("Grupuj kategoriami")]
        GroupByCategory,
        [Description("Grupuj tagami")]
        GroupByTag
    }

    public enum CompletionStatus
    {
        [Description("Domyślny")]
        Default,
        [Description("Pokaż ukończone")]
        Completed,
        [Description("Pokaż nieukończone")]
        NotCompleted,
        [Description("Pokaż wszystkie")]
        ShowAll
    }

    public enum SortOptions
    {
        [Description("Losowa kolejność")]
        RandomOrder,
        [Description("A-Z")]
        Alphabetical,
        [Description("Z-A")]
        ReverseAlphabetical,
        [Description("Względem daty \u2191 ")]
        Date,
        [Description("Względem daty \t\u2193 ")]
        ReverseDate,
        [Description("A-Z Względem daty \u2191 ")]
        DateAZ,
        [Description("Z-A Względem daty \t\u2193 ")]
        DateZA,
        [Description("Priorytet \u2191 (Wysoki -> Niski)")]
        PriorityHighToLow,
        [Description("Priorytet \u2193 (Niski -> Wysoki)")]
        PriorityLowToHigh
    }
}
