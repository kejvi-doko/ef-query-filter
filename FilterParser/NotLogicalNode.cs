using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public sealed class NotLogicalNode:ILogicalNode
    {
        public ILogicalNode LogicalNode { get; set; }
    }
}
