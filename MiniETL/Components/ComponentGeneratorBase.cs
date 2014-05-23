﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETL.Components
{
	public abstract class ComponentGeneratorBase
	{
		public abstract string DisplayName { get; }
		public abstract string Description { get; }

		public abstract ComponentBase GenerateComponent();
	}
}
