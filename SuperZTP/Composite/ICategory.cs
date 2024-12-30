﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Composite
{
    public interface ICategory
    {
        string Name { get; }
        void Add(ICategory category);
        void Delete(ICategory category); 
        IEnumerable<ICategory> GetChildren();
    }
}
