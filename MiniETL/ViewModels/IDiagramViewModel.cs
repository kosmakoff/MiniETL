using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MiniETL.ViewModels
{
	public interface IDiagramViewModel
	{
		SimpleCommand AddItemCommand { get; }
		SimpleCommand RemoveItemCommand { get; }
		SimpleCommand ClearSelectedItemsCommand { get; }
		List<SelectableDesignerItemViewModelBase> SelectedItems { get; }
		ObservableCollection<SelectableDesignerItemViewModelBase> Items { get; }
	}
}
