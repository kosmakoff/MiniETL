using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace MiniETL.UI
{
	[ContentProperty("Text")]
	public class TextBlockEx : FrameworkElement
	{
		private string _stringToRender = string.Empty;
		private bool _useEllipsis = false;

		public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
			"Text", typeof(string), typeof(TextBlockEx), new FrameworkPropertyMetadata(default(string),
				FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static readonly DependencyProperty BackgroundProperty =
			TextElement.BackgroundProperty.AddOwner(typeof(TextBlockEx));

		[TypeConverter(typeof(BrushConverter))]
		public Brush Background
		{
			get { return (Brush)GetValue(BackgroundProperty); }
			set { SetValue(BackgroundProperty, value); }
		}

		public static readonly DependencyProperty ForegroundProperty =
			TextElement.ForegroundProperty.AddOwner(typeof(TextBlockEx));

		[TypeConverter(typeof(BrushConverter))]
		public Brush Foreground
		{
			get { return (Brush)GetValue(ForegroundProperty); }
			set { SetValue(ForegroundProperty, value); }
		}

		public static readonly DependencyProperty FontWeightProperty =
			TextElement.FontWeightProperty.AddOwner(typeof(TextBlockEx));

		public FontWeight FontWeight
		{
			get { return (FontWeight)GetValue(FontWeightProperty); }
			set { SetValue(FontWeightProperty, value); }
		}

		public static readonly DependencyProperty FontSizeProperty =
			TextElement.FontSizeProperty.AddOwner(typeof(TextBlockEx));

		[TypeConverter(typeof(FontSizeConverter))]
		public double FontSize
		{
			get { return (double)GetValue(FontSizeProperty); }
			set { SetValue(FontSizeProperty, value); }
		}

		public static readonly DependencyProperty FontFamilyProperty =
			TextElement.FontFamilyProperty.AddOwner(typeof(TextBlockEx));

		[TypeConverter(typeof(FontFamilyConverter))]
		public FontFamily FontFamily
		{
			get { return (FontFamily)GetValue(FontFamilyProperty); }
			set { SetValue(FontFamilyProperty, value); }
		}

		public static readonly DependencyProperty FontStyleProperty =
			TextElement.FontStyleProperty.AddOwner(typeof(TextBlockEx));

		[TypeConverter(typeof(FontStyleConverter))]
		public FontStyle FontStyle
		{
			get { return (FontStyle)GetValue(FontStyleProperty); }
			set { SetValue(FontStyleProperty, value); }
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);

			var textToRender = _stringToRender + (_useEllipsis ? "..." : string.Empty);

			var ft = new FormattedText(textToRender, CultureInfo.CurrentCulture, FlowDirection,
				new Typeface(FontFamily, FontStyle, FontWeight,
					FontStretches.Normal), FontSize, Foreground);

			drawingContext.DrawRectangle(Background, null, new Rect(new Size(ActualWidth, ActualHeight)));
			drawingContext.DrawText(ft, new Point(0, 0));
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			_stringToRender = Text ?? string.Empty;
			_useEllipsis = false;

			var ft = new FormattedText(_stringToRender, CultureInfo.CurrentCulture, FlowDirection,
				new Typeface(FontFamily, FontStyle, FontWeight,
					FontStretches.Normal), FontSize, Foreground);

			ft.Trimming = TextTrimming.None;

			while (ft.Width > availableSize.Width)
			{
				_stringToRender = _stringToRender.Substring(0, _stringToRender.Length - 1);
				_useEllipsis = true;

				ft = new FormattedText(_stringToRender + "...", CultureInfo.CurrentCulture, FlowDirection,
					new Typeface(FontFamily, FontStyle, FontWeight,
						FontStretches.Normal), FontSize, Foreground);
			}

			var size = new Size(
				_useEllipsis ? availableSize.Width : ft.Width,
				Math.Min(availableSize.Height, ft.Height));

			return size;
		}
	}
}