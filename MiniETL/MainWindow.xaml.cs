using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MiniETL.Components;

namespace MiniETL
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ComponentsContainer Components { get; private set; }

		public MainWindow()
		{
			InitializeComponent();

			Components = new ComponentsContainer();
		}
	}

	public class ComponentsContainer : ObservableCollection<ComponentBase>
	{
		private static readonly object Lock = new object();

		private readonly Dictionary<Type, int> _counters = new Dictionary<Type, int>();

		public int GetNextCounter(Type type)
		{
			lock (Lock)
			{
				int value;
				if (_counters.TryGetValue(type, out value))
				{
					value++;
					
				}
				else
				{
					value = 1;
				}

				_counters[type] = value;
				return value;
			}
		}
	}
}
