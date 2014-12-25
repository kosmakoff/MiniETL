using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using MiniETL.Utils.PathFinding;

namespace MiniETL.ViewModels
{
	public class ConnectionViewModel : SelectableDesignerItemViewModelBase
	{
		private FullyCreatedConnectorInfo _sourceConnectorInfo;
		private ConnectorInfoBase _sinkConnectorInfo;
		private Point _sourcePointAbsolute;
		private Point _sinkPointAbsolute;
		private Rect _area;
		private List<Point> _connectionPoints;

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
				SourcePointAbsolute = SourceConnectorInfo.GetConnectionPoint();
				OnPropertyChanged();
				
				PropertyChangedEventManager.AddHandler(SourceConnectorInfo, ConnectorPropertyChanged, string.Empty);
			}
		}

		public ConnectorInfoBase SinkConnectorInfo
		{
			get { return _sinkConnectorInfo; }
			set
			{
				if (Equals(value, _sinkConnectorInfo)) return;
				_sinkConnectorInfo = value;

				var sinkConnectorInfo = SinkConnectorInfo as FullyCreatedConnectorInfo;
				if (sinkConnectorInfo != null)
				{
					SinkPointAbsolute = sinkConnectorInfo.GetConnectionPoint();

					PropertyChangedEventManager.AddHandler(sinkConnectorInfo, ConnectorPropertyChanged, string.Empty);
				}
				else
				{
					SinkPointAbsolute = ((PartCreatedConnectorInfo) SinkConnectorInfo).CurrentLocation;
				}
				
				OnPropertyChanged();
				OnPropertyChanged(() => IsFullConnection);
			}
		}

		private void ConnectorPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "ConnectorTopLeftCorner":
					// TODO: there is no need to update both points, since only one has moved
					UpdateSourceAndSinkPoints();

					break;
			}
		}

		private void UpdateSourceAndSinkPoints()
		{
			_sourcePointAbsolute = SourceConnectorInfo.GetConnectionPoint();
			var sinkConnectorInfo = SinkConnectorInfo as FullyCreatedConnectorInfo;
			if (sinkConnectorInfo != null)
			{
				_sinkPointAbsolute = sinkConnectorInfo.GetConnectionPoint();
			}

			OnPropertyChanged(() => SourcePointAbsolute);
			OnPropertyChanged(() => SinkPointAbsolute);

			UpdateArea();
		}

		public Point SourcePointAbsolute
		{
			get { return _sourcePointAbsolute; }
			set
			{
				if (value.Equals(_sourcePointAbsolute)) return;
				_sourcePointAbsolute = value;
				UpdateArea();
				OnPropertyChanged();
			}
		}

		public Point SinkPointAbsolute
		{
			get { return _sinkPointAbsolute; }
			set
			{
				if (value.Equals(_sinkPointAbsolute)) return;
				_sinkPointAbsolute = value;
				UpdateArea();
				OnPropertyChanged();
			}
		}

		// source and sink points relative to area
		public Point SourcePointRelative { get; set; }
		public Point SinkPointRelative { get; set; }

		public List<Point> ConnectionPoints
		{
			get { return _connectionPoints; }
			set
			{
				if (Equals(value, _connectionPoints)) return;
				_connectionPoints = value;
				OnPropertyChanged();
			}
		}

		private void Init(FullyCreatedConnectorInfo sourceConnectorInfo, ConnectorInfoBase sinkConnectorInfo)
		{
			Parent = sourceConnectorInfo.DesignerItem.Parent;

			SourceConnectorInfo = sourceConnectorInfo;
			SinkConnectorInfo = sinkConnectorInfo;
		}

		private void UpdateArea()
		{
			Area = new Rect(SourcePointAbsolute, SinkPointAbsolute);
		}

		private void UpdateConnectionPoints()
		{
			if (SourceConnectorInfo == null)
				return;

			if (SinkConnectorInfo == null)
				return;

			var relativeSourcePoint = new Point(
				SourcePointAbsolute.X < SinkPointAbsolute.X ? 0d : Area.Width,
				SourcePointAbsolute.Y < SinkPointAbsolute.Y ? 0d : Area.Height);

			var relativeSinkPoint = new Point(
				SourcePointAbsolute.X > SinkPointAbsolute.X ? 0d : Area.Width,
				SourcePointAbsolute.Y > SinkPointAbsolute.Y ? 0d : Area.Height);

			SourcePointRelative = relativeSourcePoint;
			SinkPointRelative = relativeSinkPoint;

			OnPropertyChanged(() => SourcePointRelative);
			OnPropertyChanged(() => SinkPointRelative);

			var sourceInfo = ConnectorInfo.Create(SourceConnectorInfo.Orientation, relativeSourcePoint);

			if (IsFullConnection)
			{
				var fullyCreatedSinkConnector = (FullyCreatedConnectorInfo) SinkConnectorInfo;

				var sinkInfo = ConnectorInfo.Create(fullyCreatedSinkConnector.Orientation, relativeSinkPoint);

				ConnectionPoints = PathFinder.GetConnectionLine(sourceInfo, sinkInfo);
			}
			else
			{
				ConnectionPoints = PathFinder.GetConnectionLine(sourceInfo, relativeSinkPoint, ConnectorOrientation.Left);
			}
		}
	}
}
