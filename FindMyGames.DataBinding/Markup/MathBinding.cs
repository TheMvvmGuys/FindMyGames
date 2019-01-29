using System.Windows.Data;
using TheMvvmGuys.FindMyGames.DataBinding.Converters;

namespace TheMvvmGuys.FindMyGames.DataBinding.Markup
{
    public class MathBinding : Binding
    {
        public string Expression
        {
            get => ConverterParameter as string;
            set => ConverterParameter = value;
        }

        private void Initialize()
        {
            Converter = new MathExpressionConverter();
        }
        public MathBinding()
        {
            Initialize();
        }

        public MathBinding(string path) : base(path)
        {
            Initialize();
        }

        public MathBinding(string path, string expression) : this(path)
        {
            Expression = expression;
        }
    }
}