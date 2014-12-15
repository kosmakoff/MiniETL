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
	public static class SelectionProps
	{
		public static readonly DependencyProperty EnabledForSelectionProperty = DependencyProperty.RegisterAttached(
			"EnabledForSelection", typeof (bool), typeof (SelectionProps), new FrameworkPropertyMetadata(default(bool), OnEnabledForSelectionChanged));

		public static void SetEnabledForSelection(DependencyObject element, bool value)
		{
			element.SetValue(EnabledForSelectionProperty, value);
		}

		public static bool GetEnabledForSelection(DependencyObject element)
		{
			return (bool) element.GetValue(EnabledForSelectionProperty);
		}

		private static void OnEnabledForSelectionChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			var frameworkElement = (FrameworkElement) dependencyObject;
			if ((bool) dependencyPropertyChangedEventArgs.NewValue)
			{
				frameworkElement.PreviewMouseDown += FrameworkElementPreviewMouseDown;
			}
			else
			{
				frameworkElement.PreviewMouseDown -= FrameworkElementPreviewMouseDown;
			}
		}

		private static void FrameworkElementPreviewMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			var selectableDesignerItemViewModelBase = (SelectableDesignerItemViewModelBase)((FrameworkElement)sender).DataContext;

			if (selectableDesignerItemViewModelBase != null)
			{
				if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
				{
					if ((Keyboard.Modifiers & (ModifierKeys.Shift)) != ModifierKeys.None)
					{
						selectableDesignerItemViewModelBase.IsSelected = !selectableDesignerItemViewModelBase.IsSelected;
					}

					if ((Keyboard.Modifiers & (ModifierKeys.Control)) != ModifierKeys.None)
					{
						selectableDesignerItemViewModelBase.IsSelected = !selectableDesignerItemViewModelBase.IsSelected;
					}
				}
				else if (!selectableDesignerItemViewModelBase.IsSelected)
				{
					foreach (SelectableDesignerItemViewModelBase item in selectableDesignerItemViewModelBase.Parent.SelectedItems)
						item.IsSelected = false;

					selectableDesignerItemViewModelBase.Parent.SelectedItems.Clear();
					selectableDesignerItemViewModelBase.IsSelected = true;
				}
			}
		}
	}
}
