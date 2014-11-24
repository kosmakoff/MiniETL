using System;
using System.Windows;
using System.Windows.Controls;
using MiniETL.ViewModels;

namespace MiniETL.StyleSelectors
{
	public class DesignerItemsControlItemStyleSelector : StyleSelector
	{
		static DesignerItemsControlItemStyleSelector()
		{
			Instance = new DesignerItemsControlItemStyleSelector();
		}

		public static DesignerItemsControlItemStyleSelector Instance { get; private set; }

		public override Style SelectStyle(object item, DependencyObject container)
		{
			var itemsControl = ItemsControl.ItemsControlFromItemContainer(container);
			if (itemsControl == null)
				throw new InvalidOperationException("DesignerItemsControlItemStyleSelector : Could not find ItemsControl");

			if (item is DesignerItemViewModelBase)
			{
				return (Style)itemsControl.FindResource("designerItemStyle");
			}

			if (item is ConnectorViewModel)
			{
				return (Style)itemsControl.FindResource("connectorItemStyle");
			}

			return null;
		}
	}
}
