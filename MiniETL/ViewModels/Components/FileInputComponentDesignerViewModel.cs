namespace MiniETL.ViewModels.Components
{
	public class FileInputComponentDesignerViewModel : DesignerItemViewModelBase
	{
		public FileInputComponentDesignerViewModel()
		{

		}

		public FileInputComponentDesignerViewModel(int id, IDiagramViewModel parent, double left, double top, string fileName)
			: base(id, parent, left, top)
		{
			FileName = fileName;
		}

		public string FileName { get; set; }
	}
}
