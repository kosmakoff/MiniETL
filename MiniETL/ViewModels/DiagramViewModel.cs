using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MiniETL.ViewModels
{
	public class DiagramViewModel : INPCBase, IDiagramViewModel
	{
		private static readonly object Lock = new object();
		private readonly Dictionary<Type, int> _counters = new Dictionary<Type, int>();

		private readonly ObservableCollection<SelectableDesignerItemViewModelBase> _items =
			new ObservableCollection<SelectableDesignerItemViewModelBase>();
		
		public ICommand AddItemCommand { get; private set; }
		public ICommand RemoveItemCommand { get; private set; }
		public ICommand ClearSelectedItemsCommand { get; private set; }
		public ICommand CreateNewDiagramCommand { get; private set; }

		public DiagramViewModel()
		{
			AddItemCommand = new SimpleCommand(ExecuteAddItemCommand);
			RemoveItemCommand = new SimpleCommand(ExecuteRemoveItemCommand);
			ClearSelectedItemsCommand = new SimpleCommand(ExecuteClearSelectedItemsCommand);
			CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);
		}

		public ObservableCollection<SelectableDesignerItemViewModelBase> Items
		{
			get { return _items; }
		}

		public List<SelectableDesignerItemViewModelBase> SelectedItems
		{
			get { return Items.Where(item => item.IsSelected).ToList(); }
		}

		public int GetNextCounter(Type type)
		{
			lock (Lock)
			{
				int value;
				if (_counters.TryGetValue(type, out value))
				{
					value++;

				}
				else
				{
					value = 1;
				}

				_counters[type] = value;
				return value;
			}
		}

		private void ExecuteAddItemCommand(object param)
		{
			if (param is SelectableDesignerItemViewModelBase)
			{
				var item = (SelectableDesignerItemViewModelBase) param;
				item.Parent = this;
				Items.Add(item);
			}
		}

		private void ExecuteRemoveItemCommand(object param)
		{
			var item = param as SelectableDesignerItemViewModelBase;
			if (item != null)
			{
				Items.Remove(item);
			}
		}

		private void ExecuteClearSelectedItemsCommand(object param)
		{
			var selectedItems = SelectedItems.Select(i => i).ToList();
			selectedItems.ForEach(item => item.IsSelected = false);
		}

		private void ExecuteCreateNewDiagramCommand(object param)
		{
			Items.Clear();
		}
	}
}
