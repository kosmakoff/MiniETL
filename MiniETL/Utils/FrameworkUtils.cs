using System;
using System.Windows;
using System.Windows.Media;

namespace MiniETL.Utils
{
	public static class FrameworkUtils
	{
		public static T GetParent<T>(Type parentType, DependencyObject dependencyObject) where T : DependencyObject
		{
			while (true)
			{
				DependencyObject parent = VisualTreeHelper.GetParent(dependencyObject);
				if (parent.GetType() == parentType)
					return (T)parent;

				dependencyObject = parent;
			}
		}
	}
}
