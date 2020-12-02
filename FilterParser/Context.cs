using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public class Context<T> where T : class

    {
        private IStrategy<T> _strategy;

        public Context()
        {
        }

        // Usually, the Context accepts a strategy through the constructor, but
        // also provides a setter to change it at runtime.
        public Context(IStrategy<T> strategy)
        {
            this._strategy = strategy;
        }

        public void SetStrategy(IStrategy<T> strategy)
        {
            this._strategy = strategy;
        }

        public ILogicalNode<T> CreateLogicalNode(params object[] data)
        {
            return _strategy.CreateNode(data);
        }
    }
}
