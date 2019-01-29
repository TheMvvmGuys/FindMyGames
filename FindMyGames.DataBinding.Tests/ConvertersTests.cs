using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheMvvmGuys.FindMyGames.DataBinding.Converters;

namespace TheMvvmGuys.FindMyGames.DataBinding.Tests
{
    [TestClass]
    public class ConvertersTests
    {
        private static readonly MathExpressionConverter Converter = new MathExpressionConverter();

        [TestMethod]
        public void MathConverter_IntToIntMultiplication_IsRight()
        {
            var value = 5;
            var result = Converter.Convert(value, typeof(int), "value * 5", CultureInfo.InvariantCulture);
            Assert.IsTrue(result is int r && r == 25);
        }

        [TestMethod]
        public void MathConverter_IntToDoubleMultiplication_IsRight()
        {
            var value = 5;
            var result = Converter.Convert(value, typeof(double), "value * 5", CultureInfo.InvariantCulture);
            Assert.IsTrue(result is double r && r.AlmostEquals(25));
        }

        [TestMethod]
        public void MathConverter_AnyOperation_Caches()
        {
            var longExpression = "value / 42 * 24 / 42 - 52 * 42 + 8 * (7 + 5)";
            var result = Converter.Convert(5, typeof(int), longExpression, CultureInfo.InvariantCulture);
            var privateType = new PrivateType(typeof(MathExpressionConverter));
            var dictionary = privateType.GetStaticField("CachedExpressions") as Dictionary<string, Delegate>;
            Assert.IsTrue(dictionary?.ContainsKey(longExpression) ?? false);
        }
    }
    public static class ConverterTestsExtensions
    {
        public static bool AlmostEquals(this double value, double expected)
        {
            return Math.Abs(value - expected) < double.Epsilon * 5;
        }
    }
}
