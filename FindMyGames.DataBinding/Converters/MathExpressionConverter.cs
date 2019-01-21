using System;
using System.Globalization;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Windows.Data;
using System.Linq.Dynamic.Core.Parser;
namespace TheMvvmGuys.FindMyGames.DataBinding.Converters
{
    [ValueConversion(typeof(double), typeof(double), ParameterType = typeof(string))]
    [ValueConversion(typeof(double), typeof(int), ParameterType = typeof(string))]
    [ValueConversion(typeof(int), typeof(double), ParameterType = typeof(string))]
    [ValueConversion(typeof(int), typeof(int), ParameterType = typeof(string))]
    public class MathExpressionConverter : IValueConverter
    {
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
            var expressionParam = Expression.Parameter(value.GetType(), "value");
            var lambda = DynamicExpressionParser.ParseLambda(new[] { expressionParam }, typeof(double), (string)parameter, value);
            var compiled = lambda.Compile();
            return compiled.DynamicInvoke(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}