using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace scvm.tools.core
{
	public class Arguments
	{
		public bool WillHaveSingleStringArguments = true;
		public char[] SegmentChars = new char[] { '=', ':' };
		public List<string> SingleStringArgument = new List<string>();
		public Dictionary<string, string?> arguments = new Dictionary<string, string?>();
		public List<ArgumentDefinition> Definitions = new List<ArgumentDefinition>();
		public Func<string, string> DescriptionLookup = DefaultDescriptionLookup;
		public Arguments()
		{
		}
		public bool TryGet(string key, out string? value)
		{
			if (arguments.TryGetValue(key, out value))
			{
				return true;
			}
			value = null;
			if (SingleStringArgument.Contains(key))
			{
				return true;
			}
			return false;
		}
		public void PrintHelp(TextWriter output, string name)
		{
			output.WriteLine("Usage:");
			if (WillHaveSingleStringArguments)
			{
				output.WriteLine($"{name} [options]... [parameter 0] ...");
			}
			else
			{
				output.WriteLine($"{name} [options] ...");
			}
			output.WriteLine("Options:");
			foreach (ArgumentDefinition argument in Definitions)
			{
				for (int i = 0; i < argument.Names.Count; i++)
				{
					string opt_name = argument.Names[i];
					output.Write($"{opt_name}");
					if (i != argument.Names.Count - 1)
					{
						output.Write($", ");
					}
					else
						output.Write($"\t");
				}
				output.WriteLine(DescriptionLookup(argument.Description));
				output.WriteLine();
			}
		}
		public static string DefaultDescriptionLookup(string s) => s;
		public bool TryFindDefinition(string str, [MaybeNullWhen(false)] out ArgumentDefinition definition)
		{
			var id = str.IndexOfAny(SegmentChars);
			if (id >= 0)
			{

			}
			foreach (ArgumentDefinition def in Definitions)
			{
				if (def.Names.Contains(str))
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

						if (i >= args.Length)
						{

							arguments.Add(definition.id, null);
							continue;
						}
						var next = args[i];

						if (TryFindDefinition(next, out _))
						{
							arguments.Add(definition.id, null);
							i--;
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
		public string Description = string.Empty;
		public bool HasValues;
		public bool IsValueOptional;
		public ArgumentDefinition(string id, List<string> names)
		{
			this.id = id;
			Names = names;
		}

		public ArgumentDefinition(string id, List<string> names, string description, bool hasValues, bool isValueOptional) : this(id, names)
		{
			Description = description;
			HasValues = hasValues;
			IsValueOptional = isValueOptional;
		}
	}
}
