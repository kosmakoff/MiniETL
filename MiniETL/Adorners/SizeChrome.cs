using System.Windows;
using System.Windows.Controls;

namespace MiniETL.Adorners
{
	public class SizeChrome : Control
	{
		static SizeChrome()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(SizeChrome), new FrameworkPropertyMetadata(typeof(SizeChrome)));
		}
	}
}
