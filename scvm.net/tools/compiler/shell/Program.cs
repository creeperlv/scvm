using Newtonsoft.Json;
using scvm.core;
using scvm.tools.compiler.core;
using scvm.tools.core;
using System.Text.Json.Serialization;

namespace scvm.tools.compiler.shell;
public class Program
{
	public static void Main(string[] args)
	{
		Arguments arguments = new Arguments();
		arguments.Definitions = [
			new("output", ["-o", "--output"], "Output folder" , true , false),
			new("help", ["-h", "--help"], "Print this help" , false , false),
		];
		arguments.Resolve(args);
		if (arguments.arguments.ContainsKey("help") || args.Length == 0)
		{
			arguments.PrintHelp(Console.Out, "scvm-asm");
		}
		arguments.TryGet("output", out var OutputFolder);
		OutputFolder ??= "./";
		Console.WriteLine("output=" + OutputFolder);
		Compiler compiler = new Compiler();
		ISADefinition definition = DefaultISADefinition.Default;
		foreach (var file in arguments.SingleStringArgument)
		{
			if (!File.Exists(file))
			{
				return;
			}
			using (var fstream = File.OpenRead(file))
			{
				DirectoryInfo di = new DirectoryInfo(file);
				var result = compiler.Assemble(fstream, di.FullName, file, null, definition);
				if (result.HasError())
				{
					foreach (var item in result.Errors)
					{
						Console.WriteLine(item.ToString());
					}
				}
				if (OutputFolder == "/dev/stdout")
				{
					Console.Write(JsonConvert.SerializeObject(result.Result, Formatting.Indented));
					continue;
				}
				var outputFile = Path.Combine(OutputFolder, file + ".obj");
				if (File.Exists(outputFile))
				{
					File.Delete(outputFile);
				}
				File.WriteAllText(outputFile, JsonConvert.SerializeObject(result.Result, Formatting.Indented));
			}
		}
	}
}