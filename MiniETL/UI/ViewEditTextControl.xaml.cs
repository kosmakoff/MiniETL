using System.Windows;
using System.Windows.Controls;

namespace MiniETL.UI
{
	/// <summary>
	/// Interaction logic for ViewEditTextControl.xaml
	/// </summary>
	public partial class ViewEditTextControl : UserControl
	{
		public static readonly DependencyProperty IsEditModeProperty = DependencyProperty.Register(
			"IsEditMode", typeof(bool), typeof(ViewEditTextControl), new PropertyMetadata(false));

		public bool IsEditMode
		{
			get { return (bool)GetValue(IsEditModeProperty); }
			set { SetValue(IsEditModeProperty, value); }
		}

		public static readonly DependencyProperty EditableTextProperty = DependencyProperty.Register(
			"EditableText", typeof(string), typeof(ViewEditTextControl), new PropertyMetadata(default(string)));

		public string EditableText
		{
			get { return (string)GetValue(EditableTextProperty); }
			set { SetValue(EditableTextProperty, value); }
		}

		public ViewEditTextControl()
		{
			InitializeComponent();
		}

		private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
		{
			IsEditMode = false;
		}

		private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
		{
			IsEditMode = true;
		}
	}
}
