using System;
using System.Linq.Expressions;

namespace FilterParser
{
    public class FilterGenerator<T> where T : class
    {
        public FilterRow root { get; set; }

        public Func<T, bool> GenerateFilter()
        {
            FilterRow currentFilter = root;
            ParameterExpression baseParameter = Expression.Parameter(typeof(T), typeof(T).Name);
            
            var property = Expression.Property(baseParameter, typeof(T).GetProperty(currentFilter.ColumnName));
            ConstantExpression c =
                Expression.Constant(currentFilter.CompareValue, currentFilter.CompareValue.GetType());

            BinaryExpression e = currentFilter.Operator switch
            {
                "EQ" => Expression.Equal(property, c),
                "GT" => Expression.GreaterThan(property, c),
                "LT" => Expression.LessThan(property, c),
                "GTE" => Expression.GreaterThanOrEqual(property, c),
                "LTE" => Expression.LessThanOrEqual(property, c),
                _ => throw new Exception("Operator is nt allowed")
            };
            
            FilterRow previousFilter = currentFilter;
            currentFilter = currentFilter.NextFilter;
            
            while (currentFilter != null)
            {

                var property2 = Expression.Property(baseParameter, typeof(T).GetProperty(currentFilter.ColumnName));
                
                ConstantExpression c2 =
                    Expression.Constant(currentFilter.CompareValue, currentFilter.CompareValue.GetType());

                BinaryExpression e2 = currentFilter.Operator switch
                {
                    "EQ" => Expression.Equal(property2, c2),
                    "GT" => Expression.GreaterThan(property2, c2),
                    "LT" => Expression.LessThan(property2, c2),
                    "GTE" => Expression.GreaterThanOrEqual(property2, c2),
                    "LTE" => Expression.LessThanOrEqual(property2, c2),
                    _ => throw new Exception("Operator is nt allowed")
                };
                
                e = previousFilter.BooleanOperator switch
                {
                    "OR" => Expression.Or(e, e2),
                    "AND" => Expression.And(e, e2),
                    _ => throw new Exception("Boolean operator is not allowed")
                };

                previousFilter = currentFilter;
                currentFilter = currentFilter.NextFilter;
            }
            
            var compliedExpression = Expression.Lambda<Func<T, bool>>(e, new[]
            {
                baseParameter
            }).Compile();

            return compliedExpression;
        }
    }
}