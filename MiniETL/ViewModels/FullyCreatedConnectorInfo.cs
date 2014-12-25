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
		private Point _connectorTopLeftCorner;

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

		public Point ConnectorTopLeftCorner
		{
			get { return _connectorTopLeftCorner; }
			set
			{
				if (value.Equals(_connectorTopLeftCorner)) return;
				_connectorTopLeftCorner = value;
				OnPropertyChanged();
			}
		}

		public Point GetConnectionPoint()
		{
			var returnValue = ConnectorTopLeftCorner;

			// returnValue.Offset(DesignerItem.Left, DesignerItem.Top);

			// offset to connector logical center
			switch (Kind)
			{
				case ConnectorKind.Output:
					returnValue.Offset(2, 8);
					break;
				case ConnectorKind.Input:
					returnValue.Offset(0, 8);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return returnValue;
		}
	}
}