using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public sealed class AndLogicalNode<T>:ILogicalNode<T> where T:class
    {
        public ILogicalNode<T> LeftNode { get; set; }
        public ILogicalNode<T> RightNode { get; set; }
        public Func<T, bool> Eval()
        {
            throw new NotImplementedException();
        }
    }
}
