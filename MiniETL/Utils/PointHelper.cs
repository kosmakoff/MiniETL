using System;
using System.Windows;
using MiniETL.ViewModels;

namespace MiniETL.Utils
{
	public static class PointHelper
	{
		public static Point GetPointForConnector(FullyCreatedConnectorInfo connector)
		{
			var designerItem = connector.DesignerItem;

			switch (connector.Orientation)
			{
				case ConnectorOrientation.Left:
					return new Point();
				case ConnectorOrientation.Right:
					return new Point();
				default:
					// top and bottom connectors are currently not supported as they are not needed
					throw new ArgumentException(string.Format("Orientation not supported: {0}", connector.Orientation), "connector");
			}
		}
	}
}
