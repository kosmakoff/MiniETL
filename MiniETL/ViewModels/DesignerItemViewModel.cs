using System;
using System.Collections.Generic;
using MiniETL.Components;

namespace MiniETL.ViewModels
{
	public class DesignerItemViewModel : SelectableDesignerItemViewModelBase
	{
		private double _left, _top;
		private bool _showConnectors;
		private double _height;
		private double _width;

		public DesignerItemViewModel(IDiagramViewModel diagram, ComponentBase component)
			: base(diagram)
		{
			Component = component;

			Init();
		}

		public ComponentBase Component { get; private set; }

		public static double MinHeight = 100;
		public static double MinWidth = 150;

		public double Left
		{
			get { return _left; }
			set
			{
				if (Math.Abs(value - _left) <= 1e-6) return;

				_left = value;
				OnPropertyChanged();
			}
		}

		public double Top
		{
			get { return _top; }
			set
			{
				if (Math.Abs(value - _top) <= 1e-6) return;

				_top = value;
				OnPropertyChanged();
			}
		}

		public double Height
		{
			get { return _height; }
			set
			{
				if (value.Equals(_height)) return;
				_height = value;
				OnPropertyChanged();
			}
		}

		public double Width
		{
			get { return _width; }
			set
			{
				if (value.Equals(_width)) return;
				_width = value;
				OnPropertyChanged();
			}
		}

		public List<FullyCreatedConnectorInfo> InputConnectors { get; private set; }
		public List<FullyCreatedConnectorInfo> OutputConnectors { get; private set; }

		public bool ShowConnectors
		{
			get { return _showConnectors; }
			set
			{
				if (_showConnectors == value) return;

				_showConnectors = value;
				OnPropertyChanged();
			}
		}

		private void Init()
		{
			Component.Init(this);

			InputConnectors = Component.GetInputConnectors();
			OutputConnectors = Component.GetOutputConnectors();

			ShowConnectors = false;
		}
	}
}