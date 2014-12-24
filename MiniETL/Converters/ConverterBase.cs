using System;
using System.Globalization;
using System.Windows.Data;

namespace MiniETL.Converters
{
	public abstract class ConverterBase<T> : IValueConverter where T : class, new()
	{
		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		static ConverterBase()
		{
			Instance = new T();
		}

		public static T Instance { get; private set; }
	}
}
