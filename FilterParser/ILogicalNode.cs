using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FilterParser
{
    public interface ILogicalNode<T> where T:class
    {
        // TODO remove base parameter, return compiled expression
        ParameterExpression BaseParameter { get; set; }
        Expression Eval(ParameterExpression baseParameter=null);
    }
}
