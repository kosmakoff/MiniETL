using System.Windows;
using System.Windows.Controls;

namespace MiniETL.Adorners
{
	public class ResizeChrome : Control
	{
		static ResizeChrome()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeChrome), new FrameworkPropertyMetadata(typeof(ResizeChrome)));
		}
	}
}
