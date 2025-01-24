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
}
