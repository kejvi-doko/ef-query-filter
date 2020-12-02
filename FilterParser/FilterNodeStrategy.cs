using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public class FilterNodeStrategy<T> : IStrategy<T> where T : class
    {
        public ILogicalNode<T> CreateNode(params object[] data)
        {
            return new FilterNode<T>()
            {
                FieldName = (string) data[0],
                FieldValue = data[1],
                Operator = (string) data[2]
            };
        }
    }
}
