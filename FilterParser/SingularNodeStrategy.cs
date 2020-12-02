using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public class SingularNodeStrategy<T> : IStrategy<T> where T : class
    {
        public ILogicalNode<T> CreateNode(params object[] data)
        {
            return new NotLogicalNode<T>()
            {
                LogicalNode =(ILogicalNode<T>) data[0]
            };
        }
    }
}
