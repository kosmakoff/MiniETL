using System.Windows;
using System.Windows.Input;
using MiniETL.Utils.PathFinding;

namespace MiniETL.ViewModels
{
	public class MainWindowViewModel : INPCBase
	{
		private DiagramViewModel _diagramViewModel;

		public ToolboxViewModel ToolboxViewModel { get; private set; }

		public DiagramViewModel DiagramViewModel
		{
			get { return _diagramViewModel; }
			set
			{
				if (Equals(value, _diagramViewModel)) return;
				_diagramViewModel = value;
				OnPropertyChanged();
			}
		}

		public ICommand ExitApplicationCommand { get; private set; }

		public MainWindowViewModel()
		{
			ToolboxViewModel = new ToolboxViewModel();
			DiagramViewModel = new DiagramViewModel();

			InitCommands();

			ConnectionViewModel.PathFinder = new StraightLinePathFinder();
		}

		private void InitCommands()
		{
			ExitApplicationCommand = new SimpleCommand(ExitApplication);
		}

		private void ExitApplication(object obj)
		{
			Application.Current.Shutdown();
		}
	}
}
