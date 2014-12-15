using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MiniETL.ViewModels
{
	public interface IDiagramViewModel
	{
		ICommand AddItemCommand { get; }
		ICommand RemoveItemCommand { get; }
		ICommand ClearSelectedItemsCommand { get; }
		ICommand CreateNewDiagramCommand { get; }

		List<SelectableDesignerItemViewModelBase> SelectedItems { get; }
		ObservableCollection<SelectableDesignerItemViewModelBase> Items { get; }
	}
}
