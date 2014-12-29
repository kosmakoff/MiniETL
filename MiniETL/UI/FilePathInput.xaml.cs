using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace MiniETL.UI
{
	/// <summary>
	/// Interaction logic for FilePathInput.xaml
	/// </summary>
	public partial class FilePathInput : UserControl
	{
		public FilePathInput()
		{
			InitializeComponent();
		}

		public bool ExistingFilesOnly { get; set; }

		public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register(
			"FilePath", typeof (string), typeof (FilePathInput), new PropertyMetadata(default(string)));

		public string FilePath
		{
			get { return (string) GetValue(FilePathProperty); }
			set { SetValue(FilePathProperty, value); }
		}

		private void ButtonBrowseClick(object sender, RoutedEventArgs e)
		{
			var dialog = new OpenFileDialog
			{
				DefaultExt = "*.txt",
				CheckFileExists = true,
				CheckPathExists = true,
				DereferenceLinks = true,
				FileName = FilePath
			};

			if (dialog.ShowDialog() == true)
			{
				FilePath = dialog.FileName;
			}

		}
	}
}
