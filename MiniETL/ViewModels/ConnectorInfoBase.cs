using System;
using MiniETL.UI.DiagramDesigner.Controls;

namespace MiniETL.ViewModels
{
	public abstract class ConnectorInfoBase : INPCBase
	{
		public ConnectorKind Kind { get; private set; }
		public ConnectorOrientation Orientation { get; private set; }
		public Type ConnecorDataType { get; private set; }

		protected ConnectorInfoBase(ConnectorKind kind, ConnectorOrientation orientation, Type connecorDataType)
		{
			Kind = kind;
			Orientation = orientation;
			ConnecorDataType = connecorDataType;
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
