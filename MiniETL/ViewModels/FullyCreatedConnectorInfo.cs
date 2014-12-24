using System;
using System.Windows;

namespace MiniETL.ViewModels
{
	/// <summary>
	/// Represents the connector that is attached to designer item
	/// </summary>
	public class FullyCreatedConnectorInfo : ConnectorInfoBase
	{
		private bool _visible;
		private Point _connectorHotspot;

		public FullyCreatedConnectorInfo(DesignerItemViewModel designerItem, ConnectorKind kind, ConnectorOrientation orientation, Type dataType)
			: base(kind, orientation, dataType)
		{
			DesignerItem = designerItem;
		}

		public DesignerItemViewModel DesignerItem { get; private set; }

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

		public Point ConnectorHotspot
		{
			get { return _connectorHotspot; }
			set
			{
				if (value.Equals(_connectorHotspot)) return;
				_connectorHotspot = value;
				OnPropertyChanged();
			}
		}

		public Point GetConnectionPoint()
		{
			return ConnectorHotspot;
		}
	}
}