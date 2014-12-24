using System.Collections.Generic;
using System.Windows;
using MiniETL.Utils;
using MiniETL.Utils.PathFinding;

namespace MiniETL.ViewModels
{
	public class ConnectionViewModel : SelectableDesignerItemViewModelBase
	{
		private FullyCreatedConnectorInfo _sourceConnectorInfo;
		private ConnectorInfoBase _sinkConnectorInfo;
		private Point _sourcePoint;
		private Point _sinkPoint;
		private Rect _area;

		public ConnectionViewModel(IDiagramViewModel parent, FullyCreatedConnectorInfo sourceConnectorInfo,
			FullyCreatedConnectorInfo sinkConnectorInfo) : base(parent)
		{
			Init(sourceConnectorInfo, sinkConnectorInfo);
		}

		public ConnectionViewModel(FullyCreatedConnectorInfo sourceConnectorInfo, ConnectorInfoBase sinkConnectorInfo)
		{
			Init(sourceConnectorInfo, sinkConnectorInfo);
		}

		public static IPathFinder PathFinder { get; set; }

		public bool IsFullConnection
		{
			get { return SinkConnectorInfo is FullyCreatedConnectorInfo; }
		}

		public Rect Area
		{
			get { return _area; }
			set
			{
				if (value.Equals(_area)) return;
				_area = value;
				UpdateConnectionPoints();
				OnPropertyChanged();
			}
		}

		public FullyCreatedConnectorInfo SourceConnectorInfo
		{
			get { return _sourceConnectorInfo; }
			set
			{
				if (Equals(value, _sourceConnectorInfo)) return;
				_sourceConnectorInfo = value;
				SourcePoint = PointHelper.GetPointForConnector(SourceConnectorInfo);
				OnPropertyChanged();
			}
		}

		public ConnectorInfoBase SinkConnectorInfo
		{
			get { return _sinkConnectorInfo; }
			set
			{
				if (Equals(value, _sinkConnectorInfo)) return;
				_sinkConnectorInfo = value;
				OnPropertyChanged();
				OnPropertyChanged(() => IsFullConnection);
			}
		}

		public Point SourcePoint
		{
			get { return _sourcePoint; }
			set
			{
				if (value.Equals(_sourcePoint)) return;
				_sourcePoint = value;
				UpdateArea();
				OnPropertyChanged();
			}
		}

		public Point SinkPoint
		{
			get { return _sinkPoint; }
			set
			{
				if (value.Equals(_sinkPoint)) return;
				_sinkPoint = value;
				UpdateArea();
				OnPropertyChanged();
			}
		}

		public List<Point> ConnectionPoints { get; set; }

		private void Init(FullyCreatedConnectorInfo sourceConnectorInfo, ConnectorInfoBase sinkConnectorInfo)
		{
			Parent = sourceConnectorInfo.DesignerItem.Parent;

			SourceConnectorInfo = sourceConnectorInfo;
			SinkConnectorInfo = sinkConnectorInfo;
		}

		private void UpdateArea()
		{
			Area = new Rect(SourcePoint, SinkPoint);
		}

		private void UpdateConnectionPoints()
		{
			
		}
	}
}
