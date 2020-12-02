using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public sealed class AndLogicalNode:ILogicalNode
    {
        public ILogicalNode LeftNode { get; set; }
        public ILogicalNode RightNode { get; set; }
    }
}
