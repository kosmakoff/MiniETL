﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using MiniETL.Adorners;
using MiniETL.Components;
using MiniETL.UI.DiagramDesigner.Controls;
using MiniETL.ViewModels;

namespace MiniETL.UI
{
	public class DesignerCanvas : Canvas
	{
		private Point? _dragStartPoint;

		public DesignerCanvas()
		{
			AllowDrop = true;
		}

		public Connector SourceConnector { get; set; }

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.LeftButton == MouseButtonState.Pressed && e.Source == this)
			{
				_dragStartPoint = e.GetPosition(this);

				var vm = (IDiagramViewModel) DataContext;
				if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
					vm.ClearSelectedItemsCommand.Execute(null);
				
				e.Handled = true;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.LeftButton != MouseButtonState.Pressed)
			{
				_dragStartPoint = null;
			}

			if (_dragStartPoint.HasValue)
			{
				AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
				if (adornerLayer != null)
				{
					var adorner = new RubberbandAdorner(this, _dragStartPoint);
					adornerLayer.Add(adorner);
				}

				e.Handled = true;
			}
		}

		protected override void OnDrop(DragEventArgs e)
		{
			base.OnDrop(e);

			var compgen = e.Data.GetData(typeof (ComponentGeneratorBase)) as ComponentGeneratorBase;

			if (compgen == null)
				return;

			var diagramViewModel = (DiagramViewModel)DataContext;
			ComponentBase component = compgen.GenerateComponent();
			component.Name = string.Format("{0} {1}", compgen.DisplayName, diagramViewModel.GetNextCounter(component.GetType()));

			/*
			var designerItem = new DesignerItem {Content = component};

			Point position = e.GetPosition(this);

			designerItem.Width = 150;
			designerItem.Height = 100;

			SetLeft(designerItem, Math.Max(0, position.X - designerItem.Width/2));
			SetTop(designerItem, Math.Max(0, position.Y - designerItem.Height/2));
			Children.Add(designerItem);

			DeselectAll();
			designerItem.IsSelected = true;
			*/

			((IDiagramViewModel) DataContext).ClearSelectedItemsCommand.Execute(null);
			Point position = e.GetPosition(this);




			e.Handled = true;
		}

		protected override Size MeasureOverride(Size constraint)
		{
			var size = new Size();
			foreach (UIElement element in Children)
			{
				double left = GetLeft(element);
				double top = GetTop(element);
				left = double.IsNaN(left) ? 0 : left;
				top = double.IsNaN(top) ? 0 : top;

				element.Measure(constraint);

				Size desiredSize = element.DesiredSize;
				if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
				{
					size.Width = Math.Max(size.Width, left + desiredSize.Width);
					size.Height = Math.Max(size.Height, top + desiredSize.Height);
				}
			}

			// add some extra margin
			size.Width += 10;
			size.Height += 10;
			return size;
		}
	}
}