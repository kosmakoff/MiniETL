using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MiniETL.ViewModels;

namespace MiniETL.UI
{
	public class Connector : Control
	{
		public ConnectorOrientation Orientation { get; set; }

		public Connector()
		{
			LayoutUpdated += ConnectorLayoutUpdated;
		}

		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			base.OnMouseLeftButtonDown(e);
			DesignerCanvas canvas = GetDesignerCanvas(this);
			if (canvas != null)
			{
				canvas.SourceConnector = this;
			}
		}

		// iterate through visual tree to get parent DesignerCanvas
		private DesignerCanvas GetDesignerCanvas(DependencyObject element)
		{
			while (element != null && !(element is DesignerCanvas))
				element = VisualTreeHelper.GetParent(element);

			return element as DesignerCanvas;
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
	}
}
