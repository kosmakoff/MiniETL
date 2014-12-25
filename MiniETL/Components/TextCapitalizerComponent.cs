using System.Collections.Generic;
using MiniETL.ViewModels;

namespace MiniETL.Components
{
	public class TextCapitalizerComponent : ComponentBase
	{
		public FullyCreatedConnectorInfo StringInputConnector { get; private set; }
		public FullyCreatedConnectorInfo StringOutputConnector { get; private set; }

		public override List<FullyCreatedConnectorInfo> GetInputConnectors()
		{
			return new List<FullyCreatedConnectorInfo> {StringInputConnector};
		}

		public override List<FullyCreatedConnectorInfo> GetOutputConnectors()
		{
			return new List<FullyCreatedConnectorInfo> {StringOutputConnector};
		}

		public override void Init(DesignerItemViewModel parent)
		{
			base.Init(parent);

			StringInputConnector = new FullyCreatedConnectorInfo(
				Parent, ConnectorKind.Input, ConnectorOrientation.Left, typeof (string));
			StringOutputConnector = new FullyCreatedConnectorInfo(
				Parent, ConnectorKind.Output, ConnectorOrientation.Right, typeof (string));
		}
	}
}
