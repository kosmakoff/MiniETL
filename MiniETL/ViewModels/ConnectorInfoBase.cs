using MiniETL.UI.DiagramDesigner.Controls;

namespace MiniETL.ViewModels
{
	public abstract class ConnectorInfoBase : INPCBase
	{
		public ConnectorOrientation Orientation { get; private set; }

		protected ConnectorInfoBase(ConnectorOrientation orientation)
		{
			Orientation = orientation;
		}

		static ConnectorInfoBase()
		{
			ConnectorHeight = 8;
			ConnectorWidth = 8;
		}

		public static double ConnectorHeight { get; private set; }

		public static double ConnectorWidth { get; private set; }
	}
}
