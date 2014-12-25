using System.Windows;
using MiniETL.Components;
using MiniETL.ViewModels;

namespace MiniETL
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindowViewModel ViewModel { get; private set; }

		public MainWindow()
		{
			InitializeComponent();

			ViewModel = new MainWindowViewModel();
			DataContext = ViewModel;
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
#if DEBUG

			var fileInputComponent = new FileInputComponent {Name = "Input File"};
			var fileInputDesignerItem = new DesignerItemViewModel(ViewModel.DiagramViewModel, fileInputComponent)
			{
				Width = 150,
				Height = 100,
				Left = 50,
				Top = 250
			};
			ViewModel.DiagramViewModel.Items.Add(fileInputDesignerItem);

			var textCapitalizerComponent = new TextCapitalizerComponent {Name = "Capser"};
			var capserDesignerItem = new DesignerItemViewModel(ViewModel.DiagramViewModel, textCapitalizerComponent)
			{
				Width = 150,
				Height = 100,
				Left = 300,
				Top = 275
			};
			ViewModel.DiagramViewModel.Items.Add(capserDesignerItem);

			var connection1 = new ConnectionViewModel(ViewModel.DiagramViewModel, fileInputComponent.FileContentsOutputConnector,
				textCapitalizerComponent.StringInputConnector);
			ViewModel.DiagramViewModel.Items.Add(connection1);
#endif
		}
	}
}
