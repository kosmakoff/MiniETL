using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace MiniETL.UI
{
	public class ZoomBox : Control
	{
		private ScaleTransform _scaleTransform;
		private Canvas _zoomCanvas;
		private Slider _zoomSlider;
		private Thumb _zoomThumb;

		#region ScrollViewer

		public static readonly DependencyProperty ScrollViewerProperty = DependencyProperty.Register(
			"ScrollViewer", typeof (ScrollViewer), typeof (ZoomBox));

		public ScrollViewer ScrollViewer
		{
			get { return (ScrollViewer) GetValue(ScrollViewerProperty); }
			set { SetValue(ScrollViewerProperty, value); }
		}

		#endregion

		#region DesignerCanvas

		public static readonly DependencyProperty DesignerCanvasProperty = DependencyProperty.Register(
			"DesignerCanvas", typeof (DesignerCanvas), typeof (ZoomBox),
			new FrameworkPropertyMetadata(default(DesignerCanvas), OnDesignerCanvasChanged));

		public DesignerCanvas DesignerCanvas
		{
			get { return (DesignerCanvas) GetValue(DesignerCanvasProperty); }
			set { SetValue(DesignerCanvasProperty, value); }
		}

		private static void OnDesignerCanvasChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var target = (ZoomBox)d;
			var oldDesignerCanvas = (DesignerCanvas)e.OldValue;
			DesignerCanvas newDesignerCanvas = target.DesignerCanvas;
			target.OnDesignerCanvasChanged(oldDesignerCanvas, newDesignerCanvas);
		}

		protected virtual void OnDesignerCanvasChanged(DesignerCanvas oldDesignerCanvas, DesignerCanvas newDesignerCanvas)
		{
			if (oldDesignerCanvas != null)
			{
				oldDesignerCanvas.LayoutUpdated -= DesignerCanvas_LayoutUpdated;
				oldDesignerCanvas.MouseWheel -= DesignerCanvas_MouseWheel;
			}

			if (newDesignerCanvas != null)
			{
				newDesignerCanvas.LayoutUpdated += DesignerCanvas_LayoutUpdated;
				newDesignerCanvas.MouseWheel += DesignerCanvas_MouseWheel;
				newDesignerCanvas.LayoutTransform = _scaleTransform;
			}
		}

		#endregion

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (ScrollViewer == null)
				return;

			_zoomThumb = Template.FindName("PART_ZoomThumb", this) as Thumb;
			if (_zoomThumb == null)
				throw new Exception("PART_ZoomThumb template is missing!");

			_zoomCanvas = Template.FindName("PART_ZoomCanvas", this) as Canvas;
			if (_zoomCanvas == null)
				throw new Exception("PART_ZoomCanvas template is missing!");

			_zoomSlider = Template.FindName("PART_ZoomSlider", this) as Slider;
			if (_zoomSlider == null)
				throw new Exception("PART_ZoomSlider template is missing!");

			_zoomThumb.DragDelta += Thumb_DragDelta;
			_zoomSlider.ValueChanged += ZoomSlider_ValueChanged;
			_scaleTransform = new ScaleTransform();
		}

		private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			double scale = e.NewValue / e.OldValue;
			double halfViewportHeight = ScrollViewer.ViewportHeight / 2;
			double newVerticalOffset = ((ScrollViewer.VerticalOffset + halfViewportHeight) * scale - halfViewportHeight);
			double halfViewportWidth = ScrollViewer.ViewportWidth / 2;
			double newHorizontalOffset = ((ScrollViewer.HorizontalOffset + halfViewportWidth) * scale - halfViewportWidth);
			_scaleTransform.ScaleX *= scale;
			_scaleTransform.ScaleY *= scale;
			ScrollViewer.ScrollToHorizontalOffset(newHorizontalOffset);
			ScrollViewer.ScrollToVerticalOffset(newVerticalOffset);
		}

		private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			double scale, xOffset, yOffset;
			InvalidateScale(out scale, out xOffset, out yOffset);
			ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.HorizontalOffset + e.HorizontalChange / scale);
			ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset + e.VerticalChange / scale);
		}

		private void DesignerCanvas_LayoutUpdated(object sender, EventArgs e)
		{
			double scale, xOffset, yOffset;
			InvalidateScale(out scale, out xOffset, out yOffset);
			_zoomThumb.Width = ScrollViewer.ViewportWidth * scale;
			_zoomThumb.Height = ScrollViewer.ViewportHeight * scale;
			Canvas.SetLeft(_zoomThumb, xOffset + ScrollViewer.HorizontalOffset * scale);
			Canvas.SetTop(_zoomThumb, yOffset + ScrollViewer.VerticalOffset * scale);
		}

		private void DesignerCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			// TODO: what the hell did the author want to say with this code???
			//divide the value by 10 so that it is more smooth
			double value = Math.Max(0, e.Delta / 10);
			value = Math.Min(e.Delta, 10);
			_zoomSlider.Value += value;
		}

		private void InvalidateScale(out double scale, out double xOffset, out double yOffset)
		{
			double w = DesignerCanvas.ActualWidth * _scaleTransform.ScaleX;
			double h = DesignerCanvas.ActualHeight * _scaleTransform.ScaleY;

			// zoom canvas size
			double x = _zoomCanvas.ActualWidth;
			double y = _zoomCanvas.ActualHeight;
			double scaleX = x / w;
			double scaleY = y / h;
			scale = (scaleX < scaleY) ? scaleX : scaleY;
			xOffset = (x - scale * w) / 2;
			yOffset = (y - scale * h) / 2;
		}
	}
}
