﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FilterParser
{
    public sealed class AndLogicalNode<T>:ILogicalNode<T> where T:class
    {
        public ILogicalNode<T> LeftNode { get; set; }
        public ILogicalNode<T> RightNode { get; set; }
        public Expression Eval()
        {
            return Expression.And(LeftNode.Eval(), RightNode.Eval());
        }
    }
}
