using System.Windows;
using MiniETL.ViewModels;

namespace MiniETL.Utils.PathFinding
{
	public struct ConnectorInfo
	{
		public Point HotspotPosition { get; set; }
		public ConnectorOrientation Orientation { get; set; }

		public static ConnectorInfo Create(ConnectorOrientation orientation, Point hotspotPosition)
		{
			return new ConnectorInfo
			{
				Orientation = orientation,
				HotspotPosition = hotspotPosition
			};
		}
	}
}
