using System.Collections.Generic;
using System.Windows;
using MiniETL.ViewModels;

namespace MiniETL.Components
{
	public abstract class ComponentBase : DependencyObject
	{
		public DesignerItemViewModel Parent { get; private set; }

		public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
			"Name", typeof (string), typeof (ComponentBase), new PropertyMetadata(default(string)));

		public string Name
		{
			get { return (string) GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}

		public virtual void Init(DesignerItemViewModel parent)
		{
			Parent = parent;
		}

		public abstract List<FullyCreatedConnectorInfo> GetInputConnectors();
		public abstract List<FullyCreatedConnectorInfo> GetOutputConnectors();
	}
}
