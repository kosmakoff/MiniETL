using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MiniETL.Converters
{
	[ValueConversion(typeof(bool), typeof(GridLength), ParameterType = typeof(GridLength))]
	public class GridLengthConverter : IValueConverter
	{
		private static readonly NullableBoolConverter BoolConverter = new NullableBoolConverter();

		public GridLength GridLengthWhenTrue { get; set; }
		public GridLength GridLengthWhenFalse { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var boolValue = ((bool?)BoolConverter.ConvertFrom(value)).GetValueOrDefault();

			return boolValue
				? GridLengthWhenTrue
				: GridLengthWhenFalse;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
