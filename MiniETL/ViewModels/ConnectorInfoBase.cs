using System;

namespace MiniETL.ViewModels
{
	public abstract class ConnectorInfoBase : INPCBase
	{
		public ConnectorKind Kind { get; private set; }
		public ConnectorOrientation Orientation { get; private set; }
		public Type ConnectorDataType { get; private set; }

		protected ConnectorInfoBase(ConnectorKind kind, ConnectorOrientation orientation, Type connectorDataType)
		{
			Kind = kind;
			Orientation = orientation;
			ConnectorDataType = connectorDataType;
		}
	}
}
