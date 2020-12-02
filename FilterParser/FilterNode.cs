using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        public Expression Eval(ParameterExpression baseParameter)
        {
            // ParameterExpression baseParameter = Expression.Parameter(typeof(T), typeof(T).Name);

            var property = Expression.Property(baseParameter, typeof(T).GetProperty(FieldName));
            ConstantExpression c =
                Expression.Constant(FieldValue, FieldValue.GetType());

            var e = Operator switch
            {
                "EQ" => Expression.Equal(property, c),
                "GT" => Expression.GreaterThan(property, c),
                "LT" => Expression.LessThan(property, c),
                "GTE" => Expression.GreaterThanOrEqual(property, c),
                "LTE" => Expression.LessThanOrEqual(property, c),
                _ => throw new Exception("Operator is nt allowed")
            };

            var q = Expression.Lambda<Func<T, bool>>(e, new[] {baseParameter})
                .Compile();

            return e;
        }

        public ParameterExpression BaseParameter { get; set; }
    }
}
