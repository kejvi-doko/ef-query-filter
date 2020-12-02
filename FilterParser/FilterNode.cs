using System;
using System.Collections.Generic;
using System.Text;

namespace FilterParser
{
    public sealed class FilterNode<T>:ILogicalNode<T> where T:class
    {
        public string FieldName { get; set; }
        private string _operator;
        public string Operator
        {
            get => _operator;
            set
            {
                var tempOperator = value.ToUpper();
                _operator = tempOperator switch
                {
                    "GT" => tempOperator,
                    "LT" => tempOperator,
                    "GTE" => tempOperator,
                    "LTE" => tempOperator,
                    "EQ" => tempOperator,
                    _ => throw new Exception("Operator is not allowed")
                };
            }
        }
        public dynamic FieldValue { get; set; }
        public Func<T, bool> Eval()
        {
            throw new NotImplementedException();
        }
    }
}
