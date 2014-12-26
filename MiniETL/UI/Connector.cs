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

		private DesignerCanvas _designerCanvas;
		
		private DesignerCanvas DesignerCanvas
		{
			get { return _designerCanvas ?? (_designerCanvas = GetDesignerCanvas(this)); }
		}

		public ConnectorOrientation Orientation { get; set; }

		public Connector()
		{
			LayoutUpdated += ConnectorLayoutUpdated;
		}

		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			if (DesignerCanvas != null && ConnectorInfo.CanStartConnection)
			{
				DesignerCanvas.SourceConnector = this;
				IsBuildingConnection = true;
			}

			e.Handled = true;
		}

		protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
			if (DesignerCanvas == null)
				return;

			if (ConnectorInfo.CanEndConnection)
			{
				var fullyCreatedSourceInfo = (FullyCreatedConnectorInfo) DesignerCanvas.SourceConnector.DataContext;

				var diagram = fullyCreatedSourceInfo.DesignerItem.Diagram;

				var newConnection = new ConnectionViewModel(fullyCreatedSourceInfo.DesignerItem.Diagram,
					DesignerCanvas.SourceConnector.ConnectorInfo, ConnectorInfo);

				diagram.AddItemCommand.Execute(newConnection);
			}
			
			DesignerCanvas.ResetPartialConnection();

			e.Handled = true;
		}

		protected override void OnMouseEnter(MouseEventArgs e)
		{
			UpdateEnabledForConnection();

			if (e.LeftButton != MouseButtonState.Pressed)
				return;

			IsBuildingConnection = true;

			if (DesignerCanvas != null)
			{
				if (!ReferenceEquals(this, DesignerCanvas.SourceConnector))
					DesignerCanvas.SinkConnector = this;
			}

			e.Handled = true;
		}

		protected override void OnMouseLeave(MouseEventArgs e)
		{
			if (DesignerCanvas != null)
			{
				if (ReferenceEquals(this, DesignerCanvas.SinkConnector))
				{
					IsBuildingConnection = false;
					DesignerCanvas.SinkConnector = null;
				}
			}
		}

		private void UpdateEnabledForConnection()
		{
			EnabledForConnection =
				DesignerCanvas.HasPartialConnection && ConnectorInfo.CanEndConnection ||
				!DesignerCanvas.HasPartialConnection && ConnectorInfo.CanStartConnection;
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

		// connection started and current can end connection
		// connection not started and current can start connection

		public static readonly DependencyProperty EnabledForConnectionProperty = DependencyProperty.Register(
			"EnabledForConnection", typeof (bool), typeof (Connector), new PropertyMetadata(default(bool)));

		public bool EnabledForConnection
		{
			get { return (bool) GetValue(EnabledForConnectionProperty); }
			set { SetValue(EnabledForConnectionProperty, value); }
		}
	}
}
