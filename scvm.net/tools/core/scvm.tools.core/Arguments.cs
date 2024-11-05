using System;
using System.Collections.Generic;

namespace scvm.tools.core
{
	public class Arguments
	{
		public List<string> SingleStringArgument = new List<string>();
		public Dictionary<string, string> arguments = new Dictionary<string, string>();
		public List<ArgumentWithParameter> Definitions = new List<ArgumentWithParameter>();
		public Arguments()
		{
		}
		public void Resolve(string[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				string arg = args[i];
				foreach (ArgumentWithParameter parameter in Definitions)
				{
					if (parameter.Names.Contains(arg))
					{
						if (parameter.HasValues)
						{
						}
					}
				}
			}
		}
	}
	public class ArgumentWithParameter
	{
		public string id;
		public List<string> Names;
		public bool HasValues;
		public bool IsValueOptional;
		public ArgumentWithParameter(string id, List<string> names)
		{
			this.id = id;
			Names = names;
		}
	}
}
