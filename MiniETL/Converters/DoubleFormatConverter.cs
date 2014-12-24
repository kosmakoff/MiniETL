using System;
using System.Globalization;
using System.Windows.Data;

namespace MiniETL.Converters
{
	[ValueConversion(typeof(double), typeof(double))]
	public class DoubleFormatConverter : ConverterBase<DoubleFormatConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double d = (double)value;
			return Math.Round(d);
		}
	}
}
