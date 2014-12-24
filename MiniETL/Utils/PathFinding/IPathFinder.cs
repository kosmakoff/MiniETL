using System.Collections.Generic;
using System.Windows;
using MiniETL.ViewModels;

namespace MiniETL.Utils.PathFinding
{
	public interface IPathFinder
	{
		List<Point> GetConnectionLine(ConnectorInfo source, ConnectorInfo sink);
		List<Point> GetConnectionLine(ConnectorInfo source, Point sinkPoint, ConnectorOrientation preferredOrientation);
	}
}
