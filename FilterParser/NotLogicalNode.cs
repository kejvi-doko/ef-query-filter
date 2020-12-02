using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FilterParser
{
    public sealed class NotLogicalNode<T>:ILogicalNode<T> where T:class
    {
        public ILogicalNode<T> LogicalNode { get; set; }
        public Expression Eval(ParameterExpression baseParameter)
        {
            if (baseParameter == null)
            {
                baseParameter = Expression.Parameter(typeof(T), typeof(T).Name);
                BaseParameter = baseParameter;
            }
            return Expression.Not(LogicalNode.Eval(baseParameter));
        }

        public ParameterExpression BaseParameter { get; set; }
    }
}
