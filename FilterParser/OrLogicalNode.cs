using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FilterParser
{
    public sealed class OrLogicalNode<T> : ILogicalNode<T> where T : class

    {
        public ILogicalNode<T> LeftNode { get; set; }
        public ILogicalNode<T> RightNode { get; set; }
        public Expression Eval()
        {
            return Expression.Or(LeftNode.Eval(), RightNode.Eval());
        }
    }
}
