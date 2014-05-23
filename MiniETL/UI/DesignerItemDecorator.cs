using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using MiniETL.Adorners;

namespace MiniETL.UI
{
	public class DesignerItemDecorator : Control
	{
		public static readonly DependencyProperty ShowDecoratorProperty =
			DependencyProperty.Register("ShowDecorator", typeof(bool), typeof(DesignerItemDecorator),
				new FrameworkPropertyMetadata(false, ShowDecoratorProperty_Changed));

		private Adorner _adorner;

		public DesignerItemDecorator()
		{
			Unloaded += DesignerItemDecorator_Unloaded;
		}

		public bool ShowDecorator
		{
			get { return (bool)GetValue(ShowDecoratorProperty); }
			set { SetValue(ShowDecoratorProperty, value); }
		}

		private void HideAdorner()
		{
			if (_adorner != null)
			{
				_adorner.Visibility = Visibility.Hidden;
			}
		}

		private void ShowAdorner()
		{
			if (_adorner == null)
			{
				AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);

				if (adornerLayer != null)
				{
					var designerItem = DataContext as ContentControl;
					_adorner = new ResizeRotateAdorner(designerItem);
					adornerLayer.Add(_adorner);

					if (ShowDecorator)
					{
						_adorner.Visibility = Visibility.Visible;
					}
					else
					{
						_adorner.Visibility = Visibility.Hidden;
					}
				}
			}
			else
			{
				_adorner.Visibility = Visibility.Visible;
			}
		}

		private void DesignerItemDecorator_Unloaded(object sender, RoutedEventArgs e)
		{
			if (_adorner != null)
			{
				AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
				if (adornerLayer != null)
				{
					adornerLayer.Remove(_adorner);
				}

				_adorner = null;
			}
		}

		private static void ShowDecoratorProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var decorator = (DesignerItemDecorator)d;
			var showDecorator = (bool)e.NewValue;

			if (showDecorator)
			{
				decorator.ShowAdorner();
			}
			else
			{
				decorator.HideAdorner();
			}
		}
	}
}
