using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public sealed class NotLogicalNode<T>:ILogicalNode<T> where T:class
    {
        public ILogicalNode<T> LogicalNode { get; set; }
        public Func<T, bool> Eval()
        {
            throw new NotImplementedException();
        }
    }
}
