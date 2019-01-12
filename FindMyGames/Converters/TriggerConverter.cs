using System;
using System.Windows.Data;
// https://stackoverflow.com/questions/335849/wpf-commandparameter-is-null-first-time-canexecute-is-called
// thancc stacc overflow
namespace TheMvvmGuys.FindMyGames.Converters
{
    public class TriggerConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            // First value is target value.
            // All others are update triggers only.
            if (values.Length < 1) return Binding.DoNothing;
            return values[0];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}