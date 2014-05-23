using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MiniETL.Adorners
{
	public class RotationChrome : Control
	{
		static RotationChrome()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (RotationChrome), new FrameworkPropertyMetadata(typeof (RotationChrome)));
		}
	}
}
