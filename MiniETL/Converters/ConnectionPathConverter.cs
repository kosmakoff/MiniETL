using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MiniETL.Converters
{
	[ValueConversion(typeof (List<Point>), typeof (PathSegmentCollection))]
	public class ConnectionPathConverter : ConverterBase<ConnectionPathConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			List<Point> points = (List<Point>) value;

			return points == null ? new PointCollection(0) : new PointCollection(points);
		}
	}
}
