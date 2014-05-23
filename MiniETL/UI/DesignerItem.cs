using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MiniETL.UI
{
	public class DesignerItem : ContentControl
	{
		public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
			"IsSelected", typeof (bool), typeof (DesignerItem), new FrameworkPropertyMetadata(false));

		public static readonly DependencyProperty MoveThumbTemplateProperty = DependencyProperty.RegisterAttached(
			"MoveThumbTemplate", typeof (ControlTemplate), typeof (DesignerItem), new PropertyMetadata(default(ControlTemplate)));

		static DesignerItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (DesignerItem), new FrameworkPropertyMetadata(typeof (DesignerItem)));
		}

		public DesignerItem()
		{
			Loaded += OnLoaded;
		}

		public bool IsSelected
		{
			get { return (bool) GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}

		public static void SetMoveThumbTemplate(DependencyObject element, ControlTemplate value)
		{
			element.SetValue(MoveThumbTemplateProperty, value);
		}

		public static ControlTemplate GetMoveThumbTemplate(DependencyObject element)
		{
			return (ControlTemplate) element.GetValue(MoveThumbTemplateProperty);
		}

		protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
		{
			base.OnPreviewMouseDown(e);
			var designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;

			if (designer != null)
			{
				if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
				{
					IsSelected = !IsSelected;
				}
				else
				{
					if (!IsSelected)
					{
						designer.DeselectAll();
						IsSelected = true;
					}
				}
			}

			e.Handled = false;
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (Template != null)
			{
				var contentPresenter = Template.FindName("PART_ContentPresenter", this) as ContentPresenter;

				var thumb = Template.FindName("PART_MoveThumb", this) as MoveThumb;

				if (contentPresenter != null && thumb != null)
				{
					var contentVisual = VisualTreeHelper.GetChild(contentPresenter, 0) as UIElement;

					if (contentVisual != null)
					{
						ControlTemplate template = GetMoveThumbTemplate(contentVisual);

						if (template != null)
						{
							thumb.Template = template;
						}
					}
				}
			}
		}
	}
}