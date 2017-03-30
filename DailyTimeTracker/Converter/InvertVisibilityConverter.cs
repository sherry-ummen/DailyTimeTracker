using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DailyTimeTracker.Converter {
    public class InvertVisibilityConverter : IValueConverter {

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) {
            if (targetType == typeof(Visibility)) {
                return (bool)value ?  Visibility.Collapsed : Visibility.Visible;
            }
            throw new InvalidOperationException("Converter can only convert to value of type Visibility.");
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) {
            throw new Exception("Invalid call - one way only");
        }
    }

    public class BoolToOppositeBoolConverter : IValueConverter {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture) {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }

        #endregion
    }

}
