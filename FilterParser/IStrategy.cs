using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public interface IStrategy<T> where T :class
    {
        ILogicalNode<T> CreateNode(params object[] data);
    }
}
