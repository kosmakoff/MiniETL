using System;
using System.Windows;
using MiniETL.UI.DiagramDesigner.Controls;

namespace MiniETL.ViewModels
{
	/// <summary>
	/// Represents the free-floating point under cursor
	/// </summary>
	public class PartCreatedConnectorInfo : ConnectorInfoBase
	{
		private Point _currentLocation;

		public Point CurrentLocation
		{
			get { return _currentLocation; }
			set
			{
				if (value.Equals(_currentLocation)) return;
				_currentLocation = value;
				OnPropertyChanged();
			}
		}

		public PartCreatedConnectorInfo(ConnectorKind kind, Type dataType, Point currentLocation)
			: base(kind, ConnectorOrientation.None, dataType)
		{
			CurrentLocation = currentLocation;
		}
	}
}
