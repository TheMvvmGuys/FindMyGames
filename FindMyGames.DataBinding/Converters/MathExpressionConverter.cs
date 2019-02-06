using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Data;
namespace TheMvvmGuys.FindMyGames.DataBinding.Converters
{
    [ValueConversion(typeof(double), typeof(double), ParameterType = typeof(string))]
    [ValueConversion(typeof(double), typeof(int), ParameterType = typeof(string))]
    [ValueConversion(typeof(int), typeof(double), ParameterType = typeof(string))]
    [ValueConversion(typeof(int), typeof(int), ParameterType = typeof(string))]
    public class MathExpressionConverter : IValueConverter
    {
        private class ExpressionInfo : IEquatable<ExpressionInfo>, IEquatable<string>
        {
            public string Expression { get; }
            public Type ValueType { get; }

            public ExpressionInfo(string expression, Type valueType)
            {
                Expression = expression;
                ValueType = valueType;
            }

            public bool Equals(ExpressionInfo other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return string.Equals(Expression, other.Expression) && ValueType == other.ValueType;
            }

            public bool Equals(string other)
            {
                return Expression.Equals(other, StringComparison.Ordinal);
            }
            public override bool Equals(object obj)
            {
                if (obj is null) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((ExpressionInfo) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Expression != null ? Expression.GetHashCode() : 0) * 397) ^ (ValueType != null ? ValueType.GetHashCode() : 0);
                }
            }

            public static implicit operator string(ExpressionInfo i) => i.Expression;
        }
        private static readonly Dictionary<ExpressionInfo, Delegate> CachedExpressions = new Dictionary<ExpressionInfo, Delegate>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture = null)
        {
            if (culture == null) culture = CultureInfo.InvariantCulture;
            if (value is null || parameter is null) return null;
            if (value is string s)
            {
                if (double.TryParse(s, NumberStyles.Any, culture, out var val))
                {
                    value = val;
                }
            }
            if (!(parameter is string)) throw new InvalidOperationException("The parameter is not a string.");
            var expression = new ExpressionInfo((string)parameter, value.GetType());
            if (CachedExpressions.ContainsKey(expression))
            {
                return ConvertToType(targetType, CachedExpressions[expression].DynamicInvoke(value));
            }
            var expressionParam = Expression.Parameter(expression.ValueType, "value");
            LambdaExpression lambda;
            try
            {
                lambda = DynamicExpressionParser.ParseLambda(new[] { expressionParam }, typeof(double), expression, value); // Parse it
            }
            catch (ParseException e)
            {
                throw new InvalidMathExpressionException("The provided expression is invalid.", e, expression);
            }
            var compiled = lambda.Compile();
            CachedExpressions.Add(expression, compiled);
            var result = compiled.DynamicInvoke(value);
            return ConvertToType(targetType, result);
        }

        private static object ConvertToType(Type targetType, object result)
        {
            var resultType = result.GetType();
            targetType = Nullable.GetUnderlyingType(targetType) ?? targetType;
            if (resultType == targetType) return result;
            try
            {
                result = System.Convert.ChangeType(result, targetType); // int -> double for example.
            }
            catch (InvalidCastException) { } // Will be rethrown at binding time.
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}