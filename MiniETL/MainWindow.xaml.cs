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
			ViewModel.DiagramViewModel.AddItemCommand.Execute(
				new DesignerItemViewModel
				{
					Component = new FileInputComponent {Name = "First Input", FileName = @"c:\temp\1.png"},
					Top = 100,
					Left = 100,
					Width = 200,
					Height = 150
				});
		}
	}
}
