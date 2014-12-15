using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace MiniETL.Adorners
{
	public class ResizeAdorner : Adorner
	{
		private readonly ResizeChrome _chrome;
		private readonly VisualCollection _visuals;

		public ResizeAdorner(FrameworkElement designerItem)
			: base(designerItem)
		{
			SnapsToDevicePixels = true;
			_chrome = new ResizeChrome {DataContext = designerItem.DataContext};
			_visuals = new VisualCollection(this) {_chrome};
		}

		protected override int VisualChildrenCount
		{
			get { return _visuals.Count; }
		}

		protected override Size ArrangeOverride(Size arrangeBounds)
		{
			_chrome.Arrange(new Rect(arrangeBounds));
			return arrangeBounds;
		}

		protected override Visual GetVisualChild(int index)
		{
			return _visuals[index];
		}
	}
}