using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MiniETL.ViewModels;

namespace MiniETL.UI
{
	public class Connector : Control
	{
		#region Statics-constants
		public static double ConnectorWidth = 7d;
		public static double ConnectorHeight = 16;
		public static Thickness InputConnectorOffset = new Thickness(-10, -8, 0, 0);
		public static Thickness OutputConnectorOffset = new Thickness(1, -8, 0, 0);
		#endregion

		public ConnectorOrientation Orientation { get; set; }

		public Connector()
		{
			LayoutUpdated += ConnectorLayoutUpdated;
		}

		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			DesignerCanvas canvas = GetDesignerCanvas(this);
			if (canvas != null && ConnectorInfo.CanStartConnection)
			{
				canvas.SourceConnector = this;
				IsBuildingConnection = true;
			}

			e.Handled = true;
		}

		protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
			DesignerCanvas canvas = GetDesignerCanvas(this);
			if (canvas == null)
				return;

			var fullyCreatedSourceInfo = (FullyCreatedConnectorInfo) canvas.SourceConnector.DataContext;

			var diagram = fullyCreatedSourceInfo.DesignerItem.Diagram;

			var newConnection = new ConnectionViewModel(fullyCreatedSourceInfo.DesignerItem.Diagram,
				canvas.SourceConnector.ConnectorInfo, ConnectorInfo);

			diagram.AddItemCommand.Execute(newConnection);

			canvas.ResetPartialConnection();
		}

		protected override void OnMouseEnter(MouseEventArgs e)
		{
			if (e.LeftButton != MouseButtonState.Pressed)
				return;

			IsBuildingConnection = true;

			DesignerCanvas canvas = GetDesignerCanvas(this);
			if (canvas != null)
			{
				canvas.SinkConnector = this;
			}

			e.Handled = true;
		}

		protected override void OnMouseLeave(MouseEventArgs e)
		{
			DesignerCanvas canvas = GetDesignerCanvas(this);

			if (canvas != null)
			{
				if (ReferenceEquals(this, canvas.SinkConnector))
				{
					IsBuildingConnection = false;
					canvas.SinkConnector = null;
				}
			}
		}

		// iterate through visual tree to get parent DesignerCanvas
		private static DesignerCanvas GetDesignerCanvas(DependencyObject element)
		{
			while (element != null && !(element is DesignerCanvas))
				element = VisualTreeHelper.GetParent(element);

			return (DesignerCanvas) element;
		}

		public FullyCreatedConnectorInfo ConnectorInfo
		{
			get { return (FullyCreatedConnectorInfo) DataContext; }
			set { DataContext = value; }
		}

		private void ConnectorLayoutUpdated(object sender, EventArgs e)
		{
			UpdateHotspot();
		}

		public static readonly DependencyProperty HotspotProperty = DependencyProperty.Register(
			"Hotspot", typeof (Point), typeof (Connector), new PropertyMetadata(default(Point)));

		public Point Hotspot
		{
			get { return (Point) GetValue(HotspotProperty); }
			set { SetValue(HotspotProperty, value); }
		}

		public static readonly DependencyProperty RootElementProperty = DependencyProperty.Register(
			"RootElement", typeof (FrameworkElement), typeof (Connector), new FrameworkPropertyMetadata(RootElementChanged));

		public FrameworkElement RootElement
		{
			get { return (FrameworkElement) GetValue(RootElementProperty); }
			set { SetValue(RootElementProperty, value); }
		}

		private static void RootElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var connector = (Connector) d;
			connector.UpdateHotspot();
		}

		private void UpdateHotspot()
		{
			if (RootElement == null)
				return;

			Hotspot = TransformToAncestor(RootElement).Transform(new Point(0, 0));
		}

		public static readonly DependencyProperty IsBuildingConnectionProperty = DependencyProperty.Register(
			"IsBuildingConnection", typeof (bool), typeof (Connector), new PropertyMetadata(default(bool)));

		public bool IsBuildingConnection
		{
			get { return (bool) GetValue(IsBuildingConnectionProperty); }
			set { SetValue(IsBuildingConnectionProperty, value); }
		}
	}
}
