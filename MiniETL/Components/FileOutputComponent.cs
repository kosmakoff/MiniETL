using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using MiniETL.UI.DiagramDesigner.Controls;
using MiniETL.ViewModels;

namespace MiniETL.Components
{
	public class FileOutputComponent : ComponentBase
	{
		public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(
			"FileName", typeof (string), typeof (FileOutputComponent), new PropertyMetadata(default(string)));

		[DisplayName(@"File Name")]
		public string FileName
		{
			get { return (string) GetValue(FileNameProperty); }
			set { SetValue(FileNameProperty, value); }
		}

		public FullyCreatedConnectorInfo FileContentsInput { get; private set; }

		public override List<FullyCreatedConnectorInfo> GetInputConnectors()
		{
			return new List<FullyCreatedConnectorInfo> {FileContentsInput};
		}

		public override List<FullyCreatedConnectorInfo> GetOutputConnectors()
		{
			return new List<FullyCreatedConnectorInfo>(0);
		}

		public override void Init(DesignerItemViewModel parent)
		{
			base.Init(parent);

			FileContentsInput = new FullyCreatedConnectorInfo(
				Parent, ConnectorKind.Input, ConnectorOrientation.Left, typeof (string));
		}
	}
}