using System;
using MiniETL.UI.DiagramDesigner.Controls;

namespace MiniETL.ViewModels
{
	/// <summary>
	/// Represents the connector that is attached to designer item
	/// </summary>
	public class FullyCreatedConnectorInfo : ConnectorInfoBase
	{
		private bool _visible;

		public FullyCreatedConnectorInfo(DesignerItemViewModel dataItem, ConnectorKind kind, ConnectorOrientation orientation, Type dataType)
			: base(kind, orientation, dataType)
		{
			DataItem = dataItem;
		}

		public DesignerItemViewModel DataItem { get; private set; }

		public bool Visible
		{
			get { return _visible; }
			set
			{
				if (_visible != value)
				{
					_visible = value;
					OnPropertyChanged();
				}
			}
		}
	}
}