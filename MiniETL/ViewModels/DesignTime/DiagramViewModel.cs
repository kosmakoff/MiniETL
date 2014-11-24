using MiniETL.ViewModels.Components;

namespace MiniETL.ViewModels.DesignTime
{
	public class DiagramViewModel : ViewModels.DiagramViewModel
	{
		public DiagramViewModel()
			:base()
		{
			Items.Add(new FileInputComponentDesignerViewModel());
			Items.Add(new FileOutputComponentDesignerViewModel());
		}
	}
}
