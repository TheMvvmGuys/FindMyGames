using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TheMvvmGuys.FindMyGames.DataBinding.Converters;
// ReSharper disable PossibleNullReferenceException

namespace TheMvvmGuys.FindMyGames.DataBinding.Tests
{
    [TestClass]
    public class ConvertersTests
    {
        private static readonly MathExpressionConverter Converter = new MathExpressionConverter();
        private const double Delta = double.Epsilon * 8;
        [TestMethod]
        public void MathConverter_IntToIntMultiplication_IsRight()
        {
            var value = 5;
            var result = Converter.Convert(value, typeof(int), "value * 5");
            Assert.AreEqual(25, (int?)result);
        }

        [TestMethod]
        public void MathConverter_IntToDoubleMultiplication_IsRight()
        {
            var value = 5;
            var result = Converter.Convert(value, typeof(double), "value * 5");
            Assert.AreEqual(25d, (double)result, Delta);
        }

        [TestMethod]
        public void MathConverter_DoubleToIntMultiplication_Rounds()
        {
            var value = 2.4;
            var result = Converter.Convert(value, typeof(int), "value * 2");
            Assert.AreEqual(5, (int)result);
        }
        [TestMethod]
        public void MathConverter_DoubleToDoubleMultiplication_IsRight()
        {
            var value = 7.5;
            var result = Converter.Convert(value, typeof(double), "value * 3");
            Assert.AreEqual(7.5d * 3, (double)result, Delta);
        }
        [TestMethod]
        public void MathConverter_DoubleStringToDouble_IsRight()
        {
            var value = "6.5";
            var result = Converter.Convert(value, typeof(double), "value * 5");
            Assert.AreEqual(6.5d * 5d, (double)result, Delta);
        }

        [TestMethod]
        public void MathConverter_CommaDoubleStringToDouble_IsRight()
        {
            var value = "7,7";
            // The French CultureInfo uses commas as decimal separators
            var result = Converter.Convert(value, typeof(double), "value * 4", CultureInfo.GetCultureInfo("fr-FR"));
            Assert.AreEqual(7.7d * 4d, (double)result, Delta);
        }
        // Throws tests
        [TestMethod]
        public void MathConverter_InvalidExpression_Throws()
        {
            Assert.ThrowsException<InvalidMathExpressionException>(() =>
                Converter.Convert(5, typeof(double), "oops * 5"));
        }

        [TestMethod]
        public void MathConverter_InvalidExpressionType_Throws()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
                Converter.Convert(5, typeof(double), new object()));
        }


        [TestMethod]
        public void MathConverter_AnyOperation_Caches()
        {
            var longExpression = "value / 42 * 24 / 42 - 52 * 42 + 8 * (7 + 5)";
            var result = Converter.Convert(5, typeof(int), longExpression);
            var privateType = new PrivateType(typeof(MathExpressionConverter));
            var dictionary = privateType.GetStaticField("CachedExpressions") as IDictionary;
            Assert.IsTrue(dictionary.Keys.Cast<IEquatable<string>>().Any(o => o.Equals(longExpression)));
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
