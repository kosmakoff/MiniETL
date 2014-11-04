using System.ComponentModel;
using System.Windows;

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
	}
}