using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MiniETL.Converters
{
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public class VisibilityConverter : IValueConverter
	{
		public bool IsInverted { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var visible = (bool)value;

			return visible && !IsInverted || !visible && IsInverted
				? Visibility.Visible
				: Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
