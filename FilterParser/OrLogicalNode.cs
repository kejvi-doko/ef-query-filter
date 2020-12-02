using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public sealed class OrLogicalNode:ILogicalNode
    {
        public ILogicalNode LeftNode { get; set; }
        public ILogicalNode RightNode { get; set; }
    }
}
