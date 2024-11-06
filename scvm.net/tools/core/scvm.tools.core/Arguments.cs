using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace scvm.tools.core
{
	public class Arguments
	{
		public List<string> SingleStringArgument = new List<string>();
		public Dictionary<string, string?> arguments = new Dictionary<string, string?>();
		public List<ArgumentDefinition> Definitions = new List<ArgumentDefinition>();
		public Arguments()
		{
		}
		public bool TryFindDefinition(string name, [MaybeNullWhen(false)] out ArgumentDefinition definition)
		{
			foreach (ArgumentDefinition def in Definitions)
			{
				if (def.Names.Contains(name))
				{
					definition = def;
					return true;
				}
			}
			definition = null;
			return false;
		}
		public void Resolve(string[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				string arg = args[i];
				if (TryFindDefinition(arg, out var definition))
				{
					if (definition.HasValues)
					{
						i++;
						if (i >= arg.Length)
						{
							arguments.Add(definition.id, null);
							continue;
						}
						var next = args[i + 1];
						if (TryFindDefinition(arg, out _))
						{
							arguments.Add(definition.id, null);
						}
						else
						{
							arguments.Add(definition.id, next);
						}
					}
					else
					{
						arguments.Add(definition.id, null);
					}
				}
				else
				{
					SingleStringArgument.Add(arg);
				}
			}
		}
	}
	public class ArgumentDefinition
	{
		public string id;
		public List<string> Names;
		public bool HasValues;
		public bool IsValueOptional;
		public ArgumentDefinition(string id, List<string> names)
		{
			this.id = id;
			Names = names;
		}
	}
}
