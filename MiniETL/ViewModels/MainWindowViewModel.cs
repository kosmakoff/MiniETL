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
		private DiagramViewModel _diagramViewModel;

		public ToolboxViewModel ToolboxViewModel { get; private set; }

		public DiagramViewModel DiagramViewModel
		{
			get { return _diagramViewModel; }
			set
			{
				if (Equals(value, _diagramViewModel)) return;
				_diagramViewModel = value;
				OnPropertyChanged();
			}
		}

		public MainWindowViewModel()
		{
			ToolboxViewModel = new ToolboxViewModel();
			DiagramViewModel = new DiagramViewModel();

			InitCommands();
		}

		public SimpleCommand DeleteSelectedItemsCommand { get; private set; }

		private void InitCommands()
		{
			DeleteSelectedItemsCommand = new SimpleCommand(ExecuteDeleteSelectedItemsCommand);
		}

		private void ExecuteDeleteSelectedItemsCommand(object parameter)
		{
			var itemsToRemove = DiagramViewModel.SelectedItems;

			// TODO: select connections and remove them, too

			foreach (var item in itemsToRemove)
			{
				DiagramViewModel.RemoveItemCommand.Execute(item);
			}
		}
	}
}
