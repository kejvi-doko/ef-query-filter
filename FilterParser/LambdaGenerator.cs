using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public abstract class LambdaGenerator<T> where T:class
    {
        private ILogicalNode _rootNode;

        public ILogicalNode RootNode
        {
            get => _rootNode;
            set => _rootNode = value ?? throw new NullReferenceException();
        }
        public virtual Func<T, bool> GenerateLambda()
        {

        }
    }
}
