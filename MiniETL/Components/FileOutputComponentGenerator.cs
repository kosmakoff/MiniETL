using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETL.Components
{
	public class FileOutputComponentGenerator : ComponentGeneratorBase
	{
		public override string DisplayName
		{
			get { return "File Output"; }
		}

		public override string Description
		{
			get { return "Writes text to file on FS"; }
		}

		public override ComponentBase GenerateComponent()
		{
			return new FileOutputComponent();
		}
	}
}
