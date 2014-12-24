using System.Collections.Generic;
using System.Windows;
using MiniETL.ViewModels;

namespace MiniETL.Utils.PathFinding
{
	public class OrthogonalPathFinder : IPathFinder
	{
		public List<Point> GetConnectionLine(ConnectorInfo source, ConnectorInfo sink)
		{
			throw new System.NotImplementedException();
		}

		public List<Point> GetConnectionLine(ConnectorInfo source, Point sinkPoint, ConnectorOrientation preferredOrientation)
		{
			throw new System.NotImplementedException();
		}
	}
}