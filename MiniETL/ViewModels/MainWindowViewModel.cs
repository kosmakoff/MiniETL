using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiniETL.ViewModels
{
	public class MainWindowViewModel : INPCBase
	{
		public ToolboxViewModel ToolboxViewModel { get; private set; }

		public MainWindowViewModel()
		{
			ToolboxViewModel = new ToolboxViewModel();

			InitCommands();
		}

		public SimpleCommand DeleteSelectedItemsCommand { get; private set; }

		private void InitCommands()
		{
			DeleteSelectedItemsCommand = new SimpleCommand(ExecuteDeleteSelectedItemsCommand);
		}

		private void ExecuteDeleteSelectedItemsCommand(object parameter)
		{
			MessageBox.Show(App.Current.MainWindow, "Not implemented yet");
		}
	}
}
