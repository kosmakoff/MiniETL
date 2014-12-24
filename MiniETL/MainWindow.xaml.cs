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
			var inputFile = new DesignerItemViewModel(ViewModel.DiagramViewModel, new FileInputComponent {Name = "Input File"})
			{
				Width = 150,
				Height = 100,
				Left = 50,
				Top = 250
			};
			ViewModel.DiagramViewModel.Items.Add(inputFile);

			var capser = new DesignerItemViewModel(ViewModel.DiagramViewModel, new TextCapitalizerComponent {Name = "Capser"})
			{
				Width = 150,
				Height = 100,
				Left = 250,
				Top = 275
			};
			ViewModel.DiagramViewModel.Items.Add(capser);

			var connector1 = new ConnectionViewModel(ViewModel.DiagramViewModel, inputFile.OutputConnectors[0],
				capser.InputConnectors[0]);
			ViewModel.DiagramViewModel.Items.Add(connector1);
#endif
		}
	}
}
