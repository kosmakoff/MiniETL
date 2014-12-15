namespace MiniETL.ViewModels.Components
{
	public class FileOutputComponentDesignerViewModel : DesignerItemViewModel
	{
		public FileOutputComponentDesignerViewModel()
		{

		}

		public FileOutputComponentDesignerViewModel(int id, IDiagramViewModel parent, double left, double top, string fileName)
			: base(id, parent, left, top)
		{
			FileName = fileName;
		}

		public string FileName { get; set; }
	}
}
