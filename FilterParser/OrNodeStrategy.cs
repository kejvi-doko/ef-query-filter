using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public class OrNodeStrategy<T> : IStrategy<T> where T : class
    {
        public ILogicalNode<T> CreateNode(params object[] data)
        {
            return new OrLogicalNode<T>()
            {
                LeftNode = (ILogicalNode<T>)data[0],
                RightNode = (ILogicalNode<T>)data[1]
            };
        }
    }
}
