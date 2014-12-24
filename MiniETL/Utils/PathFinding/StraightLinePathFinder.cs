using System;
using System.Collections.Generic;
using System.Windows;
using MiniETL.ViewModels;

namespace MiniETL.Utils.PathFinding
{
	public class StraightLinePathFinder : IPathFinder
	{
		private const int Margin = 20;

		public List<Point> GetConnectionLine(ConnectorInfo source, ConnectorInfo sink)
		{
			var lineStart = GetConnectorCenter(source);
			var lineEnd = GetConnectorCenter(sink);

			var lineStartOffset = GetOffsetPoint(lineStart, source.Orientation, Margin);
			var lineEndOffset = GetOffsetPoint(lineEnd, sink.Orientation, Margin);

			return new List<Point> {lineStart, lineStartOffset, lineEndOffset, lineEnd};
		}

		private static Point GetConnectorCenter(ConnectorInfo info)
		{
			return new Point(
				info.DesignerItemLeft + info.Position.X,
				info.DesignerItemTop + info.Position.Y);
		}

		private static Point GetOffsetPoint(Point point, ConnectorOrientation orientation, int offset)
		{
			switch (orientation)
			{
				case ConnectorOrientation.None:
					return point;
				case ConnectorOrientation.Left:
					point.Offset(-offset, 0);
					return point;
				case ConnectorOrientation.Top:
					point.Offset(0, -offset);
					return point;
				case ConnectorOrientation.Right:
					point.Offset(offset, 0);
					return point;
				case ConnectorOrientation.Bottom:
					point.Offset(0, offset);
					return point;
				default:
					throw new ArgumentOutOfRangeException("orientation");
			}
		}

		public List<Point> GetConnectionLine(ConnectorInfo source, Point sinkPoint, ConnectorOrientation preferredOrientation)
		{
			throw new NotImplementedException();
		}
	}
}
