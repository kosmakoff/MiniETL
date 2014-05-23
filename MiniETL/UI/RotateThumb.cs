using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using MiniETL.Adorners;

namespace MiniETL.UI
{
	public class RotateThumb : Thumb
	{
		private Adorner _adorner;
		private Canvas _canvas;
		private Point _centerPoint;
		private ContentControl _designerItem;
		private double _initialAngle;
		private RotateTransform _rotateTransform;
		private Vector _startVector;

		public RotateThumb()
		{
			DragDelta += RotateThumb_DragDelta;
			DragStarted += RotateThumb_DragStarted;
			DragCompleted += RotateThumb_DragCompleted;
		}

		private void RotateThumb_DragStarted(object sender, DragStartedEventArgs e)
		{
			_designerItem = DataContext as ContentControl;

			if (_designerItem == null) return;

			_canvas = VisualTreeHelper.GetParent(_designerItem) as Canvas;

			if (_canvas != null)
			{
				_centerPoint = _designerItem.TranslatePoint(
					new Point(_designerItem.Width*_designerItem.RenderTransformOrigin.X,
						_designerItem.Height*_designerItem.RenderTransformOrigin.Y),
					_canvas);

				Point startPoint = Mouse.GetPosition(_canvas);
				_startVector = Point.Subtract(startPoint, _centerPoint);

				_rotateTransform = _designerItem.RenderTransform as RotateTransform;
				if (_rotateTransform == null)
				{
					_designerItem.RenderTransform = new RotateTransform(0);
					_initialAngle = 0;
				}
				else
				{
					_initialAngle = _rotateTransform.Angle;
				}

				AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(_canvas);
				if (adornerLayer != null)
				{
					_adorner = new RotationAdorner(_designerItem);
					adornerLayer.Add(_adorner);
				}
			}
		}

		private void RotateThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			if (_designerItem != null && _canvas != null)
			{
				Point currentPoint = Mouse.GetPosition(_canvas);
				Vector deltaVector = Point.Subtract(currentPoint, _centerPoint);

				double angle = Vector.AngleBetween(_startVector, deltaVector);

				var rotateTransform = _designerItem.RenderTransform as RotateTransform;
				rotateTransform.Angle = _initialAngle + Math.Round(angle, 0);
				_designerItem.InvalidateMeasure();
			}

			e.Handled = true;
		}

		private void RotateThumb_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			if (_adorner == null) return;

			AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(_canvas);
			if (adornerLayer != null)
			{
				adornerLayer.Remove(_adorner);
			}

			_adorner = null;
		}
	}
}