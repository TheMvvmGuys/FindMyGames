using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Windows.Data;
namespace TheMvvmGuys.FindMyGames.DataBinding.Converters
{
    [ValueConversion(typeof(double), typeof(double), ParameterType = typeof(string))]
    [ValueConversion(typeof(double), typeof(int), ParameterType = typeof(string))]
    [ValueConversion(typeof(int), typeof(double), ParameterType = typeof(string))]
    [ValueConversion(typeof(int), typeof(int), ParameterType = typeof(string))]
    public class MathExpressionConverter : IValueConverter
    {
        private static readonly Dictionary<string, Delegate> CachedExpressions = new Dictionary<string, Delegate>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || parameter is null) return null;
            if (value is string s)
            {
                if (double.TryParse(s, out var val))
                {
                    value = val;
                }
            }
            if (!(parameter is string)) throw new InvalidOperationException("The parameter is not a string.");
            var stringExpression = (string)parameter;
            if (CachedExpressions.ContainsKey(stringExpression))
            {
                return ConvertToType(targetType, CachedExpressions[stringExpression].DynamicInvoke(value));
            }
            var expressionParam = Expression.Parameter(value.GetType(), "value");
            var lambda = DynamicExpressionParser.ParseLambda(new[] { expressionParam }, typeof(double), stringExpression, value); // Parse it
            var compiled = lambda.Compile();
            CachedExpressions.Add(stringExpression, compiled);
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