namespace MiniETL.Components
{
	public class TextCapitalizerComponentGenerator : ComponentGeneratorBase
	{
		public override string DisplayName
		{
			get { return "Text Capitalizer"; }
		}

		public override string Description
		{
			get { return "Makes every text char BIG"; }
		}

		public override ComponentBase GenerateComponent()
		{
			return new TextCapitalizerComponent();
		}
	}
}
