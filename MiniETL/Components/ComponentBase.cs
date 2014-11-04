using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiniETL.Components
{
	public abstract class ComponentBase : DependencyObject
	{
		public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
			"Name", typeof (string), typeof (ComponentBase), new PropertyMetadata(default(string)));

		public string Name
		{
			get { return (string) GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}
	}
}
