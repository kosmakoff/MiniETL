using System.Collections.Generic;
using MiniETL.UI.DiagramDesigner.Controls;
using MiniETL.ViewModels;

namespace MiniETL.Components
{
	public class TextCapitalizerComponent : ComponentBase
	{
		public FullyCreatedConnectorInfo StringInput { get; private set; }
		public FullyCreatedConnectorInfo StringOutput { get; private set; }

		public override List<FullyCreatedConnectorInfo> GetInputConnectors()
		{
			return new List<FullyCreatedConnectorInfo> {StringInput};
		}

		public override List<FullyCreatedConnectorInfo> GetOutputConnectors()
		{
			return new List<FullyCreatedConnectorInfo> {StringOutput};
		}

		public override void Init(DesignerItemViewModel parent)
		{
			base.Init(parent);

			StringInput = new FullyCreatedConnectorInfo(
				Parent, ConnectorKind.Input, ConnectorOrientation.Left, typeof (string));
			StringOutput = new FullyCreatedConnectorInfo(
				Parent, ConnectorKind.Output, ConnectorOrientation.Right, typeof (string));
		}
	}
}
