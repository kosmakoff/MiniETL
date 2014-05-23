using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETL.Components
{
	public class FileInputComponentGenerator : ComponentGeneratorBase
	{
		public override string DisplayName
		{
			get { return "File Input"; }
		}

		public override string Description
		{
			get { return "Takes input from file on FS"; }
		}

		public override ComponentBase GenerateComponent()
		{
			return new FileInputComponent();
		}
	}
}
