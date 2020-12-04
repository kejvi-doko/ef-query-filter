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
        private dynamic _fieldValue;

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
        public dynamic FieldValue
        {
            get => _fieldValue;
            set
            {
                
                if (value is System.Text.Json.JsonElement && value.ValueKind== System.Text.Json.JsonValueKind.String)
                {
                    if (value.TryGetDateTime(out DateTime e)) {_fieldValue = e;
                        return;
                    }

                    if (value.TryGetGuid(out Guid g)) {_fieldValue = g;
                        return;
                    }

                    _fieldValue = value.GetString();
                }
                else if (value is System.Text.Json.JsonElement &&
                         (
                         value.ValueKind == System.Text.Json.JsonValueKind.True || 
                         value.ValueKind == System.Text.Json.JsonValueKind.False))
                {
                    _fieldValue = value.GetBoolean();
                }
                else if (value is System.Text.Json.JsonElement && value.ValueKind == System.Text.Json.JsonValueKind.Number)
                {
                    if (value.TryGetInt32(out int a)) {_fieldValue = a;
                        return;
                    }

                    if (value.TryGetDouble(out double d)){ _fieldValue = d;
                        return;
                    }

                    if (value.TryGetDecimal(out decimal dc))
                        _fieldValue = dc;

                }
                else
                {
                    _fieldValue = value;
                }
            }
            
        }
        public Expression Eval(ParameterExpression baseParameter)
        {
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
