using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using MiniETL.Adorners;
using MiniETL.ViewModels;

namespace MiniETL.UI
{
	public class ResizeThumb : Thumb
	{
		public ResizeThumb()
		{
			DragDelta += ResizeThumb_DragDelta;
		}

		private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			var designerItem = DataContext as DesignerItemViewModel;

			if (designerItem != null)
			{
				double delta;

				switch (VerticalAlignment)
				{
					case VerticalAlignment.Bottom:
						delta = Math.Min(-e.VerticalChange, designerItem.Height - DesignerItemViewModel.MinHeight);
						
						designerItem.Height -= delta;
						break;
					case VerticalAlignment.Top:
						delta = Math.Min(e.VerticalChange, designerItem.Height - DesignerItemViewModel.MinHeight);

						designerItem.Top += delta;
						designerItem.Height -= delta;
						break;
				}

				switch (HorizontalAlignment)
				{
					case HorizontalAlignment.Left:
						delta = Math.Min(e.HorizontalChange, designerItem.Width - DesignerItemViewModel.MinWidth);

						designerItem.Left += delta;
						designerItem.Width -= delta;
						break;

					case HorizontalAlignment.Right:
						delta = Math.Min(-e.HorizontalChange, designerItem.Width - DesignerItemViewModel.MinWidth);

						designerItem.Width -= delta;
						break;
				}
			}

			e.Handled = true;
		}
	}
}