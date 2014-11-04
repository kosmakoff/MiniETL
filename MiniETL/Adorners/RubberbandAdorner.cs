using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using MiniETL.UI;

namespace MiniETL.Adorners
{
	public class RubberbandAdorner : Adorner
	{
		private readonly Canvas _adornerCanvas;
		private readonly DesignerCanvas _designerCanvas;
		private readonly Rectangle _rubberband;
		private readonly VisualCollection _visuals;
		private Point? _endPoint;
		private Point? _startPoint;

		public RubberbandAdorner(DesignerCanvas designerCanvas, Point? dragStartPoint) : base(designerCanvas)
		{
			_designerCanvas = designerCanvas;
			_startPoint = dragStartPoint;

			_adornerCanvas = new Canvas {Background = Brushes.Transparent};
			_visuals = new VisualCollection(this) {_adornerCanvas};

			var fillBrushColor = Colors.MediumBlue;
			fillBrushColor.A = 48;
			var fillBrush = new SolidColorBrush(fillBrushColor);

			var strokeBrushColor = Colors.Navy;
			strokeBrushColor.A = 64;
			var strokeBrush = new SolidColorBrush(strokeBrushColor);

			_rubberband = new Rectangle() {Stroke = strokeBrush, StrokeThickness = 2, Fill = fillBrush};

			_adornerCanvas.Children.Add(_rubberband);
		}

		protected override int VisualChildrenCount
		{
			get { return _visuals.Count; }
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				if (!IsMouseCaptured)
				{
					CaptureMouse();
				}

				_endPoint = e.GetPosition(this);
				UpdateRubberband();
				UpdateSelection();
				e.Handled = true;
			}
		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			if (IsMouseCaptured)
				ReleaseMouseCapture();

			var adornerLayer = Parent as AdornerLayer;
			if (adornerLayer != null)
				adornerLayer.Remove(this);
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			_adornerCanvas.Arrange(new Rect(finalSize));
			return finalSize;
		}

		protected override Visual GetVisualChild(int index)
		{
			return _visuals[index];
		}

		private void UpdateRubberband()
		{
			double left = Math.Min(_startPoint.Value.X, _endPoint.Value.X);
			double top = Math.Min(_startPoint.Value.Y, _endPoint.Value.Y);

			double width = Math.Abs(_startPoint.Value.X - _endPoint.Value.X);
			double height = Math.Abs(_startPoint.Value.Y - _endPoint.Value.Y);

			Canvas.SetLeft(_rubberband, left);
			Canvas.SetTop(_rubberband, top);

			_rubberband.Width = width;
			_rubberband.Height = height;
		}

		private void UpdateSelection()
		{
			var rubberBand = new Rect(_startPoint.Value, _endPoint.Value);
			foreach (DesignerItem item in _designerCanvas.Children)
			{
				Rect itemRect = VisualTreeHelper.GetDescendantBounds(item);
				Rect itemBounds = item.TransformToAncestor(_designerCanvas).TransformBounds(itemRect);

				item.IsSelected = rubberBand.Contains(itemBounds);
			}
		}
	}
}