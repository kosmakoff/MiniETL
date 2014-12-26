using System;
using System.Collections.Generic;
using System.Windows;
using MiniETL.ViewModels;

namespace MiniETL.Utils.PathFinding
{
	public class StraightLinePathFinder : IPathFinder
	{
		private const int SmallMargin = 10;
		private const int Margin = 20;

		public List<Point> GetConnectionLine(ConnectorInfo source, ConnectorInfo sink)
		{
			var lineStart = GetOffsetPoint(source.HotspotPosition, source.Orientation, SmallMargin);
			var lineEnd = GetOffsetPoint(sink.HotspotPosition, sink.Orientation, SmallMargin);

			var lineStartOffset = GetOffsetPoint(lineStart, source.Orientation, Margin);
			var lineEndOffset = GetOffsetPoint(lineEnd, sink.Orientation, Margin);
			
			return new List<Point> {lineStart, lineStartOffset, lineEndOffset, lineEnd};
		}

		public List<Point> GetConnectionLine(ConnectorInfo source, Point sinkPoint, ConnectorOrientation preferredOrientation)
		{
			var lineStart = GetOffsetPoint(source.HotspotPosition, source.Orientation, SmallMargin);
			var lineStartOffset = GetOffsetPoint(lineStart, source.Orientation, Margin);

			var lineEnd = sinkPoint;

			return new List<Point>{lineStart, lineStartOffset, lineEnd};
		}

		private static Point GetOffsetPoint(Point point, ConnectorOrientation orientation, int offset)
		{
			switch (orientation)
			{
				case ConnectorOrientation.Left:
					point.Offset(-offset, 0);
					return point;
				case ConnectorOrientation.Right:
					point.Offset(offset, 0);
					return point;
				default:
					throw new ArgumentOutOfRangeException("orientation");
			}
		}
	}
}
