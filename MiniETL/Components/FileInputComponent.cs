using System.ComponentModel;
using System.Windows;

namespace MiniETL.Components
{
	public class FileInputComponent : ComponentBase
	{
		public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(
			"FileName", typeof (DependencyProperty), typeof (FileInputComponent), new PropertyMetadata(default(DependencyProperty)));

		[DisplayName(@"File Name")]
		public DependencyProperty FileName
		{
			get { return (DependencyProperty) GetValue(FileNameProperty); }
			set { SetValue(FileNameProperty, value); }
		}
	}
}