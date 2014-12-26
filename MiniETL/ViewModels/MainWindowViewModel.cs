using System.Collections.Generic;
using System.Linq;
using MiniETL.Utils.PathFinding;

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

			ConnectionViewModel.PathFinder = new StraightLinePathFinder();
		}

		public SimpleCommand DeleteSelectedItemsCommand { get; private set; }

		private void InitCommands()
		{
			DeleteSelectedItemsCommand = new SimpleCommand(ExecuteDeleteSelectedItemsCommand);
		}

		private void ExecuteDeleteSelectedItemsCommand(object parameter)
		{
			var selectedDesignerItems = DiagramViewModel.SelectedItems;

			var connectionsToRemove = DiagramViewModel.Items.OfType<ConnectionViewModel>()
				.Where(connectionViewModel =>
					selectedDesignerItems.Contains(connectionViewModel.SourceConnectorInfo.DesignerItem) ||
					selectedDesignerItems.Contains(((FullyCreatedConnectorInfo) connectionViewModel.SinkConnectorInfo).DesignerItem));

			var itemsToRemove = new List<SelectableDesignerItemViewModelBase>();

			itemsToRemove.AddRange(selectedDesignerItems);
			itemsToRemove.AddRange(connectionsToRemove);

			foreach (var item in itemsToRemove)
			{
				DiagramViewModel.RemoveItemCommand.Execute(item);
			}
		}
	}
}
