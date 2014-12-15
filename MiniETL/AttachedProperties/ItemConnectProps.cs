using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MiniETL.ViewModels;

namespace MiniETL.AttachedProperties
{
	public static class ItemConnectProps
	{
		public static readonly DependencyProperty EnabledForConnectionProperty = DependencyProperty.RegisterAttached(
			"EnabledForConnection", typeof (bool), typeof (ItemConnectProps), new FrameworkPropertyMetadata(default(bool), OnEnabledForConnectionChanged));

		public static void SetEnabledForConnection(DependencyObject element, bool value)
		{
			element.SetValue(EnabledForConnectionProperty, value);
		}

		public static bool GetEnabledForConnection(DependencyObject element)
		{
			return (bool) element.GetValue(EnabledForConnectionProperty);
		}

		private static void OnEnabledForConnectionChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			var frameworkElement = (FrameworkElement) dependencyObject;

			if ((bool) dependencyPropertyChangedEventArgs.NewValue)
			{
				frameworkElement.MouseEnter += FrameworkElementMouseEnter;
				frameworkElement.MouseLeave += FrameworkElementMouseLeave;
			}
			else
			{
				frameworkElement.MouseEnter -= FrameworkElementMouseEnter;
				frameworkElement.MouseLeave -= FrameworkElementMouseLeave;
			}
		}

		private static void FrameworkElementMouseEnter(object sender, MouseEventArgs e)
		{
			var designerItemViewModel = ((FrameworkElement)sender).DataContext as DesignerItemViewModel;
			if (designerItemViewModel != null)
			{
				var designerItem = designerItemViewModel;
				designerItem.ShowConnectors = true;
			}
		}

		private static void FrameworkElementMouseLeave(object sender, MouseEventArgs e)
		{
			var designerItemViewModel = ((FrameworkElement)sender).DataContext as DesignerItemViewModel;
			if (designerItemViewModel != null && e.LeftButton == MouseButtonState.Released)
			{
				var designerItem = designerItemViewModel;
				designerItem.ShowConnectors = false;
			}
		}
	}
}
