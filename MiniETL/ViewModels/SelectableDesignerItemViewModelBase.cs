using System.Collections.Generic;

namespace MiniETL.ViewModels
{
	public interface ISelectItems
	{
		SimpleCommand SelectItemCommand { get; }
	}

	public abstract class SelectableDesignerItemViewModelBase : INPCBase, ISelectItems
	{
		private bool _isSelected;

		protected SelectableDesignerItemViewModelBase(IDiagramViewModel parent)
		{
			Parent = parent;
		}

		public List<SelectableDesignerItemViewModelBase> SelectedItems
		{
			get { return Parent.SelectedItems; }
		}

		public IDiagramViewModel Parent { get; set; }

		public SimpleCommand SelectItemCommand { get; private set; }

		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				if (value.Equals(_isSelected)) return;
				_isSelected = value;
				OnPropertyChanged();
			}
		}

		private void ExecuteSelectItemCommand(object param)
		{
			SelectItem((bool) param, !IsSelected);
		}

		private void SelectItem(bool newSelect, bool select)
		{
			if (newSelect)
			{
				foreach (SelectableDesignerItemViewModelBase designerItemViewModelBase in SelectedItems)
				{
					designerItemViewModelBase._isSelected = false;
				}
			}

			IsSelected = select;
		}

		protected SelectableDesignerItemViewModelBase()
		{
			Init();
		}

		private void Init()
		{
			SelectItemCommand = new SimpleCommand(ExecuteSelectItemCommand);
		}
	}
}
