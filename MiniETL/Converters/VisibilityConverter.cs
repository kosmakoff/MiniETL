using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MiniETL.Converters
{
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public class VisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var visible = (bool)value;
			var invert = parameter != null && (bool)parameter;

			return visible && !invert || !visible && invert
				? Visibility.Visible
				: Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
