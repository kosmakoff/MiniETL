using System.Collections.Generic;
using MiniETL.Components;

namespace MiniETL.ViewModels
{
	public class ToolboxViewModel
	{
		private readonly List<ComponentGeneratorBase> _toolboxItems = new List<ComponentGeneratorBase>();

		public ToolboxViewModel()
		{
			_toolboxItems.Add(new FileInputComponentGenerator());
			_toolboxItems.Add(new FileOutputComponentGenerator());
		}

		public List<ComponentGeneratorBase> ToolboxItems
		{
			get { return _toolboxItems; }
		}
	}
}
