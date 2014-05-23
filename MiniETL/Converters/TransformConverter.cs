using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MiniETL.Converters
{
	public class TransformConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var rotateTransform = value as RotateTransform;
			
			if (rotateTransform != null)
				return rotateTransform.Angle.ToString("0");

			return "0";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
