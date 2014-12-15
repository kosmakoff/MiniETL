using System.Windows;
using System.Windows.Controls;

namespace MiniETL.UI
{
	/// <summary>
	/// Interaction logic for DiagramControl.xaml
	/// </summary>
	public partial class DiagramControl : UserControl
	{
		public DiagramControl()
		{
			InitializeComponent();
		}

		private void DesignerCanvas_OnLoaded(object sender, RoutedEventArgs e)
		{
			var myDesignerCanvas = sender as DesignerCanvas;
			ZoomBox.DesignerCanvas = myDesignerCanvas;
		}
	}
}
