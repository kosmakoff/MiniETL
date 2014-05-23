using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MiniETL.UI
{
	public class MoveThumb : Thumb
	{
		private ContentControl _designerItem;
		private RotateTransform _rotateTransform;

		public MoveThumb()
		{
			DragStarted += MoveThumb_DragStarted;
			DragDelta += MoveThumb_DragDelta;
		}

		private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
		{
			_designerItem = DataContext as ContentControl;

			if (_designerItem != null)
			{
				_rotateTransform = _designerItem.RenderTransform as RotateTransform;
			}
		}

		private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			if (_designerItem != null)
			{
				var dragDelta = new Point(e.HorizontalChange, e.VerticalChange);

				if (_rotateTransform != null)
				{
					dragDelta = _rotateTransform.Transform(dragDelta);
				}

				Canvas.SetLeft(_designerItem, Canvas.GetLeft(_designerItem) + dragDelta.X);
				Canvas.SetTop(_designerItem, Canvas.GetTop(_designerItem) + dragDelta.Y);
			}
		}
	}
}