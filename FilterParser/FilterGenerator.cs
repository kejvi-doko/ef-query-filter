using System;
using System.Linq.Expressions;

namespace FilterParser
{
    public class FilterGenerator<T> where T : class
    {
        private FilterRow _root { get; set; }

        public FilterGenerator(FilterRow root)
        {
            _root = root;
        }

        public Func<T, bool> GenerateFilter()
        {
            Context<T> ctx = new Context<T>();

            FilterRow currentFilter = _root;
            ParameterExpression baseParameter = Expression.Parameter(typeof(T), typeof(T).Name);

            ctx.SetStrategy(new FilterNodeStrategy<T>());

            ILogicalNode<T> e = ctx.CreateLogicalNode(currentFilter.ColumnName, currentFilter.CompareValue,
                currentFilter.Operator);
            
            FilterRow previousFilter = currentFilter;
            currentFilter = currentFilter.NextFilter;
            
            while (currentFilter != null)
            {

                ILogicalNode<T> e2 = ctx.CreateLogicalNode(currentFilter.ColumnName, currentFilter.CompareValue,
                    currentFilter.Operator);


                switch (previousFilter.BooleanOperator)
                {
                    case "OR":
                        ctx.SetStrategy(new OrNodeStrategy<T>());
                        break;
                    case "AND":
                        ctx.SetStrategy(new AndNodeStrategy<T>());
                        break;
                    default:
                        throw new Exception("Boolean operator is not allowed");
                }

                e = ctx.CreateLogicalNode(e, e2);

                previousFilter = currentFilter;
                currentFilter = currentFilter.NextFilter;
            }

            var compliedExpression = Expression.Lambda<Func<T, bool>>(e.Eval(baseParameter), new[]
            {
                baseParameter
            }).Compile();

            return compliedExpression;
        }
    }
}