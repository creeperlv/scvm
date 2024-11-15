using scvm.core;
using scvm.tools.compiler.core;
using scvm.tools.core;

namespace scvm.tools.compiler.shell;
public class Program
{
	public static void Main(string[] args)
	{
		Arguments arguments = new Arguments();
		arguments.Definitions = [
			new("output", ["-o", "--output"], "Output file" , true , false),
			new("help", ["-h", "--help"], "Print this help" , false , false),
		];
		arguments.Resolve(args);
		if (arguments.arguments.ContainsKey("help") || args.Length == 0)
		{
			arguments.PrintHelp(Console.Out, "scvm-asm");
		}
		arguments.TryGet("output", out var OutputFile);
		Compiler compiler = new Compiler();
		foreach (var file in arguments.SingleStringArgument)
		{
			if (!File.Exists(file))
			{
				return;
			}
			using (var fstream = File.OpenRead(file))
			{
				DirectoryInfo di = new DirectoryInfo(file);
				var result = compiler.Assemble(fstream, di.FullName, file, null);

			}
		}
	}
}