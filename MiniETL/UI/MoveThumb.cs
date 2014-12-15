using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using MiniETL.ViewModels;

namespace MiniETL.UI
{
	public class MoveThumb : Thumb
	{
		public MoveThumb()
		{
			DragDelta += MoveThumb_DragDelta;
		}

		private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			var designerItem = DataContext as DesignerItemViewModel;

			if (designerItem != null && designerItem.IsSelected)
			{
				double minLeft = double.MaxValue;
				double minTop = double.MaxValue;

				// we only move DesignerItems
				var designerItems = designerItem.SelectedItems;

				foreach (var item in designerItems.OfType<DesignerItemViewModel>())
				{
					double left = item.Left;
					double top = item.Top;
					minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
					minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);

					double deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
					double deltaVertical = Math.Max(-minTop, e.VerticalChange);
					item.Left += deltaHorizontal;
					item.Top += deltaVertical;
				}
				e.Handled = true;
			}
		}
	}
}