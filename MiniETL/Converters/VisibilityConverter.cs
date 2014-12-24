using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MiniETL.Converters
{
	[ValueConversion(typeof (bool), typeof (Visibility), ParameterType = typeof (bool))]
	public class VisibilityConverter : ConverterBase<VisibilityConverter>
	{
		private static readonly NullableBoolConverter BoolConverter = new NullableBoolConverter();

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var visible = (bool) value;
			var invert = ((bool?)BoolConverter.ConvertFrom(parameter)).GetValueOrDefault();

			return visible && !invert || !visible && invert
				? Visibility.Visible
				: Visibility.Collapsed;
		}
	}
}
