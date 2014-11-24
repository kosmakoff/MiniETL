using System;
using System.Collections.Generic;
using MiniETL.UI.DiagramDesigner.Controls;

namespace MiniETL.ViewModels
{
	public abstract class DesignerItemViewModelBase : SelectableDesignerItemViewModelBase
	{
		private readonly List<FullyCreatedConnectorInfo> _connectors = new List<FullyCreatedConnectorInfo>();
		private double _left, _top;
		private bool _showConnectors;
		private double _itemHeight;
		private double _itemWidth;

		protected DesignerItemViewModelBase(int id, IDiagramViewModel parent, double left, double top)
			: base(id, parent)
		{
			_left = left;
			_top = top;

			Init();
		}

		protected DesignerItemViewModelBase()
		{
			Init();
		}

		public FullyCreatedConnectorInfo TopConnector
		{
			get { return _connectors[0]; }
		}

		public FullyCreatedConnectorInfo BottomConnector
		{
			get { return _connectors[1]; }
		}

		public FullyCreatedConnectorInfo LeftConnector
		{
			get { return _connectors[2]; }
		}

		public FullyCreatedConnectorInfo RightConnector
		{
			get { return _connectors[3]; }
		}

		public double Left
		{
			get { return _left; }
			set
			{
				if (Math.Abs(value - _left) <= 1e-6) return;

				_left = value;
				OnPropertyChanged();
			}
		}

		public double Top
		{
			get { return _top; }
			set
			{
				if (Math.Abs(value - _top) <= 1e-6) return;

				_top = value;
				OnPropertyChanged();
			}
		}

		public double ItemHeight
		{
			get { return _itemHeight; }
			set
			{
				if (value.Equals(_itemHeight)) return;
				_itemHeight = value;
				OnPropertyChanged();
			}
		}

		public double ItemWidth
		{
			get { return _itemWidth; }
			set
			{
				if (value.Equals(_itemWidth)) return;
				_itemWidth = value;
				OnPropertyChanged();
			}
		}

		public bool ShowConnectors
		{
			get { return _showConnectors; }
			set
			{
				if (_showConnectors == value) return;

				_showConnectors = value;
				TopConnector.ShowConnectors = value;
				BottomConnector.ShowConnectors = value;
				LeftConnector.ShowConnectors = value;
				RightConnector.ShowConnectors = value;
				OnPropertyChanged();
			}
		}

		private void Init()
		{
			_connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Top));
			_connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Bottom));
			_connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Left));
			_connectors.Add(new FullyCreatedConnectorInfo(this, ConnectorOrientation.Right));

			ItemHeight = 65;
			ItemWidth = 65;

			ShowConnectors = false;
		}
	}
}