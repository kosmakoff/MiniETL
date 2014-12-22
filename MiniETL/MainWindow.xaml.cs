using System.Windows;
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
		}
	}
}
