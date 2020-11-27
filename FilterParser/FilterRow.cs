using System;

namespace FilterParser
{
    public class FilterRow
    {
        private string _operator;
        private string _booleanOperator;
        
        public dynamic CompareValue { get; set; }
        public string ColumnName { get; set; }
        public FilterRow NextFilter { get; set; }
        
        public string BooleanOperator {
            get => _booleanOperator;
            set
            {
                var tempOperator = value.ToUpper();

                _booleanOperator = tempOperator switch
                {
                    "OR" => tempOperator,
                    "AND" => tempOperator,
                    _ => throw new Exception("Boolean operator is not allowed")
                };
            } 
        }
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
                    _ => throw new Exception("Operator is nt allowed")
                };
            } 
        }
    }
}