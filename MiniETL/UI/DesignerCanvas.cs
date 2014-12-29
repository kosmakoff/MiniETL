using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using MiniETL.Adorners;
using MiniETL.Components;
using MiniETL.ViewModels;

namespace MiniETL.UI
{
	public class DesignerCanvas : Canvas
	{
		private Point? _rubberbandSelectionStartPoint;
		
		private Connector _sourceConnector;
		private Connector _sinkConnector;

		private ConnectionViewModel _partialConnection;

		public DesignerCanvas()
		{
			AllowDrop = true;
		}

		public Connector SourceConnector
		{
			get { return _sourceConnector; }
			set
			{
				if (value == null)
				{
					_sourceConnector = null;
					return;
				}

				if (ReferenceEquals(_sourceConnector, value)) return;

				_sourceConnector = value;

				var sourceConnector = (FullyCreatedConnectorInfo) _sourceConnector.DataContext;
				var point = sourceConnector.GetConnectionPoint();
				_partialConnection = new ConnectionViewModel(sourceConnector,
					new PartCreatedConnectorInfo(ConnectorKind.Input, sourceConnector.ConnectorDataType, point));

				sourceConnector.DesignerItem.Diagram.AddItemCommand.Execute(_partialConnection);
			}
		}

		public Connector SinkConnector
		{
			get { return _sinkConnector; }
			set
			{
				if (ReferenceEquals(_sinkConnector, value)) return;

				_sinkConnector = value;
			}
		}

		public bool HasPartialConnection { get { return _partialConnection != null; } }

		public void ResetPartialConnection()
		{
			if (_partialConnection != null)
			{
				var diagram = _partialConnection.SourceConnectorInfo.DesignerItem.Diagram;
				diagram.RemoveItemCommand.Execute(_partialConnection);
			}

			_partialConnection = null;

			if (SourceConnector != null)
				SourceConnector.IsBuildingConnection = false;

			if (SinkConnector != null)
				SinkConnector.IsBuildingConnection = false;
			
			SourceConnector = null;
			SinkConnector = null;
		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.LeftButton == MouseButtonState.Pressed && ReferenceEquals(e.Source, this))
			{
				_rubberbandSelectionStartPoint = e.GetPosition(this);

				var vm = (IDiagramViewModel) DataContext;
				if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
					vm.ClearSelectedItemsCommand.Execute(null);
				
				e.Handled = true;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (SourceConnector != null)
			{
				if (e.LeftButton == MouseButtonState.Pressed)
				{
					//if (!IsMouseCaptured)
					//	CaptureMouse();

					var sourceConnectorInfo = (FullyCreatedConnectorInfo) SourceConnector.DataContext;
					var currentPoint = e.GetPosition(this);
					_partialConnection.SinkConnectorInfo = new PartCreatedConnectorInfo(ConnectorKind.Input, sourceConnectorInfo.ConnectorDataType, currentPoint);

					// HitTesting(currentPoint);
				}
			}
			else
			{
				if (e.LeftButton != MouseButtonState.Pressed)
				{
					_rubberbandSelectionStartPoint = null;
				}

				if (_rubberbandSelectionStartPoint.HasValue)
				{
					AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
					if (adornerLayer != null)
					{
						var adorner = new RubberbandAdorner(this, _rubberbandSelectionStartPoint);
						adornerLayer.Add(adorner);
					}
				}
			}

			e.Handled = true;
		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			//if (IsMouseCaptured)
			//	ReleaseMouseCapture();

			if (SourceConnector != null)
				SourceConnector.IsBuildingConnection = false;

			if (SinkConnector != null)
				SinkConnector.IsBuildingConnection = false;

			//if (SourceConnector != null && SinkConnector != null && SinkConnector.ConnectorInfo.CanEndConnection)
			//{
			//	var fullyCreatedSourceInfo = SourceConnector.ConnectorInfo;

			//	var diagram = fullyCreatedSourceInfo.DesignerItem.Diagram;

			//	var newConnection = new ConnectionViewModel(fullyCreatedSourceInfo.DesignerItem.Diagram,
			//		SourceConnector.ConnectorInfo, SinkConnector.ConnectorInfo);

			//	diagram.AddItemCommand.Execute(newConnection);
			//}

			if (SourceConnector != null && _partialConnection != null)
			{
				var sourceConnectorInfo = (FullyCreatedConnectorInfo) SourceConnector.DataContext;
				sourceConnectorInfo.DesignerItem.Diagram.RemoveItemCommand.Execute(_partialConnection);
			}

			_partialConnection = null;
			SourceConnector = null;
			SinkConnector = null;
		}

		protected override void OnDrop(DragEventArgs e)
		{
			base.OnDrop(e);

			var compgen = e.Data.GetData(typeof (ComponentGeneratorBase)) as ComponentGeneratorBase;

			if (compgen == null)
				return;

			((IDiagramViewModel)DataContext).ClearSelectedItemsCommand.Execute(null);
			Point position = e.GetPosition(this);

			var diagramViewModel = (DiagramViewModel)DataContext;

			var component = compgen.GenerateComponent();
			var viewModel = new DesignerItemViewModel(diagramViewModel, component)
			{
				Left = Math.Max(0, position.X - DesignerItemViewModel.MinWidth/2),
				Top = Math.Max(0, position.Y - DesignerItemViewModel.MinHeight/2),
				Width = DesignerItemViewModel.MinWidth,
				Height = DesignerItemViewModel.MinHeight,
				IsSelected = true
			};

			component.Name = string.Format("{0} {1}", compgen.DisplayName, diagramViewModel.GetNextCounter(component.GetType()));

			diagramViewModel.AddItemCommand.Execute(viewModel);

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

		//private void HitTesting(Point hitPoint)
		//{
		//	var hitObject = InputHitTest(hitPoint) as DependencyObject;

		//	while (hitObject != null && !(hitObject is DesignerCanvas))
		//	{
		//		var connector = hitObject as Connector;
		//		if (connector != null)
		//		{
		//			connector.UpdateEnabledForConnection();

		//			if (!ReferenceEquals(connector, SourceConnector))
		//			{
		//				SinkConnector = connector;
		//				SinkConnector.IsBuildingConnection = true;

		//				return;
		//			}
		//		}
		//		hitObject = VisualTreeHelper.GetParent(hitObject);
		//	}

		//	if (SinkConnector != null)
		//		SinkConnector.IsBuildingConnection = false;

		//	SinkConnector = null;
		//}
	}
}