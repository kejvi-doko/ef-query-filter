using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FilterParser
{
    public interface ILogicalNode<T> where T:class
    {
        Expression Eval();
    }
}
