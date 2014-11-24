using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MiniETL.Components;
using MiniETL.UI;

namespace MiniETL.AttachedProperties
{
	public static class DragAndDropProps
	{
		#region EnabledForDrag

		public static readonly DependencyProperty EnabledForDragProperty =
			DependencyProperty.RegisterAttached("EnabledForDrag", typeof(bool), typeof(DragAndDropProps),
				new FrameworkPropertyMetadata(false,
					OnEnabledForDragChanged));

		public static bool GetEnabledForDrag(DependencyObject d)
		{
			return (bool)d.GetValue(EnabledForDragProperty);
		}

		public static void SetEnabledForDrag(DependencyObject d, bool value)
		{
			d.SetValue(EnabledForDragProperty, value);
		}

		private static void OnEnabledForDragChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var fe = (FrameworkElement)d;


			if ((bool)e.NewValue)
			{
				fe.PreviewMouseDown += Fe_PreviewMouseDown;
				fe.MouseMove += Fe_MouseMove;
			}
			else
			{
				fe.PreviewMouseDown -= Fe_PreviewMouseDown;
				fe.MouseMove -= Fe_MouseMove;
			}
		}

		#endregion

		#region DragStartPoint

		public static readonly DependencyProperty DragStartPointProperty =
			DependencyProperty.RegisterAttached("DragStartPoint", typeof(Point?), typeof(DragAndDropProps));

		public static Point? GetDragStartPoint(DependencyObject d)
		{
			return (Point?)d.GetValue(DragStartPointProperty);
		}


		public static void SetDragStartPoint(DependencyObject d, Point? value)
		{
			d.SetValue(DragStartPointProperty, value);
		}

		#endregion

		private static void Fe_MouseMove(object sender, MouseEventArgs e)
		{
			Point? dragStartPoint = GetDragStartPoint((DependencyObject)sender);

			if (e.LeftButton != MouseButtonState.Pressed)
				dragStartPoint = null;

			if (dragStartPoint.HasValue)
			{
				object componentGenerator = ((ContentPresenter)sender).Content;
				var dataObject = new DataObject(typeof(ComponentGeneratorBase), componentGenerator);
				DragDrop.DoDragDrop((DependencyObject) sender, dataObject, DragDropEffects.Copy);
				e.Handled = true;
			}
		}

		private static void Fe_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			SetDragStartPoint((DependencyObject)sender, e.GetPosition((IInputElement)sender));
		}
	}
}
