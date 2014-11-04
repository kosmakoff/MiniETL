using System;
using System.ComponentModel;
using System.Windows.Markup;

namespace MiniETL.MarkupExtensions
{
	[MarkupExtensionReturnType(typeof(string))]
	public class DisplayNameExtension : MarkupExtension
	{
		public Type Type { get; set; }

		public string PropertyName { get; set; }

		public DisplayNameExtension() { }

		public DisplayNameExtension(string propertyName)
		{
			PropertyName = propertyName;
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			var prop = Type.GetProperty(PropertyName);
			var attributes = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false);
			return ((DisplayNameAttribute)attributes[0]).DisplayName;
		}
	}
}
