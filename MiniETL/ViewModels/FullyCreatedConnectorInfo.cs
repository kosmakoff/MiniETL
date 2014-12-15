using MiniETL.UI.DiagramDesigner.Controls;

namespace MiniETL.ViewModels
{
	public class FullyCreatedConnectorInfo : ConnectorInfoBase
	{
		private bool _showConnectors;

		public FullyCreatedConnectorInfo(DesignerItemViewModel dataItem, ConnectorOrientation orientation) : base(orientation)
		{
			DataItem = dataItem;
		}

		public DesignerItemViewModel DataItem { get; private set; }

		public bool ShowConnectors
		{
			get { return _showConnectors; }
			set
			{
				if (_showConnectors != value)
				{
					_showConnectors = value;
					OnPropertyChanged();
				}
			}
		}
	}
}