using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public class AndNodeStrategy<T>:IStrategy<T> where T:class
    {
        public ILogicalNode<T> CreateNode(params object[] data)
        {
            return new AndLogicalNode<T>()
            {
                LeftNode=(ILogicalNode <T>)data[0],
                RightNode=(ILogicalNode <T>)data[1]
            };
        }
    }
}
