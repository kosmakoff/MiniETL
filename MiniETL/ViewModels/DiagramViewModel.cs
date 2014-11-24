using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETL.ViewModels
{
	public class DiagramViewModel : INPCBase, IDiagramViewModel
	{
		private readonly ObservableCollection<SelectableDesignerItemViewModelBase> _items =
			new ObservableCollection<SelectableDesignerItemViewModelBase>();
		
		public SimpleCommand AddItemCommand { get; private set; }
		public SimpleCommand RemoveItemCommand { get; private set; }
		public SimpleCommand ClearSelectedItemsCommand { get; private set; }

		public ObservableCollection<SelectableDesignerItemViewModelBase> Items
		{
			get { return _items; }
		}

		public List<SelectableDesignerItemViewModelBase> SelectedItems
		{
			get { return Items.Where(item => item.IsSelected).ToList(); }
		}

		private void ExecuteAddItemCommand(object param)
		{
			
		}

		private void ExecuteRemoveItemCommand(object param)
		{
			
		}

		private void ExecuteClearSelectedItemsCommand(object param)
		{
			var selectedItems = SelectedItems.Select(i => i).ToList();

		}

		private void ExecuteClearAllItemsCommand(object param)
		{
			Items.Clear();
		}
	}
}
