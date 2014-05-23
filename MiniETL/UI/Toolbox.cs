using System.Windows;
using System.Windows.Controls;

namespace MiniETL.UI
{
	public class Toolbox : ItemsControl
	{
		public Toolbox()
		{
			DefaultItemSize = new Size(65, 65);
		}

		public Size DefaultItemSize { get; set; }

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new ToolboxItem();
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return (item is ToolboxItem);
		}
	}
}