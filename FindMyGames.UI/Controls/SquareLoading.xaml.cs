using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheMvvmGuys.FindMyGames.UI.Controls
{
    /// <summary>
    /// Logique d'interaction pour SquareLoading.xaml
    /// </summary>
    public partial class SquareLoading : UserControl
    {
        public SquareLoading()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SquareBackgroundProperty = DependencyProperty.Register(
            "SquareBackground", typeof(Brush), typeof(SquareLoading), new UIPropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush SquareBackground
        {
            get { return (Brush) GetValue(SquareBackgroundProperty); }
            set { SetValue(SquareBackgroundProperty, value); }
        }

        public static readonly DependencyProperty SquareBorderBrushProperty = DependencyProperty.Register(
            "SquareBorderBrush", typeof(Brush), typeof(SquareLoading), new UIPropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush SquareBorderBrush
        {
            get { return (Brush) GetValue(SquareBorderBrushProperty); }
            set { SetValue(SquareBorderBrushProperty, value); }
        }
    }
}
