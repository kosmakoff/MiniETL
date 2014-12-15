using System;
using System.Collections.Generic;
using MiniETL.Components;
using MiniETL.UI.DiagramDesigner.Controls;

namespace MiniETL.ViewModels
{
	public class DesignerItemViewModel : SelectableDesignerItemViewModelBase
	{
		private readonly List<FullyCreatedConnectorInfo> _connectors = new List<FullyCreatedConnectorInfo>();
		private double _left, _top;
		private bool _showConnectors;
		private double _height;
		private double _width;

		public DesignerItemViewModel(int id, IDiagramViewModel parent, double left, double top)
			: base(id, parent)
		{
			_left = left;
			_top = top;

			Init();
		}

		public DesignerItemViewModel()
		{
			Init();
		}

		public ComponentBase Component { get; set; }

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

		public static double MinHeight = 100;
		public static double MinWidth = 150;

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

		public double Height
		{
			get { return _height; }
			set
			{
				if (value.Equals(_height)) return;
				_height = value;
				OnPropertyChanged();
			}
		}

		public double Width
		{
			get { return _width; }
			set
			{
				if (value.Equals(_width)) return;
				_width = value;
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

			Height = 65;
			Width = 65;

			ShowConnectors = false;
		}
	}
}