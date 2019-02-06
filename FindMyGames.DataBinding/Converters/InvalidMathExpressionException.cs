using System;
using System.Runtime.Serialization;

namespace TheMvvmGuys.FindMyGames.DataBinding.Converters
{
    public class InvalidMathExpressionException : InvalidOperationException
    {
        public string Expression { get; }
        public InvalidMathExpressionException()
        {
        }

        public InvalidMathExpressionException(string message, string expression = null) : base(message)
        {
            Expression = expression;
        }

        public InvalidMathExpressionException(string message, Exception innerException, string expression = null) : base(message, innerException)
        {
            Expression = expression;
        }
    }
}