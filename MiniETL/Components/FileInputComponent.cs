using System.ComponentModel;
using System.Windows;

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
	}
}