﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public interface ILogicalNode<in T> where T:class
    {
        Func<T, bool> Eval();
    }
}
