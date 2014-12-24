using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using MiniETL.ViewModels;

namespace MiniETL.Components
{
	public class FileInputComponent : ComponentBase
	{
		public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(
			"FileName", typeof (string), typeof (FileInputComponent), new PropertyMetadata(string.Empty));

		[DisplayName(@"File Name")]
		public string FileName
		{
			get { return (string) GetValue(FileNameProperty); }
			set { SetValue(FileNameProperty, value); }
		}

		public FullyCreatedConnectorInfo FileContentsOutputConnector { get; private set; }

		public override List<FullyCreatedConnectorInfo> GetInputConnectors()
		{
			return new List<FullyCreatedConnectorInfo>(0);
		}

		public override List<FullyCreatedConnectorInfo> GetOutputConnectors()
		{
			return new List<FullyCreatedConnectorInfo> {FileContentsOutputConnector};
		}

		public override void Init(DesignerItemViewModel parent)
		{
			base.Init(parent);

			FileContentsOutputConnector = new FullyCreatedConnectorInfo(
				Parent, ConnectorKind.Output, ConnectorOrientation.Right, typeof (string));
		}
	}
}